﻿using System;
using System.Collections.Generic;
using System.Reflection;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using PetaPoco;

namespace Framework.Web
{
    public class ListViewControlBase : ControlBase
    {
        public ListView BaseListView;

        public Label BaseDataNotExistLabel;
        public object BaseCollection;
        public int TotalObjects;
        public int TotalColumns;
        private string _lastGroupNameValue;

        private string _listCheckBoxes = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            SetValueForBaseControls();
            SetDefaultValueForListView();

            if (!Page.IsPostBack)
            {
                BindDataToListView();
            }
        }

        protected virtual void SetValueForBaseControls()
        {
        }

        protected virtual void SetDefaultValueForListView()
        {
            if (BaseListView != null)
            {
                BaseListView.ItemPlaceholderID = "itemPlaceHolder";
                BaseListView.ItemDataBound += lvData_OnItemDataBound;
            }
        }

        /// <summary>
        /// Lấy dữ liệu cho listview
        /// </summary>
        protected virtual void GetDataList()
        {
        }

        protected virtual void LoadOtherControl()
        {
        }

        protected virtual void BindDataToListView()
        {
            //Lấy danh sách dữ liệu bind lên Gridview
            GetDataList();
            _listCheckBoxes = "[";

            if (BaseCollection?.GetType().GetGenericTypeDefinition() == typeof(Page<>) && TotalObjects == 0)
            {
                PropertyInfo TotalItems = BaseCollection.GetType().GetProperty("TotalItems");
                TotalObjects = (int)TotalItems.GetValue(BaseCollection, null);

                PropertyInfo Items = BaseCollection.GetType().GetProperty("Items");
                BaseCollection = Items.GetValue(BaseCollection, null);
            }
            else if (BaseCollection?.GetType().GetGenericTypeDefinition() == typeof(List<>) && TotalObjects == 0)
            {
                PropertyInfo TotalItems = BaseCollection.GetType().GetProperty("Count");
                TotalObjects = (int)TotalItems.GetValue(BaseCollection, null);
            }

            if (BaseListView != null && BaseCollection != null && TotalObjects > 0)
            {
                BaseListView.Visible = true;
                BaseListView.DataSource = BaseCollection;
                BaseListView.DataBind();

                if (BaseDataNotExistLabel != null)
                    BaseDataNotExistLabel.Visible = false;
            }
            else
            {
                if (BaseListView != null)
                    BaseListView.Visible = false;

                if (BaseDataNotExistLabel != null)
                    BaseDataNotExistLabel.Visible = true;
            }

        }

        protected string AddGroupingRowIfGroupNameHasChanged()
        {
            string currentGroupNameValue = Eval("GroupName").ToString();

            if (currentGroupNameValue.Length == 0)
            {
                currentGroupNameValue = "";
            }

            if (_lastGroupNameValue != currentGroupNameValue && !string.IsNullOrEmpty(currentGroupNameValue))
            {
                _lastGroupNameValue = currentGroupNameValue;
                return $"<tr class=\"group-row\"><td colspan=\"{TotalColumns}\">{currentGroupNameValue}</td></tr>";
            }

            return string.Empty;
        }

        protected void lvData_OnItemDataBound(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                var checkbox = (HtmlInputCheckBox)e.Item.FindControl("chkItemId");

                if (checkbox != null)
                {
                    _listCheckBoxes += $"'{checkbox.ClientID}',";
                }
            }

            if (e.Item.DataItemIndex == TotalObjects - 1)
            {
                if (_listCheckBoxes.Length > 1)
                {
                    _listCheckBoxes = _listCheckBoxes.Remove(_listCheckBoxes.Length - 1, 1);
                }

                _listCheckBoxes += "];\r\n";
                Page.ClientScript.RegisterStartupScript(GetType(), "VAR", "var listCheckBoxes = " + _listCheckBoxes,
                    true);
            }

            AdditionActionOnItemDataBound(sender, e);
        }

        protected virtual void AdditionActionOnItemDataBound(object sender, ListViewItemEventArgs e)
        {

        }

        protected virtual void btnDelete_OnClick(object sender, EventArgs e)
        {
            try
            {
                foreach (ListViewDataItem item in BaseListView.Items)
                {
                    var chk = (HtmlInputCheckBox)item.FindControl("chkItemId");

                    if (chk != null && chk.Checked)
                    {
                        DeleteAction(chk.Value);
                    }
                }

                Response.Redirect(Request.RawUrl);
            }
            catch (Exception ex)
            {
                WriteLog(ex);
            }
        }

        protected virtual void DeleteAction(string id)
        {

        }

        protected virtual void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                var txtSearchText = FindTextBox(this, "txtSearchText");

                if (txtSearchText != null)
                {
                    BindDataToListView();

                    LinkButton btnReload = FindLinkButton(this, "btnReload");

                    if (btnReload != null)
                    {
                        RemoveCssClass(btnReload, "display-none");
                    }
                }
            }
            catch (Exception ex)
            {
                WriteLog(ex);
            }
        }

        protected void btnReload_OnClick(object sender, EventArgs e)
        {
            Response.Redirect(Request.RawUrl);
        }

        public void RemoveCssClass(WebControl control, string css)
        {
            control.CssClass = control.CssClass.Replace(css, "");
        }

        public void AddCssClass(WebControl control, string css)
        {
            control.CssClass += css;
        }

        private static Control FindControlIterative(Control root, string id)
        {
            var ctl = root;

            var ctls = new LinkedList<Control>();

            while (ctl != null)
            {
                if (ctl.ID == id) return ctl;

                foreach (Control child in ctl.Controls)
                {
                    if (child.ID == id)
                        return child;
                    if (child.Controls.Count > 0)
                        ctls.AddLast(child);
                }

                if (ctls.Count > 0)
                {
                    ctl = ctls.First.Value;
                    ctls.Remove(ctl);
                }
                else
                    ctl = null;
            }
            return null;
        }

        private static TextBox FindTextBox(Control root, string id)
        {
            return (TextBox)FindControlIterative(root, id);
        }

        private static LinkButton FindLinkButton(Control root, string id)
        {
            return (LinkButton)FindControlIterative(root, id);
        }

        protected virtual void SearchAction()
        {
        }
    }
}