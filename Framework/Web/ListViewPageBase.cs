using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using PetaPoco;
using System.Web;

namespace Framework.Web
{
    public class ListViewPageBase : PageBase
    {
        public ListView BaseListView;
        public DataPager BasePager;
        public Label BaseDataNotExistLabel;
        public object BaseCollection;
        public int TotalObjects;
        public int TotalColumns;
        private string _lastGroupNameValue;
        public string GroupColumn;

        private string _listCheckBoxes = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            SetDefaultValueForListView();

            if (!Page.IsPostBack)
            {
                BindOtherValue();
                BindDataToListView();
            }

            if (Session["CallScript"] != null)
            {
                Page.ClientScript.RegisterStartupScript(GetType(), "CallMyFunction", Session["CallScript"].ToString(), true);
                Session["CallScript"] = null;
            }
        }

        protected void Pager_Init(object sender, EventArgs e)
        {
            SetValueForBaseControls();
            SetDefaultValueForPager();
        }

        protected virtual void SetValueForBaseControls()
        {
        }

        protected virtual void BindOtherValue()
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

        protected virtual void SetDefaultValueForPager()
        {
            if (BasePager != null)
            {
                BasePager.PageSize = 15;
                BasePager.PagedControlID = BaseListView.ID;
                BasePager.Attributes["class"] = "btn-group btn-group-sm pager-buttons";

                BasePager.Fields.Add(
                    new NextPreviousPagerField
                    {
                        ShowLastPageButton = false,
                        ShowNextPageButton = false,
                        ShowPreviousPageButton = false,
                        ShowFirstPageButton = true,
                        FirstPageText = "Trang đầu",
                        ButtonType = ButtonType.Button,
                        ButtonCssClass = "btn btn-default",
                        RenderNonBreakingSpacesBetweenControls = false
                    });

                BasePager.Fields.Add(
                    new NumericPagerField
                    {
                        ButtonType = ButtonType.Button,
                        NumericButtonCssClass = "btn btn-default",
                        CurrentPageLabelCssClass = "btn btn-default disabled",
                        NextPreviousButtonCssClass = "btn btn-default",
                        RenderNonBreakingSpacesBetweenControls = false
                    });

                BasePager.Fields.Add(
                    new NextPreviousPagerField
                    {
                        ShowLastPageButton = true,
                        ShowNextPageButton = false,
                        ShowPreviousPageButton = false,
                        ShowFirstPageButton = false,
                        LastPageText = "Trang cuối",
                        ButtonType = ButtonType.Button,
                        ButtonCssClass = "btn btn-default",
                        RenderNonBreakingSpacesBetweenControls = false
                    });
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
                TotalObjects = (int)BaseCollection.GetType().GetProperty("TotalItems").GetValue(BaseCollection, null);
                BaseCollection = BaseCollection.GetType().GetProperty("Items").GetValue(BaseCollection, null);
            }
            else if (BaseCollection?.GetType().GetGenericTypeDefinition() == typeof(List<>) && TotalObjects == 0)
            {
                TotalObjects = (int)BaseCollection.GetType().GetProperty("Count").GetValue(BaseCollection, null);
            }

            if (BaseListView != null && BaseCollection != null && TotalObjects > 0)
            {
                BaseListView.Visible = true;
                BaseListView.DataSource = BaseCollection;
                BaseListView.DataBind();

                if (BasePager != null)
                {
                    BasePager.Visible = (TotalObjects > BasePager.PageSize);
                }
            }
            else if (BaseListView != null)
            {
                BaseListView.Visible = true;
                BaseListView.DataSource = BaseCollection;
                BaseListView.DataBind();

                if (BasePager != null)
                    BasePager.Visible = false;
            }

        }

        protected string AddGroupingRowIfGroupNameHasChanged()
        {
            if (!string.IsNullOrEmpty(GroupColumn) && TotalColumns > 0)
            {
                string currentGroupNameValue = Eval(GroupColumn)?.ToString();

                if (currentGroupNameValue == null || currentGroupNameValue.Length == 0)
                {
                    currentGroupNameValue = "";
                }

                if (_lastGroupNameValue != currentGroupNameValue && !string.IsNullOrEmpty(currentGroupNameValue))
                {
                    _lastGroupNameValue = currentGroupNameValue;
                    return $"<tr class=\"group-row\"><td colspan=\"{TotalColumns}\">{currentGroupNameValue}</td></tr>";
                }
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

        protected void lvData_OnPagePropertiesChanging(object sender, PagePropertiesChangingEventArgs e)
        {
            if (BasePager != null)
            {
                BasePager.SetPageProperties(e.StartRowIndex, e.MaximumRows, false);
                BindDataToListView();
            }
        }

        protected virtual void AdditionActionOnItemDataBound(object sender, ListViewItemEventArgs e)
        {

        }

        protected virtual void btnDelete_OnClick(object sender, EventArgs e)
        {
            foreach (ListViewDataItem item in BaseListView.Items)
            {
                var chk = (HtmlInputCheckBox)item.FindControl("chkItemId");

                if (chk != null && chk.Checked)
                {
                    DeleteAction(chk.Value);
                }
            }

            HttpContext.Current.Response.Redirect(Request.RawUrl, false);            
            HttpContext.Current.ApplicationInstance.CompleteRequest();
        }

        protected virtual void DeleteAction(string id)
        {

        }

        protected virtual void btnSearch_Click(object sender, EventArgs e)
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

        protected void btnReload_OnClick(object sender, EventArgs e)
        {
            HttpContext.Current.Response.Redirect(Request.RawUrl, false);
            HttpContext.Current.ApplicationInstance.CompleteRequest();
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