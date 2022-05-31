<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="JC21SyouhinKensaku.aspx.cs" Inherits="JobzCloud.WebFront.JC21SyouhinKensaku" ValidateRequest ="False" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
<meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=0, shrink-to-fit=no" /> <%--20220307 Added エインドリ－--%>
    <title></title>
     <asp:PlaceHolder runat="server">
        <%: Scripts.Render("~/bundles/modernizr") %>
        <%: Styles.Render("~/style/StyleBundle1") %>
        <%: Scripts.Render("~/scripts/ScriptBundle1") %>
        <%: Styles.Render("~/style/UCStyleBundle") %>

    </asp:PlaceHolder>
    <link href="../Content1/bootstrap.min.css" rel="stylesheet" />
    <script src="../Scripts/bootstrap.bundle.min.js"></script>

    <link href="../resources/css/bootstrap.min.css" rel="stylesheet" />
    <script src="../resources/js/jquery.min.js"></script>
    <script src="../resources/js/bootstrap.min.js"></script>
    <script src="../resources/js/popper.min.js"></script>
 <%--   <script src="../Scripts/bootstrap.bundle.min.js"></script>--%>
     <style type="text/css">
        .font{
               font-family: "Hiragino Sans", "Open Sans", "Meiryo", "Hiragino Kaku Gothic Pro", "メイリオ", "MS ゴシック", "sans-serif";
            font-size: 13px;
        }
      /* Rounded border */
       hr.rounded {
            border-top: 8px solid #bbb;
            border-radius: 5px;
        }
        .right {
           position: absolute;
           right:404px;
          padding:2px;
            }
        .marginHeader{
           padding-left:20px;
        }

         /*.GVFixedHeader {
                font-weight: bold;
                position: absolute;
                top: expression(this.parentNode.parentNode.parentNode.scrollTop);
        }*/

         th {
       position: -webkit-sticky;
       position: sticky!important;
       top: 0;
       background-color: rgb(242,242,242);
       border-color:rgb(242,242,242);
       border:0px;
    }
          /*20220218 Added By 　エインドリ－*/
        .grid_header{
            overflow:auto;
            white-space:nowrap;
        }
        </style>
