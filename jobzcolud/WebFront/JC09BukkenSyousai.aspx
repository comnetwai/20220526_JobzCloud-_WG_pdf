<%@ Page Title="物件登録＆詳細" Language="C#" MasterPageFile="~/WebFront/JC99NavBar.Master" AutoEventWireup="true" CodeBehind="JC09BukkenSyousai.aspx.cs" Inherits="JobzCloud.WebFront.JC09BukkenSyousai" EnableEventValidation="false" MaintainScrollPositionOnPostback="true" ValidateRequest ="False" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <!DOCTYPE html>

    <html>
    <head>
        <title>物件登録＆詳細</title>
        <asp:PlaceHolder runat="server">
            <%: Scripts.Render("~/bundles/modernizr") %>
            <%: Styles.Render("~/style/StyleBundle1") %>
            <%: Scripts.Render("~/scripts/ScriptBundle1") %>
            <%: Styles.Render("~/style/UCStyleBundle") %>

        </asp:PlaceHolder>
        <webopt:BundleReference runat="server" Path="~/Content1/bootstrap" />
        <webopt:BundleReference runat="server" Path="~/Content1/css" />
        <script src="../Scripts/colResizable-1.6.min.js"></script>
        <script src="../Scripts/cookie.js"></script>
        <meta name="google" content="notranslate" />
        <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=0, shrink-to-fit=no" />
        <style>
            .oneline {
                width: 800px;
                border: none;
                float: left;
                display: inline-block;
            }
            .floatoneline {
                height: 60px;
                border: none;
                float: left;
            }

            .pageHolder {
                overflow: auto;
                width: 100%;
            }

            .JC09SaveBtn {
                font-size: 14px;
                //height: 36px;
                border-radius: 3px;
                margin-right: 13px;
                padding:6px 12px 6px 12px;
                letter-spacing:1px;
            }

            .JC09GrayButton {
                //width: 65px;
                background: rgb(242, 242, 242);
                font-size: 13px;
                //height: 30px;
                border-radius: 3px;
                border: none;
                padding:6px 12px 6px 12px;
                letter-spacing:1px;
            }

            .JC09BukenButton {
                background: transparent;
                background-image: url(../Img/expand-more-1782315-1514165.png);
                background-repeat: no-repeat;
                background-position: right;
                background-size: contain;
                margin-bottom: 10px;
                margin-left: 27px;
                text-align: left;
                font-size: 13px;
                height: 30px;
                border-radius: 6px;
                border: none;
                border: 1px solid #a6a6a6;
            }

            .lbl_mitsumoriJotai {
                float: right;
            }

            .JC09GrayButton:hover {
                background: rgb(209,205,205);
            }

            .JC09RedButton {
                width: 65px;
                background: rgb(255, 0, 0);
                font-size: 13px;
                height: 30px;
                border-radius: 6px;
                border: none;
            }

                .JC09RedButton:hover {
                    background: rgb(158,41,41);
                }

            .JC09TextBox {
                width: 350px !important;
                max-width: 350px;
                height: 28px !important;
            }

            .JC09TextBox1 {
                /*width: 100px !important;*/
                width:88px;  /*20220425 Eaindray*/
                max-width: 88px; /*20220425 Eaindray*/
                height: 28px !important;
                text-align:right;
            }

            .JC09TextArea {
                padding: 3px !important;
                width: 355px !important;
                max-width: 355px;
                font-size: 13px !important;
                resize: none;
                word-break: break-all;
                border: 0.5px solid rgb(155, 155, 155) !important;
            }

            .JC09txtFilePath {
                font-size: 13px;
                min-width: 600px;
                max-width: 600px;
            }


            .auto-style2 {
                width: 1300px;
                height: 340px;
            }

            .JC09DateButton {
                width: 70px;
                background: rgb(255, 255, 255);
                font-size: 13px;
                height: 30px;
                border-radius: 6px;
                border: none;
            }

                .JC09DateButton:hover, .JC10CancelBtn:hover, .JC12CancelBtn:hover {
                    background: rgb(209,205,205);
                }


            .auto-style3 {
                height: 43px;
            }

            .GridViewStyle 
            {
                z-index:3 !important;
                overflow:unset !important;
                overflow-x:auto !important;
                
            }

            
            .GridViewStyle tr td
            {
                z-index:3 !important;
                 overflow:unset !important;
                overflow-x:auto !important;
            }

              .GridViewStyle tr th
            {
                z-index:3 !important;
                overflow:unset !important;
               overflow-x:auto !important;
            }

            
            .auto-style4 {
                float: left;
                width: 150px;
                position: relative;
                left: 0px;
                top: 4px;
            }

            /*#DIV_GV 
            {
                position: relative;
               overflow-y: hidden !important;
               overflow-x:auto !important;
               z-index:2;
                &#DIV_GV1 {
                    z-index: 1;
                    position:absolute;
                }
            }*/

        </style>

        <%--<script type = "text/javascript">
       function Confirm() {

            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
           confirm_value.name = "confirm_value";
           confirm.value = "no";
           var hiddenField = document.getElementById('hiddenFieldId');
           var hf_ischange = document.getElementById('HF_isChange');
           if (hf_ischange.value == '1')
           {
               if (confirm("物件情報を入力されています。保存しますか？ "))
               {
                   hiddenField.value = 'Yes';
                   return;
               }
               else
               {
                   hiddenField.value = 'no';
                   return;
               }
           }
            //document.forms[0].appendChild(confirm_value);
        }
    </script>
     <script type = "text/javascript" >
        function confirm_proceed()
        {
            var hiddenField = document.getElementById('hiddenFieldId');
            hiddenField.value = 'Yes';
            return;
        }
    </script> --%>


        <script type="text/javascript">
            Sys.Application.add_load(function () {

                $("#<%= fileUpload1.ClientID %>").change(function () {
                    __doPostBack();

                });

                 $("#<%= fileUpload2.ClientID %>").change(function () {
                    __doPostBack();

                });

            });

            function copyToClipBoard() {
                var txt = document.getElementById("<%=txtfilepath.ClientID%>");
                txt.select();
                document.execCommand("Copy");
            }

            function OnDragEnter(event) {
  event.stopPropagation();
  event.preventDefault();
}

