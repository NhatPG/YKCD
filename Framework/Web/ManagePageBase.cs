using System;
using System.Web.UI.WebControls;
using Framework.Helper;

namespace Framework.Web
{
    public class ManagePageBase : PageBase
    {
        protected int? IntId;
        protected long? LongId;
        protected Label HeaderCaption;
        protected Label SmallHeaderCaption;
        protected string CreateTitle = string.Empty;
        protected string UpdateTitle = string.Empty;
        protected string SmallTitle = string.Empty;

        protected virtual void Page_Load(object sender, EventArgs e)
        {
            IntId = "id".ToUrlInteger();
            LongId = "id".ToUrlLong();

            SetDelegateForButtonsControl();
            SetBaseControl();

            if (!IsPostBack)
            {
                BindValueToPageControls();

                if (IntId > 0 || LongId > 0)
                {
                    if (HeaderCaption != null)
                        HeaderCaption.Text = UpdateTitle;
                    ShowObjectInformation();
                }
                else
                {
                    if (HeaderCaption != null)
                        HeaderCaption.Text = CreateTitle;
                    SetDefaultValueOnCreate();
                }

                if (SmallHeaderCaption != null)
                    SmallHeaderCaption.Text = SmallTitle;

                ViewState["ReferrerUrl"] = Request.UrlReferrer?.ToString();
            }
        }

        protected virtual void BindValueToPageControls()
        {

        }

        protected virtual void SetDelegateForButtonsControl()
        {

        }

        protected virtual void SetBaseControl()
        {

        }

        protected virtual void SetDefaultValueOnCreate()
        {

        }

        protected virtual void btnSave_Click(object sender, EventArgs e)
        {
            if (IntId > 0 || LongId > 0)
            {
                UpdateObject();
            }
            else
            {
                CreateNewObject();
            }

            ActionAfterFinish();
        }

        protected virtual void btnBack_Click(object sender, EventArgs e)
        {
            object referrer = ViewState["ReferrerUrl"];
            if (referrer != null)
                Response.Redirect((string)referrer);
        }

        protected virtual void ShowObjectInformation()
        {

        }

        protected virtual void CreateNewObject()
        {

        }

        protected virtual void UpdateObject()
        {
        }

        protected virtual void ActionAfterFinish()
        {

        }
    }
}