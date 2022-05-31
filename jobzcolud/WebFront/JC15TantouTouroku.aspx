<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="JC15TantouTouroku.aspx.cs" Inherits="JobzCloud.WebFront.JC15TantouTouroku" ValidateRequest ="False" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <asp:PlaceHolder runat="server">
        <%: Scripts.Render("~/bundles/modernizr") %>
        <%: Styles.Render("~/style/StyleBundle1") %>
        <%: Scripts.Render("~/scripts/ScriptBundle1") %>
        <%: Styles.Render("~/style/UCStyleBundle") %>

    </asp:PlaceHolder>
    <webopt:BundleReference runat="server" Path="~/Content1/bootstrap" />
    <%--<webopt:BundleReference runat="server" Path="~/Content1/css" />--%>
    <%--<link href="../resources/css/bootstrap.min.css" rel="stylesheet" />--%>
    <%--<script src="../resources/js/jquery.min.js"></script>
    <script src="../resources/js/bootstrap.min.js"></script>
    <script src="../resources/js/popper.min.js"></script>
    <script src="../Scripts/bootstrap-datepicker.min.js"></script>
    <script src="../Scripts/jquery-ui-1.12.1.min.js"></script>--%>
    <style>
        .modal-header {
            padding-bottom:8px;
            border-bottom: 8px solid #e6e6e6;
        }

        .modal-title {
            margin-left: 20px;
        }

        .auto-style1 {
            width: 900px;
            height: 117px;
            margin-top: 0px;
            margin-left: 0px;
        }

        .modal-content {
            width: 1000px;
        }

        .tableStyle {
            width: 116px;
            text-align: left;
            height: 52.5px;
            padding-top: 1.00em; /*// 20211209 Added エインドリ－・プ－プゥ*/
        }

        .columnStyle {
            padding-left: 2em;
            padding-top: 1.00em; /*// 20211209 Added エインドリ－・プ－プゥ*/
        }

        .columnStyle1 {
            text-align: left;
        }

        .textboxStyle1 {
            min-width: 390px !important;
            width: 390px !important;
            max-width: 390px !important;
            height: 30px !important;
            border:1px solid #9b9b9b !important;
        }

        .dLeave {
            padding-left: 2.3em;
            margin-top: 1em;
            margin-right:10px;
        }

        .setDate {
            margin-left: 1.7em;
            width: 1115px;
            border-radius: 6px;
            border-style: none;
            height: 32px;
            margin-top: 0.5em;
        }

        #modal-footer {
            height: 75px;
            padding-top: 20px;
            background-color: #eee;
            margin-top: 55px;
        }

        /*#btn_save {
            width: 120px;
            height: 33px;
            font-size: small;
            border-radius: 7px;
        }*/

        /*#btn_cancel {
            background-color: white;
            color: blue;
            margin-left: 20px;
            width: 120px;
            height: 33px;
            font-size: small;
            border-radius: 7px;
        }*/

        .modal-content {
            width: 200px;
        }

        .DisplayNone {
            display: none;
        }

        #divTantouDate {
            width: 200px;
            margin-left: 1em;
            padding-left: -5em;
            margin-top: 0.6em;
        }

        /*#btnEnroll {
            background-color: lightgray;
            border-color: darkgrey;
            border-style: solid;
            border-width: 1.5px;
            margin-top: 0.5em;
        }

        #btnLeave {
            border-color: darkgrey;
            border-style: solid;
            margin-top: 0.5em;
        }*/

        .PopupModalBackground1 {
            background-color: gray;
            filter: alpha(opacity=70);
            opacity: 0.3;
            width: 100%;
        }

        .btnSwitch{
            width: 70px;
            height:35px;
            border:1px solid lightgray !important;
            font-size:13px;
        }

        /*20211222 Added エインドリ－・プ－プゥ*/
          .error{
            font-family:"Hiragino Sans", "Open Sans", "Meiryo", "Hiragino Kaku Gothic Pro", "メイリオ", "MS ゴシック", "sans-serif";
            font-size:11px;
            color:red;
        }


    </style>
    <script>
        function validate(value) {
            var reg = /^([A-Za-z0-9_\-\.])+\@([A-Za-z0-9_\-\.])+\.([A-Za-z]{2,4})$/;
            if (reg.test(value) == false) {
                alert('Invalid Email Address');
                return false;
            }
        }
        //20211222 Added エインドリ－・プ－プゥ
        function errorLabelClear()
        {
            document.getElementById("<%=LB_login_Error.ClientID %>").innerText = "";
            document.getElementById("<%=LB_tantoumei_Error.ClientID%>").innerText = "";
            document.getElementById("<%=LB_pwd_Error.ClientID%>").innerText = "";
            document.getElementById("<%=LB_pwdConfirm_Error.ClientID%>").innerText = "";
        }
    </script>
