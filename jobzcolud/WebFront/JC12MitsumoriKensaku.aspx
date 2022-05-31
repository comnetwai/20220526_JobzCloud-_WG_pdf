<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="JC12MitsumoriKensaku.aspx.cs" Inherits="JobzCloud.WebFront.JC12MitsumoriKensaku" ValidateRequest ="False" EnableEventValidation = "false"%>
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
  
</head>
<body id="BD_Master" runat="server" class="bg-transparent" style="min-height:100vh;">
    <form id="form1" runat="server" defaultbutton="HiddenButton">
        <asp:ScriptManager ID="ScriptManager1" EnablePageMethods="True" runat="server">
            <Scripts>
                <asp:ScriptReference Name="jquery" />
                <asp:ScriptReference Name="bootstrap" />
                <asp:ScriptReference Path="../Scripts/Common/FixFocus.js" />
            </Scripts>
        </asp:ScriptManager>
        <asp:UpdatePanel ID="updBody" runat="server" UpdateMode="Conditional" RenderMode="Inline">
        <ContentTemplate>
        <div id="DIV_Body" runat="server" style="max-width:1330px;min-width:1330px;background-color:white;margin-left:auto;margin-right:auto;display:block;max-height:100vh;padding-bottom:20px;left: 50%; top: 50%; position: absolute; transform: translate(-50%, -50%);" >
        <div class="JC12MitumoriTourokuDiv" id="divMitumoriTorokuP" runat="server">
            <div style="height:65px;padding-top:5px;">
             <asp:Label ID="lblHeader" runat="server" Text="見積検索" CssClass="TitleLabelCenter d-block align-content-center"></asp:Label>
                    <asp:Button ID="btnHeaderCross" runat="server" Text="✕" CssClass="PopupCloseBtn" OnClick="btnHeaderCross_Click" />
                </div>
            <div class="Borderline"></div>
            <div class="JC12MitumoriKensakuDiv mt-2">
                <table class="table1" style="margin-left:5px;" >
                    <tr style="display:none;">
                        <td>
                             <asp:Label ID="Label2" runat="server" Text="検索条件" Font-Size="15px" Font-Bold="True"></asp:Label>
                        </td>
                    </tr>
                     </table>
                            
                <table class="table_less" style="margin-left:60px;margin-top:5px;">
                      <tr>
                        <td style="height:26px">
                            <asp:Label ID="Syohin" runat="server" Text="商品名" Font-Size="13px"></asp:Label>
                        </td>
                        <td style="height:26px">
                             <asp:Label ID="lblTokuisaki" runat="server" Text="得意先" Font-Size="13px"></asp:Label>
                        </td>
                        <td style="height:26px">
                             <asp:Label ID="lblsMitumori" runat="server" Text="見積名" Font-Size="13px"></asp:Label>
                        </td>
                        <td style="height:26px">
                           
                        </td>
                      </tr>
                      <tr>
                          <td>
                              <asp:UpdatePanel ID="updSyohin1" runat="server" UpdateMode="Conditional">
                                  <ContentTemplate>
                                       <asp:TextBox ID="txtSyohin1" runat="server" AutoPostBack="false" MaxLength="1000" CssClass="form-control TextboxStyle JC31MitumoriTokuisaki"
                                                            onkeyup="DeSelectText(this);" onfocus="this.setSelectionRange(this.value.length, this.value.length);" onchange="texboxchange()" TextMode="Search"></asp:TextBox>
                                  </ContentTemplate>
                        　　</asp:UpdatePanel>
                          </td>
                          <td>
                              <asp:UpdatePanel ID="updTokuisaki" runat="server" UpdateMode="Conditional">
                                  <ContentTemplate>
                                       <asp:TextBox ID="txtTokuisaki" runat="server" AutoPostBack="false" MaxLength="200" CssClass="form-control TextboxStyle JC31MitumoriTokuisaki"
                                                            onkeyup="DeSelectText(this);" onfocus="this.setSelectionRange(this.value.length, this.value.length);" onchange="texboxchange()" TextMode="Search"></asp:TextBox>
                                  </ContentTemplate>
                        　　</asp:UpdatePanel>
                        </td>
                         <td>
                             <asp:UpdatePanel ID="updsMitummori" runat="server" UpdateMode="Conditional">
                                  <ContentTemplate>
                                       <asp:TextBox ID="txtsMitumori" runat="server" AutoPostBack="false" MaxLength="200" CssClass="form-control TextboxStyle JC31MitumoriTokuisaki"
                                                            onkeyup="DeSelectText(this);" onfocus="this.setSelectionRange(this.value.length, this.value.length);" onchange="texboxchange()" TextMode="Search"></asp:TextBox>
                                  </ContentTemplate>
                        　　</asp:UpdatePanel>
                        </td>
                                    <td>
                                        <asp:Button runat="server" ID="BT_MitsumoriSyosai" Text="詳細条件" CssClass="JC31MitumoriSyosaiButton" Width="120px" style="border-radius:3px;" OnClick="BT_MitsumoriSyosai_Click"/>
                                    </td>
                    </tr>
                </table>
                <asp:UpdatePanel ID="updbound" runat="server" UpdateMode="Conditional">
                   <ContentTemplate>
                   <asp:Panel ID="Panel1" runat="server" style="background-color:rgb(250, 250, 250);width:780px;margin-left:60px;padding-top:4px;height:140px;margin-top:10px;" Visible="false">
                     <table ID="table1" runat="server" style="margin-left:10px;margin-top:5px;height:60px;">
                    <tr >
                        <td style="height:35px;">
                            <asp:Label ID="lblcMitumori" runat="server" Text="見積コード" Font-Size="13px"></asp:Label>
                        </td>
                        <td style="height:35px;">
                            <asp:Label ID="lblMitumoriJoutai" runat="server" Text="見積状態" Font-Size="13px"></asp:Label>
                        </td>
                        
                        <td style="height:35px;">
                            <asp:Label ID="lblTokuisakiTantousha" runat="server" Text="得意先担当者" Font-Size="13px"></asp:Label>
                        </td>
                        <td style="height:35px;">
                            <asp:Label ID="lblJishaTantousha" runat="server" Text="自社担当者" Font-Size="13px"></asp:Label>
                        </td>
                        <td style="height:35px;">
                        <asp:Label ID="lblmemo" runat="server" Text="メモ" Font-Size="13px"></asp:Label> <%-- //20220429 Added By phoo--%>
                        </td>
                    </tr>
                    <tr style="margin-bottom:5px;">
                        <td>
                            <asp:UpdatePanel ID="updcMitumori" runat="server" UpdateMode="Conditional">
                                  <ContentTemplate>
                                       <asp:TextBox ID="TB_MitsuListCode" runat="server" AutoPostBack="false" MaxLength="10" CssClass="form-control TextboxStyle JC12MitumoriCodeTextBox" onkeypress="OnlyNumericEntry()"
                                                            onkeyup="DeSelectText(this);" onfocus="this.setSelectionRange(this.value.length, this.value.length);" onchange="texboxchange()" TextMode="Search"></asp:TextBox>
                                  </ContentTemplate>
                        　　</asp:UpdatePanel>
                        </td>
                        <td>
                              <asp:UpdatePanel ID="updMitumoriJoutai" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <asp:Button ID="btn_Jyotai" Text="選択なし" runat="server" CssClass="JC31TantouBtn" Width="120px" Height="29px" Style="margin-left: 0px;" OnClientClick="PopupOpenButton()" OnClick="btn_Jyotai_Click" />
                                                        <asp:DropDownList ID="DDL_Jyotai" runat="server" Visible="false"></asp:DropDownList>
                                                        <asp:TextBox ID="txtMitumoriJoutai" runat="server" AutoPostBack="false" MaxLength="100" CssClass="form-control TextboxStyle JC12MitumoriTantousha" Visible="false"
                                                            onkeyup="DeSelectText(this);" onfocus="this.setSelectionRange(this.value.length, this.value.length);" onchange="texboxchange()" TextMode="Search"></asp:TextBox>
                                                    </ContentTemplate>
                                                    <%--<Triggers>
                                       <asp:AsyncPostBackTrigger ControlID="txtMitumoriJoutai" EventName="TextChanged" />
                                  </Triggers>--%>
                                                </asp:UpdatePanel>
                        </td>
                        <td>
                            <asp:UpdatePanel ID="updTokuisakiTantou" runat="server" UpdateMode="Conditional">
                                  <ContentTemplate>
                                       <asp:TextBox ID="txtTokuisakiTantou" runat="server" AutoPostBack="false" MaxLength="200" CssClass="form-control TextboxStyle JC12MitumoriTantousha"
                                                            onkeyup="DeSelectText(this);" onfocus="this.setSelectionRange(this.value.length, this.value.length);" onchange="texboxchange()" TextMode="Search"></asp:TextBox>
                                  </ContentTemplate>
                        　　</asp:UpdatePanel>
                        </td>
                        <td>
                           <asp:UpdatePanel ID="updTantousha" runat="server" UpdateMode="Conditional">
                                  <ContentTemplate>
                                       <asp:Button ID="btn_Tantousya" Text="選択なし" runat="server" CssClass="JC31TantouBtn" Width="120px" Height="29px" OnClientClick="PopupOpenButton()" style="margin-left:0px;" OnClick="btn_Tantousya_Click"/>
                                      <asp:DropDownList ID="DDL_Tantousya" runat="server" Visible="false"></asp:DropDownList>
                                      <asp:DropDownList ID="DDL_Jyouken" runat="server" Visible="false"></asp:DropDownList>
                                       <asp:TextBox ID="txtTantousha" runat="server" AutoPostBack="true" MaxLength="200" CssClass="form-control TextboxStyle JC12MitumoriTantousha" Visible="false"
                                                            onkeyup="DeSelectText(this);" onfocus="this.setSelectionRange(this.value.length, this.value.length);" onchange="texboxchange()" TextMode="Search"></asp:TextBox>
                                  </ContentTemplate>
                        　　</asp:UpdatePanel>
                        </td>
                        <td>
                            <asp:UpdatePanel ID="updSyohin4" runat="server" UpdateMode="Conditional">
                                  <ContentTemplate>
                                       <asp:TextBox ID="txtmemo" runat="server" AutoPostBack="false" MaxLength="1000" CssClass="form-control TextboxStyle JC31Mitumorimemo" Height="28"
                                                           onkeyup="DeSelectText(this);" onfocus="this.setSelectionRange(this.value.length, this.value.length);" onchange="texboxchange()" TextMode="Search" ></asp:TextBox>
                                  </ContentTemplate>
                        　　</asp:UpdatePanel>
                        </td>
                    </tr>
                </table>   
                <table id="table2" runat="server" style="margin-top:5px;margin-left:10px;">
                    <tr>
                        <td colspan="3">
                             <asp:Label ID="lbldMitumori1" runat="server" Text="見積日" Font-Size="13px"></asp:Label>
                        </td>
                        <td colspan="3">
                            <asp:Label ID="lbldJuchuu" runat="server" Text="受注日" Font-Size="13px" Style="margin-left:10px;"></asp:Label>
                        </td>
                        <td colspan="3">
                            <asp:Label ID="lbldUriageYotei" runat="server" Text="売上予定日" Font-Size="13px" Style="margin-left:10px;"></asp:Label>
                        </td>
                    </tr>
                    <tr style="margin-bottom:5px;">
                        <td>
                            <asp:UpdatePanel ID="updMitumoriStartDate" runat="server" UpdateMode="Conditional">
                                  <ContentTemplate>
