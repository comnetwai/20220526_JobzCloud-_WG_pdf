<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="JC02Touroku.aspx.cs" Inherits="jobzcolud.JC02Touroku" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <link href="../Content/bootstrap.min.css" rel="stylesheet" />
    <script src="../Scripts/bootstrap.bundle.min.js"></script>    
    <script src="https://ajaxzip3.github.io/ajaxzip3.js" charset="UTF-8"></script>
    <asp:PlaceHolder runat="server">
        <%: Styles.Render("~/style/AccCreate") %>
    </asp:PlaceHolder>
    <script type="text/javascript">
        
        function isNumber(evt) {
            evt = (evt) ? evt : window.event;
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode > 31 && ((charCode < 48 || charCode > 57) || (charCode > 64 && charCode < 91) || (charCode > 96 && charCode < 123) || charCode == 8 || charCode == 32)) {
                return false;
            }
            return true;
        }

        function numericFilter(txb) {
            txb.value = txb.value.replace(/[^\0-9]/ig, "");
        }

        function enableordisable() {
            if (document.getElementById('CK_agree').checked == true) {
                document.getElementById('BT_sakusei').disabled = false;
            }
            else {
                document.getElementById('BT_sakusei').disabled = true;
            }
        }

        function textclear()
        {
            LB_Yuubenbangouerror.innerText = "";
            LB_DenwaError.innerText = "";
            LB_mailerror.innerText = "";
        }

        function RestrictEnter(event) {
            if (event.keyCode === 13) {
                event.preventDefault();
            }
        }
        
      </script>
    
