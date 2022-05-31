<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="JC03Mail.aspx.cs" Inherits="jobzcolud.JC03Mail" %>

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
<body style="background-color:#d7e4f2;">
    <form id="form1" runat="server">
        <div class="container-fluid">
            <div class="row align-items-center" style="height:100vh;">
                <div class="col-md col-xxl"></div>
                <div class="col-md-7 col-xxl-5">
                    <div class="mail-form bg-white mt-4 mb-4 p-4 shadow-sm " style="border-radius:10px; height:550px;">

                        <div class="row justify-content-center align-content-center mt-3">
                            <asp:Image ID="imgLogo" CssClass="logo" runat="server" ImageUrl="~/Images/JOBZ_CLOUD_ロゴ.jpg" />
                        </div>
                       
                        <div class="row">
                            <div class="col"></div>
                            <div class="col-6">
                                <div class="text-center mt-5 mb-3">
                                <asp:Label ID="LB_message1" runat="server" Text="まだ、本登録できておりません。" class="LoginFont fw-bold" ></asp:Label>
                                </div>
                               <asp:Label ID="LB_message2" runat="server" Text="入力されたメールアドレスに認証リンクを送付しました。" CssClass=" font"></asp:Label><br />
                                <asp:Label ID="LB_message3" runat="server" Text="  メールアプリにてご確認の上、リンクをクリックして認証を行ってください。" CssClass=" font"></asp:Label>
                                <%--20220310 Added　エインドリ－ Start--%>
                                <div class="text-center">
                                     <asp:Label ID="LB_message4" runat="server" Text="" CssClass=" font"></asp:Label><br />
                                    <asp:Label ID="LB_message5" runat="server" Text=" " CssClass=" font"></asp:Label>
                                </div>
                                <%--20220310 Added　エインドリ－ End--%>
                            </div>
                            <div class="col"></div>
                        </div>
                    </div>
                </div>
                <div class="col-md col-xxl">
                </div>
            </div>

        </div>
    </form>
</body>
</html>
