<%@ Page Language="C#" MasterPageFile="~/WebFront/JC99NavBar.Master" AutoEventWireup="true" CodeBehind="JC16KoujinJougouSetting.aspx.cs" Inherits="JobzCloud.WebFront.JC16KoujinJougouSetting" EnableEventValidation = "false" ValidateRequest ="False"%>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server" >
<!DOCTYPE html>

<html>
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
     <asp:PlaceHolder runat="server">
        <%: Scripts.Render("~/bundles/modernizr") %>
        <%: Styles.Render("~/style/StyleBundle1") %>
        <%: Scripts.Render("~/scripts/ScriptBundle1") %>
        <%: Styles.Render("~/style/UCStyleBundle") %>

    </asp:PlaceHolder>
    <webopt:BundleReference runat="server" Path="~/Content1/bootstrap" />
    <webopt:BundleReference runat="server" Path="~/Content1/css" />
<meta name="google" content="notranslate"/>
<meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=0, shrink-to-fit=no" />

     <script src="../resources/js/jquery.min.js"></script>
    <style type="text/css">
        .font{ /*20220309 Update エインドリ－*/
            /*font-family:'Bookman Old Style';
            font-size:15px;*/
             font-family: "Hiragino Sans", "Open Sans", "Meiryo", "Hiragino Kaku Gothic Pro", "メイリオ", "MS ゴシック", "sans-serif";
            font-size: 13px; 
        }
        .error{
            font-family:"Hiragino Sans", "Open Sans", "Meiryo", "Hiragino Kaku Gothic Pro", "メイリオ", "MS ゴシック", "sans-serif";
            font-size:11px;
            color:red;
        }
         #txtemailAdd{
             caret-color:auto;
         }
         .JC16ButtonDisable{
             background: rgb(242, 242, 242);
    font-size: 13px;
    color:darkgray; /*20220315 Added　エインドリ－*/
    border-radius: 3px;
    margin-right: 13px;
    margin-top: 0px;
    border: none;
    padding: 6px 12px 6px 12px;
    letter-spacing: 1px;
   cursor:default;
   pointer-events:none;
  
         }

         #btnhozon:hover{
             cursor:default;
         }
      
     
        </style>
     <script type="text/javascript">
        function errorLabelClear()
        {
            document.getElementById("<%=LB_Email_Error.ClientID %>").innerText = "";
            document.getElementById("<%=LB_User_Error.ClientID%>").innerText = "";
        }
        function RestrictEnter(event) {
            if (event.keyCode === 13) {
                event.preventDefault();
            }
         }
         //20220314 Added エインドリ－
         Sys.Application.add_load(function () {
             $("#<%= btnhozon.ClientID %>").click(function () {
                 document.getElementById("<%=btnhozon.ClientID %>").className = "JC16ButtonDisable";
                 document.getElementById("<%=btnChangePwd.ClientID %>").className = "JC16ButtonDisable"; /*20220317 Added エインドリ－*/
                 setTimeout(30 * 6000);
                 $('.btnhozon').prop('disabled', true);
                 $('.btnChangePwd').prop('disabled', true); /*20220317 Added エインドリ－*/
             });
             
    });
        </script>
