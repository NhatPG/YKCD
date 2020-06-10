using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;

namespace Framework.Helper
{
    public static class Form
    {
        public static void SelectByValue(this DropDownList dropdownlist, object value)
        {
            if (dropdownlist != null && value != null)
            {
                if (dropdownlist.Items.FindByValue(dropdownlist.SelectedValue) != null)
                {
                    dropdownlist.Items.FindByValue(dropdownlist.SelectedValue).Selected = false;
                }

                if (dropdownlist.Items.FindByValue(value.ToString()) != null)
                {
                    dropdownlist.Items.FindByValue(value.ToString()).Selected = true;
                }
            }
        }

        public static void SelectByValue(this ListBox listBox, object value, bool multipleChoice = false)
        {
            if(value != null)
            {
                if (listBox.Items.FindByValue(listBox.SelectedValue) != null && !multipleChoice)
                {
                    listBox.Items.FindByValue(listBox.SelectedValue).Selected = false;
                }

                if (listBox.Items.FindByValue(value.ToString()) != null)
                {
                    listBox.Items.FindByValue(value.ToString()).Selected = true;
                }
            }            
        }

        public static void SelectByValue(this RadioButtonList radioButtonList, string value)
        {
            foreach (ListItem item in radioButtonList.Items)
            {
                if (value.Equals(item.Value))
                {
                    item.Selected = true;
                }
            }
        }

        public static void BindData(this DropDownList DropDownList, object Collection, string ValueField, string TextField, string TextChoosing = "")
        {
            DropDownList.Items.Clear();

            if (Collection != null)
            {
                DropDownList.DataValueField = ValueField;
                DropDownList.DataTextField = TextField;
                DropDownList.DataSource = Collection;
                DropDownList.DataBind();
            }            

            if (!string.IsNullOrEmpty(TextChoosing))
            {
                DropDownList.Items.Add(new ListItem(TextChoosing, string.Empty));
                DropDownList.SelectByValue(string.Empty);
            }
        }

        public static void BindData(this ListBox listBox, object collection, string valueField, string textField, string groupField = null)
        {
            listBox.Items.Clear();

            foreach (var item in (IList) collection)
            {
                if (string.IsNullOrEmpty(groupField))
                    listBox.AddSelectItem(item?.GetType()?.GetProperty(textField)?.GetValue(item, null)?.ToString(), item?.GetType()?.GetProperty(valueField)?.GetValue(item, null)?.ToString());
                else
                {
                    listBox.AddSelectItem(item?.GetType()?.GetProperty(textField)?.GetValue(item, null)?.ToString(), item?.GetType()?.GetProperty(valueField)?.GetValue(item, null)?.ToString(), item?.GetType()?.GetProperty(groupField)?.GetValue(item, null)?.ToString());
                }                    
            }
        }

        public static void SelectAll(this ListBox listBox)
        {
            foreach (ListItem item in listBox.Items)
            {
                item.Selected = true;
            }
        }

        public static void AddSelectItem(this ListBox listBox, string title, string value, string group = null)
        {
            ListItem item = new ListItem(title, value);

            if (!string.IsNullOrEmpty(group))
            {
                item.Attributes["data-category"] = group;
            }
            listBox.Items.Add(item);
        }

        public static void BindData(this ListView ListView, object Collection)
        {
            ListView.DataSource = Collection;
            ListView.DataBind();
        }

        public static void BindData(this Repeater Repeater, object Collection)
        {
            Repeater.DataSource = Collection;
            Repeater.DataBind();
        }

        public static void BindData(this DetailsView DetailsView, object Object)
        {
            DetailsView.DataSource = Object;
            DetailsView.DataBind();
        }

        public static IEnumerable<ListItem> GetSelectedItems(
           this ListItemCollection items)
        {
            return items.OfType<ListItem>().Where(item => item.Selected);
        }

        public static List<int> GetSelectedValues(this ListBox listBox)
        {
            List<int> result = new List<int>();

            foreach (var index in listBox.GetSelectedIndices())
            {
                result.Add(listBox.Items[index].Value.ToInteger());
            }

            return result;
        }
    }
}