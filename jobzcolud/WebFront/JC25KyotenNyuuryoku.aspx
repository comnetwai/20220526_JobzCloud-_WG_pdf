<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="JC25KyotenNyuuryoku.aspx.cs" Inherits="jobzcolud.WebFront.JC25KyotenNyuuryoku" ValidateRequest="false"%>

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
    <webopt:BundleReference runat="server" Path="~/Content/bootstrap" />

    <style>
       
    </style>
    <link href="../Content/bootstrap.css" rel="stylesheet" />
    <link href="../Style/StyleJC.css" rel="stylesheet" />
    <script src="https://ajaxzip3.github.io/ajaxzip3.js" charset="UTF-8"></script>
      <script src="../Scripts/jquery-3.5.1.js"></script> 
    <script type="text/javascript">
        
        function isNumber(evt) {
            evt = (evt) ? evt : window.event;
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode > 31 && ((charCode < 48 || charCode > 57) || (charCode > 64 && charCode < 91) || (charCode > 96 && charCode < 123) || charCode == 8 || charCode == 32)) {
                return false;
            }
            return true;
        }

        function textclear() {
            lbl_YubinErr.innerText = "";
            lbl_DanwaErr.innerText = "";
        }

        function RestrictEnter(event) {
            if (event.keyCode === 13) {
                event.preventDefault();
            }
        }

        function callme(element) {

            try {
                var lblFile = document.getElementById("<%=fileupload.ClientID %>");
                var filename = lblFile.files.item(0).name;               
                var reader = new FileReader();
                reader.readAsDataURL(lblFile.files.item(0));
                reader.onload = function (e) {                                     
                    document.getElementById('logo').value = reader.result;
                    document.getElementById('ext').value = filename;
                    document.getElementById('btnUpload').click();
                }

            }
            catch (error) {
                alert(error);
            }
            finally {
            }
        }

        function call_logo1() {
            document.getElementById("fileupload").click();
        }
       

        $(function () {

            //スクロール・バー配置の記憶
            $(window).scroll(function () {
                sessionStorage.scrollTop = $(this).scrollTop();
            });
        });
    </script>