<%--                                       <asp:TextBox ID="txtMitumoriStartDate" runat="server" AutoPostBack="false" MaxLength="10" CssClass="form-control TextboxStyle JC12DateStart" onkeypress="DisableDateEntry()"
                                                            onkeyup="DeSelectText(this);" onfocus="this.setSelectionRange(this.value.length, this.value.length);"  AutoCompleteType="Disabled" TextMode="Search" ></asp:TextBox>--%>
                                       <asp:Button ID="btnMitumoriStartDate" runat="server" Text="日付を設定" CssClass="JC10GrayButton" onmousedown="getTantouBoardScrollPosition();" OnClientClick="PopupOpenButton()" Height="30px" Width="90px" OnClick="btnMitumoriStartDate_Click"/>
                                                        <div id="divMitumoriStartDate" class="DisplayNone" runat="server"  style="width: 110px;">
                                                            <asp:Label ID="lblMitumoriStartDate" runat="server" Text="" CssClass="GrayLabel"></asp:Label>
                                                            <asp:Label ID="lblMitumoriStartDateYear" runat="server" CssClass="DisplayNone"></asp:Label>
                                                            <asp:Button ID="btndMitumoriStartCross" CssClass="CrossBtnGray" runat="server" Text="✕" OnClick="btndMitumoriStartCross_Click" />
                                                        </div>
                                  </ContentTemplate>
                        　　</asp:UpdatePanel>
                       </td>
                       <td style="font-size:16px;">
                             ～
                       </td>
                       <td>   
                             <asp:UpdatePanel ID="UpdMitumoriEndDate" runat="server" UpdateMode="Conditional">
                                  <ContentTemplate>
                                       <%--<asp:TextBox ID="txtMitumoriEndDate" runat="server" AutoPostBack="false" MaxLength="10" CssClass="form-control TextboxStyle JC12DateEnd" onkeypress="DisableDateEntry()"
                                                            onkeyup="DeSelectText(this);" onfocus="this.setSelectionRange(this.value.length, this.value.length);" AutoCompleteType="Disabled" TextMode="Search"></asp:TextBox>--%>
                                        <asp:Button ID="btnMitumoriEndDate" runat="server" Text="日付を設定" CssClass="JC10GrayButton" onmousedown="getTantouBoardScrollPosition();" Height="30px" Width="90px" OnClientClick="PopupOpenButton()" OnClick="btnMitumoriEndDate_Click"/>
                                                        <div id="divMitumoriEndDate" class="DisplayNone" runat="server"  style="width: 110px;">
                                                            <asp:Label ID="lblMitumoriEndDate" runat="server" Text="" CssClass="GrayLabel"></asp:Label>
                                                            <asp:Label ID="lblMitumoriEndDateYear" runat="server" CssClass="DisplayNone"></asp:Label>
                                                            <asp:Button ID="btndMitumoriEndCross" CssClass="CrossBtnGray" style="padding-top:-25px;" runat="server" Text="✕" OnClick="btndMitumoriEndCross_Click" />
                                                        </div>
                                  </ContentTemplate>
                        　　</asp:UpdatePanel>
                        </td>
                        <td>
                            <asp:UpdatePanel ID="updJuchuuStartDate" runat="server" UpdateMode="Conditional">
                                  <ContentTemplate>
                                       <%--<asp:TextBox ID="txtJuchuuStartDate" runat="server" AutoPostBack="false" MaxLength="10" CssClass="form-control TextboxStyle JC12DateStart" onkeypress="DisableDateEntry()"
                                                            onkeyup="DeSelectText(this);" onfocus="this.setSelectionRange(this.value.length, this.value.length);" AutoCompleteType="Disabled" TextMode="Search"></asp:TextBox>--%>

                                        <asp:Button ID="btnJuchuuStartDate" runat="server" Text="日付を設定" CssClass="JC10GrayButton" onmousedown="getTantouBoardScrollPosition();" Height="30px" Width="90px" OnClientClick="PopupOpenButton()" Style="margin-left: 10px;" OnClick="btnJuchuuStartDate_Click"/>
                                                        <div id="divJuchuuStartDate" class="DisplayNone" runat="server"  style="width: 110px;  margin-left: 10px;">
                                                            <asp:Label ID="lblJuchuuStartDate" runat="server" Text="" CssClass="GrayLabel"></asp:Label>
                                                            <asp:Label ID="lblJuchuuStartDateYear" runat="server" CssClass="DisplayNone"></asp:Label>
                                                            <asp:Button ID="btndJuchuuStartDateCross" CssClass="CrossBtnGray" style="padding-top:-25px;" runat="server" Text="✕" OnClick="btndJuchuuStartDateCross_Click" />
                                                        </div>
                                  </ContentTemplate>
                        　　</asp:UpdatePanel>
                       </td>
                       <td style="font-size:16px;">
                             ～
                       </td>
                       <td>   
                            <asp:UpdatePanel ID="updJuchuuEndDate" runat="server" UpdateMode="Conditional">
                                  <ContentTemplate>
                                       <%--<asp:TextBox ID="txtJuchuuEndDate" runat="server" AutoPostBack="false" MaxLength="10" CssClass="form-control TextboxStyle JC12DateEnd" onkeypress="DisableDateEntry()"
                                                            onkeyup="DeSelectText(this);" onfocus="this.setSelectionRange(this.value.length, this.value.length);" AutoCompleteType="Disabled"  TextMode="Search"></asp:TextBox>--%>

                                       <asp:Button ID="btnJuchuuEndDate" runat="server" Text="日付を設定" CssClass="JC10GrayButton" onmousedown="getTantouBoardScrollPosition();" OnClientClick="PopupOpenButton()" Height="30px" Width="90px" OnClick="btnJuchuuEndDate_Click"/>
                                                        <div id="divJuchuuEndDate" class="DisplayNone" runat="server" style="width: 110px;">
                                                            <asp:Label ID="lblJuchuuEndDate" runat="server" Text="" CssClass="GrayLabel"></asp:Label>
                                                            <asp:Label ID="lblJuchuuEndDateYear" runat="server" CssClass="DisplayNone"></asp:Label>
                                                            <asp:Button ID="btndJuchuuEndDateCross" CssClass="CrossBtnGray" style="padding-top:-25px;" runat="server" Text="✕" OnClick="btndJuchuuEndDateCross_Click" />
                                                           
                                                        </div>
                                  </ContentTemplate>
                        　　</asp:UpdatePanel>
                        </td>
                         <td>
                            <asp:UpdatePanel ID="updUriageYoteiStartDate" runat="server" UpdateMode="Conditional">
                                  <ContentTemplate>
                                       <%--<asp:TextBox ID="txtUriageYoteiStartDate" runat="server" AutoPostBack="false" MaxLength="10" CssClass="form-control TextboxStyle JC12DateStart" onkeypress="DisableDateEntry()"
                                                            onkeyup="DeSelectText(this);" onfocus="this.setSelectionRange(this.value.length, this.value.length);" AutoCompleteType="Disabled"  TextMode="Search"></asp:TextBox>--%>

                                         <asp:Button ID="btnUriageYoteiStartDate" runat="server" Text="日付を設定" CssClass="JC10GrayButton" onmousedown="getTantouBoardScrollPosition();" OnClientClick="PopupOpenButton()" Height="30px" Width="90px" Style="margin-left: 10px;" OnClick="btnUriageYoteiStartDate_Click"/>
                                                        <div id="divUriageYoteiStartDate" class="DisplayNone" runat="server" style="width: 110px;  margin-left: 10px;">
                                                            <asp:Label ID="lblUriageYoteiStartDate" runat="server" Text="" CssClass="GrayLabel"></asp:Label>
                                                            <asp:Label ID="lblUriageYoteiStartYear" runat="server" CssClass="DisplayNone"></asp:Label>
                                                            <asp:Button ID="btndUriageYoteiStartDateCross" CssClass="CrossBtnGray" style="padding-top:-25px;" runat="server" Text="✕" OnClick="btndUriageYoteiStartDateCross_Click" />
                                                        </div>
                                  </ContentTemplate>
                        　　</asp:UpdatePanel>
                       </td>
                       <td style="font-size:16px;">
                             ～
                       </td>
                       <td>   
                            <asp:UpdatePanel ID="updUriageYoteiEndDate" runat="server" UpdateMode="Conditional">
                                  <ContentTemplate>
                                       <%--<asp:TextBox ID="txtUriageYoteiEndDate" runat="server" AutoPostBack="false" MaxLength="10" CssClass="form-control TextboxStyle JC12DateEnd" onkeypress="DisableDateEntry()"
                                                            onkeyup="DeSelectText(this);" onfocus="this.setSelectionRange(this.value.length, this.value.length);" AutoCompleteType="Disabled"  TextMode="Search"></asp:TextBox>--%>

                                           <asp:Button ID="btnUriageYoteiEndDate" runat="server" Text="日付を設定" CssClass="JC10GrayButton" onmousedown="getTantouBoardScrollPosition();" OnClientClick="PopupOpenButton()" Height="30px" Width="90px" OnClick="btnUriageYoteiEndDate_Click"/>
                                                        <div id="divUriageYoteiEndDate" class="DisplayNone" runat="server"  style="width: 110px;">
                                                            <asp:Label ID="lblUriageYoteiEndDate" runat="server" Text="" CssClass="GrayLabel"></asp:Label>
                                                            <asp:Label ID="lblUriageYoteiEndYear" runat="server" CssClass="DisplayNone"></asp:Label>
                                                            <asp:Button ID="btndUriageYoteiEndDateCross" CssClass="CrossBtnGray" style="padding-top:-25px;" runat="server" Text="✕" OnClick="btndUriageYoteiEndDateCross_Click" />
                                                         
                                                        </div>
                                  </ContentTemplate>
                        　　</asp:UpdatePanel>
                        </td>
                    </tr>
                </table>
                                </asp:Panel>
                                      </ContentTemplate>
                                    </asp:UpdatePanel>
                <table style="display:none;">
                    <tr>
                        <td colspan="6">
                             <%--<asp:Label ID="lblKensakuJouken" runat="server" Text="検索条件" Font-Size="15px" Font-Bold="True"></asp:Label>--%>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <%--<asp:Label ID="lblcMitumori" runat="server" Text="見積コード" Font-Size="13px"></asp:Label>--%>
                        </td>
                        <td>
                           <%-- <asp:Label ID="lblsMitumori" runat="server" Text="見積名" Font-Size="13px"></asp:Label>--%>
                        </td>
                        <td>
                           <%-- <asp:Label ID="lblMitumoriJoutai" runat="server" Text="見積状態" Font-Size="13px"></asp:Label>--%>
                        </td>
                        <td>
                            <%--<asp:Label ID="lblTokuisaki" runat="server" Text="得意先" Font-Size="13px"></asp:Label>--%>
                        </td>
                        <td>
                            <%--<asp:Label ID="lblTokuisakiTantousha" runat="server" Text="得意先担当者" Font-Size="13px"></asp:Label>--%>
                        </td>
                        <td>
                            <%--<asp:Label ID="lblJishaTantousha" runat="server" Text="自社担当者" Font-Size="13px"></asp:Label>--%>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <%--<asp:UpdatePanel ID="updcMitumori" runat="server" UpdateMode="Conditional">
                                  <ContentTemplate>
                                       <asp:TextBox ID="txtcMitumori" runat="server" AutoPostBack="true" MaxLength="10" CssClass="form-control TextboxStyle JC12MitumoriCodeTextBox" onkeypress="OnlyNumericEntry()"
                                                            onkeyup="DeSelectText(this);" onfocus="this.setSelectionRange(this.value.length, this.value.length);" TextMode="Search"></asp:TextBox>
                                  </ContentTemplate>
                                  <Triggers>
                                       <asp:AsyncPostBackTrigger ControlID="txtcMitumori" EventName="TextChanged" />
                                  </Triggers>
                        　　</asp:UpdatePanel>--%>
                        </td>
                        <td>
                            <%--<asp:UpdatePanel ID="updsMitummori" runat="server" UpdateMode="Conditional">
                                  <ContentTemplate>
                                       <asp:TextBox ID="txtsMitumori" runat="server" AutoPostBack="true" MaxLength="200" CssClass="form-control TextboxStyle JC12MitumoriMeiTextBox"
                                                            onkeyup="DeSelectText(this);" onfocus="this.setSelectionRange(this.value.length, this.value.length);"  TextMode="Search"></asp:TextBox>
                                  </ContentTemplate>
                                  <Triggers>
                                       <asp:AsyncPostBackTrigger ControlID="txtsMitumori" EventName="TextChanged" />
                                  </Triggers>
                        　　</asp:UpdatePanel>--%>
                        </td>
                        <td>
                            <%--<asp:UpdatePanel ID="updMitumoriJoutai" runat="server" UpdateMode="Conditional">
                                  <ContentTemplate>
                                       <asp:TextBox ID="txtMitumoriJoutai" runat="server" AutoPostBack="true" MaxLength="100" CssClass="form-control TextboxStyle JC12MitumoriJoutaiTextBox"
                                                            onkeyup="DeSelectText(this);" onfocus="this.setSelectionRange(this.value.length, this.value.length);"  TextMode="Search"></asp:TextBox>
                                  </ContentTemplate>
                                  <Triggers>
                                       <asp:AsyncPostBackTrigger ControlID="txtMitumoriJoutai" EventName="TextChanged" />
                                  </Triggers>
                        　　</asp:UpdatePanel>--%>
                        </td>
                        <td>
                            <%--<asp:UpdatePanel ID="updTokuisaki" runat="server" UpdateMode="Conditional">
                                  <ContentTemplate>
                                       <asp:TextBox ID="txtTokuisaki" runat="server" AutoPostBack="true" MaxLength="200" CssClass="form-control TextboxStyle JC12MitumoriTokuisaki"
                                                            onkeyup="DeSelectText(this);" onfocus="this.setSelectionRange(this.value.length, this.value.length);"  TextMode="Search"></asp:TextBox>
                                  </ContentTemplate>
                                  <Triggers>
                                       <asp:AsyncPostBackTrigger ControlID="txtTokuisaki" EventName="TextChanged" />
                                  </Triggers>
                        　　</asp:UpdatePanel>--%>
                        </td>
                        <td>
                            <%--<asp:UpdatePanel ID="updTokuisakiTantou" runat="server" UpdateMode="Conditional">
                                  <ContentTemplate>
                                       <asp:TextBox ID="txtTokuisakiTantou" runat="server" AutoPostBack="true" MaxLength="200" CssClass="form-control TextboxStyle JC12MitumoriTantousha"
                                                            onkeyup="DeSelectText(this);" onfocus="this.setSelectionRange(this.value.length, this.value.length);" TextMode="Search"></asp:TextBox>
                                  </ContentTemplate>
                                  <Triggers>
                                       <asp:AsyncPostBackTrigger ControlID="txtTokuisakiTantou" EventName="TextChanged" />
                                  </Triggers>
                        　　</asp:UpdatePanel>--%>
                        </td>
                        <td>
                            <%--<asp:UpdatePanel ID="updTantousha" runat="server" UpdateMode="Conditional">
                                  <ContentTemplate>
                                       <asp:Button ID="btn_Tantousya" Text="選択なし" runat="server" CssClass="JC12CancelBtn" Width="120px" Height="30px" style="margin-left:0px;border-radius:4px;" OnClick="btn_Tantousya_Click"/>
                                      <asp:DropDownList ID="DDL_Tantousya" runat="server" Visible="false"></asp:DropDownList>
                                      <asp:DropDownList ID="DDL_Jyouken" runat="server" Visible="false"></asp:DropDownList>
                                       <asp:TextBox ID="txtTantousha" runat="server" AutoPostBack="true" MaxLength="200" CssClass="form-control TextboxStyle JC12MitumoriTantousha" Visible="false"
                                                            onkeyup="DeSelectText(this);" onfocus="this.setSelectionRange(this.value.length, this.value.length);" TextMode="Search"></asp:TextBox>
                                  </ContentTemplate>
                                  <Triggers>
                                       <asp:AsyncPostBackTrigger ControlID="txtTantousha" EventName="TextChanged" />
                                  </Triggers>
                        　　</asp:UpdatePanel>--%>
                        </td>
                    </tr>
                </table>
                <table style="margin-top:15px;display:none;">
                    <tr>
                        <td colspan="3">
                           <%-- <asp:Label ID="lbldMitumori" runat="server" Text="見積日" Font-Size="13px"></asp:Label>--%>
                        </td>
                        <td colspan="3">
                            <%--<asp:Label ID="lbldJuchuu" runat="server" Text="受注日" Font-Size="13px"></asp:Label>--%>
                        </td>
                        <td colspan="3">
                            <%--<asp:Label ID="lbldUriageYotei" runat="server" Text="売上予定日" Font-Size="13px"></asp:Label>--%>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <%--<asp:UpdatePanel ID="updMitumoriStartDate" runat="server" UpdateMode="Conditional">
                                  <ContentTemplate>
                                       <asp:TextBox ID="txtMitumoriStartDate" runat="server" AutoPostBack="true" MaxLength="10" CssClass="form-control TextboxStyle JC12DateStart" onkeypress="DisableDateEntry()"
                                                            onkeyup="DeSelectText(this);" onfocus="this.setSelectionRange(this.value.length, this.value.length);" AutoCompleteType="Disabled" TextMode="Search"></asp:TextBox>
                                  </ContentTemplate>
                                  <Triggers>
                                       <asp:AsyncPostBackTrigger ControlID="txtMitumoriStartDate" EventName="TextChanged" />
                                  </Triggers>
                        　　</asp:UpdatePanel>--%>
                       </td>
                       <td>
                             ～
                       </td>
                       <td>   
                            <%--<asp:UpdatePanel ID="UpdMitumoriEndDate" runat="server" UpdateMode="Conditional">
                                  <ContentTemplate>
                                       <asp:TextBox ID="txtMitumoriEndDate" runat="server" AutoPostBack="true" MaxLength="10" CssClass="form-control TextboxStyle JC12DateEnd" onkeypress="DisableDateEntry()"
                                                            onkeyup="DeSelectText(this);" onfocus="this.setSelectionRange(this.value.length, this.value.length);" AutoCompleteType="Disabled" TextMode="Search"></asp:TextBox>
                                  </ContentTemplate>
                                  <Triggers>
                                       <asp:AsyncPostBackTrigger ControlID="txtMitumoriEndDate" EventName="TextChanged" />
                                  </Triggers>
                        　　</asp:UpdatePanel>--%>
                        </td>
                        <td>
                            <%--<asp:UpdatePanel ID="updJuchuuStartDate" runat="server" UpdateMode="Conditional">
                                  <ContentTemplate>
                                       <asp:TextBox ID="txtJuchuuStartDate" runat="server" AutoPostBack="true" MaxLength="10" CssClass="form-control TextboxStyle JC12DateStart" onkeypress="DisableDateEntry()"
                                                            onkeyup="DeSelectText(this);" onfocus="this.setSelectionRange(this.value.length, this.value.length);" AutoCompleteType="Disabled" TextMode="Search"></asp:TextBox>
                                  </ContentTemplate>
                                  <Triggers>
                                       <asp:AsyncPostBackTrigger ControlID="txtJuchuuStartDate" EventName="TextChanged" />
                                  </Triggers>
                        　　</asp:UpdatePanel>--%>
                       </td>
                       <td>
                             ～
                       </td>
                       <td>   
                            <%--<asp:UpdatePanel ID="updJuchuuEndDate" runat="server" UpdateMode="Conditional">
                                  <ContentTemplate>
                                       <asp:TextBox ID="txtJuchuuEndDate" runat="server" AutoPostBack="true" MaxLength="10" CssClass="form-control TextboxStyle JC12DateEnd" onkeypress="DisableDateEntry()"
                                                            onkeyup="DeSelectText(this);" onfocus="this.setSelectionRange(this.value.length, this.value.length);" AutoCompleteType="Disabled" TextMode="Search"></asp:TextBox>
                                  </ContentTemplate>
                                  <Triggers>
                                       <asp:AsyncPostBackTrigger ControlID="txtJuchuuEndDate" EventName="TextChanged" />
                                  </Triggers>
                        　　</asp:UpdatePanel>--%>
                        </td>
                         <td>
                            <%--<asp:UpdatePanel ID="updUriageYoteiStartDate" runat="server" UpdateMode="Conditional">
                                  <ContentTemplate>
                                       <asp:TextBox ID="txtUriageYoteiStartDate" runat="server" AutoPostBack="true" MaxLength="10" CssClass="form-control TextboxStyle JC12DateStart" onkeypress="DisableDateEntry()"
                                                            onkeyup="DeSelectText(this);" onfocus="this.setSelectionRange(this.value.length, this.value.length);" AutoCompleteType="Disabled" TextMode="Search"></asp:TextBox>
                                  </ContentTemplate>
                                  <Triggers>
                                       <asp:AsyncPostBackTrigger ControlID="txtUriageYoteiStartDate" EventName="TextChanged" />
                                  </Triggers>
                        　　</asp:UpdatePanel>--%>
                       </td>
                       <td>
                             ～
                       </td>
                       <td>   
                            <%--<asp:UpdatePanel ID="updUriageYoteiEndDate" runat="server" UpdateMode="Conditional">
                                  <ContentTemplate>
                                       <asp:TextBox ID="txtUriageYoteiEndDate" runat="server" AutoPostBack="true" MaxLength="10" CssClass="form-control TextboxStyle JC12DateEnd" onkeypress="DisableDateEntry()"
                                                            onkeyup="DeSelectText(this);" onfocus="this.setSelectionRange(this.value.length, this.value.length);" AutoCompleteType="Disabled" TextMode="Search"></asp:TextBox>
                                  </ContentTemplate>
                                  <Triggers>
                                       <asp:AsyncPostBackTrigger ControlID="txtUriageYoteiEndDate" EventName="TextChanged" />
                                  </Triggers>
                        　　</asp:UpdatePanel>--%>
                        </td>
                    </tr>
                </table>
                <table style="margin-top:15px;display:none;">
                    <tr>
                        <td colspan="3">
                           <%-- <asp:Label ID="Label1" runat="server" Text="商品名" Font-Size="13px"></asp:Label>--%>
                        </td>
                    </tr>
                    <tr>
                        <td>
                           <%-- <asp:UpdatePanel ID="updSyohin1" runat="server" UpdateMode="Conditional">
                                  <ContentTemplate>
                                       <asp:TextBox ID="txtSyohin1" runat="server" AutoPostBack="true" MaxLength="1000" CssClass="form-control TextboxStyle JC12MitumoriTokuisaki"
                                                            onkeyup="DeSelectText(this);" onfocus="this.setSelectionRange(this.value.length, this.value.length);" TextMode="Search"></asp:TextBox>
                                  </ContentTemplate>
                                  <Triggers>
                                       <asp:AsyncPostBackTrigger ControlID="txtSyohin1" EventName="TextChanged" />
                                  </Triggers>
                        　　</asp:UpdatePanel>--%>
                       </td>
                       <td>   
                            <asp:UpdatePanel ID="updSyohin2" runat="server" UpdateMode="Conditional">
                                  <ContentTemplate>
                                       <asp:TextBox ID="txtSyohin2" runat="server" AutoPostBack="true" MaxLength="1000" CssClass="form-control TextboxStyle JC12MitumoriTokuisaki"
                                                            onkeyup="DeSelectText(this);" onfocus="this.setSelectionRange(this.value.length, this.value.length);" TextMode="Search"></asp:TextBox>
                                  </ContentTemplate>
                                  <Triggers>
                                       <asp:AsyncPostBackTrigger ControlID="txtSyohin2" EventName="TextChanged" />
                                  </Triggers>
                        　　</asp:UpdatePanel>
                        </td>
                        <td>
                            <asp:UpdatePanel ID="updSyohin3" runat="server" UpdateMode="Conditional">
                                  <ContentTemplate>
                                       <asp:TextBox ID="txtSyohin3" runat="server" AutoPostBack="true" MaxLength="1000" CssClass="form-control TextboxStyle JC12MitumoriTokuisaki"
                                                            onkeyup="DeSelectText(this);" onfocus="this.setSelectionRange(this.value.length, this.value.length);" TextMode="Search"></asp:TextBox>
                                  </ContentTemplate>
                                  <Triggers>
                                       <asp:AsyncPostBackTrigger ControlID="txtSyohin3" EventName="TextChanged" />
                                  </Triggers>
                        　　</asp:UpdatePanel>
                       </td>
                    </tr>
                </table>
                <div class="JC12ButtonDiv">
                    <asp:Button ID="btnSearch" runat="server" CssClass=" BlueBackgroundButton JC12SearchBtn" Text="絞り込み"
                        UseSubmitBehavior="false" OnClick="btnSearch_Click" />

                     <asp:Button ID="btnClear" runat="server" CssClass="JC10CancelBtn" Text="クリア"
                            UseSubmitBehavior="false" OnClick="btnClear_Click" />
                </div>
            </div>
                <asp:UpdatePanel ID="updJoken" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                    <div style="padding-left:20px;padding-right:20px;margin-top:5px;max-width: 1330px !important;">
             <asp:Label ID="LB_Jouken" runat="server" Text="検索条件" Font-Size="15px" Font-Bold="true"></asp:Label>
                <asp:Table ID="TB_SentakuJouken" runat="server" Style="margin-top:2px;">
                    <asp:TableRow>
                        <asp:TableCell ID="TC_Jouken" runat="server">
                                <%--<asp:Panel ID="JokenPanel" class="JC12JokenDiv" runat="server">
                                   <asp:Label ID="lbl_JokenLabel" runat="server" ForeColor="#a0a19d" Font-Size="11px">自社担当者</asp:Label>
                                   <asp:Label ID="lblsKYOTEN" runat="server" ForeColor="#a0a19d" Font-Size="11px">先小</asp:Label>
                                   <asp:Label ID="lblcKYOTEN" runat="server" Visible="false"></asp:Label>
                                   <asp:Button ID="BT_sKYOTENLIST_Cross" runat="server" BackColor="White" Text="✕" Font-Size="10px" Style="vertical-align: middle; margin-left: 10px;height: 20px; border: none;"/>
                                    </asp:Panel>--%>

                        </asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
                </div>
                            </ContentTemplate>
                    </asp:UpdatePanel>
            <table align="right" style="margin-top:20px;margin-bottom:10px;">
                 <tr>
                     <td>
                          <asp:UpdatePanel ID="updHyojikensuu1" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                     　　 <asp:Label ID="lblHyojikensuu" runat="server" Text="1-30/500" Font-Size="13px" ></asp:Label>
                                            <asp:Label ID="LB_Total" runat="server" Text="" Font-Size="13px" CssClass="JC12LblHyojikensuu"></asp:Label>
                                        </ContentTemplate>
                           </asp:UpdatePanel>
                     </td>
                     <td>
                         <asp:Label ID="lblHyoujikensuuLabel" runat="server" Text="表示件数" Font-Size="13px"></asp:Label>
                     </td>
                     <td>
                         <asp:UpdatePanel ID="updHyojikensuu" runat="server" UpdateMode="Conditional">
                              <ContentTemplate>
                                  <asp:DropDownList ID="DDL_Hyojikensuu" runat="server" Width="80px" AutoPostBack="true" CssClass="form-control JC12GridTextBox" Font-Size="13px" OnSelectedIndexChanged="DDL_Hyojikensuu_SelectedIndexChanged">
                                      <asp:ListItem Value="20" Selected="True">20件</asp:ListItem>
                                      <asp:ListItem Value="30">30件</asp:ListItem>
                                      <asp:ListItem Value="50">50件</asp:ListItem>
                                  </asp:DropDownList>
                              </ContentTemplate>
                             <Triggers>
                                   <asp:AsyncPostBackTrigger ControlID="DDL_Hyojikensuu" EventName="SelectedIndexChanged" />
                             </Triggers>
                          </asp:UpdatePanel>
                     </td>
                     <td>
                         <asp:UpdatePanel ID="updDisplayItemSetting" runat="server" UpdateMode="Conditional">
                              <ContentTemplate>
                                  <asp:Button ID="btnDisplayItemSetting" runat="server" CssClass="JC12HyojiItemSettingBtn" Text="表示項目を設定" OnClientClick="PopupOpenButton()"
                             UseSubmitBehavior="false" Width="124px" OnClick="btnDisplayItemSetting_Click" />
                              </ContentTemplate>
                         </asp:UpdatePanel>
                     </td>
                                </tr>
                        </table>

            <div id="Div5" runat="server" class="JC12GridViewDiv">
                        <asp:UpdatePanel ID="updMitsumoriGrid" runat="server" UpdateMode="Conditional">
                            <Triggers>
                                        <%--<asp:PostBackTrigger ControlID="GV_Mitumori_Original"/>--%>
                                    </Triggers>
                            <ContentTemplate>
                                 <asp:TextBox ID="GV_MiRowindex" runat="server" Value="" Style="display: none" /> 
                                <asp:GridView runat="server" ID="GV_Mitumori" BorderColor="Transparent" AutoGenerateColumns="false" EmptyDataRowStyle-CssClass="JC31NoDataMessageStyle"
                                ShowHeader="true" ShowHeaderWhenEmpty="true" CellPadding="0" OnPageIndexChanging="GV_Mitumori_PageIndexChanging" CssClass="GridViewStyle" Visible="false" DataKeyNames="cMitumori" AllowSorting="True" OnSorting="GV_Mitumori_Sorting">
                              <HeaderStyle  BackColor="#F2F2F2" HorizontalAlign="Left" Height="38px" ForeColor="Black" />
                                <RowStyle Height="43px" CssClass="JC12GridItem" />
                                    <Columns>
                                        <asp:TemplateField ItemStyle-CssClass="JC12MitumoriCodeCol" HeaderStyle-CssClass="JC12MitumoriCodeHeaderCol JC10MitumoriGridHeaderStyle">
                                            <ItemTemplate>
                                                <%--<asp:HyperLink ID="LK_cMitumori" runat="server" Text=' <%# Bind("cMitumori","{0}") %>'></asp:HyperLink>--%>  
                                                <%--<asp:LinkButton ID="LK_cMitumori" runat="server" Font-Underline="False" Text=' <%# Bind("cMitumori","{0}") %>' OnClick="LK_cMitumori_Click" Style="padding-left:3px;"></asp:LinkButton>--%>
                                                <%--<asp:LinkButton ID="LK_cMitumori" runat="server" Font-Underline="False" Text=' <%# Bind("cMitumori","{0}") %>' Style="padding-left:3px;" ForeColor="Black" Visible="false"></asp:LinkButton>--%>
                                                <asp:Label ID="LB_cMitumori1" runat="server" Font-Underline="False" Text=' <%# Bind("cMitumori","{0}") %>' Style="padding-left:3px;" ForeColor="Black"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderTemplate>
                                                <asp:Label ID="lblcMitumoriHeader" runat="server" Text="見積コード" CssClass="d-inline-block" Style="padding-left:3px;"></asp:Label>
                                            </HeaderTemplate>
                                            <HeaderStyle CssClass="JC12MitumoriCodeHeaderCol JC10MitumoriGridHeaderStyle" />
                                            <ItemStyle CssClass="JC12MitumoriCodeCol" />
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-CssClass="JC12MitumoriMeiCol" HeaderStyle-CssClass="JC12MitumoriMeiHeaderCol JC10MitumoriGridHeaderStyle">
                                            <ItemTemplate>
                                                <div class="JC12LabelItem" style="width:187px;height:35px;">
                                                <asp:Label ID="LB_cMitumori" runat="server" Text=' <%# Bind("cMitumori","{0}") %>' ToolTip='<%# Bind("sMitumori","{0}") %>' style="height:35px;width:187px;display:none;"></asp:Label>
                                                <asp:Label ID="lblsMitumori_Grid" runat="server" Text=' <%# Bind("sMitumori","{0}") %>' ToolTip='<%# Bind("sMitumori","{0}") %>' style="height:35px;width:187px;"></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderTemplate>
                                                <asp:Label ID="lblsMitumoriHeader" runat="server" Text="見積名" CssClass="d-inline-block" style="text-align:left;width:190px;"></asp:Label>
                                            </HeaderTemplate>
                                            <HeaderStyle CssClass="JC12MitumoriMeiHeaderCol JC10MitumoriGridHeaderStyle"/>
                                            <ItemStyle CssClass="JC12MitumoriMeiCol" />
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-CssClass="JC12EigyoTantouCol" HeaderStyle-CssClass="JC12EigyoTantouHeaderCol JC10MitumoriGridHeaderStyle">
                                            <ItemTemplate>
                                                 <div class="JC12LabelItem" style="width:307px;height:35px;">
                                                <asp:Label ID="lblEigyoTantou_Grid" runat="server" Text=' <%# Bind("sEigyouTantou","{0}") %>' ToolTip=' <%# Bind("sEigyouTantou","{0}") %>' style="height:35px;width:307px;"></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderTemplate>
                                                <asp:Label ID="lblEigyouTantouHeader" runat="server" Text="自社担当者" CssClass="d-inline-block" style="text-align:left;width:310px;"></asp:Label>   <%--//20220429 Added By phoo--%>
                                            </HeaderTemplate>
                                            <HeaderStyle CssClass="JC12EigyoTantouHeaderCol JC10MitumoriGridHeaderStyle"/>
                                            <ItemStyle CssClass="JC12EigyoTantouCol" />
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-CssClass="JC12KingakuCol" HeaderStyle-CssClass="JC12KingakuHeaderCol JC10MitumoriGridHeaderStyle">
                                            <ItemTemplate>
                                                 <div class="JC12LabelItem" style="width:97px;height:35px;">
                                                <asp:Label ID="lblsSakuseisya_Grid" runat="server" Text=' <%# Bind("sSakuseisya","{0}") %>' ToolTip=' <%# Bind("sSakuseisya","{0}") %>'  style="height:35px;width:97px;"></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderTemplate>
                                                <asp:Label ID="lblsSakuseisyaHeader" runat="server" Text="作成者" CssClass="d-inline-block" style="text-align:left;width:100px;"></asp:Label>
                                            </HeaderTemplate>
                                            <HeaderStyle CssClass="JC12KingakuHeaderCol JC10MitumoriGridHeaderStyle" />
                                            <ItemStyle CssClass="JC12KingakuCol" />
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-CssClass="JC12SakuseiSyaCol" HeaderStyle-CssClass="JC12SakuseiSyaHeaderCol JC10MitumoriGridHeaderStyle">
                                            <ItemTemplate>
                                                <div class="JC12LabelItem" style="width:92px;height:35px;">
                                                <asp:Label ID="lbldMitumori_Grid" runat="server" Text=' <%# Bind("dMITUMORISAKUSEI","{0:yyyy/MM/dd}") %>' ToolTip=' <%# Bind("dMITUMORISAKUSEI","{0:yyyy/MM/dd}") %>' style="height:35px;width:92px;"></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderTemplate>
                                                <asp:Label ID="lbldMitumoriHeader" runat="server" Text="見積日" CssClass="d-inline-block" style="text-align:left;width:95px;"></asp:Label>
                                            </HeaderTemplate>
                                            <HeaderStyle CssClass="JC12SakuseiSyaHeaderCol JC10MitumoriGridHeaderStyle" />
                                            <ItemStyle CssClass="JC12SakuseiSyaCol" />
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-CssClass="JC12KingakuCol" HeaderStyle-CssClass="JC12KingakuHeaderCol JC10MitumoriGridHeaderStyle">
                                            <ItemTemplate>
                                                <div class="JC12LabelItem" style="width:97px;height:35px;text-align:right;">
                                                <asp:Label ID="lblnGokeiKingaku_Grid" runat="server" Text=' <%# Bind("nGokeiKingaku","{0:#,##0.##}") %>' ToolTip=' <%# Bind("nGokeiKingaku","{0:#,##0.##}") %>' style="height:35px;width:97px;"></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderTemplate>
                                                  <div class="JC12LabelItem" style="width:100px;text-align:right;height:35px;">
                                                <asp:Label ID="lblnGokeiKingakuHeader" runat="server" Text="合計金額" CssClass="d-inline-block" style="width:97px;text-align:right;"></asp:Label>
                                                      </div>
                                            </HeaderTemplate>
                                            <HeaderStyle CssClass="JC12KingakuHeaderCol JC10MitumoriGridHeaderStyle" />
                                            <ItemStyle CssClass="JC12KingakuCol" />
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-CssClass="JC12SakuseiSyaCol" HeaderStyle-CssClass="JC12SakuseiSyaHeaderCol JC10MitumoriGridHeaderStyle">
                                            <ItemTemplate>
                                                 <div class="JC12LabelItem" style="width:92px;height:35px;">
                                                <asp:Label ID="lblMitumoriJoutai_Grid" runat="server" Text=' <%# Bind("MitumoriJoutai","{0}") %>' ToolTip=' <%# Bind("MitumoriJoutai","{0}") %>' style="height:35px;width:92px;"></asp:Label>
                                               </div>
                                            </ItemTemplate>
                                            <HeaderTemplate>
                                                <asp:Label ID="lblMitumoriJoutaiHeader" runat="server" Text="見積状態" CssClass="d-inline-block" style="text-align:left;width:95px;"></asp:Label>
                                            </HeaderTemplate>
                                            <HeaderStyle CssClass="JC12SakuseiSyaHeaderCol JC10MitumoriGridHeaderStyle" />
                                            <ItemStyle CssClass="JC12SakuseiSyaCol" />
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-CssClass="JC12KingakuCol" HeaderStyle-CssClass="JC12KingakuHeaderCol JC10MitumoriGridHeaderStyle">
                                            <ItemTemplate>
                                                 <div class="JC12LabelItem" style="width:97px;height:35px;text-align:right;">
                                                <asp:Label ID="lblnArari_Grid" runat="server" Text=' <%# Bind("nArari","{0:#,##0.##}") %>' ToolTip=' <%# Bind("nArari","{0:#,##0.##}") %>' style="height:35px;width:97px;"></asp:Label>
                                                 </div>
                                            </ItemTemplate>
                                            <HeaderTemplate>
                                                <div class="JC12LabelItem" style="width:100px;height:35px;text-align:right;">
                                                <asp:Label ID="lblnArariHeader" runat="server" Text="金額粗利" CssClass="d-inline-block" style="text-align:left;width:97px;text-align:right;"></asp:Label>
                                                    </div>
                                            </HeaderTemplate>
                                            <HeaderStyle CssClass="JC12KingakuHeaderCol JC10MitumoriGridHeaderStyle" />
                                            <ItemStyle CssClass="JC12KingakuCol" />
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-CssClass="JC12MitumoriMeiCol" HeaderStyle-CssClass="JC12MitumoriMeiHeaderCol JC10MitumoriGridHeaderStyle">
                                            <ItemTemplate>
                                                <div class="JC12LabelItem" style="width:187px;height:35px;white-space: nowrap;overflow: hidden; text-overflow: ellipsis;display: inline-block;  ">
                                                <asp:Label ID="lblMemo_Grid" runat="server" Text=' <%# Bind("Memo","{0}") %>' ToolTip=' <%# Bind("Memo","{0}") %>' style="height:35px;width:187px;"></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderTemplate>
                                               <asp:Label ID="lblMemoHeader" runat="server" Text="メモ" CssClass="d-inline-block" style="text-align:left;width:190px;"></asp:Label> <%--//20220429 Added by phoo--%>
                                            </HeaderTemplate>
                                            <HeaderStyle CssClass="JC12MitumoriMeiHeaderCol JC10MitumoriGridHeaderStyle" />
                                            <ItemStyle CssClass="JC12MitumoriMeiCol" />
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-CssClass="JC12MitumoriMeiCol AlignLeft" HeaderStyle-CssClass="JC12MitumoriMeiHeaderCol JC10MitumoriGridHeaderStyle">
                                            <ItemTemplate>
                                                <div class="JC12LabelItem" style="width:187px;height:35px;white-space: nowrap;overflow: hidden; text-overflow: ellipsis;display: inline-block;  ">
                                                <asp:Label ID="lblBikou_Grid" runat="server" Text=' <%# Bind("Bikou","{0}") %>' ToolTip=' <%# Bind("Bikou","{0}") %>' style="height:35px;width:187px;text-align:left;"></asp:Label>
                                                 </div>
                                            </ItemTemplate>
                                            <HeaderTemplate>
                                                <asp:Label ID="lblBikouHeader" runat="server" Text="備考" CssClass="d-inline-block" style="text-align:left;width:190px;"></asp:Label>  <%-- //20220429 Added by phoo--%>
                                            </HeaderTemplate>
                                            <HeaderStyle CssClass="JC12MitumoriMeiHeaderCol JC10MitumoriGridHeaderStyle" />
                                            <ItemStyle CssClass="JC12MitumoriMeiCol AlignLeft" />
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-CssClass="JC12MitumoriMeiCol AlignCenter" HeaderStyle-CssClass="JC12MitumoriMeiHeaderCol JC10MitumoriGridHeaderStyle">
                                            <ItemTemplate>
                                                <div class="JC12LabelItem" style="width:187px;height:35px;white-space: nowrap;overflow: hidden; text-overflow: ellipsis;display: inline-block;  ">
                                                <asp:Label ID="lblsTokuisaki_Grid" runat="server" Text=' <%# Bind("sTokuisaki","{0}") %>' ToolTip=' <%# Bind("sTokuisaki","{0}") %>' style="height:35px;width:187px;text-align:left;"></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderTemplate>
                                                 <asp:Label ID="lblsTokuisakiHeader" runat="server" Text="得意先" CssClass="d-inline-block" style="text-align:left;width:190px;"></asp:Label> <%-- //20220429 Added By phoo--%>
                                            </HeaderTemplate>
                                            <HeaderStyle CssClass="JC12MitumoriMeiHeaderCol JC10MitumoriGridHeaderStyle" />
                                            <ItemStyle CssClass="JC12MitumoriMeiCol AlignLeft" />
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-CssClass="JC12MitumoriMeiCol AlignLeft" HeaderStyle-CssClass="JC12MitumoriMeiHeaderCol JC10MitumoriGridHeaderStyle">
                                            <ItemTemplate>
                                                <div class="JC12LabelItem" style="width:187px;height:35px;white-space: nowrap;overflow: hidden; text-overflow: ellipsis;display: inline-block;  ">
                                                <asp:Label ID="lblsTokuisakiTantou_Grid" runat="server" Text=' <%# Bind("sTokuisakiTantou","{0}") %>' ToolTip=' <%# Bind("sTokuisakiTantou","{0}") %>' style="height:35px;width:187px;text-align:left;"></asp:Label>
                                                    </div>
                                            </ItemTemplate>
                                            <HeaderTemplate>
                                                   <asp:Label ID="lblsTokuisakiTantouHeader" runat="server" Text="得意先担当者" CssClass="d-inline-block" style="text-align:left;width:190px;"></asp:Label><%-- //20220429 Added by phoo--%>
                                            </HeaderTemplate>
                                            <HeaderStyle CssClass="JC12MitumoriMeiHeaderCol JC10MitumoriGridHeaderStyle" />
                                            <ItemStyle CssClass="JC12MitumoriMeiCol AlignLeft" />
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>

                                <asp:GridView runat="server" ID="GV_Mitumori_Original" BorderColor="Transparent" AutoGenerateColumns="false" EmptyDataRowStyle-CssClass="JC10NoDataMessageStyle" CssClass="RowHover"
                                    ShowHeader="true" ShowHeaderWhenEmpty="true" DataKeyNames="cMitumori" CellPadding="0" OnRowCreated="GV_Mitumori_Original_RowCreated" OnPageIndexChanging="GV_Mitumori_PageIndexChanging" EnableViewState="false" Visible="true" AllowSorting="True" OnSorting="GV_Mitumori_Sorting" OnRowDataBound="GV_Mitumori_Original_RowDataBound"  OnSelectedIndexChanged="GV_Mitumori_Original_SelectedIndexChanged">
                                    <EmptyDataRowStyle CssClass="JC10NoDataMessageStyle" />
                                    <HeaderStyle Height="38px" BackColor="#F2F2F2" HorizontalAlign="Left"/>
                                    <RowStyle Height="43px" CssClass="JC12GridItem" />
                                    <Columns>
                                    </Columns>
                                </asp:GridView>

                                <asp:GridView runat="server" ID="GridView1" BorderColor="Transparent" AutoGenerateColumns="true" EmptyDataRowStyle-CssClass="JC31NoDataMessageStyle"
                                            ShowHeader="false" ShowHeaderWhenEmpty="true" CellPadding="7" AllowPaging="True" OnPageIndexChanging="GV_Mitumori_PageIndexChanging"
                                            EnableViewState="false" Visible="true" OnRowDataBound="GV_MitumoriPg_Original_RowDataBound" PageSize="20" DataKeyNames="見積コード"
                                            GridLines="None" Width="1290px">


                                            <RowStyle Height="0px" />
                                            <PagerStyle Font-Size="14px" HorizontalAlign="Center" CssClass="GridPager" VerticalAlign="Middle" />
                                            <PagerSettings Mode="NumericFirstLast" FirstPageText="&lt;" LastPageText="&gt;" />
                                        </asp:GridView>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <div style="width:100%;margin-top:10px;" align="center">
                <asp:Label ID="lblDataNai" runat="server" Style="font-size: 14px; font-weight: normal;" Text="該当するデータがありません"></asp:Label>
                </div>
            <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                  <ContentTemplate>
            <asp:Button ID="btnDateChange" runat="server" Text="Button" OnClick="btnDateChange_Click" style="display:none;"/>
                      </ContentTemplate>
                </asp:UpdatePanel>
       </div>
      
        <asp:HiddenField ID="hdnHome" runat="server" />
                    <asp:HiddenField ID="HF_fSyouhin" runat="server" />
             <asp:HiddenField ID="HF_fMeisaiOK" runat="server" />
             <asp:Button ID="HiddenButton" style="display:none;" OnClientClick="return false;" runat="server" Text="Button" />

         <%--<asp:UpdatePanel ID="updHyoujiSet" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:Button ID="btnHyoujiSetting" runat="server" Text="Button" style="display:none" />
                            <asp:ModalPopupExtender ID="mpeHyoujiSetPopUp" runat="server" TargetControlID="btnHyoujiSetting"
                                PopupControlID="pnlHyoujiSetPopUpScroll" BehaviorID="pnlHyoujiSetPopUpScroll" BackgroundCssClass="PopupModalBackground"
                                RepositionMode="RepositionOnWindowResize">
                            </asp:ModalPopupExtender>
                            <asp:Panel ID="pnlHyoujiSetPopUpScroll" runat="server"  CssClass="PopupScrollDiv">
                                <asp:Panel ID="pnlHyoujiSetPopUp" runat="server">
                                    <iframe id="ifpnlHyoujiSetPopUp" runat="server" scrolling="yes" class="HyoujiSettingIframe"  seamless></iframe>
                                       <asp:Button ID="btnHyoujiClose" runat="server" Text="Button" CssClass="DisplayNone" OnClick="btnHyoujiSettingClose_Click" />
                                       <asp:Button ID="btnHyoujiSave" runat="server" Text="Button" CssClass="DisplayNone" OnClick="btnHyoujiSettingSave_Click" />
                                </asp:Panel>
                            </asp:Panel>
                        </ContentTemplate>
                    </asp:UpdatePanel>--%>

           

            

       
            </div>
             <asp:UpdatePanel ID="updHyoujiSet" runat="server" UpdateMode="Conditional">

                    <ContentTemplate>
                        <asp:Button ID="btnHyoujiSetting" runat="server" Text="Button" Style="display: none" />
                        <asp:ModalPopupExtender ID="mpeHyoujiSetPopUp" runat="server" TargetControlID="btnHyoujiSetting"
                            PopupControlID="pnlHyoujiSetPopUpScroll" BehaviorID="pnlHyoujiSetPopUpScroll" BackgroundCssClass="PopupModalBackground"
                            RepositionMode="None">
                        </asp:ModalPopupExtender>
                        <asp:Panel ID="pnlHyoujiSetPopUpScroll" runat="server" CssClass="PopupScrollDiv">
                            <asp:Panel ID="pnlHyoujiSetPopUp" runat="server">
                                <iframe id="ifpnlHyoujiSetPopUp" runat="server" class="HyoujiSettingIframe" seamless></iframe>
                                <asp:Button ID="btnHyoujiSave" runat="server" Text="Button" CssClass="DisplayNone" OnClick="btnHyoujiSettingSave_Click" onClientClick="MasterOverflow()" />
                                <asp:Button ID="btnHyoujiClose" runat="server" Text="Button" CssClass="DisplayNone" OnClick="btnHyoujiSettingClose_Click" onClientClick="MasterOverflow()" />

                            </asp:Panel>
                        </asp:Panel>
                    </ContentTemplate>
                </asp:UpdatePanel>

            <asp:UpdatePanel ID="updShinkiPopup" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:Button ID="btnShinkiPopup" runat="server" Text="Button" Style="display: none" />
                    <asp:ModalPopupExtender ID="mpeShinkiPopup" runat="server" TargetControlID="btnShinkiPopup"
                        PopupControlID="pnlShinkiPopupScroll" BackgroundCssClass="PopupModalBackground" BehaviorID="pnlShinkiPopupScroll"
                        RepositionMode="RepositionOnWindowResize">
                    </asp:ModalPopupExtender>
                    <asp:Panel ID="pnlShinkiPopupScroll" runat="server" Style="display: none; height: 100%; overflow: hidden;" CssClass="PopupScrollDiv">
                        <asp:Panel ID="pnlShinkiPopup" runat="server">
                            <%--<iframe id="ifShinkiPopup" runat="server" scrolling="yes" style="height: 520px; width: 1335px"></iframe> --%>

                            <iframe id="ifShinkiPopup" runat="server" scrolling="yes" style="height: 100vh; width: 100vw;"></iframe>
                            <asp:Button ID="btn_CloseMeisaiCopy" runat="server" Text="Button" CssClass="DisplayNone"  OnClick="btn_CloseMeisaiCopy_Click" onClientClick="MasterOverflow()"/>
                            <asp:Button ID="btn_SelectMeisaiCopy" runat="server" Text="Button" CssClass="DisplayNone" OnClick="btn_SelectMeisaiCopy_Click" onClientClick="MasterOverflow()"/>
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
                    <asp:Panel ID="pnlSentakuPopupScroll" runat="server" Style="display: none;overflow-x:hidden;overflow-y:hidden;" CssClass="PopupScrollDiv">
                        <asp:Panel ID="pnlSentakuPopup" runat="server">
                            <iframe id="ifSentakuPopup" runat="server" scrolling="no" class="NyuryokuIframe" style="border-radius:0px;" ></iframe>
                            <asp:Button ID="btnClose" runat="server" Text="Button" Style="display: none" OnClick="btnClose_Click" onClientClick="MasterOverflow()" />
                            <asp:Button ID="btnJishaTantouSelect" runat="server" Text="Button" Style="display: none"  OnClick="btnJishaTantouSelect_Click" onClientClick="MasterOverflow()" />
                            <asp:Button ID="btnToLogin" runat="server" Text="Button" Style="display: none" OnClick="btnToLogin_Click" onClientClick="MasterOverflow()" />
                            <asp:Button ID="btnClose1" runat="server" Text="Button" Style="display: none" OnClick="btnClose1_Click" onClientClick="MasterOverflow()" />
                            <asp:Button ID="btnJyoutaiSelect" runat="server" Text="Button" Style="display: none" OnClick="btnJyoutaiSelect_Click" onClientClick="MasterOverflow()" />
                        </asp:Panel>
                    </asp:Panel>
                </ContentTemplate>
            </asp:UpdatePanel>

             <!--ポップアップ画面-->
            <asp:UpdatePanel ID="upddatePopup" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:Button ID="btndatePopup" runat="server" Text="Button" CssClass="DisplayNone" />
                    <asp:ModalPopupExtender ID="mpedatePopup" runat="server" TargetControlID="btndatePopup"
                        PopupControlID="pnldatePopupScroll" BackgroundCssClass="PopupModalBackground" BehaviorID="pnldatePopupScroll"
                        RepositionMode="RepositionOnWindowResize">
                    </asp:ModalPopupExtender>
                    <asp:Panel ID="pnldatePopupScroll" runat="server" Style="display: none;" CssClass="PopupScrollDiv">
                        <asp:Panel ID="pnldatePopup" runat="server">
                            <iframe id="ifdatePopup" runat="server" class="NyuryokuIframe RadiusIframe" scrolling="no"  style="max-width: 260px; min-width: 260px; max-height: 365px; min-height: 365px;"></iframe>
                            <asp:Button ID="btnCalendarClose" runat="server" Text="Button" CssClass="DisplayNone" OnClick="btnCalendarClose_Click" onClientClick="MasterOverflow()" />
                            <asp:Button ID="btnCalendarSettei" runat="server" Text="Button" CssClass="DisplayNone" OnClick="btnCalendarSettei_Click" onClientClick="MasterOverflow()" />
                        </asp:Panel>
                    </asp:Panel>
                </ContentTemplate>
            </asp:UpdatePanel>

             </ContentTemplate>
       </asp:UpdatePanel>
    </form>