</head>
<body>
    <form id="form1" runat="server">

        <div class="container-fluid">
            <div class="row ">
                 <div class="col-md col-xxl"></div>
                <div class="col-md-7 col-xxl-5">
                    <div class="touroku-form  bg-white mt-4 mb-4 p-4 shadow-sm" style="border-radius:10px;">
                        <div class="row justify-content-center align-content-center mt-3">
                            <asp:Image ID="imgLogo" CssClass="logo" runat="server" ImageUrl="~/Images/JOBZ_CLOUD_ロゴ.jpg" />
                        </div>
                       
                        <div id="Div_touroku" runat="server">
                            <div class="form-group row"style="margin-top:75px;">
                            <div class="col-sm-1"></div>
                            <label class="col-sm-3 col-form-label LoginFont">ログインID <span class="JC02Star-color fw-bold">*</span></label>                               
                           <div class="col-sm-7">
                               <asp:TextBox ID="TB_Login_Id" runat="server" name="username" CssClass="form-control LoginFont input LoginTextboxStyle" placeholder="メールアドレスを入力" OnTextChanged="TB_Login_Id_TextChanged" MaxLength="100" onkeypress="RestrictEnter(event);textclear();"></asp:TextBox>
                            </div>
                            <div class="col-sm-4"></div>
                            <div class="col-sm-7 mb-1" style="height:1px;">
                                <asp:Label ID="LB_mailerror" runat="server" Text=" " CssClass="error"></asp:Label>
                            </div>
                            </div>

                            <div class="form-group row mt-4">
                                <div class="col-sm-1"></div>
                                <label class="col-sm-3 col-form-label LoginFont">ユーザー名 <span class="fw-bold JC02Star-color">*</span></label>                             
                                <div class="col-sm-7">
                                   <asp:TextBox ID="TB_Usermei" runat="server" name="username" CssClass="form-control LoginFont input LoginTextboxStyle" MaxLength="50" onkeypress="RestrictEnter(event);"></asp:TextBox>
                                </div>
                            </div>

                            <div class="form-group row mt-4">
                                <div class="col-sm-1"></div>
                                <label class="col-sm-3 col-form-label LoginFont">パスワード <span class="JC02Star-color fw-bold">*</span></label>
                               
                                <div class="col-sm-7">
                                   <asp:TextBox ID="TB_password" runat="server" name="username" CssClass="form-control LoginFont input LoginTextboxStyle" placeholder="8文字以上20文字以下" type="password" MaxLength="20" onkeypress="RestrictEnter(event);"></asp:TextBox>
                                </div>
                                <div class="col-sm-4"></div>
                                <div class="col-sm-7 mb-1" style="height:1px;">
                                    <asp:Label ID="LB_pwderror" runat="server" Text=" " CssClass="error"></asp:Label>
                                </div>
                            </div>

                            <div class="form-group row mt-4">
                                <div class="col-sm-1"></div>
                                <label class="col-sm-3 col-form-label LoginFont">パスワード再入力 <span class="JC02Star-color fw-bold">*</span></label>
                               <div class="col-sm-7">
                                   <asp:TextBox ID="TB_repassword" runat="server" name="username" CssClass="form-control LoginFont input LoginTextboxStyle" placeholder="8文字以上20文字以下" type="password" MaxLength="20" onkeypress="RestrictEnter(event);"></asp:TextBox>
                                </div>
                            </div>
                            <div class="form-group row mt-4">
                                <div class="col-sm-1"></div>
                                <label class="col-sm-3 col-form-label LoginFont">会社名 <span class="JC02Star-color fw-bold">*</span></label>
                                <div class="col-sm-7">
                                   <asp:TextBox ID="TB_Kaishamei" runat="server" name="username" CssClass="form-control LoginFont input LoginTextboxStyle" placeholder="個人の方は個人とご記入ください" MaxLength="100" onkeypress="RestrictEnter(event);"></asp:TextBox>
                                </div>
                            </div>

                            <div class="form-group row mt-4">
                                <div class="col-sm-1"></div>
                                <label class="col-sm-3 col-form-label LoginFont">郵便番号 <span class="JC02Star-color fw-bold">*</span></label>
                                <div class="col-sm-7">
                                   <asp:TextBox ID="TB_Yuubenbangou" runat="server" name="username" CssClass="form-control LoginFont input LoginTextboxStyle" MaxLength="7" onKeyUp="AjaxZip3.zip2addr(this,'','TB_Juusho1','TB_Juusho1');textclear();" onkeypress="return isNumber(event);" OnTextChanged="TB_Yuubenbangou_TextChanged" onkeydown="RestrictEnter(event);"></asp:TextBox>
                                </div>
                                <div class="col-sm-4"></div>
                                <div class="col-sm-7 mb-1" style="height:1px;">
                                    <asp:Label ID="LB_Yuubenbangouerror" runat="server" Text=" " CssClass="error"></asp:Label>
                                </div>
                            </div>

                            <div class="form-group row mt-4">
                                <div class="col-sm-1"></div>
                                <label class="col-sm-3 col-form-label LoginFont">住所１ <span class="JC02Star-color fw-bold">*</span></label>
                                <div class="col-sm-7">
                                   <asp:TextBox ID="TB_Juusho1" runat="server" name="username" CssClass="form-control LoginFont input LoginTextboxStyle" MaxLength="40" onkeypress="RestrictEnter(event);"></asp:TextBox>
                                </div>
                            </div>

                            <div class="form-group row mt-4">
                                <div class="col-sm-1"></div>
                                <label class="col-sm-3 col-form-label LoginFont">住所２ </label>
                                <div class="col-sm-7">
                                   <asp:TextBox ID="TB_Juusho2" runat="server" name="username" CssClass="form-control LoginFont input LoginTextboxStyle" MaxLength="40" onkeypress="RestrictEnter(event);"></asp:TextBox>
                                </div>
                            </div>

                            <div class="form-group row mt-4">
                                <div class="col-sm-1"></div>
                                <label class="col-sm-3 col-form-label LoginFont">電話 <span class="JC02Star-color fw-bold">*</span></label>
                                <div class="col-sm-7">
                                   <asp:TextBox ID="TB_Denwa" runat="server" name="username" CssClass="form-control LoginFont input LoginTextboxStyle" onkeypress="return isNumber(event);" MaxLength="11" OnTextChanged="TB_Denwa_TextChanged" onkeyup="textclear();" onkeydown="RestrictEnter(event);"></asp:TextBox>
                                </div>
                                <div class="col-sm-4"></div>
                                <div class="col-sm-7 mb-1" style="height:1px;">
                                    <asp:Label ID="LB_DenwaError" runat="server" Text=" " CssClass="error"></asp:Label>
                                </div>
                            </div>


                            <div class="form-group row mt-4 mb-2 ">
                                <div class="col-sm-3"></div>
                                <div class="col-sm-7 d-flex justify-content-center">
                               <asp:Button ID="BT_kakunin" runat="server" Text="確認画面　＞" type="button " CssClass="btn btn-primary LoginFont" style="width:170px;" OnClick="BT_kakunin_Click"/>
                               </div>
                            </div>
                        </div>
                        
                        <div id="Div_tourokukakunin" runat="server">
                            <div class="form-group row"style="margin-top:75px;">
                            <div class="col-1"></div>
                                <asp:Label ID="Label2" runat="server" Text="ログインID" CssClass="col-sm-3 col-form-label LoginFont"></asp:Label>
                                <asp:Label ID="LB_loginid" runat="server" text="test@gmail.com" CssClass="col-sm-7 col-form-label LoginFont"></asp:Label>
                        </div>

                        <div class="form-group row mt-4">
                            <div class="col-1"></div>
                                <asp:Label ID="Label3" runat="server" Text="ユーザー名" CssClass="col-sm-3 col-form-label LoginFont "></asp:Label>
                                <asp:Label ID="LB_usermei" runat="server" Text="テスト太郎" CssClass="col-sm-7 col-form-label LoginFont"></asp:Label>
                        </div>

                        <div class="form-group row mt-4">
                            <div class="col-1"></div>
                                <asp:Label ID="Label5" runat="server" Text="パスワード" CssClass="col-sm-3 col-form-label LoginFont"></asp:Label>
                                 <asp:Label ID="LB_password" runat="server" Text="******" CssClass="col-sm-7 col-form-label LoginFont"></asp:Label>
                        </div>

                        <div class="form-group row mt-4">
                            <div class="col-1"></div>
                                <asp:Label ID="Label7" runat="server" Text="パスワード再入力" CssClass="col-sm-3 col-form-label LoginFont"></asp:Label>
                                <asp:Label ID="LB_repassword" runat="server" Text="******" CssClass="col-sm-7 col-form-label LoginFont"></asp:Label>
                        </div>

                        <div class="form-group row mt-4">
                            <div class="col-1"></div>
                                <asp:Label ID="Label9" runat="server" Text="会社名" CssClass="col-sm-3 col-form-label LoginFont"></asp:Label>
                            <asp:Label ID="LB_gaishamei" runat="server" Text="テスト株式会社" CssClass="col-sm-7 col-form-label LoginFont"></asp:Label>
                        </div>

                        <div class="form-group row mt-4">
                            <div class="col-1"></div>
                                <asp:Label ID="Label11" runat="server" Text="郵便番号" CssClass="col-sm-3 col-form-label LoginFont"></asp:Label>
                            <asp:Label ID="LB_yuubenbangou" runat="server" Text="6500046" CssClass="col-sm-7 col-form-label LoginFont"></asp:Label>
                            
                        </div>

                        <div class="form-group row mt-4">
                            <div class="col-1"></div>
                                <asp:Label ID="Label13" runat="server" Text="住所１" CssClass="col-sm-3 col-form-label LoginFont"></asp:Label>
                            <asp:Label ID="LB_juusho1" runat="server" Text="神戸市中央区港島南町7丁目" CssClass="col-sm-7 col-form-label LoginFont"></asp:Label>
                        </div>

                        <div class="form-group row mt-4">
                            <div class="col-1"></div>
                                <asp:Label ID="Label15" runat="server" Text="住所２" CssClass="col-sm-3 col-form-label LoginFont"></asp:Label>
                            <asp:Label ID="LB_juusho2" runat="server" Text="" CssClass="col-sm-7 col-form-label LoginFont"></asp:Label>
                        </div>

                        <div class="form-group row mt-4">
                            <div class="col-1"></div>
                                <asp:Label ID="Label17" runat="server" Text="電話" CssClass="col-sm-3 col-form-label LoginFont"></asp:Label>
                            <asp:Label ID="LB_denwa" runat="server" Text="0780456679" CssClass="col-sm-7 col-form-label LoginFont"></asp:Label>
                        </div>

                        <div class="form-group row mt-4">
                            <div class="col-1"></div>
                            <input class="form-check-input" type="checkbox" id="CK_agree" onchange="enableordisable()" value="1" style="width:25px;height:25px;margin-left:10px;" />
                            <div class="col-8 mt-1">
                                <a href="LicenseAgreement.html" class="link-primary text-decoration-none LoginFont form-check-label" target="_blank">利用規約</a>
                                <label class="form-check-label" for="inlineCheckbox1" >を同意する</label>
                            </div>
                        </div>
                       
                        <div class="form-group row mt-4 gap-2">
                            <div class="col-md-3"></div>
                            <div class="col-md-2 text-center">
                           <asp:Button ID="BT_modoru" runat="server" Text="＜　戻る" type="button " CssClass="btn btn-outline-primary LoginFont" style="width:120px;" OnClick="BT_modoru_Click"/>
                           </div>
                            <div class="col-md-6 text-center">
                           <asp:Button ID="BT_sakusei" runat="server" Text="アカウント作成" type="button " CssClass="btn btn-primary LoginFont" style="width:220px;" Enabled="False" OnClick="BT_sakusei_Click"/>
                            </div>
                        </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-3">

                        </div>
                        <div class=" col-7 d-flex justify-content-center mb-3">
                        <a href="JC01Login.aspx" class="link-primary text-decoration-none LoginFont" runat="server">既にアカウントをお持ちの方はこちら</a>

                        </div>
                    </div>
                </div>
                 <div class="col-md col-xxl"></div>
            </div>
        </div>

    </form>
</body>
</html>