function OnDragOver(event) {
  event.stopPropagation();
  event.preventDefault();
}

    var filename;
    function OnDrop(event,b) {
        event.stopPropagation();
        event.preventDefault();
        var selectedFiles = event.dataTransfer.files;
        var data = new FormData();
        var imageTypes = ['image/png', 'image/PNG', 'image/gif', 'image/bmp', 'image/jpeg', 'image/jpg', 'image/jfif', 'image/JPG', 'image/JPEG'];
        //, 'image/ai', 'image/AI', 'image/PDF', 'image/pdf'
        for (var i = 0; i < selectedFiles.length; i++) {
            //data.append(selectedFiles[i].name, selectedFiles[i]);
            var fileType = selectedFiles[i].type;
            if (imageTypes.includes(fileType)) {
                document.getElementById("<%=HF_ImgSize.ClientID%>").value = selectedFiles[i].size;
                filename = selectedFiles[i].name;
                const reader = new FileReader();
                reader.onloadend = () => {
                    // use a regex to remove data url part
                    const base64String = reader.result
                        .replace("data:", "")
                        .replace(/^.+,/, "");

                    document.getElementById("<%=HF_ImgBase64.ClientID%>").value = base64String;
                    document.getElementById("<%=HF_FileName.ClientID%>").value = filename;
                    document.getElementById("<%=ID=BT_ImgaeDrop.ClientID%>").click();
                };
                reader.readAsDataURL(selectedFiles[i]);
            }
            else {
                var fileReader = new FileReader();
                var base64String;
                filename = selectedFiles[i].name;
                // Onload of file read the file content
                fileReader.onload = function (fileLoadedEvent) {
                    base64String = fileLoadedEvent.target.result;
                    // Print data in console
                    document.getElementById("<%=HF_ImgBase64.ClientID%>").value = base64String;
                    document.getElementById("<%=HF_FileName.ClientID%>").value = filename;
                    document.getElementById("<%=ID=BT_ImgaeDrop.ClientID%>").click();
                    
                };
                // Convert data to base64
                fileReader.readAsDataURL(selectedFiles[i]);
            }
        }

        <%--var xhr = new XMLHttpRequest();
        xhr.onreadystatechange = function ()
        {
            if (xhr.readyState == 4 && xhr.status == 200 && xhr.responseText)
            {
                //alert("upload done!");
                document.getElementById("<%=ID=BT_HyoshiImgaeDrop.ClientID%>").click();
            }
            else {
                //alert("fail");
                 }
        };
         //alert("aa");
        xhr.open('POST', "JC10MitsumoriTouroku.aspx");
        //xhr.setRequestHeader("Content-type", "multipart/form-data");
        xhr.send(data);--%>
    //alert(selectedFiles.length);
            }

            //20220512 Added Eaindray
           var validJapanese = new RegExp(/^[A-Za-z0-9]*$/);
                var lastValidChar = "";
                function validateJapanese(elem) {
                    if (validJapanese.test(elem.value.replaceAll(",", "")))
                    {
                         lastValidChar = elem.value;
                   }
                  else
                    {
                        if (lastValidChar == undefined) {
                            lastValidChar = "";
                        }
                        elem.value = lastValidChar;
                    }
                }
        </script>
       

    </head>
    <body style="background-color: #d7e4f2 !important; width: 100% !important; margin-left: 0px;">
        <%-- <form id="form" runat="server" >--%>

        <%--<asp:ScriptManager ID="ScriptManager1" EnablePageMethods="True" runat="server">--%>
        <scripts>
                <asp:ScriptReference Name="jquery" />
                <asp:ScriptReference Name="bootstrap" />
                <asp:ScriptReference Path="../Scripts/Common/FixFocus.js" />
            </scripts>
        <%--</asp:ScriptManager>--%>

        <%--<nav id="nav1" runat="server" class="navbar navbar-expand fixed-top bg-white custom-navbar-height custom-navbar2">
                    <div class="navbar-brand navbar-brand-custom">
                        <asp:LinkButton ID="lnkbtnLogo" runat="server">
                            <asp:Image ID="imgLogo" runat="server" ImageUrl="~/Img/signjobzlogodemo.png" Height="38px" Width="250px"/>
                        </asp:LinkButton>
                    </div>
                    <ul class="navbar-nav ml-auto">
                        <li class="nav-item dropdown">
                            <a class="nav-link nav-btn-custom2" href="#" id="navbarDropdownMenuLink" role="button" data-target=".navbar-collapse.show" aria-haspopup="true" aria-expanded="false">
                                  <asp:Label ID="lblLoginUserName" runat="server" CssClass="navbtn2 dropdown-toggle" Text="先小岡太郎" style="font-size:16px;padding-left:8px;padding-right:8px;"></asp:Label>
                                <asp:Label ID="lblLoginUserCode" runat="server" visible="false" Text="先小岡太郎"></asp:Label>
                            </a>
                            <div class="dropdown-menu" aria-labelledby="navbarDropdownMenuLink">
                                <asp:LinkButton ID="lnkbtnKojinJouhou" runat="server" CssClass="dropdown-item dropdown-item-custom" OnClick="lnkbtnKojinJouhou_Click">個人情報設定</asp:LinkButton>
                                <asp:LinkButton ID="lnkbtnLogOut" runat="server" CssClass="dropdown-item dropdown-item-custom" OnClientClick="displayLoadingModal();" OnClick="lnkbtnLogOut_Click">ログアウト</asp:LinkButton>
                            </div>
                        </li>
                    </ul>
                </nav>
                <nav id="nav2" runat="server" class="navbar navbar-expand  navbar-dark fixed-top custom-navbar custom-navbar-height">
                    <div class="navbar-collapse collapse" id="Div2" runat="server">
                        <ul class="navbar-nav mr-auto">
                            <li class="nav-item nav-item-custom">
                                 <asp:LinkButton ID="lnkbtnHome" runat="server" CssClass="nav-link nav-btn-custom" Text="ホーム" OnClick="lnkbtnHome_Click" ForeColor="White"></asp:LinkButton>
                            </li>
                            <li class="nav-item active dropdown dropdown1">
                                <asp:LinkButton ID="lnkbtnBukken" runat="server" CssClass="nav-link nav-btn-custom" Text="物件" data-target=".navbar-collapse.show" aria-haspopup="true" aria-expanded="false" ForeColor="White"></asp:LinkButton>
                                <div class="dropdown-menu dropdown-menu-left dropdown-menu-custom" aria-labelledby="lnkbtnBukken"  style=" z-index: 9999;margin-top:1px;">
                                <asp:LinkButton ID="lnkBukkenToroku" runat="server" CssClass="dropdown-item dropdown-item-custom" OnClick="lnkBukkenToroku_Click">物件を新規作成</asp:LinkButton>
                                <asp:LinkButton ID="lnkBukkenList" runat="server" CssClass="dropdown-item dropdown-item-custom">物件リスト</asp:LinkButton>
                            </div>
                            </li>
                            <li class="nav-item dropdown dropdown1">
                               <asp:LinkButton ID="lnkbtnMitumori" runat="server" CssClass="nav-link nav-btn-custom" Text="見積"　data-target=".navbar-collapse.show" aria-haspopup="true" aria-expanded="false" ForeColor="White"></asp:LinkButton>
                                <div class="dropdown-menu dropdown-menu-left dropdown-menu-custom" aria-labelledby="lnkbtnMitumori"  style=" z-index: 9999;margin-top:1px;" >
                                <asp:LinkButton ID="lnkMitumoriToroku" runat="server" CssClass="dropdown-item dropdown-item-custom" OnClick="lnkMitumoriToroku_Click">見積を新規作成</asp:LinkButton>
                                <asp:LinkButton ID="lnkMitumoriTaCopyCreate" runat="server" CssClass="dropdown-item dropdown-item-custom" OnClick="lnkMitumoriTaCopyCreate_Click">他見積をコピーして作成</asp:LinkButton>
                                  <asp:LinkButton ID="lnkMitumoriDirectCreate" runat="server" CssClass="dropdown-item dropdown-item-custom" OnClick="lnkMitumoriDirectCreate_Click">見積をダイレクト作成</asp:LinkButton>
                                <asp:LinkButton ID="lnkMitumoriList" runat="server" CssClass="dropdown-item dropdown-item-custom">見積リスト</asp:LinkButton>
                            </div>
                            </li>
                             <li class="nav-item nav-item-custom">
                               <asp:LinkButton ID="lnkbtnUriage" runat="server" CssClass="nav-link nav-btn-custom" Text="売上" OnClick="LinkButton4_Click" ForeColor="White"></asp:LinkButton>
                            </li>
                             <li class="nav-item nav-item-custom">
                               <asp:LinkButton ID="lnkbtnSetting" runat="server" CssClass="nav-link nav-btn-custom" Text="設定" OnClick="lnkbtnSetting_Click" ForeColor="White"></asp:LinkButton>
                            </li>
                        </ul>
                    </div>
                </nav>--%>

        <asp:UpdatePanel ID="updHeader" runat="server" UpdateMode="Conditional" RenderMode="Inline">
            <ContentTemplate>
                <div class="JC09TableWidth" id="div3" runat="server">
                    <nav id="nav3" runat="server" class="navbar-expand custom-navbar1 custom-navbar-height" style="position: absolute;">
                        <div class="collapse navbar-collapse JC09navbar JC09TableWidth" id="Div1" runat="server">
                            <label style="font-weight: bold; font-size: 14px;text-align: center; display: inline-block;">物件</label>
                            <asp:Label ID="lblLoginUserCode" runat="server" Text="" Visible="false" />
                            <asp:Label ID="lblLoginUserName" runat="server" Text="" Visible="false" />

                        </div>
                    </nav>
                </div>
                <div class="container body-content" style="background-color:#d7e4f2;padding-top:60px;cursor:context-menu;margin-bottom:100px !important;">
                <table runat="server" id="table" class="JC09TableWidth" align="center" style="cursor:context-menu;display:block;height:auto !important;">
                    <tr>
                        <td id="td1" runat="server" style="width: 100%;cursor:context-menu;">
                            <asp:Panel runat="server" ID="PanelSyousai" Style="height: 830px;cursor:context-menu;" CssClass="JC09TableWidth">
                                <div style="background-color: white;cursor:context-menu;">
                                    <table style="border: none; background-color: white;cursor:context-menu;" class="JC09TableWidth">
                                        <tr>
                                            <td class="auto-style2" style="cursor:context-menu;">
                                                <div runat="server" id="divLabelSyousai" style="border-style: none; border-color: inherit; border-width: medium;display: inline-block; margin-top: 14px; margin-left: -6px;cursor:context-menu;">

                                                    <div id="div_leftSyousai" style="width: 600px; height: 350px; float: left;">
                                                        <table  style="cursor:context-menu;">
                                                            <tr style="height: 60px;">
                                                                <td style="width: 30px; height: 40px;cursor:context-menu;"></td>
                                                                <td style="cursor:context-menu;">
                                                                    <asp:Button runat="server" ID="BT_Save" Text="物件を保存" CssClass="BlueBackgroundButton JC09SaveBtn" OnClick="BT_Save_Click" OnClientClick="confirm_proceed();" />

                                                                </td>
                                                                <td style="cursor:context-menu;">
                                                                    <asp:Button runat="server" ID="BT_Shinki" Text="物件を新規作成"
                                                                        CssClass="BlueBackgroundButton JC09SaveBtn" ForeColor="White"
                                                                        OnClick="BT_Shinki_Click" />
                                                                    <asp:Button runat="server" ID="BT_Delete" Text="削除" CssClass="JC10DeleteBtn" ForeColor="White" OnClick="BT_Delete_Click" />
                                                                </td>
                                                            </tr>
                                                            <tr style="height: 35px;border:0px solid lightgray;">
                                                                <td style="cursor:context-menu;"></td>
                                                                <td style="cursor:context-menu;padding-top:1px;" >
                                                                    <label style="cursor:text;">物件コード</label>
                                                                </td>
                                                                <td style="cursor:context-menu;" >
                                                                    <table style="max-height:35px;">
                                                                        <tr>
                                                                            <td style="min-width:110px;max-width:110px;cursor:text;padding-bottom:7px;">
                                                                                <asp:Label runat="server" ID="LB_BukkenCode" Style="display: block;"></asp:Label>
                                                                            </td>
                                                                            <td style="cursor:text; padding-bottom:7px;">
                                                                                <asp:Label runat="server" ID="lblJoutaiLabel" Style="margin-right: 20px; display: block;">物件状態</asp:Label>
                                                                            </td>
                                                                            <td style="cursor:text;padding-bottom:4px;">
                                                                                <asp:UpdatePanel ID="updJoutai" runat="server" UpdateMode="Conditional">
                                                                                    <ContentTemplate>
                                                                                        <div id="divJoutaibtn" runat="server">
                                                                                            <asp:Button ID="btnJoutai" runat="server" Text="追加" CssClass="JC09GrayButton" onmousedown="getTantouBoardScrollPosition();" OnClientClick="PopupOpenButton()" OnClick="btnJoutai_Click" Height="30px"/>
                                                                                        </div>
                                                                                        <div style="float: left; max-width: 150px; display: none; text-overflow: ellipsis; white-space: nowrap; overflow: hidden;padding-bottom:3px;" id="divJoutaiLabel" runat="server">
                                                                                            <asp:Label ID="lblsJoutai" runat="server" cssClass="JClinkBtPopup"></asp:Label>
                                                                                            <asp:Label ID="lblcJoutai" runat="server" Visible="false"></asp:Label>
                                                                                            <asp:Button ID="btnJoutaiCross" runat="server" BackColor="White" Text="✕" Style="vertical-align: middle; margin-left: 10px; font-weight: bolder; height: 30px; border: none;" Visible="false" />
                                                                                        </div>
                                                                                    </ContentTemplate>
                                                                                </asp:UpdatePanel>
                                                                            </td>
                                                                        </tr>
                                                                    </table>

                                                                </td>
                                                                <td></td>

                                                            </tr>
                                                            <tr style="height: 35px;border:0px solid lightgray;">
                                                                <td class="auto-style1" style="cursor:context-menu;"></td>
                                                                <td class="auto-style1" style="cursor:context-menu;">
                                                                    <label style="cursor:text;">物件名</label>
                                                                    <label style="color:red;cursor:text;">*</label>
                                                                </td>
                                                                <td colspan="3" class="auto-style1" style="cursor:context-menu;padding-bottom:4.5px;">
                                                                    <asp:TextBox runat="server" ID="TB_sBUKKEN" CssClass="form-control TextboxStyle JC09TextBox"  Width="400px" OnTextChanged="TB_sBUKKEN_TextChanged" AutoPostBack="True" MaxLength="80"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr style="height: 35px;border:0px solid lightgray;">
                                                                <td style="cursor:context-menu;"></td>
                                                                <td style="cursor:context-menu;">
                                                                    <label style="cursor:text;">得意先</label>
                                                                    <label style="color:red; cursor:text;">*</label>
                                                                </td>
                                                                <td colspan="3" style="cursor:context-menu;">
                                                                    <asp:UpdatePanel ID="upd_TOKUISAKI" runat="server" UpdateMode="Conditional">
                                                                        <ContentTemplate>
                                                                            <div id="divTokuisakiBtn" runat="server" style="padding-bottom:2px;">

                                                                                <asp:Button runat="server" ID="BT_sTOKUISAKI_Add" Text="追加" OnClick="BT_sTOKUISAKI_Add_Click" CssClass="JC09GrayButton" Height="30px" OnClientClick="PopupOpenButton()" />
                                                                            </div>
                                                                            <div style="float: left; max-width: 280px; display: none; margin-right: 20px; white-space: nowrap; overflow: hidden; text-overflow: ellipsis; display: inline-block;padding-top:3px;" id="divTokuisakiLabel" runat="server">
                                                                                <asp:Label ID="lblsTOKUISAKI" runat="server" Text="" cssClass="JClinkBtPopup"></asp:Label>
                                                                                <asp:Label runat="server" ID="lblcTOKUISAKI" Visible="false"></asp:Label>
                                                                            </div>
                                                                            <div id="divTokuisakiSyosai" runat="server" style="display: none;padding-bottom:5px;">
                                                                                <asp:Button runat="server" ID="BT_sTOKUISAKI_Syousai" Text="詳細" CssClass="JC09GrayButton" OnClick="BT_sTOKUISAKI_Syousai_Click" Height="30px" />
                                                                            </div>
                                                                        </ContentTemplate>
                                                                        <Triggers>
                                                                            <asp:PostBackTrigger ControlID="BT_sTOKUISAKI_Syousai" />
                                                                        </Triggers>
                                                                    </asp:UpdatePanel>
                                                                </td>
                                                            </tr>
                                                            <tr style="height: 35px;">
                                                                <td style="cursor:context-menu;"></td>
                                                                <td style="cursor:context-menu;">
                                                                    <label style="cursor:text;">得意先担当者</label>
                                                                </td>
                                                                <td colspan="3" style="cursor:context-menu;">
                                                                    <asp:UpdatePanel ID="upd_TOKUISAKI_TAN" runat="server" UpdateMode="Conditional">
                                                                        <ContentTemplate>
                                                                            <div id="divTokuisakiTanBtn" runat="server" runat="server" style="padding-bottom:3px;">

                                                                                <asp:Button runat="server" ID="BT_sTOKUISAKI_TAN_Add" Text="追加" CssClass="JC09GrayButton" OnClick="BT_sTOKUISAKI_TAN_Add_Click" Height="30px" OnClientClick="PopupOpenButton()"/>
                                                                            </div>
                                                                            <div style="float: left; max-width: 280px; display: none; margin-right: 17px;white-space: nowrap; overflow: hidden; text-overflow: ellipsis;padding-top:2px;" id="divTokuisakiTanLabel" runat="server">
                                                                                <asp:Label ID="lblsTOKUISAKI_TAN" runat="server" cssClass="JClinkBtPopup"></asp:Label>
                                                                                <asp:Button ID="BT_sTOKUISAKI_TAN_Cross" runat="server" Text="✕" BackColor="White" Style="vertical-align: middle; margin-left: 10px; font-weight: bolder;border: none;" OnClick="BT_sTOKUISAKI_TAN_Cross_Click1" />
                                                                                <%--<br />  <asp:LinkButton runat="server" ID="LB_sTOKUISAKI_TAN_JUN" ForeColor="#0080C0" Font-Underline="false" Visible="false">0</asp:LinkButton>--%>
                                                                                <br />
                                                                                <asp:Label runat="server" ID="lblsTOKUISAKI_TAN_JUN" ForeColor="#0080C0" Visible="false" Text="0"></asp:Label>

                                                                            </div>
                                                                            <div id="divTokuisakiTanSyosai" runat="server" style="display: none;padding-bottom:4px;">

                                                                                <asp:Button runat="server" ID="BT_sTOKUISAKI_TAN_Syousai" Text="詳細" CssClass="JC09GrayButton" OnClick="BT_sTOKUISAKI_TAN_Syousai_Click" Height="30px" />
                                                                            </div>
                                                                        </ContentTemplate>
                                                                        <Triggers>
                                                                            <asp:PostBackTrigger ControlID="BT_sTOKUISAKI_TAN_Syousai" />
                                                                        </Triggers>
                                                                    </asp:UpdatePanel>

                                                                </td>
                                                            </tr>
                                                            <tr style="height: 35px;">
                                                                <td style="cursor:context-menu;"></td>
                                                                <td style="cursor:context-menu;">
                                                                    <label style="cursor:text;">自社担当者</label>
                                                                </td>
                                                                <td style="cursor:context-menu;">
                                                                    <asp:UpdatePanel ID="upd_JISHATANTOUSHA" runat="server" UpdateMode="Conditional">
                                                                        <ContentTemplate>
                                                                            <div id="divTantousyaBtn" runat="server">
                                                                                <asp:Button runat="server" ID="BT_JisyaTantousya_Add" Text="追加" CssClass="JC09GrayButton" OnClick="BT_JisyaTantousya_Add_Click" Height="30px" OnClientClick="PopupOpenButton()"/>
                                                                            </div>
                                                                            <div style="float: left; max-width: 280px; display: none; white-space: nowrap; overflow: hidden; text-overflow: ellipsis;padding-bottom:3px;" id="divTantousyaLabel" runat="server">
                                                                                <asp:Label ID="lblsJISHATANTOUSHA" runat="server" cssClass="JClinkBtPopup">先小岡太郎</asp:Label>
                                                                                <asp:Label ID="lblcJISHATANTOUSHA" runat="server" Visible="false">先小岡太郎</asp:Label>
                                                                                <asp:Button ID="BT_sJISHATANTOUSHA_Cross" runat="server" BackColor="White" Text="✕" Style="vertical-align: middle; margin-left: 10px; font-weight: bolder; border: none;" OnClick="BT_sJISHATANTOUSHA_Cross_Click" />
                                                                            </div>
                                                                        </ContentTemplate>
                                                                    </asp:UpdatePanel>
                                                                </td>
                                                                <td style="cursor:context-menu;">
                                                                    <%--<asp:Button ID="BT_sJISHATANTOUSHA_Cross" runat="server" BackColor="White"  Text="✕" style="font-weight:bolder; height:30px;border:none;" OnClick="BT_sJISHATANTOUSHA_Cross_Click" />--%>
                                                                </td>
                                                            </tr>

                                                            <tr style="height: 100px;">
                                                                <td style="cursor:context-menu;"></td>
                                                                <td style="width: 100px;cursor:context-menu;padding-top:5px;" valign="top">
                                                                    <label style="cursor:text;">備考</label>
                                                                </td>
                                                                <td style="cursor:context-menu;">
                                                                    <asp:TextBox runat="server" ID="TB_sBIKOU" TextMode="MultiLine" CssClass="form-control TextboxStyle JC09TextArea" Rows="5" AutoPostBack="True" OnTextChanged="TB_sBIKOU_TextChanged" MaxLength="255"></asp:TextBox>
                                                                </td>
                                                                <td style="cursor:context-menu;"></td>
                                                            </tr>


                                                        </table>
                                                    </div>
                                                    <div id="div_rightSyousai" style="height: 350px; float: right;cursor:context-menu;display:none;">
                                                        <table>
                                                            <tr style="height: 65px;">
                                                                <td style="cursor:context-menu;"></td>
                                                            </tr>
                                                            <tr style="height: 200px;">

                                                                <td style="vertical-align: top; width: 100px;cursor:context-menu;">
                                                                    <label style="cursor:text;">画像</label>
                                                                </td>
                                                                <td style="cursor:context-menu;">
                                                                    <%-- <asp:Image ID="Image1" runat="server" Width="250" Height="200" />
                                                                    <asp:Button runat="server" ID="Button1" Text="削除" CssClass="JC09GrayButton" style="vertical-align:bottom;"/>--%>
                                                                    <%--<asp:TextBox runat="server" ID="TB_sBIKOU1" TextMode="MultiLine" CssClass="form-control TextboxStyle JC09TextArea" Rows="7" AutoPostBack="True" OnTextChanged="TB_sBIKOU_TextChanged"></asp:TextBox>--%>

                                                                    <asp:UpdatePanel ID="UpdatePanelImage" runat="server" UpdateMode="Conditional">
                                                                        <ContentTemplate>
                                                                            <div id="divImage" runat="server" style="width: 250px; height: 200px; border: #9b9b9b solid 1px; border-radius: 3px; display: none; float: left;cursor:pointer;" onclick="$('#fileUpload2').click();return false;" ondragenter="OnDragEnter(event);" ondragover="OnDragOver(event);" ondrop="OnDrop(event,0);">
                                                                                <asp:Image ID="Image1" runat="server" CssClass="JC10DaiHyouImage" />
                                                                            </div>
                                                                            <div id="divBTImageUpload" runat="server" style="float: left; display: flex; justify-content: center; align-items: center; width: 250px; height: 200px; border: #9b9b9b solid 1px; border-radius: 7px;cursor:pointer;" onclick="$('#fileUpload2').click();return false;" align="center" ondragenter="OnDragEnter(event);" ondragover="OnDragOver(event);" ondrop="OnDrop(event,0);">
                                                                                <div style="width: 250px;">
                                                                                    画像をアップロード
                                                                        <br />
                                                                                    <img src="../Img/uploadImage.png" style="width: 25px; display: block;" />
                                                                                </div>
                                                                                <asp:TextBox ID="TB_cfile" runat="server" AutoCompleteType="Disabled" AutoPostBack="False" BorderStyle="None" ReadOnly="True" Visible="false"></asp:TextBox>
                                                                                <asp:TextBox ID="TB_sfile" runat="server" AutoCompleteType="Disabled" AutoPostBack="False" BorderStyle="None" ReadOnly="True" Visible="false"></asp:TextBox>
                                                                            </div>
                                                                            <asp:FileUpload ID="fileUpload2" runat="server" accept="image/*,application/pdf,application/postscript" CssClass="DisplayNone" ClientIDMode="Static" />
                                                                            <asp:Button runat="server" ID="BT_ImageUpload" Text="画像をアップロード" Style="border: none; background-color: white; margin-left: 20px; margin-top: 70px;" CssClass="DisplayNone" OnClick="BT_ImageUpload_Click" />
                                                                            <div id="divImageDelete">
                                                                                <asp:Button runat="server" ID="BT_ImageDelete" Text="削除" CssClass="JC09GrayButton" Style="vertical-align: bottom; margin: 10px; margin-top: 155px;" OnClick="BT_ImageDelete_Click" />
                                                                            </div>
                                                                            <asp:Button ID="BT_ImgaeDrop" runat="server" Text="&times;" CssClass="DisplayNone" OnClick="BT_ImgaeDrop_Click"/>
                                                                        </ContentTemplate>
                                                                        <Triggers>
                                                                            <asp:PostBackTrigger ControlID="BT_ImageUpload" />
                                                                            <asp:AsyncPostBackTrigger ControlID="BT_ImgaeDrop" EventName="Click"/>
                                                                        </Triggers>
                                                                    </asp:UpdatePanel>

                                                                </td>
                                                            </tr>

                                                            <tr>

                                                                <td class="auto-style3" style="cursor:context-menu;">
                                                                    <label style="cursor:text;">ファイルパス</label>
                                                                </td>
                                                                <td class="auto-style3" style="cursor:context-menu;">
                                                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                                                        <ContentTemplate>
                                                                            <asp:Button ID="btnFileSelect" runat="server" Text="選択" CssClass="JC10GrayButton" onmousedown="getTantouBoardScrollPosition();" OnClientClick="$('#fileUpload1').click();return false;" />
                                                                            <asp:FileUpload ID="fileUpload1" runat="server" accept="image/*" CssClass="DisplayNone" ClientIDMode="Static" />
                                                                            <asp:Button ID="btnFilePathCopy" runat="server" Text="パスをコピー" CssClass="JC10GrayButton" onmousedown="getTantouBoardScrollPosition();" Width="120px" OnClientClick="copyToClipBoard()" />
                                                                        </ContentTemplate>
                                                                    </asp:UpdatePanel>
                                                                </td>
                                                            </tr>
                                                            <tr style="height: 30px;">
                                                                <td style="cursor:context-menu;"></td>
                                                                <td style="vertical-align: top;" style="cursor:context-menu;">
                                                                    <%--<asp:Label runat="server" ID="filepath"></asp:Label>--%>
                                                                    <asp:TextBox ID="txtfilepath" runat="server" AutoCompleteType="Disabled" AutoPostBack="False" BorderStyle="None" ReadOnly="True" CssClass="JC09txtFilePath"></asp:TextBox>
                                                                </td>
                                                            </tr>

                                                        </table>
                                                    </div>

                                                </div>
                                            </td>
                                        </tr>
                                        <tr>

                                            <td style="cursor:context-menu;">
                                                <asp:Button runat="server" ID="BT_BukkenShosai" Text="物件詳細" Width="8%" CssClass="JC10MitumoriSyosaiButton" Style="margin-top: 0px; margin-left: 20px;" OnClick="BT_BukkenShosai_Click" />
                                                <div runat="server" id="divBukenShosai" style="min-width: 1000px; max-width: 1000px; height: 80px; background-color: rgb(250, 250, 250); margin-left: 20px;">
                                                    <table style="border: none;">
                                                        <tr>
                                                            <td  style="cursor:context-menu;">
                                                                <div style="margin-left: 0px; margin-top: 25px; float: left; width: 80px; position: relative; border: none; text-align: center;">
                                                                    <asp:Label runat="server" style="cursor:text;">自社番号</asp:Label>
                                                                </div>
                                                                <%-- 20220425 Updated Eaindray Start--%>
                                                                <div class="no-ime" style="float: left; margin-left: 0px; margin-top: 20px; width: 150px; position: relative; border: none; text-align: center;"> 
                                                                    <asp:TextBox ID="tb_jishabangou" AutoPostBack="true"  runat="server" CssClass="form-control TextboxStyle JC09TextBox1" onpaste="return false"
                                                                        onkeyup="DeSelectText(this);" onfocus="this.setSelectionRange(this.value.length, this.value.length);" oninput="validateJapanese(this);" MaxLength="10" AutoCompleteType="Disabled" OnTextChanged="tb_jishabangou_TextChanged"></asp:TextBox>
                                                                    <asp:FilteredTextBoxExtender ID="TBF_Jishabangou" runat="server" FilterType="UppercaseLetters, LowercaseLetters, Numbers" TargetControlID="tb_jishabangou" />
                                                                </div>
                                                                <%-- 20220425 Updated Eaindray End--%>
                                                                <div style="float: left; margin-left: 0px; margin-top: 25px; width: 70px; position: relative; border: none;">
                                                                    <asp:Label runat="server" style="cursor:text;">物件納期</asp:Label>
                                                                </div>
                                                                <div style="float: left; margin-left: 0px; margin-top: 20px; width: 150px; position: relative; border: none;">
                                                                    <%--<asp:TextBox ID="TextBox3" runat="server" CssClass="form-control TextboxStyle JC09TextBox1"></asp:TextBox>--%>

                                                                    <asp:UpdatePanel ID="updNoukiDate" runat="server" UpdateMode="Conditional">
                                                                        <ContentTemplate>
                                                                            <asp:Button ID="btnNoukiDate" runat="server" Text="日付を設定" CssClass="JC10GrayButton" Width="90px" OnClick="btnNoukiDate_Click" Height="30px" OnClientClick="PopupOpenButton()"/>
                                                                            <div id="divNoukiDate" class="DisplayNone" runat="server">
                                                                                <asp:Label ID="lbldNouki" runat="server" Text="" CssClass="GrayLabel"></asp:Label>
                                                                                <asp:Label ID="lblNoukiDateYear" runat="server" CssClass="DisplayNone"></asp:Label>
                                                                                <asp:Button ID="btndNoukiCross" CssClass="CrossBtnGray " runat="server" Text="✕" style="padding-top:2px;" OnClick="btndNoukiCross_Click" />
                                                                            </div>
                                                                        </ContentTemplate>
                                                                    </asp:UpdatePanel>
                                                                </div>
                                                                <div style="float: left; margin-left: 0px; margin-top: 25px; width: 80px; position: relative; border: none;">
                                                                    <asp:Label runat="server" style="cursor:text;">次回点検日</asp:Label>
                                                                </div>
                                                                <div style="float: left; margin-left: 0px; margin-top: 20px; width: 150px; position: relative; border: none;">
                                                                    <%--<asp:TextBox ID="TextBox1" runat="server" CssClass="form-control TextboxStyle JC09TextBox1"></asp:TextBox>--%>
                                                                    <asp:UpdatePanel ID="updJikaitenkenDate" runat="server" UpdateMode="Conditional">
                                                                        <ContentTemplate>
                                                                            <asp:Button ID="btnJikaitenkenDate" runat="server" Text="日付を設定" CssClass="JC10GrayButton" Width="90px" OnClick="btnJikaitenkenDate_Click" Height="30px" OnClientClick="PopupOpenButton()"/>
                                                                            <div id="divJikaitenkenDate" class="DisplayNone" runat="server">
                                                                                <asp:Label ID="lbldJikaitenken" runat="server" Text="" CssClass="GrayLabel"></asp:Label>
                                                                                <asp:Label ID="lblJikauitenkenDateYear" runat="server" CssClass="DisplayNone"></asp:Label>
                                                                                <asp:Button ID="btndJikaitenkenCross" CssClass="CrossBtnGray " runat="server" Text="✕" style="padding-top:2px;" OnClick="btndJikaitenkenCross_Click" />
                                                                            </div>
                                                                        </ContentTemplate>
                                                                    </asp:UpdatePanel>
                                                                </div>
                                                                <div style="float: left; margin-left: 0px; margin-top: 25px; width: 135px; position: relative; border: none;">
                                                                    <asp:Label runat="server" style="cursor:text;">次回屋外広告申請日</asp:Label>
                                                                </div>
                                                                <div style="float: left; margin-left: 0px; margin-top: 20px; width: 150px; position: relative; border: none;">
                                                                    <%--<asp:TextBox ID="TextBox2" runat="server" CssClass="form-control TextboxStyle JC09TextBox1"></asp:TextBox>--%>
                                                                    <asp:UpdatePanel ID="updJikaiokugaiDate" runat="server" UpdateMode="Conditional">
                                                                        <ContentTemplate>
                                                                            <asp:Button ID="btnJikaiokugaiDate" runat="server" Text="日付を設定" CssClass="JC10GrayButton" Width="90px" OnClick="btnJikaiokugaiDate_Click" Height="30px" OnClientClick="PopupOpenButton()"/>
                                                                            <div id="divJikaiokugaiDate" class="DisplayNone" runat="server">
                                                                                <asp:Label ID="lbldJikaiokugai" runat="server" Text="" CssClass="GrayLabel"></asp:Label>
                                                                                <asp:Label ID="lblJikauiokugaiDateYear" runat="server" CssClass="DisplayNone"></asp:Label>
                                                                                <asp:Button ID="btndJikaiokugaiCross" CssClass="CrossBtnGray " runat="server" Text="✕" style="padding-top:2px;" OnClick="btndJikaiokugaiCross_Click" />
                                                                            </div>
                                                                        </ContentTemplate>
                                                                    </asp:UpdatePanel>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                        </tr>
                                        <tr style="height: 35px">
                                            <td style="cursor:context-menu;">
                                                <table style="width:100% !important;table-layout:fixed;">
                                                    <tr>
                                                        <td>
                                                <div style="margin-left: 10px; float: left; width: 80px; position: relative; border: 1px solid white; text-align: center;">
                                                    <asp:Label runat="server" style="cursor:text;">作成日</asp:Label>
                                                </div>
                                                <div style="float: left; margin-left: 0px; width: 150px; position: relative; border: 1px solid white; text-align: center;">
                                                    <asp:Label ID="lblSakusekiBi" runat="server" Visible="true" style="cursor:text;"></asp:Label>
                                                </div>
                                                        </td>
                                                        <td>
                                                <div style="float: left; margin-left: 0px; width: 80px; position: relative; border: 1px solid white;">
                                                    <asp:Label runat="server" style="cursor:text;">作成者</asp:Label>
                                                </div>
                                                <div style="float: left; margin-left: 0px; width: 150px; position: relative; border: 1px solid white;">
                                                    <asp:Label ID="lblSakuseiSya" runat="server" Visible="true" style="cursor:text;"></asp:Label>
                                                    <asp:Label ID="lblcSakuseiSya" runat="server" Visible="false" style="cursor:text;"></asp:Label>
                                                </div>
                                                            </td>
                                                        <td>
                                                <div style="float: left; margin-left: 0px; width: 100px; position: relative; border: 1px solid white;">
                                                    <asp:Label runat="server" style="cursor:text;">最終更新日</asp:Label>
                                                </div>
                                                <div style="float: left; margin-left: 0px; width: 150px; position: relative; border: 1px solid white;">
                                                    <asp:Label ID="lblkoushinBi" runat="server" Visible="true" style="cursor:text;"></asp:Label>
                                                </div>
                                                            </td>
                                                        <td>
                                                <div style="float: left; margin-left: 0px; width: 90px; position: relative; border: 1px solid white;">
                                                    <asp:Label runat="server" style="cursor:text;">最終更新者</asp:Label>
                                                </div>
                                                <div style="float: left; margin-left: 0px; width: 150px; position: relative; border: 1px solid white;">
                                                    <asp:Label ID="lblkoushinSya" runat="server" Visible="true" style="cursor:text;"></asp:Label>
                                                    <asp:Label ID="lblckoushinSya" runat="server" Visible="false" style="cursor:text;"></asp:Label>
                                                </div>
                                                            </td>
                                                         </tr>
                                                    </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="cursor:context-menu;">
                                                <div class="JC10Borderline"></div>
                                            </td>
                                        </tr>
                                        <tr style="height: 30px">
                                            <td class="JC09TableWidth" style="cursor:context-menu;">
                                                <asp:Button runat="server" ID="BT_MitsumoriJotai" Text="見積状態ごと集計" CssClass="JC10MitumoriSyosaiButton" Width="12%" OnClick="BT_MitsumoriJotai_Click" Style="margin-left: 20px;" />
                                                <div style="display:inline-block;" class="lbl_mitsumoriJotai">
                                                <asp:Label ID="Label19" runat="server" style="margin-right:50px;cursor:text;">合計</asp:Label>
                                                <asp:Label ID="lblkingaku" runat="server" style="margin-right:50px;cursor:text;">0</asp:Label>
                                                <asp:Label ID="lblarari" runat="server" style="margin-right:40px;cursor:text;">0</asp:Label>
                                                <asp:Label ID="Label22" runat="server" style="margin-right:10px;cursor:text;">見積</asp:Label>
                                                <asp:Label ID="lblmitsumori" runat="server" style="margin-right:30px;cursor:text;">0件</asp:Label>
                                                 </div>

                                                <div runat="server" id="divMitsumoriJotai" style="min-width: 1000px; max-width: 1000px; height: 100px; background-color: rgb(250, 250, 250); margin-left: 20px; margin-bottom: 10px;">
                                                    <table style="border: none; min-width: 1000px; max-width: 1000px;cursor:context-menu;">
                                                        <tr>
                                                            <td style="cursor:context-menu;">
                                                                <div style="margin-left: 0px; margin-top: 20px; float: left; width: 100px; position: relative; border: none; text-align: right;">
                                                                    <asp:Label ID="Label1" runat="server" Font-Bold="true" style="cursor:text;">作成中</asp:Label>
                                                                </div>
                                                                <div style="margin-left: 0px; margin-top: 20px; float: left; width: 150px; position: relative; border: none; text-align: right;">
                                                                    <asp:Label ID="Label2" runat="server" Font-Bold="true" style="cursor:text;">提出済</asp:Label>
                                                                </div>
                                                                <div style="margin-left: 0px; margin-top: 20px; float: left; width: 150px; position: relative; border: none; text-align: right;">
                                                                    <asp:Label ID="Label3" runat="server" Font-Bold="true" style="cursor:text;">受注</asp:Label>
                                                                </div>
                                                                <div style="border-style: none; border-color: inherit; border-width: medium; margin-left: 0px; margin-top: 20px; text-align: right;" class="auto-style4">
                                                                    <asp:Label ID="Label4" runat="server" Font-Bold="true" style="cursor:text;">売上済</asp:Label>
                                                                </div>
                                                                <div style="margin-left: 0px; margin-top: 20px; float: left; width: 150px; position: relative; border: none; text-align: right;">
                                                                    <asp:Label ID="Label5" runat="server" Font-Bold="true" style="cursor:text;">失注</asp:Label>
                                                                </div>
                                                                <div style="margin-left: 0px; margin-top: 20px; float: left; width: 150px; position: relative; border: none; text-align: right;">
                                                                    <asp:Label ID="Label6" runat="server" Font-Bold="true" style="cursor:text;">無効</asp:Label>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="cursor:context-menu;">
                                                                <div style="margin-left: 0px; margin-top: 5px; float: left; width: 100px; position: relative; border: none; text-align: right;">
                                                                    <asp:Label ID="lblnsakuseichukingaku" runat="server" style="cursor:text;">0</asp:Label>
                                                                </div>
                                                                <div style="margin-left: 0px; margin-top: 5px; float: left; width: 150px; position: relative; border: none; text-align: right;">
                                                                    <asp:Label ID="lblnteishutsukingaku" runat="server" style="cursor:text;">0</asp:Label>
                                                                </div>
                                                                <div style="margin-left: 0px; margin-top: 5px; float: left; width: 150px; position: relative; border: none; text-align: right;">
                                                                    <asp:Label ID="lblnjuchukingaku" runat="server" style="cursor:text;">0</asp:Label>
                                                                </div>
                                                                <div style="margin-left: 0px; margin-top: 5px; float: left; width: 150px; position: relative; border: none; text-align: right;">
                                                                    <asp:Label ID="lblnuriagezumikingaku" runat="server" style="cursor:text;">0</asp:Label>
                                                                </div>
                                                                <div style="margin-left: 0px; margin-top: 5px; float: left; width: 150px; position: relative; border: none; text-align: right;">
                                                                    <asp:Label ID="lblnshitsuchukingaku" runat="server" style="cursor:text;">0</asp:Label>
                                                                </div>
                                                                <div style="margin-left: 0px; margin-top: 5px; float: left; width: 150px; position: relative; border: none; text-align: right;">
                                                                    <asp:Label ID="lblnmukoukingaku" runat="server" style="cursor:text;">0</asp:Label>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td style="cursor:context-menu;">
                                                                <div style="margin-left: 0px; margin-top: 5px; float: left; width: 100px; position: relative; border: none; text-align: right;">
                                                                    <asp:Label ID="lblnsakuseichu" runat="server" style="cursor:text;">0件</asp:Label>
                                                                </div>
                                                                <div style="margin-left: 0px; margin-top: 5px; float: left; width: 150px; position: relative; border: none; text-align: right;">
                                                                    <asp:Label ID="lblnteishutsu" runat="server" style="cursor:text;">0件</asp:Label>
                                                                </div>
                                                                <div style="margin-left: 0px; margin-top: 5px; float: left; width: 150px; position: relative; border: none; text-align: right;">
                                                                    <asp:Label ID="lblnjuchu" runat="server" style="cursor:text;">0件</asp:Label>
                                                                </div>
                                                                <div style="margin-left: 0px; margin-top: 5px; float: left; width: 150px; position: relative; border: none; text-align: right;">
                                                                    <asp:Label ID="lblnuriagezumi" runat="server" style="cursor:text;">0件</asp:Label>
                                                                </div>
                                                                <div style="margin-left: 0px; margin-top: 5px; float: left; width: 150px; position: relative; border: none; text-align: right;">
                                                                    <asp:Label ID="lblnshitsuchu" runat="server" style="cursor:text;">0件</asp:Label>
                                                                </div>
                                                                <div style="margin-left: 0px; margin-top: 5px; float: left; width: 150px; position: relative; border: none; text-align: right;">
                                                                    <asp:Label ID="lblnmukou" runat="server" style="cursor:text;">0件</asp:Label>
                                                                </div>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </div>
                                                </td>
                                            </tr>
                                        
                    <tr>
                        <td style="height:auto !important;cursor:context-menu;table-layout:auto;padding-left:20px;padding-right:20px;" class="JC09TableWidth">
                            <div style="overflow-x:auto;width:100% !important;">
                            <div id="DIV_GV" runat="server" style="width:auto;display:inline-block;">
                                <asp:UpdatePanel ID="updMitsumoriSyosaiGrid" runat="server" UpdateMode="Conditional">
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="GV_MitumoriSyousai" EventName="selectedIndexChanged" />
                                    </Triggers>
                                    <ContentTemplate>
                                        <asp:GridView ID="GV_MitumoriSyousai" runat="server" cssClass="RowHover GridViewStyle grip" AutoGenerateColumns="false" BorderColor="Transparent" CellPadding="0"
                                            EmptyDataRowStyle-CssClass="JC10NoDataMessageStyle" ShowHeader="true" ShowHeaderWhenEmpty="true" OnRowDataBound="GV_MitumoriSyousai_RowDataBound" OnPreRender="GV_MitumoriSyousai_PreRender">
                                            <EmptyDataRowStyle CssClass="JC10NoDataMessageStyle" />
                                            <HeaderStyle BackColor="#F2F2F2" Height="40px" HorizontalAlign="Left" BorderColor="White" BorderStyle="Solid" BorderWidth="2px" />
                                            <RowStyle CssClass="JC12GridItem" Height="43px" />
                                            <SelectedRowStyle/>
                                            <Columns>
                                                <asp:TemplateField>
                                                    <HeaderTemplate>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <div style="padding-top: 0px;vertical-align: middle; text-align: center;">
                                                            <asp:UpdatePanel ID="updMitsumoriSyosaiGrid" runat="server" UpdateMode="Conditional">
                                                            <Triggers>
                                                                <asp:PostBackTrigger ControlID="BT_P" />
                                                            </Triggers>
                                                                <ContentTemplate>
                                                            <asp:Button ID="BT_P" runat="server" CssClass="JC09GridGrayBtn" Height="25px" Text="書" Width="30px" OnClick="BT_P_Click"/>
                                                                </ContentTemplate>
                                                                </asp:UpdatePanel>
                                                        </div>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="JC10ButtonCol" />
                                                    <HeaderStyle BorderWidth="2px" Wrap="False"  CssClass="JC10ButtonCol"/>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderStyle-Width="33px">
                                                    <HeaderTemplate>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <div style=" text-align: center; padding-top: 0px;vertical-align: middle;">
                                                            <asp:Button ID="BT_KO" runat="server" CssClass="JC09GridGrayBtn" Height="25px" OnClick="BT_KO_Click" Text="コ" Width="30px" />
                                                        </div>
                                                    </ItemTemplate>
                                                    <ItemStyle CssClass="JC10ButtonCol" />
                                                     <HeaderStyle BorderWidth="2px" Wrap="False" CssClass="JC10ButtonCol"/>
                                                </asp:TemplateField>
                                                <asp:TemplateField Visible="false">
                                                    <HeaderTemplate>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <div class="grip" style="padding-top: 0px; text-align: center; vertical-align: middle;">
                                                            <asp:Button ID="BT_Red" runat="server" CssClass="JC09RedButton" ForeColor="White" Height="25px" Text="削" Width="30px" />
                                                        </div>
                                                    </ItemTemplate>
                                                     <HeaderStyle BorderWidth="2px" Wrap="False"/>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Left" HeaderStyle-Width="95px">
                                                    <HeaderTemplate>
                                                        <div class="grip" style="min-width:90px;text-align: left; padding-right: 4px;padding-left:2px;overflow: hidden;  display: -webkit-box;  -webkit-line-clamp: 1; -webkit-box-orient: vertical;word-break: break-all;">
                                                        <asp:Label ID="cMITUMORIHeader" runat="server" Style="font-size: 13px; font-weight: bold;" Text="見積コード"></asp:Label>
                                                            </div>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <div class="grip" style="min-width:90px;text-align: left; padding-right: 4px;padding-left:2px;overflow: hidden;  display: -webkit-box;  -webkit-line-clamp: 1; -webkit-box-orient: vertical;word-break: break-all;">
                                                            <asp:LinkButton ID="cMITUMORI" runat="server" Font-Underline="false" OnClick="LK_cMitumori_Click" Style="font-size: 13px; font-weight: normal;" Text=' <%# Bind("cMITUMORI","{0}") %>' ToolTip=' <%# Bind("cMITUMORI","{0}") %>'></asp:LinkButton>
                                                        </div>
                                                    </ItemTemplate>
                                                     <HeaderStyle BorderWidth="2px" Wrap="False"/>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Left">
                                                    <HeaderTemplate>
                                                        <div class="grip" style="min-width:50px;text-align: left;padding-right: 4px;padding-left:2px;overflow: hidden;  display: -webkit-box;  -webkit-line-clamp: 1; -webkit-box-orient: vertical;word-break: break-all;">
                                                        <asp:Label ID="cMITUMORIHeader" runat="server" Style="font-size: 13px; font-weight: bold;" Text="見積名"></asp:Label>
                                                            </div>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <div class="grip" style="min-width:50px;text-align: left;padding-right: 4px;padding-left:2px;overflow: hidden;  display: -webkit-box;  -webkit-line-clamp: 1; -webkit-box-orient: vertical;word-break: break-all;">
                                                            <asp:Label ID="sMITUMORI" runat="server" Style="font-size: 13px; font-weight: normal;" Text='<%# Bind("sMITUMORI","{0}") %>' ToolTip='<%# Bind("sMITUMORI","{0}") %>'></asp:Label>
                                                        </div>
                                                    </ItemTemplate>
                                                     <HeaderStyle BorderWidth="2px" Wrap="False"/>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Left">
                                                    <HeaderTemplate>
                                                        <div class="grip" style="min-width:55px;text-align: left;padding-right: 4px;padding-left:2px;overflow: hidden;  display: -webkit-box;  -webkit-line-clamp: 1; -webkit-box-orient: vertical;word-break: break-all;">
                                                        <asp:Label ID="cMITUMORIHeader" runat="server" Style="font-size: 13px; font-weight: bold;" Text="営業担当"></asp:Label>
                                                            </div>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <div class="grip" style="min-width:55px;text-align: left;padding-right: 4px;padding-left:2px;overflow: hidden;  display: -webkit-box;  -webkit-line-clamp: 1; -webkit-box-orient: vertical;word-break: break-all;">
                                                            <asp:Label ID="sE_TANTOUSHA" runat="server" Style="font-size: 13px; font-weight: normal;" Text='<%# Bind("sE_TANTOUSHA","{0}") %>' ToolTip='<%# Bind("sE_TANTOUSHA","{0}") %>'></asp:Label>
                                                        </div>
                                                    </ItemTemplate>
                                                     <HeaderStyle BorderWidth="2px" Wrap="False"/>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Left">
                                                    <HeaderTemplate>
                                                        <div class="grip" style="min-width:40px;text-align: left;padding-right: 4px;padding-left:2px;overflow: hidden;  display: -webkit-box;  -webkit-line-clamp: 1; -webkit-box-orient: vertical;word-break: break-all;">
                                                        <asp:Label ID="cMITUMORIHeader" runat="server" Style="font-size: 13px; font-weight: bold;" Text="作成者"></asp:Label>
                                                            </div>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <div class="grip" style="min-width:40px;text-align: left;padding-right: 4px;padding-left:2px;overflow: hidden;  display: -webkit-box;  -webkit-line-clamp: 1; -webkit-box-orient: vertical;word-break: break-all;">
                                                            <asp:Label ID="SakuseiSHA" runat="server" Style="font-size: 13px; font-weight: normal;" Text=' <%# Bind("SakuseiSHA","{0}") %>' ToolTip=' <%# Bind("SakuseiSHA","{0}") %>'></asp:Label>
                                                        </div>
                                                    </ItemTemplate>
                                                     <HeaderStyle BorderWidth="2px" Wrap="False"/>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Left">
                                                    <HeaderTemplate>
                                                        <div class="grip" style="min-width:40px;text-align: left;padding-right: 4px;padding-left:2px;overflow: hidden;  display: -webkit-box;  -webkit-line-clamp: 1; -webkit-box-orient: vertical;word-break: break-all;">
                                                        <asp:Label ID="cMITUMORIHeader" runat="server" Style="font-size: 13px; font-weight: bold;" Text="見積日"></asp:Label>
                                                            </div>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <div class="grip" style="min-width:40px;text-align: left;padding-right: 4px;padding-left:2px;overflow: hidden;  display: -webkit-box;  -webkit-line-clamp: 1; -webkit-box-orient: vertical;word-break: break-all;">
                                                            <asp:Label ID="dMITUMORISAKUSEI" runat="server" Style="font-size: 13px; font-weight: normal;" Text=' <%# Bind("dMITUMORISAKUSEI","{0}") %>' ToolTip=' <%# Bind("dMITUMORISAKUSEI","{0}") %>'></asp:Label>
                                                        </div>
                                                    </ItemTemplate>
                                                     <HeaderStyle BorderWidth="2px" Wrap="False"/>
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <HeaderTemplate>
                                                        <div class="grip" style="min-width:60px;text-align: right;padding-right: 4px;padding-left:2px;overflow: hidden;  display: -webkit-box;  -webkit-line-clamp: 1; -webkit-box-orient: vertical;word-break: break-all;">
                                                            <asp:Label ID="cMITUMORIHeader" runat="server" Style="font-size: 13px; font-weight: bold; text-align: right;" Text="合計金額"></asp:Label>
                                                        </div>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <div class="grip" style="min-width:60px;text-align: right;padding-right: 4px;padding-left:2px;overflow: hidden;  display: -webkit-box;  -webkit-line-clamp: 1; -webkit-box-orient: vertical;word-break: break-all;">
                                                            <asp:Label ID="nKINGAKU" runat="server" Style="font-size: 13px; font-weight: normal;" Text=' <%# Bind("nKINGAKU","{0}") %>' ToolTip=' <%# Bind("nKINGAKU","{0}") %>'></asp:Label>
                                                        </div>
                                                    </ItemTemplate>
                                                     <HeaderStyle BorderWidth="2px" Wrap="False"/>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Left">
                                                    <HeaderTemplate>
                                                        <div class="grip" style="min-width:55px;text-align: left;padding-right: 4px;padding-left:2px;overflow: hidden;  display: -webkit-box;  -webkit-line-clamp: 1; -webkit-box-orient: vertical;word-break: break-all;">
                                                        <asp:Label ID="cMITUMORIHeader" runat="server" Style="font-size: 13px; font-weight: bold;" Text="見積状態"></asp:Label>
                                                            </div>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <div class="grip" style="min-width:55px;text-align: left;padding-right: 4px;padding-left:2px;overflow: hidden;  display: -webkit-box;  -webkit-line-clamp: 1; -webkit-box-orient: vertical;word-break: break-all;">
                                                            <asp:Label ID="sJYOTAI" runat="server" Style="font-size: 13px; font-weight: normal;" Text=' <%# Bind("sJYOTAI","{0}") %>' ToolTip=' <%# Bind("sJYOTAI","{0}") %>'></asp:Label>
                                                        </div>
                                                    </ItemTemplate>
                                                     <HeaderStyle BorderWidth="2px" Wrap="False"/>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderStyle-HorizontalAlign="Left">
                                                    <HeaderTemplate>
                                                        <div class="grip" style="min-width:60px;text-align: right;padding-right: 4px;padding-left:2px;overflow: hidden;  display: -webkit-box;  -webkit-line-clamp: 1; -webkit-box-orient: vertical;word-break: break-all;">
                                                            <asp:Label ID="cMITUMORIHeader" runat="server" Style="font-size: 13px; font-weight: bold;" Text="金額粗利"></asp:Label>
                                                        </div>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <div class="grip" style="min-width:60px;text-align: right;padding-right: 4px;padding-left:2px;overflow: hidden;  display: -webkit-box;  -webkit-line-clamp: 1; -webkit-box-orient: vertical;word-break: break-all;">
                                                            <asp:Label ID="nMITUMORIARARI" runat="server" Style="font-size: 14px; font-weight: normal;" Text='<%# Bind("nMITUMORIARARI","{0}") %>' ToolTip='<%# Bind("nMITUMORIARARI","{0}") %>'></asp:Label>
                                                        </div>
                                                    </ItemTemplate>
                                                     <HeaderStyle BorderWidth="2px" Wrap="False"/>
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                            <ItemTemplate>

                                                <div style="display:flex;align-content:center;align-items:center;align-items: center;justify-content: center;z-index:2;">
                                                    <asp:HoverMenuExtender ID="HoverMenuExtender2" runat="server" PopupControlID="PopupMenu"
                                                    TargetControlID="PopupMenuBtn" PopupPosition="left">
                                                </asp:HoverMenuExtender>
                                                <asp:Panel ID="PopupMenu" runat="server" CssClass="dropdown-menu fontcss " aria-labelledby="dropdownMenuButton" Style="display: none; min-width: 1rem; width: 5rem; ">
                                                    <asp:LinkButton ID="lnkbtnMitumoriDelete" class="dropdown-item" runat="server" Text='削除' style="margin-right:10px;font-size:13px;" OnClick="lnkbtnMitumoriDelete_Click"></asp:LinkButton>
                                                </asp:Panel>
                                                <asp:Panel ID="PopupMenuBtn" runat="server" CssClass="modalPopup" Style="width: 20px;">
                                                    <button class="btn dropdown-toggle" type="button" id="dropdownMenuButton" data-toggle="dropdown" 
                                                      aria-haspopup="true" aria-expanded="false" style="border:1px solid gainsboro;width:20px; height:20px;padding:0px 3px 0px 1px;margin:0;margin-top:-3px;">
                                                </asp:Panel>
                                            </div>

                                            </ItemTemplate>
                                                     <HeaderStyle BorderWidth="2px" Wrap="False" CssClass="JC09DropDown JC10DropCol"/>
                                            <ItemStyle Width="25px" CssClass="JC09DropDown JC10DropCol"/>
                                        </asp:TemplateField>  

                                            </Columns>
                                            <PagerStyle Height="40px" />
                                            <%--<RowStyle Height="40px" />--%>
                                        </asp:GridView>
                                    </ContentTemplate>

                                </asp:UpdatePanel>
                            </div>
                                </div>
                        </td>
                    </tr>
                    <tr>
                        <td style="padding: 20px;padding-top:10px;cursor:context-menu;">
                            <asp:Button ID="BT_MitumoriAdd" runat="server" cssClass="BlueBackgroundButton JC10SaveBtn" OnClick="BT_MitumoriAdd_Click" Text="✛ 見積を追加"  style="margin-right:0px !important;" />
                            <asp:Button ID="BT_MitumoriCopyAdd" runat="server" cssClass="BlueBackgroundButton JC10SaveBtn" OnClick="BT_MitumoriCopyAdd_Click" Text="✛ 他見積をコピーして追加"  OnClientClick="PopupOpenButton()"/>
                            <asp:Label ID="lblMitumoriNai" runat="server" Style="font-size: 14px; font-weight: normal; margin-left: 100px;cursor:text;" Text="見積がありません"></asp:Label>
                        </td>
                    </tr>
                                        </table>
                                    </div>
                                            
                            </asp:Panel>
                        </td>
                    </tr>
                </table>
               </div>

                <asp:DropDownList ID="DDL_Logo" runat="server" Width="200px" AutoPostBack="True" Height="30px" CssClass="form-control JC10GridTextBox" style="font-size:13px;" Visible="False">
                                                </asp:DropDownList>
                <asp:Button ID="btnYes" runat="server" Text="はい" class="BlueBackgroundButton DisplayNone" OnClick="btnYes_Click" Width="100px" Height="36px" />
                <asp:Button ID="btnNo" runat="server" Text="いいえ" class="BlueBackgroundButton DisplayNone" OnClick="btnNo_Click" Width="100px" Height="36px" />
                <asp:Button ID="btnCancel" runat="server" Text="キャンセル" class="JC09GrayButton DisplayNone" OnClick="btnCancel_Click" Width="100px" Height="36px" />
                 <asp:Button ID="btnmojiOK" runat="server" Text="はい" class="BlueBackgroundButton DisplayNone" Width="100px" Height="36px"/>

                <asp:Button ID="btnDeleteBukken" runat="server" Text="" class="JC09GrayButton DisplayNone" Width="100px" Height="36px" OnClick="btnDeleteBukken_Click" />
                <asp:Button ID="btnDeleteMitumori" runat="server" Text="" class="JC09GrayButton DisplayNone" Width="100px" Height="36px" OnClick="btnDeleteMitumori_Click" />
                 <asp:Button ID="BT_ColumnWidth" runat="server" Text="Button" OnClick="BT_ColumnWidth_Click" style="display:none;" />

                <asp:HiddenField ID="hiddenFieldId" runat="server" />
                <asp:HiddenField ID="HF_isChange" runat="server" />
                <asp:HiddenField ID="HF_flagBtn" runat="server" />
                <asp:HiddenField ID="HF_checkData" runat="server" />
                  <asp:HiddenField ID="HF_cMitumori" runat="server" />
                <asp:HiddenField ID="HF_sMitumori" runat="server" />
                <asp:HiddenField ID="HF_ImgBase64" runat="server" />
                <asp:HiddenField ID="HF_FileName" runat="server" />
                <asp:HiddenField ID="HF_ImgSize" runat="server" />
                 <asp:HiddenField ID="HF_GridSize" runat="server" />
                   <asp:HiddenField ID="HF_Grid" runat="server" />

                 <asp:Button ID="btnYesJisha" runat="server" Text="" class="JC09GrayButton DisplayNone" Width="100px" Height="36px" OnClick="btnYesJisha_Click"/><%--20220517 Chaw Added--%>

                <asp:UpdatePanel runat="server" ID="updLabelSave" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div class="success JCSuccess" id="divLabelSave" runat="server">
                            <asp:Label ID="LB_Save" Text="保存しました。" runat="server" ForeColor="White" Font-Size="13px"></asp:Label>
                            <asp:Button ID="BT_LBSaveCross" Text="✕" runat="server" Style="background-color: white; border-style: none; right: 10px; position: absolute;" OnClick="BT_LBSaveCross_Click" />
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>


        <asp:UpdatePanel ID="updShinkiPopup" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:Button ID="btnShinkiPopup" runat="server" Text="Button" Style="display: none" />
                <asp:ModalPopupExtender ID="mpeShinkiPopup" runat="server" TargetControlID="btnShinkiPopup"
                    PopupControlID="pnlShinkiPopupScroll" BackgroundCssClass="PopupModalBackground" BehaviorID="pnlShinkiPopupScroll"
                    RepositionMode="RepositionOnWindowResize">
                </asp:ModalPopupExtender>
                <asp:Panel ID="pnlShinkiPopupScroll" runat="server" Style="display: none; height: 100%; overflow: hidden;" CssClass="PopupScrollDiv">
                    <asp:Panel ID="pnlShinkiPopup" runat="server">
                        <iframe id="ifShinkiPopup" runat="server" scrolling="yes" style="height: 100vh; width: 100vw;"></iframe>
                        <asp:Button ID="btn_CloseMitumoriSearch" runat="server" Text="Button" CssClass="DisplayNone" OnClick="btn_CloseMitumoriSearch_Click" onClientClick="MasterOverflow()"/>
                        <asp:Button ID="btn_CloseTokuisakiSentaku" runat="server" Text="Button" CssClass="DisplayNone" OnClick="btn_CloseTokuisakiSentaku_Click" onClientClick="MasterOverflow()"/>
                    </asp:Panel>
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>


        <!--ポップアップ画面-->
        <asp:UpdatePanel ID="updSentakuPopup" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:Button ID="btnSentakuPopup" runat="server" Text="Button" CssClass="DisplayNone" />
                <asp:ModalPopupExtender ID="mpeSentakuPopup" runat="server" TargetControlID="btnSentakuPopup"
                    PopupControlID="pnlSentakuPopupScroll" BackgroundCssClass="PopupModalBackground" BehaviorID="pnlSentakuPopupScroll">
                </asp:ModalPopupExtender>
                <asp:Panel ID="pnlSentakuPopupScroll" runat="server" Style="display: none; overflow-x: hidden;overflow-y:hidden;" CssClass="PopupScrollDiv">
                    <asp:Panel ID="pnlSentakuPopup" runat="server">
                        <iframe id="ifSentakuPopup" runat="server" scrolling="no" class="NyuryokuIframe" style="border-radius: 0px;"></iframe>
                        <asp:Button ID="btnClose" runat="server" Text="Button" Style="display: none" OnClick="btnClose_Click" onClientClick="MasterOverflow()"/>
                        <asp:Button ID="btnTokuisakiTantouSelect" runat="server" Text="Button" Style="display: none" OnClick="btnTokuisakiTantouSelect_Click" onClientClick="MasterOverflow()"/>
                        <asp:Button ID="btnJishaTantouSelect" runat="server" Text="Button" Style="display: none" OnClick="btnJishaTantouSelect_Click" onClientClick="MasterOverflow()"/>
                        <asp:Button ID="btnTokuisakiSelect" runat="server" Text="Button" Style="display: none" OnClick="btnTokuisakiSelect_Click" onClientClick="MasterOverflow()"/>
                        <asp:Button ID="btnJoutaiSelect" runat="server" Text="Button" Style="display: none" OnClick="btnJoutaiSelect_Click" onClientClick="MasterOverflow()"/>
                        <asp:Button ID="btnPDFPageChoiceClose" runat="server" Text="Button" Style="display: none" OnClick="btnPDFPageChoiceClose_Click" onClientClick="MasterOverflow()"/>
                        <asp:Button ID="btnToLogin" runat="server" Text="Button" Style="display: none" OnClick="btnToLogin_Click" onClientClick="MasterOverflow()"/>
                    </asp:Panel>
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>

        <!--ポップアップ画面-->
        <asp:UpdatePanel ID="upddatePopup" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:Button ID="btndatePopup" runat="server" Text="Button" CssClass="DisplayNone" />
                <asp:ModalPopupExtender ID="mpedatePopup" runat="server" TargetControlID="btndatePopup"
                    PopupControlID="pnldatePopupScroll" BackgroundCssClass="PopupModalBackground" BehaviorID="pnldatePopupScroll"
                    RepositionMode="RepositionOnWindowResize">
                </asp:ModalPopupExtender>
                <asp:Panel ID="pnldatePopupScroll" runat="server" Style="display: none;" CssClass="PopupScrollDiv">
                    <asp:Panel ID="pnldatePopup" runat="server">
                        <iframe id="ifdatePopup" runat="server" class="NyuryokuIframe RadiusIframe" scrolling="no" style="max-width: 260px; min-width: 260px; max-height: 365px; min-height: 365px;"></iframe>
                        <asp:Button ID="btnCalendarClose" runat="server" Text="Button" CssClass="DisplayNone" OnClick="btnCalendarClose_Click" onClientClick="MasterOverflow()"/>
                        <asp:Button ID="btnCalendarSettei" runat="server" Text="Button" CssClass="DisplayNone" OnClick="btnCalendarSettei_Click" onClientClick="MasterOverflow()"/>
                        <asp:Button ID="btnTokuisakiClose" runat="server" Text="Button" Style="display: none" onClientClick="MasterOverflow()"/>
                    </asp:Panel>
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>