</head>
<body id="BD_Master" runat="server"  class="bg-transparent"> <%--20220307 delete  style="background-color:#FFFFFF;" width:100%--%>
    <form id="FrmSyouhinkensaku" runat="server">
        <asp:ScriptManager ID="ScriptManager1" EnablePageMethods="True" runat="server">
            <Scripts>
                <asp:ScriptReference Name="jquery" />
                <asp:ScriptReference Name="bootstrap" />
                <asp:ScriptReference Path="../Scripts/Common/FixFocus.js" />
                <asp:ScriptReference Path="../Scripts/Common/Common.js" />
                <asp:ScriptReference Path="../Scripts/colResizable-1.6.min.js"/>
                <asp:ScriptReference Path="../Scripts/cookie.js" />
            </Scripts>
        </asp:ScriptManager>
        <asp:PlaceHolder runat="server">
            <%: Scripts.Render("~/bundles/jqueryui") %>
        </asp:PlaceHolder>
        <asp:UpdatePanel ID="updHeader" runat="server" UpdateMode="Conditional" RenderMode="Inline"> 
            <ContentTemplate><%-- 20220307 Updated Style エインドリ－--%>
           <div id="Div_Body" runat="server" style="background-color: transparent;margin-top:50px; min-height: 100%;">
                <div style="max-width: 1160px; margin-left: auto; margin-right: auto; background-color: white; padding: 10px 0px 0px 0px;">
             
                <div style="margin-left:auto;margin-right:auto;">
                <asp:Label ID="Label1" runat="server" Text="商品を選択" CssClass="TitleLabel" style="text-align:left;"></asp:Label>
                    <asp:Button ID="Button1" runat="server" Text="✕" CssClass="PopupCloseBtn" OnClick="btncancel_Click"/>
                </div>
                    <div class="Borderline"></div>

        <div style="margin:15px 20px 20px 20px;">

            <table style="width:100% !important;margin-bottom:20px;">
                <tr>
                    <td>
                        <asp:TextBox runat="server" ID="txtCode" MaxLength="20"
                            placeholder="コード、商品名で検索できます。" CssClass="form-control TextboxStyle JC18TextBox " onfocus="this.setSelectionRange(this.value.length, this.value.length);" AutoPostBack="true"  Width="480px" OnTextChanged="txtCode_TextChanged" TextMode="Search"></asp:TextBox>
                    </td>
                    <td align="right">
                        <asp:Button ID="btnCreate" runat="server" Text="新規商品を作成" CssClass="BlueBackgroundButton JC10SaveBtn" style="margin:0px !important;" OnClick="btnCreate_Click" OnClientClick="PopupOpenButton()"/>
                    </td>
                </tr>
            </table>

          <div style="overflow-x:auto;width:100% !important;max-height:500px;"> <%--20220307--%>
        <div id="DIV_GV" runat="server" style="width:auto;display:inline-block !important;">
           <asp:Panel ID="Panel1" runat="server" Style="max-width: 1900px; min-width: 1100px; ">                                                                                                                                                 <%--20220218 Added CssClass by エインドリ－ --%>
              <asp:GridView runat="server" ID="gdvSyouhin"  BorderColor="Transparent" AutoGenerateColumns="false" EmptyDataRowStyle-CssClass="JC10NoDataMessageStyle"　CssClass="RowHover GridViewStyle"　
                                    ShowHeader="true" ShowHeaderWhenEmpty="true" RowStyle-CssClass="GridRow" CellPadding="0" OnSorting="gdvSyouhin_Sorting" AllowSorting="True" OnRowCreated="gdvSyouhin_RowCreated" OnPreRender="gdvSyouhin_PreRender">
                                  <EmptyDataRowStyle CssClass="JC18NoDataMessageStyle"  />
                        <%--<HeaderStyle Height="43px" BackColor="#F2F2F2" ForeColor="Black" CssClass="GVFixedHeader"/>--%>
                  <HeaderStyle Height="40px" BackColor="#F2F2F2" ForeColor="Black"  CssClass="grid_header"/> <%--20220218 Updated エインドリ－--%> <%--20220307 Update Height--%>
                        <RowStyle CssClass="JC12GridItem" Height="43px" />
                        <SelectedRowStyle BackColor="#EBEBF5" />
                           <Columns> <%-- 20220307 deleted HeaderStyle-Width=""--%>
                 <asp:TemplateField HeaderText="商品コード"  HeaderStyle-CssClass="marginHeader" HeaderStyle-Height="43px" SortExpression="cSYOUHIN">
                <ItemTemplate>
                       <%--<div style="width: 120px; text-align: center; overflow: hidden; white-space: nowrap; text-overflow: ellipsis">--%>
                    <div class="grip" style="text-align: left; overflow: hidden;  display: -webkit-box;  -webkit-line-clamp: 1; -webkit-box-orient: vertical;word-break: break-all;">  <%--20220218 エインドリ－ Updated Style --%>
                                  <asp:LinkButton runat="server" ID="lnkCode" Text='<%# Eval("cSYOUHIN ","{0}") %>' Font-Underline="false" OnClick="lnkCode_Click" CssClass="font" Font-Size="13px" CommandArgument='<%# Container.DataItemIndex %>'/>
                                    </div>     
                </ItemTemplate>

                   <HeaderStyle 　BorderColor="White" BorderWidth="2px"></HeaderStyle>　<%--20220218 Updated bordercolor and width by エインドリ－--%>
            </asp:TemplateField>
                  <asp:TemplateField HeaderText="商品名"  HeaderStyle-Height="43px" SortExpression="sSYOUHIN">
                <ItemTemplate>
                    <%--<div style="width: 258px;text-align: left;overflow: hidden; white-space: nowrap; text-overflow: ellipsis">--%>
                    <div class="grip" style="text-align: left; overflow: hidden;  display: -webkit-box;  -webkit-line-clamp: 1; -webkit-box-orient: vertical;word-break: break-all;">  <%--20220218 エインドリ－ Updated Style --%>
                         <asp:Label ID="lblsSyohin" runat="server" Text='<%# Eval("sSYOUHIN","{0}") %>' CommandArgument='<%# Container.DataItemIndex %>'></asp:Label>
                   </div>
                </ItemTemplate>

                        <HeaderStyle　BorderColor="White" BorderWidth="2px"></HeaderStyle>　<%--20220218 Updated bordercolor and width by エインドリ－--%>
            </asp:TemplateField>
             <asp:TemplateField HeaderStyle-Height="43px" SortExpression="nSYOUKISU">
                <ItemTemplate>
                    <%--<div style="width: 70px;text-align: left;overflow: hidden; white-space: nowrap; text-overflow: ellipsis">--%> <%--20220307 updated textalign エインドリ－--%>
                    <div class="grip" style="text-align: right; overflow: hidden;  display: -webkit-box;  -webkit-line-clamp: 1; -webkit-box-orient: vertical;word-break: break-all;">  <%--20220218 エインドリ－ Updated Style --%>
                         <asp:Label ID="lblSuryo" runat="server" Text='<%# Eval(" nSYOUKISU","{0}") %>' CommandArgument='<%# Container.DataItemIndex %>'></asp:Label>
                   </div>
                </ItemTemplate>
                 <HeaderTemplate>
                     <div class="grip" style="text-align: right; overflow: hidden;  display: -webkit-box;  -webkit-line-clamp: 1; -webkit-box-orient: vertical;word-break: break-all;">
                     <asp:Label ID="SuryoHeader" runat="server" Text="数量"></asp:Label>
                         </div>
                 </HeaderTemplate>

                 <HeaderStyle 　BorderColor="White" BorderWidth="2px"></HeaderStyle>　<%--20220218 Updated bordercolor and width by エインドリ－--%>
            </asp:TemplateField>
             <asp:TemplateField HeaderText="単位" HeaderStyle-Height="43px" SortExpression="sTANI">
                <ItemTemplate>
                     <%--<div style="width: 50px;text-align: left;overflow: hidden; white-space: nowrap; text-overflow: ellipsis">--%>
                    <div class="grip" style="text-align: left;  overflow: hidden;  display: -webkit-box;  -webkit-line-clamp: 1; -webkit-box-orient: vertical;word-break: break-all;">  <%--20220218 エインドリ－ Updated Style --%>
                         <asp:Label ID="lblTani" runat="server" Text=' <%# Eval(" sTANI","{0}") %>' CommandArgument='<%# Container.DataItemIndex %>'></asp:Label>
                   </div>
                </ItemTemplate>

                 <HeaderStyle 　BorderColor="White" BorderWidth="2px"></HeaderStyle>　<%--20220218 Updated bordercolor and width by エインドリ－--%>
            </asp:TemplateField>
             <asp:TemplateField HeaderStyle-Height="43px" SortExpression="nHANNBAIKAKAKU"> 
                <ItemTemplate>
                     <%--<div style="width: 120px;text-align: left;overflow: hidden; white-space: nowrap; text-overflow: ellipsis">--%><%--20220307 updated textalign エインドリ－--%>
                    <div class="grip" style="text-align: right; overflow: hidden;  display: -webkit-box;  -webkit-line-clamp: 1; -webkit-box-orient: vertical;word-break: break-all;">  <%--20220218 エインドリ－ Updated Style --%>
                         <asp:Label ID="lblTanka" runat="server" Text='<%# Eval("nHANNBAIKAKAKU","{0}") %>' CommandArgument='<%# Container.DataItemIndex %>'></asp:Label>
                   </div>
                </ItemTemplate>
                 <HeaderTemplate>
                     <div class="grip" style="text-align: right; overflow: hidden;  display: -webkit-box;  -webkit-line-clamp: 1; -webkit-box-orient: vertical;word-break: break-all;"> 
                     <asp:Label ID="TankaHeader" runat="server" Text="単価"></asp:Label>
                         </div>
                 </HeaderTemplate>

                   <HeaderStyle 　BorderColor="White" BorderWidth="2px"></HeaderStyle>　<%--20220218 Updated bordercolor and width by エインドリ－--%>
            </asp:TemplateField>
             <asp:TemplateField  HeaderStyle-Height="43px" SortExpression="nSHIIREKAKAKU"> 
                <ItemTemplate>
                      <%--<div style="width: 120px;text-align: left; padding-left: 1px; overflow: hidden; white-space: nowrap; text-overflow: ellipsis">--%><%--20220307 updated textalign エインドリ－--%>
                    <div class="grip" style="text-align: right; overflow: hidden;  display: -webkit-box;  -webkit-line-clamp: 1; -webkit-box-orient: vertical;word-break: break-all;">  <%--20220218 エインドリ－ Updated Style --%>
                         <asp:Label ID="lblGenka" runat="server" Text='<%# Eval("nSHIIREKAKAKU ","{0}") %>' CommandArgument='<%# Container.DataItemIndex %>'></asp:Label>
                   </div>
                </ItemTemplate>
                 <HeaderTemplate>
                      <div class="grip" style="text-align: right; overflow: hidden;  display: -webkit-box;  -webkit-line-clamp: 1; -webkit-box-orient: vertical;word-break: break-all;"> 
                     <asp:Label ID="GenkaHeader" runat="server" Text="原価"></asp:Label>
                         </div>
                 </HeaderTemplate>
                 <HeaderStyle 　BorderColor="White" BorderWidth="2px"></HeaderStyle>　<%--20220218 Updated bordercolor and width by エインドリ－--%>
            </asp:TemplateField>
             <asp:TemplateField HeaderText="大分類"  HeaderStyle-Height="43px" SortExpression="sSYOUHIN_DAIGRP">
                <ItemTemplate>
                    <%--<div style="width: 150px;text-align: left; padding-left: 1px; overflow: hidden; white-space: nowrap; text-overflow: ellipsis">--%>
                    <div class="grip" style="text-align: left;overflow: hidden;  display: -webkit-box;  -webkit-line-clamp: 1; -webkit-box-orient: vertical;word-break: break-all;">  <%--20220218 エインドリ－ Updated Style --%>
                         <asp:Label ID="lblDaibunrui" runat="server" Text='<%# Eval("sSYOUHIN_DAIGRP","{0}") %>' CommandArgument='<%# Container.DataItemIndex %>'></asp:Label>
                   </div>
                </ItemTemplate>

                 <HeaderStyle 　BorderColor="White" BorderWidth="2px"></HeaderStyle>　<%--20220218 Updated bordercolor and width by エインドリ－--%>
            </asp:TemplateField>
             <asp:TemplateField HeaderText="中分類"  HeaderStyle-Height="43px" SortExpression="sSYOUHIN_TYUUGRP">
                <ItemTemplate>
                    <%--<div style="width: 150px;text-align: left; padding-left: 1px; overflow: hidden; white-space: nowrap; text-overflow: ellipsis">--%>
                    <div class="grip" style="text-align: left; overflow: hidden;  display: -webkit-box;  -webkit-line-clamp: 1; -webkit-box-orient: vertical;word-break: break-all;">  <%--20220218 エインドリ－ Updated Style --%>
                         <asp:Label ID="lblChubunrui" runat="server" Text='<%# Eval(" sSYOUHIN_TYUUGRP","{0}") %>' CommandArgument='<%# Container.DataItemIndex %>'></asp:Label>
                   </div>
                </ItemTemplate>
                 <HeaderStyle 　BorderColor="White" BorderWidth="2px"></HeaderStyle>　<%--20220218 Updated bordercolor and width by エインドリ－--%>
            </asp:TemplateField>
             <asp:TemplateField HeaderText="備考"  HeaderStyle-Height="43px" SortExpression="sBIKOU">
                <ItemTemplate>
                    <%--<div style="width: 165px;text-align: left; padding-left: 1px; overflow: hidden; white-space: nowrap; text-overflow: ellipsis">--%>
                    <div class="grip" style="text-align: left; overflow: hidden;  display: -webkit-box;  -webkit-line-clamp: 1; -webkit-box-orient: vertical;word-break: break-all;">  <%--20220218 エインドリ－ Updated Style --%>
                         <asp:Label ID="lblBikou" runat="server" Text='<%# Eval("sBIKOU ","{0}") %>' CommandArgument='<%# Container.DataItemIndex %>'></asp:Label>
                   </div>
                </ItemTemplate>

                 <HeaderStyle 　BorderColor="White" BorderWidth="2px"></HeaderStyle>　<%--20220218 Updated bordercolor and width by エインドリ－--%>
            </asp:TemplateField>
             
            <asp:TemplateField HeaderText="">
                   <ItemTemplate>
                          <div style="text-align: center;">
                                <asp:HoverMenuExtender ID="HoverMenuExtender2" runat="server" PopupControlID="PopupMenu"
                                      TargetControlID="PopupMenuBtn" PopupPosition="left">
                          </asp:HoverMenuExtender>
                         <asp:Panel ID="PopupMenu" runat="server" CssClass="dropdown-menu fontcss " aria-labelledby="dropdownMenuButton" Style="display: none; min-width: 1rem; width: 5rem;">
                              <asp:LinkButton ID="btnEdit" class="dropdown-item" runat="server" Text='編集' Style="margin-right: 10px; font-size: 13px;" OnClick="btnEdit_Click"></asp:LinkButton>
                               <asp:LinkButton ID="btnDelete" class="dropdown-item " runat="server" Text='削除' Style="margin-right: 10px; font-size: 13px;" OnClick="btnDelete_Click"></asp:LinkButton>
                                 </asp:Panel>
                          <asp:Panel ID="PopupMenuBtn" runat="server" CssClass="modalPopup" Style="width: 20px;">
                                <button class="btn dropdown-toggle" type="button" id="dropdownMenuButton" data-toggle="dropdown"
                                 aria-haspopup="true" aria-expanded="false" style="border: 1px solid gainsboro; width: 20px; height: 20px; padding: 0px 3px 0px 1px; margin: 0; margin-top: -3px;">
                         </asp:Panel>
                         </div>

                     </ItemTemplate>
                    <HeaderStyle CssClass="JC18HeaderCol JC10DropCol AlignRight"/>
                    <ItemStyle CssClass="JC09DropDown JC10DropCol AlignCenter" />
               </asp:TemplateField>
           
              </Columns>
            
                </asp:GridView>
            <div style="width:100%;margin-top:10px;" align="center">
                <asp:Label ID="lblDataNai" runat="server" Style="font-size: 14px; font-weight: normal;" Text="該当するデータがありません"></asp:Label>
                </div>
                    </asp:Panel>                       
             </div>
                  </div>
               </div>

               <%-- <div class="container text-center"  >
                <div class="col-md-12  text-center fixed-bottom "  style =" Height :65px;background :#D7DBDD; " >--%>
                    <div class="text-center" style="height: 75px; background: #D7DBDD;"> <%--20220307 Update Style--%>
                    <%--<asp:Button ID="btncancel" runat="server"    Text="キャンセル" type="JC20button " CssClass="btn text-primary font btn-sm btn btn-outline-primary " style ="background-color:white;  margin-top: 20px;" Width="99px" Height="31" OnClick="btncancel_Click"  OnClientClick="document.forms[0].target = '_self';"/>--%>
                    <asp:Button ID="btnKyotenlistCancel" runat="server" Text="キャンセル" CssClass="btn text-primary font btn-sm btn btn-outline-primary" style="width:auto !important;background-color:white; margin-left:10px;border-radius:3px;font-size:13px;padding:6px 12px 6px 12px;letter-spacing:1px;margin-top:15px;" OnClick="btncancel_Click" Height="35px"/>  
                </div>
              <%--  </div>--%>
        <asp:HiddenField ID="hdnHome" runat="server" />
        <asp:HiddenField ID="HF_rowindex" runat="server" />
                    <asp:Button ID="btnDeleteSyouhin" runat="server" Text="" class="JC09GrayButton DisplayNone" Width="100px" Height="36px" OnClick="btnDeleteSyouhin_Click" />
                   <asp:Button ID="BT_ColumnWidth" runat="server" Text="Button" OnClick="BT_ColumnWidth_Click" style="display:none;" /><%--20220307 Added エインドリ－--%>
                 <asp:HiddenField ID="HF_GridSize" runat="server" /> <%--20220307 Added エインドリ－--%>
                         <asp:HiddenField ID="HF_Grid" runat="server" /><%--20220307 Added エインドリ－--%>
                    <asp:HiddenField ID="HF_cSyouhin" runat="server" />
                    </div>
               </div>
                 <!--ポップアップ画面-->
                <asp:UpdatePanel ID="updShouhinPopup" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:Button ID="btnShouhinPopup" runat="server" Text="Button" Style="display: none" />
                <asp:ModalPopupExtender ID="mpeShouhinPopup" runat="server" TargetControlID="btnShouhinPopup"
                    PopupControlID="pnlShouhinPopupScroll" BackgroundCssClass="PopupModalBackground" BehaviorID="pnlShouhinPopupScroll"
                    RepositionMode="RepositionOnWindowResize">
                </asp:ModalPopupExtender>
                <asp:Panel ID="pnlShouhinPopupScroll" runat="server" Style="display: none; max-height: 100% !important; overflow: hidden !important;" CssClass="PopupScrollDiv">
                    <asp:Panel ID="pnlShouhinPopup" runat="server">
                        <iframe id="ifShouhinPopup" runat="server" scrolling="yes" style="height: 100vh; width: 100vw;"></iframe>
                     <asp:Button ID="btnSyouhinNew_Close" runat="server" Text="Button" CssClass="DisplayNone" OnClick="btnSyouhinNew_Close_Click" onClientClick="MasterOverflow()" />  <%--OnClick="btn_Close_Click"--%>
                       
                    </asp:Panel>
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>
            
                   </ContentTemplate>
    </asp:UpdatePanel>
    </form>
    <%--20220218 エインドリ－ Added Start --%>
    <%--<script type="text/javascript">
     $(function (){
         if ($.cookie('colWidthSyouhinKensaku') != null) {
             var columns = $.cookie('colWidthSyouhinKensaku').split(',');
             var i = 0;
             $('.GridViewStyle th').each(function () {
                 $(this).width(columns[i++]);
             });
         }         

         $(".GridViewStyle").colResizable({
             liveDrag: true,
             resizeMode: 'overflow',
             postbackSafe: true,
             partialRefresh: true,
             flush: true,
             gripInnerHtml: "<div class='grip'></div>",
             draggingClass: "dragging",
             onResize: onSampleResized
         });

         });


        var onSampleResized = function (e)
        {
             var columns = $(e.currentTarget).find("th");
             var msg = "";
             var date = new Date();
             date.setTime(date.getTime() + (60 * 20000));
            columns.each(function ()
            {
                msg += $(this).width() + ",";
            })
            $.cookie("colWidthSyouhinKensaku", msg, { expires: date }); // expires after 20 minutes
        }; 

    var prm = Sys.WebForms.PageRequestManager.getInstance();
        if (prm != null)
        {
            prm.add_endRequest(function (sender, e)
            {
                if (sender._postBackSettings.panelsToUpdate != null)
                {
                    if ($.cookie('colWidthSyouhinKensaku') != null)
                    {
                        var columns = $.cookie('colWidthSyouhinKensaku').split(',');
                        var i = 0;
                        $('.GridViewStyle th').each(function ()
                        {
                            $(this).width(columns[i++]);
                        });
                    }         


                    $(".GridViewStyle").colResizable({
                        liveDrag: true,
                        resizeMode: 'overflow',
                        postbackSafe: true,
                        partialRefresh: true,
                        flush: true,
                        gripInnerHtml: "<div class='grip'></div>",
                        draggingClass: "dragging",
                        onResize: onSampleResized
                    });
                }
            });
        };
