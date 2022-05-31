<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="JC04Mailkakunin.aspx.cs" Inherits="jobzcolud.JC04Mailkakunin" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <link href="../Content/bootstrap.min.css" rel="stylesheet" />
    <script src="../Scripts/bootstrap.bundle.min.js"></script>
    <script src="http://code.jquery.com/jquery-1.11.0.min.js"></script>
    <title></title>
     <asp:PlaceHolder runat="server">
        <%: Styles.Render("~/style/AccCreate") %>
     </asp:PlaceHolder>

</head>
<body>
    <form id="form1" runat="server">
        <div class="container-fluid">
            <div class="row align-items-center" style="height:100vh;">
                <div class="col-md col-xxl">
                </div>
                <div class="col-md-7 col-xxl-5">
                    <div class="mailkakunin-form bg-white mt-4 mb-4 p-4 shadow-sm " style="border-radius:10px; height:550px;">

                        <div class="row justify-content-center align-content-center mt-3">
                            <asp:Image ID="imgLogo" CssClass="logo" runat="server" ImageUrl="~/Images/JOBZ_CLOUD_ロゴ.jpg" />
                        </div>
                        <div id="Div_ok" runat="server">
                            <div class="row">
                            <div class="col"></div>
                            <div class="col-7">
                                <div class="mt-5 mb-4">
                                    <asp:Label ID="Label1" runat="server" Text="正常に認証できました。ご登録ありがとうございます。" class="titlefont ms-4" ></asp:Label>
                                </div>
                                <div class="mb-4">
                                    <asp:Label ID="Label2" runat="server" Text="下記のリンクより見えるJOBZをご利用ください。" CssClass=" font ms-4"></asp:Label><br />
                                </div>
                                <div class="text-center">
                                    <a href="JC01Login.aspx" class="link-primary font" runat="server">見えるJOBZをログインする</a>
                                </div>
                            </div>
                            <div class="col"></div>
                        </div>
                        </div>
                        
                        <div id="Div_error" runat="server">
                            <div class="text-center mt-5 mb-4">
                                <asp:Label ID="LB_message1" runat="server" Text="認証失敗しました。" class="titlefont" ></asp:Label>
                            </div>
                            <div class="text-center mb-4">
                                <asp:Label ID="LB_message2" runat="server" Text="お手数ですが、下記のより最初からやり直してください。" CssClass=" font"></asp:Label><br />
                            </div>
                            <div class="text-center">
                                <a href="JC02Touroku.aspx" class="link-primary font" runat="server">アカウント作成</a>
                            </div>
                        </div>
                        
                        <div class="col"></div>

                        <div id="Div_check_error" runat="server">
                            <div class="text-center mt-5 mb-4">
                                <asp:Label ID="Label3" runat="server" Text="申し訳ございませんが、認証できませんでした。" class="titlefont" ></asp:Label>
                            </div>
                        </div>


                        <div class="col"></div>

                        <div>                            
                            <asp:Button ID="BT_Submit" runat="server" Text="Load Customers" style="display :none" OnClick="BT_Submit_Click"/>
                        </div>
                        <div id="Div_load" runat="server" align="center" >
                            <div class="text-center mt-5 mb-4">
                                <asp:Label ID="LB_Wait" runat="server" Text="しばらくお待ちください。" class="titlefont" ></asp:Label>
                            </div>
                            <div class="col"></div>
                            <img src="../Images/Loading.gif" />
                        </div>
                       <%-- 20220310 Added エインドリ Start－--%>
                         <div id="Div_koujiMailkaku" runat="server">
                             <div class="text-center mt-5 mb-4">
                                <asp:Label ID="LB_Msg" runat="server" Text="正しく認証できました。新しいメールアドレスで" class="titlefont" ><a href="JC01Login.aspx?id=true" class="link-primary font" runat="server">再度ログインする</a></asp:Label>
                            </div>
                        </div>
                         <%-- 20220310 Added エインドリ End－--%>
                         <%-- 20220315 Added Phoo Start－--%>
                         <div id="Div_passwordHenkouMailkaku" runat="server">
                             <div class="text-center mt-5 mb-4">
                                <asp:Label ID="LB_Msg_Login" runat="server" Text="パスワードを正しく変更されました。" class="titlefont" ><a href="JC01Login.aspx?id=true" class="link-primary font" runat="server">再度ログインする</a></asp:Label>
                            </div>
                        </div>
                         <%-- 20220315 Added Phoo End－--%>
                    </div>
                
                </div>
                <div class="col-md col-xxl">
                </div>
            </div>
            </div>

    </form>
</body>
</html>