</head>
<body class="bg-transparent" style="height: 650px;">
    <form id="form1" runat="server" autocomplete="off">
     <asp:ScriptManager ID="ScriptManager1" EnablePageMethods="True" runat="server">
    <scripts>
                <asp:ScriptReference Name="jquery" />
                <asp:ScriptReference Name="bootstrap" />
                <asp:ScriptReference Path="../Scripts/Common/FixFocus.js" />
                <asp:ScriptReference Path="../Scripts/Common/Common.js" />
            </scripts>
    </asp:ScriptManager>
        <asp:PlaceHolder runat="server">
            <%: Scripts.Render("~/bundles/jqueryui") %>
        </asp:PlaceHolder>
        <div style="max-width: 715px; min-width: 715px; min-height: 650px; background-color: white; margin-left: auto; margin-right: auto; padding: 0; left: 50%; top: 50%; position: absolute; transform: translate(-50%, -50%);">
            <asp:UpdatePanel ID="updTokuisakiDate" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <%--<div class="modal-header" style="padding-top: 0px; margin-top: 25px; padding-right: 50px;">
                        <h5 class="modal-title" id="lbltitle" runat="server">ユーザーを新規登録</h5>
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <asp:Button ID="btn_cross" runat="server" CssClass="btn-close" data-bs-dismiss="modal" aria-label="Close" OnClick="btn_cross_Click" />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>--%>
                    <div class="modal-header" style="padding-top: 0px;padding-right: 50px;margin-top:14px;padding-left:35px;">
                    <%--<div class="f_row" style="height: 47px; padding-top: 17px;">--%>
                        <asp:Label ID="lbl_title" runat="server" Text="ユーザーを新規登録" Font-Bold="True" Font-Size="Large" style="padding-top:9px;"></asp:Label>
                        <asp:Button ID="btn_cross" runat="server" CssClass="PopupCloseBtn mr-n3" style="margin-top:1px;" Text="✕" OnClick="btn_cross_Click" />
                    </div>
                    <div class="modal-body" style="padding-left:35px;">
                        <table class="auto-style1">
                            <%--login Id and textbox--%>
                            <%--<tr id="tr_mail" runat="server">--%>
                            <tr>
                                <td class="tableStyle">
                                    <asp:Label ID="lb_login" runat="server" Text="ログインID"></asp:Label><span class="JC02Star-color fw-bold" style="margin-left:3px;">*</span>
                                </td>
                                <td class="columnStyle"><%--20211208 Added error text--%> <%--20211209 deleted error text--%>
                                        <%--<asp:TextBox ID="tb_login" runat="server" onchange="validate(this.value)" onkeyup="errorLabelClear();" onkeydown="return (event.keyCode!=13);" CssClass="form-control TextboxStyle textboxStyle" placeholder="メールアドレスを入力" ></asp:TextBox>--%>
                                   <%--20211222 Updated エインドリ－・プ－プゥ　(delete onchange method and add error label)--%>     
                                   <asp:TextBox ID="tb_login" runat="server" onkeyup="errorLabelClear();" onkeydown="return (event.keyCode!=13);" CssClass="form-control TextboxStyle textboxStyle1" placeholder="メールアドレスを入力" ></asp:TextBox>
                                   <asp:Label ID="LB_login_Error" Text="" runat="server" CssClass="error" ></asp:Label>
                                </td>
                            </tr>
                            <%--Tantoumei--%>
                            <tr>
                                <td class="tableStyle">
                                    <asp:Label ID="lb_tantoumei" runat="server" Text="ユーザー名"></asp:Label><span class="JC02Star-color fw-bold" style="margin-left:3px;">*</span>
                                </td>
                                <td class="columnStyle">
                                    <div>
                                        <asp:TextBox ID="tb_tantoumei" runat="server" CssClass="form-control TextboxStyle textboxStyle1" onkeyup="errorLabelClear();" onkeydown="return (event.keyCode!=13);" MaxLength="50" AutoCompleteType="Disabled" BorderWidth="1px"></asp:TextBox>
                                       <%--20211222 Added エインドリ－・プ－プゥ--%>
                                        <asp:Label ID="LB_tantoumei_Error" Text="" runat="server" CssClass="error"></asp:Label>
                                    </div>
                                </td>
                            </tr>
                            <%--Input Password--%>
                            <tr>
                                <td class="tableStyle">
                                    <asp:Label ID="lb_pwd" runat="server" Text="パスワード"></asp:Label><span class="JC02Star-color fw-bold" style="margin-left:3px;">*</span>
                                </td>
                                <td class="columnStyle">
                                    <%--20220119 Update MaxLength=20 エインドリ－・プ－プゥ--%>
                                    <asp:TextBox ID="tb_pwd" name="tb_pwd" runat="server" CssClass="form-control TextboxStyle textboxStyle1" onkeyup="errorLabelClear();" type="Password" onkeydown="return (event.keyCode!=13);" MaxLength="20" onfocus="this.type='password'" autocomplete="new-password"></asp:TextBox>
                                   <%--20211222 Added エインドリ－・プ－プゥ--%>
                                    <asp:Label ID="LB_pwd_Error" Text="" runat="server" CssClass="error"></asp:Label>
                                </td>
                            </tr>
                            <%--Password Confirm--%>
                            <tr>
                                <td class="tableStyle">
                                    <asp:Label ID="lb_pwdConfirm" runat="server" Text="パスワード再入力"></asp:Label><span class="JC02Star-color fw-bold" style="margin-left:3px;">*</span>
                                </td>
                                <td class="columnStyle"><%--20211209 Deleted ErrorMessage--%><%--20220119 Update MaxLength=20 エインドリ－・プ－プゥ--%>
                                    <asp:TextBox ID="tb_pwdConfirm" runat="server" CssClass="form-control TextboxStyle textboxStyle1" type="Password" onkeyup="errorLabelClear();" onkeydown="return (event.keyCode!=13);" MaxLength="20"></asp:TextBox>
                                   <%--20211222 Added エインドリ－・プ－プゥ--%>
                                    <asp:Label ID="LB_pwdConfirm_Error" Text="" runat="server" CssClass="error"></asp:Label>
                                    <%-- <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="tb_pwd" ControlToValidate="tb_pwdConfirm"  ForeColor="Red"></asp:CompareValidator>--%> <%--20211213 Colse Comment エインドリ－・プ－プゥ--%>
                                </td>
                            </tr>
                            <%--Kyoten Button--%>
                            <tr>
                                <td class="tableStyle"  style="text-align:right !important;padding-right:2px;">
                                    <asp:Label ID="lb_kyoten" runat="server" Text="拠点"></asp:Label>
                                </td>
                                <td class="columnStyle">
                                    <asp:UpdatePanel ID="upd_KYOTENLIST" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <div id="divKyotenbtn" runat="server">
                                                <asp:Button runat="server" ID="btnKyoten" Text="追加" CssClass="JC10GrayButton" OnClick="BT_Kyoten_Add_Click" Height="30px" />
                                            </div>
                                            <div style="float: left; width: 370px; display: none;" id="divKyotenLabel" runat="server">
                                                <asp:Label ID="lblsKYOTEN" runat="server" ForeColor="#0080C0"></asp:Label>
                                                <asp:Label ID="lblcKYOTEN" runat="server" Visible="false"></asp:Label>
                                                <asp:Button ID="BT_sKYOTENLIST_Cross" runat="server" BackColor="White" Text="✕" Style="vertical-align: middle; margin-left: 10px; font-weight: bolder; height: 30px; border: none;" OnClick="BT_sKYOTENLIST_Cross_Click" />
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                            <%--Bumon Button--%>
                            <tr>
                                <td class="tableStyle"  style="text-align:right !important;padding-right:2px;">
                                    <asp:Label ID="lb_bumon" runat="server" Text="部門"></asp:Label>
                                </td>
                                <td class="columnStyle">
                                    <asp:UpdatePanel ID="upd_Bumon" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <div id="divBumonbtn" runat="server">
                                                <asp:Button runat="server" ID="btnBumon" Text="追加" CssClass="JC10GrayButton" OnClick="btnBumon_Click" Height="30px" />
                                            </div>
                                            <div style="float: left; width: 370px; display: none;" id="divBumonLabel" runat="server">
                                                <asp:Label ID="lblcBumon" runat="server" Visible="false"></asp:Label>
                                                <asp:Label ID="lblsBumon" runat="server" ForeColor="#0080C0"></asp:Label>
                                                <asp:Button ID="BT_sBumon_Cross" runat="server" BackColor="White" Text="✕" Style="vertical-align: middle; margin-left: 10px; font-weight: bolder; height: 30px; border: none;" OnClick="BT_sBumon_Cross_Click" />
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                            <%--Authorithy Button--%>
                            <tr>
                                <td class="tableStyle" style="text-align:right !important;padding-right:2px;">
                                    <asp:Label ID="lb_Authorithy" runat="server" Text="権限"></asp:Label>
                                </td>
                                <td class="columnStyle">

                                    <asp:UpdatePanel ID="upd_Kengen" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <div id="divKengenBtn" runat="server">
                                                <asp:Button runat="server" ID="btnKengen" Text="追加" CssClass="JC10GrayButton" OnClick="btnKengen_Click" Height="30px" />
                                            </div>
                                            <div style="float: left; width: 370px; display: none;" id="divKengenLabel" runat="server">
                                                <asp:Label ID="lblsKengen" runat="server" ForeColor="#0080C0"></asp:Label>
                                                <asp:Label ID="lblcKengen" runat="server" Visible="false"></asp:Label>
                                                <asp:Button ID="BT_sKengen_Cross" runat="server" BackColor="White" Text="✕" Style="vertical-align: middle; margin-left: 10px; font-weight: bolder; height: 30px; border: none;" OnClick="BT_sKengen_Cross_Click" />
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>


                            <%--Checkbox leave--%>
                            <tr>
                                <td></td>
                                <td class="columnStyle">
                                    <table>
                                        <tr>
                                            <td>
                                                <%--<div class="btn-group" role="group" aria-label="...">
                                        <asp:Button ID="btnEnroll" runat="server" CssClass="btn"  Text="在籍"
                                            OnClick="btnOn_Click" BackColor="LightGray" style="font-size:13px;padding:6px 12px 6px 12px;letter-spacing:1px;border-radius:3px;"/>
                                        <asp:Button ID="btnLeave" runat="server" CssClass="btn btn-default"  Text="退社" style="font-size:13px;padding:6px 12px 6px 12px;letter-spacing:1px;border-radius:3px;"
                                            OnClick="btnOff_Click" />
                                            </div>--%>
                                                <div class="btn-group" role="group" aria-label="Basic example">
                                           
                                             <asp:Button ID="btnEnroll" runat="server" Text="在籍" CssClass="btnSwitch"  OnClick="btnOn_Click"/>
                                                    
                                             <asp:Button ID="btnLeave" runat="server" Text="退社" CssClass="btnSwitch" OnClick="btnOff_Click"/>
                                             

                                         </div>
                                            </td>
                                            <td>
                                                <asp:Label ID="DLeave" runat="server" Text="退社日" CssClass="dLeave" Visible="false"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:UpdatePanel ID="updTantoudate" runat="server" Visible="false" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <asp:Button ID="btnTantouDate" runat="server" Text="日付を設定" CssClass="JC10GrayButton" onmousedown="getTantouBoardScrollPosition();" OnClick="btnTantouDate_Click" Height="30px" />
                                           
                                                <div id="divTantouDate" class="DisplayNone" runat="server" style="margin-top:-2px !important;margin-left:5px !important;">
                                                <asp:Button ID="btnLeftArrowdTokuisaki" runat="server" Text="<" CssClass="DateArrowButton DisplayNone" OnClick="btnLeftArrowdTantou_Click" />
                                                <asp:Label ID="lbldTantou" runat="server" Text="" CssClass="GrayLabel"></asp:Label>
                                                <asp:Label ID="lblTantouDateYear" runat="server" CssClass="DisplayNone"></asp:Label>
                                                <asp:Button ID="btndTantouCross" CssClass="CrossBtnGray" runat="server" Text="✕" OnClick="btndTantouCross_Click" />
                                                <asp:Button ID="btnRightArrowdTantou" runat="server" Text=">" CssClass="DateArrowButton DisplayNone" OnClick="btnRightArrowdTantou_Click" />
                                                </div>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                            </td>
                                        </tr>
                                    </table>
                                    
                                        
                                        

                                </td>
                            </tr>
                        </table>
                    </div>
                    <asp:HiddenField ID="hiddenFieldId" runat="server" />
                    <asp:HiddenField ID="HF_isChange" runat="server" />
                </ContentTemplate>
            </asp:UpdatePanel>
            <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <div class="text-center" id="modal-footer">
                        <asp:Button ID="btn_save" class="JCHKadouJikanSetteiBtn BlueBackgroundButton" data-bs-dismiss="modal" style="font-size:14px;" runat="server" OnClick="save_btn_Click" Text="保存" />

                        <asp:Button ID="btn_cancel" CssClass="btn text-primary font btn-sm btn btn-outline-primary" style="width:auto !important;background-color:white; margin-left:10px;border-radius:3px;font-size:13px;padding:6px 12px 6px 12px;letter-spacing:1px;" data-bs-dismiss="modal" runat="server" Text="キャンセル" OnClick="btn_cancel_Click" Height="35px" />

                    </div>

                </ContentTemplate>
            </asp:UpdatePanel>

        </div>
    <!--Date-->
    <asp:UpdatePanel ID="updDatePopup" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:Button ID="btnDatePopup" runat="server" Text="Button" CssClass="DisplayNone" />
            <asp:ModalPopupExtender ID="mpeDatePopup" runat="server" TargetControlID="btnDatePopup"
                PopupControlID="pnlDatePopupScroll" BackgroundCssClass="PopupModalBackground" BehaviorID="pnlDatePopupScroll"
                RepositionMode="None">
            </asp:ModalPopupExtender>
            <asp:Panel ID="pnlDatePopupScroll" runat="server" Style="display: none;" CssClass="PopupScrollDiv">
                <asp:Panel ID="pnlDatePopup" runat="server">
                    <iframe id="ifDatePopup" runat="server" class="NyuryokuIframe RadiusIframe" scrolling="no" style="max-width: 260px; min-width: 260px; max-height: 365px; min-height: 365px;"></iframe>
                    <asp:Button ID="btnCalendarClose" runat="server" Text="Button" CssClass="DisplayNone" OnClick="btnCalendarClose_Click" />
                    <asp:Button ID="btnCalendarSettei" runat="server" Text="Button" CssClass="DisplayNone" OnClick="btnCalendarSettei_Click" />
                    <asp:Button ID="btnTokuisakiClose" runat="server" Text="Button" Style="display: none" />
                </asp:Panel>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>

    <!--ポップアップ画面-->
    <asp:UpdatePanel ID="updSentakuPopup" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <asp:Button ID="btnSentakuPopup" runat="server" Text="Button" CssClass="DisplayNone" />

            <asp:ModalPopupExtender ID="mpeSentakuPopup" runat="server" TargetControlID="btnSentakuPopup"
                PopupControlID="pnlSentakuPopupScroll" BehaviorID="pnlSentakuPopupScroll" BackgroundCssClass="PopupModalBackground1">
            </asp:ModalPopupExtender>
            <asp:Panel ID="pnlSentakuPopupScroll" runat="server" Style="display: none; overflow: hidden;" CssClass="PopupScrollDiv" HorizontalAlign="Center">
                <asp:Panel ID="pnlSentakuPopup" runat="server">
                    <iframe id="ifSentakuPopup" runat="server" scrolling="no" class="NyuryokuIframe" style="border-radius: 0px;"></iframe>
                    <asp:Button ID="btnClose" runat="server" Text="Button" Style="display: none" OnClick="btnClose_Click" />
                    <asp:Button ID="btnKyotanSelect" runat="server" Text="Button" Style="display: none" OnClick="btnKyotenSelect_Click" />
                    <asp:Button ID="btnBumonSelect" runat="server" Text="Button" Style="display: none" OnClick="btnBumonSelect_Click" />
                    <asp:Button ID="btnKengennSelect" runat="server" Text="Button" Style="display: none" OnClick="btnKengennSelect_Click" />
                </asp:Panel>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:HiddenField ID="hdnHome" runat="server" />
    </form>
</body>
</html>


