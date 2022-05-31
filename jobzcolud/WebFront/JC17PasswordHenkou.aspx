<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="JC17PasswordHenkou.aspx.cs" Inherits="JobzCloud.WebFront.JC17PasswordHenkou" ValidateRequest ="False" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
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
    <webopt:BundleReference runat="server" Path="~/Content1/bootstrap" />   <%--//20220314 Added by phoo－--%>
    <webopt:BundleReference runat="server" Path="~/Content1/css" />
    <link href="../Content1/bootstrap.min.css" rel="stylesheet" />
    <script src="../Scripts/bootstrap.bundle.min.js"></script>
     <script src="../resources/js/jquery.min.js"></script>
    <style type="text/css">
        .font{
            /*font-family:'Bookman Old Style';
            font-size:15px;*/
             font-family: "Hiragino Sans", "Open Sans", "Meiryo", "Hiragino Kaku Gothic Pro", "メイリオ", "MS ゴシック", "sans-serif";
            font-size: 13px;  /*20220315 Updated Phoo*/
        }
          /* Rounded border */
        hr.rounded {
        border-top: 8px solid #bbb;
        border-radius: 5px;
          /*white-space: nowrap;
                overflow: hidden;*/
        }
         .JC17ButtonDisable{     <%--//20220314 Added by phoo－--%>
             background: rgb(242, 242, 242);
    font-size: 13px;
     color:darkgray; /*20220315 Added　Phoo*/
    border-radius: 3px;
    margin-right: 13px;
    margin-top: 0px;
    border: none;
    padding: 6px 12px 6px 12px;
    letter-spacing: 1px;
   cursor:default !important;
   pointer-events:none;
         }
        /*20220317 Added エインドリ－*/
         .CrossBtnDisable{
              background-color: white;
    padding: 0px;
    border: none;
    float: right;
    width: 53px;
    font-size: 21px;
    height: 48px;
    line-height: 26px;
    color: rgb(75,75,75) !important;
    margin-top: -56px;
    margin-right: 10px;
      cursor:default !important;
   pointer-events:none;
         }
      
        </style>
    
</head>

