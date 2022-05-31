<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="JC39Chubunrui.aspx.cs" Inherits="jobzcolud.WebFront.JC39Chubunri" ValidateRequest="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <link href="../Content/bootstrap.css" rel="stylesheet" />
    <link href="../Style/StyleJC.css" rel="stylesheet" />
    <link href="../Icons/font/bootstrap-icons.css" rel="stylesheet" />
    <asp:PlaceHolder runat="server">
        <%: Scripts.Render("~/bundles/modernizr") %>
        <%: Styles.Render("~/style/StyleBundle1") %>
        <%: Scripts.Render("~/scripts/ScriptBundle1") %>
    </asp:PlaceHolder>
    <webopt:BundleReference runat="server" Path="~/Content/bootstrap" />
    <style>
        .editBtns{
            left:260px!important;
        }
    </style>
</head>
<body class="fontcss bg-transparent" ><%--style="background-color: #11ffee00;"--%>
    <div class="J06Div RadiusIframe pl-4 pr-4 " ">
        <form id="form1" runat="server">

            <asp:ScriptManager ID="ScriptManager1" EnablePageMethods="True" runat="server">
                <Scripts>
                    <asp:ScriptReference Name="jquery" />
                    <asp:ScriptReference Name="bootstrap" />
                    <asp:ScriptReference Path="../Scripts/Common/FixFocus.js" />
                </Scripts>
            </asp:ScriptManager>
            <asp:PlaceHolder runat="server">
                <%: Scripts.Render("~/bundles/jqueryui") %>
            </asp:PlaceHolder>

          
          <div style="width: 470px;max-height:750px; left: 50%; top: 50%; position: absolute; transform: translate(-50%, -50%); background-color: #fff;">
                <%-- height: 50vh;--%>
                <asp:UpdatePanel ID="updChuubunruiList" runat="server" UpdateMode="Conditional" DefaultButton="btnHiddenSubmit">
                    <ContentTemplate>

                        <asp:Button ID="btnHiddenSubmit" runat="server" CssClass="DefaultBtn" />
                        <div >
                            <div class="row" style="display: flex; justify-content: center; align-items: center">
                                <asp:Label ID="lblHeader" runat="server" CssClass="shiaraititleStyle fw-bold txt-style" Text="中分類" />
                                <asp:Button ID="btnHeaderCross" runat="server" Text="✕" CssClass="PopupCloseBtn mr-n3 JC08btnHeaderCross" OnClick="btnCross_Click" />
                            </div>
                            <div class="titleLine"></div>
                            <div class="row divmargin">
                                <div class="col-sm-12">
                                    <asp:Label ID="Label1" runat="server" CssClass="shiaraititleStyle txt-style" Style="margin-left:15px;margin-right:8px" Text="大分類　：" />
                                    <asp:Label ID="lblDaibunrui" runat="server" CssClass="txt-style" />
                                    <asp:TextBox ID="txt_daibunrui" runat="server" CssClass="form-control TextboxStyle " Style="width: 50px; border-color: lightgray; display: none"></asp:TextBox>

                                </div>
                            </div>
                            <div class="row divmargin">
                                <div class="col-sm-12 text-center">

                                    <asp:Button ID="btnnewChuubunrui" runat="server" Width="140px" CssClass="btnnewChuubunrui JC10GrayButton mr-2" Text="✛ 中分類を追加"
                                        OnClick="btnnewChuubunrui_Click" />
                                </div>
                            </div>
                            <div id="Gvdiv" runat="server"  style=" overflow: auto; width: 450px; max-height:378px !important;height:auto!important;overflow-x:hidden !important;margin-bottom:20px !important;">
                                <asp:GridView ID="gvchuubunrilist" runat="server" DataKeyNames="cSYOUHIN_TYUUGRP" OnRowDataBound="OnRowDataBound" OnRowEditing="OnRowEditing"
                                    OnRowCommand="gvchuubunrilist_RowCommand" OnRowDeleting="OnRowDeleting"
                                    AutoGenerateColumns="false" GridLines="None" ShowHeader="False" CssClass="sonotapop">
                                    <RowStyle Height="35px" />
                                    <%----%>
                                    <Columns>
                                        <asp:BoundField DataField="cSYOUHIN_TYUUGRP" ItemStyle-CssClass="bfieldcss" />
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                 <div style="width:403px;">
                                            <asp:Panel ID="pnlChuBunrui" style="float:left;" runat="server">
                                            <asp:TextBox ID="txt_editChuu" CssClass="form-control TextboxStyle" runat="server" Text='<%# Bind("sSYOUHIN_TYUUGRP") %>' Width="240px" MaxLength="50"></asp:TextBox> 
                                                </asp:Panel>
                                            <asp:Panel ID="PopupMenu" CssClass="popupMenu JCEditedPopup_1" style="padding:0px !important;padding-left:7px !important;float:left;" runat="server">
                                                 <asp:Button ID="btnUpdate" runat="server" style="font-size:13px;" Text="更新" CssClass="JC10GrayButton" CommandName="Update" CommandArgument="<%# Container.DataItemIndex %>" OnClick="btnUpdate_Click" />
                                                 <asp:Button ID="btnEditCancel" runat="server" Text="キャンセル" CssClass="JC07HyojiItemSettingBtn" style="margin-left:3px;font-size:13px;" OnClick="btnEditCancel_Click"/>
                                            </asp:Panel>
                                                </div>
                                            </ItemTemplate>
                                            <ItemStyle Width="354px" />
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                  <asp:Panel ID="pnlChuubunruimei" runat="server" BorderColor="Black" style="width:457px !important;padding-right:50px;height:35px;padding-top:5px;">
                                               <div style="width:370px !important;overflow: hidden; display: -webkit-box; -webkit-line-clamp: 1; -webkit-box-orient: vertical; word-break: break-all;">
                                            <asp:LinkButton ID="lkbtnChuubunruiMei" runat="server"  style="font-size:13px;" Text='<%# Server.HtmlEncode((string)Eval("sSYOUHIN_TYUUGRP"))%>' CommandName="Select" CommandArgument="<%# Container.DataItemIndex %>"></asp:LinkButton>
                                                   </div>

                                            </asp:Panel>
                                            <asp:Panel ID="pnlEdited" runat="server" CssClass="PopupMenu JCEditedPopup" Style="display: none;    padding: 5px 0px 0px 0px;margin-left:-90px !important;">
                                                <asp:LinkButton ID="imgbtnCopy" runat="server" CssClass="btn-icons" CommandName="Edit" CommandArgument="<%# Container.DataItemIndex %>" >
                                                    <i class="bi bi-pencil-fill"></i>
                                                </asp:LinkButton>
                                                <asp:LinkButton ID="imgbtnDelete" runat="server" CssClass="btn-icons" CommandName="Delete" CommandArgument="<%# Container.DataItemIndex %>" OnClick="btnDelete_Click" >
                                                    <i class="bi bi-trash-fill"></i>
                                                </asp:LinkButton>
                                               
                                            </asp:Panel>
                                            <asp:HoverMenuExtender ID="hmeKyotenListEdit" runat="server" TargetControlID="pnlChuubunruimei" 
                                                PopupControlID="pnlEdited" PopupPosition="Right">
                                            </asp:HoverMenuExtender>
                                             <asp:Button ID="BT_DeleteOk" runat="server" Text="はい" class="BlueBackgroundButton DisplayNone" Width="100px" Height="36px" OnClick="BT_DeleteOk_Click" />
                                             <asp:Button ID="BT_DeleteCancel" runat="server" Text="キャンセル" class="JC09GrayButton DisplayNone" Width="100px" Height="36px" />


                                            </ItemTemplate>
                                            <ItemStyle Width="457px" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <%-- <RowStyle CssClass="smgridViewRow" />
                                <AlternatingRowStyle CssClass="smgridViewRow" />--%>
                                </asp:GridView>
                            </div>

                            <div id="newDiv" class="row " style="visibility: hidden;margin-left:40px!important" runat="server">
                                <%-- --%>
                                <table style="margin: 3px 0px 3px 0px;">
                                    <tr>
                                        <td style="width: 240px">
                                            <asp:TextBox ID="txt_newChuubunrui" runat="server" Text='' MaxLength="50" Width="236px" CssClass="form-control TextboxStyle"></asp:TextBox><%--OnTextChanged="txt_newChuubunrui_TextChanged"--%>

                                        </td>
                                        <td ><%--style="padding-left: 10px"--%>
                                            <asp:Button ID="btnnewChuubunruiSave" runat="server" Text="保存" CssClass="JC10GrayButton" OnClick="btnnewChuubunruiSave_Click" Style="margin-left:5px;margin-right:5px"/>
                                            <asp:Button ID="btnnewChuubunruiCancel" runat="server" Text="キャンセル" CssClass="JC07HyojiItemSettingBtn" OnClick="btnnewChuubunruiCancel_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </div>


                        <%--------------add by テテ 20211206 start------------%>
                        <div id="smFooter" style="display: block; width: 100%;">
                            <div style="display: flex; justify-content: center; align-items: center; background-color: #eee; width: 100%; padding: 20px 0">
                                <asp:Button ID="btnKyotenlistCancel" runat="server" Text="キャンセル" CssClass="JC10CancelBtn" Width="99px" OnClick="btnCancel_Click" />

                            </div>
                        </div>
                        <%--------------add by テテ 20211206 end------------%>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <asp:UpdatePanel ID="updDaibunruiPopup" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:Button ID="btnDaibunruiPopup" runat="server" Text="Button" CssClass="DisplayNone" />
                    <asp:ModalPopupExtender ID="mpeDaibunruiPopup" runat="server" TargetControlID="btnDaibunruiPopup"
                        PopupControlID="pnlDaibunruiPopupScroll" BackgroundCssClass="PopupModalBackground" BehaviorID="pnlDaibunruiPopupScroll">
                    </asp:ModalPopupExtender>
                    <asp:Panel ID="pnlDaibunruiPopupScroll" runat="server" Style="display: none; height: 100%; overflow: hidden;" CssClass="PopupScrollDiv aa" HorizontalAlign="Center">
                        <asp:Panel ID="pnlDaibunruiPopup" runat="server">
                            <iframe id="ifDaibunruiPopup" runat="server" scrolling="no" class="NyuryokuIframe daiPopupStyle" style="border-radius: 0px;"></iframe><%--class="NyuryokuIframe"--%>
                            <%--<asp:Button ID="btnClose" runat="server" Text="Button" Style="display: none" OnClick="btnClose_Click" />--%>
                            <asp:Button ID="btnDaiClose" runat="server" Text="Button" Style="display: none" OnClick="btnDaiClose_Click" />
                            <asp:Button ID="btnDaiSelect" runat="server" Text="Button" Style="display: none" OnClick="btnDaiSelect_Click" />
                            <asp:Button ID="btnChuuSelect" runat="server" Text="Button" Style="display: none" OnClick="btnChuuSelect_Click" />
                        </asp:Panel>
                    </asp:Panel>
                </ContentTemplate>
            </asp:UpdatePanel>
            <asp:Button ID="btnYes" runat="server" Text="はい" class="BlueBackgroundButton DisplayNone" OnClick="btnYes_Click" Width="100px" Height="36px" />
            <asp:Button ID="btnCancel" runat="server" Text="キャンセル" class="JC09GrayButton DisplayNone" OnClick="btnDialogCancel_Click" Width="100px" Height="36px" />
            <asp:HiddenField ID="hdnHome" runat="server" />
            <asp:HiddenField ID="hdnScrollPos" runat="server" />
        </form>
    </div>
    <script src="../Scripts/jquery-3.5.1.js"></script>
    <script type="text/javascript"> 
         function selectText(editindex) {
           $('[id*=gvchuubunrilist] tr').eq(editindex).find("[Id*='txt_editChuu']").select();
        }

        var xPos, yPos;
        var prm = Sys.WebForms.PageRequestManager.getInstance();
        prm.add_beginRequest(BeginRequestHandler);
        prm.add_endRequest(EndRequestHandler);
        function BeginRequestHandler(sender, args) {
            xPos = $get('Gvdiv').scrollLeft;
            yPos = $get('Gvdiv').scrollTop;
        }
        function EndRequestHandler(sender, args) {
            $get('Gvdiv').scrollLeft = xPos;
            $get('Gvdiv').scrollTop = yPos;
        }

    </script>
        
    <script src="../Scripts/jquery-3.3.1.js"></script>
    <script src="../Scripts/jquery-ui-1.12.1.min.js"></script>
    <script src="../Scripts/cloudflare-jquery-ui-i18n.min.js"></script>
</body>
</html>
