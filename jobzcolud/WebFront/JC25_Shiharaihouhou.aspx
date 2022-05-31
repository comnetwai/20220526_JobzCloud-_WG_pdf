<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="JC25_Shiharaihouhou.aspx.cs" Inherits="jobzcolud.WebFront.JC25_Shiharaihouhou"  ValidateRequest="false"  %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="../Content/bootstrap.css" rel="stylesheet" />

    <link href="../Style/StyleJC.css" rel="stylesheet" />
    <link href="../Icons/font/bootstrap-icons.css" rel="stylesheet" />
    <%--<link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap-icons@1.8.0/font/bootstrap-icons.css">--%>

    <script src="../Scripts/jquery-3.5.1.js"></script>        
    <asp:PlaceHolder runat="server">
        <%: Scripts.Render("~/bundles/modernizr") %>
        <%: Styles.Render("~/style/StyleBundle1") %>
        <%: Scripts.Render("~/scripts/ScriptBundle1") %>
    </asp:PlaceHolder>
    <webopt:BundleReference runat="server" Path="~/Content/bootstrap" />   

  
</head>
<body class="fontcss" style="background-color: #11ffee00;">
    <div class="J06Div RadiusIframe pl-4 pr-4 ">
        <form id="form1" runat="server" >

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

            
            <div style="width: 100%;  background-color: #fff"><%-- height: 50vh;--%>
                <asp:UpdatePanel ID="updShiharailist" runat="server" UpdateMode="Conditional" DefaultButton="btnHiddenSubmit" >
                    <ContentTemplate>
                       
                        <asp:Button ID="btnHiddenSubmit" runat="server" CssClass="DefaultBtn" />
                        <%--<div >--%>
                             <div class="row" style="display: flex; justify-content: center; align-items: center">
                            <asp:Label ID="lblHeader" runat="server" CssClass="shiaraititleStyle fw-bold txt-style" Text="支払方法" />
                            <asp:Button ID="btnHeaderCross" runat="server" Text="✕" CssClass="PopupCloseBtn mr-n3 JC08btnHeaderCross" OnClick="btnCancel_Click" />
                        </div>
                        <div class="titleLine"></div>
                        
                        <div class="row divmargin">
                            <div class="col-sm-12 text-center">

                                <asp:Button ID="btnShiharailistNewPopup" runat="server" Width="140px" CssClass="JC10GrayButton mr-2" Text="✛ 支払方法を追加"
                                    OnClick="btnnewshiarai_Click" />
                            </div>
                        </div>
                        <%--<div id="Gvdiv" runat="server" style=" overflow: auto; width: 450px;margin-bottom: 3px;margin-top:5px;">--%>
                             <div id="Gvdiv" runat="server" style="overflow: auto; width: 450px;max-height:379px !important;height:auto!important; margin-bottom: 3px;margin-top: 5px;overflow-x:hidden !important;"> <%--20220405--%>
                            <asp:GridView ID="gvshiarailist" runat="server" DataKeyNames="cSHIHARAI" OnRowDataBound="OnRowDataBound" OnRowEditing="OnRowEditing"
                                OnRowCommand="gvshiarailist_RowCommand" OnRowDeleting="OnRowDeleting"
                                AutoGenerateColumns="false"  GridLines="None" ShowHeader="False"  CssClass="sonotapop">
                                <RowStyle Height="35px"  /><%----%>
                                <Columns>
                                    <asp:BoundField DataField="cSHIHARAI" ItemStyle-CssClass="bfieldcss" />
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                             <%--<asp:Panel ID="pnlShiharai" runat="server">
                                              <asp:TextBox ID="txtsSHIHARAI" runat="server" Text='<%# Bind("sSHIHARAI") %>' Width="240px" CssClass="form-control TextboxStyle"></asp:TextBox>
                                             </asp:Panel>
                                            <asp:HoverMenuExtender ID="hmeKyotenListUpdate" runat="server" TargetControlID="pnlShiharai" PopupControlID="pnlUpdate" 
                                             HoverCssClass="popupHover" PopupPosition="right" PopDelay="0">
                                            </asp:HoverMenuExtender>
                                            <asp:Panel ID="pnlUpdate" runat="server" CssClass="popupMenu JCEditedPopup_1">                                             
                                                <asp:Button ID="btnUpdate" runat="server" style="font-size:13px;" Text="更新" CssClass="JC10GrayButton" OnClick="btnUpdate_Click" />
                                                 <asp:Button ID="btnSaveCancel" runat="server" Text="キャンセル" CssClass="JC07HyojiItemSettingBtn" style="margin-left:3px;font-size:13px;" OnClick="btnSaveCancel_Click" />
                                            </asp:Panel>--%>
                                          <%--  <div class="row">
                                                <table style="margin-left:10px;width:100%;">
                                                <tr>
                                                    <td style="width:240px;">
                                                       <asp:TextBox ID="txtsSHIHARAI" runat="server" Text='<%# Bind("sSHIHARAI") %>' Width="240px" CssClass="form-control TextboxStyle"></asp:TextBox>

                                                    </td>
                                                    <td style="padding-left: 10px">
                                                          <asp:Button ID="btnUpdate" runat="server" style="font-size:13px;" Text="更新" CssClass="JC10GrayButton" OnClick="btnUpdate_Click" />
                                                          <asp:Button ID="btnSaveCancel" runat="server" Text="キャンセル" CssClass="JC07HyojiItemSettingBtn" style="margin-left:3px;font-size:13px;" OnClick="btnSaveCancel_Click" />
                                                    </td>
                                                </tr>
                                            </table>
                                            </div>--%>
                                             <div style="width: 403px;">
                                                <asp:Panel ID="pnlShiharai" Style="float: left;" runat="server">
                                                    <asp:TextBox ID="txtsSHIHARAI" CssClass="form-control TextboxStyle" runat="server" Text='<%# Bind("sSHIHARAI") %>' Width="240px"></asp:TextBox>
                                                </asp:Panel>

                                                <asp:Panel ID="PopupMenu" CssClass="popupMenu JCEditedPopup_1" Style="padding: 0px !important; padding-left: 7px !important; float: left;" runat="server">
                                                    <asp:Button ID="btnUpdate" runat="server" Style="font-size: 13px;" Text="更新" CssClass="JC10GrayButton" OnClick="btnUpdate_Click" />
                                                    <asp:Button ID="btnSaveCancel" runat="server" Text="キャンセル" CssClass="JC07HyojiItemSettingBtn" Style="margin-left: 3px; font-size: 13px;" OnClick="btnSaveCancel_Click" />
                                                </asp:Panel>
                                            </div>
                                        </ItemTemplate>
                                        <ItemStyle Width="385px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                             <asp:Panel ID="pnlKyotenmei" runat="server" BorderColor="Black" Style="width:457px !important;padding-right:50px;height:35px;padding-top:5px;">
                                                <%--20220405--%>
                                                <div style="width: 370px !important; overflow: hidden; display: -webkit-box; -webkit-line-clamp: 1; -webkit-box-orient: vertical; word-break: break-all;">
                                                    <asp:LinkButton ID="lkbtnKyotenMei" runat="server" Style="font-size: 13px;" Text='<%# Server.HtmlEncode((string)Eval("sSHIHARAI"))%>' CommandName="Select" CommandArgument="<%# Container.DataItemIndex %>"></asp:LinkButton>
                                                </div>
                                            </asp:Panel>
                                            <asp:Panel ID="pnlEdited" runat="server" CssClass="PopupMenu JCEditedPopup" Style="display: none; padding: 5px 0px 0px 0px; margin-left: -95px !important;">
                                                <%--20220405--%>
                                                <asp:LinkButton ID="imgbtnCopy" runat="server" CssClass="btn-icons" CommandName="Edit" CommandArgument="<%# Container.DataItemIndex %>">
                                                    <i class="bi bi-pencil-fill"></i>
                                                </asp:LinkButton>
                                                <asp:LinkButton ID="imgbtnDelete" runat="server" CssClass="btn-icons" CommandName="Delete" CommandArgument="<%# Container.DataItemIndex %>" OnClick="btnDelete_Click" >
                                                    <i class="bi bi-trash-fill"></i>
                                                </asp:LinkButton>
