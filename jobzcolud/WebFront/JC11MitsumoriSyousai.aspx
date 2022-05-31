<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="JC11MitsumoriSyousai.aspx.cs" Inherits="JobzCloud.WebFront.JC11MitsumoriSyousai" EnableEventValidation="false" MaintainScrollPositionOnPostback="true" ValidateRequest ="False"%>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
<meta name="google" content="notranslate"/>
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
       z-index:10000;
    }

     .inline-rb input[type="radio"] {
    width: auto;
}

.inline-rb label 
{
    font-size:13px !important;
}
   
</style>

    <script type="text/javascript">

        window.onload = function () {
            var intY = document.getElementById("divMitumoriMeisaiGrid").scrollTop;
            var intY1 = document.getElementById("Div5").scrollTop;
            var intX1 = document.getElementById("Div5").scrollLeft;
            document.cookie = "yPos=#~" + intY + "~#";
            document.cookie = "yPos1=&~" + intY1 + "~&";
            document.cookie = "xPos1=$~" + intX1 + "~$";
    }
    function SetDivPosition() {
        var intY = document.getElementById("divMitumoriMeisaiGrid").scrollTop;
       document.cookie = "yPos=#~" + intY + "~#";
        }

           function SetDivPosition1() {
               var intY = document.getElementById("Div5").scrollTop;
               var intX = document.getElementById("Div5").scrollLeft;
               document.cookie = "yPos1=&~" + intY + "~&";
               document.cookie = "xPos1=$~" + intX + "~$";
        }

        function SetScrollTop()
        {
            document.getElementById("Div5").scrollTop = 0;
            var intY = document.getElementById("Div5").scrollTop;
            document.cookie = "yPos1=&~" + intY + "~&";
        }

        var validSuryoNumber = new RegExp(/^-?[0-9]{0,14}((\.)[0-9]{0,2}){0,1}$/);
        var validRituNumber = new RegExp(/^-?[0-9]{0,8}$/);
        var lastValid_Suryo = '';
        var lastValid_Ritu = '';

        function validateSuryo(elem) {
            if (validSuryoNumber.test(elem.value.replaceAll(',', '')))
            {
                lastValid_Suryo = elem.value;
            } else
            {
                if (lastValid_Suryo == undefined)
                {
                    lastValid_Suryo = "";
                }
                elem.value = lastValid_Suryo;
            }
        }

        function validateRitu(elem) {
            if (validRituNumber.test(elem.value.replaceAll(',', '')))
            {
                lastValid_Ritu = elem.value;
            } else
            {
                if (lastValid_Ritu == undefined)
                {
                    lastValid_Ritu = "";
                }
                elem.value = lastValid_Ritu;
            }
    }
</script>

