<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="JC14TantouKensaku.aspx.cs" Inherits="JobzCloud.WebFront.JC14TantouKensaku" ValidateRequest ="False" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
    
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
   <%-- 20220211  エインドリ－・プ－プゥ Added--%>
    <link href="../Icons/font/bootstrap-icons.css" rel="stylesheet" />
    <asp:PlaceHolder runat="server">
        <%: Scripts.Render("~/bundles/modernizr") %>
        <%: Styles.Render("~/style/StyleBundle1") %>
        <%: Scripts.Render("~/scripts/ScriptBundle1") %>
        <%: Styles.Render("~/style/UCStyleBundle") %>

    </asp:PlaceHolder>
    <link href="../Content1/bootstrap.min.css" rel="stylesheet" />
    <script src="../Scripts/bootstrap.bundle.min.js"></script>

    <link href="../resources/css/bootstrap.min.css" rel="stylesheet" />
    <script src="../resources/js/jquery.min.js"></script>
    <script src="../resources/js/bootstrap.min.js"></script>
    <script src="../resources/js/popper.min.js"></script>

    <style>
        #form1 {
            /*margin:80px;*/
            margin-top: 20px;
        }

        #line {
            height: 8px;
            /*background-color: lightgray;*/
            background-color:#eeeeee !important;
        }

        .f_row {
            width: 660px;
            margin-left: auto;
            margin-right: auto;
        }

        #s_row {
            padding-top: 15px;
            width: 620px;
            margin-left: auto;
            margin-right: auto;
        }

        .searchStyle {
            width: 410px;
            height: 30px;
            caret-color: auto;
            display: inline-block;

        }

        .btnStyle {
            height: 40px;
            width: 150px;
            margin-left: 1.5em;
            border-radius: 7px;
            border-style: none;
            color: white;
            float: right;
        }

        #table_tantousha {
            width: 600px;
        }

        #div_footer {
            height: 75px; /*20220307 Updated　エインドリ－*/
            padding-top: 15px;
            /*background-color: lightgray;*/
            background-color: #eee;/*20220307 Updated　エインドリ－*/
        }

        #btn_cancel {
            margin-left: 20px;
            width: 120px;
            height: 33px;
            font-size: small;
            border-radius: 7px;
        }

        #table_tantousha {
            margin-top: 12px;
        }

        #table_row {
            border-spacing: 10px;
        }

        .dropbtn {
            padding: 5px;
            background-color: #04AA6D;
            border: none;
        }

        #tr {
            margin: 30px;
        }

        .dropdown {
            position: relative;
            display: inline-block;
            padding-right: 23px;
            padding-left:20px;
            min-width: 190px;
            max-width: 190px;
            margin-top: 10px;
            margin-right:10px;
        }

        .dropdown-content {
            display: none;
            position: absolute;
            float: right;
            min-width: 10px;
            z-index: 1;
            margin-left: 1px;
            top: 0%;
        }

            .dropdown-content a {
                color: black;
                padding-right:2px;
                text-decoration: none;
                height: 10px;
            }

        .fa-fa-trash-o:hover + .tooltip-text, .fa-fa-list:hover + .tooltip-text, .fa-fa-pencil:hover + .tooltip-text {
            /*color: black;*/
            animation: fadeIn 2s;
        }

        .dropdown:hover .dropdown-content {
            display: block;
            float: right;
        }

        .dropdown-content a:hover {
            background-color: transparent;
            cursor: pointer;
        }

        .divTantouList {
            height: 400px;
            overflow-y: auto;
            display: block;
            margin-bottom: 17px;
            /*width:620px;*/
            width: 640px; /*20220211 Updated width エインドリ－・プ－プゥ*/
            margin-left: auto;
            margin-right: auto;
        }

        .crossbtn {
            background-color: white;
            padding: 0px;
            border: none;
            float: right;
            width: 53px;
            font-size: 21px;
            height: 48px;
            line-height: 26px;
            color: rgb(75,75,75) !important;
            margin-top: -10px;
            margin-right: 10px;
        }

            .crossbtn:hover {
                background: rgba(230, 230, 230, 1);
                color: white !important;
            }

        #updUserAdd {
            width: 100px;
            float: right;
        }
        /*20220211　エインドリ－・プ－プゥ Added Start*/ 
        .bi:hover{
            color:#0000EE;
        }
         /*20220211　エインドリ－・プ－プゥ Added End*/ 
    </style>
