<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="JC25_BukkenJyoutai.aspx.cs" Inherits="jobzcolud.WebFront.JC25_BukkenJyoutai" ValidateRequest ="False" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
   <%-- 20220211 エインドリ－・プ－プゥ Added --%>
     <link href="../Style/StyleJC.css" rel="stylesheet" />
    <link href="../Icons/font/bootstrap-icons.css" rel="stylesheet" />

     <link href="../Content/bootstrap.css" rel="stylesheet" />
    <asp:PlaceHolder runat="server">
        <%: Scripts.Render("~/bundles/modernizr") %>
        <%: Styles.Render("~/style/StyleBundle1") %>
        <%: Scripts.Render("~/scripts/ScriptBundle1") %>
    </asp:PlaceHolder>
    <webopt:BundleReference runat="server" Path="~/Content/bootstrap" />
    <script src="Scripts/jquery-3.3.1.js"></script>
    <script type="text/javascript">

        window.onload = function () {
            var intY = document.getElementById("GV_DIV").scrollTop;
            document.cookie = "yPos=?~" + intY + "~?";
            
    }
    function SetDivPosition() {
        var intY = document.getElementById("GV_DIV").scrollTop;
       document.cookie = "yPos=?~" + intY + "~?";
        }

        function SetScrollTop()
        {
            document.getElementById("GV_DIV").scrollTop = 0;
            var intY = document.getElementById("GV_DIV").scrollTop;
            document.cookie = "yPos=?~" + intY + "~?";
        }


        </script>
    <style>
       .gridViewRow td
        {
	        padding-bottom:20px;
        }
        .sonotapop {
             margin: 0px 3% 0px 5.5%;
             margin-left: 19px !important;
            }
        /*.divmargin {
            margin-top:3px;
        }*/
        .bfieldcss {
            display:none;
        }
        .EditedPopup {
            //margin-left: 15px;
        }
        .JC25_cancelbtn {
            height: 37px;
            text-align: center;
            border: none;
            font-size: 13px;
            border-radius: 6px;
            color: rgb(255, 255, 255);
            width: 120px;
            padding-left: 10px;
            padding-left: 10px;
        }
        .JCjoytaiAddButton:hover {
        background: rgb(209,205,205);
    }
        /*#Footer
{
    position: absolute;
    bottom: 0px;
    background-color: #666;
    color: #eee;
}*/
        .JCjoytaiAddButton {
    width: 70px;
    background: rgb(242, 242, 242);
    font-size: 13px;
     height: 37px;
    border-radius: 6px;
    border: none;
    margin-left: 10px;
}
        .JCjoytaiGrayButton {
    width: 70px;
    background: rgb(242, 242, 242);
    font-size: 13px;
    height: 37px;
    border-radius: 6px;
    border: none;
    margin-left:10px;
}

    .JCjoytaiGrayButton:hover{
        background: rgb(209,205,205);
    }
    </style>