<%--                                                <asp:ImageButton ID="imgbtnCopy" runat="server" CommandName="Edit" CommandArgument="<%# Container.DataItemIndex %>" ImageUrl="~/Images/draw.png" Height="14px" Width="14px" />--%>
<%--                                                <asp:ImageButton ID="imgbtnDelete" runat="server" CommandName="Delete" CommandArgument="<%# Container.DataItemIndex %>" ImageUrl="~/Images/trash.png" Height="14px" Width="14px" />--%>
                                               <%-- <asp:ConfirmButtonExtender ID="cbtneDelete" runat="server" ConfirmText="削除してもよろしいでしょうか？" TargetControlID="imgbtnDelete" />--%>
                                            </asp:Panel>
                                            <asp:HoverMenuExtender ID="hmeKyotenListEdit" runat="server" TargetControlID="pnlKyotenmei"
                                                PopupControlID="pnlEdited" PopupPosition="Right">
                                            </asp:HoverMenuExtender>
                                             <asp:Button ID="BT_DeleteOk" runat="server" Text="はい" class="BlueBackgroundButton DisplayNone" Width="100px" Height="36px" OnClick="BT_DeleteOk_Click" />
                                             <asp:Button ID="BT_DeleteCancel" runat="server" Text="キャンセル" class="JC09GrayButton DisplayNone" Width="100px" Height="36px" />
                                        </ItemTemplate>
                                        <ItemStyle Width="457px" />
                                    </asp:TemplateField>
                                </Columns>
                               <%-- <RowStyle CssClass="smgridViewRow" />
                                <AlternatingRowStyle CssClass="smgridViewRow" />--%>
                            </asp:GridView>
                        </div>
                        
                        <div id="newshiarai" class="row " style="visibility:hidden" runat="server" ><%-- --%>
                        <%--    <table style="margin: 3px 0px 3px 0px;">--%>
                            <table style="margin: 3px 0px 3px -7px;"> <%--20220405--%>
                                <tr>
                                    <td style="Width:240px">
                                       <asp:TextBox ID="txt_newshiarai" runat="server" Text='' Width="240px" CssClass="form-control TextboxStyle"  OnTextChanged="txt_newshiarai_TextChanged"></asp:TextBox>
                                    </td>
                                    <td style="padding-left: 10px">
                                        <asp:Button ID="btnnewshiaraiSave" runat="server" Text="保存" CssClass="JC10GrayButton" OnClick="btnnewshiaraiSave_Click" />
                                         <asp:Button ID="btnnewshiharaiCancel" runat="server" Text="キャンセル" CssClass="JC07HyojiItemSettingBtn" OnClick="btnnewshiharaiCancel_Click" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                      <%--  </div>--%>
                        <%--------------add by テテ 20211206 start------------%>
                        <div id="smFooter" style="display: block; width: 100%;">
                            <div style="display: flex; justify-content: center; align-items: center; background-color: #eee;  width: 100%; padding: 20px 0">
                                <asp:Button ID="btnKyotenlistCancel" runat="server" Text="キャンセル" CssClass="JC10CancelBtn" Width="99px" OnClick="btnCancel_Click" />

                            </div>
                        </div>
                        <%--------------add by テテ 20211206 end------------%>
                      
                  </ContentTemplate>
                        
                </asp:UpdatePanel>   
               
            </div>
            <asp:HiddenField ID="hdnHome" runat="server" />
             <asp:HiddenField id="hdnScrollPos" runat="server"/>
         
        </form>
    </div>

