<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="JC25KyotenList.aspx.cs" Inherits="jobzcolud.WebFront.JC25KyotenList"  ValidateRequest="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
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

    <script type="text/javascript">
        // master pageのPopupの大きさのため
        //['load', 'resize'].forEach(function (e) {
        //    window.addEventListener(e, function () {
        //        //var isMobile = navigator.userAgent.toLowerCase().match(/mobile/i);
        //        showPopup('pnlKyotenNewPopupScroll', 'pnlKyotenNewPopup', 'ifKyotenNewPopup');
        //    });
        //});

    </script>

</head>
<body class="fontcss bg-transparent">
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
            <div style="width: 440px !important;max-height:620px !important;height:auto !important; background-color: white; margin-left: auto; margin-right: auto; padding: 0; left: 50%; top: 50%; position: absolute; transform: translate(-50%, -50%);">
                <asp:HiddenField ID="hdnHome" runat="server" />
                <asp:UpdatePanel ID="updKyotenlist" runat="server" UpdateMode="Conditional" DefaultButton="btnHiddenSubmit">
                    <ContentTemplate>
                        <asp:Button ID="btnHiddenSubmit" runat="server" CssClass="DefaultBtn" OnClick="btnHiddenSubmit_Click" />
                        <div class="row" style="display: flex; justify-content: center; align-items: center">
                            <asp:Label ID="lblHeader" runat="server" Text="拠点" CssClass="kyotentitleStyle fw-bold txt-style"></asp:Label>
                            <asp:Button ID="btnHeaderCross" runat="server" Text="✕" CssClass="PopupCloseBtn mr-n3 JC08btnHeaderCross" OnClick="btnCancel_Click" />
                        </div>
                        <div class="Borderline" style="margin: 0;"></div>
                        <div class="row divmargin">
                            <div class="col-sm-12 text-center">

                                <asp:Button ID="btnKyotenlistNewPopup" runat="server" Width="110px" CssClass="JC10GrayButton mr-2" Text="✛ 拠点を追加"
                                    OnClick="btnKyotenlistNewPopup_Click" Height="30px" />
                            </div>
                        </div>
                        <div id="Gvdiv" runat="server" style="overflow: auto; width: 419px;max-width:419px; margin-bottom: 20px;margin-top: 5px; max-height:377px !important; background-color: white;overflow-x:hidden !important;"> <%--height: 250px; --%>
                            <asp:GridView ID="gvKyotenlist" runat="server" DataKeyNames="cCo" OnRowDataBound="OnRowDataBound" OnRowEditing="OnRowEditing"
                                OnRowCommand="gvKyotenlist_RowCommand" OnRowDeleting="OnRowDeleting"
                                AutoGenerateColumns="false" GridLines="None"  ShowHeader="False" CssClass="sonotapop">
                                 <RowStyle Height="35px"  /><%----%>
                                <Columns>
                                    <asp:BoundField DataField="cCo" ItemStyle-CssClass="bfieldcss"/>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtsKYOTEN" runat="server" Text='<%# Bind("sKYOTEN") %>' Width="316px" CssClass="form-control TextboxStyle"></asp:TextBox>
                                            <asp:Panel ID="pnlUpdate" runat="server" CssClass="HoverPopup JCKyotenListEditedPopup">
                                                <asp:LinkButton ID="lkbtnUpdate" runat="server" Text='更新' CommandName="Update" CommandArgument="<%# Container.DataItemIndex %>" Style="margin-right: 10px;color:black" OnClick="OnUpdate"></asp:LinkButton>
                                                <asp:ImageButton ID="btnUpd_Delete" runat="server" CommandName="Delete" CommandArgument="<%# Container.DataItemIndex %>" ImageUrl="~/Images/trash.png" Height="14px" Width="14px" />
                                                <asp:ConfirmButtonExtender ID="cbtneUpdDelete" runat="server" ConfirmText="削除してもよろしいでしょうか？" TargetControlID="btnUpd_Delete" />
                                            </asp:Panel>
                                            <asp:HoverMenuExtender ID="hmeKyotenListUpdate" runat="server" TargetControlID="txtsKYOTEN"
                                                PopupControlID="pnlUpdate" PopupPosition="Right">
                                            </asp:HoverMenuExtender>
                                        </ItemTemplate>
                                        <ItemStyle Width="316px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:Panel ID="pnlKyotenmei" runat="server" BorderColor="Black" style="width:419px !important;padding-right:50px;height:35px;padding-top:5px;">
                                                <div style="width:3332px !important;overflow: hidden; display: -webkit-box; -webkit-line-clamp: 1; -webkit-box-orient: vertical; word-break: break-all;">
                                                <asp:LinkButton ID="lkbtnKyotenMei" runat="server" Text='<%# Server.HtmlEncode((string)Eval("sKYOTEN"))%>' CommandName="Select" CommandArgument="<%# Container.DataItemIndex %>"></asp:LinkButton>
                                                    </div>
                                            </asp:Panel>

                                            <asp:Panel ID="pnlEdited" runat="server" CssClass="HoverPopup JCKyotenListEditedPopup" Style="display:none;    padding: 4px 0px 0px 0px;margin-left:-60px !important;">