</head>
<body class="bg-transparent" style="overflow:hidden !important;">
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" EnablePageMethods="True" runat="server">
            <Scripts>
                <asp:ScriptReference Name="jquery" />
                <asp:ScriptReference Name="bootstrap" />
                <asp:ScriptReference Path="../Scripts/Common/FixFocus.js" />
                <asp:ScriptReference Path="../Scripts/Common/Common.js" /> <%--20220117 Added エインドリ－・プ－プゥ--%>
            </Scripts>
        </asp:ScriptManager>
        <%--20220117 Added エインドリ－・プ－プゥ--%>
        <asp:PlaceHolder runat="server">
            <%: Scripts.Render("~/bundles/jqueryui") %>
        </asp:PlaceHolder>
        <asp:UpdatePanel ID="updBody" runat="server" UpdateMode="Conditional" RenderMode="Inline">
            <ContentTemplate>
                <div style="min-width: 715px; min-height: 650px; background-color: white; margin-left: auto; margin-right: auto; padding: 0; left: 50%; top: 50%; position: absolute; transform: translate(-50%, -50%);padding-top:16px;">
                    <div class="f_row" style="height: 53px; padding-top: 5px;">
                        <asp:Label ID="lbl_title" runat="server" Text="ユーザーを選択" Font-Bold="True" Font-Size="Large"></asp:Label>
                        <asp:Button ID="btn_cross" runat="server" CssClass="crossbtn" Text="✕" OnClick="btn_cross_Click" />
                    </div>
                     <div class="Borderline"></div>
                    <div class="f_row" style="padding-right:40px !important;padding-top:5px;">
                        <asp:TextBox ID="tb_code" runat="server" placeholder="コード、名前、部門で検索できます。" OnTextChanged="tb_search_textChanged" AutoPostBack="true" CssClass="form-control TextboxStyle searchStyle" TextMode="Search"></asp:TextBox>
                        <asp:UpdatePanel ID="updUserAdd" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <asp:Button ID="btn_add" runat="server" Text="ユーザーを追加" OnClick="btn_add_Clicked" CssClass="BlueBackgroundButton  JC10SaveBtn"/>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <div id="s_row">
                        <asp:CheckBox ID="chk_taisya" runat="server" OnCheckedChanged="chk_taisya_CheckedChanged" AutoPostBack="true" />
                        <asp:Label ID="Label1" runat="server" Text="退社のみを表示"></asp:Label>
                    </div>
                    <div class="divTantouList" >
                        <table id="tb" runat="server">
                            <%-- Create Rows and Columns Dynamically --%>
                        </table>
                        <div style="width: 100%; margin-top: 10px;" align="center">
                            <asp:Label ID="lblDataNai" runat="server" Style="font-size: 14px; font-weight: normal;" Text="該当するデータがありません"></asp:Label>
                        </div>
                        <asp:HiddenField ID="hiddenCode" Value="" runat="server" />
                        <asp:HiddenField ID="hiddenName" Value="" runat="server" />
                        <asp:HiddenField ID="HF_fTime" Value="" runat="server" />
                        <asp:Button ID="btnLink" Text="" runat="server" CssClass="btn btn-outline-primary" OnClick="btnLink_Click" Style="display: none;" />
                        <asp:Button ID="btnEdit" Text="" runat="server" CssClass="btn btn-outline-primary" OnClick="btnEdit_Click" Style="display: none;" />
                    </div>
                    <div id="div_footer" class="text-center" style="padding-top:20px;">
                        <asp:Button ID="btnSettei" runat="server" Text="設定" CssClass="JCHKadouJikanSetteiBtn BlueBackgroundButton" style="margin-left:0px !important;" OnClick="btnSettei_Click" />
                        <asp:Button ID="btn_cancel" Text="キャンセル" runat="server" CssClass="btn text-primary font btn-sm btn btn-outline-primary" style="width:auto !important;background-color:white; margin-left:10px;border-radius:3px;font-size:13px;padding:6px 12px 6px 12px;letter-spacing:1px;" OnClick="btn_cancel_Click" Height="35px" />
                        <asp:Button ID="btn_delete" Text="キャンセル" runat="server" CssClass="btn btn-outline-primary" OnClick="btn_delete_Click" Style="display: none;" Height="35px" />
                        <%--20220117 Added エインドリ－・プ－プゥ--%>
                        <asp:Button ID="btnDeleteTantou" Text="削除" runat="server" CssClass="btn btn-outline-primary" OnClick="btnDeleteTantou_Click" Style="display: none;" />
                    </div>
                </div>

                <!--ポップアップ画面-->
        <asp:UpdatePanel ID="updSentakuPopup" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:Button ID="btnSentakuPopup" runat="server" Text="Button" CssClass="DisplayNone" />
                <asp:ModalPopupExtender ID="mpeSentakuPopup" runat="server" TargetControlID="btnSentakuPopup"
                    PopupControlID="pnlSentakuPopupScroll" BehaviorID="pnlSentakuPopupScroll">
                </asp:ModalPopupExtender>
                <asp:Panel ID="pnlSentakuPopupScroll" runat="server" Style="display: none; overflow: hidden;" CssClass="PopupScrollDiv">
                    <asp:Panel ID="pnlSentakuPopup" runat="server">
                        <iframe id="ifSentakuPopup" runat="server" scrolling="no" class="NyuryokuIframe" style="border-radius: 0px;"></iframe>
                        <asp:Button ID="btnClose1" runat="server" Text="Button" Style="display: none" OnClick="btnClose_Click" />
                        <asp:Button ID="btnToLogin" runat="server" Text="Button" Style="display: none" OnClick="btnToLogin_Click"/>
                    </asp:Panel>
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>

        <asp:HiddenField ID="hdnHome" runat="server" />
        <asp:HiddenField ID="HF_selectIndex" runat="server" />
        <asp:HiddenField ID="HF_isKensaku" runat="server" />
            </ContentTemplate>
        </asp:UpdatePanel>
        <script>


            function Confirm(id) { //20211217 Updated エインドリ－・プ－プゥ
                //if (confirm("削除しますか？")) {
                //    var newURL = document.location.href = "?value=" + id + "&" + "del";
                //    return newURL;
                //}
                //else {
                //    //alert("false");
                //    var newURL = document.location.href = "../WebFront/JC14TantouKensaku";
                //    return newURL;
                //}
                hiddenCode.value = id;
                document.getElementById("btn_delete").click();
            }

            function checkEnterKeyEvent(event) {
                if (event.keyCode == 13) {
                    return false;
                }
                else {
                    return true;
                }
            }

            function get_id(element, index) {
                if (index != "") {
                    index = "'" + index + "'";
                    if (element.style.backgroundColor == 'rgb(68, 114, 196)') {
                        var txt = HF_selectIndex.value;
                        var ar = txt.split(',');
                        var i = ar.indexOf(index);
                        if (i > -1) ar.splice(i, 1);
                        HF_selectIndex.value = ar;

                    }
                    else {
                        var txt = HF_selectIndex.value;
                        if (txt != "") {
                            txt = txt.replace("'0'", "");
                            HF_selectIndex.value = txt + "," + index;
                        }
                        else {
                            HF_selectIndex.value = index;
                        }
                    }
                }
                else {
                    index = "'" + index + "'";
                    HF_selectIndex.value = index;
                }
                //hiddenCode.value = element;
                //hiddenName.value = stantou;
                
                document.getElementById("btnLink").click();
            }

            function get_id1(ctantou, stantou) {
                hiddenCode.value = ctantou;
                hiddenName.value = stantou;
                document.getElementById("btnLink").click();
            }

            function edit(ctantou) {
                //alert(ctantou);
                hiddenCode.value = ctantou;
                document.getElementById("btnEdit").click();
            }
        </script>

        
    </form>
</body>
</html>