</body>
    <script src="../Scripts/jquery-3.5.1.js"></script>                
    <script type="text/javascript"> 
        //20220405 Start
         function getShiftJISByteLength(s) {
                            return s.replace(/[^\x00-\x80&#63728;｡｢｣､･ｦｧｨｩｪｫｬｭｮｯｰｱｲｳｴｵｶｷｸｹｺｻｼｽｾｿﾀﾁﾂﾃﾄﾅﾆﾇﾈﾉﾊﾋﾌﾍﾎﾏﾐﾑﾒﾓﾔﾕﾖﾗﾘﾙﾚﾛﾜﾝ ﾞ ﾟ&#63729;&#63730;&#63731;]/g, 'xx').length;
                        }
        function selectText(editindex) {
            $('[id*=gvshiarailist] tr').eq(editindex).find("[Id*='txtsSHIHARAI']").select();

            $('[id*=gvshiarailist] tr').eq(editindex).find("[Id*='txtsSHIHARAI']").on('keyup keydown', function (e) {
                 var text = $(this).val();
                                var byteAmount = getShiftJISByteLength(text);
                                if (e.keyCode != "Backspace" && e.key != "Delete") {
                                    while (byteAmount > 28) {
                                        text = text.substring(0, text.length - 1);
                                        byteAmount = getShiftJISByteLength(text);
                                        $('[id*=gvshiarailist] tr').eq(editindex).find("[Id*='txtsSHIHARAI']").val(text);
                                    }
                                }
                });
            $('[id*=gvshiarailist] tr').eq(editindex).find("[Id*='txtsSHIHARAI']").on('change', function (e) {
                 var text = $(this).val();
                                var byteAmount = getShiftJISByteLength(text);
                                if (e.keyCode != "Backspace" && e.key != "Delete") {
                                    while (byteAmount > 28) {
                                        text = text.substring(0, text.length - 1);
                                        byteAmount = getShiftJISByteLength(text);
                                        $('[id*=gvshiarailist] tr').eq(editindex).find("[Id*='txtsSHIHARAI']").val(text);
                                    }
                                }
                });
               
        }
       
        var prm = Sys.WebForms.PageRequestManager.getInstance();
            if (prm != null) {
                prm.add_endRequest(function (sender, e) {
                    if (sender._postBackSettings.panelsToUpdate != null)
                    {
                              $("#txt_newshiarai").on('keyup keydown', function (e) {
                 var text = $(this).val();
                                var byteAmount = getShiftJISByteLength(text);
                                if (e.keyCode != "Backspace" && e.key != "Delete") {
                                    while (byteAmount > 28) {
                                        text = text.substring(0, text.length - 1);
                                        byteAmount = getShiftJISByteLength(text);
                                         $("#txt_newshiarai").val(text);
                                    }
                                }
                });
            $("#txt_newshiarai").on('change', function (e) {
                 var text = $(this).val();
                                var byteAmount = getShiftJISByteLength(text);
                                if (e.keyCode != "Backspace" && e.key != "Delete") {
                                    while (byteAmount > 28) {
                                        text = text.substring(0, text.length - 1);
                                        byteAmount = getShiftJISByteLength(text);
                                         $("#txt_newshiarai").val(text);
                                    }
                                }
                });
                        
                    };
                });
                }
     //20220405 End
     var xPos, yPos;
    var prm = Sys.WebForms.PageRequestManager.getInstance();
    prm.add_beginRequest(BeginRequestHandler);
    prm.add_endRequest(EndRequestHandler);
    function BeginRequestHandler(sender, args) {
        xPos = $get('Gvdiv').scrollLeft;
        yPos = $get('Gvdiv').scrollTop;
    }
    function EndRequestHandler(sender, args) {
        $get('Gvdiv').scrollLeft = xPos;
        $get('Gvdiv').scrollTop = yPos;
            }
        
</script>

    
<script src="https://code.jquery.com/jquery-3.2.1.min.js"></script>
<script src="https://code.jquery.com/ui/1.12.1/jquery-ui.min.js"></script>
 <script src="../Scripts/cloudflare-jquery-ui-i18n.min.js"></script>
</html>