</head>
<body  class="bg-transparent">
    <form id="form1" runat="server">
         <asp:ScriptManager ID="ScriptManager1" EnablePageMethods="True" runat="server" ScriptMode="Release" >
            <Scripts>
                <asp:ScriptReference Name="jquery" />
                <asp:ScriptReference Name="bootstrap" />
                <asp:ScriptReference Path="../Scripts/Common/FixFocus.js" />
            </Scripts>
        </asp:ScriptManager>
         <asp:UpdatePanel ID="updBody" runat="server" UpdateMode="Conditional" RenderMode="Inline">
        <ContentTemplate>
            <div class="JC11MitumoriSyosaiDiv" id="divMitumoriSyosaiP" runat="server">
                <div>
                <div style="height:65px;padding-top:5px;">
                <asp:Label ID="lblHeader" runat="server" Text="見積詳細登録" CssClass="TitleLabelCenter d-block align-content-center"></asp:Label>
               <%--  <asp:Button ID="btnFusenHeaderCross" runat="server" Text="✕" CssClass="PopupCloseBtn" OnClick="btncancel_Click"  />--%> <%--  20220426 maythu delete --%>
                      <asp:Button ID="btnFusenHeaderCross" runat="server" Text="✕" CssClass="PopupCloseBtn"  OnClick="btnOk_Click"  /> <%--  20220426 maythu add --%>
                       </div>
                <div class="Borderline"></div>

                <div id="divMitumoriMeisaiGrid" runat="server" class="JC11GridViewDiv" onscroll="SetDivPosition()">
                        <asp:UpdatePanel ID="updMitsumoriMeisaiGrid" runat="server" UpdateMode="Conditional">
                            <Triggers>
                                <asp:AsyncPostBackTrigger ControlID="GV_MitumoriMeisai" EventName="selectedIndexChanged"/>
                            </Triggers>
                            <ContentTemplate>
                                <asp:GridView runat="server" ID="GV_MitumoriMeisai" BorderColor="Transparent" AutoGenerateColumns="false" EmptyDataRowStyle-CssClass="JC10NoDataMessageStyle"
                                    ShowHeader="true" ShowHeaderWhenEmpty="true" CellPadding="0" OnRowDataBound="GV_MitumoriMeisai_RowDataBound" OnSelectedIndexChanged="GV_MitumoriMeisai_SelectedIndexChanged">
                                    <EmptyDataRowStyle CssClass="JC10NoDataMessageStyle" />
                                    <HeaderStyle Height="38px" BackColor="#F2F2F2" HorizontalAlign="Left"/>
                                    <RowStyle Height="35px" />
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                 <asp:Button ID="btnMeisaiCopy" runat="server" Text="コピー" CssClass="JC10GrayButton" onmousedown="getTantouBoardScrollPosition();" Width="60px" Height="28px" OnClick="btnMeisaiCopy_Click"/>
                                                 <asp:Label ID="lblhdnStatus" runat="server" Text='<%#Eval("status") %>' CssClass="DisplayNone"></asp:Label>
                                                 <asp:Label ID="lblfgenkatanka" runat="server" Text='<%#Eval("fgentankatanka") %>' CssClass="DisplayNone"></asp:Label>
                                                 <asp:Label ID="lblRowNo" runat="server" Text='<%#Eval("rowNo") %>' CssClass="DisplayNone"></asp:Label>
                                                <asp:Label runat="server" ID="lblKubun" Text='<%#Eval("sKUBUN","{0}")%>' ToolTip='<%#Eval("sKUBUN","{0}")%>' CssClass="DisplayNone"></asp:Label>
                                                <asp:Label ID="lblfjitais" runat="server" Text='<%#Eval("fJITAIS") %>' CssClass="DisplayNone"></asp:Label>
                                                 <asp:Label ID="lblfjitaiq" runat="server" Text='<%#Eval("fJITAIQ") %>' CssClass="DisplayNone"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderTemplate>
                                            </HeaderTemplate>
                                            <HeaderStyle Width="64px"/>
                                            <ItemStyle Width="64px" CssClass="AlignCenter"/>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-CssClass="JC11SyohinCodeCol AlignLeft" HeaderStyle-CssClass="JC11SyohinCodeHeaderCol JC10MitumoriGridHeaderStyle">
                                            <ItemTemplate>
                                                <div class="JC11LabelItem" style="width:90px;height:35px;">
                                                <asp:Label ID="lblcMeisai" runat="server" Text=' <%# Bind("cSYOHIN","{0}") %>' ToolTip='<%# Bind("cSYOHIN","{0}") %>' style="height:35px;width:90px;text-align:left;"></asp:Label>
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
                                                <div class="JC11LabelItem" style="width:277px;height:35px;">
                                                <asp:Label ID="lblsMeisai" runat="server" Text=' <%# Bind("sSYOHIN","{0}") %>' ToolTip='<%# Bind("sSYOHIN","{0}") %>' style="height:35px;width:277px;"></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderTemplate>
                                                <asp:Label ID="lblsMeisaiHeader" runat="server" Text="商品名" CssClass="d-inline-block" style="text-align:left;width:280px;"></asp:Label>
                                            </HeaderTemplate>
                                            <HeaderStyle CssClass="JC11SyohinNameMeisaiHeaderCol JC10MitumoriGridHeaderStyle"/>
                                            <ItemStyle CssClass="JC11SyohinNameMeisaiCol" />
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-CssClass="JC11SyohinSuryoCol AlignCenter" HeaderStyle-CssClass="JC11SyohinSuryoHeaderCol AlignCenter JC10MitumoriGridHeaderStyle">
                                            <ItemTemplate>
                                                 <div class="JC11LabelItem" style="width:52px;height:35px;">
                                                <asp:Label ID="lblMeisai_Suyo" runat="server" Text=' <%# Bind("nSURYO","{0}") %>' ToolTip=' <%# Bind("nSURYO","{0}") %>' style="height:35px;width:52px;"></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderTemplate>
                                                <asp:Label ID="lblMeisai_SuyoHeader" runat="server" Text="数量" CssClass="d-inline-block"></asp:Label>
                                            </HeaderTemplate>
                                            <HeaderStyle CssClass="JC11SyohinSuryoHeaderCol AlignCenter JC10MitumoriGridHeaderStyle"/>
                                            <ItemStyle CssClass="JC11SyohinSuryoCol AlignCenter" />
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-CssClass="AlignLeft" HeaderStyle-CssClass="JC11SyohinTaniHeaderCol AlignCenter JC10MitumoriGridHeaderStyle">
                                            <ItemTemplate>
                                                  <asp:Label ID="lblMeisai_cTANI" runat="server" Text='<%# Eval("cTANI") %>'/>
                                                <asp:DropDownList ID="DDL_MeisaicTANI" runat="server" Width="51px" AutoPostBack="True" Height="26px" CssClass="form-control JC10GridTextBox DisplayNone" Enabled="false" Visible="false" style="text-align:left;" >
                                                </asp:DropDownList>
                                            </ItemTemplate>
                                            <HeaderTemplate>
                                                <asp:Label ID="lblMeisai_cTANIHeader" runat="server" Text="単位" CssClass="d-inline-block"></asp:Label>
                                            </HeaderTemplate>
                                            <HeaderStyle CssClass="JC11SyohinTaniHeaderCol AlignCenter JC10MitumoriGridHeaderStyle" />
                                            <ItemStyle CssClass="AlignLeft" />
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-CssClass="JC11KingakuCol AlignRight" HeaderStyle-CssClass="JC11KingakuHeaderCol JC10MitumoriGridHeaderStyle">
                                            <ItemTemplate>
                                                <div class="JC11LabelItem" style="width:98px;height:35px;padding-left:3px;">
                                                <asp:Label ID="lblMeisai_nTanka" runat="server" Text=' <%# Bind("nTANKA","{0:#,##0.##}") %>' ToolTip=' <%# Bind("nTANKA","{0:#,##0.##}") %>' style="height:35px;width:98px;"></asp:Label>
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
                                                <asp:Label ID="lblMeisai_nSIKIRITANKA" runat="server" Text=' <%# Bind("nSIKIRITANKA","{0:#,##0.##}") %>' ToolTip=' <%# Bind("nSIKIRITANKA","{0:#,##0.##}") %>' style="height:35px;width:98px;"></asp:Label>
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
                                                <asp:Label ID="lblMeisai_nTANKAGOUKEI" runat="server" Text=' <%# Bind("nTANKAGOUKEI","{0:#,##0.##}") %>' ToolTip=' <%# Bind("nTANKAGOUKEI","{0:#,##0.##}") %>' style="height:35px;width:98px;"></asp:Label>
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
                                                <asp:Label ID="lblMeisai_nGENKATANKA" runat="server" Text=' <%# Bind("nGENKATANKA","{0:#,##0.##}") %>' ToolTip=' <%# Bind("nGENKATANKA","{0:#,##0.##}") %>' style="height:35px;width:98px;"></asp:Label>
                                                 </div>
                                            </ItemTemplate>
                                            <HeaderTemplate>
                                                <asp:Label ID="lblMeisai_nGENKATANKAHeader" runat="server" Text="原価単価" CssClass="d-inline-block" style="text-align:right;width:98px;"></asp:Label>
                                            </HeaderTemplate>
                                            <HeaderStyle CssClass="JC11KingakuHeaderCol JC10MitumoriGridHeaderStyle" />
                                            <ItemStyle CssClass="JC11KingakuCol AlignRight" />
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-CssClass="JC11SyohinTaniCol AlignRight" HeaderStyle-CssClass="JC11SyohinTaniHeaderCol AlignRight JC10MitumoriGridHeaderStyle">
                                            <ItemTemplate>
                                                  <asp:Label ID="lblMeisai_nRITU" runat="server" Text='<%# Eval("nRITU") %>' style="height:35px;width:55px;text-align:right;"/>
                                            </ItemTemplate>
                                            <HeaderTemplate>
                                                <asp:Label ID="lblMeisai_nRituHeader" runat="server" Text="掛率" CssClass="d-inline-block" style="text-align:right;width:55px;"></asp:Label>
                                            </HeaderTemplate>
                                            <HeaderStyle CssClass="JC11SyohinTaniHeaderCol AlignRight JC10MitumoriGridHeaderStyle" />
                                            <ItemStyle CssClass="JC11SyohinTaniCol AlignRight" />
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-CssClass="JC11KingakuCol AlignRight" HeaderStyle-CssClass="JC11KingakuHeaderCol JC10MitumoriGridHeaderStyle">
                                            <ItemTemplate>
                                                 <div class="JC11LabelItem" style="width:98px;height:35px;padding-left:3px;">
                                                <asp:Label ID="lblMeisai_nGENKAGOUKEI" runat="server" Text=' <%# Bind("nGENKAGOUKEI","{0:#,##0.##}") %>' ToolTip=' <%# Bind("nGENKAGOUKEI","{0:#,##0.##}") %>' style="height:35px;width:98px;"></asp:Label>
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
                                                <asp:Label ID="lblMeisai_nARARI" runat="server" Text=' <%# Bind("nARARI","{0:#,##0.##}") %>' ToolTip=' <%# Bind("nARARI","{0:#,##0.##}") %>' style="height:35px;width:98px;"></asp:Label>
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
                                                <asp:Label ID="lblMeisai_nARARISu" runat="server" Text=' <%# Bind("nARARISu","{0}") %>' ToolTip=' <%# Bind("nARARISu","{0}") %>' style="height:35px;width:98px;"></asp:Label>
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
                                            <HeaderStyle Width="30px"/>
                                            <ItemStyle Width="30px" />
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </ContentTemplate>
                            
                        </asp:UpdatePanel>
                    </div>

                 <div class="JC11Borderline"></div>
                <table style="margin-left:13px;">
                    <tr>
                        <td style="padding-right:10px;">
                            <asp:UpdatePanel ID="updFukusuCopy" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:Button ID="btnFukusuCopy" runat="server" Text="複数コピー" CssClass="JC10GrayButton" onmousedown="getTantouBoardScrollPosition();" OnClick="btnFukusuCopy_Click" Font-Size="14px" Height="35px" />
                                            </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                        <td style="padding-right:10px;"> <%--20220517 Added Eaindray--%>
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:Button ID="btn_delete" runat="server" Text="削除" CssClass="JC10GrayButton" onmousedown="getTantouBoardScrollPosition();" Font-Size="14px" Height="35px" OnClick="btn_delete_Click"/>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                        </td>
                         <td style="font-size:13px !important;padding-top:2px;">
                            <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:Button ID="btn_genka" runat="server" Text="総原価方式" CssClass="JC10ZeikomiBtnActive" Height="35px" Width="100px"  onmousedown="getTantouBoardScrollPosition();" OnClick="btn_genka_Click"/>
                                                <%--<asp:RadioButton ID="RB_genka" runat="server"  CssClass="inline-rb DisplayNone" Text="総原価方式" AutoPostBack="True" OnCheckedChanged="RB_genka_CheckedChanged" GroupName="rdTanka"/>--%> 
                                        </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                         <td style="padding-right:40px;font-size:13px !important;padding-top:2px;">
                            <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:Button ID="btn_gentanka" runat="server" Text="単価原価方式" CssClass="JC10ZeikomiBtn" Height="35px" Width="100px" onmousedown="getTantouBoardScrollPosition();" OnClick="btn_gentanka_Click"/>
                                            <%--<asp:RadioButton ID="RB_gentanka" runat="server" runat="server"  CssClass="inline-rb DisplayNone" Text="単価原価方式" OnCheckedChanged="RB_gentanka_CheckedChanged" AutoPostBack="True"  GroupName="rdTanka"/>--%> 

                                        </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                        <td>
                            <asp:Panel ID="ItemPanel" runat="server" BorderColor="#BFBFBF" CssClass="JC11ItemPanel">
                                <table>
                                    <tr>
                                        <td align="center">
                                             <asp:Label ID="lblSuryoLabel" runat="server" Text="数量"></asp:Label>
                                        </td>
                                        <td align="center">
                                             <asp:Label ID="lblTaniLabel" runat="server" Text="単位"></asp:Label>
                                        </td>
                                        <td align="center">
                                             <asp:Label ID="lblnHyouJunTankaLabel" runat="server" Text="標準単価"></asp:Label>
                                        </td>
                                        <td align="center">
                                             <asp:Label ID="lblKakeRitsuLabel" runat="server" Text="掛率"></asp:Label>
                                        </td>
                                        <td align="center">
                                             <asp:Label ID="lblTankaLabel" runat="server" Text="単価"></asp:Label>
                                        </td>
                                        <td align="center">
                                             <asp:Label ID="lblGokeiKingakuLabel" runat="server" Text="合計金額"></asp:Label>
                                        </td>
                                        <td align="center">
                                             <asp:Label ID="lblGenTankaLabel" runat="server" Text="原単価"></asp:Label>
                                        </td>
                                        <td align="center">
                                             <asp:Label ID="lblGenkaLabel" runat="server" Text="原価"></asp:Label>
                                        </td>
                                        <td align="center">
                                             <asp:Label ID="lblArariLabel" runat="server" Text="粗利"></asp:Label>
                                        </td>
                                        <td align="center">
                                             <asp:Label ID="lblArariSuLabel" runat="server" Text="粗利率"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                             <asp:UpdatePanel ID="updSuryoItem" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <asp:TextBox ID="txtSuryoItem" runat="server" AutoPostBack="true" MaxLength="100" CssClass="form-control TextboxStyle JC11MitumoriItemTextBox" style="text-align:right;"
                                                            onkeyup="DeSelectText(this);" onfocus="this.setSelectionRange(this.value.length, this.value.length);" Width="55px" ReadOnly="true"></asp:TextBox>
                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="txtSuryoItem" EventName="TextChanged" />
                                                    </Triggers>
                                                </asp:UpdatePanel>
                                        </td>
                                        <td>
                                             <asp:UpdatePanel ID="updTaniItem" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <asp:TextBox ID="txttaniItem" runat="server" AutoPostBack="true" MaxLength="100" CssClass="form-control TextboxStyle JC11MitumoriItemTextBox"
                                                            onkeyup="DeSelectText(this);" onfocus="this.setSelectionRange(this.value.length, this.value.length);" Width="50px" ReadOnly="true"></asp:TextBox>
                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="txttaniItem" EventName="TextChanged" />
                                                    </Triggers>
                                                </asp:UpdatePanel>
                                        </td>
                                        <td>
                                             <asp:UpdatePanel ID="updHyouJunTankaItem" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <asp:TextBox ID="txtHyouJunTankaItem" runat="server" AutoPostBack="true" MaxLength="100" CssClass="form-control TextboxStyle JC11MitumoriItemTextBox" style="text-align:right;"
                                                            onkeyup="DeSelectText(this);" onfocus="this.setSelectionRange(this.value.length, this.value.length);" Width="90px" ReadOnly="true"></asp:TextBox>
                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="txtHyouJunTankaItem" EventName="TextChanged" />
                                                    </Triggers>
                                                </asp:UpdatePanel>
                                        </td>
                                        <td>
                                             <asp:UpdatePanel ID="updKakeRitsuItem" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <asp:TextBox ID="txtKakeRitsuItem" runat="server" AutoPostBack="true" MaxLength="100" CssClass="form-control TextboxStyle JC11MitumoriItemTextBox" style="text-align:right;"
                                                            onkeyup="DeSelectText(this);" onfocus="this.setSelectionRange(this.value.length, this.value.length);" Width="55px" ReadOnly="true"></asp:TextBox>
                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="txtKakeRitsuItem" EventName="TextChanged" />
                                                    </Triggers>
                                                </asp:UpdatePanel>
                                        </td>
                                        <td>
                                             <asp:UpdatePanel ID="updTankaItem" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <asp:TextBox ID="txtTankaItem" runat="server" AutoPostBack="true" MaxLength="100" CssClass="form-control TextboxStyle JC11MitumoriItemTextBox" style="text-align:right;"
                                                            onkeyup="DeSelectText(this);" onfocus="this.setSelectionRange(this.value.length, this.value.length);" Width="90px" ReadOnly="true"></asp:TextBox>
                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="txtTankaItem" EventName="TextChanged" />
                                                    </Triggers>
                                                </asp:UpdatePanel>
                                        </td>
                                        <td>
                                             <asp:UpdatePanel ID="updGokeiKingakuItem" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <asp:TextBox ID="txtGokeiKingakuItem" runat="server" AutoPostBack="true" MaxLength="100" CssClass="form-control TextboxStyle JC11MitumoriItemTextBox" style="text-align:right;"
                                                            onkeyup="DeSelectText(this);" onfocus="this.setSelectionRange(this.value.length, this.value.length);" Width="90px" ReadOnly="true"></asp:TextBox>
                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="txtGokeiKingakuItem" EventName="TextChanged" />
                                                    </Triggers>
                                                </asp:UpdatePanel>
                                        </td>
                                        <td>
                                             <asp:UpdatePanel ID="updGenTankaItem" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <asp:TextBox ID="txtGenTankaItem" runat="server" AutoPostBack="true" MaxLength="100" CssClass="form-control TextboxStyle JC11MitumoriItemTextBox" style="text-align:right;"
                                                            onkeyup="DeSelectText(this);" onfocus="this.setSelectionRange(this.value.length, this.value.length);" Width="90px" ReadOnly="true"></asp:TextBox>
                                                       
                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="txtGenTankaItem" EventName="TextChanged" />
                                                    </Triggers>
                                                </asp:UpdatePanel>
                                        </td>
                                        <td>
                                             <asp:UpdatePanel ID="updGenkaItem" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <asp:TextBox ID="txtGenkaItem" runat="server" AutoPostBack="true" MaxLength="100" CssClass="form-control TextboxStyle JC11MitumoriItemTextBox" style="text-align:right;"
                                                            onkeyup="DeSelectText(this);" onfocus="this.setSelectionRange(this.value.length, this.value.length);" Width="90px" ReadOnly="true"></asp:TextBox>
                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="txtGenkaItem" EventName="TextChanged" />
                                                    </Triggers>
                                                </asp:UpdatePanel>
                                        </td>
                                        <td>
                                             <asp:UpdatePanel ID="updArariItem" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <asp:TextBox ID="txtArariItem" runat="server" AutoPostBack="true" MaxLength="100" CssClass="form-control TextboxStyle JC11MitumoriItemTextBox" style="text-align:right;"
                                                            onkeyup="DeSelectText(this);" onfocus="this.setSelectionRange(this.value.length, this.value.length);" Width="90px" ReadOnly="true"></asp:TextBox>
                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="txtArariItem" EventName="TextChanged" />
                                                    </Triggers>
                                                </asp:UpdatePanel>
                                        </td>
                                        <td>
                                             <asp:UpdatePanel ID="updArariSuItem" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <asp:TextBox ID="txtArariSuItem" runat="server" AutoPostBack="true" MaxLength="100" CssClass="form-control TextboxStyle JC11MitumoriItemTextBox" style="text-align:right;"
                                                            onkeyup="DeSelectText(this);" onfocus="this.setSelectionRange(this.value.length, this.value.length);" Width="55px" ReadOnly="true"></asp:TextBox>
                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="txtArariSuItem" EventName="TextChanged" />
                                                    </Triggers>
                                                </asp:UpdatePanel>
                                        </td>
                                    </tr>
                                </table>
                                </asp:Panel>
                        </td>
                    </tr>
                </table>
                                                                
                <div id="Div5" runat="server" class="JC11SyosaiGridViewDiv" onscroll="SetDivPosition1()">
                        <asp:UpdatePanel ID="updMitsumoriSyohinGrid" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <asp:Button ID="BT_Sort" runat="server" Text="Button" OnClick="BT_Sort_Click" style="display:none;" />
                                <asp:GridView runat="server" ID="GV_MitumoriSyohin" BorderColor="Transparent" AutoGenerateColumns="false" EmptyDataRowStyle-CssClass="JC10NoDataMessageStyle"
                                    ShowHeader="true" ShowHeaderWhenEmpty="true" RowStyle-CssClass="GridRow" CellPadding="0" OnRowDataBound="GV_MitumoriSyohin_RowDataBound">
                                    <EmptyDataRowStyle CssClass="JC10NoDataMessageStyle" />
                                    <HeaderStyle Height="37px" BackColor="#F2F2F2" />
                                    <RowStyle CssClass="GridRow" Height="37px" />
                                    <SelectedRowStyle BackColor="#EBEBF5" />
                                    <Columns>
                                        <asp:TemplateField ItemStyle-CssClass="JC10MitumoriCheckboxCol AlignCenter" HeaderStyle-CssClass="JC10MitumoriGridCheckboxHeaderCol JC10MitumoriGridHeaderStyle">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkSelectSyouhin" runat="server" AutoPostBack="true" CssClass="M01AnkenGridCheck" OnCheckedChanged="chkSelectSyouhin_CheckedChanged" />
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
                                                <asp:UpdatePanel ID="updSyohinAddBtn" runat="server" UpdateMode="Conditional">
                                                    <Triggers>
                                                         <asp:AsyncPostBackTrigger ControlID="btnSyouhinAdd" EventName="Click"/>
                                                     </Triggers>
                                                     <ContentTemplate>
                                                <asp:Button ID="btnSyouhinAdd" runat="server" Text="＋" CssClass="JC09GridGrayBtn" onmousedown="getTantouBoardScrollPosition();" Width="30px" Height="28px" OnClick="btnSyouhinAdd_Click" />
                                                          </ContentTemplate>
                                                </asp:UpdatePanel>
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
                                                    <asp:UpdatePanel ID="updSyohinCopyBtn" runat="server" UpdateMode="Conditional">
                                                    <Triggers>
                                                         <asp:AsyncPostBackTrigger ControlID="btnSyohinCopy" EventName="Click"/>
                                                     </Triggers>
                                                     <ContentTemplate>
                                                    <asp:Button ID="btnSyohinCopy" runat="server" Text="コ" CssClass="JC09GridGrayBtn" onmousedown="getTantouBoardScrollPosition();" Width="30px" Height="28px" OnClick="btnSyohinCopy_Click" />
                                                          </ContentTemplate>
                                                </asp:UpdatePanel>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderTemplate>
                                                <asp:Label ID="Label1" runat="server" Text="" CssClass="d-inline-block"></asp:Label>
                                            </HeaderTemplate>
                                            <HeaderStyle CssClass="JC10MitumoriGridPludBtnHeaderCol JC10MitumoriGridHeaderStyle" />
                                            <ItemStyle CssClass="JC10MitumoriGridPludBtnCol AlignCenter" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-Width="30px">
                                            <ItemTemplate>   
                                                 <div class="grip" style="display:flex;align-content:center;align-items:center;align-items: center;justify-content: center;z-index:2;">
                                                         <asp:HoverMenuExtender ID="HoverMenuExtender" runat="server" PopupControlID="PopupMenuSyo"
                                                    TargetControlID="PopupMenuBtnSyo" PopupPosition="bottom">
                                                </asp:HoverMenuExtender>
                                                <asp:Panel ID="PopupMenuSyo" runat="server" CssClass="dropdown-menu fontcss " aria-labelledby="dropdownMenuButton" Style="display: none; min-width: 1rem; width: 122px !important; z-index:10000;margin-top:0px;padding:0px !important;border-radius:1px;">
                                                    <asp:LinkButton ID="LK_Syou" class="fontcss dropdown-item" runat="server" Text='文字を小さくする' OnClick="LK_Syou_Click"  style="margin-right:10px;font-size:12px !important;padding:4px 12px !important;"></asp:LinkButton>
                                                </asp:Panel>
                                                <asp:Panel ID="PopupMenuBtnSyo" runat="server" CssClass="modalPopup" Style="width: 20px;">
                                                    <button class="btn dropdown-toggle" type="button" id="dropdownMenuButton" data-toggle="dropdown" 
                                                      aria-haspopup="true" aria-expanded="false" style="border:1px solid gainsboro;width:20px; height:20px;padding:0px 3px 0px 1px;margin:0;margin-top:-3px;">
                                                </asp:Panel>
                                                     </div>
                                            </ItemTemplate>
                                            <HeaderTemplate>
                                                <div class="grip" style="display:flex;align-content:center;align-items:center;align-items: center;justify-content: center;z-index:2;">
                                                     <asp:Label ID="LB_drop" runat="server" Text="dropdown" style="display:none;"></asp:Label>
                                                    </div>
                                            </HeaderTemplate>
                                            <ItemStyle Width="25px" CssClass="JC09DropDown JC10DropCol" />
                                            <HeaderStyle BorderWidth="0px" CssClass="JC09DropDown JC10DropCol JC10MitumoriGridHeaderStyle" />
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-CssClass="JC11MitumoriGridSyohinCodeCol AlignLeft" HeaderStyle-CssClass="JC11MitumoriGridSyohinCodeHeaderCol JC10MitumoriGridHeaderStyle">
                                            <ItemTemplate>
                                                <asp:UpdatePanel ID="updtxtcSYOUHIN" runat="server" UpdateMode="Conditional">
                                                    <Triggers>
                                                         <asp:AsyncPostBackTrigger ControlID="txtcSYOHIN" EventName="TextChanged"/>
                                                     </Triggers>
                                                    <ContentTemplate>
                                                            <div style="padding-right:3px;">
                                                <asp:TextBox ID="txtcSYOHIN" runat="server" Text=' <%# Bind("cSYOHIN","{0}") %>' Width="91px" Height="25px" MaxLength="10" CssClass="form-control TextboxStyle JC10GridTextBox" autocomplete="off" AutoPostBack="true" onkeypress="return isNumberKey()"  OnTextChanged="txtcSYOHIN_TextChanged"></asp:TextBox>
                                                <asp:FilteredTextBoxExtender ID="TBF_cSYOHIN" runat="server" FilterType="Numbers" TargetControlID="txtcSYOHIN" />
                                                                </div>
                                                        </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </ItemTemplate>
                                            <HeaderTemplate>
                                                <asp:Label ID="lblcSyohin" runat="server" Text="商品コード" CssClass="d-inline-block" style="padding-left:4px;"></asp:Label>
                                            </HeaderTemplate>
                                            <HeaderStyle CssClass="JC11MitumoriGridSyohinCodeHeaderCol JC10MitumoriGridHeaderStyle" />
                                            <ItemStyle CssClass="JC11MitumoriGridSyohinCodeCol AlignLeft" />
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-CssClass="JC10MitumoriGridPludBtnCol AlignCenter" HeaderStyle-CssClass="JC10MitumoriGridPludBtnHeaderCol JC10MitumoriGridHeaderStyle">
                                            <ItemTemplate>
                                                <div>
                                                    <asp:UpdatePanel ID="updbtnSyohinSelectn" runat="server" UpdateMode="Conditional">
                                                    <Triggers>
                                                         <asp:AsyncPostBackTrigger ControlID="btnSyohinSelect" EventName="Click"/>
                                                     </Triggers>
                                                     <ContentTemplate>
                                                    <asp:Button ID="btnSyohinSelect" runat="server" Text="商" CssClass="JC09GridGrayBtn" onmousedown="getTantouBoardScrollPosition();" Width="30px" Height="28px" OnClick="btnSyohinSelect_Click"/>
                                                          </ContentTemplate>
                                                </asp:UpdatePanel>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderTemplate>
                                                <asp:Label ID="Label1" runat="server" Text="" CssClass="d-inline-block"></asp:Label>
                                            </HeaderTemplate>
                                            <HeaderStyle CssClass="JC10MitumoriGridPludBtnHeaderCol JC10MitumoriGridHeaderStyle" />
                                            <ItemStyle CssClass="JC10MitumoriGridPludBtnCol AlignCenter" />
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-CssClass="JC11MitumoriGridSyohinNameCol AlignLeft" HeaderStyle-CssClass="JC11MitumoriGridSyohinNameHeaderCol JC10MitumoriGridHeaderStyle">
                                            <ItemTemplate>
                                                <asp:UpdatePanel ID="updtxtsSYOHIN" runat="server" UpdateMode="Conditional">
                                                    <Triggers>
                                                         <asp:AsyncPostBackTrigger ControlID="txtsSYOHIN" EventName="TextChanged"/>
                                                     </Triggers>
                                                    <ContentTemplate>
                                                <asp:TextBox ID="txtsSYOHIN" runat="server" Text=' <%# Bind("sSYOHIN","{0}") %>' Width="216px" Height="25px" MaxLength="1000" CssClass="form-control TextboxStyle JC10GridTextBox txtsSyohin" autocomplete="off" AutoPostBack="true" OnTextChanged="txtsSYOHIN_TextChanged"></asp:TextBox>
                                                      </ContentTemplate>
                                                </asp:UpdatePanel>  
                                            </ItemTemplate>
                                            <HeaderTemplate>
                                                <asp:Label ID="lblsSyohin" runat="server" Text="商品名" CssClass="d-inline-block"></asp:Label>
                                            </HeaderTemplate>
                                            <HeaderStyle CssClass="JC11MitumoriGridSyohinNameHeaderCol JC10MitumoriGridHeaderStyle" />
                                            <ItemStyle CssClass="JC11MitumoriGridSyohinNameCol AlignLeft" />
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-CssClass="JC10MitumoriGridSyuryoCol AlignRight" HeaderStyle-CssClass="JC10MitumoriGridSyuryoHeaderCol JC10MitumoriGridHeaderStyle">
                                            <ItemTemplate>
                                                <asp:UpdatePanel ID="updtxtnSURYO" runat="server" UpdateMode="Conditional">
                                                    <Triggers>
                                                         <asp:AsyncPostBackTrigger ControlID="txtnSURYO" EventName="TextChanged"/>
                                                     </Triggers>
                                                    <ContentTemplate>
                                                        <div style="padding-right:2px;">
                                                <asp:TextBox ID="txtnSURYO" runat="server" Text=' <%# Bind("nSURYO","{0}") %>' Height="25px" MaxLength="25" CssClass="form-control TextboxStyle JC10GridTextBox txtSuryo" autocomplete="off"  AutoPostBack="true" style="text-align:right;" oninput="validateSuryo(this);" OnTextChanged="txtnSURYO_TextChanged"></asp:TextBox>
                                                            </div>
                                                          </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </ItemTemplate>
                                            <HeaderTemplate>
                                                <asp:Label ID="lblSyuryo" runat="server" Text="数量" CssClass="d-inline-block" style="text-align:right;width:66px;"></asp:Label>
                                            </HeaderTemplate>
                                            <HeaderStyle CssClass="JC10MitumoriGridSyuryoHeaderCol JC10MitumoriGridHeaderStyle" />
                                            <ItemStyle CssClass="JC10MitumoriGridSyuryoCol AlignRight" />
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-CssClass="JC10MitumoriGridTaniCol AlignLeft" HeaderStyle-CssClass="JC10MitumoriGridTaniHeaderCol JC10MitumoriGridHeaderStyle">
                                            <ItemTemplate>
                                               <%-- <asp:Label ID="lblcTANI" runat="server" Text='<%# Eval("cTANI") %>' CssClass="DisplayNone" />
                                                <asp:DropDownList ID="DDL_cTANI" runat="server" Width="52px" AutoPostBack="True" Height="25px" CssClass="form-control JC10GridTextBox" style="font-size:13px;padding-top:0px;padding-right:2px;padding-left:2px;border-radius:2px !important;" OnSelectedIndexChanged="DDL_cTANI_SelectedIndexChanged">
                                                </asp:DropDownList>--%>
                                                <asp:UpdatePanel ID="updTani" runat="server" UpdateMode="Conditional"> 
                                                         <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="txtTani" EventName="TextChanged"/>
                                                             <asp:AsyncPostBackTrigger ControlID="DDL_cTANI"/>
                                                        </Triggers>
                                                          <ContentTemplate>
                                                <div class="select-editable" style=" min-width: 55px;max-width: 55px;">
                                                <asp:Label ID="lblcTANI" runat="server" Text='<%# Eval("cTANI") %>' CssClass="DisplayNone" />
                                                    <asp:DropDownList ID="DDL_cTANI" runat="server" AutoPostBack="True" CssClass="user_select" style="width: 51px;"  OnSelectedIndexChanged="DDL_cTANI_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                    <asp:TextBox ID="txtTani" runat="server" Text='<%# Eval("cTANI") %>' CssClass="txtTani" style="width: 35px;"  autocomplete="off" AutoPostBack="true" OnTextChanged="txtTani_TextChanged"></asp:TextBox> 
                                                </div>      
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </ItemTemplate>
                                            <HeaderTemplate>
                                                <asp:Label ID="lblTani" runat="server" Text="単位" CssClass="d-inline-block" style="padding-left:4px;"></asp:Label>
                                            </HeaderTemplate>
                                            <HeaderStyle CssClass="JC10MitumoriGridTaniHeaderCol JC10MitumoriGridHeaderStyle" />
                                            <ItemStyle CssClass="JC10MitumoriGridTaniCol AlignLeft" />
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-CssClass="JC10MitumoriGridKingakuCol AlignRight" HeaderStyle-CssClass="JC10MitumoriGridKingakuHeaderCol JC10MitumoriGridHeaderStyle">
                                            <ItemTemplate>
                                                <asp:UpdatePanel ID="updtxtnTANKA" runat="server" UpdateMode="Conditional">
                                                    <Triggers>
                                                         <asp:AsyncPostBackTrigger ControlID="txtnTANKA" EventName="TextChanged"/>
                                                     </Triggers>
                                                    <ContentTemplate>
                                                <asp:TextBox ID="txtnTANKA" runat="server" Text=' <%# Bind("nTANKA","{0}") %>'  MaxLength="25" Width="99px" Height="25px" CssClass="form-control TextboxStyle JC10GridTextBox txtSuryo" autocomplete="off" AutoPostBack="true" style="text-align:right;" oninput="validateSuryo(this);"  OnTextChanged="txtnTANKA_TextChanged"></asp:TextBox>
                                                <asp:Label runat="server" ID="lblnTanka" Text='<%# Bind("nTANKA","{0}") %>' ToolTip='<%# Bind("nTANKA","{0}") %>' CssClass="d-inline-block" style="width:100px;cursor:default;user-select:none;padding-right:2px;text-align:right;"></asp:Label>
                                                        </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </ItemTemplate>
                                            <HeaderTemplate>
                                                <asp:Label ID="lblnTanka" runat="server" Text="標準単価" CssClass="d-inline-block" style="text-align:right;width:100px;"></asp:Label>
                                            </HeaderTemplate>
                                            <HeaderStyle CssClass="JC10MitumoriGridKingakuHeaderCol JC10MitumoriGridHeaderStyle" />
                                            <ItemStyle CssClass="JC10MitumoriGridKingakuCol AlignRight" />
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-CssClass="JC10MitumoriGridKingakuCol" HeaderStyle-CssClass="JC10MitumoriGridKingakuHeaderCol JC10MitumoriGridHeaderStyle">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblTanka" Text='<%#Eval("nSIKIRITANKA","{0:#,##0.##}")%>' ToolTip='<%#Eval("nSIKIRITANKA","{0:#,##0.##}")%>' CssClass="d-inline-block" style="width:100px;cursor:default;user-select:none;padding-right:2px;text-align:right;"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderTemplate>
                                                <asp:Label ID="lblTankaHeader" runat="server" Text="単価" CssClass="d-inline-block" style="width:100px;text-align:right;"></asp:Label>
                                            </HeaderTemplate>
                                            <HeaderStyle CssClass="JC10MitumoriGridKingakuHeaderCol JC10MitumoriGridHeaderStyle" />
                                            <ItemStyle CssClass="JC10MitumoriGridKingakuCol" />
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-CssClass="JC10MitumoriGridKingakuCol AlignRight" HeaderStyle-CssClass="JC10MitumoriGridKingakuHeaderCol JC10MitumoriGridHeaderStyle">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblTankaGokei" Text='<%#Eval("nTANKAGOUKEI")%>' ToolTip='<%#Eval("nTANKAGOUKEI")%>' style="width:100px;cursor:default;user-select:none;padding-right:2px;text-align:right;"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderTemplate>
                                                <asp:Label ID="lblTankaGokeiHeader" runat="server" Text="合計金額" CssClass="d-inline-block" style="width:100px;text-align:right;"></asp:Label>
                                            </HeaderTemplate>
                                            <HeaderStyle CssClass="JC10MitumoriGridKingakuHeaderCol JC10MitumoriGridHeaderStyle" />
                                            <ItemStyle CssClass="JC10MitumoriGridKingakuCol AlignRight" />
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-CssClass="JC10MitumoriGridKingakuCol AlignRight" HeaderStyle-CssClass="JC10MitumoriGridKingakuHeaderCol JC10MitumoriGridHeaderStyle">
                                            <ItemTemplate>
                                                <asp:UpdatePanel ID="updtxtnGENKATANKA" runat="server" UpdateMode="Conditional">
                                                    <Triggers>
                                                         <asp:AsyncPostBackTrigger ControlID="txtnGENKATANKA" EventName="TextChanged"/>
                                                     </Triggers>
                                                    <ContentTemplate>
                                                <asp:TextBox ID="txtnGENKATANKA" runat="server" Text=' <%# Bind("nGENKATANKA","{0}") %>' Width="99px" Height="25px" MaxLength="25" CssClass="form-control TextboxStyle JC10GridTextBox txtSuryo" autocomplete="off" AutoPostBack="true" style="text-align:right;" oninput="validateSuryo(this);" OnTextChanged="txtnGENKATANKA_TextChanged"></asp:TextBox>
                                                         </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </ItemTemplate>
                                            <HeaderTemplate>
                                                <asp:Label ID="lblnGENKATANKA" runat="server" Text="原価単価" CssClass="d-inline-block" style="text-align:right;width:100px;"></asp:Label>
                                            </HeaderTemplate>
                                            <HeaderStyle CssClass="JC10MitumoriGridKingakuHeaderCol JC10MitumoriGridHeaderStyle" />
                                            <ItemStyle CssClass="JC10MitumoriGridKingakuCol AlignRight" />
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-CssClass="JC10MitumoriGridTaniCol AlignRight" HeaderStyle-CssClass="JC10MitumoriGridTaniHeaderCol JC10MitumoriGridHeaderStyle">
                                            <ItemTemplate>
                                                 <asp:UpdatePanel ID="updtxtnRITU" runat="server" UpdateMode="Conditional">
                                                    <Triggers>
                                                         <asp:AsyncPostBackTrigger ControlID="txtnRITU" EventName="TextChanged"/>
                                                     </Triggers>
                                                    <ContentTemplate>
                                                         
                                                <asp:TextBox ID="txtnRITU" runat="server" Text=' <%# Bind("nRITU","{0:#,##0.##}") %>' Width="52px" Height="25px" MaxLength="25" CssClass="form-control TextboxStyle JC10GridTextBox txtRitu" autocomplete="off" AutoPostBack="true" style="text-align:right;" oninput="validateRitu(this);" OnTextChanged="txtnRITU_TextChanged"></asp:TextBox>
                                                 <asp:Label runat="server" ID="lblnRITU" Text=' <%# Bind("nRITU","{0:#,##0.##}") %>' ToolTip=' <%# Bind("nRITU","{0:#,##0.##}") %>' CssClass="d-inline-block" style="width:50px;cursor:default;user-select:none;padding-right:2px;text-align:right;"></asp:Label>
                                                 </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </ItemTemplate>
                                            <HeaderTemplate>
                                                <asp:Label ID="lblnRITU" runat="server" Text="掛率" CssClass="d-inline-block" style="text-align:right;width:52px;"></asp:Label>
                                            </HeaderTemplate>
                                            <HeaderStyle CssClass="JC10MitumoriGridTaniHeaderCol JC10MitumoriGridHeaderStyle" />
                                            <ItemStyle CssClass="JC10MitumoriGridTaniCol AlignRight" />
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-CssClass="JC10MitumoriGridKingakuCol AlignRight" HeaderStyle-CssClass="JC10MitumoriGridKingakuHeaderCol JC10MitumoriGridHeaderStyle">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblGenkaGokei" Text='<%#Eval("nGENKAGOUKEI")%>' ToolTip='<%#Eval("nGENKAGOUKEI")%>' style="width:100px;cursor:default;user-select:none;padding-right:2px;text-align:right;"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderTemplate>
                                                <asp:Label ID="lblGenkaGokeiHeader" runat="server" Text="原価合計" CssClass="d-inline-block" style="width:100px;text-align:right;"></asp:Label>
                                            </HeaderTemplate>
                                            <HeaderStyle CssClass="JC10MitumoriGridKingakuHeaderCol JC10MitumoriGridHeaderStyle" />
                                            <ItemStyle CssClass="JC10MitumoriGridKingakuCol AlignRight" />
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-CssClass="JC10MitumoriGridKingakuCol AlignRight" HeaderStyle-CssClass="JC10MitumoriGridKingakuHeaderCol JC10MitumoriGridHeaderStyle">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblnARARI" Text='<%#Eval("nARARI")%>' ToolTip='<%#Eval("nARARI")%>' style="width:100px;cursor:default;user-select:none;padding-right:2px;text-align:right;"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderTemplate>
                                                <asp:Label ID="lblnARARIHeader" runat="server" Text="粗利" CssClass="d-inline-block" style="width:100px;text-align:right;"></asp:Label>
                                            </HeaderTemplate>
                                            <HeaderStyle CssClass="JC10MitumoriGridKingakuHeaderCol JC10MitumoriGridHeaderStyle" />
                                            <ItemStyle CssClass="JC10MitumoriGridKingakuCol AlignRight" />
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-CssClass="JC10MitumoriGridKingakuCol AlignRight" HeaderStyle-CssClass="JC10MitumoriGridKingakuHeaderCol JC10MitumoriGridHeaderStyle">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblnARARISu" Text='<%#Eval("nARARISu")%>' ToolTip='<%#Eval("nARARISu")%>' style="width:100px;cursor:default;user-select:none;padding-right:2px;text-align:right;"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderTemplate>
                                                <asp:Label ID="lblnnARARISuHeader" runat="server" Text="粗利率" CssClass="d-inline-block" style="width:100px;text-align:right;"></asp:Label>
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
                                        
                                        <%--<asp:TemplateField ItemStyle-CssClass="JC10MitumoriGridPludBtnCol AlignCenter" HeaderStyle-CssClass="JC10MitumoriGridPludBtnHeaderCol JC10MitumoriGridHeaderStyle">
                                            <ItemTemplate>
                                                <div>
                                                    <asp:Button ID="btnSyohinDelete" runat="server" Text="削" CssClass="JC09GridGrayBtn" onmousedown="getTantouBoardScrollPosition();" Width="30px" Height="28px" OnClick="btnSyohinDelete_Click" />
                                                </div>
                                            </ItemTemplate>
                                            <HeaderTemplate>
                                                <asp:Label ID="Label1" runat="server" Text="" CssClass="d-inline-block"></asp:Label>
                                            </HeaderTemplate>
                                            <HeaderStyle CssClass="JC10MitumoriGridPludBtnHeaderCol JC10MitumoriGridHeaderStyle" />
                                            <ItemStyle CssClass="JC10MitumoriGridPludBtnCol AlignCenter" />
                                        </asp:TemplateField>--%>
                                        <asp:TemplateField Visible="false">
                                            <ItemTemplate>   
                                                <%--<div class="dropdown">
                                                  <button class="btn dropdown-toggle" type="button" id="dropdownMenuButton" data-toggle="dropdown" 
                                                      aria-haspopup="true" aria-expanded="false" style="border:1px solid gainsboro;width:20px; height:20px;padding:0px 3px 0px 1px;margin:0; z-index:2000;">
                                    
                                                  </button>
                                                  <div class="dropdown-menu dropdown-menu-right" aria-labelledby="dropdownMenuButton" style="min-width: 1rem;width: 5rem;">
                                                    <asp:LinkButton ID="lnkbtnSyohinDelete" class="dropdown-item font" runat="server" Text='削除' style="z-index:2001;margin-right:10px;" OnClick="btnSyohinDelete_Click"></asp:LinkButton>
                                                  </div>
                                                </div>--%>
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
                                            </ItemTemplate>
                                            <ItemStyle Width="25px" />
                                        </asp:TemplateField> 
                                    </Columns>
                                </asp:GridView>
                            </ContentTemplate>
                        </asp:UpdatePanel>

                    <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
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

                                                <asp:Button ID="btnSyouhinAdd" runat="server" Text="＋" CssClass="JC10GrayButton" onmousedown="getTantouBoardScrollPosition();" Width="35px" Height="28px" OnClick="btnSyouhinAdd_Click" />

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
                                                <asp:Label ID="lblcTANI" runat="server" Text='<%# Eval("cTANI") %>' CssClass="DisplayNone" />
                                                <asp:DropDownList ID="DDL_cTANI" runat="server" Width="66px" AutoPostBack="True" Height="26px" CssClass="form-control JC10GridTextBox">
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
                                        <asp:TemplateField ItemStyle-CssClass="JC10MitumoriGridKingakuCol" HeaderStyle-CssClass="JC10MitumoriGridKingakuHeaderCol JC10MitumoriGridHeaderStyle">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblTanka" Text='<%#Eval("nSIKIRITANKA","{0:#,##0.##}")%>' ToolTip='<%#Eval("nSIKIRITANKA","{0:#,##0.##}")%>' CssClass="d-inline-block" style="width:100px;cursor:default;user-select:none;padding-right:2px;text-align:right;"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderTemplate>
                                                <asp:Label ID="lblTankaHeader" runat="server" Text="単価" CssClass="d-inline-block" style="width:100px;text-align:right;"></asp:Label>
                                            </HeaderTemplate>
                                            <HeaderStyle CssClass="JC10MitumoriGridKingakuHeaderCol JC10MitumoriGridHeaderStyle" />
                                            <ItemStyle CssClass="JC10MitumoriGridKingakuCol" />
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
                                        <asp:TemplateField ItemStyle-CssClass="JC11MitumoriGridPludBtnCol AlignCenter" HeaderStyle-CssClass="JC10MitumoriGridPludBtnHeaderCol JC10MitumoriGridHeaderStyle">
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
                                                    <asp:Button ID="btnSyohinCopy" runat="server" Text="コ" CssClass="JC10GrayButton" onmousedown="getTantouBoardScrollPosition();" Width="35px" Height="28px" OnClick="btnSyohinCopy_Click" />
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
                                                    <asp:Button ID="btnSyohinDelete" runat="server" Text="削" CssClass="JC10GrayButton" onmousedown="getTantouBoardScrollPosition();" Width="35px" Height="28px" OnClick="btnSyohinDelete_Click" />
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

                    <asp:Button ID="BT_SyohinEmptyAdd" runat="server" CssClass="JC10GrayButton DisplayNone"  Text="＋　商品追加" Width="125px" OnClick="BT_SyohinEmptyAdd_Click" style="margin-top:5px;" />

                    </div>
</div>
                <div class="text-center"  style =" Height :65px;background :#D7DBDD;min-width:100%; margin-top:20px;display:flex;justify-content:center;align-items:center;" >
                    <asp:Button ID="btnOk" runat="server" CssClass="BlueBackgroundButton" Text="OK" style ="margin-top: 10px;padding:6px 12px 6px 12px;letter-spacing:1px;font-size:14px;"
                                            OnClientClick="javascript:disabledTextChange(this);"  OnClick="btnOk_Click" />
                    <asp:Button ID="btncancel" runat="server"    Text="キャンセル"  CssClass="btn text-primary font btn-sm btn btn-outline-primary " style ="width:auto !important;background-color:white;  margin-top: 10px;margin-left:10px;border-radius:3px;font-size:13px;padding:6px 12px 6px 12px;letter-spacing:1px;" OnClick="btncancel_Click" Height="35px"/>
                    
                 
                </div>
            </div>
            <asp:HiddenField ID="hdnHome" runat="server" />
            <asp:HiddenField ID="HF_selectRowIndex" runat="server" />
            <asp:HiddenField ID="HF_rowIndex" runat="server" />
            <asp:HiddenField ID="HF_fgentanka" runat="server" />
             <asp:HiddenField ID="HF_OK" runat="server" />

               <asp:Button ID="btnYes" runat="server" Text="はい" class="BlueBackgroundButton DisplayNone" Width="100px" Height="36px" OnClick="btnYes_Click" />
                <asp:Button ID="btnNo" runat="server" Text="いいえ" class="BlueBackgroundButton DisplayNone" Width="100px" Height="36px" OnClick="btnNo_Click" />
                <asp:Button ID="btnCancel1" runat="server" Text="キャンセル" class="JC09GrayButton DisplayNone" Width="100px" Height="36px" />
            

            <asp:UpdatePanel ID="updShinkiPopup" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:Button ID="btnShinkiPopup" runat="server" Text="Button" Style="display: none" />
                    <asp:ModalPopupExtender ID="mpeShinkiPopup" runat="server" TargetControlID="btnShinkiPopup"
                        PopupControlID="pnlShinkiPopupScroll" BackgroundCssClass="PopupModalBackground" BehaviorID="pnlShinkiPopupScroll"
                        RepositionMode="RepositionOnWindowResize">
                    </asp:ModalPopupExtender>
                    <asp:Panel ID="pnlShinkiPopupScroll" runat="server" Style="display: none;height:100%;overflow:hidden;" CssClass="PopupScrollDiv">
                        <asp:Panel ID="pnlShinkiPopup" runat="server">
                            <iframe id="ifShinkiPopup" runat="server" scrolling="yes"  style="height:520px;width:1335px"></iframe>
                        <asp:Button ID="btn_CloseSyosaiCopy" runat="server" Text="Button" CssClass="DisplayNone" OnClick="btn_CloseSyosaiCopy_Click"/>
                             <asp:Button ID="btn_SelectSyosaiCopy" runat="server" Text="Button" CssClass="DisplayNone" OnClick="btn_SelectSyosaiCopy_Click"/>
                            </asp:Panel>
                    </asp:Panel>
                </ContentTemplate>
            </asp:UpdatePanel>

               <asp:UpdatePanel ID="updShinkiPopup1" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:Button ID="btnShinkiPopup1" runat="server" Text="Button" Style="display: none" />
                    <asp:ModalPopupExtender ID="mpeShinkiPopup1" runat="server" TargetControlID="btnShinkiPopup1"
                        PopupControlID="pnlShinkiPopupScroll1" BackgroundCssClass="PopupModalBackground" BehaviorID="pnlShinkiPopupScroll1"
                        RepositionMode="RepositionOnWindowResize">
                    </asp:ModalPopupExtender>
                    <asp:Panel ID="pnlShinkiPopupScroll1" runat="server" Style="display: none;height:100%;overflow:hidden;" CssClass="PopupScrollDiv">
                        <asp:Panel ID="pnlShinkiPopup1" runat="server">
                            <iframe id="ifShinkiPopup1" runat="server" scrolling="yes"  style="height:100vh;width:100vw;"></iframe>
                            </asp:Panel>
                    </asp:Panel>
                </ContentTemplate>
            </asp:UpdatePanel>

                    <!--ポップアップ画面-->
        <asp:UpdatePanel ID="updSentakuPopup" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:Button ID="btnSentakuPopup" runat="server" Text="Button" CssClass="DisplayNone" />
                <asp:ModalPopupExtender ID="mpeSentakuPopup" runat="server" TargetControlID="btnSentakuPopup"
                    PopupControlID="pnlSentakuPopupScroll" BackgroundCssClass="PopupModalBackground" BehaviorID="pnlSentakuPopupScroll">
                </asp:ModalPopupExtender>
                <asp:Panel ID="pnlSentakuPopupScroll" runat="server" Style="display: none;height:100%;overflow:hidden;" CssClass="PopupScrollDiv" HorizontalAlign="Center">
                    <asp:Panel ID="pnlSentakuPopup" runat="server">
                        <iframe id="ifSentakuPopup" runat="server" scrolling="no" class="NyuryokuIframe" style="border-radius: 0px;"></iframe>
                        <asp:Button ID="btnClose" runat="server" Text="Button" Style="display: none" OnClick="btnClose_Click" />
                        <asp:Button ID="btnSyohinGridSelect" runat="server" Text="Button" Style="display: none" OnClick="btnSyohinGridSelect_Click"/>
                        <asp:Button ID="btnToLogin" runat="server" Text="Button" Style="display: none" OnClick="btnToLogin_Click"/>
                        <asp:Button ID="btn_CloseTokuisakiSentaku" runat="server" Text="Button" CssClass="DisplayNone" OnClick="btn_CloseTokuisakiSentaku_Click"/> <%--20220518 Added Eaindray--%>
                    </asp:Panel>
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>

             <asp:HiddenField ID="HF_beforeSortIndex" runat="server" />
            <asp:HiddenField ID="HF_afterSortIndex" runat="server" />
             <asp:HiddenField ID="HF_isChange" runat="server" />
            <asp:HiddenField ID="HF_TxtTani" runat="server" />
        </ContentTemplate>
        </asp:UpdatePanel>
        
    </form>
</body>
 <script src="../Scripts/jquery-ui-1.12.1.min.js"></script>
    <script type="text/javascript">
        Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(function (evt, args) {

            function getShiftJISByteLength(s) {
               return s.replace(/[^\x00-\x80&#63728;｡｢｣､･ｦｧｨｩｪｫｬｭｮｯｰｱｲｳｴｵｶｷｸｹｺｻｼｽｾｿﾀﾁﾂﾃﾄﾅﾆﾇﾈﾉﾊﾋﾌﾍﾎﾏﾐﾑﾒﾓﾔﾕﾖﾗﾘﾙﾚﾛﾜﾝ ﾞ ﾟ&#63729;&#63730;&#63731;]/g, 'xx').length;
        }

        $(".txtTani").on('keyup keydown', function (e) {
            var text = $(this).val();
               var byteAmount = getShiftJISByteLength(text);
            if (e.keyCode != "Backspace" && e.key != "Delete")
            {
               while (byteAmount > 4) {
                     text = text.substring(0, text.length - 1);
                   byteAmount = getShiftJISByteLength(text);
                }
                
                document.getElementById("HF_TxtTani").value = text;
                $(this).val(text);
            }
        });

        $(".txtTani").on('change', function (e) {
              var text = $(this).val();
              var byteAmount = getShiftJISByteLength(text);
            while (byteAmount > 4)
            {
              text = text.substring(0, text.length - 1);
                byteAmount = getShiftJISByteLength(text);
            }
            document.getElementById("HF_TxtTani").value = text;
        });       

    $("#GV_MitumoriSyohin").sortable({
        items: 'tr:not(tr:first-child)',
        cursor: 'pointer',
        //handle:'.dragBtn',
        axis: 'y',
        dropOnEmpty: false,
        start: function (e, ui) {
            ui.item.addClass("selected");
             document.getElementById("HF_beforeSortIndex").value = ui.item.index();
        },
        stop: function (e, ui) {
            ui.item.removeClass("selected");
            document.getElementById("HF_afterSortIndex").value = ui.item.index();
            document.getElementById("BT_Sort").click();
        },
        receive: function (e, ui) {
            $(this).find("tbody").append(ui.item);
        }
     });

     var strCook = document.cookie;
            if (strCook.indexOf("#~") != 0) {
            var intS = strCook.indexOf("#~");
            var intE = strCook.indexOf("~#");
                var strPos = strCook.substring(intS + 2, intE);
                document.getElementById("divMitumoriMeisaiGrid").scrollTop = strPos;
     }

     if (strCook.indexOf("&~") != 0) {
         var intS = strCook.indexOf("&~");
         var intE = strCook.indexOf("~&");
         var strPos = strCook.substring(intS + 2, intE);
         document.getElementById("Div5").scrollTop = strPos;
            }

        if (strCook.indexOf("$~") != 0) {
            var intS = strCook.indexOf("$~");
            var intE = strCook.indexOf("~$");
            var strPos = strCook.substring(intS + 2, intE);
            document.getElementById("Div5").scrollLeft = strPos;
     }

      function getShiftJISByteLength(s)
        {
            return s.replace(/[^\x00-\x80&#63728;｡｢｣､･ｦｧｨｩｪｫｬｭｮｯｰｱｲｳｴｵｶｷｸｹｺｻｼｽｾｿﾀﾁﾂﾃﾄﾅﾆﾇﾈﾉﾊﾋﾌﾍﾎﾏﾐﾑﾒﾓﾔﾕﾖﾗﾘﾙﾚﾛﾜﾝ ﾞ ﾟ&#63729;&#63730;&#63731;]/g, 'xx').length;
        }
        $(".txtsSyohin").on('keyup keydown', function (e) {
            var text = $(this).val();
            var txtMaxLength = $(this).attr('maxlength');
            var byteAmount = getShiftJISByteLength(text);
            if (e.key != "Backspace" && e.key != "Delete") {
                while (byteAmount > txtMaxLength) {
                    text = text.substring(0, text.length - 1);
                    byteAmount = getShiftJISByteLength(text);
                   $(this).val(text);
               }
            }
        });

        $(".txtsSyohin").on('change', function (e) {
            var text = $(this).val();
            var txtMaxLength = $(this).attr('maxlength');
            var byteAmount = getShiftJISByteLength(text);
            while (byteAmount > txtMaxLength) {
                    text = text.substring(0, text.length - 1);
                    byteAmount = getShiftJISByteLength(text);
                    $(this).val(text);
               }
        });

    });
</script>

</html>
