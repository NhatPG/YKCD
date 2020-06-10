<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/SubAgency.Master" AutoEventWireup="true" CodeBehind="BaoCao_QuanLy.aspx.cs" Inherits="YKCD.SubAgency.Application.SubAgency.BaoCao_QuanLy" %>
<%@ Register Src="~/Controls/ThongTinYKienChiDaoControl.ascx" TagPrefix="uc1" TagName="ThongTinYKienChiDaoControl" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <script>
        //On UpdatePanel Refresh
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        if (prm != null) {
            prm.add_endRequest(function (sender, e) {
                if (sender._postBackSettings.panelsToUpdate != null) {
                    $('.datepicker').datepicker({ format: 'dd/mm/yyyy', autoclose: true });

                    $(".datepicker").click(function () {
                        $(this).datepicker('show');
                    });

                    $('.select2').select2();

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
                }
            });
        };
    </script>
    <uc1:ThongTinYKienChiDaoControl runat="server" ID="ThongTinYKienChiDaoControl" />
    <fieldset>
        <legend>
            <asp:Label ID="lblHeaderCaption" runat="server"></asp:Label></legend>
    </fieldset>
    <div class="form-horizontal">
        <div class="form-group" id="VanBanDaBaoCaoGroup" runat="server" visible="False">
            <label class="col-sm-3 control-label">
                Chọn văn bản báo cáo
            </label>
            <div class="col-sm-9">
                <asp:UpdatePanel ID="VanBanPanel" runat="server">
                    <ContentTemplate>
                        <asp:ListBox ID="VanBanDaBaoCao" runat="server" CssClass="select2 form-control input-sm" Width="100%" SelectionMode="Single" OnSelectedIndexChanged="VanBanDaBaoCao_OnSelectedIndexChanged" AutoPostBack="True"></asp:ListBox>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
        <div class="form-group">
            <label class="col-sm-3 control-label">
                Nội dung thực hiện <span class="error">(*)</span>
            </label>
            <div class="col-sm-9">
                <asp:UpdatePanel ID="NoiDungThucHienPanel" runat="server">
                    <ContentTemplate>
                        <asp:TextBox ID="NoiDungThucHien" runat="server" CssClass="form-control input-sm required" TextMode="MultiLine" Rows="3"></asp:TextBox>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="VanBanDaBaoCao" EventName="SelectedIndexChanged" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
        </div>
        <div class="form-group">
            <label class="col-sm-3 control-label">
                Trạng thái thực hiện <span class="error">(*)</span>
            </label>
            <div class="col-sm-3">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <asp:DropDownList ID="TrangThai" runat="server" CssClass="form-control select2" Width="100%">
                            <Items>
                                <asp:ListItem Text="Chưa thực hiện" Value="0"></asp:ListItem>
                                <asp:ListItem Text="Đang thực hiện" Value="1"></asp:ListItem>
                                <asp:ListItem Text="Đã hoàn thành" Value="2"></asp:ListItem>
                            </Items>
                        </asp:DropDownList>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="VanBanDaBaoCao" EventName="SelectedIndexChanged" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
            <label class="col-sm-3 control-label">
                Thực hiện
            </label>
            <div class="col-sm-3">
                <asp:ListBox ID="ThucHien" runat="server" CssClass="select2 form-control input-sm" Width="100%" SelectionMode="Multiple" Rows="3"></asp:ListBox>
            </div>
        </div>
        <div class="form-group">
            <label class="col-sm-3 control-label">
                Thời gian thực hiện <span class="error">(*)</span>
            </label>
            <div class="col-sm-3">
                <asp:UpdatePanel ID="ThoiGianThucHienPanel" runat="server">
                    <ContentTemplate>
                        <asp:TextBox ID="ThoiGianThucHien" runat="server" CssClass="form-control input-sm datepicker required"></asp:TextBox>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="VanBanDaBaoCao" EventName="SelectedIndexChanged" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
            <label class="col-sm-3 control-label">
                File đính kèm
            </label>
            <div class="col-sm-3">
                <div class="fileupload fileupload-new" data-provides="fileupload">
                    <span class="btn btn-success btn-file btn-sm"><span class="fileupload-new"><span class="glyphicon glyphicon-open" aria-hidden="true"></span>
                        Chọn file...</span>
                        <span class="fileupload-exists">Thay đổi</span>
                        <input id="FileDinhKem" type="file" runat="server" /></span>
                    <span class="fileupload-preview"></span>
                    <a href="#" class="close fileupload-exists" data-dismiss="fileupload" style="float: none">×</a>
                </div>
            </div>
        </div>
        <div class="form-group">
            <div class="col-sm-offset-3 col-sm-9">
                <asp:LinkButton ID="btnSave" runat="server" Text="<span class='glyphicon glyphicon-floppy-disk'></span>&nbsp;Lưu"
                    CssClass="btn btn-success" OnClick="btnSave_Click" OnClientClick="return CheckHaveFile();" />
            </div>
        </div>
    </div>
    <div class="modal fade" tabindex="-1" role="dialog" id="thong-bao-chua-chon-file">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title">Thông báo chưa có file đính kèm!</h4>
                </div>
                <div class="modal-body">
                    <p>Yêu cầu đính kèm file báo cáo !!!</p>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-primary btn-sm" data-dismiss="modal">Đồng ý</button>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