</head>
<body style="background-color: white; padding: 0px; margin: 0px; height: 100%; overflow-x: hidden">

    <form id="form1" runat="server" style="width: 708px;">
        <asp:ScriptManager ID="ScriptManager1" EnablePageMethods="True" runat="server">
            <Scripts>
                <asp:ScriptReference Name="jquery" />
                <asp:ScriptReference Name="bootstrap" />
                <asp:ScriptReference Path="../Scripts/CommonFixFocus.js" />
            </Scripts>
        </asp:ScriptManager>
        <div>
            <asp:UpdatePanel ID="updKyotenToroku" runat="server" UpdateMode="Conditional">
                <%--DefaultButton="Button1"--%>
                <ContentTemplate>
                    <asp:Button ID="Button1" runat="server" CssClass="DefaultBtn" />
                    <div class="row" style="display: flex; justify-content: center; align-items: center; padding: 10px 0 0 40px">
                        <asp:Label ID="lblHeader" runat="server" Text="拠点を登録" CssClass="kyoutennewtitleStyle fw-bold txt-style"></asp:Label>
                        <asp:Button ID="btnHeaderCross" runat="server" Text="✕" CssClass="PopupCloseBtn mr-n3 JC08btnHeaderCross" OnClick="btnCancel_Click" Style="width: 10%; margin: 0!important" />
                    </div>
                    <div class="titleLine"></div>
                   <div class=" modal-body" style="height:750px;"  > <%-- 20220519 maythu add --%>
                        <%--<div class="row mb JC25KyotenNyuuryokuDiv" >--%><%-- 20220503 maythu delete --%> 
                        <div class="row mb mt-sm-2 JC25KyotenNyuuryokuDiv" ><%-- 20220503 maythu add --%> 
                            <div class="col-xs-2 col-sm-2">
                                <asp:Label ID="lblKyotenMei" runat="server" Text="拠点名"></asp:Label>
                                <asp:Label runat="server" Style="color: red;"> * </asp:Label>
                            </div>
                            <div class="col-xs-9 col-sm-9">
                                <asp:UpdatePanel ID="updKyotenMei" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                      <%--  <asp:TextBox ID="txtKyotenMei" AutoPostBack="true" runat="server" onkeyup="DeSelectText(this);" 
                                            CssClass="form-control TextboxStyle " OnTextChanged="txtKyotenMei_TextChanged" onfocus="this.setSelectionRange(this.value.length, this.value.length);"></asp:TextBox>
                                   --%> <%-- 20220503 maythu delete --%>
                                        <%-- 20220503 maythu add --%>
                                          <asp:TextBox ID="txtKyotenMei" AutoPostBack="true" runat="server" onkeyup="DeSelectText(this);" 
                                            CssClass="form-control TextboxStyle JC25Kt_NyuryokuTxtBox " OnTextChanged="txtKyotenMei_TextChanged" onfocus="this.setSelectionRange(this.value.length, this.value.length);"></asp:TextBox> 
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="txtKyotenMei" EventName="TextChanged" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </div>
                        </div>

                        <%--<div class="row mb-2 JC25KyotenNyuuryokuDiv" >--%><%-- 20220503 maythu delete --%> 
                        <div class="row mb-2 mt-sm-2 JC25KyotenNyuuryokuDiv" ><%-- 20220503 maythu add --%> 
                            <div class="col-xs-2 col-sm-2">
                                <asp:Label ID="lblKaishaMei" runat="server" Text="会社名"></asp:Label>
                                <asp:Label runat="server" Style="color: red;"> * </asp:Label>
                            </div>
                            <div class="col-xs-9 col-sm-9">
                                <asp:UpdatePanel ID="updKaishaMei" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                     <%--   <asp:TextBox ID="txtKaishaMei" AutoPostBack="true" runat="server"  onkeyup="DeSelectText(this);"
                                            CssClass="TextboxStyle form-control" OnTextChanged="txtKaishaMei_TextChanged" onfocus="this.setSelectionRange(this.value.length, this.value.length);"></asp:TextBox>
                                   --%>     <%-- 20220503 maythu delete --%>
                                        <%-- 20220503 maythu add --%> 
                                        <asp:TextBox ID="txtKaishaMei" AutoPostBack="true" runat="server"  onkeyup="DeSelectText(this);" 
                                            CssClass="TextboxStyle form-control JC25Kt_NyuryokuTxtBox " OnTextChanged="txtKaishaMei_TextChanged" onfocus="this.setSelectionRange(this.value.length, this.value.length);"></asp:TextBox>
                                  </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="txtKaishaMei" EventName="TextChanged" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </div>
                        </div>

                        <%--<div class="row JC25KyotenNyuuryokuDiv">--%><%-- 20220503 maythu delete --%> 
                        <div class="row mt-sm-2 JC25KyotenNyuuryokuDiv"><%-- 20220503 maythu add --%> 
                            <div class="col-xs-2 col-sm-2">
                                <asp:Label ID="lblYubinBangou" runat="server" Text="郵便番号"></asp:Label>
                                <asp:Label runat="server" Style="color: red;"> * </asp:Label>
                            </div>
                            <div class="col-xs-9 col-sm-9">
                                <asp:UpdatePanel ID="updYubinBangou" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                       <%-- <asp:TextBox ID="txtYubinBangou" AutoPostBack="true" runat="server" MaxLength="7"
                                            CssClass="TextboxStyle form-control"
                                            OnTextChanged="txtYubinBangou_TextChanged"
                                            onfocus="this.setSelectionRange(this.value.length, this.value.length);" onKeyUp="AjaxZip3.zip2addr(this,'','txtJusho1','txtJusho1');textclear();"  onkeypress="return isNumber(event);" onkeydown="RestrictEnter(event);">                                        
                                        </asp:TextBox>--%> <%-- 20220503 maythu delete --%>
                                        <%-- 20220503 maythu add --%>
                                         <asp:TextBox ID="txtYubinBangou" AutoPostBack="true" runat="server" MaxLength="7" 
                                            CssClass="TextboxStyle form-control JC25Kt_NyuryokuTxtBox "
                                            OnTextChanged="txtYubinBangou_TextChanged"
                                            onfocus="this.setSelectionRange(this.value.length, this.value.length);" onKeyUp="AjaxZip3.zip2addr(this,'','txtJusho1','txtJusho1');textclear();"  onkeypress="return isNumber(event);" onkeydown="RestrictEnter(event);">                                        
                                        </asp:TextBox>
                                        <asp:Label ID="lbl_YubinErr" runat="server" Text=" " CssClass="error"></asp:Label>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="txtYubinBangou" EventName="TextChanged" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </div>
                        </div>

                        <%--<div class="row  JC25KyotenNyuuryokuDiv">--%><%-- 20220503 maythu delete --%> 
                        <div class="row mt-sm-2 JC25KyotenNyuuryokuDiv"><%-- 20220503 maythu add --%> 
                            <div class="col-xs-2 col-sm-2">
                                <asp:Label ID="lblJusho1" runat="server" Text="住所1"></asp:Label>
                                <asp:Label runat="server" Style="color: red;"> * </asp:Label>
                            </div>
                            <div class="col-xs-9 col-sm-9">
                                <asp:UpdatePanel ID="updJusho1" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                   <%--     <asp:TextBox ID="txtJusho1" AutoPostBack="true" runat="server" onkeyup="DeSelectText(this);"
                                            CssClass="TextboxStyle form-control" OnTextChanged="txtJusho1_TextChanged" onfocus="this.setSelectionRange(this.value.length, this.value.length);"></asp:TextBox>
                                   --%>   <%-- 20220503 maythu delete --%>
                                        <%-- 20220503 maythu add --%>
                                        <asp:TextBox ID="txtJusho1" AutoPostBack="true" runat="server" onkeyup="DeSelectText(this);" 
                                            CssClass="TextboxStyle form-control  JC25Kt_NyuryokuTxtBox " OnTextChanged="txtJusho1_TextChanged" onfocus="this.setSelectionRange(this.value.length, this.value.length);"></asp:TextBox>
                                     </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="txtJusho1" EventName="TextChanged" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </div>
                        </div>

                        <%--<div class="row JC25KyotenNyuuryokuDiv">--%><%-- 20220503 maythu delete --%> 
                        <div class="row mt-sm-2 JC25KyotenNyuuryokuDiv"><%-- 20220503 maythu add --%> 
                            <div class="col-xs-2 col-sm-2">
                                <asp:Label ID="lbljusho2" runat="server" Text="住所2"></asp:Label>
                            </div>
                            <div class="col-xs-9 col-sm-9">
                                <asp:UpdatePanel ID="updjusho2" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                    <%--    <asp:TextBox ID="txtjusho2" AutoPostBack="true" runat="server" onkeyup="DeSelectText(this);"
                                            CssClass="TextboxStyle form-control" OnTextChanged="txtjusho2_TextChanged" onfocus="this.setSelectionRange(this.value.length, this.value.length);"></asp:TextBox>
                                   --%>     <%-- 20220503 maythu delete --%>
                                        <%-- 20220503 maythu add --%>
                                        <asp:TextBox ID="txtjusho2" AutoPostBack="true" runat="server" onkeyup="DeSelectText(this);" 
                                            CssClass="TextboxStyle form-control JC25Kt_NyuryokuTxtBox " OnTextChanged="txtjusho2_TextChanged" onfocus="this.setSelectionRange(this.value.length, this.value.length);"></asp:TextBox>
                                  </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="txtjusho2" EventName="TextChanged" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </div>
                        </div>

                        <%--<div class="row JC25KyotenNyuuryokuDiv">--%><%-- 20220503 maythu delete --%> 
                        <div class="row mt-sm-2 JC25KyotenNyuuryokuDiv"><%-- 20220503 maythu add --%> 
                            <div class="col-xs-2 col-sm-2 ">
                                <asp:Label ID="lbldanwa" runat="server" Text="電話"></asp:Label>
                            </div>
                            <div class="col-xs-4 col-sm-4">
                                <asp:UpdatePanel ID="upddanwa" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                      <%--  <asp:TextBox ID="txtdanwa" AutoPostBack="true" runat="server" MaxLength="20" onkeyup="DeSelectText(this);"
                                            CssClass="TextboxStyle form-control" OnTextChanged="txtdanwa_TextChanged" onfocus="this.setSelectionRange(this.value.length, this.value.length);" onkeypress="return isNumber(event);"></asp:TextBox>
                                       --%>  <%-- 20220503 maythu delete --%>
                                        <%-- 20220503 maythu add --%>
                                        <asp:TextBox ID="txtdanwa" AutoPostBack="true" runat="server" MaxLength="20" onkeyup="DeSelectText(this);" 
                                            CssClass="TextboxStyle form-control JC25Kt_NyuryokuSmallTxtBox " OnTextChanged="txtdanwa_TextChanged" onfocus="this.setSelectionRange(this.value.length, this.value.length);" onkeypress="return isNumber(event);"></asp:TextBox>
                                        <asp:Label ID="lbl_DanwaErr" runat="server" Text=" " CssClass="error"></asp:Label>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="txtdanwa" EventName="TextChanged" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </div>
                            <div class="col-xs-1 col-sm-1">
                                <asp:Label ID="lblfax" runat="server" Text="FAX"></asp:Label>
                            </div>
                            <div class="col-xs-4 col-sm-4">
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <%--<asp:TextBox ID="txtfax" AutoPostBack="true" runat="server" MaxLength="20" onkeyup="DeSelectText(this);"
                                            CssClass="TextboxStyle form-control" OnTextChanged="txtfax_TextChanged" onfocus="this.setSelectionRange(this.value.length, this.value.length);" onkeypress="return isNumber(event);"></asp:TextBox>
                                      --%>   <%-- 20220503 maythu delete --%>
                                        <%-- 20220503 maythu add --%>
                                        <asp:TextBox ID="txtfax" AutoPostBack="true" runat="server" MaxLength="20" onkeyup="DeSelectText(this);" 
                                            CssClass="TextboxStyle form-control  JC25Kt_NyuryokuSmallTxtBox " OnTextChanged="txtfax_TextChanged" onfocus="this.setSelectionRange(this.value.length, this.value.length);" onkeypress="return isNumber(event);"></asp:TextBox>
                                      <asp:Label ID="lbl_FaxErr" runat="server" Text=" " CssClass="error"></asp:Label>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="txtfax" EventName="TextChanged" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </div>
                        </div>    
                        
                        <%-- 20220429 maythu add start --%>
                         <%--<div class="row JC25KyotenNyuuryokuDiv">--%>
                         <div class="row mt-sm-2 JC25KyotenNyuuryokuDiv"> <%-- 20220503 maythu add --%> 
                            <div class="col-xs-2 col-sm-2">
                                <asp:Label ID="lblURL" runat="server" Text="HP URL"></asp:Label>
                            </div>
                            <div class="col-xs-9 col-sm-9">
                                <asp:UpdatePanel ID="updURL" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <%--<asp:TextBox ID="txtURL" AutoPostBack="true" runat="server" onkeyup="DeSelectText(this);" 
                                            CssClass="TextboxStyle form-control"  onfocus="this.setSelectionRange(this.value.length, this.value.length);"></asp:TextBox>--%> <%-- 20220503 maythu delete --%>
                                        <%-- 20220503 maythu add --%>
                                  <asp:TextBox ID="txtURL" AutoPostBack="true" runat="server" onkeyup="DeSelectText(this);"  
                                            CssClass="TextboxStyle form-control  JC25Kt_NyuryokuTxtBox "  onfocus="this.setSelectionRange(this.value.length, this.value.length);"></asp:TextBox>  </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="txtURL" EventName="TextChanged" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </div>
                        </div>

                         <%--<div class="row JC25KyotenNyuuryokuDiv">--%>
                         <div class="row mt-sm-2 JC25KyotenNyuuryokuDiv"> <%-- 20220503 maythu add --%> 
                            <div class="col-xs-2 col-sm-2">
                                <asp:Label ID="lblmail" runat="server" Text="代表mail"></asp:Label>
                            </div>
                            <div class="col-xs-9 col-sm-9">
                                <asp:UpdatePanel ID="updmail" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                       <%-- <asp:TextBox ID="txtmail" AutoPostBack="true" runat="server" onkeyup="DeSelectText(this);"
                                            CssClass="TextboxStyle form-control"  onfocus="this.setSelectionRange(this.value.length, this.value.length);"></asp:TextBox>--%>  <%-- 20220503 maythu delete --%>
                                        <%-- 20220503 maythu add --%>
                                    <asp:TextBox ID="txtmail" AutoPostBack="true" runat="server" onkeyup="DeSelectText(this);" 
                                            CssClass="TextboxStyle form-control  JC25Kt_NyuryokuTxtBox "  onfocus="this.setSelectionRange(this.value.length, this.value.length);"></asp:TextBox>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="txtmail" EventName="TextChanged" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                         
                         <%--<div class="row JC25KyotenNyuuryokuDiv" >--%>
                         <div class="row mt-sm-2 JC25KyotenNyuuryokuDiv" ><%-- 20220503 maythu add --%> 
                            <div class="col-xs-2 col-sm-2">
                                <asp:Label ID="lblLogo" runat="server" Text="ロゴ"></asp:Label>
                            </div>
                              <div class="col-xs-3 col-sm-3"  >
                        <asp:UpdatePanel ID="updCombobox" runat="server" UpdateMode="conditional">
                           <ContentTemplate>
                               <%-- <asp:DropDownList ID="DDL_kyoutenLogo" runat="server"  CssClass="user_select font" Width="100px" AutoPostBack="True" Height="30px" style="border:1px solid #9b9b9b;border-radius:4px;" OnSelectedIndexChanged="DDL_kyoutenLogo_SelectedIndexChanged">--%><%-- 20220504 maythu delete --%> 
                                <asp:DropDownList ID="DDL_kyoutenLogo" runat="server"  CssClass="user_select font" Width="100px" AutoPostBack="True" Height="30px" 
                                    style="border:1px solid #9b9b9b;border-radius:2px;" OnSelectedIndexChanged="DDL_kyoutenLogo_SelectedIndexChanged"> <%-- 20220505 maythu add --%> 
                                    <%--<asp:ListItem Selected="True"></asp:ListItem>--%>
                                    <asp:ListItem >ロゴ1</asp:ListItem>
                                    <asp:ListItem >ロゴ2</asp:ListItem>
                                    <asp:ListItem >ロゴ3</asp:ListItem>
                                    <asp:ListItem >ロゴ4</asp:ListItem>
                                    <asp:ListItem >ロゴ5</asp:ListItem>
                                      </asp:DropDownList>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                                </div>
                                
                             <div class="col-xs-2 col-sm-2 " >
                                <asp:Label ID="lblLogotitle" runat="server" Text="ロゴタイトル"></asp:Label>
                            </div>
                             <div class="col-xs-4 col-sm-4">
                                <asp:UpdatePanel ID="updLogotitle" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                      <%--  <asp:TextBox ID="txtLogotitle" runat="server" AutoPostBack="true" onkeyup="DeSelectText(this);" CssClass="TextboxStyle form-control"
                                            OnTextChanged="txtLogotitle_TextChanged" onfocus="this.setSelectionRange(this.value.length, this.value.length);" ></asp:TextBox>
                                     --%>   <%-- 20220503 maythu delete --%>
                                        <%-- 20220503 maythu add --%>
                                          <asp:TextBox ID="txtLogotitle" runat="server" AutoPostBack="true" onkeyup="DeSelectText(this);" CssClass="TextboxStyle form-control JC25Kt_NyuryokuSmallTxtBox "
                                            OnTextChanged="txtLogotitle_TextChanged" onfocus="this.setSelectionRange(this.value.length, this.value.length);"
                                              ></asp:TextBox>
                                     </ContentTemplate>
                                        </asp:UpdatePanel>
                            </div>
                        </div>
                        <%-- 20220429 maythu add end  --%>

                        <%-- 20220429 maythu delete start --%>
                        <%--<asp:UpdatePanel ID="updRadio" runat="server" UpdateMode="conditional">
                           <ContentTemplate>
                               <div class="container-fluid ">
                                   <div class="row JC25KyotenNyuuryokuDiv " style="background-color: #dddddd;height:30px;width:95%"> 
                                         <asp:RadioButton ID="rbtnLogo1" Text="" GroupName="rgKyotentoroku" runat="server" OnCheckedChanged="rbtnLogo1_CheckedChanged" AutoPostBack="true" />                                        
                                         <asp:TextBox ID="txtlogoName1" CssClass="logotextstyle form-control" runat="server" Width="100px" Style="font-size:13px;" placeholder="ロゴ名１" OnTextChanged="logoName1txtChanged" AutoPostBack="true" ></asp:TextBox>                                          
                                         <asp:RadioButton ID="rbtnLogo2"  Text="" GroupName="rgKyotentoroku" runat="server" OnCheckedChanged="rbtnLogo2_CheckedChanged" AutoPostBack="true" />                                            
                                         <asp:TextBox ID="txtlogoName2" CssClass="logotextstyle form-control" runat="server" Width="100px" Style="font-size:13px;" placeholder="ロゴ名２" OnTextChanged="logoName2txtChanged" AutoPostBack="true" ></asp:TextBox>                                                                                    
                                         <asp:RadioButton ID="rbtnLogo3"  Text="" GroupName="rgKyotentoroku" runat="server" OnCheckedChanged="rbtnLogo3_CheckedChanged" AutoPostBack="true" />
                                         <asp:TextBox ID="txtlogoName3" CssClass="logotextstyle form-control" runat="server" Width="100px" Style="font-size:13px;" placeholder="ロゴ名3" OnTextChanged="logoName3txtChanged" AutoPostBack="true" ></asp:TextBox>                                        
                                         <asp:RadioButton ID="rbtnLogo4"  Text="" GroupName="rgKyotentoroku" runat="server" OnCheckedChanged="rbtnLogo4_CheckedChanged" AutoPostBack="true" />
                                         <asp:TextBox ID="txtlogoName4" CssClass="logotextstyle form-control Editform"  runat="server" Width="100px" Style="font-size:13px;" placeholder="ロゴ名4" OnTextChanged="logoName4txtChanged" AutoPostBack="true" ></asp:TextBox>
                                         <asp:RadioButton ID="rbtnLogo5"  Text="" GroupName="rgKyotentoroku" runat="server" OnCheckedChanged="rbtnLogo5_CheckedChanged" AutoPostBack="true" />
                                         <asp:TextBox ID="txtlogoName5" CssClass="logotextstyle form-control Editform" runat="server" Width="100px" Style="font-size:13px;" placeholder="ロゴ名5" OnTextChanged="logoName5txtChanged" AutoPostBack="true" ></asp:TextBox>
                                     </div>
                                </div>
                                   </ContentTemplate>
                                </asp:UpdatePanel>--%>
                        <%-- 20220429 maythu delete end --%>
                                
                            <%--</div>--%>
                        

                        <div class="JC25KyotenNyuuryokuDiv">
                          <%--  <table style="margin: 20px 20px 20px 20px " >--%>  <%-- 20220503 maythu delete --%> 
                              <table style="margin: -10px 20px 20px 122px;" ><%-- 20220505 maythu add --%>
                                 <%-- 20220505 maythu add start --%>
                                  <tr>
                                    <td>
                                        <asp:UpdatePanel ID="updImageError" runat="server" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <asp:Label ID="lblImgError" runat="server" CssClass="LblErrorMessage col-sm-8"></asp:Label>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </td>
                                    <td></td>
                                </tr>
                                  <%-- 20220505 maythu add end --%>
                                <tr>
                                    <td style="vertical-align: top;padding-right: 25px ">                                        
                                        <asp:UpdatePanel ID="updImg" runat="server" UpdateMode="conditional" ChildrenAsTriggers="false">
                                                 <%--<Triggers>
                                                    <asp:PostBackTrigger ControlID="btnUpload" />                                                    
                                                  </Triggers>--%>
                                                <ContentTemplate>
                                                   <%--<div style="padding:0;margin:0;"> --%> <%-- 20220505 maythu delete --%>
                                                   <div style="padding:0;margin-top:7px;"> <%-- 20220505 maythu add --%>
                                                        <div style="display: flex; justify-content:flex-end; padding:0;margin:0;position:relative">                            
                                                               <asp:Button ID="Button2" runat="server" Text="✕" CssClass="PopupCloseBtn mr-n3 JC25DelImg" OnClick="btnDelete_Click" />
                                                        </div>
                                                        <%-- <div style="display: flex; justify-content:center; padding:0;margin:-30px 0px 19px 0px ; ">--%><%-- 20220425 maythu delete --%> 
                                                       <%--<div style="display:flex ; justify-content:center; padding:0px !important;margin:-30px 0px 19px 0px ;overflow:hidden; width:350px;height:150px; border:0.5px solid rgb(155,155,155) !important ; "> --%><%-- 20220427 maythu add --%>     
                                                       <div style="display:flex ; justify-content:center; padding:0px !important;margin:-30px 0px 8px 0px ;overflow:hidden; width:350px;height:150px; border:0.5px solid rgb(155,155,155) !important ; "> <%-- 20220503 maythu add --%>     
                                                       <asp:ImageButton ID="btnKyotenLogo1" runat="server" CssClass="JC25Kt_NyuurokuImg" ImageUrl="~/Img/logokyoten.png" OnClientClick="call_logo1()" />
                                                                <asp:ImageButton ID="btnKyotenLogo2" runat="server" CssClass="JC25Kt_NyuurokuImg" ImageUrl="~/Img/logokyoten.png" OnClientClick="call_logo1()" />
                                                                <asp:ImageButton ID="btnKyotenLogo3" runat="server" CssClass="JC25Kt_NyuurokuImg" ImageUrl="~/Img/logokyoten.png" OnClientClick="call_logo1()" />
                                                                <asp:ImageButton ID="btnKyotenLogo4" runat="server" CssClass="JC25Kt_NyuurokuImg" ImageUrl="~/Img/logokyoten.png" OnClientClick="call_logo1()" />
                                                                <asp:ImageButton ID="btnKyotenLogo5" runat="server" CssClass="JC25Kt_NyuurokuImg" ImageUrl="~/Img/logokyoten.png" OnClientClick="call_logo1()" />                                                    
                                                                <asp:FileUpload ID="fileupload" runat="server" onchange="callme(this);" accept=".png,.jpg,.jpeg,.bmp" Style="display: none; position: absolute" />
                                                        </div>
                                           
                                                       <%-- 20220429 maythu delete start --%>
                                                        <%-- <div style="display: flex; justify-content:center; padding:0;margin:0;margin-top: -25px;">
                                                            <asp:Button ID="btnPreview" runat="server" Text="プレビュー" CssClass="JC25Kt_NyuurokuPreview" OnClick="btnPreview_Click" />
                                                        </div>--%>
                                                        <%-- 20220429 maythu delete end --%>

                                                     </div>
                                                </ContentTemplate>
                                                    <Triggers>
                                                        <asp:PostBackTrigger ControlID="btnPreview" />
                                                     </Triggers>
                                             </asp:UpdatePanel>
                                        </td>
                                       <%-- <td style="vertical-align: top;">
                                            <asp:TextBox ID="txtSetsuMei" AutoPostBack="true" runat="server" TextMode="MultiLine" onkeyup="DeSelectText(this);" MaxLength="200" placeholder="説明文書"
                                                CssClass="TextboxStyle form-control JC25Kt_NyuryokuTxtArea" Style="font-size: 13px;" Rows="15" Columns="40" OnTextChanged="txtSetsuMei_TextChanged" onfocus="this.setSelectionRange(this.value.length, this.value.length);"></asp:TextBox>                                        
                                        </td>--%>

                                    <%-- 20220429 maythu add start --%>
                                    <td style=" vertical-align: bottom;" >
                                     <%--<div style="display: flex; justify-content:center; padding:0;margin-bottom:20px;" >--%><%-- 20220503 maythu delete --%> 
                                     <div style="display: flex; justify-content:center; padding:0; margin-bottom:8px;" ><%-- 20220503 maythu add --%> 
                                        <asp:Button ID="btnPreview" runat="server" Text="プレビュー" CssClass="JC25Kt_NyuurokuPreview"  OnClick="btnPreview_Click" />
                                     </div>

                                    </td>
                                    <%-- 20220429 maythu add end --%>

                                </tr>
                                   <%-- 20220505 maythu delete start --%>
                                <%--<tr>
                                    <td>
                                        <asp:UpdatePanel ID="updImageError" runat="server" UpdateMode="Conditional">
                                            <ContentTemplate>
                                                <asp:Label ID="lblImgError" runat="server" CssClass="LblErrorMessage col-sm-8"></asp:Label>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </td>
                                    <td></td>
                                </tr>--%>
                                   <%-- 20220505 maythu delete end --%>
                                <tr>
                                     <td style="vertical-align: top;">
                                            <asp:TextBox ID="txtSetsuMei" AutoPostBack="true" runat="server" TextMode="MultiLine" onkeyup="DeSelectText(this);" MaxLength="200" placeholder="説明文書"
                                              CssClass="TextboxStyle form-control JC25Kt_NyuryokuTxtArea" Style="font-size: 13px;" Rows="9" Columns="20" OnTextChanged="txtSetsuMei_TextChanged" onfocus="this.setSelectionRange(this.value.length, this.value.length);"></asp:TextBox>                                        
                                     </td>
                                     <%-- 20220429 maythu add start --%>
                                    <td style="vertical-align: bottom;">
                                     <div style="display: flex; justify-content:center; padding:0;margin:0;"> 
                                    <asp:Button ID="btnShokimodoru" runat="server" Text="初期値に戻す" CssClass="JC25Kt_NyuurokuShokimodoru"   OnClick="btnShokimodoru_Click"  />
                                     </div>
                                    </td>
                                    <%-- 20220429 maythu add end --%>
                                </tr>
                            </table>
                        </div>

                    </div>
                    <%--------------add by テテ 20211206 start------------%>
                    <div id="Footer" style="display: block; min-width: 100%; bottom: 0px; padding: 0px;position:fixed;">
                        <div style="display: flex; justify-content: center; align-items: center; height: 75px; background-color: #eee; margin: 0px; min-width: 715px;">
                            <asp:Button ID="btnKyotenSave" runat="server" Text="保存" CssClass="BlueBackgroundButton" OnClick="btnSave_Click" Style="width: 100px;" />
                            <asp:Button ID="btnKyotenCancel" runat="server" CssClass="JC10CancelBtn" Width="100px" Text="キャンセル" OnClick="btnCancel_Click" />

                        </div>

                    </div>
                    <%--------------add by テテ 20211206 end------------%>
                    <div style="display: none; position: absolute">
                        <asp:TextBox ID="txt_cCo" runat="server"></asp:TextBox>
                        <asp:Button ID="btnUpload" runat="server" Text="upload" OnClick="imgBtn_click" Style="display: none; position: absolute" />
                        <asp:HiddenField ID="logo" runat="server" />
                        <asp:HiddenField ID="ext" runat="server" />
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </form>
</body>
</html>
