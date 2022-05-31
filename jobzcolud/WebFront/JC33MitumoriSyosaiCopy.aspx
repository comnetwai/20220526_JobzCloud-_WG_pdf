<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="JC33MitumoriSyosaiCopy.aspx.cs" Inherits="jobzcolud.WebFront.JC33MitumoriSyosaiCopy" ValidateRequest ="False"%>

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
    <webopt:BundleReference runat="server" Path="~/Content1/bootstrap" />
    <webopt:BundleReference runat="server" Path="~/Content1/css" />
    <style>
     th {
       position: -webkit-sticky;
       position: sticky!important;
       top: 0;
       background-color: rgb(242,242,242);
       border-color:rgb(242,242,242);
       border:0px;
    }
   
</style>
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
       <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="JC11MitumoriSyosaiDiv" style="max-width:1330px !important;min-width:1330px !important;overflow:hidden;" id="divMitumoriSyosaiP" runat="server">
                <div style="height:65px;padding-top:5px;">
                <asp:Label ID="lblHeader" runat="server" Text="見積詳細コピー" CssClass="TitleLabel d-block align-content-center"></asp:Label>
                <asp:Button ID="btnFusenHeaderCross" runat="server" Text="✕" CssClass="PopupCloseBtn" OnClick="btncancel_Click" />
                       </div>
                <div class="Borderline"></div>

                <div id="divMitumoriMeisaiGrid" runat="server" class="JC11GridViewDiv" style="height:348px;">
                        <asp:UpdatePanel ID="updMitsumoriMeisaiGrid" runat="server" UpdateMode="Conditional">
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="GV_MitumoriSyosai" EventName="selectedIndexChanged"/>
                            </Triggers>
                            <ContentTemplate>
                                <asp:GridView runat="server" ID="GV_MitumoriSyosai" BorderColor="Transparent" AutoGenerateColumns="false" EmptyDataRowStyle-CssClass="JC10NoDataMessageStyle"
                                    ShowHeader="true" ShowHeaderWhenEmpty="true" CellPadding="0" RowStyle-CssClass="GridRow" OnRowDataBound="GV_MitumoriSyosai_RowDataBound">
                                    <EmptyDataRowStyle CssClass="JC10NoDataMessageStyle" />
                                    <HeaderStyle Height="38px" BackColor="#F2F2F2" HorizontalAlign="Left"/>
                                    <RowStyle Height="35px" />
                                    <SelectedRowStyle BackColor="#EBEBF5" />
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkSelectSyouhin" runat="server" AutoPostBack="true" CssClass="M01AnkenGridCheck" />
                                                 <asp:Label ID="lblhdnStatus" runat="server" Text='<%#Eval("status") %>' CssClass="DisplayNone"></asp:Label>
                                                  <asp:Label ID="lblRowNo" runat="server" Text='<%#Eval("rowNo") %>' CssClass="DisplayNone"></asp:Label>
                                                  <asp:Label ID="lblfjitais" runat="server" Text='<%#Eval("fJITAIS") %>' CssClass="DisplayNone"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderTemplate>
                                            </HeaderTemplate>
                                            <HeaderStyle Width="64px"/>
                                            <ItemStyle Width="64px" CssClass="AlignCenter"/>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-CssClass="JC11SyohinCodeCol AlignLeft" HeaderStyle-CssClass="JC11SyohinCodeHeaderCol JC10MitumoriGridHeaderStyle">
                                            <ItemTemplate>
                                                <div class="JC11LabelItem" style="width:112px;height:35px;">
                                                <asp:Label ID="lblcSyohin" runat="server" Text=' <%# Bind("cSYOHIN","{0}") %>' ToolTip='<%# Bind("cSYOHIN","{0}") %>' style="height:35px;width:112px;"></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderTemplate>
                                                <asp:Label ID="lblcMeisaiHeader" runat="server" Text="商品コード" CssClass="d-inline-block"></asp:Label>
                                            </HeaderTemplate>
                                            <HeaderStyle CssClass="JC11SyohinCodeHeaderCol JC10MitumoriGridHeaderStyle" />
                                            <ItemStyle CssClass="JC11SyohinCodeCol AlignLeft" />
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-CssClass="JC11SyohinNameMeisaiCol" HeaderStyle-CssClass="JC11SyohinNameMeisaiHeaderCol JC10MitumoriGridHeaderStyle">
                                            <ItemTemplate>
                                                <div class="JC11LabelItem" style="width:347px;height:35px;">
                                                <asp:Label ID="lblsSyohin" runat="server" Text=' <%# Bind("sSYOHIN","{0}") %>' ToolTip='<%# Bind("sSYOHIN","{0}") %>' style="height:35px;width:347px;"></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderTemplate>
                                                <asp:Label ID="lblsMeisaiHeader" runat="server" Text="商品名" CssClass="d-inline-block" style="text-align:left;width:350px;"></asp:Label>
                                            </HeaderTemplate>
                                            <HeaderStyle CssClass="JC11SyohinNameMeisaiHeaderCol JC10MitumoriGridHeaderStyle"/>
                                            <ItemStyle CssClass="JC11SyohinNameMeisaiCol" />
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-CssClass="JC11SyohinSuryoCol AlignCenter" HeaderStyle-CssClass="JC11SyohinSuryoHeaderCol AlignCenter JC10MitumoriGridHeaderStyle">
                                            <ItemTemplate>
                                                 <div class="JC11LabelItem" style="width:52px;height:35px;">
                                                <asp:Label ID="lblSuyo" runat="server" Text=' <%# Bind("nSURYO","{0}") %>' ToolTip=' <%# Bind("nSURYO","{0}") %>' style="height:35px;width:52px;"></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderTemplate>
                                                <asp:Label ID="lblMeisai_SuyoHeader" runat="server" Text="数量" CssClass="d-inline-block"></asp:Label>
                                            </HeaderTemplate>
                                            <HeaderStyle CssClass="JC11SyohinSuryoHeaderCol AlignCenter JC10MitumoriGridHeaderStyle"/>
                                            <ItemStyle CssClass="JC11SyohinSuryoCol AlignCenter" />
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-CssClass="JC11SyohinTaniCol AlignLeft" HeaderStyle-CssClass="JC11SyohinTaniHeaderCol AlignCenter JC10MitumoriGridHeaderStyle">
                                            <ItemTemplate>
                                                  <asp:Label ID="lblcTANI" runat="server" Text='<%# Eval("cTANI") %>'/>
                                                <asp:DropDownList ID="DDL_MeisaicTANI" runat="server" Width="51px" AutoPostBack="True" Height="26px" CssClass="form-control JC10GridTextBox DisplayNone" Enabled="false" Visible="false" >
                                                </asp:DropDownList>
                                            </ItemTemplate>
                                            <HeaderTemplate>
                                                <asp:Label ID="lblMeisai_cTANIHeader" runat="server" Text="単位" CssClass="d-inline-block"></asp:Label>
                                            </HeaderTemplate>
                                            <HeaderStyle CssClass="JC11SyohinTaniHeaderCol AlignCenter JC10MitumoriGridHeaderStyle" />
                                            <ItemStyle CssClass="JC11SyohinTaniCol AlignLeft" />
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-CssClass="JC11KingakuCol AlignRight" HeaderStyle-CssClass="JC11KingakuHeaderCol JC10MitumoriGridHeaderStyle">
                                            <ItemTemplate>
                                                <div class="JC11LabelItem" style="width:98px;height:35px;padding-left:3px;">
                                                <asp:Label ID="lblnTanka" runat="server" Text=' <%# Bind("nTANKA","{0:#,##0.##}") %>' ToolTip=' <%# Bind("nTANKA","{0:#,##0.##}") %>' style="height:35px;width:98px;"></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderTemplate>
                                                <asp:Label ID="lblMeisai_nTankaHeader" runat="server" Text="標準単価" CssClass="d-inline-block" style="text-align:right;width:98px;"></asp:Label>
                                            </HeaderTemplate>
                                            <HeaderStyle CssClass="JC11KingakuHeaderCol JC10MitumoriGridHeaderStyle" />
                                            <ItemStyle CssClass="JC11KingakuCol AlignRight" />
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-CssClass="JC11KingakuCol AlignRight" HeaderStyle-CssClass="JC11KingakuHeaderCol JC10MitumoriGridHeaderStyle">
                                            <ItemTemplate>
                                                <div class="JC11LabelItem" style="width:98px;height:35px;padding-left:3px;">
                                                <asp:Label ID="lbl_nSIKIRITANKA" runat="server" Text=' <%# Bind("nSIKIRITANKA","{0:#,##0.##}") %>' ToolTip=' <%# Bind("nSIKIRITANKA","{0:#,##0.##}") %>' style="height:35px;width:98px;"></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderTemplate>
                                                <asp:Label ID="lblMeisai_nSIKIRITANKAHeader" runat="server" Text="単価" CssClass="d-inline-block" style="text-align:right;width:98px;"></asp:Label>
                                            </HeaderTemplate>
                                            <HeaderStyle CssClass="JC11KingakuHeaderCol JC10MitumoriGridHeaderStyle" />
                                            <ItemStyle CssClass="JC11KingakuCol AlignRight" />
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-CssClass="JC11KingakuCol AlignRight" HeaderStyle-CssClass="JC11KingakuHeaderCol JC10MitumoriGridHeaderStyle">
                                            <ItemTemplate>
                                                <div class="JC11LabelItem" style="width:98px;height:35px;padding-left:3px;">
                                                <asp:Label ID="lblnTANKAGOUKEI" runat="server" Text=' <%# Bind("nTANKAGOUKEI","{0:#,##0.##}") %>' ToolTip=' <%# Bind("nTANKAGOUKEI","{0:#,##0.##}") %>' style="height:35px;width:98px;"></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderTemplate>
                                                <asp:Label ID="lblMeisai_nTANKAGOUKEIHeader" runat="server" Text="合計金額" CssClass="d-inline-block" style="text-align:right;width:98px;"></asp:Label>
                                            </HeaderTemplate>
                                            <HeaderStyle CssClass="JC11KingakuHeaderCol JC10MitumoriGridHeaderStyle" />
                                            <ItemStyle CssClass="JC11KingakuCol AlignRight" />
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-CssClass="JC11KingakuCol AlignRight" HeaderStyle-CssClass="JC11KingakuHeaderCol JC10MitumoriGridHeaderStyle">
                                            <ItemTemplate>
                                                 <div class="JC11LabelItem" style="width:98px;height:35px;padding-left:3px;">
                                                <asp:Label ID="lblnGENKATANKA" runat="server" Text=' <%# Bind("nGENKATANKA","{0:#,##0.##}") %>' ToolTip=' <%# Bind("nGENKATANKA","{0:#,##0.##}") %>' style="height:35px;width:98px;"></asp:Label>
                                                 </div>
                                            </ItemTemplate>
                                            <HeaderTemplate>
                                                <asp:Label ID="lblMeisai_nGENKATANKAHeader" runat="server" Text="原価単価" CssClass="d-inline-block" style="text-align:right;width:98px;"></asp:Label>
                                            </HeaderTemplate>
                                            <HeaderStyle CssClass="JC11KingakuHeaderCol JC10MitumoriGridHeaderStyle" />
                                            <ItemStyle CssClass="JC11KingakuCol AlignRight" />
                                        </asp:TemplateField>
                                       <asp:TemplateField ItemStyle-CssClass="JC11SyohinTaniCol AlignRight" HeaderStyle-CssClass="JC11SyohinTaniHeaderCol AlignCenter JC10MitumoriGridHeaderStyle">
                                            <ItemTemplate>
                                                  <asp:Label ID="lbl_nRITU" runat="server" Text='<%# Eval("nRITU") %>' style="height:35px;width:55px;text-align:right;"/>
                                            </ItemTemplate>
                                            <HeaderTemplate>
                                                <asp:Label ID="lblMeisai_nRituHeader" runat="server" Text="掛率" CssClass="d-inline-block" style="text-align:right;width:55px;"></asp:Label>
                                            </HeaderTemplate>
                                            <HeaderStyle CssClass="JC11SyohinTaniHeaderCol AlignCenter JC10MitumoriGridHeaderStyle" />
                                            <ItemStyle CssClass="JC11SyohinTaniCol AlignRight" />
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-CssClass="JC11KingakuCol AlignRight" HeaderStyle-CssClass="JC11KingakuHeaderCol JC10MitumoriGridHeaderStyle">
                                            <ItemTemplate>
                                                 <div class="JC11LabelItem" style="width:98px;height:35px;padding-left:3px;">
                                                <asp:Label ID="lblnGENKAGOUKEI" runat="server" Text=' <%# Bind("nGENKAGOUKEI","{0:#,##0.##}") %>' ToolTip=' <%# Bind("nGENKAGOUKEI","{0:#,##0.##}") %>' style="height:35px;width:98px;"></asp:Label>
                                                 </div>
                                            </ItemTemplate>
                                            <HeaderTemplate>
                                                <asp:Label ID="lblMeisai_nGENKAGOUKEIHeader" runat="server" Text="原価合計" CssClass="d-inline-block" style="text-align:right;width:98px;"></asp:Label>
                                            </HeaderTemplate>
                                            <HeaderStyle CssClass="JC11KingakuHeaderCol JC10MitumoriGridHeaderStyle" />
                                            <ItemStyle CssClass="JC11KingakuCol AlignRight" />
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-CssClass="JC11KingakuCol AlignRight" HeaderStyle-CssClass="JC11KingakuHeaderCol JC10MitumoriGridHeaderStyle">
                                            <ItemTemplate>
                                                 <div class="JC11LabelItem" style="width:98px;height:35px;padding-left:3px;">
                                                <asp:Label ID="lblnARARI" runat="server" Text=' <%# Bind("nARARI","{0:#,##0.##}") %>' ToolTip=' <%# Bind("nARARI","{0:#,##0.##}") %>' style="height:35px;width:98px;"></asp:Label>
                                                 </div>
                                            </ItemTemplate>
                                            <HeaderTemplate>
                                                <asp:Label ID="lblMeisai_nARARIHeader" runat="server" Text="粗利" CssClass="d-inline-block" style="text-align:right;width:98px;"></asp:Label>
                                            </HeaderTemplate>
                                            <HeaderStyle CssClass="JC11KingakuHeaderCol JC10MitumoriGridHeaderStyle" />
                                            <ItemStyle CssClass="JC11KingakuCol AlignRight" />
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-CssClass="JC11KingakuCol AlignRight" HeaderStyle-CssClass="JC11KingakuHeaderCol JC10MitumoriGridHeaderStyle">
                                            <ItemTemplate>
                                                 <div class="JC11LabelItem" style="width:98px;height:35px;padding-left:3px;">
                                                <asp:Label ID="lbl_nARARISu" runat="server" Text=' <%# Bind("nARARISu","{0}") %>' ToolTip=' <%# Bind("nARARISu","{0}") %>' style="height:35px;width:98px;"></asp:Label>
                                                 </div>
                                            </ItemTemplate>
                                            <HeaderTemplate>
                                                <asp:Label ID="lblMeisai_nARARISuHeader" runat="server" Text="粗利率" CssClass="d-inline-block" style="text-align:right;width:98px;"></asp:Label>
                                            </HeaderTemplate>
                                            <HeaderStyle CssClass="JC11KingakuHeaderCol JC10MitumoriGridHeaderStyle" />
                                            <ItemStyle CssClass="JC11KingakuCol AlignRight" />
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                            </ItemTemplate>
                                            <HeaderTemplate>
                                            </HeaderTemplate>
                                            <HeaderStyle Width="50px"/>
                                            <ItemStyle Width="50px" />
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </ContentTemplate>
                            
                        </asp:UpdatePanel>
                    </div>

                <div class="text-center"  style =" Height :65px;background :#D7DBDD;min-width:100%; margin-top:20px;display:flex;justify-content:center;align-items:center;" >
                    <asp:Button ID="btnOk" runat="server" CssClass="BlueBackgroundButton" Text="OK" style ="margin-top: 10px;padding:6px 12px 6px 12px;letter-spacing:1px;font-size:14px;"
                                            OnClientClick="javascript:disabledTextChange(this);" OnClick="btnOk_Click" />
                    <asp:Button ID="btncancel" runat="server"    Text="キャンセル"  CssClass="btn text-primary font btn-sm btn btn-outline-primary " style ="width:auto !important;background-color:white; background-color:white;  margin-top: 10px;margin-left:10px;border-radius:3px;font-size:13px;padding:6px 12px 6px 12px;letter-spacing:1px;" Width="99px" Height="35" OnClick="btncancel_Click"/>
                    
                </div>
            </div>
            <asp:HiddenField ID="hdnHome" runat="server" />
        </ContentTemplate>
        </asp:UpdatePanel>
    </form>
</body>
</html>