<body style="background-color:#FFFFFF;">
      <form id="Frmpasswordhenkou" runat="server">
         <asp:ScriptManager ID="ScriptManager2" EnablePageMethods="True" runat="server"> <%--20220314--%>
            <Scripts>
                <asp:ScriptReference Name="jquery" />
                <asp:ScriptReference Name="bootstrap" />
                <asp:ScriptReference Path="../Scripts/Common/FixFocus.js" />
            </Scripts>
  </asp:ScriptManager>
             <asp:UpdatePanel ID="updHeader" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
          <div class="container" style="margin-left:65px;margin-right:0px;min-width:630px !important;">
                <label class=" col-form-label font-weight-bold" style="margin-top:15px;">パスワード変更</label>
               <asp:Button ID="btn_cross" runat="server" Text="✕" CssClass="PopupCloseBtn" style="margin-top:3px;" OnClick="BtnCancel_Click"/> <%--20220317 update btn name--%>
        </div>
             <%--<hr class="rounded">--%>
          <div class="Borderline"></div>
         <%--<div class="container bg-white" style="margin:65px;margin-bottom:98px; width: auto;!important">--%>
         <div class="container font bg-white" style="margin:25px 65px 5px 65px; width: auto;!important;"> <%--//20220314 Added by phoo－--%>
             <table runat="server">
                 <tr style="height:60px;">
                     <td style="min-width:180px;">
                          <label style="font-size:14px;">現在のパスワード</label>
                     </td>
                     <td>
                         <asp:TextBox ID="txtCurrentPwd" runat="server" type="password" name="password" MaxLength="20" CssClass="form-control TextboxStyle" Height="35px" Width="400px"></asp:TextBox>
                         <asp:Label ID="LB_CurPwd_Error" runat="server" Text=" " ClientID="LB_CurPwd_Error" CssClass="error"></asp:Label>
                     </td>
                 </tr>
                 <tr style="height:60px;">
                     <td>
                         <label style="font-size:14px;">新しいパスワード</label>
                     </td>
                     <td>
                         <asp:TextBox ID="txtNewPwd" runat="server" type="password" name="password" MaxLength="20" CssClass="form-control TextboxStyle" Height="35px" Width="400px"></asp:TextBox>
                         <asp:Label ID="LB_NewPwd_Error" runat="server" Text=" " ClientID="LB_NewPwd_Error" CssClass="error"></asp:Label>
                     </td>
                 </tr>
                 <tr style="height:60px;">
                     <td>
                         <label style="font-size:14px;">新しいパスワード（確認）</label>
                     </td>
                     <td>
                         <asp:TextBox ID="txtConfirmPwd" runat="server" type="password" name="password"  MaxLength="20" CssClass="form-control TextboxStyle"  Height="35px" Width="400px"></asp:TextBox>
                         <asp:Label ID="LB_ConPwd_Error" runat="server" Text=" " ClientID="LB_ConPwd_Error" CssClass="error"></asp:Label>
                     </td>
                 </tr>
             </table>
           </div>
          <br />
                <%--<div style ="Height :70px;background :#d4d4d4; " align="center" >--%>
                <div style ="Height :70px;background-color :#eee; " align="center" > <%--20220315 Updated Phoo--%>
                       <div style="margin-left: 20px; margin-right: 20px;">
                      <div style="overflow-x:auto;width:100%  !important;">  <%--20220309 Added by phoo－--%>
              <div class="d-flex flex-column" >
                               <div style="width: auto; overflow-x: auto; margin-left: 20px; margin-right: 20px;"> <%--20220309 Added by phoo－--%>
                    <table>
                        <tr>
                            <td>
                                 <%--<asp:Button ID="btnhozon" runat="server" Text="保存" type="JC17button" CssClass="BlueBackgroundButton" style ="margin-right:15px; !important;  margin-top: 17px;border-radius:3px;padding:6px 12px 6px 12px;letter-spacing:1px;"  OnClick="btnhozon_Click" />  <%--20220309 Added by phoo－--%>
                                 <%--OnClientClick="javascript:disabledTextChange(this);"--%>

                                  <%--<asp:Button ID="btnhozon" runat="server" Text="保存" type="JC17button" CssClass="BlueBackgroundButton JC10SaveBtn" style ="margin-right:15px; !important;  margin-top: 17px;border-radius:3px;padding:6px 12px 6px 12px;letter-spacing:1px;"  
                                      OnClick="btnhozon_Click" />--%>  <%--20220309 Added by phoo－--%>
                                <asp:Button ID="btnhozon" runat="server" Text="保存" type="JC17button" CssClass="BlueBackgroundButton JC10SaveBtn" style ="margin-right:15px; !important;  margin-top: 17px;border-radius:3px;letter-spacing:1px;"  
                                      OnClick="btnhozon_Click" /><%-- 20220315 Updated Phoo--%>
                            </td>
                            <td>
                                <asp:Button ID="BtnCancel" runat="server"    Text="キャンセル" type="JC17button " CssClass="btn text-primary font btn-sm btn btn-outline-primary " style ="width: auto !important; background-color:white;  margin-top: 17px;border-radius:3px;padding:6px 12px 6px 12px;letter-spacing:1px;font-size:13px;height:35px !important;max-height:35px !important;" OnClick="BtnCancel_Click"  /> <%--20220309 Added by phoo－--%>
                            </td>
                        </tr>
                    </table>
 </div>
                   <asp:HiddenField ID="hdnHome" runat="server" />
                     <asp:HiddenField ID="hdnhenkou" runat="server" />
                  </ContentTemplate>
    </asp:UpdatePanel>

                   <script type="text/javascript"> <%--//20220314 Added by phoo－--%>
        Sys.Application.add_load(function () {
            $("#<%= btnhozon.ClientID %>").click(function () {
                document.getElementById("<%=btnhozon.ClientID %>").className = "JC17ButtonDisable";
                document.getElementById("<%=BtnCancel.ClientID %>").className = "JC17ButtonDisable"; /*20220317 Added エインドリ－*/
                document.getElementById("<%=btn_cross.ClientID %>").className = "CrossBtnDisable"; /*20220317 Added エインドリ－*/
                document.getElementById("<%=BtnCancel.ClientID %>").style.backgroundColor = " rgb(242, 242, 242)"; /*20220317 Added エインドリ－*/
                 setTimeout(30 * 6000);
                $('.btnhozon').prop("enabled", false);
                $('.BtnCancel').prop("enabled", false);/*20220317 Added エインドリ－*/
                $('.btn_cross').prop("enabled", false);/*20220317 Added エインドリ－*/
             });
            
    });

    </script>
                   
       
                  <%--<asp:HiddenField ID="HF_Url" runat="server" />--%>
<%--          function ClosePopWindow() {
     var parentButtonClick = window.parent;
     window.parent.PopWindow.Hide();
    }
         
          function ClearPopWindow() {
        var paentWin = window.parent;
        paentWin.PopWindow.SetContentHtml(null);            
   }--%>

            
          <%--<script type="text/javascript">
    function Closepopup() {
        debugger;
        $('#myModal').modal('close');
    }
</script>--%>
           
</div>
       </div>     
        <%--   <div class="container-fluid">
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
                                <asp:Label ID="LB_message1" runat="server" Text="パスワードを正しく変更されました。" class="LoginFont fw-bold" ></asp:Label>
                                </div>
                                <asp:Label ID="LB_message2" runat="server" Text="再度ログインする" CssClass=" font"></asp:Label><br />
                               
                            </div>
                            <div class="col"></div>
                        </div>
                    </div>
                </div>
                <div class="col-md col-xxl">
                </div>--%>
            </div>

        </div>
          
    </form>
</body>
</html>
