<%@ Page Language="C#" MasterPageFile="~/WebFront/JC99NavBar.Master" AutoEventWireup="true" CodeBehind="JC26Setting.aspx.cs" Inherits="jobzcolud.WebFront.JC26Setting" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <!DOCTYPE html>

    <html>
    <head>
        <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
        <title></title>
        <link href="Content/bootstrap.css" rel="stylesheet" />
        <asp:PlaceHolder runat="server">
            <%: Scripts.Render("~/bundles/modernizr") %>
            <%: Styles.Render("~/style/StyleBundle1") %>
            <%: Scripts.Render("~/scripts/ScriptBundle1") %>
            <%: Styles.Render("~/style/UCStyleBundle") %>
        </asp:PlaceHolder>
        <webopt:BundleReference runat="server" Path="~/Content/bootstrap" />
        <style>
            .mycontent {
                overflow: auto;
                padding-top: 40px;
                padding-left: 0 !important;
                padding-right: 0 !important;
            }

            .fontcss {
                font-family: "Hiragino Sans", "Open Sans", "Meiryo", "Hiragino Kaku Gothic Pro", "メイリオ", "MS ゴシック", "sans-serif";
                font-size: 13px;
            }

            .JC26Btn {
                font-size: 13px;
                height: 35px;
                border-radius: 6px;
                margin-right: 13px;
                background-color: white;
                border: 1px solid gray;
            }

            .btnPadding {
                padding-left: 15px;
                padding-right: 15px;
                margin-right:3px;
            }
        </style>
    </head>
    <body>

        <div class="mycontent" style="padding-top: 15px;">
            <div style="padding: 15px 0px 0px 10px;">
                <div style="padding-top: 40px; padding-left: 40px; background-color: white; height: 85vh;">
                    <asp:Button ID="btnJishaInfoSetting" runat="server" CssClass="JC07HyojiItemSettingBtn btnPadding fontcss" Text="自社情報設定" OnClick="btnJishaInfoSetting_Click" />
                    <asp:Button ID="btnUserSetting" runat="server" CssClass="JC07HyojiItemSettingBtn btnPadding fontcss" Text="ユーザー設定" OnClick="btnUserSetting_Click" />
                      <asp:Button ID="btnSyouhinSetting" runat="server" CssClass="JC07HyojiItemSettingBtn btnPadding fontcss" Text="商品新規作成" OnClick="btnsyouhinSetting_Click" />  
                    <asp:UpdatePanel ID="updhyojipopup" runat="server" AutoPostBack="true" UpdateMode="Conditional">
                        <ContentTemplate>
                           <%-- <asp:Button ID="btnSupplierSearch" runat="server" CssClass="JC26Btn btnPadding fontcss" Text="仕入先検索"
                                OnClick="btnSupplierSearch_Click" AutoPostBack="true" />--%>
                        </ContentTemplate>

                    </asp:UpdatePanel>
                </div>
            </div>
            <div>
                <!--ポップアップ画面-->
               <asp:UpdatePanel ID="updShinkiPopup" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:Button ID="btnShinkiPopup" runat="server" Text="Button" Style="display: none" />
                    <asp:ModalPopupExtender ID="mpeShinkiPopup" runat="server" TargetControlID="btnShinkiPopup"
                        PopupControlID="pnlShinkiPopupScroll" BackgroundCssClass="PopupModalBackground" BehaviorID="pnlShinkiPopupScroll"
                        RepositionMode="RepositionOnWindowResize">
                    </asp:ModalPopupExtender>
                    <asp:Panel ID="pnlShinkiPopupScroll" runat="server" Style="display: none;height:100%;overflow:hidden;" CssClass="PopupScrollDiv">
                        <asp:Panel ID="pnlShinkiPopup" runat="server">
                            <iframe id="ifShinkiPopup" runat="server" scrolling="yes"  style="height:100vh;width:100vw;"></iframe>
                        <asp:Button ID="btn_CloseSearch" runat="server" Text="Button" CssClass="DisplayNone" OnClick="btn_CloseSearch_Click"/>
                        <%--<asp:Button ID="btn_getSyosai" runat="server" Text="Button" CssClass="DisplayNone" OnClick="btn_getSyosai_Click"/>--%>
                            <%--<asp:Button ID="btn_CloseMitumoriSearch" runat="server" Text="Button" CssClass="DisplayNone" OnClick="btn_CloseMitumoriSearch_Click" />
                           --%>
                             <asp:Button ID="btn_CloseShinkiSentaku" runat="server" Text="Button" CssClass="DisplayNone" OnClick="btn_CloseShinkiSentaku_Click" />
                            
                            </asp:Panel>
                    </asp:Panel>
                </ContentTemplate>
            </asp:UpdatePanel>
                <!--ポップアップ画面-->
                <asp:UpdatePanel ID="updShouhinPopup" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:Button ID="btnShouhinPopup" runat="server" Text="Button" Style="display: none" />
                <asp:ModalPopupExtender ID="mpeShouhinPopup" runat="server" TargetControlID="btnShouhinPopup"
                    PopupControlID="pnlShouhinPopupScroll" BackgroundCssClass="PopupModalBackground" BehaviorID="pnlShouhinPopupScroll"
                    RepositionMode="RepositionOnWindowResize">
                </asp:ModalPopupExtender>
                <asp:Panel ID="pnlShouhinPopupScroll" runat="server" Style="display: none; height: 100%; overflow: hidden;" CssClass="PopupScrollDiv">
                    <asp:Panel ID="pnlShouhinPopup" runat="server">
                        <iframe id="ifShouhinPopup" runat="server" scrolling="yes" style="height: 100vh; width: 100vw;"></iframe>
                     <asp:Button ID="btn_Close" runat="server" Text="Button" CssClass="DisplayNone" OnClick="btn_Close_Click" />
                       
                    </asp:Panel>
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>
            </div>

        </div>

    </body>

    </html>
</asp:Content>
