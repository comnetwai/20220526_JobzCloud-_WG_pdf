<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="JC01Login.aspx.cs" Inherits="jobzcolud.JC01Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    
    <link href="../Content/bootstrap.min.css" rel="stylesheet" />
    <script src="../Scripts/bootstrap.bundle.min.js"></script>
    <title>
        JOBZCloud
    </title>
    <asp:PlaceHolder runat="server">
        <%: Styles.Render("~/style/AccCreate") %>
    </asp:PlaceHolder>
    <script type="text/javascript">
        function errorLabelClear()
        {
            LB_Mail_Error.innerText = "";
            LB_Pass_Error.innerText = "";

        }
        function RestrictEnter(event) {
            if (event.keyCode === 13) {
                event.preventDefault();
            }
        }
        </script>
</head>
<body>
    <form id="form1" runat="server" >
        <div class="container-fluid">
           <div class="row align-items-center" style="height:100vh;">
            <div class="col-md-6 offset-md-3 col-xxl-4 offset-xxl-4 justify-content-center align-content-center JC01Login">
                <div class="login-form bg-white p-4 shadow-sm" style="border-radius:10px;">
                        <div class="row justify-content-center align-content-center mt-3">
                            <asp:Image ID="imgLogo" CssClass="logo" runat="server" ImageUrl="~/Images/JOBZ_CLOUD_ロゴ.jpg" />
                        </div>

                        <div class="form-group row"style="margin-top:75px;">
                            <label class="col-xl-2 offset-xl-1 col-form-label LoginFont ">ログインID</label>
                           <div class="col-xl-7">
                               <asp:TextBox ID="TB_id" runat="server" name="username" CssClass="form-control LoginFont input LoginTextboxStyle" placeholder="メールアドレスを入力" MaxLength="100" onkeyup="errorLabelClear();"></asp:TextBox>
                            </div>
                       
                            <div class="col-xl-7 offset-xl-3 mb-1" style="height:1px;">
                                <asp:Label ID="LB_Mail_Error" runat="server" Text=" " CssClass="error"></asp:Label>
                            </div>
                        </div>

                        <div class="form-group row mt-4">
                            <label class="col-xl-2 offset-xl-1 col-form-label LoginFont">パスワード</label>
                           <div class="col-xl-7">
                               <asp:TextBox ID="TB_Pass" runat="server" type="password" name="password" CssClass="form-control LoginFont input LoginTextboxStyle" MaxLength="20" placeholder="パスワード" onkeyup="errorLabelClear();"></asp:TextBox>
                            </div>     
                            <div class="col-xl-7 offset-xl-3 mb-1" style="height:1px;">
                                <asp:Label ID="LB_Pass_Error" runat="server" Text=" " CssClass="error"></asp:Label>
                            </div>
                         </div>
                        
                        <div class="form-group row mt-4 text-center">
                           <div class="col-xl-7 offset-xl-3">
                           <asp:Button ID="BT_login" runat="server" Text="ログイン" type="button " CssClass="btn btn-primary col-sm-12 LoginFont" OnClick="BT_login_Click"/>
                           </div>
                        </div>
                        
                    <div class="form-group row mt-4 ms-2">
                           <div class="col-xl-6 offset-xl-3 text-center" >
                            <%--<a href="#" class="link-primary text-decoration-none font">ユーザーID</a>--%>
                               <%--<span> / </span>--%>
                            <a href="JC05Passwordreset.aspx" class="link-primary text-decoration-none LoginFont">パスワードをお忘れの方はこちら</a>
                           </div>
                        </div>

            </div>
                <div class="form-group row p-4 text-center">
                           <div class="col-xl-7 offset-xl-3">
                           <asp:Button ID="Button1" runat="server" Text="アカウント作成" type="button " CssClass="btn btn-secondary col-sm-12 LoginFont" style="background-color:#E67E22; border-color:#E67E22;" OnClick="BT_create_Click"/>
                           </div>
                </div>
           </div>
            </div>
        </div>
    </form>
    <p>
        c</p>
</body>
</html>
