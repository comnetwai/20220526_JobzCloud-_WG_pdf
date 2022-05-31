<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="JC38Daibunrui.aspx.cs" Inherits="jobzcolud.WebFront.JC38Daibunrui" %>

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
</head>
<body class="fontcss" style="background-color: #11ffee00;">
    <div class="J06Div RadiusIframe pl-4 pr-4 pt-5 ">
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

        <div style="width: 470px;max-height:620px;margin-left: auto; margin-right: auto;  padding: 0; left: 50%; top: 50%; position: absolute; transform: translate(-50%, -50%);background-color: #fff;">
            <asp:UpdatePanel ID="updDaibunList" runat="server" UpdateMode="Conditional" DefaultButton="btnHiddenSubmit">
                <ContentTemplate>
                    <asp:Button ID="btnHiddenSubmit" runat="server" CssClass="DefaultBtn" />
                    <div class="row" style="display: flex; justify-content: center; align-items: center">
                        <asp:Label ID="lblHeader" runat="server" CssClass="shiaraititleStyle fw-bold txt-style" Text="大分類" />
                        <%--<asp:Button ID="btnHeaderCross" runat="server" Text="✕" CssClass="PopupCloseBtn mr-n3 JC08btnHeaderCross" OnClick="btnCancel_Click" />--%>
                        <asp:Button ID="btnHeaderCross" runat="server" Text="✕" CssClass="PopupCloseBtn mr-n3 JC08btnHeaderCross" OnClick="btnDaiCross_Click" /><%--テテ changed OnClick name 20220329--%>
                    </div>
                    <div class="titleLine"></div>

                    <div class="row divmargin" style=""">
                        <div class="col-sm-12 text-center">
                            <asp:Button ID="btnnewDaibun" runat="server" Width="140px" CssClass="JC10GrayButton mr-2" Text="✛ 大分類を追加"
                                    OnClick="btnnewDaibun_Click" />
                        </div>
                    </div>
                    <div id="Gvdiv" runat="server" style=" overflow: auto; width: 450px; max-height:357px !important;height:auto!important;overflow-x:hidden !important;"> <%--height: 200px;--%>
                        <asp:GridView ID="gvDaibunList" runat="server" DataKeyNames="cSYOUHIN_DAIGRP" ShowHeader="False" GridLines="None" OnRowDataBound="OnRowDataBound" OnRowEditing="OnRowEditing"
                                OnRowCommand="gvDaibunList_RowCommand" OnRowDeleting="OnRowDeleting"
                                AutoGenerateColumns="false"  CssClass="sonotapop">
                            <RowStyle Height="35px"  />
                            <Columns>
                                <asp:BoundField DataField="cSYOUHIN_DAIGRP" ItemStyle-CssClass="bfieldcss" />
                                <asp:TemplateField>
                                    <ItemTemplate>
                                          <div style="width:403px;">
                                            <asp:Panel ID="pnlDaibunrui" style="float:left;" runat="server">
                                            <asp:TextBox ID="txtsSYOUHIN" CssClass="form-control TextboxStyle" runat="server" Text='<%# Bind("sSYOUHIN_DAIGRP") %>' Width="240px" MaxLength="50"></asp:TextBox> 
                                                </asp:Panel>
                                            <asp:Panel ID="PopupMenu" CssClass="popupMenu JCEditedPopup_1" style="padding:0px !important;padding-left:7px !important;float:left;" runat="server">
                                                 <asp:Button ID="btnUpdate" runat="server" style="font-size:13px;" Text="更新" CssClass="JC10GrayButton" CommandName="Update" CommandArgument="<%# Container.DataItemIndex %>" OnClick="btnUpdate_Click" />
                                                 <asp:Button ID="btnSaveCancel" runat="server" Text="キャンセル" CssClass="JC07HyojiItemSettingBtn" style="margin-left:3px;font-size:13px;" OnClick="btnSaveCancel_Click"/>
                                            </asp:Panel>
                                                </div>
                                    </ItemTemplate>

                                    <ItemStyle Width="385px" />
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                       
                                           <asp:Panel ID="pnlDaibunmei" runat="server" BorderColor="Black" style="width:457px !important;padding-right:50px;height:35px;padding-top:5px;">
                                               <div style="width:370px !important;overflow: hidden; display: -webkit-box; -webkit-line-clamp: 1; -webkit-box-orient: vertical; word-break: break-all;">
                                            <asp:LinkButton ID="lkbtnDaibunmei" runat="server"  style="font-size:13px;" Text='<%# Server.HtmlEncode((string)Eval("sSYOUHIN_DAIGRP"))%>' CommandName="Select" CommandArgument="<%# Container.DataItemIndex %>"></asp:LinkButton>
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
                                            <asp:HoverMenuExtender ID="hmeBumonListEdit" runat="server" TargetControlID="pnlDaibunmei" 
                                                PopupControlID="pnlEdited" PopupPosition="Right">
                                            </asp:HoverMenuExtender>
                                             <asp:Button ID="BT_DeleteOk" runat="server" Text="はい" class="BlueBackgroundButton DisplayNone" Width="100px" Height="36px" OnClick="BT_DeleteOk_Click" />
                                             <asp:Button ID="BT_DeleteCancel" runat="server" Text="キャンセル" class="JC09GrayButton DisplayNone" Width="100px" Height="36px" />


                                    </ItemTemplate>
                                    <ItemStyle Width="457px" />
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>
                    <div class="row " style="visibility:hidden;" runat="server" id="newDaibun"> <%--display: none;--%>
                            <table style="margin: 3px 0px 3px 0px;">
                                <tr>
                                    <td style="width:240px;">
                                        <asp:TextBox ID="txt_newDaibun" runat="server" Text='' Width="240px" CssClass="form-control TextboxStyle" MaxLength="50" OnTextChanged="txt_newDaibun_TextChanged" ></asp:TextBox>

                                    </td>
                                    <td style="padding-left: 8px">
                                        <asp:Button ID="btnsavedaibun" runat="server" Text="保存" CssClass="JC10GrayButton" OnClick="btnsavedaibun_Click" />
                                         <asp:Button ID="btncanceldaibun" runat="server" Text="キャンセル" CssClass="JC07HyojiItemSettingBtn" style="margin-left:3px;" OnClick="btncanceldaibun_Click" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                    <div  style="display: block; width:100%; background-color: white;"> <%--id="smFooter"--%>
                            <div style="display: flex; justify-content: center; align-items: center; background-color:#eee; width: 100%; padding: 20px 0">
                                <asp:Button ID="btnYukoKigenCancel" runat="server" Text="キャンセル" CssClass="JC10CancelBtn" Width="99px" OnClick="btnCancel_Click" />

                            </div>
                           </div>
                </ContentTemplate>
            </asp:UpdatePanel>

            

        </div>
         <asp:UpdatePanel ID="updShinkiPopup" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:Button ID="btnShinkiPopup" runat="server" Text="Button" Style="display: none" />
                <asp:ModalPopupExtender ID="mpeShinkiPopup" runat="server" TargetControlID="btnShinkiPopup"
                    PopupControlID="pnlShinkiPopupScroll" BackgroundCssClass="PopupModalBackground" BehaviorID="pnlShinkiPopupScroll"
                    RepositionMode="RepositionOnWindowResize">
                </asp:ModalPopupExtender>
                <asp:Panel ID="pnlShinkiPopupScroll" runat="server" Style="display: none; height: 100%; overflow: hidden;" CssClass="PopupScrollDiv" >
                    <asp:Panel ID="pnlShinkiPopup" runat="server">
                        <iframe id="ifShinkiPopup" runat="server" scrolling="no" frameborder="0" style="height: 100vh; width: 100vw;  overflow:hidden"></iframe>
                        
                        <asp:Button ID="btnClose" runat="server" Text="Button" CssClass="DisplayNone" OnClick="btnClose_Click" />
                    
                        <asp:Button ID="btnChuuSelect" runat="server" Text="Button" Style="display: none" OnClick="btnChuuSelect_Click" />

                         
                    </asp:Panel>
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>
        <asp:HiddenField ID="hdnHome" runat="server" />
             <input type="hidden" id="div_position" name="div_position" />
    </form>
        </div>
    <script src="../Scripts/jquery-3.5.1.js"></script>                
    <script type="text/javascript"> 
        function selectText(editindex) {
           $('[id*=gvDaibunList] tr').eq(editindex).find("[Id*='txtsSYOUHIN']").select();
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
<script src="https://code.jquery.com/jquery-3.2.1.min.js"></script>
<script src="https://code.jquery.com/ui/1.12.1/jquery-ui.min.js"></script>
 <script src="../Scripts/cloudflare-jquery-ui-i18n.min.js"></script>
</body>
</html>
