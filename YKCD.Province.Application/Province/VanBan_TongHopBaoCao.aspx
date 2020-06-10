<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Province.Master" AutoEventWireup="true" CodeBehind="VanBan_TongHopBaoCao.aspx.cs" Inherits="YKCD.Province.Application.Province.VanBan_TongHopBaoCao" %>
<%@ Register Src="~/Controls/ThongTinVanBanControl.ascx" TagPrefix="uc1" TagName="ThongTinVanBanControl" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="<%=Page.ResolveClientUrl("~/tinymce/tinymce.min.js")%>" type="text/javascript"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <uc1:ThongTinVanBanControl runat="server" ID="ThongTinVanBanControl" />
    <fieldset>
        <legend>
            BÁO CÁO TỔNG HỢP TÌNH HÌNH THỰC HIỆN</legend>
    </fieldset>
    <div class="form-horizontal">
        <div class="form-group">
            <div class="col-sm-12">
                <asp:TextBox ID="txtBaoCaoTongHop" runat="server" CssClass="form-control input-sm required" TextMode="MultiLine" Rows="6"></asp:TextBox>
            </div>
        </div>
        <div class="form-group">
            <div class="col-sm-offset-3 col-sm-9">
                <asp:LinkButton ID="btnSave" runat="server" Text="<span class='glyphicon glyphicon-floppy-disk'></span>&nbsp;Lưu"
                    CssClass="btn btn-success btn-sm" OnClick="btnSave_OnClick" />
                <asp:LinkButton ID="btnExport" runat="server" Text="<span class='glyphicon glyphicon-floppy-disk'></span>&nbsp;Tải về"
                    CssClass="btn btn-default btn-sm" OnClick="btnExport_Click" />
            </div>
        </div>
    </div>
    <script type="text/javascript">
    var cleanHTML = function (input) {
        console.log("BEFORE >");
        console.log(input);

        // 1. remove line breaks / Mso classes
        var stringStripper = /(\n|\r| class=(")?Mso[a-zA-Z]+(")?)/g;
        var output = input.replace(stringStripper, ' ');

        console.log("STEP 1 >");
        console.log(output);

        // 2. strip Word generated HTML comments
        var commentSripper = new RegExp('<!--(.*?)-->', 'g');
        var output = output.replace(commentSripper, '');

        console.log("STEP 2 >");
        console.log(output);

        // 3. remove tags leave content if any
        var tagStripper = new RegExp('<(\/)*(title|meta|link|span|\\?xml:|st1:|o:|font)(.*?)>', 'gi');
        output = output.replace(tagStripper, '');

        console.log("STEP 3 >");
        console.log(output);

        // 4. Remove everything in between and including tags '<style(.)style(.)>'
        var badTags = ['style', 'script', 'applet', 'embed', 'noframes', 'noscript'];

        for (var i = 0; i < badTags.length; i++) {
            var tagStripper = new RegExp('<' + badTags[i] + '.*?' + badTags[i] + '(.*?)>', 'gi');
            output = output.replace(tagStripper, '');
        }

        console.log("STEP 4 >");
        console.log(output);

        // A different attempt
        //output = (output).replace(/font-family\:[^;]+;?|font-size\:[^;]+;?|line-height\:[^;]+;?/g, '');

        // 5. remove attributes ' style="..."'
        var badAttributes = ['start', 'align'];
        for (var i = 0; i < badAttributes.length; i++) {
            var attributeStripper = new RegExp(' ' + badAttributes[i] + '="(.*?)"', 'gi');
            output = output.replace(attributeStripper, '');
        }

        console.log("STEP 5 >");
        console.log(output);

        return output;
    };

    tinymce.init({
        selector: "textarea", theme: "modern", width: '100%', height: 200,
        plugins: [
            "advlist autolink link image lists charmap print preview hr anchor pagebreak",
            "searchreplace wordcount visualblocks visualchars insertdatetime media nonbreaking",
            "table contextmenu directionality emoticons paste textcolor"
        ],
        toolbar1: "bold italic underline | alignleft aligncenter alignright alignjustify | bullist numlist | responsivefilemanager | link | image | forecolor",
        convert_fonts_to_spans: true,
        paste_word_valid_elements: "b,strong,i,em,h1,h2,u,p,ol,ul,li,a[href],span,color,font-size,font-color,font-family,mark,table,tr,td",
        paste_retain_style_properties: "all",
        image_advtab: true,
        paste_as_text: false,
        setup: function (editor) {
            // Register tooltip button
            editor.addButton('custom_tooltip', {
                text: 'Tooltip',
                title: 'Add a tool tip to the selected text.',
                onclick: function () {
                    editor.windowManager.open({
                        title: 'Insert Tooltip',
                        body: [{
                            type: 'textbox',
                            name: 'tooltipText',
                            label: 'Tooltip Text',
                            value: ''
                        }],
                        onsubmit: function (e) {
                            var title = e.data.tooltipText;
                            var content = editor.selection.getContent();
                            editor.insertContent('<span class="tooltip" title="' + title + '">' + content + '</span>');
                        }
                    });
                }
            });
        }
    });
</script>
</asp:Content>