</body>
 <link href="../Style/cloudflare-jquery-ui.min.css" rel="stylesheet" />
<script src="https://code.jquery.com/jquery-3.2.1.min.js"></script>
<script src="https://code.jquery.com/ui/1.12.1/jquery-ui.min.js"></script>
<script src="../Scripts/cloudflare-jquery-ui-i18n.min.js"></script>
<script type="text/javascript">
  Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(function(evt, args) {
        var userLang = navigator.language || navigator.userLanguage;

        var options = $.extend({},
            $.datepicker.regional["ja"], {
                dateFormat: "yy/mm/dd",
                beforeShow: function () {
                    $(".ui-datepicker").css('font-size', 14)
                },
                changeMonth: true,
                changeYear: true,
                highlightWeek: true,
                onSelect: function () {
                    //HF_fHidukeValueChange.value = "1";
                    //__doPostBack();
                    //document.getElementById('btnDateChange').click(); 
                    document.getElementById("<%=btnDateChange.ClientID %>").click();
                }
            }
        );

      $("#txtMitumoriStartDate").datepicker(options);
      $("#txtMitumoriEndDate").datepicker(options); 
      $("#txtJuchuuStartDate").datepicker(options);
      $("#txtJuchuuEndDate").datepicker(options);
      $("#txtUriageYoteiStartDate").datepicker(options);
      $("#txtUriageYoteiEndDate").datepicker(options);

      $("#fileUpload1").change(function () {
                         __doPostBack();
    });
    });
</script>
</html>