</head>
<body class="font" style="background-color: #11ffee00;">
        <div class="J06Div RadiusIframe pl-4 pr-4 ">
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
        <div style="width: 100%; background-color: #fff;">
             <asp:UpdatePanel ID="updJyoutailist" runat="server" UpdateMode="Conditional" DefaultButton="btnHiddenSubmit">
                 <ContentTemplate>
                     <div class="row" style="display: flex; justify-content: center; align-items: center">
                            <asp:Label ID="lblHeader" runat="server" CssClass="shiaraititleStyle fw-bold txt-style" Text="物件状態" />
                            <asp:Button ID="btnHeaderCross" runat="server" Text="✕" CssClass="PopupCloseBtn mr-n3 JC08btnHeaderCross" OnClick="btnCancel_Click" />
                        </div>
                     <div class="titleLine"></div>
                       <%--20220428 Chaw Deleted Start--%>
                    <%-- <div class="row divmargin">
                            <div class="col-sm-12 text-center">

                                <asp:Button ID="btnJyoutailistNewPopup" runat="server" Width="140px" CssClass="JC10GrayButton mr-2" Text="✛ 物件状態を追加"
                                    OnClick="btnNewJoytai_Click" />
                            </div>
                        </div>--%>
                      <%--20220428 Chaw Deleted End--%>
                     <div id="GV_DIV" runat="server"  style=" overflow: auto; width: 437px;max-width:437px; max-height:369px !important;height:auto!important;overflow-x:hidden;" onscroll="SetDivPosition()"> 
                         <asp:GridView ID="gvJyotailist" runat="server" DataKeyNames="cJYOTAI" OnRowDataBound="OnRowDataBound"   OnRowEditing="OnRowEditing"
                                     onrowcommand="gvJyotailist_RowCommand" 　OnRowDeleting="OnRowDeleting"
                                     AutoGenerateColumns="false"  gridlines="None" CssClass="sonotapop"> 
                              <RowStyle Height="35px"  />
                             <Columns>
                                    <asp:BoundField DataField="cJYOTAI" ItemStyle-CssClass="bfieldcss"/>  
                                   <asp:TemplateField>
                                        <ItemTemplate>
                                            <div style="width:403px;">
                                            <asp:Panel ID="pnlBukkenJoutai" style="float:left;" runat="server">
                                            <asp:TextBox ID="txtsJYOTAI" CssClass="form-control TextboxStyle" runat="server" Text='<%# Bind("sJYOTAI") %>' Width="240px" MaxLength="50"></asp:TextBox> <%--20211119 Updated Eaindray+Phoo--%>
                                                </asp:Panel>
                                             
                                            <%--<asp:HoverMenuExtender ID="hmeKyotenListUpdate" runat="server" TargetControlID="txtsJYOTAI"
                                                PopupControlID="pnlUpdate" PopupPosition="Right">
                                            </asp:HoverMenuExtender>

                                            <asp:HoverMenuExtender ID="hmeBukkenJoutaiUpdate" runat="Server" TargetControlID="pnlBukkenJoutai" PopupControlID="PopupMenu"
                                                HoverCssClass="popupHover" PopupPosition="right"  />--%>

                                                  <%--20220428 Chaw Deleted Start--%>
                                            <%--<asp:Panel ID="PopupMenu" CssClass="popupMenu JCEditedPopup_1" style="padding:0px !important;padding-left:7px !important;float:left;" runat="server">
                                                 <asp:Button ID="lkbtnUpdate" runat="server" style="font-size:13px;" Text="更新" CssClass="JC10GrayButton" CommandName="Update" CommandArgument="<%# Container.DataItemIndex %>" OnClick="lkbtnUpdate_Click" />
                                                 <asp:Button ID="btnSaveCancel" runat="server" Text="キャンセル" CssClass="JC07HyojiItemSettingBtn" style="margin-left:3px;font-size:13px;" OnClick="btnSaveCancel_Click"/>
                                            </asp:Panel>--%>
                                                  <%--20220428 Chaw Deleted End--%>
                                                </div>
                                        </ItemTemplate>
                                       <ItemStyle Width="240px" />
                                    </asp:TemplateField>
                                   <asp:TemplateField>
                                       <ItemTemplate>
                                           <%--<asp:Panel ID="Panel_Joutai" runat="server"  Style="min-width:300px;max-width:300px;display:inline-block;">
                                            <asp:LinkButton ID="lkbtnKyotenMei" runat="server" Text='<%# Bind("sJYOTAI") %>' CommandName="Select" CommandArgument="<%# Container.DataItemIndex %>"></asp:LinkButton>
                                               </asp:Panel>--%>
                                           
