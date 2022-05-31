<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="JC05Passwordreset.aspx.cs" Inherits="jobzcolud.WebFront.JC05Passwordreset" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <link href="../Content/bootstrap.min.css" rel="stylesheet" />
    <script src="../Scripts/bootstrap.bundle.min.js"></script>
    <title></title>
    <asp:PlaceHolder runat="server">
        <%: Styles.Render("~/style/AccCreate") %>
     </asp:PlaceHolder>
</head>
<body>
    <form id="form1" runat="server">
        <div class="container-fluid">
            <div class="row align-items-center" style="height:100vh;">
                 <div class="col-md col-xxl"></div>
                <div class="col-md-7 col-xxl-5">
                    <div class="passwordreset-form  bg-white mt-4 mb-4 p-4 shadow-sm" style="border-radius:10px;">
                        <div class="row justify-content-center align-content-center mt-3">
                            <asp:Image ID="imgLogo" CssClass="logo" runat="server" ImageUrl="~/Images/JOBZ_CLOUD_ロゴ.jpg" />
                        </div>
                        <div id="Div_mail" runat="server">
                         <div class="text-center mt-5 mb-4">
                             <asp:Label ID="LB_Title" runat="server" Text="パスワードのリセット方法をメールでお送りします。" class="font" ></asp:Label>
                         </div>

                         <div class="form-group row mt-4 gap-2">
                            <div class="col-sm-1"></div>
                            <div class="col-sm-7">
                               <asp:TextBox ID="TB_Email" runat="server" name="email" CssClass="form-control font input TextboxStyle" placeholder="登録したメールアドレスを入力"></asp:TextBox>
                                <asp:Label ID="LB_Error" runat="server" Text=" " CssClass="error ms-1"></asp:Label>

                            </div>
                            <div class="col-sm-2 text-center">
                                <asp:Button ID="BT_Mailsend" runat="server" Text="メールを送信" type="button " CssClass="btn btn-primary font" OnClick="BT_Mailsend_Click" />
                            </div>
                        </div>
                            </div>

                        <div id="Div_mailsend" runat="server">
                            <div class="text-center mt-5 mb-4">
                             <asp:Label ID="Label2" runat="server" Text="登録済みのメールアドレスにパスワードのリセット方法を送信しました。" class="font" ></asp:Label><br />
                                 
                            </div>
                            <div class="text-center mt-2 mb-4">
                                 <asp:Label ID="LB_Mailaddress" runat="server" Text="s.gi@comnet-network.co.jp" class="font fw-bold" ></asp:Label>
                            </div>
                            </div>

                        <div id="Div_pwdreset" runat="server">
                            <div class="text-center mt-5 mb-4">
                             <asp:Label ID="Label3" runat="server" Text="パスワード再設定" class="font fw-bold" ></asp:Label><br />   
                            </div>

                            <div class="form-group row mt-3">
                                <div class="col-sm-1"></div>
                                    <label class="col-sm-3 col-form-label font">パスワード</label>       
                                <div class="col-sm-6">
                                    <asp:TextBox ID="TB_Password" runat="server" name="password" CssClass="form-control font TextboxStyle" type="password" placeholder="8文字以上20文字以下" MaxLength="20"></asp:TextBox>
                                    </div>

                                <div class="col-sm-4"></div>
                                 <div class="col-sm-7 mb-2" style="height:1px;">
                                    <asp:Label ID="LB_Pwderror" runat="server" Text=" " CssClass="error"></asp:Label>
                                </div>
                                </div>

                            <div class="form-group row mt-3">
                                <div class="col-sm-1"></div>
                                    <label class="col-sm-3 col-form-label font">パスワードを再度入力</label>       
                                <div class="col-sm-6">
                                    <asp:TextBox ID="TB_Repassword" runat="server" name="password" CssClass="form-control font TextboxStyle" type="password" placeholder="8文字以上20文字以下" MaxLength="20"></asp:TextBox>
                                    </div>
                                <div class="col-sm-2"></div>
                                </div>

                            <div class="form-group row mt-4 mb-2 ">
                                <div class="d-flex justify-content-center">
                               <asp:Button ID="BT_Pwdhenkou" runat="server" Text="変更" type="button " CssClass="btn btn-primary font" style="width:100px;" OnClick="BT_Pwdhenkou_Click"/>
                               </div>
                            </div>

                            </div>
                    </div>
                    <div class="row">
                        <div class=" d-flex justify-content-center mb-3">
                        <a href="JC01Login.aspx" class="link-primary text-decoration-none font" runat="server">ログイン画面に戻る</a>

                        </div>
                    </div>
                    

                </div>
                 <div class="col-md col-xxl"></div>
            </div>
        </div>
    </form>
</body>
</html>