<%--                                                <asp:ImageButton ID="imgbtnCopy" runat="server" CommandName="Edit" CommandArgument="<%# Container.DataItemIndex %>" ImageUrl="~/Images/draw.png" Height="14px" Width="14px" />--%>
                                                <asp:LinkButton ID="imgbtnCopy" runat="server" CssClass="btn-icons" CommandName="Edit" CommandArgument="<%# Container.DataItemIndex %>">
                                                    <i class="bi bi-pencil-fill"></i>
                                                </asp:LinkButton>
                                                
<%--                                                <asp:ImageButton ID="imgbtnDelete" runat="server" CommandName="Delete" CommandArgument="<%# Container.DataItemIndex %>" ImageUrl="~/Images/trash.png" Height="14px" Width="14px" />--%>
<%--                                                <asp:ConfirmButtonExtender ID="cbtneDelete" runat="server" ConfirmText="削除してもよろしいでしょうか？" TargetControlID="imgbtnDelete" />--%>
                                            </asp:Panel>
                                            <asp:HoverMenuExtender ID="hmeKyotenListEdit" runat="server" TargetControlID="pnlKyotenmei"
                                                PopupControlID="pnlEdited" PopupPosition="Right">
                                            </asp:HoverMenuExtender>
                                        </ItemTemplate>
                                        <ItemStyle Width="419px" />
                                    </asp:TemplateField>
                                </Columns>
                              <%--  <RowStyle CssClass="kyotengridViewRow" />
                                <AlternatingRowStyle CssClass="kyotengridViewRow" />--%>
                            </asp:GridView>

                        </div>

                        
                        <div id="Footer" style="display: block; max-width: 440px; background-color: white;">
                            <div style="display: flex; justify-content: center; align-items: center; background-color: #eee; width: 440px; padding: 20px 0">
                                <asp:Button ID="btnKyotenlistCancel" runat="server" Text="キャンセル" CssClass="JC10CancelBtn" Width="99px" OnClick="btnCancel_Click" Height="35px" />

                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <asp:UpdatePanel ID="updShinkiPopup" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:Button ID="btnShinkiPopup" runat="server" Text="Button" Style="display: none" />
                    <asp:ModalPopupExtender ID="mpeShinkiPopup" runat="server" TargetControlID="btnShinkiPopup"
                        PopupControlID="pnlShinkiPopupScroll" BehaviorID="pnlShinkiPopupScroll"
                        RepositionMode="RepositionOnWindowResize">
                    </asp:ModalPopupExtender>
                    <asp:Panel ID="pnlShinkiPopupScroll" runat="server" Style="display: none; position: relative; margin-left: auto; margin-right: auto" HorizontalAlign="Center">
                        <iframe id="ifShinkiPopup" runat="server" scrolling="yes" class="KyotenPopupStyle"></iframe>
                        <asp:Button ID="btnKyotenNewClose" runat="server" Text="Button" CssClass="DisplayNone" OnClick="btnKyotenNewClose_Click" />
                    </asp:Panel>
                </ContentTemplate>
            </asp:UpdatePanel>
        </form>
    </div>

</body>
     <script src="../Scripts/jquery-3.5.1.js"></script>                
    <script type="text/javascript"> 
       
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
</html>
