<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="JC35PdfPageChoice.aspx.cs" Inherits="jobzcolud.WebFront.JC35PdfPageChoice" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <asp:PlaceHolder runat="server">
            <%: Scripts.Render("~/bundles/modernizr") %>
            <%: Styles.Render("~/style/StyleBundle1") %>
            <%: Scripts.Render("~/scripts/ScriptBundle1") %>
            <%: Styles.Render("~/style/UCStyleBundle") %>

        </asp:PlaceHolder>
     <link href="../Content1/bootstrap.min.css" rel="stylesheet" />
    
</head>
<body class="bg-transparent">
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" EnablePageMethods="True" runat="server">
            <Scripts>
                <asp:ScriptReference Name="jquery" />
                <asp:ScriptReference Name="bootstrap" />
                 <asp:ScriptReference Path="../Scripts/Common/FixFocus.js" />
            </Scripts>
        </asp:ScriptManager>
        <asp:PlaceHolder runat="server">
            <%: Scripts.Render("~/bundles/jqueryui") %>
        </asp:PlaceHolder>
        <div style="min-width:350px;min-height:100px;background-color:white;margin-left:auto;margin-right:auto;padding:0; left: 50%; top       : 50%;  position  : absolute;   transform : translate(-50%, -50%);"">
            <asp:UpdatePanel ID="updJyoutailist" runat="server" UpdateMode="Conditional" DefaultButton="btnHiddenSubmit">
                 <ContentTemplate>
            <table style="margin:20px;">
                <tr>
                    <td style="height:25px;padding-bottom:10px;" colspan="2">
                        <asp:Label ID="label" runat="server" Text="PDFファイルのページを指定する"></asp:Label>
                     </td>
                    <td>

                    </td>
                </tr>
                <tr>
                     <td style="min-width:200px;min-width:200px;">
                    <asp:TextBox ID="txtPageNumber" runat="server" AutoPostBack="false" MaxLength="100" CssClass="form-control TextboxStyle" Width="200px" Height="28px" style="text-align:right;" onkeypress="return isNumberKey()"
                                                            onkeyup="DeSelectText(this);" onfocus="this.setSelectionRange(this.value.length, this.value.length);"></asp:TextBox> 
                         </td>
                    <td style="min-width:50px;min-width:50px;padding-bottom:3px;">
                    <asp:Label ID="LB_pageCount" runat="server" Text="/5" style="margin-left:5px;margin-right:5px;"></asp:Label>
                </td>
                    <td>
                        <asp:Button runat="server" ID="BT_Close"  Text="閉じる"　CssClass="JC10GrayButton" onmousedown="getTantouBoardScrollPosition();" OnClick="BT_Close_Click"/>
                    </td>
                </tr>
               
                </table>
                      </ContentTemplate>
             </asp:UpdatePanel>
        </div>
        <asp:HiddenField ID="hdnHome" runat="server" />
         <asp:HiddenField ID="HF_pageCount" runat="server" />
    </form>
</body>
</html>