</head>
<body style="background-color:#EBF5FB;"> <%--20220309 Update Form Design エインドリ－--%>
       <%--<asp:ScriptManager ID="ScriptManager2" EnablePageMethods="True" runat="server">--%>
            <Scripts>
                <asp:ScriptReference Name="jquery" />
                <asp:ScriptReference Name="bootstrap" />
                <asp:ScriptReference Path="../Scripts/Common/FixFocus.js" />
            </Scripts>
  <%--</asp:ScriptManager>--%>
            <asp:UpdatePanel ID="updHeader" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
           
            <asp:Panel runat="server" Style="margin-top:100px; padding-bottom:40px; width:1000px;background-color:white;margin-right:auto;margin-left:auto;">
                <div style="background-color: #f2f2f2;width:auto;height:45px;padding:12px 0px 5px 0px;padding-left:15px;">
                    <asp:Label runat="server" CssClass="col-form-label" style="font-weight:bold; ">個人情報設定</asp:Label>
                </div>
                <div>
                    <table runat="server" style="margin-top:50px;margin-left:162px;">
                        <tr style="padding:30px;">
                            <td>
                                <div style="padding-bottom:25px;">
                                    <asp:Label runat="server" CssClass="font">ログインID<span style="color:red; cursor:text;padding-bottom:4px;font-weight:bold;padding-left:5px;">*</span></asp:Label>
                                </div>
                            </td>
                            <td>
                                <div style="padding-left:25px;">
                                    <asp:TextBox ID="txtemailAdd" runat="server"  CssClass="form-control font input TextboxStyle" placeholder="メールアドレス" onkeyup="errorLabelClear();" onkeypress="RestrictEnter(event);" Width="500px"></asp:TextBox> <%--20211119 Updated Eaindray+Phoo--%>
                                    
                                </div>
                                <div style="padding-left:25px;height:25px;">
                                    <asp:Label ID="LB_Email_Error" runat="server" Text=" " ClientID="LB_Email_Error" CssClass="error"></asp:Label>
                                </div>
                            </td>
                            <td>
                                <div style="padding-left:40px;padding-bottom:25px;"> <%--20220315 Updated　エインドリ－--%>
                                    <asp:Button ID="btnChangePwd" runat="server" Text="パスワードを変更する" type="button " CssClass="BlueBackgroundButton" Width="180px" OnClientClick="PopupOpenButton()" OnClick="btnChangePwd_Click"/>
                                </div>
                            </td>
                        </tr>
                        <tr style="padding:30px;" >
                            <td style="padding-bottom:30px;">
                               <div>
                                   <asp:Label runat="server" CssClass="font">ユーザー名<span style="color:red; cursor:text;padding-top:4px;font-weight:bold;padding-left:5px;">*</span></asp:Label>
                               </div>
                            </td>
                            <td style="padding-bottom:10px;">
                                <div style="padding-left:25px;">
                                    <asp:TextBox ID="txtPCharge" runat="server" CssClass="form-control font input TextboxStyle" onkeyup="errorLabelClear();" onkeypress="RestrictEnter(event);"></asp:TextBox><%--20211119 Updated Eaindray+Phoo--%>
                                </div>
                                <div style="padding-left:25px;height:15px;">
                                    <asp:Label ID="LB_User_Error" runat="server" Text=" " ClientID="LB_User_Error" CssClass="error"></asp:Label>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td style="padding-top:2px;">
                                <div style="margin-left:115px;" runat="server">
                                      <asp:Button ID="btnhozon" runat="server"  Text="保存" CssClass="BlueBackgroundButton JC10SaveBtn" OnClientClick="javascript:disabledTextChange(this);"
                                            OnClick="btnhozon_Click" style ="margin-left: 23px;"/> <%--20220315 Updated エインドリ－--%>
                                </div>
                            </td>
                        </tr>
                    </table>
                </div>
                 <asp:Label ID="lblLoginUserCode" runat="server" Text="" Visible="false"/>
            </asp:Panel>
                 <asp:HiddenField ID="HF_Url" runat="server" />
                 <asp:Button ID="btn_cancel" CssClass="DisplayNone" data-bs-dismiss="modal" runat="server" Text="キャンセル" OnClick="btn_cancel_Click" />
                 </ContentTemplate>
    </asp:UpdatePanel>
            <!--ポップアップ画面-->
            <asp:UpdatePanel ID="updSentakuPopup" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:Button ID="btnSentakuPopup" runat="server" Text="Button" CssClass="DisplayNone" />
                    <asp:ModalPopupExtender ID="mpeSentakuPopup" runat="server" TargetControlID="btnSentakuPopup"
                        PopupControlID="pnlSentakuPopupScroll" BackgroundCssClass="PopupModalBackground" BehaviorID="pnlSentakuPopupScroll">
                    </asp:ModalPopupExtender>
                    <asp:Panel ID="pnlSentakuPopupScroll" runat="server" Style="display: none;" CssClass="PopupScrollDiv">
                        <asp:Panel ID="pnlSentakuPopup" runat="server">
                            <iframe id="ifSentakuPopup" runat="server" scrolling="no" class="NyuryokuIframe" style="border-radius:0px;" ></iframe>
                            <asp:Button ID="btnClose" runat="server" Text="Button" Style="display: none" OnClick="btnClose_Click" onClientClick="MasterOverflow()"/>
                            <asp:Button ID="btnSavePwd" runat="server" Text="Button" Style="display: none" OnClick="btnSavePwd_Click" onClientClick="MasterOverflow()"/>
                        </asp:Panel>
                    </asp:Panel>
                </ContentTemplate>
            </asp:UpdatePanel>

                 <asp:UpdatePanel ID="updShinkiPopup" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:Button ID="btnShinkiPopup" runat="server" Text="Button" Style="display: none" />
                    <asp:ModalPopupExtender ID="mpeShinkiPopup" runat="server" TargetControlID="btnShinkiPopup"
                        PopupControlID="pnlShinkiPopupScroll" BackgroundCssClass="PopupModalBackground" BehaviorID="pnlShinkiPopupScroll"
                        RepositionMode="RepositionOnWindowResize">
                    </asp:ModalPopupExtender>
                    <asp:Panel ID="pnlShinkiPopupScroll" runat="server" Style="display: none;height:100%;position:relative;margin-left:auto;margin-right:auto" HorizontalAlign="Center">
                            <iframe id="ifShinkiPopup" runat="server" scrolling="yes"  style="height:100%;position:absolute;width:1350px;"></iframe>
                        <asp:Button ID="btn_CloseMitumoriSearch" runat="server" Text="Button" CssClass="DisplayNone" OnClick="btn_CloseMitumoriSearch_Click" onClientClick="MasterOverflow()"/>
                    </asp:Panel>
                </ContentTemplate>
            </asp:UpdatePanel>
</body>
</html>

</asp:Content>
