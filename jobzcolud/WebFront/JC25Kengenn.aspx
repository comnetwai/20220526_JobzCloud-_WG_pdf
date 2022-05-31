<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="JC25Kengenn.aspx.cs" Inherits="jobzcolud.WebFront.JC25Kengenn" %>

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

    <script src="Scripts/jquery-3.3.1.js"></script>
    <script type="text/javascript">
        function HideIframe() {
            $('[id*=mpeKengennPopUp]', window.parent.document).hide();
            $('[id*=pnlKengennSetPopUpScroll]', window.parent.document).hide();
        }
    </script>
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
            <div style="width: 100%;margin-left: auto; margin-right: auto; padding: 0; left: 50%; top: 50%; position: absolute; transform: translate(-50%, -50%); background-color: #fff"> <%--height: 50vh;--%>
                <asp:UpdatePanel ID="updKengenlist" runat="server" UpdateMode="Conditional" DefaultButton="btnHiddenSubmit">
                    <ContentTemplate>
                        <asp:Button ID="btnHiddenSubmit" runat="server" CssClass="DefaultBtn" />
                        <div class="row" style="display: flex; justify-content: center; align-items: center">
                            <asp:Label ID="lblHeader" runat="server" CssClass="shiaraititleStyle fw-bold txt-style" Text="権限" />
                            <asp:Button ID="btnHeaderCross" runat="server" Text="✕" CssClass="PopupCloseBtn mr-n3 JC08btnHeaderCross" OnClick="btnCancel_Click" />
                        </div>
                        <div class="titleLine"></div>
                        
                        <div style=" overflow: auto; width: 410px;"><%-- height: 280px;--%>
                            <asp:GridView ID="gvKengennlist" runat="server" DataKeyNames="cKENGENN" OnRowCommand="gvKengennlist_RowCommand"
                                AutoGenerateColumns="false" GridLines="None" CssClass="sonotapop">
                                <Columns>
                                    <asp:BoundField DataField="cKENGENN" ItemStyle-CssClass="bfieldcss" />

                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lkbtnKengennMei" runat="server" Text='<%# Bind("sKENGENN") %>' CommandName="Select" CommandArgument="<%# Container.DataItemIndex %>"></asp:LinkButton>
                                            <asp:TextBox ID="txtsKENGENN" runat="server" Text='<%# Bind("sKENGENN") %>' Width="285px" CssClass="DisplayNone"></asp:TextBox>

                                        </ItemTemplate>
                                        <ItemStyle Width="280px" />
                                    </asp:TemplateField>
                                </Columns>
                                <RowStyle CssClass="smgridViewRow" />
                                <AlternatingRowStyle CssClass="smgridViewRow" />
                            </asp:GridView>
                        </div>

                        <%--------------add by テテ 20211206 start------------%>
                        <div id="smFooter" style="display: block; max-width: 440px; background-color: white;">
                            <div style="display: flex; justify-content: center; align-items: center; background-color: #eee; width: 440px; padding: 20px 0">
                                <asp:Button ID="btnKyotenlistCancel" runat="server" Text="キャンセル" CssClass="JC10CancelBtn" Width="99px" OnClick="btnCancel_Click" />

                            </div>
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
