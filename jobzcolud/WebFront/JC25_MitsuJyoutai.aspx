<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="JC25_MitsuJyoutai.aspx.cs" Inherits="jobzcolud.WebFront.JC25_MitsuJyoutai" ValidateRequest="false"  %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="../Content/bootstrap.css" rel="stylesheet" />
    <link href="../Style/StyleJC.css" rel="stylesheet" />
    <asp:PlaceHolder runat="server">
        <%: Scripts.Render("~/bundles/modernizr") %>
        <%: Styles.Render("~/style/StyleBundle1") %>
        <%: Scripts.Render("~/scripts/ScriptBundle1") %>
    </asp:PlaceHolder>
    <webopt:BundleReference runat="server" Path="~/Content/bootstrap" />

    <script type="text/javascript">
        // master pageのPopupの大きさのため
        //['load', 'resize'].forEach(function (e) {
        //    window.addEventListener(e, function () {
        //        //var isMobile = navigator.userAgent.toLowerCase().match(/mobile/i);
        //        showPopup('pnlKyotenNewPopupScroll', 'pnlKyotenNewPopup', 'ifKyotenNewPopup');
        //    });
        //});

    </script>
    <script src="Scripts/jquery-3.3.1.js"></script>

</head>
<body class="fontcss" style="background-color: #11ffee00;">
    <div class="J06Div RadiusIframe pl-4 pr-4 ">
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
            <%-- <div style="width:410px;height:30vh;">--%>
            <div style="width: 100%; max-height:450px; margin-left: auto; margin-right: auto; padding: 0; left: 50%; top: 50%; position: absolute; transform: translate(-50%, -50%); background-color: #fff"> <%--height: 50vh;--%>
                <asp:UpdatePanel ID="updKyotenlist" runat="server" UpdateMode="Conditional" DefaultButton="btnHiddenSubmit">
                    <ContentTemplate>
                        <asp:Button ID="btnHiddenSubmit" runat="server" CssClass="DefaultBtn" />

                        <div class="row" style="display: flex; justify-content: center; align-items: center">
                            <asp:Label ID="lblHeader" runat="server" CssClass="shiaraititleStyle fw-bold txt-style" Text="見積状態" />
                            <asp:Button ID="btnHeaderCross" runat="server" Text="✕" CssClass="PopupCloseBtn mr-n3 JC08btnHeaderCross" OnClick="btnCancel_Click" />
                        </div>
                        <div class="titleLine"></div>
                      
                        <div>
                            <%--style="height:200px;overflow:auto;"--%>
                            <asp:GridView ID="gvJyotailist" runat="server" DataKeyNames="cJYOTAI" OnRowDataBound="OnRowDataBound" OnRowEditing="OnRowEditing"
                                OnRowCommand="gvJyotailist_RowCommand" OnRowDeleting="OnRowDeleting"
                                AutoGenerateColumns="false" GridLines="None" CssClass="sonotapop">
                                <Columns>
                                    <asp:BoundField DataField="cJYOTAI" ItemStyle-CssClass="bfieldcss" />
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtsJYOTAI" runat="server" Text='<%# Bind("sJYOTAI") %>' Width="285px"></asp:TextBox>
                                            <%--<asp:Panel ID="pnlUpdate" runat="server" CssClass="HoverPopup EditedPopup">
                                                <asp:LinkButton ID="lkbtnUpdate" runat="server" Text='更新' CommandName="Update" CommandArgument="<%# Container.DataItemIndex %>" Style="margin-right: 10px; color: black;"></asp:LinkButton>
                                                <asp:ImageButton ID="btnUpd_Delete" runat="server" CommandName="Delete" CommandArgument="<%# Container.DataItemIndex %>" ImageUrl="~/Images/trash.png" Height="14px" Width="14px" />
                                                <asp:ConfirmButtonExtender ID="cbtneUpdDelete" runat="server" ConfirmText="削除してもよろしいでしょうか？" TargetControlID="btnUpd_Delete" />
                                            </asp:Panel>--%>
                                           <%-- <asp:HoverMenuExtender ID="hmeKyotenListUpdate" runat="server" TargetControlID="txtsJYOTAI"
                                                PopupControlID="pnlUpdate" PopupPosition="Right">
                                            </asp:HoverMenuExtender>--%>
                                        </ItemTemplate>
                                        <ItemStyle Width="285px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lkbtnKyotenMei" runat="server" Text='<%# Server.HtmlEncode((string)Eval("sJYOTAI"))%>' CommandName="Select" CommandArgument="<%# Container.DataItemIndex %>"></asp:LinkButton>
                                            <%--<asp:Panel ID="pnlEdited" runat="server" CssClass="HoverPopup EditedPopup">
                                                
                                                <asp:ImageButton ID="imgbtnCopy" runat="server" CommandName="Edit" CommandArgument="<%# Container.DataItemIndex %>" ImageUrl="~/Images/draw.png" Height="14px" Width="14px" />
                                                <asp:ImageButton ID="imgbtnDelete" runat="server" CommandName="Delete" CommandArgument="<%# Container.DataItemIndex %>" ImageUrl="~/Images/trash.png" Height="14px" Width="14px" />
                                                <asp:ConfirmButtonExtender ID="cbtneDelete" runat="server" ConfirmText="削除してもよろしいでしょうか？" TargetControlID="imgbtnDelete" />
                                            </asp:Panel>--%>
                                           <%-- <asp:HoverMenuExtender ID="hmeKyotenListEdit" runat="server" TargetControlID="lkbtnKyotenMei"
                                                PopupControlID="pnlEdited" PopupPosition="Right">
                                            </asp:HoverMenuExtender>--%>
                                        </ItemTemplate>
                                        <ItemStyle Width="280px" />
                                    </asp:TemplateField>
                                </Columns>
                                <RowStyle CssClass="smgridViewRow" />
                                <AlternatingRowStyle CssClass="smgridViewRow" />
                            </asp:GridView>
                        </div>

                        <div class="row" style="display: none;" runat="server" id="newjyotai">
                            <table>
                                <tr>
                                    <td>
                                        <asp:TextBox ID="txt_newJoytai" runat="server" Text='' Width="200px" CssClass="form-control TextboxStyle"></asp:TextBox>

                                    </td>
                                    <td style="margin-left: 10px">
                                        <asp:Button ID="Button3" runat="server" Text="保存" CssClass="JCjoytaiGrayButton" OnClick="btnnewjoytaiSave_Click" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                        
                        <%--------------add by テテ 20211206 start------------%>
                        <div id="smFooter" style="display: block; max-width: 440px; background-color: white;">
                            <div style="display: flex; justify-content: center; align-items: center; background-color: #eee ; width: 440px; padding: 20px 0">
                                <asp:Button ID="btnKyotenlistCancel" runat="server" Text="キャンセル" CssClass="JC10CancelBtn" Width="99px" OnClick="btnCancel_Click" />

                            </div>
                            <%--------------add by テテ 20211206 end------------%>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <asp:HiddenField ID="hdnHome" runat="server" />
        </form>
    </div>

</body>

</html>
