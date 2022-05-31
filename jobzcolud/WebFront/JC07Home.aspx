<%@ Page Language="C#" MasterPageFile="~/WebFront/JC99NavBar.Master" AutoEventWireup="true" CodeBehind="JC07Home.aspx.cs" Inherits="jobzcolud.WebFront.JC07Home" ValidateRequest = "false" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server"  >
<!DOCTYPE html>
<html>
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>ホーム</title>
    <asp:PlaceHolder runat="server">
        <%: Scripts.Render("~/bundles/modernizr") %>
        <%: Styles.Render("~/style/StyleBundle1") %>
        <%: Scripts.Render("~/scripts/ScriptBundle1") %>
        <%: Styles.Render("~/style/UCStyleBundle") %>

    </asp:PlaceHolder>
    <webopt:BundleReference runat="server" Path="~/Content1/bootstrap" />
    <webopt:BundleReference runat="server" Path="~/Content1/css" />
     <style type="text/css">

         .bukkenRightArr {
             width:25px;
             max-width:25px !important;
             min-width:25px !important;
         }

        .JC07ArrowCol {
             width: 2%;
             min-width:2% !important;
             max-width:2% !important;
         }
       
        .JC07CodeCol {
             /*width: auto;*/
              width: 100px !important;
              min-width: 100px !important;
            max-width:100px !important;
            
         }

       .JC07BukkenMeiCol {
            /*width: auto;
            min-width: 150px;
            max-width:200px;*/
             width: 150px;
            min-width: 150px;
            max-width:150px;
            padding-right:2px;/*20220421 Chaw Added*/
         }
       
        .JC07BukkenBikoCol {
             /*width: auto;
             min-width:15%;
             max-width:200px;*/
              width:200px;
             min-width:200px;
             max-width:200px;
         }

        .JC07BukkenMitsuCountCol {
             width: auto;
             min-width:40px;
             max-width:40px;
         }

         .JC07BukkenTokuisakiCol {
             width: auto;
             min-width:14%;
             max-width:200px;
         }

          .JC07BukkenTokuisakiTantouCol {
             width: auto;
             min-width:11%;
             max-width:11%;
             padding-right:5px;/*20220421Chaw Added*/
         }

        .JC07BukkenSakuseiBiCol {
             width: auto;
             min-width:8%;
             max-width:8%;
             padding-right:5px;/*20220421Chaw Added*/
         }

          .JC07BukkenJishaTantouCol {
             width: 8%;
             min-width:8%;
             max-width:8%;
         }
        .JC07BukkengazoCol {
             width: auto;
             min-width:60px;
             max-width:60px;
         }
        .JC07MitsuCodeCol {
             width: 7%;
             min-width:7%;
             max-width:7%;
             padding-right:5px;/*20220421Chaw Added*/
         }
         .JC07MitsuTokuisakiTantouCol {
             width: 85px;
             min-width:85px;
             max-width:85px;
             padding-right:5px;/*20220421Chaw Added*/
         }
         .JC07MitsuJyoutaiCol {
            width: 85px;
             min-width:85px;
             max-width:85px;
         }
         .JC07MitsuEigyouTanCol {
              width: 100px;
             min-width:100px;
             max-width:100px;
         }
         .JC07MitsuChargeCol {
             width: 70px;
             min-width:70px;
             max-width:70px;
             text-align:right;/*20220421Chaw Added*/
             padding-right:5px;/*20220421Chaw Added*/
            
         }
         .JC07MitsugazoCol {
              width: 80px;
             min-width:80px;
             max-width:80px;
         }
         .JC07DirectMitsu {
             margin:0 !important; 
             display: inline-block;
         }
         .JC07HeaderCol {
         line-height: 0px;
         font-size: 13px;
         }
          .JC07MitToukueiCol {
             width: 150px;
            min-width: 150px;
            max-width:150px;
         }

          .JC07Kingaku{
              padding-right:5px;
          }
    </style>
