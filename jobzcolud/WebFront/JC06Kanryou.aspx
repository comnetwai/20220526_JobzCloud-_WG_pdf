<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="JC06Kanryou.aspx.cs" Inherits="jobzcolud.WebFront.JC06Kanryou" %>

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
                    <div class="kanryou-form  bg-white mt-4 mb-3 p-4 shadow-sm" style="border-radius:10px; height:550px;" >
                        <div class="row justify-content-center align-content-center mt-3">
                            <asp:Image ID="imgLogo" CssClass="logo" runat="server" ImageUrl="~/Images/JOBZ_CLOUD_ロゴ.jpg" />
                        </div>
                        <div id="Div_pwd" runat="server">
                            <div class="text-center mt-5 mb-4">
                             <asp:Label ID="Label2" runat="server" Text="パスワーの再設定が完了しました。" class="font fw-bold" ></asp:Label><br />                  
                            </div>
                            <div class="text-center mt-2 mb-4">
                                 <asp:Label ID="Label5" runat="server" Text="下記のリンクより見えるJOBZをログインしてください" class="font" ></asp:Label><br />
                            </div>
                            <div class=" d-flex justify-content-center mt-2 mb-3">
                                <a href="JC01Login.aspx" class="link-primary font" runat="server">見えるJOBZのトップページへ</a>

                            </div>
                            </div>

                        </div>
                </div>
                 <div class="col-md col-xxl"></div>
            </div>
        </div>
    </form>
</body>
</html>
