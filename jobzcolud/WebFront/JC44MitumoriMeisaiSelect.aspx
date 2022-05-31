<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="JC44MitumoriMeisaiSelect.aspx.cs" Inherits="jobzcolud.WebFront.JC44MitumoriMeisaiSelect" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <asp:PlaceHolder runat="server">
        <%: Scripts.Render("~/bundles/modernizr") %>
        <%: Styles.Render("~/style/StyleBundle1") %>
        <%: Scripts.Render("~/scripts/ScriptBundle1") %>
        <%: Styles.Render("~/style/UCStyleBundle") %>

    </asp:PlaceHolder>
    <webopt:BundleReference runat="server" Path="~/Content1/bootstrap" />
    <webopt:BundleReference runat="server" Path="~/Content1/css" />
    <style>
        th {
            position: -webkit-sticky;
            position: sticky !important;
            top: 0;
            background-color: rgb(242,242,242);
            border-color: rgb(242,242,242);
            border: 0px;
        }
    </style>

    <script type="text/javascript">
        function SetMeisaiOK()
        {
            window.parent.document.getElementById('HF_fMeisaiOK').value = "1";
            
        }
    </script>
</head>
<body class="bg-transparent">
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" EnablePageMethods="True" runat="server">
            <Scripts>
                <asp:ScriptReference Name="jquery" />
                <asp:ScriptReference Name="bootstrap" />
                <asp:ScriptReference Path="../Scripts/Common/FixFocus.js" />
                 <asp:ScriptReference Path="../Scripts/Common/Common.js" />
            </Scripts>
        </asp:ScriptManager>
         <asp:PlaceHolder runat="server">
            <%: Scripts.Render("~/bundles/jqueryui") %>
        </asp:PlaceHolder>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                     <div id="Div_Body" runat="server" style="background-color: transparent; padding-top: 150px;"><%--20220518 MyatNoe Added--%>
                    <div style="max-width: 1330px; margin-left: auto; margin-right: auto; background-color: white; padding: 10px 0px 0px 0px; overflow: hidden;"
                       id="divMitumoriSyosaiP" runat="server"><%--20220518 MyatNoe Update--%>
                    <div style="height: 65px; padding-top: 5px;">
                        <asp:Label ID="lblHeader" runat="server" Text="見積明細選択画面" CssClass="TitleLabel d-block align-content-center"></asp:Label>
                        <asp:Button ID="btnFusenHeaderCross" runat="server" Text="✕" CssClass="PopupCloseBtn" OnClick="btncancel_Click" />
                    </div>
                    <div class="Borderline"></div>


                    <%--<div id="Div5" runat="server" style=" height: 348px; margin-left: 20px; margin-right: 20px; overflow-x: auto;" onscroll="SetDivPosition()">--%>
                    <div id="Div7" runat="server" style=" height: 342px; width: auto; display: inline-block;padding:0px 0px 0px 15px !important;overflow-y:auto !important;margin-right:15px !important;">
                        <asp:UpdatePanel ID="updMitsumoriSyohinGrid" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                    <asp:GridView runat="server" ID="GV_MitumoriSyohin_Original" BorderColor="Transparent" AutoGenerateColumns="false" EmptyDataRowStyle-CssClass="JC10NoDataMessageStyle" CssClass="gvMitumoriSyohin GridViewStyleSyohin"
                                    ShowHeader="true" ShowHeaderWhenEmpty="true" RowStyle-CssClass="GridRow" CellPadding="0" Visible="false">
                                    <EmptyDataRowStyle CssClass="JC10NoDataMessageStyle" />
                                    <HeaderStyle Height="37px" BackColor="#F2F2F2" />
                                    <RowStyle CssClass="GridRow" Height="37px" />
                                    <SelectedRowStyle BackColor="#EBEBF5" />
                                    <Columns>
                                        <asp:TemplateField ItemStyle-CssClass="AlignCenter" HeaderStyle-CssClass="JC10MitumoriGridHeaderStyle JC10CheckBox">
                                            <ItemTemplate>
                                                 <div style="text-align: center;padding-left:2px;overflow: hidden;  display: -webkit-box;  -webkit-line-clamp: 1; -webkit-box-orient: vertical;word-break: break-all;">
                                                <asp:CheckBox ID="chkSelectSyouhin" runat="server" AutoPostBack="true" CssClass="M01AnkenGridCheck" TabIndex="-1" OnCheckedChanged="chkSelectSyouhin_CheckedChanged"/>
                                                <asp:Label ID="lblhdnStatus" runat="server" Text='<%#Eval("status") %>' CssClass="DisplayNone"></asp:Label>
                                                <asp:Label ID="lblfgenkatanka" runat="server" Text='<%#Eval("fgentankatanka") %>' CssClass="DisplayNone"></asp:Label>
                                                 <asp:Label ID="lblRowNo" runat="server" Text='<%#Eval("rowNo") %>' CssClass="DisplayNone"></asp:Label>
                                                     <asp:Label ID="lblfjitais" runat="server" Text='<%#Eval("fJITAIS") %>' CssClass="DisplayNone"></asp:Label>
                                                 <asp:Label ID="lblfjitaiq" runat="server" Text='<%#Eval("fJITAIQ") %>' CssClass="DisplayNone"></asp:Label>
                                                     </div>
                                            </ItemTemplate>
                                            <HeaderTemplate>
                                                <div class="grip" style="text-align: center;padding-left:2px;overflow: hidden;  display: -webkit-box;  -webkit-line-clamp: 1; -webkit-box-orient: vertical;word-break: break-all;">
                                                    <asp:Label ID="LB_chk" runat="server" Text="checkbox" style="display:none;"></asp:Label>
                                                    </div>
                                            </HeaderTemplate>
                                            <HeaderStyle CssClass="JC10MitumoriGridHeaderStyle JC10CheckBox" BorderWidth="2px" />
                                            <ItemStyle CssClass="JC10CheckBox AlignCenter" />
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-CssClass="AlignCenter" HeaderStyle-CssClass="JC10MitumoriGridHeaderStyle JC10ButtonCol" Visible="false">
                                            <ItemTemplate>
                                                 <div class="grip" style="padding-top: 0px;vertical-align: middle; text-align: center;">
                                                <asp:UpdatePanel ID="updSyohinAddBtn" runat="server" UpdateMode="Conditional">
                                                    <Triggers>
                                                         <asp:AsyncPostBackTrigger ControlID="btnSyouhinAdd" EventName="Click"/>
                                                     </Triggers>
                                                     <ContentTemplate>
                                                    <asp:Button ID="btnSyouhinAdd" runat="server" Text="＋" CssClass="JC09GridGrayBtn" onmousedown="getTantouBoardScrollPosition();" Width="30px" Height="28px" TabIndex="-1"/>
                                                   </ContentTemplate>
                                                </asp:UpdatePanel>
                                                     </div>
                                            </ItemTemplate>
                                            <HeaderTemplate>
                                                <div class="grip" style="padding-top: 0px;vertical-align: middle; text-align: center;">
                                                <asp:Label ID="LBAddSyouhin" runat="server" Text="AddSyouhin" style="display:none;"></asp:Label>
                                                    </div>
                                            </HeaderTemplate>
                                            <HeaderStyle CssClass="JC10MitumoriGridHeaderStyle JC10ButtonCol" BorderWidth="2px" />
                                            <ItemStyle CssClass="AlignCenter JC10ButtonCol" />
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-CssClass="AlignCenter" HeaderStyle-CssClass="JC10MitumoriGridHeaderStyle JC10ButtonCol" Visible="false">
                                            <ItemTemplate>
                                                <div class="grip" style="padding-top: 0px;vertical-align: middle; text-align: center;">
                                                    <asp:UpdatePanel ID="updSyohinCopyBtn" runat="server" UpdateMode="Conditional">
                                                    <Triggers>
                                                         <asp:AsyncPostBackTrigger ControlID="btnSyohinCopy" EventName="Click"/>
                                                     </Triggers>
                                                     <ContentTemplate>
                                                     <asp:Button ID="btnSyohinCopy" runat="server" Text="コ" CssClass="JC09GridGrayBtn" onmousedown="getTantouBoardScrollPosition();" Width="30px" Height="28px" TabIndex="-1"  />
                                                   </ContentTemplate>
                                                </asp:UpdatePanel>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderTemplate>
                                                  <div class="grip" style="padding-top: 0px;vertical-align: middle; text-align: center;">
                                                <asp:Label ID="LBCopySyouhin" runat="server" Text="CopySyouhin" style="display:none;"></asp:Label>
                                                      </div>
                                            </HeaderTemplate>
                                            <HeaderStyle CssClass="JC10MitumoriGridHeaderStyle JC10ButtonCol" BorderWidth="2px"/>
                                            <ItemStyle CssClass="AlignCenter JC10ButtonCol" />
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-CssClass="AlignCenter" HeaderStyle-CssClass="JC10MitumoriGridHeaderStyle JC10ButtonCol" Visible="false">
                                            <ItemTemplate>
                                                <div class="grip" style="padding-top: 0px;vertical-align: middle; text-align: center;">
                                                <asp:Button ID="btnSyohinShosai" runat="server" Text="詳" CssClass="JC09GridGrayBtn" onmousedown="getTantouBoardScrollPosition();" Width="30px" Height="28px" TabIndex="-1"  OnClientClick="PopupOpenButton()"/>
                                                    <div style="padding-right:3px;">
                                                <asp:TextBox ID="txtMidashi" runat="server" Text='' Height="25px" Width="100%" MaxLength="10" CssClass="form-control TextboxStyle JC10GridTextBox" autocomplete="off" AutoPostBack="true" Visible="false" placeholder="見出しを入力"></asp:TextBox>
                                                <asp:TextBox ID="txtSyokei" runat="server" Text='小計' Height="25px" Width="100%" MaxLength="10" CssClass="form-control TextboxStyle JC10GridTextBox" autocomplete="off" AutoPostBack="true" Visible="false" style="text-align:right;" Enabled="false"></asp:TextBox>
                                                        </div>
                                                 </div>
                                            </ItemTemplate>
                                            <HeaderTemplate>
                                                <div class="grip" style="padding-top: 0px;vertical-align: middle; text-align: center;">
                                                <asp:Label ID="LB_SyouhinSyosai" runat="server" Text="SyouhinSyosai" style="display:none;"></asp:Label>
                                                    </div>
                                            </HeaderTemplate>
                                            <HeaderStyle CssClass="JC10MitumoriGridHeaderStyle JC10ButtonCol" BorderWidth="2px"/>
                                            <ItemStyle CssClass="AlignCenter JC10ButtonCol"/>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-Width="30px" Visible="false">
                                            <ItemTemplate>   
                                                <%-- <div class="grip" style="display:flex;align-content:center;align-items:center;align-items: center;justify-content: center;z-index:2;">
                                                         <asp:HoverMenuExtender ID="HoverMenuExtender" runat="server" PopupControlID="PopupMenuSyo"
                                                    TargetControlID="PopupMenuBtnSyo" PopupPosition="bottom">
                                                </asp:HoverMenuExtender>
                                                <asp:Panel ID="PopupMenuSyo" runat="server" CssClass="dropdown-menu fontcss " aria-labelledby="dropdownMenuButton" Style="display: none; min-width: 1rem; width: 122px !important; z-index:10000;margin-top:0px;padding:0px !important;border-radius:1px;">
                                                    <asp:LinkButton ID="LK_Syou" class="fontcss dropdown-item" runat="server" Text='文字を小さくする' OnClick="LK_Syou_Click" style="margin-right:10px;font-size:12px !important;padding:4px 12px !important;"></asp:LinkButton>
                                                     <asp:LinkButton ID="LK_Kyo" class="fontcss dropdown-item" runat="server" Text='文字を強調する' OnClick="LK_Kyo_Click" style="margin-right:10px;font-size:12px !important;padding:4px 12px !important;"></asp:LinkButton>
                                                </asp:Panel>
                                                <asp:Panel ID="PopupMenuBtnSyo" runat="server" CssClass="modalPopup" Style="width: 20px;">
                                                    <button class="btn dropdown-toggle" type="button" id="dropdownMenuButton" data-toggle="dropdown" 
                                                      aria-haspopup="true" aria-expanded="false" style="border:1px solid gainsboro;width:20px; height:20px;padding:0px 3px 0px 1px;margin:0;margin-top:-3px;">
                                                </asp:Panel>
                                                     </div>--%>
                                            </ItemTemplate>
                                            <HeaderTemplate>
                                                <div class="grip" style="display:flex;align-content:center;align-items:center;align-items: center;justify-content: center;z-index:2;">
                                                     <asp:Label ID="LB_drop" runat="server" Text="dropdown" style="display:none;"></asp:Label>
                                                    </div>
                                            </HeaderTemplate>
                                            <ItemStyle Width="25px" CssClass="JC09DropDown JC10DropCol" />
                                            <HeaderStyle BorderWidth="2px" CssClass="JC09DropDown JC10DropCol" />
                                        </asp:TemplateField> 
                                        <asp:TemplateField ItemStyle-CssClass="JC10MitumoriCheckboxCol AlignCenter" HeaderStyle-CssClass="JC10MitumoriGridCheckboxHeaderCol JC10MitumoriGridHeaderStyle" Visible="false">
                                            <ItemTemplate>
                                                <div class="grip" style="text-align: center;padding-left:2px;overflow: hidden;  display: -webkit-box;  -webkit-line-clamp: 1; -webkit-box-orient: vertical;word-break: break-all;">
                                                    <asp:Label runat="server" ID="lblKubun" Text='<%#Eval("sKUBUN","{0}")%>' ToolTip='<%#Eval("sKUBUN","{0}")%>' style="cursor:default;user-select:none;"></asp:Label>
                                                    <asp:Label runat="server" ID="lblKubun1" Text='' ToolTip='' style="cursor:default;user-select:none;" Visible="false"></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderTemplate>
                                                <div class="grip" style="text-align: center;padding-left:2px;overflow: hidden;  display: -webkit-box;  -webkit-line-clamp: 1; -webkit-box-orient: vertical;word-break: break-all;">
                                                <asp:Label ID="LB_Kubun" runat="server" Text="Kubun"></asp:Label>
                                                    </div>
                                            </HeaderTemplate>
                                            <HeaderStyle CssClass="JC10MitumoriGridCheckboxHeaderCol JC10MitumoriGridHeaderStyle" BorderWidth="2px"/>
                                            <ItemStyle CssClass="JC10MitumoriCheckboxCol AlignCenter" />
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-CssClass="AlignLeft" HeaderStyle-CssClass="JC10MitumoriGridHeaderStyle">
                                            <ItemTemplate>
                                                <div class="grip" style="min-width:90px;text-align: left;padding-right: 4px;padding-left:1px;overflow: hidden;  display: -webkit-box;  -webkit-line-clamp: 1; -webkit-box-orient: vertical;word-break: break-all;">
                                                <asp:UpdatePanel ID="updtxtcSYOUHIN" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                <%--<asp:TextBox ID="txtcSYOHIN" runat="server" Text=' <%# Bind("cSYOHIN","{0}") %>' Width="100%" Height="25px" MaxLength="10" CssClass="form-control TextboxStyle JC10GridTextBox" autocomplete="off" AutoPostBack="true" onkeypress="return isNumberKey()" onkeydown="gridtextboxKeydown()" OnTextChanged="txtcSYOHIN_TextChanged"></asp:TextBox>--%>
                                                        <asp:Label ID="LB_cSYOHIN" runat="server" Text='<%#Eval("cSYOHIN","{0}")%>' ToolTip='<%#Eval("cSYOHIN","{0}")%>' style="cursor:default;user-select:none;" TabIndex="-1"></asp:Label>
                                                        </ContentTemplate>
                                                </asp:UpdatePanel>
                                                    </div>
                                            </ItemTemplate>
                                            <HeaderTemplate>
                                                <div class="grip" style="min-width:90px;text-align: left;padding-right: 4px;padding-left:1px;overflow: hidden;  display: -webkit-box;  -webkit-line-clamp: 1; -webkit-box-orient: vertical;word-break: break-all;">
                                                <asp:Label ID="lblcSyohin" runat="server" Text="商品コード" CssClass="d-inline-block" style="padding-left:4px;"></asp:Label>
                                                    </div>
                                            </HeaderTemplate>
                                            <HeaderStyle CssClass="JC10MitumoriGridHeaderStyle" BorderWidth="2px"/>
                                            <ItemStyle CssClass="AlignLeft" />
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-CssClass="AlignCenter" HeaderStyle-CssClass="JC10MitumoriGridHeaderStyle JC10ButtonCol" Visible="false">
                                            <ItemTemplate>
                                                <div class="grip" style="padding-top: 0px;vertical-align: middle; text-align: center;">
                                                    <asp:UpdatePanel ID="updbtnSyohinSelectn" runat="server" UpdateMode="Conditional">
                                                    <Triggers>
                                                         <asp:AsyncPostBackTrigger ControlID="btnSyohinSelect" EventName="Click"/>
                                                     </Triggers>
                                                     <ContentTemplate>
                                                    <asp:Button ID="btnSyohinSelect" runat="server" Text="商" CssClass="JC09GridGrayBtn" onmousedown="getTantouBoardScrollPosition();" Width="30px" Height="28px" TabIndex="-1"  OnClientClick="PopupOpenButton()"/>
                                                         </ContentTemplate>
                                                </asp:UpdatePanel>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderTemplate>
                                                <div class="grip" style="padding-top: 0px;vertical-align: middle; text-align: center;">
                                                <asp:Label ID="LB_Syouhin" runat="server" Text="Syouhin" style="display:none;"></asp:Label>
                                                    </div>
                                            </HeaderTemplate>
                                            <HeaderStyle CssClass="JC10MitumoriGridHeaderStyle JC10ButtonCol" BorderWidth="2px"/>
                                            <ItemStyle CssClass="AlignCenter JC10ButtonCol" />
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-CssClass="AlignLeft" HeaderStyle-CssClass="JC10MitumoriGridHeaderStyle">
                                            <ItemTemplate>
                                                  <div class="grip" style="min-width:40px;text-align: left;padding-right: 2px;padding-left:1px;overflow: hidden;  display: -webkit-box;  -webkit-line-clamp: 1; -webkit-box-orient: vertical;word-break: break-all;">
                                                <asp:UpdatePanel ID="updtxtsSYOHIN" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                <%--<asp:TextBox ID="txtsSYOHIN" runat="server"  Text=' <%# Bind("sSYOHIN","{0}") %>' Width="100%" Height="25px" MaxLength="10000" CssClass="form-control TextboxStyle JC10GridTextBox txtsSyohin" autocomplete="off" AutoPostBack="true" OnTextChanged="txtsSYOHIN_TextChanged"></asp:TextBox>--%>
                                                       <asp:Label ID="LB_sSYOHIN" runat="server" Text='<%#Eval("sSYOHIN","{0}")%>' ToolTip='<%#Eval("sSYOHIN","{0}")%>' style="cursor:default;user-select:none;" TabIndex="-1"></asp:Label>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                                      </div>
                                            </ItemTemplate>
                                            <HeaderTemplate>
                                                <div class="grip" style="min-width:40px;text-align: left;padding-right: 2px;padding-left:1px;overflow: hidden;  display: -webkit-box;  -webkit-line-clamp: 1; -webkit-box-orient: vertical;word-break: break-all;">
                                                <asp:Label ID="lblsSyohin" runat="server" Text="商品名"  style="padding-left:4px;"></asp:Label>
                                                    </div>
                                            </HeaderTemplate>
                                            <HeaderStyle CssClass="JC10MitumoriGridHeaderStyle" BorderWidth="2px"/>
                                            <ItemStyle CssClass="AlignLeft" />
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-CssClass="AlignRight" HeaderStyle-CssClass="JC10MitumoriGridHeaderStyle">
                                            <ItemTemplate>
                                                <div class="grip" style="min-width:40px;text-align: right;padding-right: 4px;overflow: hidden;  display: -webkit-box;  -webkit-line-clamp: 1; -webkit-box-orient: vertical;word-break: break-all;">
                                                <asp:UpdatePanel ID="updtxtnSURYO" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                <%--<asp:TextBox ID="txtnSURYO" runat="server" Text=' <%# Bind("nSURYO","{0}") %>' Width="100%" Height="25px" MaxLength="10" CssClass="form-control TextboxStyle JC10GridTextBox" autocomplete="off" AutoPostBack="true" style="text-align:right;" onkeypress="return isNumberKey()" oninput="validateSuryo(this);" OnTextChanged="txtnSURYO_TextChanged"></asp:TextBox>--%>
                                                        <asp:Label ID="LB_nSURYO" runat="server" Text='<%#Eval("nSURYO","{0}")%>' ToolTip='<%#Eval("nSURYO","{0}")%>' style="cursor:default;user-select:none;" TabIndex="-1"></asp:Label>
                                                     </ContentTemplate>
                                                </asp:UpdatePanel>
                                                    </div>
                                            </ItemTemplate>
                                            <HeaderTemplate>
                                                <div class="grip" style="min-width:40px;text-align: right;padding-right: 4px;overflow: hidden;  display: -webkit-box;  -webkit-line-clamp: 1; -webkit-box-orient: vertical;word-break: break-all;">
                                                <asp:Label ID="lblSyuryo" runat="server" Text="数量" CssClass="d-inline-block"></asp:Label>
                                                    </div>
                                            </HeaderTemplate>
                                            <HeaderStyle CssClass="JC10MitumoriGridHeaderStyle" BorderWidth="2px"/>
                                            <ItemStyle/>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-CssClass="AlignLeft" HeaderStyle-CssClass="JC10MitumoriGridHeaderStyle">
                                            <ItemTemplate>
                                                 <div class="grip" style="text-align: left;overflow: hidden;  display: -webkit-box;  -webkit-line-clamp: 1; -webkit-box-orient: vertical;word-break: break-all;">
                                                 <asp:UpdatePanel ID="updTani" runat="server" UpdateMode="Conditional"> 
                                                          <ContentTemplate>
                                                    <asp:Label ID="lblcTANI" runat="server" Text='<%#Eval("cTANI","{0}")%>' ToolTip='<%#Eval("cTANI","{0}")%>' style="cursor:default;user-select:none;" TabIndex="-1"></asp:Label>

                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                                     </div>
                                            </ItemTemplate>
                                            <HeaderTemplate>
                                                <div class="grip" style="text-align: left;overflow: hidden;  display: -webkit-box;  -webkit-line-clamp: 1; -webkit-box-orient: vertical;word-break: break-all;">
                                                <asp:Label ID="lblTani" runat="server" Text="単位" CssClass="d-inline-block" style="padding-left:4px;"></asp:Label>
                                                    </div>
                                            </HeaderTemplate>
                                            <HeaderStyle CssClass="JC10MitumoriGridHeaderStyle" BorderWidth="2px"/>
                                            <ItemStyle CssClass="AlignCenter" />
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-CssClass="AlignRight" HeaderStyle-CssClass="JC10MitumoriGridHeaderStyle">
                                            <ItemTemplate>
                                                <div class="grip" style="min-width:60px;text-align: right;padding-right: 4px;overflow: hidden;  display: -webkit-box;  -webkit-line-clamp: 1; -webkit-box-orient: vertical;word-break: break-all;">
                                                <asp:UpdatePanel ID="updtxtnTANKA" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <%--<asp:TextBox ID="txtnTANKA" runat="server" Text=' <%# Bind("nTANKA","{0:#,##0.##}") %>' Width="100%" Height="25px" MaxLength="10" CssClass="form-control TextboxStyle JC10GridTextBox" autocomplete="off" AutoPostBack="true" style="text-align:right;" onkeypress="return isNumberKey()" oninput="validateSuryo(this);" OnTextChanged="txtnTANKA_TextChanged"></asp:TextBox>--%>
                                                        <asp:Label ID="LB_nTANKA" runat="server" Text='<%#Eval("nTANKA","{0:#,##0.##}")%>' ToolTip='<%#Eval("nTANKA","{0:#,##0.##}")%>' style="cursor:default;user-select:none;" TabIndex="-1"></asp:Label>
                                                        </ContentTemplate>
                                                </asp:UpdatePanel>
                                                    </div>
                                            </ItemTemplate>
                                            <HeaderTemplate>
                                                <div class="grip" style="min-width:60px;text-align: right;padding-right: 4px;overflow: hidden;  display: -webkit-box;  -webkit-line-clamp: 1; -webkit-box-orient: vertical;word-break: break-all;">
                                                <asp:Label ID="lblnTankaHeader" runat="server" Text="標準単価" CssClass="d-inline-block"></asp:Label>
                                                    </div>
                                            </HeaderTemplate>
                                            <HeaderStyle CssClass="JC10MitumoriGridHeaderStyle" BorderWidth="2px"/>
                                            <ItemStyle CssClass="AlignRight" />
                                        </asp:TemplateField>
                                        <asp:TemplateField  HeaderStyle-CssClass="JC10MitumoriGridHeaderStyle">
                                            <ItemTemplate>
                                                <div class="grip" style="min-width:40px;text-align: right;padding-right: 4px;overflow: hidden;  display: -webkit-box;  -webkit-line-clamp: 1; -webkit-box-orient: vertical;word-break: break-all;">
                                                <asp:Label runat="server" ID="lblTanka" Text='<%#Eval("nSIKIRITANKA","{0:#,##0.##}")%>' ToolTip='<%#Eval("nSIKIRITANKA","{0:#,##0.##}")%>' style="cursor:default;user-select:none;" TabIndex="-1"></asp:Label>
                                                    </div>
                                            </ItemTemplate>
                                            <HeaderTemplate>
                                                <div class="grip" style="min-width:40px;text-align: right;padding-right: 4px;overflow: hidden;  display: -webkit-box;  -webkit-line-clamp: 1; -webkit-box-orient: vertical;word-break: break-all;">
                                                <asp:Label ID="lblTankaHeader" runat="server" Text="単価" CssClass="d-inline-block"></asp:Label>
                                                    </div>
                                            </HeaderTemplate>
                                            <HeaderStyle CssClass="JC10MitumoriGridHeaderStyle" BorderWidth="2px"/>
                                            <ItemStyle/>
                                        </asp:TemplateField>
                                        <asp:TemplateField  HeaderStyle-CssClass="JC10MitumoriGridHeaderStyle">
                                            <ItemTemplate>
                                                <div class="grip" style="min-width:60px;text-align: right;padding-right: 4px;overflow: hidden;  display: -webkit-box;  -webkit-line-clamp: 1; -webkit-box-orient: vertical;word-break: break-all;">
                                                <asp:Label runat="server" ID="lblTankaGokei" Text='<%#Eval("nTANKAGOUKEI","{0:#,##0.##}")%>' ToolTip='<%#Eval("nTANKAGOUKEI","{0:#,##0.##}")%>' style="cursor:default;user-select:none;" TabIndex="-1"></asp:Label>
                                                    </div>
                                            </ItemTemplate>
                                            <HeaderTemplate>
                                                <div class="grip" style="min-width:60px;text-align: right;padding-right: 4px;overflow: hidden;  display: -webkit-box;  -webkit-line-clamp: 1; -webkit-box-orient: vertical;word-break: break-all;">
                                                <asp:Label ID="lblTankaGokeiHeader" runat="server" Text="合計金額" CssClass="d-inline-block"></asp:Label>
                                                    </div>
                                            </HeaderTemplate>
                                            <HeaderStyle CssClass="JC10MitumoriGridHeaderStyle" BorderWidth="2px"/>
                                            <ItemStyle/>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-CssClass="AlignRight" HeaderStyle-CssClass="JC10MitumoriGridHeaderStyle">
                                            <ItemTemplate>
                                                <div class="grip" style="min-width:60px;text-align: right;padding-right: 2px;overflow: hidden;  display: -webkit-box;  -webkit-line-clamp: 1; -webkit-box-orient: vertical;word-break: break-all;">
                                                <asp:UpdatePanel ID="updtxtnGENKATANKA" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                <%--<asp:TextBox ID="txtnGENKATANKA" runat="server" Text=' <%# Bind("nGENKATANKA","{0:#,##0.##}") %>' Width="100%" Height="25px" MaxLength="10" CssClass="form-control TextboxStyle JC10GridTextBox" autocomplete="off" AutoPostBack="true" style="text-align:right;" onkeypress="return isNumberKey()" oninput="validateSuryo(this);" OnTextChanged="txtnGENKATANKA_TextChanged"></asp:TextBox>--%>
                                                        <asp:Label runat="server" ID="LB_nGENKATANKA" Text='<%#Eval("nGENKATANKA","{0:#,##0.##}")%>' ToolTip='<%#Eval("nGENKATANKA","{0:#,##0.##}")%>' style="cursor:default;user-select:none;" TabIndex="-1"></asp:Label>
                                                        </ContentTemplate>
                                                </asp:UpdatePanel>
                                                    </div>
                                            </ItemTemplate>
                                            <HeaderTemplate>
                                                <div class="grip" style="min-width:60px;text-align: right;padding-right: 2px;overflow: hidden;  display: -webkit-box;  -webkit-line-clamp: 1; -webkit-box-orient: vertical;word-break: break-all;">
                                                <asp:Label ID="lblnGENKATANKA" runat="server" Text="原価単価" CssClass="d-inline-block"></asp:Label>
                                                    </div>
                                            </HeaderTemplate>
                                            <HeaderStyle CssClass="JC10MitumoriGridHeaderStyle" BorderWidth="2px"/>
                                            <ItemStyle CssClass="AlignRight" />
                                        </asp:TemplateField>
                                         <asp:TemplateField ItemStyle-CssClass="AlignRight" HeaderStyle-CssClass="JC10MitumoriGridHeaderStyle">
                                            <ItemTemplate>
                                                <div class="grip" style="min-width:40px;text-align: right;padding-right: 2px;overflow: hidden;  display: -webkit-box;  -webkit-line-clamp: 1; -webkit-box-orient: vertical;word-break: break-all;">
                                                <asp:UpdatePanel ID="updtxtnRITU" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                <%--<asp:TextBox ID="txtnRITU" runat="server" Text=' <%# Bind("nRITU","{0:#,##0.##}") %>' Width="100%" Height="25px" MaxLength="10" CssClass="form-control TextboxStyle JC10GridTextBox" autocomplete="off" AutoPostBack="true" style="text-align:right;" onkeypress="return isNumberKey()" oninput="validateRitu(this);" OnTextChanged="txtnRITU_TextChanged"></asp:TextBox>--%>
                                                        <asp:Label runat="server" ID="LB_nRITU" Text='<%#Eval("nRITU","{0:#,##0.##}")%>' ToolTip='<%#Eval("nRITU","{0:#,##0.##}")%>' style="cursor:default;user-select:none;" TabIndex="-1"></asp:Label>
                                                        </ContentTemplate>
                                                </asp:UpdatePanel>
                                                    </div>
                                            </ItemTemplate>
                                            <HeaderTemplate>
                                                <div class="grip" style="min-width:40px;text-align: right;padding-right:2px;overflow: hidden;  display: -webkit-box;  -webkit-line-clamp: 1; -webkit-box-orient: vertical;word-break: break-all;">
                                                <asp:Label ID="lblnRITU" runat="server" Text="掛率" CssClass="d-inline-block"></asp:Label>
                                                    </div>
                                            </HeaderTemplate>
                                            <HeaderStyle CssClass="JC10MitumoriGridHeaderStyle" BorderWidth="2px"/>
                                            <ItemStyle CssClass="AlignRight" />
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-CssClass="AlignRight" HeaderStyle-CssClass="JC10MitumoriGridHeaderStyle">
                                            <ItemTemplate>
                                                <div class="grip" style="min-width:60px;text-align: right;padding-right: 4px;overflow: hidden;  display: -webkit-box;  -webkit-line-clamp: 1; -webkit-box-orient: vertical;word-break: break-all;">
                                                <asp:Label runat="server" ID="lblGenkaGokei" Text='<%#Eval("nGENKAGOUKEI","{0:#,##0.##}")%>' ToolTip='<%#Eval("nGENKAGOUKEI","{0:#,##0.##}")%>' TabIndex="-1" style="cursor:default;user-select:none;"></asp:Label>
                                                    </div>
                                            </ItemTemplate>
                                            <HeaderTemplate>
                                                <div class="grip" style="min-width:60px;text-align: right;padding-right: 4px;overflow: hidden;  display: -webkit-box;  -webkit-line-clamp: 1; -webkit-box-orient: vertical;word-break: break-all;">
                                                <asp:Label ID="lblGenkaGokeiHeader" runat="server" Text="原価合計" CssClass="d-inline-block"></asp:Label>
                                                    </div>
                                            </HeaderTemplate>
                                            <HeaderStyle CssClass="JC10MitumoriGridHeaderStyle" BorderWidth="2px"/>
                                            <ItemStyle CssClass="AlignRight" />
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-CssClass="AlignRight" HeaderStyle-CssClass="JC10MitumoriGridHeaderStyle">
                                            <ItemTemplate>
                                                <div class="grip" style="min-width:40px;text-align: right;padding-right: 4px;overflow: hidden;  display: -webkit-box;  -webkit-line-clamp: 1; -webkit-box-orient: vertical;word-break: break-all;">
                                                <asp:Label runat="server" ID="lblnARARI" Text='<%#Eval("nARARI")%>' ToolTip='<%#Eval("nARARI","{0:#,##0.##}")%>' TabIndex="-1" style="cursor:default;user-select:none;"></asp:Label>
                                                    </div>
                                            </ItemTemplate>
                                            <HeaderTemplate>
                                                 <div class="grip" style="min-width:40px;text-align: right;padding-right: 4px;overflow: hidden;  display: -webkit-box;  -webkit-line-clamp: 1; -webkit-box-orient: vertical;word-break: break-all;">
                                                <asp:Label ID="lblnARARIHeader" runat="server" Text="粗利" CssClass="d-inline-block"></asp:Label>
                                                     </div>
                                            </HeaderTemplate>
                                            <HeaderStyle CssClass="JC10MitumoriGridHeaderStyle" BorderWidth="2px"/>
                                            <ItemStyle CssClass="AlignRight" />
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-CssClass="AlignRight" HeaderStyle-CssClass="JC10MitumoriGridHeaderStyle">
                                            <ItemTemplate>
                                                <div class="grip" style="min-width:50px;text-align: right;padding-right: 4px;overflow: hidden;  display: -webkit-box;  -webkit-line-clamp: 1; -webkit-box-orient: vertical;word-break: break-all;">
                                                <asp:Label runat="server" ID="lblnARARISu" Text='<%#Eval("nARARISu")%>' ToolTip='<%#Eval("nARARISu")%>' TabIndex="-1" style="cursor:default;user-select:none;"></asp:Label>
                                                    </div>
                                            </ItemTemplate>
                                            <HeaderTemplate>
                                                <div class="grip" style="min-width:50px;text-align: right;padding-right: 4px;overflow: hidden;  display: -webkit-box;  -webkit-line-clamp: 1; -webkit-box-orient: vertical;word-break: break-all;">
                                                <asp:Label ID="lblnnARARISuHeader" runat="server" Text="粗利率" CssClass="d-inline-block"></asp:Label>
                                                    </div>
                                            </HeaderTemplate>
                                            <HeaderStyle CssClass="JC10MitumoriGridHeaderStyle" BorderWidth="2px"/>
                                            <ItemStyle CssClass="AlignRight" />
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-CssClass="AlignCenter drag" HeaderStyle-CssClass="JC10MitumoriGridHeaderStyle JC10ButtonCol" Visible="false"><%--20220504Myatnoe,Chaw Added--%>
                                            <ItemTemplate>
                                                <div class="grip" style="padding-top: 0px;vertical-align: middle; text-align: center;" tabindex="-1">
                                                    <span class="dragBtn" tabindex="-1">三</span>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderTemplate>
                                                <div class="grip" style="padding-top: 0px;vertical-align: middle; text-align: center;" tabindex="-1">
                                                <asp:Label ID="LB_drag" runat="server" Text="drag" style="display:none;"></asp:Label>
                                                    </div>
                                            </HeaderTemplate>
                                            <HeaderStyle CssClass="JC10MitumoriGridHeaderStyle JC10ButtonCol" BorderWidth="2px"/>
                                            <ItemStyle CssClass="AlignCenter JC10ButtonCol" />
                                        </asp:TemplateField>
                                       <%-- <asp:TemplateField ItemStyle-CssClass="JC10MitumoriGridPludBtnCol AlignCenter" HeaderStyle-CssClass="JC10MitumoriGridPludBtnHeaderCol JC10MitumoriGridHeaderStyle">
                                            <ItemTemplate>
                                                <div style="display:none;">
                                                    <asp:Button ID="btnSyohinDelete" runat="server" Text="削" CssClass="JC09GridGrayBtn" onmousedown="getTantouBoardScrollPosition();" Width="30px" Height="28px" OnClick="btnSyohinDelete_Click" />
                                                </div>
                                            </ItemTemplate>
                                            <HeaderTemplate>
                                                <asp:Label ID="Label1" runat="server" Text="" CssClass="d-inline-block"></asp:Label>
                                            </HeaderTemplate>
                                            <HeaderStyle CssClass="JC10MitumoriGridPludBtnHeaderCol JC10MitumoriGridHeaderStyle" />
                                            <ItemStyle CssClass="JC10MitumoriGridPludBtnCol AlignCenter" />
                                        </asp:TemplateField>--%>
                                        <asp:TemplateField HeaderStyle-Width="30px" Visible="false">
                                            <ItemTemplate>   
                                                 <%--<div class="grip" style="display:flex;align-content:center;align-items:center;align-items: center;justify-content: center;z-index:2;">
                                                         <asp:HoverMenuExtender ID="HoverMenuExtender2" runat="server" PopupControlID="PopupMenu"
                                                    TargetControlID="PopupMenuBtn" PopupPosition="left">
                                                </asp:HoverMenuExtender>
                                                <asp:Panel ID="PopupMenu" runat="server" CssClass="dropdown-menu fontcss " aria-labelledby="dropdownMenuButton" Style="display: none; min-width: 1rem; width: 5rem; z-index:10000;">
                                                    <asp:LinkButton ID="lnkbtnSyohinDelete" class="dropdown-item" runat="server" Text='削除' style="margin-right:10px;font-size:13px;" OnClick="btnSyohinDelete_Click"></asp:LinkButton>
                                                </asp:Panel>
                                                <asp:Panel ID="PopupMenuBtn" runat="server" CssClass="modalPopup" Style="width: 20px;">
                                                    <button class="btn dropdown-toggle" type="button" id="dropdownMenuButton" data-toggle="dropdown" 
                                                      aria-haspopup="true" aria-expanded="false" style="border:1px solid gainsboro;width:20px; height:20px;padding:0px 3px 0px 1px;margin:0;margin-top:-3px;">
                                                </asp:Panel>
                                                     </div>--%>
                                            </ItemTemplate>
                                            <HeaderTemplate>
                                                <div class="grip" style="display:flex;align-content:center;align-items:center;align-items: center;justify-content: center;z-index:2;">
                                                     <asp:Label ID="LB_drop" runat="server" Text="dropdown" style="display:none;"></asp:Label>
                                                    </div>
                                            </HeaderTemplate>
                                            <ItemStyle Width="25px" CssClass="JC09DropDown JC10DropCol" />
                                            <HeaderStyle BorderWidth="2px" CssClass="JC09DropDown JC10DropCol" />
                                        </asp:TemplateField> 
                                    </Columns>
                                </asp:GridView>

                                <asp:GridView runat="server" ID="GV_MitumoriSyohin" BorderColor="Transparent" AutoGenerateColumns="false" EmptyDataRowStyle-CssClass="JC10NoDataMessageStyle" CssClass="gvMitumoriSyohin GridViewStyleSyohin"
                                    ShowHeader="true" ShowHeaderWhenEmpty="true" RowStyle-CssClass="GridRow" CellPadding="0" >
                                    <EmptyDataRowStyle CssClass="JC10NoDataMessageStyle" />
                                    <HeaderStyle Height="37px" BackColor="#F2F2F2" />
                                    <RowStyle CssClass="GridRow" Height="37px" />
                                    <SelectedRowStyle BackColor="#EBEBF5" />
                                    <Columns>
                                    </Columns>
                                </asp:GridView>

                                <asp:GridView runat="server" ID="GV_Syosai" BorderColor="Transparent" AutoGenerateColumns="false" EmptyDataRowStyle-CssClass="JC10NoDataMessageStyle"
                                    ShowHeader="true" ShowHeaderWhenEmpty="true" RowStyle-CssClass="GridRow" CellPadding="0" Visible="false">
                                    <EmptyDataRowStyle CssClass="JC10NoDataMessageStyle" />
                                    <HeaderStyle Height="37px" BackColor="#F2F2F2" />
                                    <RowStyle CssClass="GridRow" Height="37px" />
                                    <SelectedRowStyle BackColor="#EBEBF5" />
                                    <Columns>
                                        <asp:TemplateField ItemStyle-CssClass="JC10MitumoriCheckboxCol AlignCenter" HeaderStyle-CssClass="JC10MitumoriGridCheckboxHeaderCol JC10MitumoriGridHeaderStyle">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkSelectSyouhin" runat="server" AutoPostBack="true" CssClass="M01AnkenGridCheck" />
                                                <asp:Label ID="lblhdnStatus" runat="server" Text='<%#Eval("status") %>' CssClass="DisplayNone"></asp:Label>
                                                <asp:Label ID="lblRowNo" runat="server" Text='<%#Eval("rowNo") %>' CssClass="DisplayNone"></asp:Label>
                                                <asp:Label ID="lblfjitais" runat="server" Text='<%#Eval("fJITAIS") %>' CssClass="DisplayNone"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderTemplate>
                                            </HeaderTemplate>
                                            <HeaderStyle CssClass="JC10MitumoriGridCheckboxHeaderCol JC10MitumoriGridHeaderStyle" />
                                            <ItemStyle CssClass="JC10MitumoriCheckboxCol AlignCenter" />
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-CssClass="JC10MitumoriGridPludBtnCol AlignCenter" HeaderStyle-CssClass="JC10MitumoriGridPludBtnHeaderCol JC10MitumoriGridHeaderStyle">
                                            <ItemTemplate>

                                                <asp:Button ID="btnSyouhinAdd" runat="server" Text="＋" CssClass="JC10GrayButton" onmousedown="getTantouBoardScrollPosition();" Width="30px" Height="28px"/>

                                            </ItemTemplate>
                                            <HeaderTemplate>
                                                <asp:Label ID="Label1" runat="server" Text="" CssClass="d-inline-block"></asp:Label>
                                            </HeaderTemplate>
                                            <HeaderStyle CssClass="JC10MitumoriGridPludBtnHeaderCol JC10MitumoriGridHeaderStyle" />
                                            <ItemStyle CssClass="JC10MitumoriGridPludBtnCol AlignCenter" />
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-CssClass="JC11MitumoriGridSyohinCodeCol AlignCenter" HeaderStyle-CssClass="JC11MitumoriGridSyohinCodeHeaderCol JC10MitumoriGridHeaderStyle">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtcSYOHIN" runat="server" Text=' <%# Bind("cSYOHIN","{0}") %>' Width="91px" Height="25px" MaxLength="10" CssClass="form-control TextboxStyle JC10GridTextBox" autocomplete="off" AutoPostBack="true" ></asp:TextBox>
                                            </ItemTemplate>
                                            <HeaderTemplate>
                                                <asp:Label ID="lblcSyohin" runat="server" Text="商品コード" CssClass="d-inline-block"></asp:Label>
                                            </HeaderTemplate>
                                            <HeaderStyle CssClass="JC11MitumoriGridSyohinCodeHeaderCol JC10MitumoriGridHeaderStyle" />
                                            <ItemStyle CssClass="JC11MitumoriGridSyohinCodeCol AlignCenter" />
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-CssClass="JC10MitumoriGridPludBtnCol AlignCenter" HeaderStyle-CssClass="JC10MitumoriGridPludBtnHeaderCol JC10MitumoriGridHeaderStyle">
                                            <ItemTemplate>
                                                <div>
                                                    <asp:Button ID="btnSyohinSelect" runat="server" Text="商" CssClass="JC10GrayButton" onmousedown="getTantouBoardScrollPosition();" Width="35px" Height="28px" />
                                                </div>
                                            </ItemTemplate>
                                            <HeaderTemplate>
                                                <asp:Label ID="Label1" runat="server" Text="" CssClass="d-inline-block"></asp:Label>
                                            </HeaderTemplate>
                                            <HeaderStyle CssClass="JC10MitumoriGridPludBtnHeaderCol JC10MitumoriGridHeaderStyle" />
                                            <ItemStyle CssClass="JC10MitumoriGridPludBtnCol AlignCenter" />
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-CssClass="JC11MitumoriGridSyohinNameCol AlignCenter" HeaderStyle-CssClass="JC11MitumoriGridSyohinNameHeaderCol JC10MitumoriGridHeaderStyle">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtsSYOHIN" runat="server" Text=' <%# Bind("sSYOHIN","{0}") %>' Width="256px" Height="25px" MaxLength="1000" CssClass="form-control TextboxStyle JC10GridTextBox" autocomplete="off" AutoPostBack="true"></asp:TextBox>
                                            </ItemTemplate>
                                            <HeaderTemplate>
                                                <asp:Label ID="lblsSyohin" runat="server" Text="商品名" CssClass="d-inline-block"></asp:Label>
                                            </HeaderTemplate>
                                            <HeaderStyle CssClass="JC11MitumoriGridSyohinNameHeaderCol JC10MitumoriGridHeaderStyle" />
                                            <ItemStyle CssClass="JC11MitumoriGridSyohinNameCol AlignCenter" />
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-CssClass="JC10MitumoriGridSyuryoCol AlignCenter" HeaderStyle-CssClass="JC10MitumoriGridSyuryoHeaderCol JC10MitumoriGridHeaderStyle">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtnSURYO" runat="server" Text=' <%# Bind("nSURYO","{0}") %>' Width="66px" Height="25px" MaxLength="10" CssClass="form-control TextboxStyle JC10GridTextBox" autocomplete="off" AutoPostBack="true" style="text-align:right;"></asp:TextBox>
                                            </ItemTemplate>
                                            <HeaderTemplate>
                                                <asp:Label ID="lblSyuryo" runat="server" Text="数量" CssClass="d-inline-block"></asp:Label>
                                            </HeaderTemplate>
                                            <HeaderStyle CssClass="JC10MitumoriGridSyuryoHeaderCol JC10MitumoriGridHeaderStyle" />
                                            <ItemStyle CssClass="JC10MitumoriGridSyuryoCol AlignCenter" />
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-CssClass="JC10MitumoriGridSyuryoCol AlignCenter" HeaderStyle-CssClass="JC10MitumoriGridSyuryoHeaderCol JC10MitumoriGridHeaderStyle">
                                            <ItemTemplate>
                                                <asp:Label ID="lblcTANI" runat="server" Text='<%# Eval("cTANI") %>'/>
                                                <asp:DropDownList ID="DDL_cTANI" runat="server" Width="66px" AutoPostBack="True" Height="26px" CssClass="DisplayNone">
                                                </asp:DropDownList>
                                            </ItemTemplate>
                                            <HeaderTemplate>
                                                <asp:Label ID="lblTani" runat="server" Text="単位" CssClass="d-inline-block"></asp:Label>
                                            </HeaderTemplate>
                                            <HeaderStyle CssClass="JC10MitumoriGridSyuryoHeaderCol JC10MitumoriGridHeaderStyle" />
                                            <ItemStyle CssClass="JC10MitumoriGridSyuryoCol AlignCenter" />
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-CssClass="JC10MitumoriGridKingakuCol AlignCenter" HeaderStyle-CssClass="JC10MitumoriGridKingakuHeaderCol JC10MitumoriGridHeaderStyle">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtnTANKA" runat="server" Text=' <%# Bind("nTANKA","{0}") %>' Width="96px" Height="25px" CssClass="form-control TextboxStyle JC10GridTextBox" autocomplete="off" AutoPostBack="true" style="text-align:right;"></asp:TextBox>
                                            </ItemTemplate>
                                            <HeaderTemplate>
                                                <asp:Label ID="lblnTanka" runat="server" Text="標準単価" CssClass="d-inline-block"></asp:Label>
                                            </HeaderTemplate>
                                            <HeaderStyle CssClass="JC10MitumoriGridKingakuHeaderCol JC10MitumoriGridHeaderStyle" />
                                            <ItemStyle CssClass="JC10MitumoriGridKingakuCol AlignCenter" />
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-CssClass="JC10MitumoriGridKingakuCol AlignRight" HeaderStyle-CssClass="JC10MitumoriGridKingakuHeaderCol JC10MitumoriGridHeaderStyle">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblTanka" Text='<%#Eval("nSIKIRITANKA")%>' ToolTip='<%#Eval("nSIKIRITANKA")%>' style="cursor:default;user-select:none;padding-right:4px;"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderTemplate>
                                                <asp:Label ID="lblTankaHeader" runat="server" Text="単価" CssClass="d-inline-block" style="width:91px;padding-right:4px;text-align:right;"></asp:Label>
                                            </HeaderTemplate>
                                            <HeaderStyle CssClass="JC10MitumoriGridKingakuHeaderCol JC10MitumoriGridHeaderStyle" />
                                            <ItemStyle CssClass="JC10MitumoriGridKingakuCol AlignRight" />
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-CssClass="JC10MitumoriGridKingakuCol AlignRight" HeaderStyle-CssClass="JC10MitumoriGridKingakuHeaderCol JC10MitumoriGridHeaderStyle">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblTankaGokei" Text='<%#Eval("nTANKAGOUKEI")%>' ToolTip='<%#Eval("nTANKAGOUKEI")%>' style="cursor:default;user-select:none;padding-right:4px;"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderTemplate>
                                                <asp:Label ID="lblTankaGokeiHeader" runat="server" Text="合計金額" CssClass="d-inline-block" style="width:91px;padding-right:4px;text-align:right;"></asp:Label>
                                            </HeaderTemplate>
                                            <HeaderStyle CssClass="JC10MitumoriGridKingakuHeaderCol JC10MitumoriGridHeaderStyle" />
                                            <ItemStyle CssClass="JC10MitumoriGridKingakuCol AlignRight" />
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-CssClass="JC10MitumoriGridKingakuCol AlignCenter" HeaderStyle-CssClass="JC10MitumoriGridKingakuHeaderCol JC10MitumoriGridHeaderStyle">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtnGENKATANKA" runat="server" Text=' <%# Bind("nGENKATANKA","{0}") %>' Width="96px" Height="25px" MaxLength="10" CssClass="form-control TextboxStyle JC10GridTextBox" autocomplete="off" AutoPostBack="true" style="text-align:right;"></asp:TextBox>
                                            </ItemTemplate>
                                            <HeaderTemplate>
                                                <asp:Label ID="lblnGENKATANKA" runat="server" Text="原価単価" CssClass="d-inline-block"></asp:Label>
                                            </HeaderTemplate>
                                            <HeaderStyle CssClass="JC10MitumoriGridKingakuHeaderCol JC10MitumoriGridHeaderStyle" />
                                            <ItemStyle CssClass="JC10MitumoriGridKingakuCol AlignCenter" />
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-CssClass="JC10MitumoriGridTaniCol AlignCenter" HeaderStyle-CssClass="JC10MitumoriGridTaniHeaderCol JC10MitumoriGridHeaderStyle">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtnRITU" runat="server" Text=' <%# Bind("nRITU","{0:#,##0.##}") %>' Width="52px" Height="25px" MaxLength="10" CssClass="form-control TextboxStyle JC10GridTextBox" autocomplete="off" AutoPostBack="true" style="text-align:right;"></asp:TextBox>
                                            </ItemTemplate>
                                            <HeaderTemplate>
                                                <asp:Label ID="lblnRITU" runat="server" Text="掛率" CssClass="d-inline-block"></asp:Label>
                                            </HeaderTemplate>
                                            <HeaderStyle CssClass="JC10MitumoriGridTaniHeaderCol JC10MitumoriGridHeaderStyle" />
                                            <ItemStyle CssClass="JC10MitumoriGridTaniCol AlignCenter" />
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-CssClass="JC10MitumoriGridKingakuCol AlignRight" HeaderStyle-CssClass="JC10MitumoriGridKingakuHeaderCol JC10MitumoriGridHeaderStyle">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblGenkaGokei" Text='<%#Eval("nGENKAGOUKEI")%>' ToolTip='<%#Eval("nGENKAGOUKEI")%>' style="cursor:default;user-select:none;padding-right:4px;"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderTemplate>
                                                <asp:Label ID="lblGenkaGokeiHeader" runat="server" Text="原価合計" CssClass="d-inline-block" style="width:91px;padding-right:4px;text-align:right;"></asp:Label>
                                            </HeaderTemplate>
                                            <HeaderStyle CssClass="JC10MitumoriGridKingakuHeaderCol JC10MitumoriGridHeaderStyle" />
                                            <ItemStyle CssClass="JC10MitumoriGridKingakuCol AlignRight" />
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-CssClass="JC10MitumoriGridKingakuCol AlignRight" HeaderStyle-CssClass="JC10MitumoriGridKingakuHeaderCol JC10MitumoriGridHeaderStyle">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblnARARI" Text='<%#Eval("nARARI")%>' ToolTip='<%#Eval("nARARI")%>' style="cursor:default;user-select:none;padding-right:4px;"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderTemplate>
                                                <asp:Label ID="lblnARARIHeader" runat="server" Text="粗利" CssClass="d-inline-block" style="width:91px;padding-right:4px;text-align:right;"></asp:Label>
                                            </HeaderTemplate>
                                            <HeaderStyle CssClass="JC10MitumoriGridKingakuHeaderCol JC10MitumoriGridHeaderStyle" />
                                            <ItemStyle CssClass="JC10MitumoriGridKingakuCol AlignRight" />
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-CssClass="JC10MitumoriGridKingakuCol AlignRight" HeaderStyle-CssClass="JC10MitumoriGridKingakuHeaderCol JC10MitumoriGridHeaderStyle">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblnARARISu" Text='<%#Eval("nARARISu")%>' ToolTip='<%#Eval("nARARISu")%>' style="cursor:default;user-select:none;padding-right:4px;"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderTemplate>
                                                <asp:Label ID="lblnnARARISuHeader" runat="server" Text="粗利率" CssClass="d-inline-block" style="width:91px;text-align:right;"></asp:Label>
                                            </HeaderTemplate>
                                            <HeaderStyle CssClass="JC10MitumoriGridKingakuHeaderCol JC10MitumoriGridHeaderStyle" />
                                            <ItemStyle CssClass="JC10MitumoriGridKingakuCol AlignRight" />
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-CssClass="JC11MitumoriGridPludBtnCol AlignCenter" HeaderStyle-CssClass="JC10MitumoriGridPludBtnHeaderCol JC10MitumoriGridHeaderStyle" Visible="false">
                                            <ItemTemplate>
                                               <div>
                                                    <span class="dragBtn">三</span>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderTemplate>
                                                <asp:Label ID="Label1" runat="server" Text="" CssClass="d-inline-block"></asp:Label>
                                            </HeaderTemplate>
                                            <HeaderStyle CssClass="JC10MitumoriGridPludBtnHeaderCol JC10MitumoriGridHeaderStyle" />
                                            <ItemStyle CssClass="JC10MitumoriGridPludBtnCol AlignCenter" />
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-CssClass="JC10MitumoriGridPludBtnCol AlignCenter" HeaderStyle-CssClass="JC10MitumoriGridPludBtnHeaderCol JC10MitumoriGridHeaderStyle">
                                            <ItemTemplate>
                                                <div>
                                                    <asp:Button ID="btnSyohinCopy" runat="server" Text="コ" CssClass="JC10GrayButton" onmousedown="getTantouBoardScrollPosition();" Width="35px" Height="28px"/>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderTemplate>
                                                <asp:Label ID="Label1" runat="server" Text="" CssClass="d-inline-block"></asp:Label>
                                            </HeaderTemplate>
                                            <HeaderStyle CssClass="JC10MitumoriGridPludBtnHeaderCol JC10MitumoriGridHeaderStyle" />
                                            <ItemStyle CssClass="JC10MitumoriGridPludBtnCol AlignCenter" />
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-CssClass="JC10MitumoriGridPludBtnCol AlignCenter" HeaderStyle-CssClass="JC10MitumoriGridPludBtnHeaderCol JC10MitumoriGridHeaderStyle">
                                            <ItemTemplate>
                                                <div>
                                                    <asp:Button ID="btnSyohinDelete" runat="server" Text="削" CssClass="JC10GrayButton" onmousedown="getTantouBoardScrollPosition();" Width="35px" Height="28px" />
                                                </div>
                                            </ItemTemplate>
                                            <HeaderTemplate>
                                                <asp:Label ID="Label1" runat="server" Text="" CssClass="d-inline-block"></asp:Label>
                                            </HeaderTemplate>
                                            <HeaderStyle CssClass="JC10MitumoriGridPludBtnHeaderCol JC10MitumoriGridHeaderStyle" />
                                            <ItemStyle CssClass="JC10MitumoriGridPludBtnCol AlignCenter" />
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                                
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    <%--</div>--%>
                    </div>
                    <div class="text-center" style="height: 65px; background: #D7DBDD; min-width: 100%; margin-top: 20px; display: flex; justify-content: center; align-items: center;">
                        <asp:Button ID="btnOk" runat="server" CssClass="BlueBackgroundButton" Text="OK" style ="margin-top: 10px;padding:6px 12px 6px 12px;letter-spacing:1px;font-size:14px;"
                                            OnClientClick="javascript:disabledTextChange(this);" OnClick="btnOk_Click" />
                        <asp:Button ID="btncancel" runat="server"    Text="キャンセル"  CssClass="btn text-primary font btn-sm btn btn-outline-primary " style ="width:auto !important;background-color:white; background-color:white;  margin-top: 10px;margin-left:10px;border-radius:3px;font-size:13px;padding:6px 12px 6px 12px;letter-spacing:1px;" Width="99px" Height="35" OnClick="btncancel_Click"/>

                    </div>
                </div>
                <asp:HiddenField ID="hdnHome" runat="server" />
                 <asp:HiddenField ID="HF_cMitumori" runat="server" />
                         </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </form>
</body>
</html>