</script>--%>
<%--20220218 エインドリ－ Added End --%>

     <%--20220307 Added エインドリ Start－--%>
   <script type="text/javascript">
     $(function (){

         $(".GridViewStyle").colResizable({
             liveDrag: true,
             resizeMode: 'overflow',
             postbackSafe: true,
             partialRefresh: true,
             flush: true,
             disabledColumns:[9],
             gripInnerHtml: "<div class='grip'></div>",
             draggingClass: "dragging",
             onResize: onSampleResized
         });
       });

        var onSampleResized = function (e)
        {
             var columns = $(e.currentTarget).find("th");
             var msg = "";
            columns.each(function ()
            {
                msg += $(this).width() + ",";
            })
             document.getElementById("<%=HF_Grid.ClientID%>").value = $(e.currentTarget).width();
            document.getElementById("<%=HF_GridSize.ClientID%>").value = msg;
            document.getElementById("<%=BT_ColumnWidth.ClientID%>").click();
        }; 

    var prm = Sys.WebForms.PageRequestManager.getInstance();
            if (prm != null) {
                prm.add_endRequest(function (sender, e) {
                    if (sender._postBackSettings.panelsToUpdate != null)
                    {
                        $(".GridViewStyle").colResizable({
                            liveDrag: true,
                            resizeMode: 'overflow',
                            postbackSafe: true,
                            partialRefresh: true,
                            flush: true,
                            disabledColumns:[9],
                            gripInnerHtml: "<div class='grip'></div>",
                            draggingClass: "dragging",
                            onResize: onSampleResized
                        });
                        
                    };

                });

              
                }
</script>
                <%--20220307 Added エインドリ End－--%>
</body>
</html>

