<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/SubAgency.Master" AutoEventWireup="true" CodeBehind="VanBan_QuanLy.aspx.cs" Inherits="YKCD.SubAgency.Application.SubAgency.VanBan_QuanLy" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" />
    <script>
        //On UpdatePanel Refresh
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        if (prm != null) {
            prm.add_endRequest(function (sender, e) {
                if (sender._postBackSettings.panelsToUpdate != null) {
                    $.validator.setDefaults({
                        highlight: function (element) {
                            $(element).closest('.form-group').addClass('has-error');
                        },
                        unhighlight: function (element) {
                            $(element).closest('.form-group').removeClass('has-error');
                        },
                        errorElement: 'span',
                        errorClass: 'help-block',
                        errorPlacement: function (error, element) {
                            if (element.parent('.input-group').length) {
                                error.insertAfter(element.parent());
                            } else {
                                error.insertAfter(element);
                            }
                        }
                    });
                    $.validator.messages.required = '';

                    $('form:first').validate();

                    $('.select2').select2();

                    $('.datepicker').datepicker({ language: "vi", format: 'dd/mm/yyyy', autoclose: true, todayHighlight: true });

                    $(".datepicker").click(function () {
                        $(this).datepicker('show');
                    });

                    var groups = {};
                    $("select option[data-category]").each(function () {
                        groups[$.trim($(this).attr("data-category"))] = true;
                    });
                    $.each(groups, function (c) {
                        $("select option[data-category='" + c + "']").wrapAll('<optgroup label="' + c + '">');
                    });
                }
            });
        };
    </script>
    <fieldset>
        <legend>
            <asp:Label ID="lblHeaderCaption" runat="server"></asp:Label></legend>
    </fieldset>
    <div class="form-horizontal">
        <div class="form-group">
            <label class="col-sm-3 control-label">
                Số hiệu văn bản <span class="error">(*)</span>
            </label>
            <div class="col-sm-3">
                <asp:TextBox ID="SoHieuVanBan" runat="server" CssClass="form-control input-sm required"></asp:TextBox>
            </div>
            <label class="col-sm-3 control-label">
                Ngày ban hành <span class="error">(*)</span>
            </label>
            <div class="col-sm-3">
                <asp:TextBox ID="NgayBanHanh" runat="server" CssClass="form-control input-sm datepicker required"></asp:TextBox>
            </div>
        </div>
        <div class="form-group">
            <label class="col-sm-3 control-label">
            </label>
            <div class="col-sm-9">
                <asp:UpdatePanel ID="VanBanNgoaiUP" runat="server">
                    <ContentTemplate>
                        <div class="checkbox checkbox-primary">
                            <asp:CheckBox ID="VanBanNgoai" runat="server" AutoPostBack="true" OnCheckedChanged="VanBanNgoai_CheckedChanged" />
                            <label for="<%= VanBanNgoai.ClientID %>">
                                Văn bản ngoài đơn vị ban hành
                            </label>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
        <asp:UpdatePanel ID="NguoiKyUP" runat="server">
            <ContentTemplate>
                <div class="form-group">
                    <label class="col-sm-3 control-label">
                        Người ký <span class="error">(*)</span>
                    </label>
                    <div class="col-sm-3">
                        <asp:TextBox ID="TenNguoiKy" runat="server" CssClass="form-control input-sm" Width="100%" Visible="false"></asp:TextBox>
                        <asp:ListBox ID="NguoiKy" runat="server" CssClass="select2 form-control input-sm" Width="100%"></asp:ListBox>
                    </div>
                    <asp:PlaceHolder ID="SoanThaoPH" runat="server">
                        <label class="col-sm-3 control-label">
                            Người soạn thảo <span class="error">(*)</span>
                        </label>
                        <div class="col-sm-3">
                            <asp:ListBox ID="NguoiSoanThao" runat="server" CssClass="select2 form-control input-sm" Width="100%"></asp:ListBox>
                        </div>
                    </asp:PlaceHolder>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="VanBanNgoai" EventName="CheckedChanged" />
            </Triggers>
        </asp:UpdatePanel>
        <div class="form-group">
            <label class="col-sm-3 control-label">
                Trích yếu <span class="error">(*)</span>
            </label>
            <div class="col-sm-9">
                <asp:TextBox ID="TrichYeu" runat="server" CssClass="form-control input-sm required" TextMode="MultiLine" Rows="5"></asp:TextBox>
            </div>
        </div>
        <div class="form-group">
            <label class="col-sm-3 control-label">
                Loại văn bản <span class="error">(*)</span>
            </label>
            <div class="col-sm-3">
                <asp:ListBox ID="LoaiVanBan" runat="server" CssClass="select2 form-control input-sm" Width="100%"></asp:ListBox>
            </div>
            <label class="col-sm-3 control-label">
            </label>
            <div class="col-sm-3">
                <div class="fileupload fileupload-new" data-provides="fileupload">
                    <span class="btn btn-success btn-file btn-sm"><span class="fileupload-new"><span class="glyphicon glyphicon-paperclip" aria-hidden="true"></span>
                        Đính kèm file...</span>
                        <span class="fileupload-exists">Thay đổi</span>
                        <input id="FileDinhKem" type="file" runat="server" /></span>
                    <span class="fileupload-preview"></span>
                    <a href="#" class="close fileupload-exists" data-dismiss="fileupload" style="float: none">×</a>
                </div>
            </div>
        </div>
        <div class="form-group">
            <div class="col-sm-offset-6 col-sm-6">
                <asp:LinkButton ID="btnSave" runat="server" Text="<span class='glyphicon glyphicon-floppy-disk'></span>&nbsp;Lưu"
                    CssClass="btn btn-success btn-sm" OnClick="btnSave_Click" OnClientClick="return validateForm();" />
                <a class="btn btn-default btn-sm" href="<%= Redirector.GetLink(ViewNames.SubAgency.YKCD_QuanLy) %>">
                    <span class="glyphicon glyphicon-comment"></span>&nbsp;Nhập YKCD không có văn bản
                </a>
            </div>
        </div>
    </div>
</asp:Content>