</head>
<body style="overflow:scroll;">  
     <asp:PlaceHolder runat="server">
            <%: Scripts.Render("~/bundles/jqueryui") %>
        </asp:PlaceHolder>
    <div class="JC10navbar"  style="margin-top:30px;">
          <div style="height:60px;" class="bg-white ">
                <div class="center" style=" font-size: 0;">
                     <asp:Button ID="btnBukkenNew" runat="server" CssClass="BlueBackgroundButton JC07btnpadding" Text="物件を新規作成" style="margin-right:10px;" OnClick="btnBukkenNew_Click"/> 
                 <asp:Button ID="btnMitsumori" runat="server"  CssClass="BlueBackgroundButton JC07btnpadding JC07DirectMitsu" Text="見積をダイレクト作成"  OnClick="btnMitsumori_Click"/>                    
                 <asp:HyperLink ID="hl_createMitsu" runat="server" class="JC07tooltip" alt="テスト"  >
                        <asp:Image ID="Img_createMitsu" runat="server"  border="0" alt = "" src="../Img/hatena.png" style=" Width: 14px;Height: 14px; margin-bottom: 22px !important;"/>
                    </asp:HyperLink>                   
                </div>                
            </div>               
            <div >           
               
                <asp:Panel ID="pnlBukken" runat="server">
                    <asp:UpdatePanel ID="updpnlBukken" UpdateMode="Conditional" ChildrenAsTriggers="False" runat="server">
                         <Triggers>
                              <asp:AsyncPostBackTrigger ControlID="UpdateBukken" EventName="Click" />
                         </Triggers>
                         <ContentTemplate>                              
                             <asp:Button ID="UpdateBukken" runat="server" Text="UpdateBukken" cssClass="Hidecss" OnClick="UpdateBukken_Click"/>
                            <div  style="padding:15px 10px 15px 10px;background-color:white; ">                                    
                               <div class=" d-flex justify-content-between" style="background-color:white">
                                    <div id="MyBukken" style="padding-top:8px;font-weight: bold;" class="gridHeaderDiv1 fontcss" runat="server">私が担当する物件</div>
                                    <div class="gridHeaderDiv2">
                                        <asp:Button ID="btnHyoujisetPopUp" runat="server"  CssClass="JC07HyojiItemSettingBtn" Text="表示項目を設定"
                                       OnClick="btnBukkenHyoujiSetting_Click"  onClientClick="HyoujiSettingClick()"/></div> 
                               </div>
                            </div>
                            <div class="d-flex justify-content-center" style ="background-color:white;padding:0px 40px 0px 40px;">               
                               <div style="width:auto;overflow-x: auto;">

                                    <asp:GridView ID="gvBukkenOriginal"  runat="server" AutoGenerateColumns="false" cellpadding="7" Display="None"
                                 DataKeyNames="cBUKKEN" cssClass="RowHover">
                                     <HeaderStyle BackColor="#F2F2F2" HorizontalAlign="Left" Height="37px" ForeColor="Black"/>
                                 <Columns>
                                     <asp:TemplateField>
                                        <ItemTemplate>                                
                                             <%--<img id="imgArrow" border="0" alt = "" src="../Img/img-rightarrow.png" style="Width:14px; Height:14px;"/> --%>
                                            <asp:Image ID="imgArrow" runat="server" border="0" alt = "" src="../Img/img-rightarrow.png" style="Width:14px; Height:14px;"/>
                                            <asp:Panel ID="pnlSubBukken" runat="server" Style=" display:none; " >                               
                                                <div style="padding:5px; ">
                                                     <asp:GridView ID="gvSubBukken"  runat="server" AutoGenerateColumns="false"  cellpadding="7" gridlines="None" Width="900px"
                                                    DataKeyNames="cMITUMORI"  >
                                                   
                                                         <HeaderStyle BackColor="#F2F2F2" HorizontalAlign="Left" Height="37px"/>                                                                                             
                                                        <RowStyle CssClass="JC12GridItem" Height="37px" />
                                                    <Columns>
                                                        <asp:TemplateField ItemStyle-Width="35px">  
                                                               <ItemTemplate>                                                   
                                                                 <asp:Button  ID="btnCopy" runat="server" Text="コ" CssClass="JC07GridCopyButton" Width="30px" Height="25px" commandName="Copy"  CommandArgument="<%# Container.DataItemIndex %>" onclick="btnCopy_Click"　onClientClick="EditSubGridClick()"/> 
                                                               </ItemTemplate>  
                                                                <ItemStyle Width="35px" />
                                                                <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                           </asp:TemplateField>                             
                                                     
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnkbtn_cMitsumori" runat="server"  Text='<%#Eval("見積コード") %>' OnClick="btnMitsuHyouji_Click"></asp:LinkButton>
                                                            </ItemTemplate>
                                                            <HeaderTemplate>
                                                                 <asp:Label ID="lbl_HSubcMitsumori" runat="server" Text="見積コード" CssClass="d-inline-block" ></asp:Label>
                                                             </HeaderTemplate>
                                                             <HeaderStyle CssClass="JC28CodeHeaderCol" />
                                                             <ItemStyle CssClass="JC28CodeCol" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                               <div class="JC12LabelItem" style="height:35px;"> 
                                                                   <asp:Label ID="lbl_sMitsumori" runat="server" CssClass="JC07Labelcss" Text='<%# Server.HtmlEncode((string)Eval("見積名"))%>'></asp:Label>
                                                               </div>
                                                            </ItemTemplate>
                                                            <HeaderTemplate>
                                                                 <asp:Label ID="lbl_HSubsMitsumori" runat="server" Text="見積名" CssClass="d-inline-block" ></asp:Label>
                                                             </HeaderTemplate>
                                                             <HeaderStyle CssClass="JC28CodeHeaderCol" />
                                                             <ItemStyle CssClass="JC28CodeCol" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                               <div class="JC12LabelItem" style="height:35px;"> 
                                                                   <asp:Label ID="lbl_sTANTOUSHA" runat="server" CssClass="JC07Labelcss" Text='<%# Server.HtmlEncode((string)Eval("営業担当"))%>'></asp:Label>
                                                               </div>
                                                            </ItemTemplate>
                                                            <HeaderTemplate>
                                                                 <asp:Label ID="lbl_HSubsTANTOUSHA" runat="server" Text="自社担当者" CssClass="d-inline-block" ></asp:Label>
                                                             </HeaderTemplate>
                                                             <HeaderStyle CssClass="JC28CodeHeaderCol" />
                                                             <ItemStyle CssClass="JC28CodeCol" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                               <div class="JC12LabelItem" style="height:35px;"> 
                                                                   <asp:Label ID="lbl_dMITUMORISAKUSEI" runat="server" CssClass="JC07Labelcss" Text='<%#Eval("見積日") %>'></asp:Label>
                                                               </div>
                                                            </ItemTemplate>
                                                            <HeaderTemplate>
                                                                 <asp:Label ID="lbl_HSubdMITUMORISAKUSEI" runat="server" Text="見積日" CssClass="d-inline-block" ></asp:Label>
                                                             </HeaderTemplate>
                                                             <HeaderStyle CssClass="JC28CodeHeaderCol" />
                                                             <ItemStyle CssClass="JC28CodeCol" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                               <div class="JC12LabelItem" style="height:35px;padding-right:4px;"> 
                                                                   <asp:Label ID="lbl_nKINGAKU" runat="server" CssClass="JC07Labelcss" Text='<%#Eval("合計金額") %>'></asp:Label>
                                                               </div>
                                                            </ItemTemplate>
                                                            <HeaderTemplate>
                                                                 <asp:Label ID="lbl_HSubnKINGAKU" runat="server" Text="合計金額" CssClass="d-inline-block" style="padding-right:4px;" ></asp:Label>
                                                             </HeaderTemplate>
                                                             <HeaderStyle CssClass="JC28CodeHeaderCol  AlignRight" />
                                                             <ItemStyle CssClass="JC28CodeCol AlignRight" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                               <div class="JC12LabelItem" style="height:35px;"> 
                                                                   <asp:Label ID="lbl_cJYOTAI_MITUMORI" runat="server" CssClass="JC07Labelcss" Text='<%#Eval("見積状態") %>'></asp:Label>
                                                               </div>
                                                            </ItemTemplate>
                                                            <HeaderTemplate>
                                                                 <asp:Label ID="lbl_HSubJyoutai" runat="server" Text="見積状態" CssClass="d-inline-block" ></asp:Label>
                                                             </HeaderTemplate>
                                                             <HeaderStyle CssClass="JC28CodeHeaderCol" />
                                                             <ItemStyle CssClass="JC28CodeCol" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                               <div class="JC12LabelItem" style="height:35px;padding-right:4px;"> 
                                                                   <asp:Label ID="lbl_nMITUMORIARARI" runat="server" CssClass="JC07Labelcss" Text='<%#Eval("金額粗利") %>'></asp:Label>
                                                               </div>
                                                            </ItemTemplate>
                                                            <HeaderTemplate>
                                                                 <asp:Label ID="lbl_HSubArari" runat="server" Text="金額粗利" CssClass="d-inline-block" style="padding-right:4px;"></asp:Label>
                                                             </HeaderTemplate>
                                                             <HeaderStyle CssClass="JC28CodeHeaderCol AlignRight" />
                                                             <ItemStyle CssClass="JC28CodeCol AlignRight" />
                                                        </asp:TemplateField>

                                                        <asp:TemplateField>
                                                              <ItemTemplate>                                                               
                                                                 <div class="dropdown">
                                                                   <button class="btn dropdown-toggle" type="button" id="dropdownMenuButton" data-toggle="dropdown" 
                                                                       aria-haspopup="true" aria-expanded="false" style="border:1px solid gainsboro;width:20px; height:20px;padding:0px 3px 0px 1px;margin:0;">                                             
                                                                   </button>
                                                                   <div class="dropdown-menu fontcss dpoption" aria-labelledby="dropdownMenuButton" style="min-width: 1rem;width: 5rem;">                                                        
                                                                    <asp:LinkButton ID="btnDelete" class="dropdown-item " runat="server" Text='削除' style="margin-right:10px"  CommandArgument="<%# Container.DataItemIndex %>" OnClick="btnDelete_Click" OnClientClick="EditSubGridClick()"></asp:LinkButton>                                        
                                                                   </div>
                                                                 </div>
                                                                   <asp:Button ID="btnBukenSubDeleteOk" runat="server" Text="はい" class="BlueBackgroundButton DisplayNone" Width="100px" Height="36px" OnClick="btnBukenSubDeleteOk_Click" />
                                                                   <asp:Button ID="btnBukenSubDeleteCancel" runat="server" Text="キャンセル" class="JC09GrayButton DisplayNone" Width="100px" Height="36px" OnClick="btnBukenSubDeleteCancel_Click"/>
                                                             </ItemTemplate>
                                                             <ItemStyle HorizontalAlign="left" VerticalAlign="Middle" />
                                                             <ItemStyle Width="30px" />   
                                                        </asp:TemplateField>

                                                    </Columns>
                                                    
                                                </asp:GridView>
                                                </div>
                                               <div style="padding-bottom:10px; background-color:white">
                                                   <asp:Button ID="btnAddMitsumori" runat="server" Text="✛ 見積を追加" cssClass="BlueBackgroundButton JC07btnpadding" style="padding:0 15px 0 15px;" CommandArgument="<%# Container.DataItemIndex %>" OnClick="btnAddMitsumori_Click" OnClientClick="tamitsumoriClick()"/> 
                                                <asp:Button ID="btnTaMitsumori" runat="server" Text="✛ 他見積をコピーして追加"  cssClass="BlueBackgroundButton JC07btnpadding" style="padding:0 15px 0 15px;" UseSubmitBehavior="False" CausesValidation="False" CommandArgument="<%# Container.DataItemIndex %>" OnClick="btnTaMitsumori_Click" OnClientClick="tamitsumoriClick()"/> 

                                               </div>
                                    
                                            </asp:Panel>
                                        </ItemTemplate>
                                        <ItemStyle cssClass="JC07ArrowCol" />
                              
                                    </asp:TemplateField>  

                                     <asp:TemplateField HeaderStyle-CssClass="Hidecss">
                                        <HeaderTemplate >test
                                            </HeaderTemplate>
                                       <ItemTemplate>
                                            <%--After PostBack Use this attribute, open Sub Grid --%>
                                           <asp:TextBox ID="IsExpanded" runat="server" Value="" style="width:20px;"/>                              
                                           <%--Use　Request Form , get this value in class --%>
                                            <input name="IsExpanded" value='0' style="width:20px;"/>  
                                       </ItemTemplate>                           
                                      <ItemStyle Width="30" />
                                     <ItemStyle CssClass ="Hidecss"/>
                                   </asp:TemplateField>

                                    <asp:TemplateField HeaderText="物件コード" SortExpression="物件コード">   <%--//20220504 Added by phoo--%>
                                         <ItemTemplate>
                                             <div class="JC07Divcss">
                                               <asp:LinkButton ID="lnkbtn_cBukken" runat="server" CssClass="JC07Labelcss" Text=' <%# Bind("物件コード") %>' OnClick="btnBukkenHyouji_Click" onClientClick="bukkenClick()"></asp:LinkButton>                                               
                                             </div>
                                         </ItemTemplate>
                                        <%-- <HeaderTemplate>
                                              <asp:Label ID="lbl_HcBukken" runat="server" Text="コード" CssClass="d-inline-block"></asp:Label>
                                         </HeaderTemplate>--%>
                                         <HeaderStyle CssClass="JC28CodeHeaderCol" />
                                         <ItemStyle CssClass="JC07CodeCol" />
                                     </asp:TemplateField>

                                     <asp:TemplateField HeaderText="物件名" SortExpression="物件名">
                                         <ItemTemplate>
                                             <div class="JC07Divcss" style="height:35px;padding-right:4px !important;">                  
                                                <asp:Label ID="lbl_sBukken" runat="server" CssClass="JC07Labelcss" Text='<%# Server.HtmlEncode((string)Eval("物件名"))%>' ToolTip='<%#Eval("物件名") %>'></asp:Label>
                                             </div>
                                         </ItemTemplate>
                                        <%-- <HeaderTemplate>
                                               <asp:Label ID="lbl_HsBukken" runat="server" Text="物件名" CssClass="d-inline-block" ></asp:Label>
                                         </HeaderTemplate>--%>
                                         <HeaderStyle CssClass="JC28CodeHeaderCol" />
                                         <ItemStyle CssClass="JC07BukkenMeiCol" />
                                     </asp:TemplateField>

                                     <asp:TemplateField HeaderText="備考" SortExpression="備考">
                                         <ItemTemplate>
                                             <div class="JC07Divcss" style="height:35px;padding-right:4px !important;">  
                                                 <asp:Label ID="lbl_sBiko" runat="server" CssClass="JC07Labelcss" Text='<%# Server.HtmlEncode((string)Eval("備考"))%>' ToolTip='<%#Eval("備考") %>'></asp:Label>
                                             </div>
                                         </ItemTemplate>
                                         <%-- <HeaderTemplate>
                                               <asp:Label ID="lbl_HsBiko" runat="server" Text="備考" CssClass="d-inline-block" ></asp:Label>
                                         </HeaderTemplate>--%>
                                         <HeaderStyle CssClass="JC28CodeHeaderCol" />
                                         <ItemStyle CssClass="JC07BukkenBikoCol" />
                                     </asp:TemplateField>

                                     <asp:TemplateField HeaderText="見積数" SortExpression="見積数"> <%--//20220429 Added by phoo--%>
                                         <ItemTemplate>
                                             <div class="JC07Divcss" style="height:35px;padding-right:4px !important;">  
                                                  <asp:Label ID="lbl_nMitsumori" runat="server" CssClass="JC07Labelcss" Text='<%#Eval("見積数") %>' ToolTip='<%#Eval("見積数") %>' ></asp:Label>
                                             </div>
                                         </ItemTemplate>
                                        <%-- <HeaderTemplate>
                                               <asp:Label ID="lbl_HnMitsumori" runat="server" Text="見積" CssClass="d-inline-block" ></asp:Label>
                                         </HeaderTemplate>--%>
                                         <ItemStyle CssClass="JC07BukkenMitsuCountCol" />
                                          <HeaderStyle CssClass="JC07HeaderCol" /><%--JC18HeaderCol--%>
                                     </asp:TemplateField>

                                     <asp:TemplateField HeaderText="得意先" SortExpression="得意先"> <%--//20220429 Added by phoo--%>
                                         <ItemTemplate>
                                             <div class="JC07Divcss" style="height:35px;padding-right:4px !important;">  
                                                   <asp:Label ID="lbl_sTokuisaki" runat="server" CssClass="JC07Labelcss" Text='<%# Server.HtmlEncode((string)Eval("得意先"))%>' ToolTip='<%#Eval("得意先") %>'></asp:Label>
                                             </div>
                                         </ItemTemplate>
                                         <%--<HeaderTemplate>
                                               <asp:Label ID="lbl_HsTokuisaki" runat="server" Text="得意先名" CssClass="d-inline-block" ></asp:Label>
                                         </HeaderTemplate>--%>
                                         <HeaderStyle CssClass="JC28CodeHeaderCol" />
                                         <ItemStyle CssClass="JC07BukkenTokuisakiCol" />
                                     </asp:TemplateField>

                                     <asp:TemplateField HeaderText="得意先担当者" SortExpression="得意先担当者"> <%--//20220429 Added by phoo--%>
                                         <ItemTemplate>
                                             <div class="JC07Divcss" style="height:35px;padding-right:4px !important;">  
                                                  <asp:Label ID="lbl_sTokuiTantou" runat="server" CssClass="JC07Labelcss" Text='<%# Server.HtmlEncode((string)Eval("得意先担当者"))%>' ToolTip='<%#Eval("得意先担当者") %>'></asp:Label>
                                             </div>
                                         </ItemTemplate>
                                         <%--<HeaderTemplate>
                                               <asp:Label ID="lbl_HsTokuiTantou" runat="server" Text="得意先担当" CssClass="d-inline-block" ></asp:Label>
                                         </HeaderTemplate>--%>
                                       <HeaderStyle CssClass="JC28CodeHeaderCol" />
                                         <ItemStyle CssClass="JC07BukkenTokuisakiTantouCol" />
                                     </asp:TemplateField>

                                      <asp:TemplateField HeaderText="作成日" SortExpression="物件作成日"> <%--//20220429 Added by phoo--%>
                                         <ItemTemplate>
                                             <div class="JC07Divcss" style="height:35px;padding-right:4px !important;">  
                                                   <asp:Label ID="lbl_dSakuseibi" runat="server" CssClass="JC07Labelcss" Text='<%#Eval("物件作成日") %>' ToolTip='<%#Eval("物件作成日") %>'></asp:Label>
                                             </div>
                                         </ItemTemplate>
                                         <%--<HeaderTemplate>
                                               <asp:Label ID="lbl_HdSakuseibi" runat="server" Text="物件作成日" CssClass="d-inline-block" ></asp:Label>
                                         </HeaderTemplate>--%>
                                         <HeaderStyle CssClass="JC28CodeHeaderCol" />
                                         <ItemStyle CssClass="JC07BukkenSakuseiBiCol" />
                                     </asp:TemplateField>

                                      <asp:TemplateField HeaderText="自社担当者" SortExpression="自社担当者"> <%--//20220429 Added by phoo--%>
                                         <ItemTemplate>
                                             <div class="JC07Divcss" style="height:35px;padding-right:4px !important;">  
                                                  <asp:Label ID="lbl_sJishaTantou" runat="server" CssClass="JC07Labelcss" Text='<%# Server.HtmlEncode((string)Eval("自社担当者"))%>' ToolTip='<%#Eval("自社担当者") %>'></asp:Label>
                                             </div>
                                         </ItemTemplate>
                                         <%--<HeaderTemplate>
                                               <asp:Label ID="lbl_HsJishaTantou" runat="server" Text="自社担当" CssClass="d-inline-block" ></asp:Label>
                                         </HeaderTemplate>--%>
                                         <HeaderStyle CssClass="JC28CodeHeaderCol" />
                                         <ItemStyle CssClass="JC07BukkenJishaTantouCol" />
                                     </asp:TemplateField>

                                     <asp:TemplateField HeaderText="画像" Visible="false">
                                         <ItemTemplate>  
                                           
                                            <asp:Panel ID="pnl_image" runat="server" style="display:none">
                                                  
                                               <asp:Image ID="img_bukken" runat="server" cssClass="imagecss" Visible='<%# Eval("file64string").ToString() != "../Img/imgerr.png" %>'  ImageUrl='<%# Eval("file64string")%>' />
                                            </asp:Panel>
                                            <asp:Panel ID="pnl_showimage" runat="server"  >
                                            <asp:Image ID="img_popupBukken" runat="server" CssClass="popupHover" Height="30px" Width="30px" Visible='<%# Eval("file64string").ToString() != "../Img/imgerr.png" %>' ImageUrl='<%# Eval("file64string")%>'/>
                                            </asp:Panel>
                                            <asp:HoverMenuExtender ID="hme_image" runat="server" PopupControlID="pnl_image" TargetControlID="pnl_showimage" PopupPosition="Bottom">
                                            </asp:HoverMenuExtender>                                              
                                         </ItemTemplate>
                                        <%-- <HeaderTemplate>
                                               <asp:Label ID="lbl_Himage" runat="server" Text="画像" CssClass="d-inline-block" ></asp:Label>
                                         </HeaderTemplate>--%>
                                         <HeaderStyle CssClass="JC18HeaderCol" />
                                         <ItemStyle CssClass="JC07BukkengazoCol" />
                                     </asp:TemplateField>

                                 </Columns>
                                    
                                    </asp:GridView>   

                                   <asp:GridView ID="gvBukken"  runat="server" AutoGenerateColumns="false" cellpadding="7" gridlines="None" cssClass="gvbukkencss hover pointercss"
                                 DataKeyNames="cBUKKEN" OnRowDataBound="gvBukken_RowDataBound"  AllowSorting="true" OnSorting="GV_Bukken_Sorting" OnRowCreated="GV_Bukken_RowCreated"><%----%>
                                     <HeaderStyle BackColor="#F2F2F2" HorizontalAlign="Left" Height="37px" ForeColor="Black"/>
                                      <%--   <EmptyDataRowStyle CssClass="JC10NoDataMessageStyle" />   --%>                                
                                    <RowStyle CssClass="JC12GridItem" Height="37px" />
                                 <Columns>
                                   
                                 </Columns>
                                  <EmptyDataRowStyle CssClass="JC30NoDataMessageStyle" />  
                                  <EmptyDataTemplate>
                                    <div class="d-flex justify-content-center align-items-center preventHover JC07msg" >
                                        私が担当する物件がありません。
                                        <asp:LinkButton ID="lnkbtn_CreateNewBukken" runat="server" OnClick="btnBukkenNew_Click" CssClass="JC07newlink">物件を新規作成する</asp:LinkButton>
                                    </div>
                                </EmptyDataTemplate>
                                    </asp:GridView>                  
                               </div>
                                
                            </div>
                            <div class="d-flex justify-content-center" style="padding:20px 0px 20px 0px;background-color:white;">
                                   <asp:LinkButton ID="lkbtnBukkenAll" runat="server" Text='すべてを確認 ＞' style="margin-right:10px;font-size:14px;"  OnClick="lkbtnBukkenAll_Click" ></asp:LinkButton>
                            </div>
                            <div style="height:35px;">
                            </div>
                           <div  style="display:none; position:absolute">
                               <asp:TextBox ID="OpenSubgrid" runat="server" Value=""  style="display:none;"/> 
                               <asp:TextBox ID="tamitsumoriId" runat="server" Value="" style="display:none;"/>
                               <asp:TextBox ID="fbukkenClick" runat="server" Value="" style="display:none;"/>  
                           </div>
                             
                        </ContentTemplate>
                         
                    </asp:UpdatePanel>
                </asp:Panel>
                 <asp:Panel ID="pnlMitsumori" runat="server">
                     <asp:UpdatePanel ID="updpnlMitsumori" UpdateMode="Conditional" ChildrenAsTriggers="False" runat="server">
                         <Triggers>
                             <asp:AsyncPostBackTrigger ControlID="UpdateMitsumori" EventName="Click" />
                         </Triggers>
                         <ContentTemplate>
                               <asp:Button ID="UpdateMitsumori" runat="server" Text="UpdateMitsumori" cssClass="Hidecss" OnClick="UpdateMitsumori_Click" />
                              <div>
                                <div style="padding:15px 10px 15px 10px;background-color:white">                                     
                                  <div  style="background-color:white">
                                     <div class=" d-flex justify-content-between" style="background-color:white">
                                         <div id="MyMitsumori" style="padding-top:8px;font-weight: bold;padding-left:45px;" class="fontcss" runat="server">私が担当する見積</div>
                                         <div style="padding-right:45px;"> 
                                             <asp:Button ID="btnHyoujiSettingM" runat="server" CssClass="JC07HyojiItemSettingBtn" Text="表示項目を設定"
                                                  OnClick="btnMitsuHyoujiSetting_Click"  onClientClick="HyoujiSettingClick()"/></div>
                                         </div>
                                     </div>
                                 </div>
                                 <div class="d-flex justify-content-center" style ="background-color:white;padding:0px 10px 0px 10px;">
                                     <div style="overflow-x: auto;width:1500px;margin-left:40px; margin-right:40px;">
                                         <asp:GridView ID="gvMitsumoriOriginal" runat="server" AutoGenerateColumns="false" cellpadding="7" gridlines="None" 
                                          DataKeyNames="cMITUMORI"  >
                                                <EmptyDataRowStyle CssClass="JC10NoDataMessageStyle" />
                                                <HeaderStyle Height="37px" BackColor="#F2F2F2" />
                                                <RowStyle CssClass="JC12GridItem" Height="37px" />
                                             <Columns>
                                                  <asp:TemplateField>
                                                     <ItemTemplate>                                                               
                                                        <%-- <div class="dropdown" style="width:27px;word-break:break-all;">
                                                           <button class="btn dropdown-toggle" type="button" id="dropdownMenuButton1" data-toggle="dropdown" 
                                                               aria-haspopup="true" aria-expanded="false" style="border:1px solid gainsboro;width:20px; height:20px;padding:0px 3px 0px 1px;margin:0;">                                             
                                                           </button>
                                                           <div class="dropdown-menu fontcss dpoption" aria-labelledby="dropdownMenuButton" style="min-width: 1rem;width: 5rem;">
                                                            <asp:LinkButton ID="lnkbtnMitsuEdit1" class="dropdown-item" runat="server" Text='編集' style="margin-right:10px;z-index:1000;"  CommandArgument="<%# Container.DataItemIndex %>" OnClick="OnUpdateClick" ></asp:LinkButton>
                                                            <asp:LinkButton ID="lnkbtnMitsuDelete1" class="dropdown-item" runat="server" Text='削除' style="margin-right:10px;z-index:1000;"  CommandArgument="<%# Container.DataItemIndex %>" OnClick="OnDeleteClick" ></asp:LinkButton>                                        
                                                           </div>
                                                         </div>--%>
                                                          <div style=" width: 27px;  overflow: hidden; ">
                                                            <asp:HoverMenuExtender ID="HmeMitsuOpt" runat="server" PopupControlID="PnlOpt"
                                                                TargetControlID="PnlBtn" PopupPosition="Bottom">
                                                            </asp:HoverMenuExtender>
                                                            <asp:Panel ID="PnlOpt" runat="server" CssClass=" dropdown-menu fontcss " aria-labelledby="dropdownMenuButton" Style="display: none; min-width: 1rem; width: 5rem; margin-top:0;">
                                                                 <asp:LinkButton ID="lnkbtnMitsuEdit" class="dropdown-item" runat="server" Text='編集' style="margin-right:10px;font-size:13px;"  CommandArgument="<%# Container.DataItemIndex %>" OnClick="OnUpdateClick" ></asp:LinkButton>
                                                                        <asp:LinkButton ID="lnkbtnMitsuDelete" class="dropdown-item" runat="server" Text='削除' style="margin-right:10px;font-size:13px;"  CommandArgument="<%# Container.DataItemIndex %>" OnClick="OnDeleteClick" ></asp:LinkButton>                      
                                                            </asp:Panel>
                                                            <asp:Panel ID="PnlBtn" runat="server" CssClass="modalPopup" Style=" width: 20px;">
                                                                <button class="btn dropdown-toggle" type="button" id="dropdownMenuButton" data-toggle="dropdown"
                                                                    aria-haspopup="true" aria-expanded="false" style="border: 1px solid gainsboro; width: 20px; height: 20px; padding: 0px 3px 0px 1px; margin: 0;">
                                                                </button>
                                                            </asp:Panel>
                                                        </div>
                                                         <asp:Button ID="btnMitsumoriDeleteOk" runat="server" Text="はい" class="BlueBackgroundButton DisplayNone" Width="100px" Height="36px" OnClick="btnMitsumoriDeleteOk_Click" />
                                                         <asp:Button ID="btnBukenSubDeleteCancel" runat="server" Text="キャンセル" class="JC09GrayButton DisplayNone" Width="100px" Height="36px" />
                                                     </ItemTemplate>
                                                     <ItemStyle Width="27px" />                             
                                                 </asp:TemplateField>

                                                 <asp:TemplateField HeaderText="見積コード" SortExpression="見積コード">
                                                     <ItemTemplate>
                                                         <div>
                                                             <asp:LinkButton ID="lnkbtn_McMitsumori" runat="server" CssClass="JC07Labelcss" Text='<%#Eval("見積コード") %>' OnClick="btnMitsuHyouji_Click"></asp:LinkButton>
                                                         </div>                                                          
                                                     </ItemTemplate>
                                                    <%-- <HeaderTemplate>
                                                         <asp:Label ID="lbl_HMcMitsumori" runat="server" Text="見積コード" CssClass="d-inline-block" ></asp:Label>
                                                      </HeaderTemplate>--%>
                                                    
                                                        <ItemStyle CssClass="JC07MitsuCodeCol" />
                                                 </asp:TemplateField>

                                                  <asp:TemplateField HeaderText="見積名" SortExpression="見積名">
                                                     <ItemTemplate>
                                                         <div class='JC07Divcss'>
                                                            <asp:Label ID="lbl_MsMitsumori" runat="server" CssClass="JC07Labelcss" Text='<%# Server.HtmlEncode((string)Eval("見積名"))%>' ToolTip='<%#Eval("見積名") %>'></asp:Label>
                                                         </div>
                                                     </ItemTemplate>
                                                      <%--<HeaderTemplate>
                                                         <asp:Label ID="lbl_MsMitsumori" runat="server" Text="見積名" CssClass="d-inline-block" ></asp:Label>
                                                      </HeaderTemplate>--%>
                                                      <ItemStyle CssClass="JC07BukkenMeiCol" />
                                                 </asp:TemplateField>

                                                  <asp:TemplateField HeaderText="メモ" SortExpression="メモ"><%--//20220502Added by phoo--%>
                                                     <ItemTemplate>
                                                        <div class='JC07Divcss'>
                                                            <asp:Label ID="lbl_Mmemo" runat="server" CssClass="JC07Labelcss" Text='<%# Server.HtmlEncode((string)Eval("メモ"))%>' ToolTip='<%#Eval("メモ") %>'></asp:Label>
                                                         </div>
                                                     </ItemTemplate>
                                                     <%-- <HeaderTemplate>
                                                         <asp:Label ID="lbl_HMmemo" runat="server" Text="社内メモ" CssClass="d-inline-block" ></asp:Label>
                                                      </HeaderTemplate>--%>
                                                     <ItemStyle CssClass="JC07BukkenBikoCol" />
                                                 </asp:TemplateField>

                                                  <asp:TemplateField HeaderText="備考" SortExpression="備考"> <%--//20220502Added by phoo--%>
                                                     <ItemTemplate>
                                                         <div class='JC07Divcss' style="padding-right:4px !important;">
                                                            <asp:Label ID="lbl_MBiko" runat="server" CssClass="JC07Labelcss" Text='<%# Server.HtmlEncode((string)Eval("備考"))%>' ToolTip='<%#Eval("備考") %>'></asp:Label>
                                                         </div>                                                        
                                                     </ItemTemplate>
                                                     <%-- <HeaderTemplate>
                                                         <asp:Label ID="lbl_HMBiko" runat="server" Text="見積書備考" CssClass="d-inline-block" ></asp:Label>
                                                      </HeaderTemplate>--%>
                                                       <ItemStyle CssClass="JC07BukkenBikoCol" />
                                                 </asp:TemplateField>

                                                 <asp:TemplateField HeaderText="得意先" SortExpression="得意先"> <%--//20220502 Added by phoo--%>
                                                     <ItemTemplate>                                                        
                                                         <div class='JC07Divcss' style="padding-right:4px !important;">
                                                            <asp:Label ID="lbl_MTokuisaki" runat="server" CssClass="JC07Labelcss" Text='<%# Server.HtmlEncode((string)Eval("得意先"))%>' ToolTip='<%#Eval("得意先") %>'></asp:Label>
                                                         </div>
                                                     </ItemTemplate>
                                                    <%-- <HeaderTemplate>
                                                         <asp:Label ID="lbl_HMTokuisaki" runat="server" Text="得意先名" CssClass="d-inline-block" ></asp:Label>
                                                      </HeaderTemplate>--%>
                                                     <ItemStyle CssClass="JC07MitToukueiCol"/>
                                                 </asp:TemplateField>

                                                   <asp:TemplateField HeaderText="得意先担当者" SortExpression="得意先担当者"> <%--//20220502 Added by phoo--%>
                                                     <ItemTemplate>
                                                         <div class='JC07Divcss' style="padding-right:4px !important;">
                                                            <asp:Label ID="lbl_MTokuisakiTantou" runat="server" CssClass="JC07Labelcss" Text='<%# Server.HtmlEncode((string)Eval("得意先担当者"))%>' ToolTip='<%#Eval("得意先担当者") %>'></asp:Label>
                                                         </div>
                                                     </ItemTemplate>
                                                       <%--<HeaderTemplate>
                                                         <asp:Label ID="lbl_HMTokuisakiTantou" runat="server" Text="得意先担当" CssClass="d-inline-block" ></asp:Label>
                                                      </HeaderTemplate>--%>
                                                       <ItemStyle CssClass="JC07MitsuTokuisakiTantouCol" />
                                                 </asp:TemplateField>

                                                 <asp:TemplateField HeaderText="見積日" SortExpression="見積日">
                                                     <ItemTemplate>                                                        
                                                         <div class='JC07Divcss' style="padding-right:4px !important;">
                                                            <asp:Label ID="lbl_MmitsuDate" runat="server" CssClass="JC07Labelcss" Text='<%#Eval("見積日") %>' ToolTip='<%#Eval("見積日") %>'></asp:Label>
                                                         </div>
                                                     </ItemTemplate>
                                                     <%-- <HeaderTemplate>
                                                         <asp:Label ID="lbl_HMmitsuDate" runat="server" Text="見積日" CssClass="d-inline-block" ></asp:Label>
                                                      </HeaderTemplate>--%>
                                                     <ItemStyle CssClass="JC07BukkenSakuseiBiCol" />
                                                 </asp:TemplateField>

                                                 <asp:TemplateField HeaderText="自社担当者" SortExpression="自社担当者">  <%--//20220502 Added by phoo--%>
                                                     <ItemTemplate>                                                        
                                                         <div class='JC07Divcss' style="padding-right:4px !important;">
                                                            <asp:Label ID="lbl_MEigyouTantou" runat="server" CssClass="JC07Labelcss" Text='<%# Server.HtmlEncode((string)Eval("自社担当者"))%>' ToolTip='<%#Eval("自社担当者") %>'></asp:Label>
                                                         </div>
                                                     </ItemTemplate>
                                                      <%-- <HeaderTemplate>
                                                         <asp:Label ID="lbl_HMEigyouTantou" runat="server" Text="営業担当" CssClass="d-inline-block" ></asp:Label>
                                                      </HeaderTemplate>--%>
                                                        <ItemStyle CssClass="JC07MitsuEigyouTanCol" />
                                                 </asp:TemplateField>

                                                 <asp:TemplateField HeaderText="見積状態" SortExpression="見積状態">
                                                     <ItemTemplate>                                                        
                                                         <div class='JC07Divcss' style="padding-right:4px !important;">
                                                            <asp:Label ID="lbl_Mjyoutai" runat="server" CssClass="JC07Labelcss" Text='<%#Eval("見積状態") %>' ToolTip='<%#Eval("見積状態") %>'></asp:Label>
                                                         </div>
                                                     </ItemTemplate>
                                                      <%--<HeaderTemplate>
                                                         <asp:Label ID="lbl_HMjyoutai" runat="server" Text="見積状態" CssClass="d-inline-block" ></asp:Label>
                                                      </HeaderTemplate>--%>
                                                       <ItemStyle CssClass="JC07BukkenTokuisakiTantouCol" />
                                                 </asp:TemplateField>

                                                   <asp:TemplateField HeaderText="合計金額" SortExpression="合計金額">
                                                     <ItemTemplate>                                                        
                                                         <div class='JC07Divcss' style="padding-right:4px !important;">
                                                            <asp:Label ID="lbl_MGoukeikingaku" runat="server" CssClass="JC07Labelcss" Text='<%#Eval("合計金額") %>'  ToolTip='<%#Eval("合計金額") %>'></asp:Label>
                                                         </div>
                                                     </ItemTemplate>
                                                       <%--<HeaderTemplate>
                                                         <asp:Label ID="lbl_HMGoukeikingaku" runat="server" Text="合計金額" CssClass="d-inline-block" ></asp:Label>
                                                      </HeaderTemplate>--%>
                                                       <HeaderStyle CssClass="JC07Kingaku AlignRight" />
                                                         <ItemStyle CssClass="JC07MitsuChargeCol" />
                                                 </asp:TemplateField>

                                                  <asp:TemplateField HeaderText="金額粗利" SortExpression="金額粗利">
                                                     <ItemTemplate>                                                        
                                                          <div class='JC07Divcss' style="padding-right:4px !important;">
                                                            <asp:Label ID="lbl_MArari" runat="server" CssClass="JC07Labelcss" Text='<%#Eval("金額粗利") %>' ToolTip='<%#Eval("金額粗利") %>'></asp:Label>
                                                         </div>
                                                     </ItemTemplate>
                                                      <%-- <HeaderTemplate>
                                                         <asp:Label ID="lbl_HMArari" runat="server" Text="金額粗利" CssClass="d-inline-block" ></asp:Label>
                                                      </HeaderTemplate>--%>
                                                       <HeaderStyle CssClass="JC07Kingaku AlignRight" />
                                                        <ItemStyle CssClass="JC07MitsuChargeCol" />
                                                 </asp:TemplateField>

                                                  <asp:TemplateField HeaderText="作成者" SortExpression="作成者">
                                                     <ItemTemplate>                                                        
                                                         <div class='JC07Divcss' style="padding-right:4px !important;">
                                                            <asp:Label ID="lbl_MSakuseisha" runat="server" CssClass="JC07Labelcss" Text='<%# Server.HtmlEncode((string)Eval("作成者"))%>' ToolTip='<%#Eval("作成者") %>'></asp:Label>
                                                         </div>
                                                     </ItemTemplate>
                                                       <%--<HeaderTemplate>
                                                         <asp:Label ID="lbl_HMSakuseisha" runat="server" Text="作成者" CssClass="d-inline-block" ></asp:Label>
                                                      </HeaderTemplate>--%>
                                                       <%-- <ItemStyle CssClass="JC07BukkenTokuisakiTantouCol" />--%>
                                                        <ItemStyle CssClass="JC07MitsuTokuisakiTantouCol" />
                                                 </asp:TemplateField>

                                                  <asp:TemplateField HeaderText="画像" Visible="false">
                                                     <ItemTemplate>
                                                        <asp:Panel ID="pnl_image" runat="server" style="display:none">
                                                             <asp:Image ID="img_mitsumori" runat="server" cssClass="imagecss" Visible='<%# Eval("file64string").ToString() != "../Img/imgerr.png" %>'  ImageUrl='<%# Eval("file64string") %>' />
                                                         </asp:Panel>
                                                         <asp:Panel ID="pnl_showimage" runat="server"  >
                                                              <asp:Image ID="img_popupmitsumori" runat="server" CssClass="popupHover" Height="30px" Width="30px" Visible='<%# Eval("file64string").ToString() != "../Img/imgerr.png"  %>'  ImageUrl='<% # Eval("file64string") %>'/>
                                                         </asp:Panel>
                                                         <asp:HoverMenuExtender ID="hme_Mimage" runat="server" PopupControlID="pnl_image" TargetControlID="pnl_showimage" PopupPosition="Bottom">
                                                         </asp:HoverMenuExtender>
                                                     </ItemTemplate>
                                                        <%--<HeaderTemplate>
                                                         <asp:Label ID="lbl_HMimage" runat="server" Text="画像" CssClass="d-inline-block" ></asp:Label>
                                                      </HeaderTemplate>--%>
                                                        <ItemStyle CssClass="JC07MitsugazoCol" />
                                                 </asp:TemplateField>

                                             </Columns>
                                             
                                         </asp:GridView>

                                         <asp:GridView ID="gvMitsumori" runat="server" AutoGenerateColumns="false" cellpadding="7" gridlines="None" Width="1400px"
                                          DataKeyNames="cMITUMORI" cssClass="RowHover pointercss" AllowSorting="true" OnSorting="GV_Mitsumori_Sorting" OnRowCreated="GV_Mitsumori_RowCreated">
                                          <HeaderStyle BackColor="#F2F2F2" HorizontalAlign="Left" Height="37px" Font-Size="13px" ForeColor="Black"/>
                                                <EmptyDataRowStyle CssClass="JC10NoDataMessageStyle" />
                                   
                                                <RowStyle CssClass="JC12GridItem" Height="37px" />
                                             <Columns>
                                                 
                                             </Columns>
                                               <EmptyDataRowStyle CssClass="JC30NoDataMessageStyle" />  
                                              <EmptyDataTemplate>
                                                <div class="d-flex justify-content-center align-items-center JC07msg JC07msgM" > 
                                                    私が担当する見積がありません。
                                                    <asp:LinkButton ID="lnkbtn_CreateNewMitsumori" runat="server" OnClick="btnMitsumori_Click" Style="text-decoration:underline;">見積をダイレクト作成する</asp:LinkButton>                                                    
                                                </div>
                                            </EmptyDataTemplate>
                                          
                                           </asp:GridView>
                                     </div>
                                     
                                 </div>
                   
                                 <div class="d-flex justify-content-center" style="padding:20px 0px 20px 0px;margin-bottom:20px;background-color:white;">
                                      <asp:LinkButton ID="lkbtnMitsumoriAll" runat="server" Text='すべてを確認 ＞' style="margin-right:10px;font-size:14px;" OnClick="lkbtnMitsumoriAll_Click" ></asp:LinkButton>
                                 </div>
                           </div>
                         </ContentTemplate>
                     </asp:UpdatePanel>
                 </asp:Panel>
                
                      
                    <asp:UpdatePanel ID="updHyoujiSet" runat="server" UpdateMode="Conditional" >
                       
                        <ContentTemplate>
                            <asp:Button ID="btnHyoujiSetting" runat="server" Text="Button" style="display:none" />
                            <asp:ModalPopupExtender ID="mpeHyoujiSetPopUp" runat="server" TargetControlID="btnHyoujiSetting"
                                PopupControlID="pnlHyoujiSetPopUpScroll" BehaviorID="pnlHyoujiSetPopUpScroll" BackgroundCssClass="PopupModalBackground"
                                RepositionMode="None">
                            </asp:ModalPopupExtender>
                            <asp:Panel ID="pnlHyoujiSetPopUpScroll" runat="server"  CssClass="PopupScrollDiv"><%--Style="display: none; width:950px; height:98vh; margin-left:17%"--%><%--remove style and add media query in <style></style> by テテ--%>
                                <asp:Panel ID="pnlHyoujiSetPopUp" runat="server">
                                    <iframe id="ifpnlHyoujiSetPopUp" runat="server" class="HyoujiSettingIframe"  seamless></iframe><%--style="width: 940px;height:94vh;background-color:white"--%> <%--remove style and add media query in <style></style> by テテ--%>                   
                                       <asp:Button ID="btnHyoujiSave" runat="server" Text="Button" CssClass="DisplayNone" OnClick="btnSaveHyoujiClose_Click" onClientClick="MasterOverflow()" /><%--20211206 テテ added--%>
                                       <asp:Button ID="btnHyoujiClose" runat="server" Text="Button" CssClass="DisplayNone" OnClick="btnHyoujiSettingClose_Click" onClientClick="MasterOverflow()" /><%--20211108 テテ added--%>
                                         <%--<asp:TextBox ID="OpenSubgrid" runat="server" Value="" style="display:none"/>--%>
                                </asp:Panel>
                            </asp:Panel>
                            
                        </ContentTemplate>                        
                    </asp:UpdatePanel>
                     
                    <asp:UpdatePanel ID="updShinkiPopup" runat="server" UpdateMode="Conditional" >
                        <ContentTemplate>
                            <asp:Button ID="btnShinkiPopup" runat="server" Text="Button" Style="display: none" />
                            <asp:ModalPopupExtender ID="mpeShinkiPopup" runat="server" TargetControlID="btnShinkiPopup"
                                PopupControlID="pnlShinkiPopupScroll" BackgroundCssClass="PopupModalBackground" BehaviorID="pnlShinkiPopupScroll"
                                RepositionMode="RepositionOnWindowResize">
                            </asp:ModalPopupExtender>
                            <asp:Panel ID="pnlShinkiPopupScroll" runat="server" Style="display: none; height: 100%; overflow: hidden;" CssClass="PopupScrollDiv">
                                <asp:Panel ID="pnlShinkiPopup" runat="server">
                                    <iframe id="ifShinkiPopup" runat="server" scrolling="yes" style="height: 100vh; width: 100vw;"></iframe>                                    
                                    <asp:Button ID="btn_CloseMitumoriSearch" runat="server" Text="Button" CssClass="DisplayNone" OnClick="btn_CloseMitumoriSearch_Click" onClientClick="MasterOverflow()" />
                                </asp:Panel>
                            </asp:Panel>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
              
                <script src="../Scripts/jquery-3.5.1.js"></script>                
                <script type="text/javascript"> 
                    function HyoujiSettingClick() {
                         document.getElementById("<%= tamitsumoriId.ClientID %>").value = "0";   
                        document.getElementById("<%= OpenSubgrid.ClientID %>").value = "0";
                        document.getElementById("BD_Master").style.overflow = "hidden";
                    }

                    function tamitsumoriClick() {
                        document.getElementById("<%= tamitsumoriId.ClientID %>").value = "1";                   
                        document.getElementById("<%= OpenSubgrid.ClientID %>").value = "0"; 
                        document.getElementById("BD_Master").style.overflow = "hidden";
                    }

                    function EditSubGridClick() {
                        document.getElementById("<%= tamitsumoriId.ClientID %>").value = "1";
                        document.getElementById("<%= OpenSubgrid.ClientID %>").value = "1"; 
                    }
                    function bukkenClick() {
                       document.getElementById("<%= fbukkenClick.ClientID %>").value = "1"; 
                    }
                    $(function () {

                        var imgUrl_rightArrow = "data:image/png;base64,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";
                        var imgUrl_DownArrow = "data:image/png;base64,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";
                       
                        //スクロール・バー配置の記憶
                        $(window).scroll(function () {
                            sessionStorage.scrollTop = $(this).scrollTop();
                        });
                        $(document).ready(function () {
                           
                           
                            if (sessionStorage.scrollTop != "undefined") {
                                $(window).scrollTop(sessionStorage.scrollTop);
                            }
                            
                            //物件リスト一覧行をクリックすると、行の色を変更する 
                            $('[id*=gvBukken] tr td').on('click', function () { 
                                var fbukkenClick = document.getElementById("<%= fbukkenClick.ClientID %>").value;
                              
                                if (fbukkenClick == "1") {
                                    return;
                                }
                                if (this.parentNode.rowIndex > 0)
                                {
                                    var col = $(this).parent().children().index($(this));                      
                                    var title = $(this).closest("table").find("th").eq(col).text();
                                    title = title.trim();
                           
                                
                                    var row = $(this).parent();                                                                
                                    $("[id*=gvBukken] tr").each(function () {               
                                        if ($(this)[0] != row[0]) {
                                            $("td", this).removeClass("JC07Selected_row");
                                        }
                                   });

                                   //色付ける
                                    $("td", row).each(function () {
                                        if (!$(this).hasClass("JC07Selected_row")) {
                                            $(this).addClass("JC07Selected_row");
                   
                                        } else {
                                            $(this).removeClass("JC07Selected_row");
                    
                                        }
                                     });
                                     $("[id*=gvSubBukken] td", row).each(function () {
                                            $(this).removeClass("JC07Selected_row");
               
                                   });

                                   //行を展開する
                                   var expandvalue = $(this).closest("tr").find("[name*='IsExpanded']").val();                           
                                   var col = $(this).parent().children().index($(this));
                                   if (expandvalue == "1") {    
                               
                                       $(this).closest("tr").next().remove();
                                       var row = $(this).closest("tr");
                                       row.find("[name*='IsExpanded']").val("0");
                                       // $("[src*=img-buttonarrow]", $(this).closest("tr")).attr("src", "/Img/img-rightarrow.png");
                                       $("[id*=imgArrow]", $(this).closest("tr")).attr("src", imgUrl_rightArrow);
                              
                                   }
                                   else {
                                       //alert('a');
                                        $(this).closest("tr").after("<tr><td></td><td colspan = '999'>" + $("[id*=pnlSubBukken]", $(this).closest("tr")).html() + "</td></tr>");                                 
                                        var row = $(this).closest("tr");
                                        row.find("[name*='IsExpanded']").val("1");                                   
                                       // $("[src*=img-rightarrow]", $(this).closest("tr")).attr("src", "/Img/img-buttonarrow.png");
                                       $("[id*=imgArrow]", $(this).closest("tr")).attr("src", imgUrl_DownArrow);
                               
                                   }
                           
                                }
                            });
                            //物件リスト一覧行をmouse hover すると行に色を付ける
                             $('[id*=gvBukken] td').hover( function () {                           
                                 var col = $(this).parent().children().index($(this));                      
                                
                                      var row = $(this).parent();                                                                
                                      $("[id*=gvBukken] tr").each(function () {               
                                          if ($(this)[0] != row[0]) {
                                              $("td", this).removeClass("JC07Selected_row");
                                          }
                                     });

                                     //色付ける
                                      $("td", row).each(function () {
                                          if (!$(this).hasClass("JC07Selected_row")) {
                                              $(this).addClass("JC07Selected_row");
                   
                                          } else {
                                              $(this).removeClass("JC07Selected_row");
                    
                                          }
                                       });
                                       $("[id*=gvSubBukken] td", row).each(function () {
                                              $(this).removeClass("JC07Selected_row");
               
                                     });
                                
                              });
                     
                        //物件詳細にあるコピー、削除したあと、物件詳細を表示する
                            $("[id*=IsExpanded]").each(function () {    
                                //alert('b');
                            if ($(this).val() == "1") {
                              $(this).closest("tr").find("[name*='IsExpanded']").val("1");
                              $(this).closest("tr").after("<tr><td></td><td colspan = '999'>" + $("[id*=pnlSubBukken]", $(this).closest("tr")).html() + "</td></tr>")
                             // $("[src*=img-rightarrow]", $(this).closest("tr")).attr("src", "/Img/img-buttonarrow.png");
                              $("[id*=imgArrow]", $(this).closest("tr")).attr("src", imgUrl_DownArrow);
                            }
                        });
                        });
                        
                    });

                    //after parital render jquery is not woking 
                     var parameter = Sys.WebForms.PageRequestManager.getInstance();

                    parameter.add_endRequest(function () {

                        var imgUrl_rightArrow = "data:image/png;base64,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";
                        var imgUrl_DownArrow = "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAOEAAADhCAYAAAA+s9J6AAAAIGNIUk0AAHolAACAgwAA+f8AAIDpAAB1MAAA6mAAADqYAAAXb5JfxUYAAAAJcEhZcwAADsQAAA7EAZUrDhsAAAn8SURBVHhe7dy/ix3XGcZx7UbaRbEkhCRXhgj0F9idC/cJJAGnTjpXLk06YxAqTCqDa/eubXAITqoUqZO/QGBDKoOEkETErrR7fR77HXMjr+7Oj3PmOWfm+2l2zvpq996Z85z3zLyL9zabzQUAPvvxFYAJIQTMCCFgRggBM0IImBFCwIwQAmaEEDAjhIAZIQTMCCFgRggBM0IImBFCwIwQAmaEEDAjhIAZIQTMCCFgRggBM0IImBFCwIwQAmaEEDAjhIAZIQTMCCFgRggBM0IImBFCwIwQAmaEEDAjhIAZIQTMCCFgRggBM0IImBFCwIwQAmaEEDAjhIAZIQTMCCFgRggBM0IImBFCwIwQAmaEEDAjhIAZIQTMCCFgRggBM0IImBFCwIwQAmaEEDAjhIDZ3mazicPz3bt3L45+dHh4+Nb+/v6fnzx58s7p6ekv0/H/rl69+q9nz579KV4CLMar5ns6/uTo6Og/8bIf3L17N47ON7oSXrp06ev0i/+dAvfHixcv3j44OHhdXzVO/3mT3vAXP74SaN+u+a7vp/FX8dLBRoXwxYsX958/f/7rGJ4pvbF39boYAs3qM9+Pj49/N3a+Dw6hKlxaAe7EcCe9Lr35b2MINEfzd8h8H7MDHBxCVbg47CWV8V+lFeKbGALN0LzV/I1hL8qH7h1j2MugEF65cuWzOBxEe+e0ovw3hkD1NF81b2M4SAru+3HYy6AQPn369M04HCy9sTeoiGhBVMA3YjjY48eP347DXgaF8OTk5HocjhIVkXtEVCvuAUdVwM7p6elrcdjLoBCm1eG7OBxNe2wqImo05h7wLCnED+Kwl0EhvHbt2j/jcBIqImqTowJ2rl+//o847GVQCB89evRRHE6mFYcgogaahzkqYGdoTgaFUFLKP47Dydiawi3XFrQzJh+DQ6iUHxwc/DWGk7E1hUvOLagoF2N2i4NDKMfHx79Pq8ffYzgZFRFzy10BlQflIoaDjAqhpFXkN+kX/y2Gk0VFpKGP4jTPclZA5UB5iOFgo0Mo6Rf/VitADCdLP4uGPoqKCji6Ef8yzX/lIIajTAqhaAXQShDDybhHRCmaVzVVwM7kEIpWAq0IMZws/SzuEZFViXtAzfsYTpIlhKIVQStDDCejIiIXzaMaK2AnWwhFK4NWiBhOppWLIGIKzR/NoxhOpvmteR7DLLKGULRC5A4iW1OMUWgLmq0CdrKHUCKIbE1ho/lS8xZ0W5EQSnrDPKyBRc0PYc5SLISilaPAn7jR0McraX7krICav5rHMSyiaAilwJ+40dDHmaICZm3Ej/1TtCGKh1AKVUTuEfETzYfWKmBnlhAKf/SNUkrcA85RATuzhVC0sqQPyFNTZJO7Amp+zlUBO7OGUNIHpKGPLHTddf1jOJnmpeZnDGczewhFK03uILI1XZcSW1DNyxjOyhJCiSCyNcVgus6tb0G32UIo6YPT0McghSrg7FvQbdYQilYgGvroQ9c1ZwWcsw2xiz2EQkMf54kK2Fwjvo8qQiiFKiL3iAug67jECtipJoRCQx8vK3EPWEsF7FQVQtEKlU4UT02RvQJqXtVUATvVhVDSiaKhv3K6XrpuMZxM80nzKoZVqTKEohUrdxDZmrahxBZU8ymG1ak2hBJBZGu6Iro+a9iCbqs6hJJOIA39lShUAavcgm6rPoSilYyG/rLpeuSsgLW1IXZpIoRCQ3+5ogIushHfRzMhlEIVkXtEI53/tVbATlMhFBr6y1HiHrClCthpLoSilS6dcJ6aNix3BdR8aK0CdpoMoaQTTkO/UTrPOt8xnEzzQPMhhs1pNoSilS93ENmallViC6p5EMMmNR1CiSCyNW2Azitb0J9rPoSSLgQN/coVqoDNbkG3LSKEohWRhn6ddB5zVsAW2xC7LCaEQkO/PlEBV9uI72NRIZRCFZF7xBF03qiA51tcCIWGvl+Je8ClVcDOIkMoWjHTheOpqUHuCqjruMQK2FlsCCVdOBr6M9P50XmK4WS6frqOMVykRYdQtILmDiJb07OV2ILq+sVwsRYfQokgsjUtSOeDLeg4qwihpAtKQ7+QQhVw0VvQbasJoWhlpaGflz5/zgq41DbELqsKodDQzycqII34iVYXQilUEVd1j6jPSwXMY5UhFBr645W4B1xjBeysNoSilTdNAJ6aDpC7Aur8r7UCdlYdQkkTgIZ+T/pc+nwxnEznXec/hqu1+hCKVuLcQVza1rTEFlTnPYarRghDBJGt6Rn0OdiClkMIt6SJUaKhfz+GTSpUAVe/Bd1GCF+iFTpz++KOKkkMm5LeN434GRDCMxRqXzRVEaMC0oifASF8hTVXRL1PKuB8COEOa6yIJe4BqYC7EcJzaAVPEynnU9M7tQYxdwXUeaMCno8Q9pAmUtanpjUGUQHMXQF13mKIHQhhT1rRlxpEvY8CAaQC9kQIB4ggZt2aqgLF0EK/X+8jhpPp/BDAYQjhQGmCLaahX6gCsgUdiBCOoJW+9faFfl/OCkgbYjxCOFLL7YsSFZA2xHiEcIIWKyIVsD6EcKKWKiIVsE6EMANVgjQhq27o6+flrID6vFTAPAhhJmlCVtvQLxBAnoJmRAgziopYVRALBZAKmBEhzCyCWEVDX/8ucwDZghZACAtIE9Xe0Nfr9e9iOJk+jz5XDJERISxEFcPVvtDrclZA2hBlEcKCHO2LEhWQNkRZhLCwEhUxBe3M/52ivk8FbA8hnEHuipiCdvvliqixvh/DyaiA8yGEM1FFSRO7SEM/AshT0EYRwhmliZ29oZ++bDIHkKegMyOEM4uKmC2IOUUAqYAzI4QGEcRsW9Mc9H4IoAchNNGWr5aKGBWQLagJITRS5cnZvhiDNoQfITTL3b4YQr+XNoQfIayAoyJSAetBCCsxZ0WkAtaFEFZElSkFpOhTU/18KmBdCGFlUkCKPTXVz9XPjyEqQQgrFBUxaxAjgFTAChHCSkUQs2xN9XMIYL0IYcW0dZxaEaMCsgWtGCGsnCrY2PYFbYg2EMIGjGlf6PW0IdpACBuhinZ4ePhlDHfS66iA7SCEDTk6OvrDzZs3Pzg5OTmNb/2fTXLjxo0P9br4FhpACBvz4MGDT3+R3Lp1673Lly9/rief+qrx3t7e/sOHD/8SL0Uj9tLiGYcAHKiEgBkhBMwIIWBGCAEzQgiYEULAjBACZoQQMCOEgBkhBMwIIWBGCAEzQgiYEULAjBACZoQQMCOEgBkhBMwIIWBGCAEzQgiYEULAjBACZoQQMCOEgBkhBMwIIWBGCAEzQgiYEULAjBACZoQQMCOEgBkhBMwIIWBGCAEzQgiYEULAjBACZoQQMCOEgBkhBMwIIWBGCAEzQgiYEULAjBACZoQQMCOEgBkhBMwIIWBGCAEzQgiYEULAjBACZoQQMCOEgNWFC98Dx51YwXy8AmIAAAAASUVORK5CYII=";
                        if (sessionStorage.scrollTop != "undefined") {
                                $(window).scrollTop(sessionStorage.scrollTop);
                            }

                            //物件リスト一覧行をクリックすると、行の色を変更する 
                         $('[id*=gvBukken] tr td').on('click', function () {   
                             var btnId = document.getElementById("<%= tamitsumoriId.ClientID %>").value;   
                         
                             if (btnId != 1)
                             {
                                 var col = $(this).parent().children().index($(this));                      
                                 var title = $(this).closest("table").find("th").eq(col).text();
                                
                                  var row = $(this).parent();                                                                
                                  $("[id*=gvBukken] tr").each(function () {               
                                        if ($(this)[0] != row[0]) {
                                            $("td", this).removeClass("JC07Selected_row");
                                        }
                                   });

                                 //色付ける
                                  $("td", row).each(function () {
                                        if (!$(this).hasClass("JC07Selected_row")) {
                                            $(this).addClass("JC07Selected_row");
                   
                                        } else {
                                            $(this).removeClass("JC07Selected_row");
                    
                                        }
                                     });
                                  $("[id*=gvSubBukken] td", row).each(function () {
                                            $(this).removeClass("JC07Selected_row");
               
                                   });

                                 //行を展開する
                                 var expandvalue = $(this).closest("tr").find("[name*='IsExpanded']").val();                           
                                 var col = $(this).parent().children().index($(this));
                                 if (expandvalue == "1") {    
                              
                                       $(this).closest("tr").next().remove();
                                       var row = $(this).closest("tr");
                                       row.find("[name*='IsExpanded']").val("0");
                                     // $("[src*=img-buttonarrow]", $(this).closest("tr")).attr("src", "/Img/img-rightarrow.png");
                                     $("[id*=imgArrow]", $(this).closest("tr")).attr("src", imgUrl_rightArrow);
                              
                                   }
                                 else {
                                     //alert('c');
                                        $(this).closest("tr").after("<tr><td></td><td colspan = '999'>" + $("[id*=pnlSubBukken]", $(this).closest("tr")).html() + "</td></tr>");                                 
                                        var row = $(this).closest("tr");
                                        row.find("[name*='IsExpanded']").val("1");                                   
                                       // $("[src*=img-rightarrow]", $(this).closest("tr")).attr("src", "/Img/img-buttonarrow.png");
                                        $("[id*=imgArrow]", $(this).closest("tr")).attr("src", imgUrl_DownArrow);
                               
                                   }
                                
                             }
                         });
                           //物件リスト一覧の行をマウスオーバーすると行を変更する
                         $('[id*=gvBukken] td').hover(function () {  
                              var btnId = document.getElementById("<%= tamitsumoriId.ClientID %>").value;   
                           
                             if (btnId != 1) {
                                 var col = $(this).parent().children().index($(this));

                                 var row = $(this).parent();
                                 $("[id*=gvBukken] tr").each(function () {
                                     if ($(this)[0] != row[0]) {
                                         $("td", this).removeClass("JC07Selected_row");
                                     }
                                 });

                                 //色付ける
                                 $("td", row).each(function () {
                                     if (!$(this).hasClass("JC07Selected_row")) {
                                         $(this).addClass("JC07Selected_row");

                                     } else {
                                         $(this).removeClass("JC07Selected_row");

                                     }
                                 });
                                 $("[id*=gvSubBukken] td", row).each(function () {
                                     $(this).removeClass("JC07Selected_row");

                                 });
                             }
                             else
                             {
                                   document.getElementById("<%= tamitsumoriId.ClientID %>").value = "0";
                             }
                        });
                      
                         //物件詳細にあるコピー、削除したあと、物件詳細を表示する
                         $("[id*=IsExpanded]").each(function () {  
                             var flag = document.getElementById("<%= OpenSubgrid.ClientID %>").value;
                            <%-- var flagmitsu = document.getElementById("<%= Opensubgridmitsu.ClientID %>").value;
                             var flagEditsubGrid = document.getElementById("<%= EditSubgrid.ClientID %>").value;--%>
                             //alert('flag<<'+flag+ '<<<flagmitsu<<' + flagmitsu + '<<< flagEditsubGrid<<' + flagEditsubGrid +' <<< $(this).val()' + $(this).val());
                             //alert('flag<<' + flag);
                             
                             if ($(this).val() == "1") {
                                 if (flag == "1") {
                                   
                                     //|| flagmitsu == "1" || flagEditsubGrid == "1"
                                     $(this).closest("tr").find("[name*='IsExpanded']").val("1");
                                     $(this).closest("tr").after("<tr><td></td><td colspan = '999'>" + $("[id*=pnlSubBukken]", $(this).closest("tr")).html() + "</td></tr>")
                                    // $("[src*=img-rightarrow]", $(this).closest("tr")).attr("src", "/Img/img-buttonarrow.png");
                                     $("[id*=imgArrow]", $(this).closest("tr")).attr("src", imgUrl_DownArrow);
                                 }
                             }
                        });
                    });
                   
            </script>  
            </div>
      
    
</body>
    <script src="https://code.jquery.com/jquery-3.2.1.min.js"></script>
<script src="https://code.jquery.com/ui/1.12.1/jquery-ui.min.js"></script>
 <script src="../Scripts/cloudflare-jquery-ui-i18n.min.js"></script>
</html>
 </asp:Content>