<%--                                            <asp:HoverMenuExtender ID="hmeKyotenListEdit" runat="server" TargetControlID="Panel_Joutai"
                                                PopupControlID="pnlEdited" PopupPosition="Right" OffsetX="-30">
                                            </asp:HoverMenuExtender>--%> 
                                           <asp:Panel ID="Panel_Joutai" runat="server" BorderColor="Black" style="width:437px !important;padding-right:50px;height:35px;padding-top:5px;">
                                               <div style="width:350px !important;overflow: hidden; display: -webkit-box; -webkit-line-clamp: 1; -webkit-box-orient: vertical; word-break: break-all;">
                                            <asp:LinkButton ID="lkbtnKyotenMei" runat="server"  style="font-size:13px;" Text='<%# Bind("sJYOTAI") %>' CommandName="Select" CommandArgument="<%# Container.DataItemIndex %>"></asp:LinkButton>
                                                   </div>
                                            </asp:Panel>
                                          <%-- 20220428 Chaw delete start
                                            <asp:Panel ID="pnlEdited" runat="server" CssClass="PopupMenu JCEditedPopup" Style="display: none;    padding: 5px 10px 0px 10px;margin-left:-95px !important;">
                                                <asp:LinkButton ID="imgbtnCopy" runat="server" CssClass="btn-icons" CommandName="Edit" CommandArgument="<%# Container.DataItemIndex %>" >
                                                    <i class="bi bi-pencil-fill"></i>
                                                </asp:LinkButton>
                                                <asp:LinkButton ID="imgbtnDelete" runat="server" CssClass="btn-icons" CommandName="Delete" CommandArgument="<%# Container.DataItemIndex %>" OnClick="btnDelete_Click" ><%--OnClientClick="return ConfirmDelete(this);"
                                                    <i class="bi bi-trash-fill"></i>
                                                </asp:LinkButton>
                                                <%--<asp:ImageButton ID="imgbtnCopy" runat="server" CommandName="Edit" CommandArgument="<%# Container.DataItemIndex %>" ImageUrl="~/Images/draw.png" Height="14px" Width="14px" />
                                                <asp:ImageButton ID="imgbtnDelete" runat="server" CommandName="Delete" CommandArgument="<%# Container.DataItemIndex %>" ImageUrl="~/Images/trash.png" Height="14px" Width="14px" />--%>
                                              <%--  <asp:ConfirmButtonExtender ID="cbtneDelete" runat="server" ConfirmText="削除してもよろしいでしょうか？" TargetControlID="imgbtnDelete"  OnClientCancel="CancelClick" />--%>
                                            <%--</asp:Panel>
                                            <asp:HoverMenuExtender ID="hmeBukkenJoutaiListEdit" runat="server" TargetControlID="Panel_Joutai" 
                                                PopupControlID="pnlEdited" PopupPosition="Right">
                                            </asp:HoverMenuExtender>
                                            20220428 Chaw delete end--%>
                                             <asp:Button ID="BT_DeleteOk" runat="server" Text="はい" class="BlueBackgroundButton DisplayNone" Width="100px" Height="36px" OnClick="BT_DeleteOk_Click" />
                                             <asp:Button ID="BT_DeleteCancel" runat="server" Text="キャンセル" class="JC09GrayButton DisplayNone" Width="100px" Height="36px" />
                                        </ItemTemplate>
                                        <ItemStyle Width="437px" />
                                  </asp:TemplateField>
                               </Columns>
                               <%--<RowStyle CssClass="gridViewRow" />
                               <AlternatingRowStyle CssClass="gridViewRow" />--%>
                         </asp:GridView>
                     </div>
                  <%--   <div class="row" style="display:none;" runat="server" id="newjyotai">
                                <table>
                             <tr>
                                 <td>
                                     <asp:TextBox ID="txt_newJoytai" runat="server" Text='' Width="200px" CssClass="form-control TextboxStyle"></asp:TextBox>

                                 </td>
                                 <td style="margin-left:10px">
                                    <asp:Button ID="Button3" runat="server"   Text="保存" CssClass="JCjoytaiGrayButton"  OnClick="btnNewjoytaiSave_Click"/>  
                                 </td>
                             </tr>
                         </table>
                     </div>--%>
                     <div class="row" style="visibility:hidden;" runat="server" id="newjyotai"> <%--display: none;--%>
                            <table style="margin: 15px 0px 3px 0px;">
                                <tr>
                                    <td style="width:240px;">
                                        <asp:TextBox ID="txt_newJoytai" runat="server" Text='' Width="240px" CssClass="form-control TextboxStyle" MaxLength="50" ></asp:TextBox>

                                    </td>
                                    <td style="padding-left: 8px">
                                        <asp:Button ID="btnnewbumonSave" runat="server" Text="保存" CssClass="JC10GrayButton" OnClick="btnNewjoytaiSave_Click" />
                                         <asp:Button ID="btnnewJoutaiCancel" runat="server" Text="キャンセル" CssClass="JC07HyojiItemSettingBtn" style="margin-left:3px;" OnClick="btnnewJoutaiCancel_Click" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                      <div  id="Footer" style="display: block; width:100%; background-color: white;">
                    <div style="display: flex; justify-content: center; align-items: center; background-color:#eee; width: 100%; padding: 20px 0">
                   <asp:Button ID="btnKyotenlistCancel" runat="server" Text="キャンセル" CssClass="JC25_cancelbtn" 
                       Style="color: rgba(0,113,188,1);background-color:white;border:1px solid rgba(0,113,188,1);" OnClick="btnCancel_Click"/>  
              </div>
                 </ContentTemplate>
             </asp:UpdatePanel>
        </div>
         <asp:HiddenField ID="hdnHome" runat="server" />
    </form>
            </div>
</body>
    <script>
                                 Sys.Application.add_load(function () {

                                     var strCook = document.cookie;
                                     if (strCook.indexOf("?~") != 0) {
         var intS = strCook.indexOf("?~");
         var intE = strCook.indexOf("~?");
         var strPos = strCook.substring(intS + 2, intE);
         document.getElementById("GV_DIV").scrollTop = strPos;
            }
    });
                          </script>
</html>
