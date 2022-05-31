<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="JC32ArarisuIkkatsuSetting.aspx.cs" Inherits="jobzcolud.WebFront.JC32ArarisuIkkatsuSetting" ValidateRequest ="False" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
      <asp:PlaceHolder runat="server">
        <%: Scripts.Render("~/bundles/modernizr") %>
        <%: Styles.Render("~/style/StyleBundle1") %>
        <%: Scripts.Render("~/scripts/ScriptBundle1") %>
    </asp:PlaceHolder>
    <webopt:BundleReference runat="server" Path="~/Content/bootstrap" />
    <link href="../Content/bootstrap.min.css" rel="stylesheet" />
    <style>
       
         .btn {
    background: rgb(255, 255, 255);
    font-size: 13px;
    border: 1px solid rgb(166, 166, 166);
    border-radius:0px;
}

.btnActive {
    background: rgb(217, 217, 217);
    font-size: 13px;
    border: 1px solid rgb(166, 166, 166);
    border-radius:0px;
}

/*.btn:hover {
    background: rgb(209,205,205);
}*/

.BlueBorderButton
{
    //line-height: 30px;
    text-align: center;
    border: solid 1px rgb(91, 155, 213);
    font-size: 13px;
    border-radius: 3px;
    color: rgb(91, 155, 213);
    font-weight:bold;
    margin-left:10px;
    padding:6px 12px 6px 12px;
    letter-spacing:1px;
    width:auto !important;
    height:35px !important;
    
}
    </style>
    <script type="text/javascript">
        var validRituNumber = new RegExp(/^[0-9]{0,2}$/);
        var lastValid_Ritu = document.getElementsByClassName("txtRitu");

    function validateRitu(elem) {
            if (validRituNumber.test(elem.value.replaceAll(',', '')))
            {
                lastValid_Ritu = elem.value;
            }
            else
            {
                if (lastValid_Ritu == undefined)
                {
                    lastValid_Ritu = "";
                }
                elem.value = lastValid_Ritu;
            }
        }

       function RefreshIt() {
      __doPostBack('<%= tb_nRIIRITSU.ClientID %>', '');
    }
    </script>
</head>
<body>
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
        <asp:UpdatePanel ID="updBody" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
        <div style="width:100%;height:50vh;">
            <%--<div style="height:50px;border-bottom-style:solid;border-bottom-width:7px;border-bottom-color:LightGrey;padding-left:20px;">
            <asp:Label ID="lbl_arariritsu_settei" runat="server">粗利率一括設定</asp:Label>
                
                </div>--%>
              <div class="row">
                    <div class="col-sm-12">
                            <asp:Label ID="lblHeader" runat="server" Text="粗利率一括設定" CssClass="TitleLabel d-block"></asp:Label>
                             <asp:Button ID="btnHeaderCross" runat="server" Text="✕" CssClass="PopupCloseBtn mr-n3" OnClick="btn_cancel_Click" />
                    </div>
                    <div class="Borderline"></div>
              </div>
            <div style="height:300px;padding-left:20px;">
                 <asp:Label ID="Label1" runat="server" width="100px" Height="50px">粗利率</asp:Label>
                 <asp:TextBox ID="tb_nRIIRITSU" runat="server" width="50px" Text="50" Height="30px" MaxLength="2" CssClass="TextboxStyle txtRitu" style="border-radius:5px; border-style:solid;border-width:1px;text-align:right;" AutoPostBack="True" onkeyup="javascript:RefreshIt();" onfocus="this.setSelectionRange(this.value.length, this.value.length);" oninput="validateRitu(this);" OnTextChanged="tb_nRIIRITSU_TextChanged" AutoCompleteType="Disabled"></asp:TextBox>
                        <asp:Label ID="Label4" runat="server">%</asp:Label>
           
           <br />
            <asp:Label ID="Label2" runat="server" width="100px" Height="50px">適用範囲</asp:Label>

                <div class="btn-group" role="group" >
                                           
                                             <asp:Button ID="bt_gyou" runat="server" Text="選択行" type="button " CssClass="btnActive" OnClick="bt_gyou_Click" Width="75px" />
                                             <asp:Button ID="bt_zengyou" runat="server" Text="全行" type="button " CssClass="btn" OnClick="bt_zengyou_Click" Width="75px"/>
                                         </div>

            <br />
                <div  style="margin-left:105px">
            <asp:CheckBox ID="CK_Meisai" runat="server" Text="明細金額上書き" width="150px" Height="30px" Checked="true"/>
            <asp:CheckBox ID="CK_Syosai" runat="server" Text="詳細金額上書き" width="150px" Height="30px" Checked="true"/>
                    </div>
            <br />
            <asp:Label ID="Label3" runat="server"  width="100px" Height="50px" >端数処理</asp:Label>
                <div class="btn-group" role="group" >
                                           
                                             <asp:Button ID="bt_shishagonyuu" runat="server" Text="四捨五入" type="button " CssClass="btnActive" Width="75px" OnClick="bt_shishagonyuu_Click" />
                                                    
                                             <asp:Button ID="bt_kiriage" runat="server" Text="切り上げ" type="button " CssClass="btn" Width="75px" OnClick="bt_kiriage_Click"/>
                                             <asp:Button ID="bt_kirisute" runat="server" Text="切り捨て" type="button " CssClass="btn" Width="75px" OnClick="bt_kirisute_Click"/>
                                         </div>
         
            <br />
 <div class="btn-group" role="group"   style="margin-left:105px">
                                           
                                             <asp:Button ID="bt_1" runat="server" Text="1" type="button " CssClass="btnActive" Width="30px" OnClick="bt_1_Click" />
                                             <asp:Button ID="bt_10" runat="server" Text="10" type="button " CssClass="btn" Width="40px" OnClick="bt_10_Click"/>
                                             <asp:Button ID="bt_100" runat="server" Text="100" type="button " CssClass="btn" Width="60px" OnClick="bt_100_Click"/>
                                             <asp:Button ID="bt_1000" runat="server" Text="1000" type="button " CssClass="btn" Width="70px" OnClick="bt_1000_Click" />
                                         </div>
           <br />
                </div>
           
            <div style="height:75px; background-color:LightGrey;display:flex;justify-content:center;align-items:center;" align="center">
            <asp:Button ID="btn_ok" runat="server" Text="OK"  CssClass="BlueBackgroundButton" OnClick="btn_ok_Click"/>
            <asp:Button ID="btn_cancel" runat="server" Text="キャンセル" CssClass="BlueBorderButton" OnClick="btn_cancel_Click" Height="35px" />
                </div>
        </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        <asp:HiddenField ID="hdnHome" runat="server" />
    </form>
   </body>
</html>