<script type="text/javascript">
     $(function (){

         $(".GridViewStyle").colResizable({
             liveDrag: true,
             resizeMode: 'overflow',
             postbackSafe: true,
             partialRefresh: true,
             flush: true,
             disabledColumns:[0,1,10],
             gripInnerHtml: "<div class='grip'></div>",
             draggingClass: "dragging",
             onResize: onSampleResized
         });

          function getShiftJISByteLength(s)
        {
            return s.replace(/[^\x00-\x80&#63728;｡｢｣､･ｦｧｨｩｪｫｬｭｮｯｰｱｲｳｴｵｶｷｸｹｺｻｼｽｾｿﾀﾁﾂﾃﾄﾅﾆﾇﾈﾉﾊﾋﾌﾍﾎﾏﾐﾑﾒﾓﾔﾕﾖﾗﾘﾙﾚﾛﾜﾝ ﾞ ﾟ&#63729;&#63730;&#63731;]/g, 'xx').length;
        }

            $('#<%= TB_sBUKKEN . ClientID%>').on('keyup keydown', function (e) {
            var text = $(this).val();
            var byteAmount = getShiftJISByteLength(text);
            if (e.key != "Backspace" && e.key != "Delete") {
                while (byteAmount > 80) {
                    text = text.substring(0, text.length - 1);
                    byteAmount = getShiftJISByteLength(text);
                    document.getElementById("<%= TB_sBUKKEN . ClientID%>").value = text;
               }
            }
        });

            $('#<%= TB_sBUKKEN . ClientID%>').on('change', function (e) {
          var text = $(this).val();
            var byteAmount = getShiftJISByteLength(text);
                while (byteAmount > 80) {
                    text = text.substring(0, text.length - 1);
                    byteAmount = getShiftJISByteLength(text);
                    document.getElementById("<%= TB_sBUKKEN . ClientID%>").value = text;
               }
         });

            $('#<%= TB_sBIKOU . ClientID%>').on('keyup keydown', function (e) {
                var text = $(this).val();
                var byteAmount = text.length;
            if (e.key != "Backspace" && e.key != "Delete") {
                while (byteAmount > 255) {
                    text = text.substring(0, text.length - 1);
                    byteAmount = text.length;
                    document.getElementById("<%= TB_sBIKOU . ClientID%>").value = text;
               }
            }
        });

         $('#<%= TB_sBIKOU . ClientID%>').on('change', function (e) {
             var text = $(this).val();
             var byteAmount = text.length;
             while (byteAmount > 255) {
                 text = text.substring(0, text.length - 1);
                 byteAmount = text.length;
                 document.getElementById("<%= TB_sBIKOU . ClientID%>").value = text;
             }
         });

         });


        var onSampleResized = function (e)
        {
             var columns = $(e.currentTarget).find("th");
             var msg = "";
            columns.each(function ()
            {
                msg += $(this).width() + ",";
            })
            document.getElementById("<%=HF_Grid.ClientID%>").value = $(e.currentTarget).width();
            document.getElementById("<%=HF_GridSize.ClientID%>").value = msg;
            document.getElementById("<%=BT_ColumnWidth.ClientID%>").click();
        }; 

    var prm = Sys.WebForms.PageRequestManager.getInstance();
            if (prm != null) {
                prm.add_endRequest(function (sender, e) {
                    if (sender._postBackSettings.panelsToUpdate != null)
                    {
                        $(".GridViewStyle").colResizable({
                            liveDrag: true,
                            resizeMode: 'overflow',
                            postbackSafe: true,
                            partialRefresh: true,
                            flush: true,
                            gripInnerHtml: "<div class='grip'></div>",
                            draggingClass: "dragging",
                            disabledColumns:[0,1,10],
                            onResize: onSampleResized
                        });

                         function getShiftJISByteLength(s)
        {
            return s.replace(/[^\x00-\x80&#63728;｡｢｣､･ｦｧｨｩｪｫｬｭｮｯｰｱｲｳｴｵｶｷｸｹｺｻｼｽｾｿﾀﾁﾂﾃﾄﾅﾆﾇﾈﾉﾊﾋﾌﾍﾎﾏﾐﾑﾒﾓﾔﾕﾖﾗﾘﾙﾚﾛﾜﾝ ﾞ ﾟ&#63729;&#63730;&#63731;]/g, 'xx').length;
        }

            $('#<%= TB_sBUKKEN . ClientID%>').on('keyup keydown', function (e) {
            var text = $(this).val();
            var byteAmount = getShiftJISByteLength(text);
            if (e.key != "Backspace" && e.key != "Delete") {
                while (byteAmount > 80) {
                    text = text.substring(0, text.length - 1);
                    byteAmount = getShiftJISByteLength(text);
                    document.getElementById("<%= TB_sBUKKEN . ClientID%>").value = text;
               }
            }
        });

            $('#<%= TB_sBUKKEN . ClientID%>').on('change', function (e) {
          var text = $(this).val();
            var byteAmount = getShiftJISByteLength(text);
                while (byteAmount > 80) {
                    text = text.substring(0, text.length - 1);
                    byteAmount = getShiftJISByteLength(text);
                    document.getElementById("<%= TB_sBUKKEN . ClientID%>").value = text;
               }
            });
            $('#<%= TB_sBIKOU . ClientID%>').on('keyup keydown', function (e) {
                var text = $(this).val();
                var byteAmount = text.length;
            if (e.key != "Backspace" && e.key != "Delete") {
                while (byteAmount > 255) {
                    text = text.substring(0, text.length - 1);
                    byteAmount = text.length;
                    document.getElementById("<%= TB_sBIKOU . ClientID%>").value = text;
               }
            }
        });

                        $('#<%= TB_sBIKOU . ClientID%>').on('change', function (e) {
                            var text = $(this).val();
                            var byteAmount = text.length;
                            while (byteAmount > 255) {
                                text = text.substring(0, text.length - 1);
                                byteAmount = text.length;
                                document.getElementById("<%= TB_sBIKOU . ClientID%>").value = text;
                            }
                        });

                    };
                });
                }
</script>
                
            </ContentTemplate>
        </asp:UpdatePanel>
        <%--        </form>--%>
    </body>

    </html>
</asp:Content>

