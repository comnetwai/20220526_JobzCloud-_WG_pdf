<%@ Page Language="C#" MasterPageFile="~/WebFront/JC99NavBar.Master" AutoEventWireup="true" CodeBehind="JC34UriageList.aspx.cs" Inherits="jobzcolud.WebFront.JC34UriageList" ValidateRequest ="False"%>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <!DOCTYPE html>

    <html>
    <head>
        <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
        
        <link href="../Content/bootstrap.min.css" rel="stylesheet" />
        <script src="../Scripts/bootstrap.bundle.min.js"></script>

        <%--<script src="../Scripts/colResizable-1.6.min.js"></script>--%>
        <asp:PlaceHolder runat="server">
            <%: Scripts.Render("~/bundles/modernizr") %>
            <%: Styles.Render("~/style/StyleBundle1") %>
            <%: Scripts.Render("~/scripts/ScriptBundle1") %>
            <%: Styles.Render("~/style/UCStyleBundle") %>
        </asp:PlaceHolder>
        <title></title>

       <%-- <script type="text/javascript">
            $(function () {                $('#<%=GV_Uriage.ClientID %>').colResizable({                 liveDrag: true,                 gripInnerHtml: "<div class='grip'></div>",                 draggingClass: "dragging",             });         });  
    </script>--%>

          <style>
            .JC28CodeHeaderCol{
                padding-right:4px;
            }
        </style>

          </head>
    <body>
        
        <%-- <asp:PlaceHolder runat="server">            <%: Scripts.Render("~/bundles/jqueryui") %>        </asp:PlaceHolder>--%>
         <asp:UpdatePanel ID="updBody" runat="server" UpdateMode="Conditional">
          <ContentTemplate>
        <div class="container-fluid">
            <div style="margin-top: 30px;"></div>

            <div class="container-fluid bg-white JC34UriageListDiv" id="Div_UriageList" runat="server">
                <div class="d-flex justify-content-center align-content-center mt-4">
                    <%--<div style="overflow-x: auto;width:auto;overflow:hidden;">--%>
                    <asp:Label ID="lblLoginUserCode" runat="server" Text="" Visible="false"/>
                    <asp:Label ID="lblLoginUserName" runat="server" Text="" Visible="false"/>
                    <div class="JC34UriageKensakuDiv" style="margin-top:20px;">                        <%--<div class="row" style="margin-left:12px;">                            <label class="col-form-label fw-bold mt-2 mb-2">検索条件</label>                        </div>--%>                                                <div class="row " runat="server" style="margin-left:40px;margin-top:-5px;" >                            <div class="col-3">                                <label class="col-form-label font JC34labellayout2">商品名</label>                                  <%--<asp:Label ID="Label11" runat="server" CssClass="col-form-label font">商品名</asp:Label>--%>                                <asp:UpdatePanel ID="updShouhinmei" runat="server" UpdateMode="Conditional">                                    <ContentTemplate>                                <asp:TextBox ID="TB_Shouhinmei" runat="server" CssClass="form-control font TextboxStyle input JC34labellayout" TextMode="Search" Width="200" Height="28" ></asp:TextBox>                                 </ContentTemplate>                                </asp:UpdatePanel>                            </div>                            <div class="col-3">                                <label class="col-form-label font JC34labellayout2" >得意先</label>                                  <%--<asp:Label ID="Label9" runat="server" CssClass="col-form-label font">得意先</asp:Label>--%>                                <asp:UpdatePanel ID="updTokuisaki" runat="server" UpdateMode="Conditional">                                    <ContentTemplate>                                <asp:TextBox ID="TB_Tokuisaki" runat="server" CssClass="form-control font TextboxStyle input JC34labellayout" TextMode="Search" Width="200"  Height="28"></asp:TextBox>                                        </ContentTemplate>                                </asp:UpdatePanel>                            </div>                            <div class="col-3">                                <label class="col-form-label font JC34labellayout2">売上件名</label>                                <%--<asp:Label ID="Label2" runat="server" CssClass="col-form-label font">売上件名</asp:Label>--%>                                <asp:UpdatePanel ID="updUriageKenmei" runat="server" UpdateMode="Conditional">                                   <ContentTemplate>                                <asp:TextBox ID="TB_UriageKenmei" runat="server" CssClass="form-control font TextboxStyle input JC34labellayout" TextMode="Search" Width="200"  Height="28"></asp:TextBox>                                </ContentTemplate>                                </asp:UpdatePanel>                            </div>                             <div class="col-2" style="margin-top:30px;">                                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">                                    <ContentTemplate>                                        <asp:Button ID="BT_UriageSyosai" Text="詳細条件" runat="server" CssClass="JC34UriageSyosaiButton" Width="120px" Height="30px" Style="margin-left: 0px;" OnClick="BT_UriageSyosai_Click" />                                                                                <%--<asp:TextBox ID="TB_JishaTantousha" runat="server" CssClass="form-control font TextboxStyle"></asp:TextBox>--%>                                    </ContentTemplate>                                </asp:UpdatePanel>                            </div>                                                    </div>                                               <div id="divshowallcomponet" runat="server" style="background-color:rgb(250, 250, 250);width:850px;margin-left:40px;height:147px;margin-top:10px;margin-bottom:10px;" visible="false">
                         <asp:UpdatePanel ID="updbound" runat="server" UpdateMode="Conditional">
                         <ContentTemplate>                         <asp:Panel ID="Panel1" runat="server" >                        <div class="row ms-0" runat="server" >                            <div class="col-2" style="width:130px !important" >                                <label class="col-form-label font JC34labellayout2">売上コード</label>                                <%--<asp:Label ID="LB_UriLstCode" runat="server" CssClass="col-form-label font">売上コード</asp:Label>--%>                                <asp:UpdatePanel ID="updcUriageCode" runat="server" UpdateMode="Conditional">                                   <ContentTemplate>                                    <asp:TextBox ID="TB_UriageCode" runat="server" CssClass="form-control font TextboxStyle" AutoPostBack="true" Width="110"  Height="28" onkeypress="OnlyNumericEntry()" OnTextChanged="TB_UriageCode_TextChanged" onfocus="this.setSelectionRange(this.value.length, this.value.length);" TextMode="Search"></asp:TextBox>                               </ContentTemplate>                               </asp:UpdatePanel>                            </div>                            <div class="col-2" style="width:130px !important" >                               <label class="col-form-label font JC34labellayout2">見積コード</label>                                <%--<asp:Label ID="Label1" runat="server" CssClass="col-form-label font">見積コード</asp:Label>--%>                                <asp:UpdatePanel ID="updcMitsumoriCode" runat="server" UpdateMode="Conditional">                                   <ContentTemplate>                                    <asp:TextBox ID="TB_MitsumoriCode" runat="server" CssClass="form-control font TextboxStyle" AutoPostBack="true" Width="110" Height="28" onkeypress="OnlyNumericEntry()"  OnTextChanged="TB_MitsumoriCode_TextChanged" onfocus="this.setSelectionRange(this.value.length, this.value.length);" TextMode="Search"></asp:TextBox>                               </ContentTemplate>                                </asp:UpdatePanel>                            </div>                            <%--<div class="col-4 JC34labellayout">                                <asp:Label ID="Label2" runat="server" CssClass="col-form-label font">売上件名</asp:Label>                                <asp:UpdatePanel ID="updUriageKenmei" runat="server" UpdateMode="Conditional">                                   <ContentTemplate>                                <asp:TextBox ID="TB_UriageKenmei" runat="server" CssClass="form-control font TextboxStyle input" TextMode="Search" Width="266" ></asp:TextBox>                                </ContentTemplate>                                </asp:UpdatePanel>                            </div>--%>                            <div class="col-2 "style="width:140px !important">                                <asp:UpdatePanel ID="updTantousha" runat="server" UpdateMode="Conditional">                                    <ContentTemplate>                                        <label class="col-form-label font JC34labellayout2">自社担当者</label>                                        <%--<asp:Label ID="Label3" runat="server" CssClass="col-form-label font">自社担当者</asp:Label>--%>                                        <asp:Button ID="BT_Tantousya" Text="選択なし" runat="server" CssClass="JC34JishaTantou" Width="120px" Height="29px" Style="margin-left: 0px;" OnClientClick="PopupOpenButton()" OnClick="BT_Tantousya_Click" />                                        <asp:DropDownList ID="DDL_Tantousya" runat="server" Visible="false"></asp:DropDownList>                                         <asp:DropDownList ID="DDL_Jyouken" runat="server" Visible="false"></asp:DropDownList>                                        <%--<asp:TextBox ID="TB_JishaTantousha" runat="server" CssClass="form-control font TextboxStyle"></asp:TextBox>--%>                                    </ContentTemplate>                                </asp:UpdatePanel>                            </div>                            <div class="col-3" style="width:220px !important">                                <label class="col-form-label font JC34labellayout2">請求先</label>                                <%--<asp:Label ID="Label10" runat="server" CssClass="col-form-label font">請求先</asp:Label>--%>                                <asp:UpdatePanel ID="updSeikyusaki" runat="server" UpdateMode="Conditional">                                    <ContentTemplate>                                <asp:TextBox ID="TB_Seikyusaki" runat="server" CssClass="form-control font TextboxStyle" onfocus="this.setSelectionRange(this.value.length, this.value.length);" AutoPostBack="true" TextMode="Search" Width="200" Height="28"></asp:TextBox>                                        </ContentTemplate>                                </asp:UpdatePanel>                            </div>                            <div class="col-3 ">                                   <label class="col-form-label font JC34labellayout2">メモ</label>  <%--//20220429 Added By phoo--%>                                <%--<asp:Label ID="Label12" runat="server" CssClass="col-form-label font">売上社内メモ</asp:Label>--%>                                <asp:UpdatePanel ID="updUriageShanaiMemo" runat="server" UpdateMode="Conditional">                                    <ContentTemplate>                                <asp:TextBox ID="TB_UriageShanaiMemo" runat="server" CssClass="form-control font TextboxStyle" onfocus="this.setSelectionRange(this.value.length, this.value.length);" AutoPostBack="true" TextMode="Search" Width="185" Height="28"></asp:TextBox>                                </ContentTemplate>                                </asp:UpdatePanel>                            </div>                        </div>                        <div class="row mt-2 mb-1" runat="server" style="margin-left:1px;">                            <div class="col-4 " style="width:220px !important">                                <label class="col-form-label font JC34labellayout2">発行日</label>                                <%--<asp:Label ID="Label4" runat="server" CssClass="col-form-label font">発行日</asp:Label>--%>                                <table>                                    <tr>                                        <td>                                           <asp:UpdatePanel ID="updHakkouStartDate" runat="server" UpdateMode="Conditional">                                                    <ContentTemplate>                                                        <asp:Button ID="BT_HakkouStartDate" runat="server" Text="日付を設定" onmousedown="getTantouBoardScrollPosition();" CssClass="JC10GrayButton" Width="90px" Height="30px" OnClick="BT_HakkouStartDate_Click" OnClientClick="PopupOpenButton()"/>                                                        <div id="divHakkouStartDate" class="DisplayNone mt-1" runat="server" style="padding-bottom:4px;">                                                            <%--<asp:Button ID="BT_LeftArrowdHakkou" runat="server" Text="<" CssClass="DateArrowButton" OnClick="BT_LeftArrowdHakkou_Click" />--%>                                                            <asp:Label ID="LB_HakkouStart" runat="server" Text="" CssClass="GrayLabel" Width="100px"></asp:Label>                                                            <asp:Label ID="LB_HakkouStartDateYear" runat="server" CssClass="DisplayNone"></asp:Label>                                                            <asp:Button ID="BT_dHakkouStartCross" CssClass="CrossBtnGray" runat="server" Text="✕" OnClick="BT_dHakkouStartCross_Click" Width="22px" />                                                            <%--<asp:Button ID="BT_RightArrowdHakkou" runat="server" Text=">" CssClass="DateArrowButton" OnClick="BT_RightArrowdHakkou_Click" />--%>                                                        </div>                                                    </ContentTemplate>                                            </asp:UpdatePanel>                                             <%--<asp:UpdatePanel ID="updHakkouStartDate" runat="server" UpdateMode="Conditional">                                            <ContentTemplate>                                            <asp:TextBox ID="TB_HakkouStartDate" runat="server" MaxLength="10" CssClass="form-control font TextboxStyle" Width="122" onkeypress="DisableDateEntry()"                                                             onfocus="SetDateFocus();this.blur();"  AutoCompleteType="Disabled" TextMode="Search"></asp:TextBox>                                              <asp:Button ID="BT_HakkouStartDate" runat="server" CssClass="DisplayNone" OnClick="BT_HakkouStartDate_Click" />                                            </ContentTemplate>                                            </asp:UpdatePanel>--%>                                        </td>                                        <td>                                            <div style="margin-left:3px;margin-right:3px;">                                            <asp:Label ID="Label5" runat="server" CssClass="col-form-label LoginFont">～</asp:Label>                                            </div>                                        </td>                                       <td>                                           <asp:UpdatePanel ID="updHakkouEndDate" runat="server" UpdateMode="Conditional">                                                    <ContentTemplate>                                                        <asp:Button ID="BT_HakkouEndDate" runat="server" Text="日付を設定" onmousedown="getTantouBoardScrollPosition();" CssClass="JC10GrayButton" Width="90px" Height="30px" OnClick="BT_HakkouEndDate_Click" OnClientClick="PopupOpenButton()"/>                                                        <div id="divHakkouEndDate" class="DisplayNone mt-1" runat="server" style="padding-bottom:4px;">                                                            <%--<asp:Button ID="BT_LeftArrowdHakkou" runat="server" Text="<" CssClass="DateArrowButton" OnClick="BT_LeftArrowdHakkou_Click" />--%>                                                            <asp:Label ID="LB_HakkouEnd" runat="server" Text="" CssClass="GrayLabel" Width="100px"></asp:Label>                                                            <asp:Label ID="LB_HakkouEndDateYear" runat="server" CssClass="DisplayNone"></asp:Label>                                                            <asp:Button ID="BT_dHakkouEndCross" CssClass="CrossBtnGray" runat="server" Text="✕" OnClick="BT_dHakkouEndCross_Click" Width="22px"/>                                                            <%--<asp:Button ID="BT_RightArrowdHakkou" runat="server" Text=">" CssClass="DateArrowButton" OnClick="BT_RightArrowdHakkou_Click" />--%>                                                        </div>                                                    </ContentTemplate>                                            </asp:UpdatePanel>                                            <%--<asp:UpdatePanel ID="updHakkouEndDate" runat="server" UpdateMode="Conditional">                                            <ContentTemplate>                                           <asp:TextBox ID="TB_HakkouEndDate" runat="server" MaxLength="10" CssClass="form-control font TextboxStyle" Width="122" onkeypress="DisableDateEntry()"                                                            onfocus="SetDateFocus();this.blur();"  AutoCompleteType="Disabled" TextMode="Search"></asp:TextBox>                                            <asp:Button ID="BT_HakkouEndDate" runat="server" CssClass="DisplayNone" OnClick="BT_HakkouEndDate_Click" />                                            </ContentTemplate>                                            </asp:UpdatePanel>--%>                                       </td>                                      </tr>                                </table>                            </div>                                                        <div class="col-4"  style="width:220px !important">                                 <label class="col-form-label font JC34labellayout2">売上日</label>                                <%--<asp:Label ID="Label6" runat="server" CssClass="col-form-label font">売上日</asp:Label>--%>                                <table>                                    <tr>                                        <td>                                            <%--<asp:UpdatePanel ID="updUriageStartDate" runat="server" UpdateMode="Conditional">                                            <ContentTemplate>                                           <asp:TextBox ID="TB_UriageStartDate" runat="server" MaxLength="10" CssClass="form-control font TextboxStyle" Width="122" onkeypress="DisableDateEntry()"                                                            onfocus="SetDateFocus();this.blur();"  AutoCompleteType="Disabled" TextMode="Search"></asp:TextBox>                                            <asp:Button ID="BT_UriageStartDate" runat="server" CssClass="DisplayNone" OnClick="BT_UriageStartDate_Click" />                                            </ContentTemplate>                                            </asp:UpdatePanel>--%>                                            <asp:UpdatePanel ID="updUriageStartDate" runat="server" UpdateMode="Conditional">                                                    <ContentTemplate>                                                        <asp:Button ID="BT_UriageStartDate" runat="server" Text="日付を設定" onmousedown="getTantouBoardScrollPosition();" CssClass="JC10GrayButton" Width="90px" Height="30px" OnClick="BT_UriageStartDate_Click" OnClientClick="PopupOpenButton()"/>                                                        <div id="divUriageStartDate" class="DisplayNone mt-1" runat="server" style="padding-bottom:4px;">                                                            <%--<asp:Button ID="BT_LeftArrowdHakkou" runat="server" Text="<" CssClass="DateArrowButton" OnClick="BT_LeftArrowdHakkou_Click" />--%>                                                            <asp:Label ID="LB_UriageStart" runat="server" Text="" CssClass="GrayLabel" Width="100px"></asp:Label>                                                            <asp:Label ID="LB_UriageStartDateYear" runat="server" CssClass="DisplayNone"></asp:Label>                                                            <asp:Button ID="BT_dUriageStartCross" CssClass="CrossBtnGray" runat="server" Text="✕" OnClick="BT_dUriageStartCross_Click" Width="22px" />                                                            <%--<asp:Button ID="BT_RightArrowdHakkou" runat="server" Text=">" CssClass="DateArrowButton" OnClick="BT_RightArrowdHakkou_Click" />--%>                                                        </div>                                                    </ContentTemplate>                                            </asp:UpdatePanel>                                                                                          </td>                                        <td>                                            <div style="margin-left:3px;margin-right:3px;">                                            <asp:Label ID="Label7" runat="server" CssClass="col-form-label LoginFont">～</asp:Label>                                                </div>                                        </td>                                        <td>                                            <%--<asp:UpdatePanel ID="updUriageEndDate" runat="server" UpdateMode="Conditional">                                            <ContentTemplate>                                             <asp:TextBox ID="TB_UriageEndDate" runat="server" MaxLength="10" CssClass="form-control font TextboxStyle" Width="122" onkeypress="DisableDateEntry()"                                                            onfocus="SetDateFocus();this.blur();"  AutoCompleteType="Disabled" TextMode="Search"></asp:TextBox>                                            <asp:Button ID="BT_UriageEndDate" runat="server" CssClass="DisplayNone" OnClick="BT_UriageEndDate_Click" />                                            </ContentTemplate>                                            </asp:UpdatePanel>--%>                                            <asp:UpdatePanel ID="updUriageEndDate" runat="server" UpdateMode="Conditional">                                                    <ContentTemplate>                                                        <asp:Button ID="BT_UriageEndDate" runat="server" Text="日付を設定" onmousedown="getTantouBoardScrollPosition();" CssClass="JC10GrayButton" Width="90px" Height="30px" OnClick="BT_UriageEndDate_Click" OnClientClick="PopupOpenButton()"/>                                                        <div id="divUriageEndDate" class="DisplayNone mt-1" runat="server" style="padding-bottom:4px;">                                                            <%--<asp:Button ID="BT_LeftArrowdHakkou" runat="server" Text="<" CssClass="DateArrowButton" OnClick="BT_LeftArrowdHakkou_Click" />--%>                                                            <asp:Label ID="LB_UriageEnd" runat="server" Text="" CssClass="GrayLabel" Width="100px" ></asp:Label>                                                            <asp:Label ID="LB_UriageEndDateYear" runat="server" CssClass="DisplayNone"></asp:Label>                                                            <asp:Button ID="BT_dUriageEndCross" CssClass="CrossBtnGray" runat="server" Text="✕" OnClick="BT_dUriageEndCross_Click" Width="22px" />                                                            <%--<asp:Button ID="BT_RightArrowdHakkou" runat="server" Text=">" CssClass="DateArrowButton" OnClick="BT_RightArrowdHakkou_Click" />--%>                                                        </div>                                                    </ContentTemplate>                                            </asp:UpdatePanel>                                        </td>                                    </tr>                                </table>                            </div>                                                       <div class="col-2 ">                                <label class="col-form-label font JC34labellayout2">売上状態</label>                                <%--<asp:Label ID="Label8" runat="server" CssClass="col-form-label font">売上状態</asp:Label>--%>                                <asp:UpdatePanel ID="updUraigeJoutai" runat="server" UpdateMode="Conditional">                                    <ContentTemplate>                                <%--<asp:TextBox ID="TB_UraigeJoutai" runat="server" CssClass="form-control font TextboxStyle" onfocus="this.setSelectionRange(this.value.length, this.value.length);" AutoPostBack="true" TextMode="Search" ></asp:TextBox>--%>                                        <asp:DropDownList ID="DDL_UraigeJoutai" runat="server" CssClass="user_select font" Width="120px" AutoPostBack="True" Height="30px" style="border:1px solid #9b9b9b;">
                                          <asp:ListItem Selected="True"></asp:ListItem>
                                          <asp:ListItem Value="00" >作成中</asp:ListItem>
                                          <asp:ListItem Value="01">作成済</asp:ListItem>
                                          <asp:ListItem Value="02">請求締処理</asp:ListItem>
                                          <asp:ListItem Value="03">入金</asp:ListItem>
                                          <asp:ListItem Value="04">売掛締処理</asp:ListItem>
                                      </asp:DropDownList>                                    </ContentTemplate>                                </asp:UpdatePanel>                            </div>                        </div>                        <div class="row mt-2 mb-1" runat="server" style="margin-left:75px;margin-right:23px;">                            <%--<div class="col-3 JC34labellayout">                                <asp:Label ID="Label9" runat="server" CssClass="col-form-label font">得意先</asp:Label>                                <asp:UpdatePanel ID="updTokuisaki" runat="server" UpdateMode="Conditional">                                    <ContentTemplate>                                <asp:TextBox ID="TB_Tokuisaki" runat="server" CssClass="form-control font TextboxStyle input" TextMode="Search" Width="185"></asp:TextBox>                                        </ContentTemplate>                                </asp:UpdatePanel>                            </div>--%>                            <%--<div class="col-3 JC34labellayout">                                <asp:Label ID="Label10" runat="server" CssClass="col-form-label font">請求先</asp:Label>                                <asp:UpdatePanel ID="updSeikyusaki" runat="server" UpdateMode="Conditional">                                    <ContentTemplate>                                <asp:TextBox ID="TB_Seikyusaki" runat="server" CssClass="form-control font TextboxStyle input" TextMode="Search" Width="185"></asp:TextBox>                                        </ContentTemplate>                                </asp:UpdatePanel>                            </div>--%>                           <%-- <div class="col-3 JC34labellayout">                                <asp:Label ID="Label11" runat="server" CssClass="col-form-label font">商品名</asp:Label>                                <asp:UpdatePanel ID="updShouhinmei" runat="server" UpdateMode="Conditional">                                    <ContentTemplate>                                <asp:TextBox ID="TB_Shouhinmei" runat="server" CssClass="form-control font TextboxStyle input" TextMode="Search" Width="185"></asp:TextBox>                                 </ContentTemplate>                                </asp:UpdatePanel>                            </div>--%>                            <%--<div class="col-3 JC34labellayout">                                <asp:Label ID="Label12" runat="server" CssClass="col-form-label font">売上社内メモ</asp:Label>                                <asp:UpdatePanel ID="updUriageShanaiMemo" runat="server" UpdateMode="Conditional">                                    <ContentTemplate>                                <asp:TextBox ID="TB_UriageShanaiMemo" runat="server" CssClass="form-control font TextboxStyle input" TextMode="Search" Width="185"></asp:TextBox>                                </ContentTemplate>                                </asp:UpdatePanel>                            </div>--%>                        </div>                             </asp:Panel>                             </ContentTemplate>                             </asp:UpdatePanel>                            </div>                        <div class="row justify-content-center me-4" runat="server" style="margin-top:8px;margin-bottom:9px;">                            <div class="col-1 me-5 JC34labellayout1">                                <asp:Button ID="BT_UriageHyouji" runat="server" Text="絞り込み" class="BlueBackgroundButton JC10SaveBtn" Width="110px" OnClick="BT_UriageHyouji_Click" />                            </div>                            <%--<div class="col-sm-1"></div>--%>                            <div class="col-1 JC34labellayout1" style="margin-left:6px;">                                <asp:Button ID="BT_Clear" runat="server" Text="クリア" CssClass="JC10CancelBtn" Width="110px" OnClick="BT_Clear_Click" />                            </div>                            </div>                        </div>
                       <%-- </div>--%>

                </div>
                
                <div class="row" runat="server" >
                    <div class="col-sm-8"></div>
                    <div class="container-fluid bg-white">
                        
                        <asp:UpdatePanel ID="updJoken" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <div class="JC34KensakuJoken">
                                    <asp:Label ID="LB_Jouken" runat="server" Text="検索条件" Font-Size="15px" Font-Bold="true" ></asp:Label>
                                    
                                    <asp:Table ID="TB_SentakuJouken" runat="server" Style="margin-top: 2px;" >
                                        <asp:TableRow>
                                            <asp:TableCell ID="TC_Jouken" runat="server">  
                                              <%--<asp:Button ID="btnJokenCross" runat="server" Text="Button" OnClick="btnJokenCross_Click" style="display:none;" CssClass="JC12JokenCross"/>--%>

                                            </asp:TableCell>
                                        </asp:TableRow>
                                    </asp:Table>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                            
                    </div>
                   
                    <div class="container-fluid bg-white" align="right">                        <table style="margin-top: 20px;">                            <tr>                                <td>                                  <%--  <label class="col-form-label font">1-20</label>--%>                                                                        <asp:UpdatePanel ID="updhyoujisuryou" runat="server" UpdateMode="Conditional">                                        <ContentTemplate>                                            <asp:Label ID="LB_Show_Data" runat="server" Text="1_20" class="" Font-Size="13px" ></asp:Label>                                            <%--<asp:Label ID="Label13" runat="server" Text="/" class=""></asp:Label>                                            <asp:Label ID="LB_Total" runat="server" Text="" class="" Font-Size="13px"></asp:Label>--%>                                       </ContentTemplate>                                    </asp:UpdatePanel>                                </td>                                   <td>                                    <label class="col-form-label font ms-4 mt-1 ">表示件数</label>                                </td>                                <td>                                    <asp:DropDownList ID="DDL_Show_Count" runat="server" AutoPostBack="True" CssClass="JC34GridTextBox ms-3" Width="80px" Height="35px" Font-Size="13px" OnSelectedIndexChanged="DDL_Show_Count_SelectedIndexChanged" >                                        <asp:ListItem Text="20件" Value="1"></asp:ListItem>                                        <asp:ListItem Text="30件" Value="2"></asp:ListItem>                                        <asp:ListItem Text="50件" Value="3"></asp:ListItem>                                    </asp:DropDownList>                                </td>                                <td>                                    <div style="margin-left:2px;margin-right:20px;">                                    <asp:Button ID="BT_HyojikoumokuSettei" runat="server" Text="表示項目を設定" class="JC07HyojiItemSettingBtn" OnClick="BT_HyojikoumokuSettei_Click" OnClientClick="PopupOpenButton();"/>                                    </div>                                </td>                            </tr>                        </table>                    </div>
                </div>

                
               <%--<div id="LargeImageContainerDiv" style="position: absolute;bottom:30px; z-index:2"></div>--%>
                <div id="Div6" runat="server"  class="d-flex justify-content-center mt-3 mb-3" >
                    <asp:UpdatePanel ID="updUriageSyohinGrid" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                       <%--<div class="d-flex justify-content-center" style="background-color: white; padding: 0px 10px 0px 10px;">--%>
                    <div style="overflow-x: auto;width:auto;" id="Div7" >
                   <%-- <asp:UpdatePanel ID="updUriageSyohinGrid" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>--%>
                                <asp:GridView runat="server" ID="GV_Uriage" BorderColor="Transparent"  AutoGenerateColumns="false" EmptyDataRowStyle-CssClass="JC10NoDataMessageStyle" AllowPaging="true"  AllowSorting="True" OnSorting="GV_Uriage_Sorting" OnRowCreated="GV_Uriage_RowCreated"
                                    ShowHeader="true" ShowHeaderWhenEmpty="true" RowStyle-CssClass="GridRow"  CellPadding="0" AlternatingRowStyle-Wrap="True" RowStyle-BorderStyle="NotSet" CssClass="GridView1" ClientIDMode="Static"  OnPageIndexChanging="OnPageIndexChanging"  visible="True" OnRowDataBound="GV_Uriage_RowDataBound" OnRowCommand="GV_Uriage_RowCommand" DataKeyNames="売上コード" PagerSettings-Visible="False">
                                   <%--<PagerSettings Position="Bottom" Visible="False" /> 
                                    <PagerStyle horizontalalign="Center" CssClass="JC27gridPager"/>--%>
                                    <%--<PagerStyle CssClass="GridPager1" />--%>
                                    <%--<PagerSettings  Mode="NumericFirstLast" FirstPageText="&lt;"  LastPageText="&gt;" />--%>
                                    <EmptyDataRowStyle CssClass="JC30NoDataMessageStyle" />
                                    <HeaderStyle Height="37px" BackColor="#F2F2F2" />
                                    <RowStyle CssClass="JC34GridItem" Height="37px" />
                                    <EmptyDataTemplate>
                                     該当するデータがありません。
                                   </EmptyDataTemplate>
                                    <%--<SelectedRowStyle BackColor="#EBEBF5" />--%>
                                    
                                    <Columns>
                                         
                                    </Columns>
                                       
                                  <%-- <PagerStyle cssClass="gridpager" HorizontalAlign="Center" />--%>
                                </asp:GridView>
                               </div>
                           <%--</div>--%>
                                <asp:GridView runat="server" ID="GV_Uriage_Original" BorderColor="Transparent" AutoGenerateColumns="false" EmptyDataRowStyle-CssClass="JC10NoDataMessageStyle"
                                    ShowHeader="true" ShowHeaderWhenEmpty="true" CellPadding="0" AllowPaging="True" EnableViewState="false" Visible="false" PageSize="20" >
                                    <EmptyDataRowStyle CssClass="JC10NoDataMessageStyle" />
                                    <HeaderStyle Height="38px" BackColor="#F2F2F2" HorizontalAlign="Left"/>
                                   <%-- <PagerSettings Mode="NumericFirstLast" PageButtonCount="4" FirstPageText="<" LastPageText=">"/> 
                                    <PagerStyle horizontalalign="Center" CssClass="gridPager cssPager"/>--%>
                                    <%--<EmptyDataRowStyle CssClass="JC10NoDataMessageStyle" />--%>
                                    <RowStyle CssClass="JC34GridItem" Height="37px" />
                                   <%-- <SelectedRowStyle BackColor="#EBEBF5" />--%>
                                    <Columns>
                                        
                                          <%--<asp:TemplateField HeaderStyle-CssClass="JC28DropDownCol">
                                             <ItemTemplate> 
                                                 <div class="grip" style="display:flex; align-content:center;align-items:center;align-items: center;justify-content: center;min-width:30px; overflow: hidden;">
                                                <div class="dropdown" style="position:absolute;">
                                              <button class="btn dropdown-toggle" type="button" id="dropdownMenuButton" data-toggle="dropdown" 
                                                  aria-haspopup="true" aria-expanded="false" style="border:1px solid gainsboro;width:20px; height:20px;padding:0px 3px 0px 1px;margin:0;">--%>
                                            <asp:TemplateField HeaderStyle-Width="30px">
                                            <ItemTemplate>   
                                                 <div class="grip" style="display:flex; align-content:center;align-items:center;align-items: center;justify-content: center;min-width:30px; overflow: hidden;">
                                              <div class="dropdown" style="position:absolute;">
                                                  <button class="btn dropdown-toggle" type="button" id="dropdownMenuButton" data-toggle="dropdown" 
                                                      aria-haspopup="true" aria-expanded="false" style="border:1px solid gainsboro;width:20px; height:20px;padding:0px 3px 0px 1px;margin:0; z-index:200;">
                                    
                                              </button>
                                                    <asp:Button ID="BT_DeleteOk" runat="server" Text="はい" class="BlueBackgroundButton DisplayNone" Width="100px" Height="36px" OnClick="BT_DeleteOk_Click" />
                                                    <asp:Button ID="BT_DeleteCancel" runat="server" Text="キャンセル" class="JC09GrayButton DisplayNone" Width="100px" Height="36px" />
                    
                                              <div class="dropdown-menu fontcss" aria-labelledby="dropdownMenuButton">
                                                  <asp:LinkButton ID="LKB_UriageEdit" class="dropdown-item" runat="server" Text='編集' style="margin-right:10px" CommandName="Update" CommandArgument="<%# Container.DataItemIndex %>" OnClick="LKB_UriageEdit_Clicked" ></asp:LinkButton>
                                                  <%--<asp:LinkButton ID="LKB_UriageCopy" class="dropdown-item" runat="server" Text='コピーして登録' style="margin-right:10px" CommandName="Copy" CommandArgument="<%# Container.DataItemIndex %>" OnClick="LKB_UriageCopy_Clicked"></asp:LinkButton>--%>
                                                  <asp:LinkButton ID="LKB_UriagePDF" class="dropdown-item" runat="server" Text='兼請求書PDF出力' style="margin-right:10px" CommandName="PDF" CommandArgument="<%# Container.DataItemIndex %>"></asp:LinkButton>
                                                  <asp:LinkButton ID="lnkbtnMitsuDelete" class="dropdown-item" runat="server" Text='削除' style="margin-right:10px" CommandName="Delete" CommandArgument="<%# Container.DataItemIndex %>" OnClick="LKB_UriageDelete_Clicked" autopostback="true"></asp:LinkButton>                                        
                                              </div>
                                            </div>
                                                     </div>
                                        </ItemTemplate>
                                              <HeaderStyle />
                                             <%-- <ItemStyle Width="25px" />--%>
                                        </asp:TemplateField>

                                        <%--<asp:TemplateField ItemStyle-CssClass="JC28CodeCol" HeaderStyle-CssClass="JC28CodeHeaderCol">
                                            <ItemTemplate>
                                                <%--<asp:TextBox ID="txtcSYOHIN" runat="server" Text=' <%# Bind("cSYOHIN","{0}") %>' Width="91px" Height="25px" MaxLength="10" CssClass="form-control TextboxStyle JC10GridTextBox" autocomplete="off" AutoPostBack="true"></asp:TextBox>--%>
                                                <%--<asp:LinkButton ID="LKB_cUriage" runat="server" Text=' <%# Bind("売上コード","{0}") %>' CssClass="d-inline-block font" Width="89px" Font-Underline="false" OnClick="LKB_uriagecode_Click"></asp:LinkButton>
                                            </ItemTemplate>
                                            <HeaderTemplate>
                                                <asp:Label ID="LKB_cUriage1" runat="server" Text="売上コード" CssClass="d-inline-block" ></asp:Label>
                                            </HeaderTemplate>
                                            <HeaderStyle CssClass="JC28CodeHeaderCol" />
                                            <ItemStyle CssClass="JC28CodeCol" />
                                        </asp:TemplateField>--%>

                                        <asp:TemplateField HeaderText="売上コード" SortExpression="売上コード" HeaderStyle-Width="89px" HeaderStyle-ForeColor="Black">
                                        <ItemTemplate>
                                            <div class="grip" style="min-width: 89px; text-align: left; overflow: hidden; padding-right: 4px; white-space: nowrap; text-overflow: ellipsis; display: -webkit-box;  -webkit-line-clamp: 1; -webkit-box-orient: vertical;word-break: break-all;">
                                                <asp:LinkButton ID="LKB_cUriage" runat="server" Text=' <%# Bind("売上コード","{0}") %>' CssClass="d-inline-block font" Width="89px" Font-Underline="false" OnClick="LKB_uriagecode_Click" ></asp:LinkButton>
                                                
                                            </div>
                                        </ItemTemplate>
                                            <HeaderStyle CssClass="JC28CodeHeaderCol" />

                                        </asp:TemplateField>

                                         <%--<asp:TemplateField ItemStyle-CssClass="JC28CodeCol" HeaderStyle-CssClass="JC28CodeHeaderCol">
                                            <ItemTemplate>
                                                <asp:Label ID="LB_cMitsumori" runat="server" Text=' <%# Bind("見積コード","{0}") %>' CssClass="d-inline-block" Width="89px"></asp:Label>--%>
                                               <%-- <asp:LinkButton ID="LKB_cMitsumori" runat="server" Text=' <%# Bind("見積コード","{0}") %>' CssClass="d-inline-block font" Width="89px" Font-Underline="false" OnClick="LKB_mitsumoricode_Click"></asp:LinkButton>
                                            </ItemTemplate>
                                            <HeaderTemplate>
                                                <asp:Label ID="LB_cMitsumori1" runat="server" Text="見積コード" CssClass="d-inline-block" ></asp:Label>
                                            </HeaderTemplate>
                                            <HeaderStyle CssClass="JC28CodeHeaderCol" />
                                            <ItemStyle CssClass="JC28CodeCol" />
                                        </asp:TemplateField>--%>

                                        <asp:TemplateField HeaderText="見積コード" SortExpression="見積コード" HeaderStyle-Width="89px" HeaderStyle-ForeColor="Black">
                                        <ItemTemplate>
                                            <div class="grip" style="min-width: 89px; text-align: left; overflow: hidden; padding-right: 4px; white-space: nowrap; text-overflow: ellipsis; display: -webkit-box;  -webkit-line-clamp: 1; -webkit-box-orient: vertical;word-break: break-all;">
                                                <asp:LinkButton ID="LKB_cMitsumori" runat="server" Text=' <%# Bind("見積コード","{0}") %>' CssClass="d-inline-block font" Width="89px" Font-Underline="false" OnClick="LKB_mitsumoricode_Click" ></asp:LinkButton>
                                                
                                            </div>
                                        </ItemTemplate>
                                            <HeaderStyle CssClass="JC28CodeHeaderCol" />

                                        </asp:TemplateField>

                                        <%--<asp:TemplateField ItemStyle-CssClass="JC28NameCol" HeaderStyle-CssClass="JC28NameHeaderCol">
                                            <ItemTemplate>
                                                <div class="JC12LabelItem" style="width:150px;height:35px; ">
                                                <asp:Label ID="LB_sSeikyusaki" runat="server" Text=' <%# Bind("請求先名","{0}") %>' ToolTip='<%# Bind("請求先名","{0}") %>' style="text-align:left;width:150px;"></asp:Label>
                                             </div>
                                           </ItemTemplate>
                                            <HeaderTemplate>
                                                <asp:Label ID="LB_sSeikyusaki1" runat="server" Text="請求先名" CssClass="d-inline-block" Width="125px"></asp:Label>
                                            </HeaderTemplate>
                                            <HeaderStyle CssClass="JC28NameHeaderCol" />
                                            <ItemStyle CssClass="JC28NameCol" />
                                        </asp:TemplateField>--%>

                                       <asp:TemplateField HeaderText="請求先" SortExpression="請求先名" HeaderStyle-Width="150px" HeaderStyle-ForeColor="Black"> <%--//20220429 Added by phoo--%>
                                        <ItemTemplate>
                                              <div class="grip" style="min-width:150px;text-align: left; padding-right: 4px;padding-left:2px;overflow: hidden;  display: -webkit-box;  -webkit-line-clamp: 1; -webkit-box-orient: vertical;word-break: break-all;">
                                                <asp:Label ID="LB_sSeikyusaki" runat="server" Text=' <%# Bind("請求先名","{0}") %>' ToolTip='<%# Bind("請求先名","{0}") %>' style="text-align:left;"></asp:Label>
                                                
                                            </div>
                                        </ItemTemplate>
                                            <HeaderStyle CssClass="JC28CodeHeaderCol" />

                                        </asp:TemplateField>

                                       <%-- <asp:TemplateField ItemStyle-CssClass="JC28NameCol" HeaderStyle-CssClass="JC28NameHeaderCol">
                                            <ItemTemplate>
                                                <div class="JC12LabelItem" style="width:150px;height:35px; ">
                                                 <asp:Label ID="LB_sTokuisaki" runat="server" Text=' <%# Bind("得意先名","{0}") %>' ToolTip='<%# Bind("得意先名","{0}") %>' style="text-align:left;width:150px;"></asp:Label>
                                            </div>
                                             </ItemTemplate>
                                            <HeaderTemplate>
                                                <asp:Label ID="LB_sTokuisaki1" runat="server" Text="得意先名" CssClass="d-inline-block" Width="125px"></asp:Label>
                                            </HeaderTemplate>
                                            <HeaderStyle CssClass="JC28NameHeaderCol" />
                                            <ItemStyle CssClass="JC28NameCol" />
                                        </asp:TemplateField>--%>

                                       <asp:TemplateField HeaderText="得意先" SortExpression="得意先名" HeaderStyle-Width="150px" HeaderStyle-ForeColor="Black">   <%--//20220429 Added by phoo--%>
                                        <ItemTemplate>
                                              <div class="grip" style="min-width:150px;text-align: left; padding-right: 4px;padding-left:2px;overflow: hidden;  display: -webkit-box;  -webkit-line-clamp: 1; -webkit-box-orient: vertical;word-break: break-all;">
                                                <asp:Label ID="LB_sTokuisaki" runat="server" Text=' <%# Bind("得意先名","{0}") %>' ToolTip='<%# Bind("得意先名","{0}") %>' style="text-align:left;"></asp:Label>
                                                
                                            </div>
                                        </ItemTemplate>
                                            <HeaderStyle CssClass="JC28CodeHeaderCol" />

                                        </asp:TemplateField>

                                        <%--<asp:TemplateField ItemStyle-CssClass="JC28NameCol" HeaderStyle-CssClass="JC28NameHeaderCol">
                                            <ItemTemplate>
                                                <div class="JC12LabelItem" style="width:150px;height:35px;">
                                                <asp:Label ID="lblcSyohin1" runat="server" Text='<%# Server.HtmlEncode((string)Eval("売上件名","{0}"))%>' ToolTip='<%# Bind("売上件名","{0}") %>' style="text-align:left;width:150px;"></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderTemplate>
                                                <asp:Label ID="lblcSyohin" runat="server" Text="売上件名" CssClass="d-inline-block" Width="150px"></asp:Label>
                                            </HeaderTemplate>
                                            <HeaderStyle CssClass="JC28NameHeaderCol" />
                                            <ItemStyle CssClass="JC28NameCol" />
                                        </asp:TemplateField>--%>

                                        <asp:TemplateField HeaderText="売上件名" SortExpression="売上件名" HeaderStyle-Width="150px" HeaderStyle-ForeColor="Black">
                                        <ItemTemplate>
                                              <div class="grip" style="min-width:150px;text-align: left; padding-right: 4px;padding-left:2px;overflow: hidden;  display: -webkit-box;  -webkit-line-clamp: 1; -webkit-box-orient: vertical;word-break: break-all;">
                                                <asp:Label ID="lblcSyohin1" runat="server" Text='<%# Server.HtmlEncode((string)Eval("売上件名","{0}"))%>' ToolTip='<%# Bind("売上件名","{0}") %>' style="text-align:left;"></asp:Label>
                                                
                                            </div>
                                        </ItemTemplate>
                                            <HeaderStyle CssClass="JC28CodeHeaderCol" />

                                        </asp:TemplateField>

                                        <%--<asp:TemplateField ItemStyle-CssClass="JC28NameCol" HeaderStyle-CssClass="JC28NameHeaderCol">
                                            <ItemTemplate>
                                                <div class="JC12LabelItem" style="width:150px;height:35px;">
                                                <asp:Label ID="LB_Eigyoutantousha" runat="server" Text='<%# Server.HtmlEncode((string)Eval("営業担当者","{0}"))%>' ToolTip='<%# Bind("営業担当者","{0}") %>' style="text-align:left;width:150px;"></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderTemplate>
                                                <asp:Label ID="LB_Eigyoutantousha1" runat="server" Text="営業担当者" CssClass="d-inline-block"></asp:Label>
                                            </HeaderTemplate>
                                            <HeaderStyle CssClass="JC28NameHeaderCol" />
                                            <ItemStyle CssClass="JC28NameCol" />
                                        </asp:TemplateField>--%>

                                         <asp:TemplateField HeaderText="自社担当者" SortExpression="営業担当者" HeaderStyle-Width="150px" HeaderStyle-ForeColor="Black">  <%--//2022029 Added by  phoo--%>
                                        <ItemTemplate>
                                              <div class="grip" style="min-width:150px;text-align: left; padding-right: 4px;padding-left:2px;overflow: hidden;  display: -webkit-box;  -webkit-line-clamp: 1; -webkit-box-orient: vertical;word-break: break-all;">
                                                <asp:Label ID="LB_Eigyoutantousha" runat="server" Text='<%# Server.HtmlEncode((string)Eval("営業担当者","{0}"))%>' ToolTip='<%# Bind("営業担当者","{0}") %>' style="text-align:left;"></asp:Label>
                                                
                                            </div>
                                        </ItemTemplate>
                                            <HeaderStyle CssClass="JC28CodeHeaderCol"  />

                                        </asp:TemplateField>

                                        <%--<asp:TemplateField ItemStyle-CssClass="JC28CodeCol" HeaderStyle-CssClass="JC28CodeHeaderCol">
                                            <ItemTemplate>
                                                <asp:Label ID="Lb_UriageDate" runat="server" Text=' <%# Bind("売上日","{0}") %>' CssClass="d-inline-block" Width="125px"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderTemplate>
                                                <asp:Label ID="Lb_UriageDate1" runat="server" Text="売上日" CssClass="d-inline-block"></asp:Label>
                                            </HeaderTemplate>
                                            <HeaderStyle CssClass="JC28CodeHeaderCol" />
                                            <ItemStyle CssClass="JC28CodeCol" />
                                        </asp:TemplateField>--%>

                                         <asp:TemplateField HeaderText="売上日" SortExpression="売上日" HeaderStyle-Width="125px" HeaderStyle-ForeColor="Black">
                                        <ItemTemplate>
                                              <div class="grip" style="min-width:125px;text-align: left; padding-right: 4px;padding-left:2px;overflow: hidden;  display: -webkit-box;  -webkit-line-clamp: 1; -webkit-box-orient: vertical;word-break: break-all;">
                                                <asp:Label ID="Lb_UriageDate" runat="server" Text=' <%# Bind("売上日","{0}") %>' CssClass="d-inline-block"></asp:Label>
                                                
                                            </div>
                                        </ItemTemplate>
                                            <HeaderStyle CssClass="JC28CodeHeaderCol" />

                                        </asp:TemplateField>

                                        <%--<asp:TemplateField ItemStyle-CssClass="JC28KingakuCol" HeaderStyle-CssClass="JC28KingakuHeaderCol">
                                            <ItemTemplate>
                                                <div class="JC12LabelItem" style="width:110px;height:35px;">
                                                <asp:Label ID="LB_kingaku" runat="server" Text=' <%# Bind("売上金額","{0}") %>' ToolTip='<%# Bind("売上金額","{0}") %>' style="text-align:left;width:110px;"></asp:Label> 
                                                 </div>
                                                    </ItemTemplate>
                                            <HeaderTemplate>
                                                <asp:Label ID="LB_kingaku1" runat="server" Text="売上金額" CssClass="d-inline-block"></asp:Label>
                                            </HeaderTemplate>
                                            <HeaderStyle CssClass="JC28KingakuHeaderCol" />
                                            <ItemStyle CssClass="JC28KingakuCol" />
                                        </asp:TemplateField>--%>

                                        <asp:TemplateField HeaderText="売上金額" SortExpression="売上金額" HeaderStyle-Width="110px" HeaderStyle-ForeColor="Black">
                                        <ItemTemplate>
                                              <div class="grip" style="min-width:110px;text-align: right; padding-right: 4px;padding-left:2px;overflow: hidden;  display: -webkit-box;  -webkit-line-clamp: 1; -webkit-box-orient: vertical;word-break: break-all;">
                                                <asp:Label ID="LB_kingaku" runat="server" Text=' <%# Bind("売上金額","{0}") %>' ToolTip='<%# Bind("売上金額","{0}") %>' style="text-align:right;"></asp:Label> 
                                                
                                            </div>
                                        </ItemTemplate>
                                             <HeaderStyle CssClass="JC28CodeHeaderCol AlignRight"  />

                                        </asp:TemplateField>

                                        <%--<asp:TemplateField ItemStyle-CssClass="JC28JoutaiCol" HeaderStyle-CssClass="JC28JoutaiHeaderCol">
                                            <ItemTemplate>
                                                <asp:Label ID="LB_joutai" runat="server" Text=' <%# Bind("売上状態","{0}") %>' CssClass="d-inline-block"></asp:Label>                                              
                                            </ItemTemplate>
                                            <HeaderTemplate>
                                                <asp:Label ID="LB_joutai1" runat="server" Text="売上状態" CssClass="d-inline-block"></asp:Label>
                                            </HeaderTemplate>
                                            <HeaderStyle CssClass="JC28JoutaiHeaderCol" />
                                            <ItemStyle CssClass="JC28JoutaiCol" />
                                        </asp:TemplateField>--%>

                                        <asp:TemplateField HeaderText="売上状態" SortExpression="売上状態" HeaderStyle-Width="80px" HeaderStyle-ForeColor="Black">
                                        <ItemTemplate>
                                              <div class="grip" style="min-width:75px;text-align: left; padding-right: 4px;padding-left:2px;overflow: hidden;  display: -webkit-box;  -webkit-line-clamp: 1; -webkit-box-orient: vertical;word-break: break-all;">
                                                <asp:Label ID="LB_joutai" runat="server" Text=' <%# Bind("売上状態","{0}") %>' CssClass="d-inline-block"></asp:Label>  
                                                
                                            </div>
                                        </ItemTemplate>
                                            <HeaderStyle CssClass="JC28CodeHeaderCol" />

                                        </asp:TemplateField>

                                        <%--<asp:TemplateField ItemStyle-CssClass="JC28NameCol" HeaderStyle-CssClass="JC28NameHeaderCol">
                                            <ItemTemplate>
                                             <div class="JC12LabelItem" style="width:125px;height:35px;">
                                                <asp:Label ID="LB_memo" runat="server" Text='<%# Server.HtmlEncode((string)Eval("売上社内メモ","{0}"))%>' ToolTip='<%# Bind("売上社内メモ","{0}") %>' style="text-align:left;width:125px;"></asp:Label>
                                            </div>
                                            </ItemTemplate>
                                            <HeaderTemplate>
                                                <asp:Label ID="LB_memo1" runat="server" Text="売上社内メモ" CssClass="d-inline-block"></asp:Label>
                                            </HeaderTemplate>
                                            <HeaderStyle CssClass="JC28NameHeaderCol" />
                                            <ItemStyle CssClass="JC28NameCol " />
                                        </asp:TemplateField>--%>

                                        <asp:TemplateField HeaderText="メモ" SortExpression="売上社内メモ" HeaderStyle-Width="125px" HeaderStyle-ForeColor="Black"> <%--//20220429 Added by phoo--%>
                                        <ItemTemplate>
                                              <div class="grip" style="min-width:125px;text-align: left; padding-right: 4px;padding-left:2px;overflow: hidden;  display: -webkit-box;  -webkit-line-clamp: 1; -webkit-box-orient: vertical;word-break: break-all;">
                                                <asp:Label ID="LB_memo" runat="server" Text='<%# Server.HtmlEncode((string)Eval("売上社内メモ","{0}"))%>' ToolTip='<%# Bind("売上社内メモ","{0}") %>' style="text-align:left;"></asp:Label>
                                                
                                            </div>
                                        </ItemTemplate>
                                            <HeaderStyle CssClass="JC28CodeHeaderCol" />

                                        </asp:TemplateField>
                                                  
                                    </Columns>
                                </asp:GridView>
                              <div style="padding-bottom: 12px;">
                                <asp:GridView runat="server" ID="GridView1" BorderColor="Transparent" AutoGenerateColumns="true" EmptyDataRowStyle-CssClass="JC31NoDataMessageStyle"
                                            ShowHeader="false" ShowHeaderWhenEmpty="true" CellPadding="0" AllowPaging="True" PageSize="20" OnPageIndexChanging="OnPageIndexChanging"
                                             OnRowDataBound="GV_UriagePg_Original_RowDataBound" DataKeyNames="売上コード" Visible="True" CssClass="GridView1">
                                             
                                            <RowStyle CssClass="JC34GridItem" Height="37px" />
                                            <PagerStyle Font-Size="14px" HorizontalAlign="Center" CssClass="JC27gridPager" VerticalAlign="Middle" />
                                            <PagerSettings Mode="NumericFirstLast" FirstPageText="&lt;" LastPageText="&gt;" />
                                        </asp:GridView>
                                  </div>
                        
                                 
                            </ContentTemplate>
                       
                        </asp:UpdatePanel>
                        </div>
                                           
                   <%--<asp:TextBox ID="TB_Uriage_Code" runat="server" Value="" style="display:none;"></asp:TextBox>
                    <asp:Button ID="BT_Uriage_Display" runat="server" Text="" style="display:none;" OnClick="BT_Uriage_Display_Click" />--%>
                
                <%--<asp:GridView runat="server" ID="GridView1" BorderColor="Transparent" AutoGenerateColumns="true" EmptyDataRowStyle-CssClass="JC31NoDataMessageStyle"
                                            ShowHeader="false" ShowHeaderWhenEmpty="true" CellPadding="0" AllowPaging="True" PageSize="20" OnPageIndexChanging="OnPageIndexChanging"
                                             OnRowDataBound="GV_UriagePg_Original_RowDataBound" DataKeyNames="売上コード" Visible="True" PagerStyle-VerticalAlign="Middle" PagerStyle-HorizontalAlign="Center">
                                             


                                            <RowStyle Height="0px" />
                                            <PagerStyle Font-Size="14px" HorizontalAlign="Center" CssClass="JC27gridPager" VerticalAlign="Middle" />
                                            <PagerSettings Mode="NumericFirstLast" FirstPageText="&lt;" LastPageText="&gt;" />
                                        </asp:GridView>--%>
                    
                        
               <%-- <div id="grid_container">
    <table id="grid"></table>
    <div id="gridpager"></div>
</div>--%>
        <%-- <tr>
        <td colspan="7">
            <table>
            <tr class="JC27gridPager " >
                <td class="align-content-center"><span>1</span></td><td><a href="javascript:__doPostBack(&#39;ctl00$body$GV_Uriage&#39;,&#39;Page$2&#39;)">2</a></td><td><a href="javascript:__doPostBack(&#39;ctl00$body$GV_Uriage&#39;,&#39;Page$3&#39;)">3</a></td>
           </tr>
        </table>
        </td>
    </tr>--%>
                
                <asp:Button ID="BT_DateChange" runat="server" Text="Button" OnClick="BT_DateChange_Click" style="display:none;"/>
                <asp:Button ID="btnOK" runat="server" Text="OK" class="BlueBackgroundButton DisplayNone" Width="100px" Height="36px" />
                 
                <asp:UpdatePanel ID="upd_Hidden" runat="server" UpdateMode="Conditional" >
                            <Triggers>
                                 <asp:PostBackTrigger ControlID="BT_UriagePDF" />
                            </Triggers>
                             <ContentTemplate>
                         <asp:Button ID="BT_UriagePDF" runat="server" Text="Button" OnClick="BT_UriagePDF_Click" style="display:none;"/>
                        </ContentTemplate>  
                      </asp:UpdatePanel>

            </div>
        </div>
        <asp:HiddenField ID="hdnHome" runat="server" />
        <!--ポップアップ画面-->
        <asp:UpdatePanel ID="updSentakuPopup" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:Button ID="btnSentakuPopup" runat="server" Text="Button" CssClass="DisplayNone" />
                <asp:ModalPopupExtender ID="mpeSentakuPopup" runat="server" TargetControlID="btnSentakuPopup"
                    PopupControlID="pnlSentakuPopupScroll" BackgroundCssClass="PopupModalBackground" BehaviorID="pnlSentakuPopupScroll">
                </asp:ModalPopupExtender>
                <asp:Panel ID="pnlSentakuPopupScroll" runat="server" Style="display: none; overflow-x: hidden; overflow-y: hidden;" CssClass="PopupScrollDiv">
                    <asp:Panel ID="pnlSentakuPopup" runat="server">
                        <iframe id="ifSentakuPopup" runat="server" scrolling="no" class="NyuryokuIframe" style="border-radius: 0px;"></iframe>
                        <asp:Button ID="btnClose" runat="server" Text="Button" Style="display: none" OnClick="btnClose_Click" onClientClick="MasterOverflow();"/>
                        <asp:Button ID="btnJishaTantouSelect" runat="server" Text="Button" Style="display: none" OnClick="btnJishaTantouSelect_Click" onClientClick="MasterOverflow();"/>
                    </asp:Panel>
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>
        <!--ポップアップ画面-->
                    <asp:UpdatePanel ID="updHyoujiSet" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:Button ID="btnHyoujiSetting" runat="server" Text="Button" style="display:none" />
                            <asp:ModalPopupExtender ID="mpeHyoujiSetPopUp" runat="server" TargetControlID="btnHyoujiSetting"
                                PopupControlID="pnlHyoujiSetPopUpScroll" BehaviorID="pnlHyoujiSetPopUpScroll" BackgroundCssClass="PopupModalBackground"
                                RepositionMode="None">
                            </asp:ModalPopupExtender>
                            <asp:Panel ID="pnlHyoujiSetPopUpScroll" runat="server"  CssClass="n_PopupScrollDiv">
                                <asp:Panel ID="pnlHyoujiSetPopUp" runat="server">
                                    <iframe id="ifpnlHyoujiSetPopUp" runat="server" class="HyoujiSettingIframe"  seamless></iframe>
                                    <asp:Button ID="btnHyoujiClose" runat="server" Text="HyoujiClose" CssClass="DisplayNone" OnClick="btnHyoujiSettingClose_Click" onClientClick="MasterOverflow();"/>
                                       <asp:Button ID="btnHyoujiSave" runat="server" Text="HyoujiSave" CssClass="DisplayNone" OnClick="btnHyoujiSettingSave_Click" onClientClick="MasterOverflow();"/>
                                </asp:Panel>
                            </asp:Panel>
                        </ContentTemplate>
                    </asp:UpdatePanel>

               <!--ポップアップ画面-->            <asp:UpdatePanel ID="upddatePopup" runat="server" UpdateMode="Conditional">                <ContentTemplate>                    <asp:Button ID="btndatePopup" runat="server" Text="Button" CssClass="DisplayNone" />                    <asp:ModalPopupExtender ID="mpedatePopup" runat="server" TargetControlID="btndatePopup"                        PopupControlID="pnldatePopupScroll" BackgroundCssClass="PopupModalBackground" BehaviorID="pnldatePopupScroll"                        RepositionMode="RepositionOnWindowResize">                    </asp:ModalPopupExtender>                    <asp:Panel ID="pnldatePopupScroll" runat="server" Style="display: none;" CssClass="PopupScrollDiv">                        <asp:Panel ID="pnldatePopup" runat="server">                            <iframe id="ifdatePopup" runat="server" class="NyuryokuIframe RadiusIframe" scrolling="no"  style="max-width: 260px; min-width: 260px; max-height: 365px; min-height: 365px;"></iframe>                            <asp:Button ID="btnCalendarClose" runat="server" Text="Button" CssClass="DisplayNone" OnClick="btnCalendarClose_Click" onClientClick="MasterOverflow();"/>                            <asp:Button ID="btnCalendarSettei" runat="server" Text="Button" CssClass="DisplayNone" OnClick="btnCalendarSettei_Click" onClientClick="MasterOverflow();"/>                        </asp:Panel>                    </asp:Panel>                </ContentTemplate>            </asp:UpdatePanel>

              </ContentTemplate>
         </asp:UpdatePanel>
    </body>
  <link href="../Style/cloudflare-jquery-ui.min.css" rel="stylesheet" /><script src="https://code.jquery.com/jquery-3.2.1.min.js"></script><script src="https://code.jquery.com/ui/1.12.1/jquery-ui.min.js"></script><script src="../Scripts/cloudflare-jquery-ui-i18n.min.js"></script><script src="//ajax.googleapis.com/ajax/libs/jqueryui/1.11.4/i18n/jquery-ui-i18n.min.js"></script><%--<script src="Scripts/colResizable-1.3.min.js"></script>--%><%--<script src="../Scripts/colResizable-1.6.min.js" type="text/javascript"></script><script src="../Scripts/cookie.js" type="text/javascript"></script>--%>        <script type="text/javascript">
            function ClickPDFButton()
            {
                document.getElementById("<%=BT_UriagePDF.ClientID %>").click();
            }

            <%--function SetDateFocus() {                if (document.getElementById('<%=TB_HakkouStartDate.ClientID %>') == document.activeElement) {                    if (document.getElementById('<%=TB_HakkouStartDate.ClientID %>').value.length == 0) {                        document.getElementById('<%=BT_HakkouStartDate.ClientID %>').click();                    }                }                else if (document.getElementById('<%=TB_HakkouEndDate.ClientID %>') == document.activeElement) {                    if (document.getElementById('<%=TB_HakkouEndDate.ClientID %>').value.length == 0) {                        document.getElementById('<%=BT_HakkouEndDate.ClientID %>').click();                    }                }                else if (document.getElementById('<%=TB_UriageStartDate.ClientID %>') == document.activeElement) {                    if (document.getElementById('<%=TB_UriageStartDate.ClientID %>').value.length == 0) {                        document.getElementById('<%=BT_UriageStartDate.ClientID %>').click();                    }                }                else if (document.getElementById('<%=TB_UriageEndDate.ClientID %>') == document.activeElement) {                    if (document.getElementById('<%=TB_UriageEndDate.ClientID %>').value.length == 0) {                        document.getElementById('<%=BT_UriageEndDate.ClientID %>').click();                    }                }                            }--%>

            //$(function test() {            //    var thHeight = $('[id*=GV_Uriage] th:first').height();            //    //alert("thheight" + thHeight);            //    $('[id*=GV_Uriage] th').resizable({            //        handles: 'e',            //        minHeight: thHeight,            //        maxHeight: thHeight,            //        minWidth: 2,            //        resize: function (event, ui) {            //            var colIndex = ui.helper.index() + 1;            //            $('[id*=GV_Uriage] tr td:nth-child(' + colIndex + ')').width(ui.size.width);            //        }            //    });            //});

            //$(function () {            //    if ($.cookie('colWidthmUriage') != null) {            //        var columns = $.cookie('colWidthmUriage').split(',');            //        var i = 0;            //        $('.GridViewStyle th').each(function () {            //            $(this).width(columns[i++]);            //        });            //    }            //    else {            //        var columns = [30, 89, 89, 150, 150, 150, 150, 125, 110, 75, 125];            //        var i = 0;            //        $('.GridViewStyle th').each(function () {            //            $(this).width(columns[i++]);            //        });            //    }            //    $(".GridViewStyle").colResizable({            //        liveDrag: true,            //        resizeMode: 'overflow',            //        postbackSafe: true,            //        partialRefresh: true,            //        flush: true,            //        disabledColumns: ['10'],            //        gripInnerHtml: "<div class='grip'></div>",            //        draggingClass: "dragging",            //        onResize: onSampleResized            //    });            //});            //var onSampleResized = function (e) {            //    var columns = $(e.currentTarget).find("th");            //    var msg = "";            //    var date = new Date();            //    date.setTime(date.getTime() + (60 * 20000));            //    columns.each(function () {            //        msg += $(this).width() + ",";            //    })            //    $.cookie("colWidthmUriage", msg, { expires: date }); // expires after 20 minutes            //};            //var prm = Sys.WebForms.PageRequestManager.getInstance();            //if (prm != null) {            //    prm.add_endRequest(function (sender, e) {            //        if (sender._postBackSettings.panelsToUpdate != null) {            //            if ($.cookie('colWidthmUriage') != null) {            //                var columns = $.cookie('colWidthmUriage').split(',');            //                var i = 0;            //                $('.GridViewStyle th').each(function () {            //                    $(this).width(columns[i++]);            //                });            //            }            //            else {            //                var columns = [30, 89, 89, 150, 150, 150, 150, 125, 110, 75, 125];            //                var i = 0;            //                $('.GridViewStyle th').each(function () {            //                    $(this).width(columns[i++]);            //                });            //            }            //            $(".GridViewStyle").colResizable({            //                liveDrag: true,            //                resizeMode: 'overflow',            //                postbackSafe: true,            //                partialRefresh: true,            //                flush: true,            //                gripInnerHtml: "<div class='grip'></div>",            //                draggingClass: "dragging",            //                onResize: onSampleResized            //            });            //        }            //    });            //};


            //$(function () {
            //    if ($.cookie('colWidthMitsumoriList') != null) {
            //        var columns = $.cookie('colWidthMitsumoriList').split(',');
            //        var i = 0;
            //        $('.GridViewStyle th').each(function () {
            //            $(this).width(columns[i++]);
            //        });
            //    }

            //    $(".GridViewStyle").colResizable({
            //        liveDrag: true,
            //        resizeMode: 'overflow',
            //        postbackSafe: true,
            //        partialRefresh: true,
            //        flush: true,
            //        gripInnerHtml: "<div class='grip'></div>",
            //        draggingClass: "dragging",
            //        onResize: onSampleResized
            //    });

            //});


            //var onSampleResized = function (e) {
            //    var columns = $(e.currentTarget).find("th");
            //    var msg = "";
            //    var date = new Date();
            //    date.setTime(date.getTime() + (60 * 20000));
            //    columns.each(function () {
            //        msg += $(this).width() + ",";
            //    })
            //    $.cookie("colWidthMitsumoriList", msg, { expires: date }); // expires after 20 minutes
            //};

            //var prm = Sys.WebForms.PageRequestManager.getInstance();
            //if (prm != null) {
            //    prm.add_endRequest(function (sender, e) {
            //        if (sender._postBackSettings.panelsToUpdate != null) {
            //            if ($.cookie('colWidthMitsumoriList') != null) {
            //                var columns = $.cookie('colWidthMitsumoriList').split(',');
            //                var i = 0;
            //                $('.GridViewStyle th').each(function () {
            //                    $(this).width(columns[i++]);
            //                });
            //            }


            //            $(".GridViewStyle").colResizable({
            //                liveDrag: true,
            //                resizeMode: 'overflow',
            //                postbackSafe: true,
            //                partialRefresh: true,
            //                flush: true,
            //                gripInnerHtml: "<div class='grip'></div>",
            //                draggingClass: "dragging",
            //                onResize: onSampleResized
            //            });
            //        }
            //    });
            //};

            //$(function () {
            //    if ($.cookie('colWidthUriageList') != null) {
            //        var columns = $.cookie('colWidthUriageList').split(',');
            //        var i = 0;
            //        $('.GridViewStyle th').each(function () {
            //            $(this).width(columns[i++]);
            //        });
            //    }

            //    $(".GridViewStyle").colResizable({
            //        liveDrag: true,
            //        resizeMode: 'overflow',
            //        postbackSafe: true,
            //        partialRefresh: true,
            //        flush: true,
            //        gripInnerHtml: "<div class='grip'></div>",
            //        draggingClass: "dragging",
            //        onResize: onSampleResized
            //    });

            //});


            //var onSampleResized = function (e) {
            //    var columns = $(e.currentTarget).find("th");
            //    var msg = "";
            //    var date = new Date();
            //    //date.setTime(date.getTime() + (60 * 20000));
            //    columns.each(function () {
            //        msg += $(this).width() + ",";
            //    })
            //    $.cookie("colWidthUriageList", msg, { expires: date }); // expires after 20 minutes
            //};


            //var prm = Sys.WebForms.PageRequestManager.getInstance();
            //if (prm != null) {
            //    prm.add_endRequest(function (sender, e) {
            //        if (sender._postBackSettings.panelsToUpdate != null) {
            //            if ($.cookie('colWidthUriageList') != null) {
            //                var columns = $.cookie('colWidthUriageList').split(',');
            //                var i = 0;
            //                $('.GridViewStyle th').each(function () {
            //                    $(this).width(columns[i++]);
            //                });
            //            }


            //            $(".GridViewStyle").colResizable({
            //                liveDrag: true,
            //                resizeMode: 'overflow',
            //                postbackSafe: true,
            //                partialRefresh: true,
            //                flush: true,
            //                gripInnerHtml: "<div class='grip'></div>",
            //                draggingClass: "dragging",
            //                onResize: onSampleResized
            //            });
            //        }
            //    });
            //};

            //$(function () {
            //    if ($.cookie('colWidthUriageList') != null) {
            //        var columns = $.cookie('colWidthUriageList').split(',');
            //        var i = 0;
            //        $('.GridViewStyle th').each(function () {
            //            $(this).width(columns[i++]);
            //        });
            //    }

            //    $(".GridViewStyle").colResizable({
            //        liveDrag: true,
            //        resizeMode: 'overflow',
            //        postbackSafe: true,
            //        partialRefresh: true,
            //        flush: true,
            //        gripInnerHtml: "<div class='grip'></div>",
            //        draggingClass: "dragging",
            //        onResize: onSampleResized
            //    });

            //});


            //var onSampleResized = function (e) {
            //    var columns = $(e.currentTarget).find("th");
            //    var msg = "";
            //    var date = new Date();
            //    date.setTime(date.getTime() + (60 * 20000));
            //    columns.each(function () {
            //        msg += $(this).width() + ",";
            //    })
            //    $.cookie("colWidthUriageList", msg, { expires: date }); // expires after 20 minutes
            //};

            //var prm = Sys.WebForms.PageRequestManager.getInstance();
            //if (prm != null) {
            //    prm.add_endRequest(function (sender, e) {
            //        if (sender._postBackSettings.panelsToUpdate != null) {
            //            if ($.cookie('colWidthUriageList') != null) {
            //                var columns = $.cookie('colWidthUriageList').split(',');
            //                var i = 0;
            //                $('.GridViewStyle th').each(function () {
            //                    $(this).width(columns[i++]);
            //                });
            //            }


            //            $(".GridViewStyle").colResizable({
            //                liveDrag: true,
            //                resizeMode: 'overflow',
            //                postbackSafe: true,
            //                partialRefresh: true,
            //                flush: true,
            //                gripInnerHtml: "<div class='grip'></div>",
            //                draggingClass: "dragging",
            //                onResize: onSampleResized
            //            });
            //        }
            //    });
            //};

          </script>       
<%--<script type="text/javascript">    Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(function (evt, args) {        var userLang = navigator.language || navigator.userLanguage;        var options = $.extend({},            $.datepicker.regional["ja"], {                dateFormat: "yy/mm/dd",            beforeShow: function () {                $(".ui-datepicker").css('font-size', 14)            },            changeMonth: true,            changeYear: true,            highlightWeek: true,            onSelect: function () {                //HF_fHidukeValueChange.value = "1";                //__doPostBack();                //document.getElementById('btnDateChange').click();                 document.getElementById("<%=BT_DateChange.ClientID %>").click();            }        }        );      $('#' + '<%= TB_HakkouStartDate.ClientID %>').datepicker(options);      $('#' + '<%= TB_HakkouEndDate.ClientID %>').datepicker(options);      $('#' + '<%= TB_UriageStartDate.ClientID %>').datepicker(options);      $('#' + '<%= TB_UriageEndDate.ClientID %>').datepicker(options);              $("#fileUpload1").change(function () {          __doPostBack();      });    });    $(function () {
        $("#GV_Uriage").ejGrid({
            dataSource: window.gridData,
            allowPaging: true,
            pageSettings: { pageSize: 14 }
        });
    });    </script> --%> 
</html>
</asp:Content>
