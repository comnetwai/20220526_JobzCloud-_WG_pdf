<%@ Page Language="C#" MasterPageFile="~/WebFront/JC99NavBar.Master" AutoEventWireup="true" CodeBehind="JC27UriageTouroku.aspx.cs" Inherits="jobzcolud.WebFront.JC27UriageTouroku" ValidateRequest ="False" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server" >
    <!DOCTYPE html>


<html>
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <link href="../Content/bootstrap.min.css" rel="stylesheet" />
    <script src="../Scripts/bootstrap.bundle.min.js"></script>
     <asp:PlaceHolder runat="server">
        <%: Scripts.Render("~/bundles/modernizr") %>
        <%: Styles.Render("~/style/StyleBundle1") %>
        <%: Scripts.Render("~/scripts/ScriptBundle1") %>
        <%: Styles.Render("~/style/UCStyleBundle") %>

    </asp:PlaceHolder>
    <title></title>
            <style>
             .JC27TextBox {
                width:88px; 
                max-width: 88px; 
                height: 28px !important;
                text-align:right;
            }
        </style>
    <script type="text/javascript">

        //20220520 Eaindary,Chaw Added Start
            var validJapanese = new RegExp(/^[A-Za-z0-9]*$/);
                var lastValidChar = "";
                function validateJapanese(elem) {
                    if (validJapanese.test(elem.value.replaceAll(",", "")))
                    {
                         lastValidChar = elem.value;
                   }
                  else
                    {
                        if (lastValidChar == undefined) {
                            lastValidChar = "";
                        }
                        elem.value = lastValidChar;
                    }
                }
            //20220520 Eaindary,Chaw Added End


        function ChangeTypeTel(ctrl) {
            ctrl.setAttribute("type", "tel");
            ctrl.focus();
            ctrl.setSelectionRange(ctrl.value.length, ctrl.value.length);
        }

        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode > 31 && (charCode < 48 || charCode > 57))
                return false;
            return true;
        }

        Sys.Application.add_load(function () {

            $(".gvUriageSyohin").sortable({
                items: 'tr:not(tr:first-child)',
                cursor: 'pointer',
                //handle: '.dragBtn',<%--20220504Myatnoe,Chaw Deleted--%>
                axis: 'y',
                dropOnEmpty: false,
                start: function (e, ui) {
                    ui.item.addClass("selected");
                    document.getElementById("<%=HF_beforeSortIndex.ClientID%>").value = ui.item.index();
                },
                stop: function (e, ui) {
                    ui.item.removeClass("selected");
                    document.getElementById("<%=HF_afterSortIndex.ClientID%>").value = ui.item.index();
            //alert(document.getElementById("<%=HF_beforeSortIndex.ClientID%>").value + ',' + document.getElementById("<%=HF_afterSortIndex.ClientID%>").value);
                    document.getElementById("<%=BT_Sort.ClientID%>").click();

                },
                receive: function (e, ui) {
                    $(this).find("tbody").append(ui.item);
                }
            });
        });

</script>
</head>
<body>
    
        <div style="margin-top:0px;"> </div>
    <div id="Div_shousai" runat="server" >
        <div class="container-fluid bg-white JC27UriageDiv" >
        <div class="row">
            <asp:Label ID="lblLoginUserCode" runat="server" Text="" Visible="false"/>
            <asp:Label ID="lblLoginUserName" runat="server" Text="" Visible="false"/>
            <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
             <ContentTemplate>
             <div class="row gap-2" style="margin-top:20px;">                                         <div class="col-sm-1" style="margin-left:8px;">
                        <asp:Button ID="BT_Uriagehozon" runat="server" Text="売上を保存" class="BlueBackgroundButton JC10SaveBtn"  OnClick="BT_Uriagehozon_Click" />
                    </div>
                    <div class="col-sm-1" style="margin-left:-5px;" >
                       <asp:Button ID="BT_Pdf" runat="server" Text="兼請求書PDF出力" class="BlueBackgroundButton JC10SaveBtn" Width="190px" OnClick="BT_Pdf_Click"/>
                    </div>
                    <div class="col-sm-1" style="margin-left:90px;">
                        <asp:Button ID="BT_Sakujo" runat="server" Text="削除" class="JC10DeleteBtn" OnClick="BT_Sakujo_Click"/>
                    </div>                </div>
                </ContentTemplate>
                 <Triggers>
                     <asp:AsyncPostBackTrigger ControlID="BT_Uriagehozon" EventName="Click"/>
                     <asp:AsyncPostBackTrigger ControlID="BT_Sakujo" EventName="Click"/>
                     <asp:PostBackTrigger ControlID="BT_Pdf" />
                     <asp:PostBackTrigger ControlID="btnYes1" />
                     <asp:PostBackTrigger ControlID="btnNo1" />
                     <asp:PostBackTrigger ControlID="btnCancel1" />
                    </Triggers>
                </asp:UpdatePanel>

            <div class="row">

            <div class="col-sm" style="margin-left:8px;" >
               
                <div class="row mt-2">
                    
                    <div class="col-sm-0">
                        <label class="col-form-label font">売上コード </label>  
                    </div>
                    <div class="col-sm-4 mt-1">
                        <asp:UpdatePanel ID="upd_uriagecode" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <asp:Label ID="LB_Uriage_Code" runat="server" Text="" CssClass="col-form-label font JC27labellayout"></asp:Label>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                    </div>
                    <div class="col-sm-0">
                        <label class="col-form-label font">見積コード</label>
                    </div>
                    <div class="col-sm-2 mt-1">
                        <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                          <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="BT_Mitsumori_Code"/>
                           </Triggers>
                          <ContentTemplate>
                      <%-- <asp:Label ID="LB_Mitsumori_Code" runat="server" Text="" CssClass="col-form-label font"></asp:Label>--%>
                        <asp:Label ID="LB_Mitsumori_Code" runat="server" Text="" CssClass="JClinkBtPopup JC27labellayout" ></asp:Label>
                        <asp:Button ID="BT_Mitsumori_Code" Text="見積コード" runat="server" style="display:none;" OnClick="BT_Mitsumori_Code_Click"/>            
                   </ContentTemplate>
                            </asp:UpdatePanel>
                              </div>
                               
                </div>
                <div class="row mt-2">
                    <div class="col-sm-0">
                        <label class="col-form-label font">売上件名 </label>  
                    </div>
                    <div class="col-sm-9">
                        <asp:UpdatePanel ID="updtxtUriagekenmei" runat="server" UpdateMode="Conditional">
                          <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="TB_Uriagekenmei" EventName="TextChanged"/>
                           </Triggers>
                          <ContentTemplate>
                     <asp:TextBox ID="TB_Uriagekenmei" runat="server" name="" CssClass="form-control font TextboxStyle" OnTextChanged="TB_Uriagekenmei_TextChanged" autopostback="true" Width="455px"></asp:TextBox>
                          </ContentTemplate>
                         </asp:UpdatePanel>
                    </div>
                    
                </div>
                <div class="row mt-2">                    <div class="col-sm-0">                        <label class="col-form-label font">自社担当者 </label>                      </div>                    <div class="col-sm-4">                        <%--<asp:Button ID="BT_Eigyoutantousha" runat="server" Text="追加" CssClass="JC10GrayButton" onmousedown="getTantouBoardScrollPosition();" />--%>                        <asp:UpdatePanel ID="upd_EIGYOUTANTOUSHA" runat="server" UpdateMode="Conditional">                            <ContentTemplate>                                <div id="divTantousyaBtn" runat="server">                                    <asp:Button ID="BT_EigyouTantousya_Add" runat="server" Text="追加" CssClass="JC10GrayButton" onmousedown="getTantouBoardScrollPosition();" OnClientClick="PopupOpenButton()" OnClick="BT_EigyouTantousya_Add_Click" Height="30px"/>                                </div>                                <div id="divTantousyaLabel" runat="server" style="display:none;" class="mt-1">                                    <div style="float:left;max-width:150px;text-overflow:ellipsis;white-space: nowrap;overflow: hidden;" >                                    <asp:Label ID="lblsJISHATANTOUSHA" runat="server" cssClass="JClinkBtPopup JC27labellayout">先小岡太郎</asp:Label>                                    <asp:Label ID="lblcJISHATANTOUSHA" runat="server" Visible="false">先小岡太郎</asp:Label>                                </div>                                    <asp:Button ID="BT_sEIGYOUTANTOUSHA_Cross" runat="server" BackColor="White"  Text="✕" style="vertical-align:middle;margin-left:10px; font-weight:bolder; height:30px;border:none;" OnClick="BT_sEIGYOUTANTOUSHA_Cross_Click" />                                </div>                                                              </ContentTemplate>                        </asp:UpdatePanel>                    </div>                    <div class="col-sm-0">                        <label class="col-form-label font">拠点</label>                    </div>                    <div class="col-sm-2 mt-1">                       <asp:Label ID="LB_skyoten" runat="server" Text="" CssClass="col-form-label font JC27labellayout"></asp:Label>                                                       </div>                </div>
                 <div class="row mt-2">
                    <div class="col-sm-0">
                        <label class="col-form-label font">発行日</label>  
                    </div>
                    <div class="col-sm-4">
                        <asp:UpdatePanel ID="updHakkouDate" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <asp:Button ID="BT_HakkouDate" runat="server" Text="日付を設定" onmousedown="getTantouBoardScrollPosition();" CssClass="JC10GrayButton" OnClientClick="PopupOpenButton()" Width="90px" OnClick="BT_HakkouDate_Click" Height="30px" />
                                <div id="divHakkouDate" class="DisplayNone mt-1" runat="server" style="padding-bottom:4px;">
                                    <%--<asp:Button ID="BT_LeftArrowdHakkou" runat="server" Text="<" CssClass="DateArrowButton" OnClick="BT_LeftArrowdHakkou_Click" />--%>
                                    <asp:Label ID="LB_Hakkou" runat="server" Text="" CssClass="GrayLabel"></asp:Label>
                                    <asp:Label ID="LB_HakkouDateYear" runat="server" CssClass="DisplayNone"></asp:Label>
                                    <asp:Button ID="BT_dHakkouCross" CssClass="CrossBtnGray" runat="server" Text="✕" OnClick="BT_dHakkouCross_Click" />
                                    <%--<asp:Button ID="BT_RightArrowdHakkou" runat="server" Text=">" CssClass="DateArrowButton" OnClick="BT_RightArrowdHakkou_Click" />--%>
                                </div>
                            </ContentTemplate>
                    </asp:UpdatePanel> 
                       
                    </div>
                    <div class="col-sm-0">
                        <label class="col-form-label font">売上日</label>
                    </div>
                     <div class="col-sm-4">
                         <asp:UpdatePanel ID="updUriageDate" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <asp:Button ID="BT_UriageDate" runat="server" Text="日付を設定" CssClass="JC10GrayButton" onmousedown="getTantouBoardScrollPosition();" OnClientClick="PopupOpenButton()" Width="90px" OnClick="BT_UriageDate_Click" Height="30px"/>
                                <div id="divUriageDate" class="DisplayNone mt-1" runat="server">
                                    <%--<asp:Button ID="BT_LeftArrowdUriage" runat="server" Text="<" CssClass="DateArrowButton" OnClick="BT_LeftArrowdUriage_Click" />--%>
                                    <asp:Label ID="LB_Uriage" runat="server" Text="" CssClass="GrayLabel"></asp:Label>
                                    <asp:Label ID="LB_UriageDateYear" runat="server" CssClass="DisplayNone"></asp:Label>
                                    <asp:Button ID="BT_dUriageCross" CssClass="CrossBtnGray" runat="server" Text="✕" OnClick="BT_dUriageCross_Click" />
                                    <%--<asp:Button ID="BT_RightArrowdUriage" runat="server" Text=">" CssClass="DateArrowButton" OnClick="BT_RightArrowdUriage_Click" />--%>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
                 <div class="row mt-2">
                                <div class="col-sm-0">
                                    <label class="col-form-label font">入金予定日</label>
                                </div>
                                <div class="col-sm-4">
                                    <asp:UpdatePanel ID="updNyuukinYouteiDate" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:Button ID="BT_NyuukinYouteiDate" runat="server" Text="日付を設定" CssClass="JC10GrayButton" onmousedown="getTantouBoardScrollPosition();" OnClientClick="PopupOpenButton()" Width="90px" OnClick="BT_NyuukinYouteiDate_Click" Height="30px" />
                                            <div id="divNyuukinYouteiDate" class="DisplayNone mt-1" runat="server">
                                                <%--<asp:Button ID="BT_LeftArrowdNyuukinYoutei" runat="server" Text="<" CssClass="DateArrowButton" OnClick="BT_LeftArrowdNyuukinYoutei_Click" />--%>
                                                <asp:Label ID="LB_NyuukinYoutei" runat="server" Text="" CssClass="GrayLabel"></asp:Label>
                                                <asp:Label ID="LB_NyuukinYouteiDateYear" runat="server" CssClass="DisplayNone"></asp:Label>
                                                <asp:Button ID="BT_dNyuukinYouteiCross" CssClass="CrossBtnGray" runat="server" Text="✕" OnClick="BT_dNyuukinYouteiCross_Click" />
                                                <%--<asp:Button ID="BT_RightArrowdNyuukinYoutei" runat="server" Text=">" CssClass="DateArrowButton" OnClick="BT_RightArrowdNyuukinYoutei_Click" />--%>
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                                <%--20220518 Chaw Added Start--%>
                                <div class="col-sm-0">
                                    <label class="col-form-label font">自社番号</label>
                                </div>
                                <div class="col-sm-4">
                                    <div class="no-ime" style="float: left; width: 150px;position: relative; text-align: center;">
                                        <asp:TextBox ID="tb_jishabangou" AutoPostBack="true" runat="server" CssClass="form-control TextboxStyle JC27TextBox" onpaste="return false"
                                            onkeyup="DeSelectText(this);" onfocus="this.setSelectionRange(this.value.length, this.value.length);" oninput="validateJapanese(this);" MaxLength="10" AutoCompleteType="Disabled" OnTextChanged="tb_jishabangou_TextChanged" ></asp:TextBox>
                                        <asp:FilteredTextBoxExtender ID="TBF_Jishabangou" runat="server" FilterType="UppercaseLetters, LowercaseLetters, Numbers" TargetControlID="tb_jishabangou" />
                                    </div>
                                </div>
                            </div>
                            <%--20220518 Chaw Added End--%>
                 <asp:UpdatePanel ID="updjyuchu" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                    <div class="row mt-2">
                        <div class="col-sm-0">
                            <label class="col-form-label font">受注額</label>  
                        </div>
                        <div class="col-sm-4 mt-1">
                            <asp:Label ID="CB_Jyuchuukingaku" runat="server" Text="0" CssClass="col-form-label font JC27labellayout"></asp:Label>

                        </div>
                        <div class="col-sm-0">
                            <label class="col-form-label font">受注残</label>
                        </div>
                        <div class="col-sm-2 mt-1">  
                            <asp:Label ID="CB_Jyuchuuzan" runat="server" Text="0" CssClass="col-form-label font JC27labellayout"></asp:Label>
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                            <ContentTemplate> 
                                <asp:Label ID="CB_NouhinKingaku" runat="server" Text="0" CssClass="DisplayNone" ></asp:Label>
                            </ContentTemplate>
                            </asp:UpdatePanel>
                         </div>
                    </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
                <div class="row mt-2">
                    <div class="col-sm-0">
                        <label class="col-form-label font">売上状態</label>  
                    </div>
                    <div class="col-sm mt-1">
                     <%--<asp:Button ID="BT_Uriagejoutai" runat="server" Text="追加" CssClass="JC10GrayButton" onmousedown="getTantouBoardScrollPosition();" />--%>
                     <asp:Label ID="LB_Uriage_Jyoutai" runat="server" Text="" CssClass="col-form-label font JC27labellayout"></asp:Label>
                    </div>
                </div>
                <div class="row mt-2 ">
                    <div class="col-sm-0">
                        <label class="col-form-label font">伝票備考 </label>  
                    </div>
                    <div class="col-sm-9">
                   <asp:UpdatePanel ID="updtxtDenpyoubikou" runat="server" UpdateMode="Conditional">
                          <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="TB_Denpyoubikou" EventName="TextChanged"/>
                           </Triggers>
                          <ContentTemplate>
                     <asp:TextBox ID="TB_Denpyoubikou" runat="server" name="username" CssClass="form-control font TextboxStyle JC27BorderColor" TextMode="MultiLine" Height="100" OnTextChanged="TB_Denpyoubikou_TextChanged" AutoPostBack="true" Width="455px"></asp:TextBox>
                    </ContentTemplate>
                       </asp:UpdatePanel>
                    </div>
                    
                </div>
                <div class="row mt-2">
                    <div class="col-sm-0">
                        <label class="col-form-label font">作成日</label>  
                    </div>
                    <div class="col-sm-4 mt-1">
                       <%--<asp:Label ID="LB_SakuseiDate" runat="server" Text="" CssClass="col-form-label font JC27labellayout"></asp:Label>--%>
                        <asp:UpdatePanel ID="updSakuseiDate" runat="server" UpdateMode="Conditional">                            <ContentTemplate>                                <asp:Label ID="LB_SakuseiDate" runat="server" Text="" CssClass="col-form-label font JC27labellayout"></asp:Label>                                </ContentTemplate>                       </asp:UpdatePanel>
                    </div>
                    <div class="col-sm-0">
                        <label class="col-form-label font">作成者</label>
                    </div>
                    <div class="col-sm-3" style="margin-top:3px;">
                      <%-- <asp:Label ID="LB_cSakuseisha" runat="server" CssClass="col-form-label font JC27labellayout" Visible="false"></asp:Label>
                       <asp:Label ID="LB_sSakuseisha" runat="server" CssClass="col-form-label font JC27labellayout"></asp:Label>--%>
                        <asp:UpdatePanel ID="updSakuseisha" runat="server" UpdateMode="Conditional">                            <ContentTemplate>                       <asp:Label ID="LB_cSakuseisha" runat="server" CssClass="col-form-label font JC27labellayout" Visible="false"></asp:Label>                       <asp:Label ID="LB_sSakuseisha" runat="server" CssClass="col-form-label font JC27labellayout"></asp:Label>                                </ContentTemplate>                       </asp:UpdatePanel>
                    </div>
                </div>
            </div>
        
            <div class="col-sm ">
                <div class="row mt-2 me-1" >
                   <div class="col-sm-2 mt-1">
                        <asp:Label ID="lblTokuisakiLabel" runat="server" Text="得意先" CssClass="JC27labellayout"></asp:Label>
                      <%-- <label class="col-form-label font">得意先</label>  --%>
                    </div>
                    <div class="col-sm">
                   <asp:UpdatePanel ID="updTokuisaki" runat="server" UpdateMode="Conditional">
                     <ContentTemplate>
                       <div id="divTokuisakiBtn" runat="server">
                         <asp:Button runat="server" ID="BT_Tokuisaki"  Text="追加" CssClass="JC10GrayButton" onmousedown="getTantouBoardScrollPosition();" OnClientClick="PopupOpenButton()" OnClick="BT_Tokuisaki_Click" Height="30px"/>
                       </div>
                         <div style="float:left;max-width: 300px; display: none;text-overflow:ellipsis;white-space: nowrap;overflow: hidden;margin-top:5px;margin-right:20px;" id="divTokuisakiLabel" runat="server">
                           <asp:Label ID="LB_sTOKUISAKI" runat="server" Text="" cssClass="JClinkBtPopup JC27labellayout"></asp:Label>
                            <asp:Label runat="server" ID="LB_cTOKUISAKI" Visible="false"></asp:Label>
                             </div>
                              <div id="divTokuisakiSyosai" runat="server" style="display:none;">
                                <asp:Button runat="server" ID="BT_sTOKUISAKI_Syousai"  Text="詳細"　CssClass="JC10GrayButton" OnClick="BT_sTOKUISAKI_Syousai_Click" Height="30px"/>
                               </div>    
                       </ContentTemplate>
                           <Triggers>
                                <asp:PostBackTrigger ControlID="BT_sTOKUISAKI_Syousai" />
                           </Triggers>
                       </asp:UpdatePanel>
                    </div>
               </div>

               <div class="row mt-2 me-1" >
                    <div class="col-sm-2 mt-1">
                        <label class="col-form-label font">得意先担当者</label>  
                    </div>
                    <div class="col-sm mt-1">
                        <asp:UpdatePanel ID="updTokuisakiTantou" runat="server" UpdateMode="Conditional">
                               <ContentTemplate>
                                     <div id="divTokuisakiTanBtn" runat="server">
                                          <asp:Button runat="server" ID="BT_TokuisakiTantou"  Text="追加"　CssClass="JC10GrayButton" onmousedown="getTantouBoardScrollPosition();" OnClientClick="PopupOpenButton()" OnClick="BT_TokuisakiTantou_Click" Height="30px" />
                                          <asp:Label ID="LB_Jun" runat="server" Text='<%# Eval("sTOKUISAKI_TAN_Jun") %>' CssClass="DisplayNone" />
                                         <asp:Label ID="LB_Yakusyoku" runat="server" Text='<%# Eval("sTOKUISAKI_YAKUSYOKU") %>' CssClass="DisplayNone" />
                                         <asp:Label ID="LB_Keisyo" runat="server" Text='<%# Eval("sTOKUISAKI_KEISYO") %>' CssClass="DisplayNone" />
                                     </div>
                                        <div style="float:left;max-width: 300px; display: none;text-overflow:ellipsis;white-space: nowrap;overflow: hidden;margin-top:2px;margin-right:2px;" id="divTokuisakiTanLabel" runat="server">
                                              <asp:Label ID="LB_sTOKUISAKI_TAN" runat="server" cssClass="JClinkBtPopup JC27labellayout"></asp:Label>
                                    <br />  <asp:Label runat="server" ID="LB_sTOKUISAKI_TAN_JUN" ForeColor="#0080C0" Visible="false" Text="0"></asp:Label>
                                    
                                        </div>
                                        <div id="divTokuisakiTanSyosai" runat="server" style="display:none;">
                                   <asp:Button ID="BT_sTOKUISAKI_TAN_Cross" runat="server"  Text="✕" BackColor="White" style="vertical-align:middle;margin-left:10px; font-weight:bolder; height:30px;border:none;" OnClick="BT_sTOKUISAKI_TAN_Cross_Click1"/> 

                                                 <asp:Button runat="server" ID="BT_sTOKUISAKI_TAN_Syousai"  Text="詳細"　CssClass="JC10GrayButton" OnClick="BT_sTOKUISAKI_TAN_Syousai_Click" Height="30px"/>
                                    </div>
                                    </ContentTemplate>
                                    <Triggers>
                                         <asp:PostBackTrigger ControlID="BT_sTOKUISAKI_TAN_Syousai" />
                                     </Triggers>
                               </asp:UpdatePanel>
                    </div>
                    
                </div>
                <div class="row mt-2 me-1" >
                    <div class="col-sm-2">
                        <label class="col-form-label font">得意先部門</label>  
                    </div>
                    <div class="col-sm-9">
                     <asp:UpdatePanel ID="updtokuisakibumon" runat="server" UpdateMode="Conditional">
                             <ContentTemplate>
                            <asp:TextBox ID="TB_Tokuisakibumon" runat="server" name="username" CssClass="form-control font TextboxStyle" TextMode="Search" OnTextChanged="TB_Tokuisakibumon_TextChanged" AutoPostBack="true" Width="455px"></asp:TextBox>
                            </ContentTemplate>
                         </asp:UpdatePanel>
                    </div>
                </div>
                <div class="row mt-2 me-1" >
                     <div class="col-sm-2 mt-1">
                         <asp:Label ID="Label14" runat="server" Text="請求先" CssClass="JC27labellayout"></asp:Label>
                         <%--<label class="col-form-label font">請求先</label>  --%>
                         </div>
                    <div class="col-sm">
                      <asp:UpdatePanel ID="updSeikyusaki" runat="server" UpdateMode="Conditional">
                         <ContentTemplate>
                          <div id="divSeikyusakiBtn" runat="server">
                           <asp:Button runat="server" ID="BT_Seikyusaki"  Text="追加" CssClass="JC10GrayButton" onmousedown="getTantouBoardScrollPosition();" OnClientClick="PopupOpenButton()" OnClick="BT_Seikyuusaki_Click"/>
                            </div>
                             <div style="float:left;max-width: 300px; display: none;text-overflow:ellipsis;white-space: nowrap;overflow: hidden;margin-top:5px;margin-right:20px;" id="divSeikyusakiLabel" runat="server">
                               <asp:Label ID="LB_sSEIKYUSAKI" runat="server" Text="" cssClass="JClinkBtPopup JC27labellayout"></asp:Label>
                                 <asp:Label runat="server" ID="LB_cSEIKYUSAKI" Visible="false"></asp:Label>
                             </div>
                             <div id="divSeikyusakiSyosai" runat="server" style="display:none;">
                               <asp:Button runat="server" ID="BT_sSEIKYUSAKI_Syousai1"  Text="詳細"　CssClass="JC10GrayButton" OnClick="BT_sSEIKYUSAKI_Syousai_Click" Height="30px"/>
                              </div> 
                             </ContentTemplate>
                              <Triggers>
                                 <asp:PostBackTrigger ControlID="BT_sSEIKYUSAKI_Syousai1" />
                               </Triggers>
                           </asp:UpdatePanel>
                 </div>  
                </div>
               
                <div class="row mt-2 me-1" >
                    <div class="col-sm-2 mt-1">
                        <label class="col-form-label font">請求先担当者</label>  
            
                    </div>
                    <div class="col-sm mt-1">
                        <asp:UpdatePanel ID="updSeikyuTantou" runat="server" UpdateMode="Conditional">
                               <ContentTemplate>
                                     <div id="divSeikyusakiTanBtn" runat="server">
                                        <asp:Button runat="server" ID="BT_SeikyuTantou"  Text="追加"　CssClass="JC10GrayButton" onmousedown="getTantouBoardScrollPosition();" OnClientClick="PopupOpenButton()" OnClick="BT_SeikyuTantou_Click" Height="30px" />
                                      　<asp:Label ID="LB_Seikyu_JUN" runat="server" Text='<%# Eval("sTOKUISAKI_TAN_Jun") %>' CssClass="DisplayNone" />
                                         <asp:Label ID="LB_Seikyu_YAKUSYOKU" runat="server" Text='<%# Eval("sTOKUISAKI_YAKUSYOKU") %>' CssClass="DisplayNone" />
                                        <asp:Label ID="LB_Seikyu_KEISYO" runat="server" Text='<%# Eval("sTOKUISAKI_KEISYO") %>' CssClass="DisplayNone" />
                                        <asp:Label ID="LB_fseikyuukubun" runat="server" Text="0" CssClass="DisplayNone" />
                                     </div>
                                        <div style="float:left;max-width: 300px; display: none;text-overflow:ellipsis;white-space: nowrap;overflow: hidden;margin-top:2px;margin-right:2px;" id="divSeikyusakiTanLabel" runat="server">
                                              <asp:Label ID="LB_sSEIKYUSAKI_TAN" runat="server" cssClass="JClinkBtPopup JC27labellayout"></asp:Label>
                                    <br />  <asp:Label runat="server" ID="LB_sSEIKYUSAKI_TAN_JUN" ForeColor="#0080C0" Visible="false" Text="0"></asp:Label>
                                    
                                        </div>
                                        <div id="divSeikyusakiTanSyosai" runat="server" style="display:none;">
                                          <asp:Button ID="BT_SEIKYUSAKI_TAN_Cross" runat="server"  Text="✕" BackColor="White" style="vertical-align:middle;margin-left:10px; font-weight:bolder; height:30px;border:none;" OnClick="BT_SEIKYUSAKI_TAN_Cross_Click1"/> 
                                          <asp:Button runat="server" ID="BT_sSEIKYUSAKI_TAN_Syousai"  Text="詳細"　CssClass="JC10GrayButton" OnClick="BT_sSEIKYUSAKI_TAN_Syousai_Click" Height="30px"/>
                                    </div>
                                    </ContentTemplate>
                                    <Triggers>
                                         <asp:PostBackTrigger ControlID="BT_sSEIKYUSAKI_TAN_Syousai" />
                                     </Triggers>
                               </asp:UpdatePanel>
                    </div>
                   <%-- <div class="col-sm">
                         <asp:Button ID="Button7" runat="server" Text="詳細" class="btn JC10GrayButton font" Width="50"/>
                    </div>--%>
                    
                </div>
                <div class="row mt-2 me-1" >
                    <div class="col-sm-2">
                        <label class="col-form-label font">請求先部門                  </div>
                    <div class="col-sm-9">
                        <asp:UpdatePanel ID="updseikyusakibumon" runat="server" UpdateMode="Conditional">
                             <ContentTemplate>
                            <asp:TextBox ID="TB_Seikyuusakibumon" runat="server" name="username" CssClass="form-control font TextboxStyle"  TextMode="Search" OnTextChanged="TB_Seikyuusakibumon_TextChanged" AutoPostBack="true" Width="455px"></asp:TextBox>
                            </ContentTemplate>
                         </asp:UpdatePanel>
                    </div>
                </div>


                <div class="row me-1" style="margin-top:45px;">
                    <div class="col-sm-2">
                        <label class="col-form-label font">メモ</label>  
                    </div>
                    <div class="col-sm-9">
                        <asp:UpdatePanel ID="updtxtShanaimemo" runat="server" UpdateMode="Conditional">
                          <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="TB_Shanaimemo" EventName="TextChanged"/>
                           </Triggers>
                          <ContentTemplate>
                     <asp:TextBox ID="TB_Shanaimemo" runat="server" name="" CssClass="form-control font TextboxStyle JC27BorderColor"  TextMode="MultiLine" Height="100" OnTextChanged="TB_Shanaimemo_TextChanged" AutoPostBack="true" Width="455px"></asp:TextBox>
                       </ContentTemplate>
                            </asp:UpdatePanel>
                    </div>
                    
                </div>

                <div class="row mt-2 me-1">
                    <div class="col-sm-2">
                        <label class="col-form-label font">更新日</label>  
                    </div>
                    <div class="col-sm-4 mt-1">
                       <%--<asp:Label ID="LB_KoushinDate" runat="server" Text="" CssClass="col-form-label font JC27labellayout"></asp:Label>--%>
                        <asp:UpdatePanel ID="updKoushinDate" runat="server" UpdateMode="Conditional">                        <ContentTemplate>                       <asp:Label ID="LB_KoushinDate" runat="server" Text="" CssClass="col-form-label font JC27labellayout"></asp:Label>                        </ContentTemplate>                        </asp:UpdatePanel>

                    </div>
                    <div class="col-sm-2">
                        <label class="col-form-label font">最終更新者</label>
                    </div>
                    <div class="col-sm-3 mt-1">
                       <asp:UpdatePanel ID="updKoushinsha" runat="server" UpdateMode="Conditional">                        <ContentTemplate>                       <asp:Label ID="LB_cSaishukoushinsha" runat="server" CssClass="col-form-label font JC27labellayout" Visible="false"></asp:Label>                       <asp:Label ID="LB_sSaishukoushinsha" runat="server" CssClass="col-form-label font JC27labellayout"></asp:Label>                            </ContentTemplate>                        </asp:UpdatePanel>
                    </div>
                </div>

            </div>
        </div>
            </div>
            </div>
            <%--<div style="border-bottom:2px solid rgb(230, 230, 230);"></div>--%>
                    <div style="border-bottom:2px solid rgb(230, 230, 230);" class="JC27UriageDiv"></div>
             <div class="container-fluid bg-white JC27UriageDiv">
            <div class="row">
                <div class="col">
                     <asp:UpdatePanel ID="pudgoukei" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                    <div class="row  d-flex justify-content-end">
                        
                                <div class="col-sm-auto">
                                <label class="col-form-label font">合計売上</label>  
                                </div>
                                <div class="col-sm-auto mt-1">
                                    <asp:Label ID="CB_Uriage" runat="server" Text="" CssClass="col-form-label font JC27labellayout"></asp:Label>
                                    <asp:Label ID="CB_Henpin" runat="server" Text="" CssClass="DisplayNone" ></asp:Label>
                                    <asp:Label ID="CB_Tatekae" runat="server" Text="" CssClass="DisplayNone" ></asp:Label>
                                    <asp:Label ID="CB_Kazei" runat="server" Text="" CssClass="DisplayNone" ></asp:Label>
                                    <asp:Label ID="CB_Hikazei" runat="server" Text="" CssClass="DisplayNone" ></asp:Label>
                                </div>
                                <div class="col-sm-auto">
                                <label class="col-form-label font">合計値引</label>  
                                  </div>
                                <div class="col-sm-auto mt-1">
                                    <asp:Label ID="CB_Nebiki" runat="server" Text="" CssClass="col-form-label font JC27labellayout"></asp:Label>
                                </div>
                                <div class="col-sm-auto">
                                <label class="col-form-label font">小計</label>  
                                </div>
                                <div class="col-sm-auto mt-1">
                                    <asp:Label ID="CB_Shoukei" runat="server" Text="" CssClass="col-form-label font JC27labellayout"></asp:Label>                              
                                </div>
                                <div class="col-sm-auto">
                                <label class="col-form-label font">消費税</label>
                                 <br />
                                <label class="col-form-label font">課税対象額</label>
                                </div>
                                <div class="col-sm-auto mt-1">
                                <asp:Label ID="CB_Syouhizei" runat="server" Text="" CssClass="col-form-label font JC27labellayout"></asp:Label><br />
                                     <asp:UpdatePanel ID="updkazei" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <div style="margin-top:10px;">
                                    <asp:Label ID="LB_Kazei" runat="server" Text="" CssClass="col-form-label font JC27labellayout pb-0"></asp:Label>
                                        </div>
                                   </ContentTemplate>
                                   </asp:UpdatePanel>
                                </div>
                                <div class="col-sm-auto">
                                <label class="col-form-label font">合計金額</label>  
                                </div>
                                <div class="col-sm-auto mt-1">
                                <asp:Label ID="CB_KINGAKU" runat="server" Text="" CssClass="col-form-label font JC27labellayout"></asp:Label>                            
                                </div>                        
                   </div>
                     </ContentTemplate>
                         </asp:UpdatePanel>
                </div>
                 </div>
            <div class="row">
                
                <div class="col">
                    <div class="row">
                        <div class="col-sm">
                            <asp:UpdatePanel ID="updftansuushori" runat="server" UpdateMode="Conditional" class="DisplayNone">
                                <ContentTemplate>
                                <div class="col-sm">
                                    <asp:DropDownList ID="DDL_ftansuushori" runat="server">
                                        <asp:ListItem>切り捨て</asp:ListItem>
                                        <asp:ListItem>四捨五入</asp:ListItem>
                                        <asp:ListItem>切り上げ</asp:ListItem>
                                    </asp:DropDownList>
                                </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>

                            <asp:Button ID="BT_Ok" runat="server" Text="はい" class="BlueBackgroundButton DisplayNone" Width="100px" Height="36px" OnClick="BT_Ok_Click" />
                            <asp:Button ID="BT_No" runat="server" Text="いいえ" class="BlueBackgroundButton DisplayNone" Width="100px" Height="36px" OnClick="BT_No_Click" />
                            <asp:Button ID="BT_Cancel" runat="server" Text="キャンセル" class="JC09GrayButton DisplayNone" Width="100px" Height="36px" OnClick="BT_Cancel_Click" />
                        </div>
                   </div>
                </div>
               
                 </div>
           
                
                <div id="Div5" runat="server" class="d-flex justify-content-center mt-1" >
                    <div style="overflow-x: auto;width:auto;">
                        <asp:UpdatePanel ID="updUriageSyohinGrid" runat="server" UpdateMode="Conditional">
                           
                            <ContentTemplate>
                                <asp:GridView runat="server" ID="GV_UriageSyohin" BorderColor="Transparent" AutoGenerateColumns="false" EmptyDataRowStyle-CssClass="JC10NoDataMessageStyle"
                                    ShowHeader="true" ShowHeaderWhenEmpty="true" RowStyle-CssClass="GridRow" CellPadding="0" OnRowDataBound="GV_UriageSyohin_RowDataBound" CssClass="gvUriageSyohin" >
                                    <EmptyDataRowStyle CssClass="JC10NoDataMessageStyle" />
                                    <HeaderStyle Height="37px" BackColor="#F2F2F2" />
                                    <RowStyle CssClass="GridRow" Height="37px" />
                                    <SelectedRowStyle BackColor="#EBEBF5" />
                                    <Columns>
                                        
                                        <asp:TemplateField ItemStyle-CssClass="JC27UriageGridPludBtnCol AlignCenter" HeaderStyle-CssClass="JC27UriageGridPludBtnHeaderCol JC27UriageGridHeaderStyle">
                                            <ItemTemplate>
                                                <div class="grip" style="min-width:35px;text-align: center;padding-right: 2px;padding-left:2px;overflow: hidden;  display: -webkit-box;  -webkit-line-clamp: 1; -webkit-box-orient: vertical;word-break: break-all;">
                                                <asp:Button ID="BT_SyouhinAdd" runat="server" Text="＋" CssClass="JC09GridGrayBtn" TabIndex="-1" onmousedown="getTantouBoardScrollPosition();" Width="30px" Height="28px" OnClick="BT_SyouhinAdd_Click" />
                                            </div>
                                                    </ItemTemplate>
                                            <HeaderTemplate>
                                                <asp:Label ID="LB_ADD" runat="server" Text="" CssClass="d-inline-block"></asp:Label>
                                            </HeaderTemplate>
                                            <HeaderStyle CssClass="JC27UriageGridPludBtnHeaderCol JC27UriageGridHeaderStyle" />
                                            <ItemStyle CssClass="JC27UriageGridPludBtnCol AlignCenter" />
                                        </asp:TemplateField>
                                       
                                        <asp:TemplateField ItemStyle-CssClass="JC27UriageGridSyuryoCol AlignCenter" HeaderStyle-CssClass="JC27UriageGridSyuryoHeaderCol">
                                            <ItemTemplate>
                                                 <div class="grip" style="min-width:60px;text-align: center;padding-right: 4px;padding-left:1px;overflow: hidden;  display: -webkit-box;  -webkit-line-clamp: 1; -webkit-box-orient: vertical;word-break: break-all;">
                                                <%--<asp:Label ID="lblsKubun" runat="server" Text='<%# Eval("skubun") %>' CssClass="DisplayNone" />--%>
                                                <asp:DropDownList ID="DDL_Kubun" runat="server" Width="60px" AutoPostBack="True" Height="26px" CssClass="JC27GridTextBox" TabIndex="-1" OnSelectedIndexChanged="DDL_Kubun_SelectedIndexChanged"  >                                           
                                                </asp:DropDownList>
                                                    </div>
                                            </ItemTemplate>
                                            <HeaderTemplate>
                                                <div class="grip" style="padding-top: 0px;vertical-align: middle;">
                                                <asp:Label ID="LB_Kubun" runat="server" Text="区分" CssClass="d-inline-block" style="margin-left:5px;"></asp:Label>
                                            </div>
                                                    </HeaderTemplate>
                                            <HeaderStyle CssClass="JC27UriageGridSyuryoHeaderCol" />
                                            <ItemStyle CssClass="JC27UriageGridSyuryoCol AlignCenter" />
                                        </asp:TemplateField>

                                        <asp:TemplateField ItemStyle-CssClass="JC27UriageGridSyuryoCol AlignCenter" HeaderStyle-CssClass="JC27UriageGridSyuryoHeaderCol">
                                            <ItemTemplate>
                                                <%--<asp:Label ID="lblfKazei" runat="server" Text='<%# Eval("fKazei") %>' CssClass="DisplayNone" />--%>
                                                <asp:DropDownList ID="DDL_KazeiKubun" runat="server" Width="65px" AutoPostBack="True" Height="26px" CssClass="JC27GridTextBox" TabIndex="-1" OnSelectedIndexChanged="DDL_KazeiKubun_SelectedIndexChanged" >
                                                </asp:DropDownList>
                                            </ItemTemplate>
                                            <HeaderTemplate>
                                                <asp:Label ID="LB_KazeiKubun" runat="server" Text="課税区分" CssClass="d-inline-block" style="margin-left:3px;"></asp:Label>
                                            </HeaderTemplate>
                                            <HeaderStyle CssClass="JC27UriageGridSyuryoHeaderCol" />
                                            <ItemStyle CssClass="JC27UriageGridSyuryoCol AlignCenter" />
                                        </asp:TemplateField>

                                        <asp:TemplateField ItemStyle-CssClass="JC27UriageGridSyohinCodeCol AlignCenter" HeaderStyle-CssClass="JC27UriageGridSyohinCodeHeaderCol">
                                            <ItemTemplate>
                                                <div class="grip" style="min-width:91px;text-align: center;padding-right: 4px;padding-left:1px;overflow: hidden;  display: -webkit-box;  -webkit-line-clamp: 1; -webkit-box-orient: vertical;word-break: break-all;">
                                                <asp:UpdatePanel ID="updtxtcSYOUHIN" runat="server" UpdateMode="Conditional">
                                                    <Triggers>
                                                         <asp:AsyncPostBackTrigger ControlID="TB_cSYOHIN" EventName="TextChanged"/>
                                                     </Triggers>
                                                    <ContentTemplate>
                                                    <asp:TextBox ID="TB_cSYOHIN" runat="server" Text=' <%# Bind("cSYOUHIN","{0}") %>' Width="100%" Height="25px" MaxLength="10" CssClass="form-control TextboxStyle JC27GridTextBox" autocomplete="off" AutoPostBack="true" onkeypress="return isNumberKey(event)" OnTextChanged="TB_cSYOHIN_TextChanged" onfocus="ChangeTypeTel(this);" oninput="process(this)"></asp:TextBox>
                                                </ContentTemplate>
                                                </asp:UpdatePanel>
                                                        </div>
                                                </ItemTemplate>
                                            <HeaderTemplate>
                                                <asp:Label ID="LB_cSyohin" runat="server" Text="商品コード" CssClass="d-inline-block" style="margin-left:3px;"></asp:Label>
                                            </HeaderTemplate>
                                            <HeaderStyle CssClass="JC27UriageGridSyohinCodeHeaderCol" />
                                            <ItemStyle CssClass="JC27UriageGridSyohinCodeCol AlignCenter" />
                                        </asp:TemplateField>
                                       
                                        <asp:TemplateField ItemStyle-CssClass="JC27UriageGridSyohinNameCol AlignCenter" HeaderStyle-CssClass="JC27UriageGridSyohinNameHeaderCol">
                                            <ItemTemplate>
                                                <div class="grip" style=" min-width:246px;text-align: center;padding-right: 4px;padding-left:1px;overflow: hidden;  display: -webkit-box;  -webkit-line-clamp: 1; -webkit-box-orient: vertical;word-break: break-all;">
                                                <asp:UpdatePanel ID="updtxtsSYOUHIN" runat="server" UpdateMode="Conditional">
                                                    <Triggers>
                                                         <asp:AsyncPostBackTrigger ControlID="TB_sSYOHIN" EventName="TextChanged"/>
                                                     </Triggers>
                                                    <ContentTemplate>
                                                    <asp:TextBox ID="TB_sSYOHIN" runat="server" Text=' <%# Bind("sSYOUHIN_R","{0}") %>' Width="100%" Height="25px" MaxLength="50" CssClass="form-control TextboxStyle JC27GridTextBox" autocomplete="off" AutoPostBack="true" OnTextChanged="TB_sSYOHIN_TextChanged"  ></asp:TextBox>
                                           </ContentTemplate>
                                                </asp:UpdatePanel>
                                                        </div>
                                                    </ItemTemplate>
                                            <HeaderTemplate>
                                                <asp:Label ID="LB_sSyohin" runat="server" Text="商品名" CssClass="d-inline-block" style="margin-left:3px;"></asp:Label>
                                            </HeaderTemplate>
                                            <HeaderStyle CssClass="JC27UriageGridSyohinNameHeaderCol" />
                                            <ItemStyle CssClass="JC27UriageGridSyohinNameCol AlignCenter" />
                                        </asp:TemplateField>

                                        <asp:TemplateField ItemStyle-CssClass="JC27UriageGridSyuryoCol AlignRight" HeaderStyle-CssClass="JC27UriageGridSyuryoHeaderCol">                                            <ItemTemplate>                                                <div class="grip" style="min-width:66px;text-align: center;padding-right: 4px;padding-left:1px;overflow: hidden;  display: -webkit-box;  -webkit-line-clamp: 1; -webkit-box-orient: vertical;word-break: break-all;">                                                <asp:UpdatePanel ID="updtxtnSURYO" runat="server" UpdateMode="Conditional">
                                                    <Triggers>
                                                         <asp:AsyncPostBackTrigger ControlID="TB_nSURYO" EventName="TextChanged"/>
                                                     </Triggers>
                                                    <ContentTemplate>                                                    <asp:TextBox ID="TB_nSURYO" runat="server" Text=' <%# Bind("nSURYO","{0}") %>' Width="100%" Height="25px" MaxLength="10" CssClass="form-control TextboxStyle JC27GridTextBox" autocomplete="off" AutoPostBack="true" style="text-align:right;" onkeypress="return isNumberKey(event)" OnTextChanged="TB_nSURYO_TextChanged" onfocus="ChangeTypeTel(this);" oninput="process(this)"></asp:TextBox>                                           </ContentTemplate>
                                                </asp:UpdatePanel>                                               </div>                                            </ItemTemplate>                                            <HeaderTemplate>                                                    <asp:Label ID="LB_Syuryo" runat="server" Text="数量" CssClass="d-inline-block" style="text-align:right; width:100%;"></asp:Label>                                            </HeaderTemplate>                                            <HeaderStyle CssClass="JC27UriageGridSyuryoHeaderCol" />                                            <ItemStyle CssClass="JC27UriageGridSyuryoCol AlignRight" />                                        </asp:TemplateField>

                                        <%--<asp:TemplateField ItemStyle-CssClass="JC10MitumoriGridSyuryoCol AlignCenter" HeaderStyle-CssClass="JC10MitumoriGridSyuryoHeaderCol">
                                            <ItemTemplate>
                                                <asp:Label ID="LB_cTANI" runat="server" Text='<%# Eval("sTANI") %>' CssClass="DisplayNone" />
                                                <asp:DropDownList ID="DDL_cTANI" runat="server" Width="66px" AutoPostBack="True" Height="26px" CssClass="JC10GridTextBox" OnSelectedIndexChanged="DDL_cTANI_SelectedIndexChanged">
                                                </asp:DropDownList>
                                                <asp:TextBox ID="TB_sTANI" runat="server" Text='<%# Eval("sTANI") %>' CssClass="txtTani" autocomplete="off" AutoPostBack="true" OnTextChanged="TB_sTANI_TextChanged"></asp:TextBox> 
                                            </ItemTemplate>
                                            <HeaderTemplate>
                                                <asp:Label ID="LB_Tani" runat="server" Text="単位" CssClass="d-inline-block" style="margin-left:3px;"></asp:Label>
                                            </HeaderTemplate>
                                            <HeaderStyle CssClass="JC10MitumoriGridSyuryoHeaderCol" />
                                            <ItemStyle CssClass="JC10MitumoriGridSyuryoCol AlignCenter" />
                                        </asp:TemplateField>--%>

                                        <asp:TemplateField ItemStyle-CssClass="AlignCenter" HeaderStyle-CssClass="JC27UriageGridTaniHeaderCol">                                            <ItemTemplate>                                                <div class="select-editable">                                                <asp:Label ID="LB_cTANI" runat="server" Text='<%# Eval("sTANI") %>' CssClass="DisplayNone" />                                                <asp:DropDownList ID="DDL_cTANI" runat="server"  AutoPostBack="True"  CssClass="user_select" TabIndex="-1" OnSelectedIndexChanged="DDL_cTANI_SelectedIndexChanged">                                                    </asp:DropDownList>                                                <asp:TextBox ID="TB_sTANI" runat="server" Text='<%# Eval("sTANI") %>' CssClass="txtTani" MaxLength="4" autocomplete="off" AutoPostBack="true" OnTextChanged="TB_sTANI_TextChanged"></asp:TextBox>                                                 </div>                                               </ItemTemplate>                                            <HeaderTemplate>                                                <asp:Label ID="LB_Tani" runat="server" Text="単位" CssClass="d-inline-block" style="margin-left:3px;"></asp:Label>                                            </HeaderTemplate>                                            <HeaderStyle CssClass="JC27UriageGridTaniHeaderCol" />                                            <ItemStyle CssClass="AlignCenter" />                                        </asp:TemplateField>

                                        <asp:TemplateField ItemStyle-CssClass="JC27UriageGridKingakuCol AlignRight" HeaderStyle-CssClass="JC27UriageGridTankaHeaderCol">                                            <ItemTemplate>                                                <div class="grip" style="min-width:96px;text-align: center;padding-right: 4px;padding-left:1px;overflow: hidden;  display: -webkit-box;  -webkit-line-clamp: 1; -webkit-box-orient: vertical;word-break: break-all;">                                                <asp:UpdatePanel ID="updtxtnSIKIRITANKA" runat="server" UpdateMode="Conditional">
                                                    <Triggers>
                                                         <asp:AsyncPostBackTrigger ControlID="TB_nSIKIRITANKA" EventName="TextChanged"/>
                                                     </Triggers>
                                                    <ContentTemplate>                                                    <asp:TextBox ID="TB_nSIKIRITANKA" runat="server" Text=' <%# Bind("nSIKIRITANKA","{0}") %>' Width="100%" Height="25px" MaxLength="10" CssClass="form-control TextboxStyle JC27GridTextBox" autocomplete="off" AutoPostBack="true" style="text-align:right;" onkeypress="return isNumberKey(event)" OnTextChanged="TB_nSIKIRITANKA_TextChanged" onfocus="ChangeTypeTel(this);" oninput="process(this)"></asp:TextBox>                                            </ContentTemplate>
                                                </asp:UpdatePanel>                                                        </div>                                                    </ItemTemplate>                                            <HeaderTemplate>                                                <asp:Label ID="LB_Sikiritanka" runat="server" Text="標準単価" CssClass="d-inline-block" style="text-align:right;width:100%;"></asp:Label>                                            </HeaderTemplate>                                            <HeaderStyle CssClass="JC27UriageGridTankaHeaderCol" />                                            <ItemStyle CssClass="JC27UriageGridKingakuCol AlignRight" />                                        </asp:TemplateField>

                                        <asp:TemplateField ItemStyle-CssClass="JC27UriageGridKingakuCol AlignRight" HeaderStyle-CssClass="JC27UriageGridKingakuHeaderCol">                                            <ItemTemplate>                                                <div class="JC12LabelItem" style="width:95px;height:35px; ">                                                    <asp:Label runat="server" ID="LB_nSIKIRIKINGAKU" Text='<%#Eval("nSIKIRIKINGAKU")%>' ToolTip='<%#Eval("nSIKIRIKINGAKU")%>' style="cursor:default;user-select:none;padding-right:4px;" TabIndex="-1"></asp:Label>                                                </div>                                            </ItemTemplate>                                            <HeaderTemplate>                                                <asp:Label ID="LB_Sikirikingaku" runat="server" Text="標準合計" CssClass="d-inline-block" style="width:92px;text-align:right;"></asp:Label>                                            </HeaderTemplate>                                            <HeaderStyle CssClass="JC27UriageGridKingakuHeaderCol" />                                            <ItemStyle CssClass="JC27UriageGridKingakuCol AlignRight" />                                        </asp:TemplateField>

                                        <%--<asp:TemplateField ItemStyle-CssClass="AlignRight" HeaderStyle-CssClass="JC27UriageGridHeaderStyle">
                                            <ItemTemplate>
                                                <div class="grip" style="min-width:95px;text-align: right;padding-right: 4px;overflow: hidden;  display: -webkit-box;  -webkit-line-clamp: 1; -webkit-box-orient: vertical;word-break: break-all;">
                                                <asp:Label runat="server" ID="LB_nSIKIRIKINGAKU" Text='<%#Eval("nSIKIRIKINGAKU")%>' ToolTip='<%#Eval("nSIKIRIKINGAKU","{0:#,##0.##}")%>' style="cursor:default;user-select:none;" TabIndex="-1"></asp:Label>
                                                    </div>
                                            </ItemTemplate>
                                            <HeaderTemplate>
                                                 <div class="grip" style="min-width:95px;text-align: right;padding-right: 4px;overflow: hidden;  display: -webkit-box;  -webkit-line-clamp: 1; -webkit-box-orient: vertical;word-break: break-all;">
                                                <asp:Label ID="LB_Sikirikingaku" runat="server" Text="標準合計" CssClass="d-inline-block"></asp:Label>
                                                     </div>
                                            </HeaderTemplate>
                                            <HeaderStyle CssClass="JC27UriageGridHeaderStyle" BorderWidth="2px"/>
                                            <ItemStyle CssClass="AlignRight" />
                                        </asp:TemplateField>--%>


                                       <%-- <asp:TemplateField ItemStyle-CssClass="JC10MitumoriGridKingakuCol AlignRight" HeaderStyle-CssClass="JC10MitumoriGridKingakuHeaderCol JC10MitumoriGridHeaderStyle">
                                            <ItemTemplate>

                                                <asp:Label runat="server" ID="LB_nSIKIRIKINGAKU" Text='<%#Eval("nSIKIRIKINGAKU")%>' ToolTip='<%#Eval("nSIKIRIKINGAKU")%>' style="cursor:default;user-select:none;padding-right:4px;" CssClass="JC12LabelItem"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderTemplate>
                                                <asp:Label ID="LB_Sikirikingaku" runat="server" Text="標準合計" CssClass="d-inline-block" style="width:91px;padding-right:4px;text-align:right;"></asp:Label>
                                            </HeaderTemplate>
                                            <HeaderStyle CssClass="JC10MitumoriGridKingakuHeaderCol JC10MitumoriGridHeaderStyle" />
                                            <ItemStyle CssClass="JC10MitumoriGridKingakuCol AlignRight" />
                                        </asp:TemplateField>--%>


                                        <asp:TemplateField ItemStyle-CssClass="JC27UriageGridSyohinNameCol AlignCenter" HeaderStyle-CssClass="JC27UriageGridSyohinNameHeaderCol">
                                            <ItemTemplate>
                                                <div class="grip" style="min-width:226px;text-align: center;padding-right: 4px;padding-left:1px;overflow: hidden;  display: -webkit-box;  -webkit-line-clamp: 1; -webkit-box-orient: vertical;word-break: break-all;">
                                                <asp:TextBox ID="TB_sbikou" runat="server" Text=' <%# Bind("sbikou","{0}") %>' Width="100%" Height="25px" MaxLength="40" CssClass="form-control TextboxStyle JC27GridTextBox" autocomplete="off" AutoPostBack="true" OnTextChanged="TB_sbikou_TextChanged"></asp:TextBox>
                                            </div>
                                                    </ItemTemplate>
                                            <HeaderTemplate>
                                                <asp:Label ID="LB_sbikou" runat="server" Text="備考" CssClass="d-inline-block" style="margin-left:2px;"></asp:Label>
                                            </HeaderTemplate>
                                            <HeaderStyle CssClass="JC27UriageGridSyohinNameHeaderCol" />
                                            <ItemStyle CssClass="JC27UriageGridSyohinNameCol AlignCenter" />
                                        </asp:TemplateField>
                                        
                                         <asp:TemplateField ItemStyle-CssClass="JC27UriageGridPludBtnCol AlignCenter drag" HeaderStyle-CssClass="JC27UriageGridPludBtnHeaderCol JC27UriageGridHeaderStyle" Visible="false">
                                            <ItemTemplate>
                                                <div TabIndex="-1">
                                                    <span class="dragBtn">三</span>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderTemplate>
                                                <asp:Label ID="LB_Move" runat="server" Text="" CssClass="d-inline-block"></asp:Label>
                                            </HeaderTemplate>
                                            <HeaderStyle CssClass="JC27UriageGridPludBtnHeaderCol JC27UriageGridHeaderStyle" />
                                            <ItemStyle CssClass="JC27UriageGridPludBtnCol AlignCenter" />
                                        </asp:TemplateField>

                                        <asp:TemplateField ItemStyle-CssClass="JC27UriageGridPludBtnCol AlignCenter" HeaderStyle-CssClass="JC27UriageGridPludBtnHeaderCol JC27UriageGridHeaderStyle">                                            <ItemTemplate>                                                <div>                                                    <asp:Button ID="BT_SyohinCopy" runat="server" Text="コ" CssClass="JC09GridGrayBtn" TabIndex="-1" onmousedown="getTantouBoardScrollPosition();" Width="30px" Height="28px" OnClick="BT_SyohinCopy_Click"  />                                                </div>                                            </ItemTemplate>                                            <HeaderTemplate>                                                <asp:Label ID="LB_Copy" runat="server" Text="" CssClass="d-inline-block"></asp:Label>                                            </HeaderTemplate>                                            <HeaderStyle CssClass="JC27UriageGridPludBtnHeaderCol JC27UriageGridHeaderStyle" />                                            <ItemStyle CssClass="JC27UriageGridPludBtnCol AlignCenter" />                                        </asp:TemplateField>                                        <asp:TemplateField ItemStyle-CssClass="JC27UriageGridPludBtnCol AlignCenter" HeaderStyle-CssClass="JC27UriageGridPludBtnHeaderCol JC27UriageGridHeaderStyle">                                            <ItemTemplate>                                                <div>                                                    <asp:Button ID="BT_SyohinDelete" runat="server" Text="削" CssClass="JC09GridGrayBtn" TabIndex="-1" onmousedown="getTantouBoardScrollPosition();" Width="30px" Height="28px" OnClick="BT_SyohinDelete_Click"  />                                                </div>                                            </ItemTemplate>                                            <HeaderTemplate>                                                <asp:Label ID="LB_Delete" runat="server" Text="" CssClass="d-inline-block"></asp:Label>                                            </HeaderTemplate>                                            <HeaderStyle CssClass="JC27UriageGridPludBtnHeaderCol JC127UriageGridHeaderStyle" />                                            <ItemStyle CssClass="JC27UriageGridPludBtnCol AlignCenter" />                                        </asp:TemplateField>

                                    </Columns>
                                </asp:GridView>

                                 <asp:Button ID="BT_Add" runat="server" CssClass="JC10GrayButton DisplayNone mt-1 JC27AddBtn" Text="＋　商品追加" Width="125px" OnClick="BT_Add_Click" />

                                <asp:Button ID="BT_DeleteOk" runat="server" Text="はい" class="BlueBackgroundButton DisplayNone" Width="100px" Height="36px" OnClick="BT_DeleteOk_Click" />
                                <asp:Button ID="BT_DeleteCancel" runat="server" Text="キャンセル" class="JC09GrayButton DisplayNone" Width="100px" Height="36px" />
                                <asp:Button ID="BT_UriagezumiOk" runat="server" Text="OK" class="BlueBackgroundButton DisplayNone" Width="100px" Height="36px" OnClick="BT_UriagezumiOk_Click" />
                                <asp:Button ID="BT_UriagezumiCancel" runat="server" Text="キャンセル" class="JC09GrayButton DisplayNone" Width="100px" Height="36px" OnClick="BT_UriagezumiCancel_Click" />
                                
                            <asp:Button ID="btnYes1" runat="server" Text="はい" class="BlueBackgroundButton DisplayNone" Width="100px" Height="36px" OnClick="btnYes1_Click" />                                <asp:Button ID="btnNo1" runat="server" Text="いいえ" class="BlueBackgroundButton DisplayNone" Width="100px" Height="36px" OnClick="btnNo1_Click" />                            <asp:Button ID="btnCancel1" runat="server" Text="キャンセル" class="BlueBackgroundButton DisplayNone" Width="100px" Height="36px" />
                           <asp:Button ID="btnOK" runat="server" Text="OK" class="BlueBackgroundButton DisplayNone" Width="100px" Height="36px" />
                           <asp:Button ID="btnmojiOK" runat="server" Text="はい" class="BlueBackgroundButton DisplayNone" Width="100px" Height="36px"/>
                            </ContentTemplate>
                        </asp:UpdatePanel>

                    </div>
                    </div>
                 </div>
    </div>
        <asp:HiddenField ID="HF_fHidukeValueChange" runat="server" />
        <asp:HiddenField ID="HF_beforeSortIndex" runat="server" />
        <asp:HiddenField ID="HF_afterSortIndex" runat="server" />
        <asp:HiddenField ID="TB_sa_goukei" runat="server" />
    <asp:UpdatePanel ID="updHiddenField" runat="server" UpdateMode="Conditional">
                   <ContentTemplate>
                       <asp:HiddenField ID="HF_sMituJoutai" runat="server" />
                <asp:HiddenField ID="HF_cMituJoutai" runat="server" />
                <asp:HiddenField ID="HF_curiage" runat="server" />
                <asp:HiddenField ID="HF_dHenkou" runat="server" />
                   </ContentTemplate>
                
           </asp:UpdatePanel>
       
        <asp:Button ID="BT_Sort" runat="server" Text="Button" CssClass="DisplayNone" OnClick="BT_Sort_Click" />
        

        <div class="container-fluid bg-white JC27UriageSettei" id="Div_settei" runat="server">
            <div class="row ms-4">
                <div class="col-sm" style="margin-top:22px;">
                    <asp:Button ID="BT_Hozon" runat="server" Text="保存" class="BlueBackgroundButton JC10SaveBtn" OnClick="BT_Hozon_Click"/>
                </div>
            </div>

            <div class="row ms-4">
               <label class="col-form-label fw-bold mt-2 mb-3">納品書・請求書共通</label> 
                
                <div class="col-sm-1 ms-5">
                      <label class="col-form-label fw-bold font">日付</label> 

                </div>
                <div class="col-2">
                    <table>
                        <tr>
                                    
                            <td>
                                <asp:Button ID="BT_Hidzukeari" runat="server" Text="あり" onmousedown="getTantouBoardScrollPosition();" Width="50px" Height="35px" CssClass="JC10ZeikomiBtnActive" OnClick="BT_Hidzukeari_Click" />
                            </td>
                            <td>
                                <asp:Button ID="BT_Hidzukearinashi" runat="server" Text="なし" onmousedown="getTantouBoardScrollPosition();" Width="50px" Height="35px" CssClass="JC10ZeikomiBtn" OnClick="BT_Hidzukearinashi_Click" />
                            </td>
                        </tr>
                    </table>
                </div>
                <div class="col-sm-1">
                         <label class="col-form-label fw-bold font">書式</label> 
                </div>
                <div class="col-sm-2">
                    <table>
                        <tr>
                                    
                            <td>
                                <asp:Button ID="BT_kenseikyuusho" runat="server" Text="兼請求書" onmousedown="getTantouBoardScrollPosition();" Width="90px" Height="35px" CssClass="JC10ZeikomiBtnActive" OnClick="BT_kenseikyuusho_Click" />
                            </td>
                            <td>
                                <asp:Button ID="BT_Nouhinsho" runat="server" Text="納品書と請求書" onmousedown="getTantouBoardScrollPosition();" Width="110px" Height="35px" CssClass="JC10ZeikomiBtn" OnClick="BT_Nouhinsho_Click" />
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
            <div class="row ms-4 mt-3">
                <div class="col-1 ms-5">
                      <label class="col-form-label fw-bold font">ロゴ</label> 

                </div>
                <div class="col-sm-2">
                     <%--<asp:TextBox ID="TB_Logo" runat="server" name="" CssClass="form-control font TextboxStyle" MaxLength="20" ></asp:TextBox>--%>
                     <asp:DropDownList ID="DDL_Logo" runat="server" Width="200px" AutoPostBack="True" Height="35px">
                             </asp:DropDownList>
                </div>
               
            </div>
            <div class="row ms-4">
                <label class="col-form-label fw-bold mt-4 mb-3">納品書</label> 
                
                <div class="col-sm-1 ms-5">
                      <label class="col-form-label fw-bold font">金額</label> 
                    <%--  --%>
                </div>
                <div class="col-2">
                    <table>
                        <tr>
                                    
                            <td>
                                <asp:Button ID="BT_Kingakuari" runat="server" Text="あり" onmousedown="getTantouBoardScrollPosition();" Width="50px" Height="35px" CssClass="JC10ZeikomiBtnActive" OnClick="BT_Kingakuari_Click" />
                            </td>
                            <td>
                                <asp:Button ID="BT_Kingakunashi" runat="server" Text="なし" onmousedown="getTantouBoardScrollPosition();" Width="50px" Height="35px" CssClass="JC10ZeikomiBtn" OnClick="BT_Kingakunashi_Click"/>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
           <div class="row ms-4">
                <label class="col-form-label fw-bold mt-4 mb-3">請求書</label> 
                
                <div class="col-sm-1 ms-5 mb-4">
                      <label class="col-form-label fw-bold font">請求書備考</label> 

                </div>
                <div class="col-sm-2 mb-4">
                    <asp:DropDownList ID="DDL_bikou" runat="server" Width="200px" AutoPostBack="True" Height="35px">
                        </asp:DropDownList>

                </div>
            </div>

            <%--<div class="row">--%>
                <asp:Button ID="btnYes" runat="server" Text="はい" class="BlueBackgroundButton DisplayNone" Width="100px" Height="36px" OnClick="btnYes_Click" />
                <asp:Button ID="btnNo" runat="server" Text="いいえ" class="BlueBackgroundButton DisplayNone" Width="100px" Height="36px" OnClick="btnNo_Click" />
                <asp:Button ID="btnCancel" runat="server" Text="キャンセル" class="JC09GrayButton DisplayNone" Width="100px" Height="36px" />
            <%--</div>--%>
            
        </div>

    
    <asp:UpdatePanel runat="server" ID="updLabelSave" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <div class="success JCSuccess" id="divLabelSave" runat="server" style="position:fixed;display:none;height:30pt;width:100vw; left:0;background-color:#92d050;align-content:center;align-items:center; border-radius:7px;padding-left:10px;margin:2px;" >
                                     <asp:Label ID="LB_Save" Text="保存しました。" runat="server" ForeColor="White" Font-Size="13px"></asp:Label>
                                     <asp:Button id="BT_LBSaveCross" Text="✕" runat="server" style="background-color:white;border-style:none;right:10px;position:absolute;" OnClick="BT_LBSaveCross_Click" />
                                    </div>
                                  </ContentTemplate>
                            </asp:UpdatePanel>
    <%-- <!--Date-->
        <asp:UpdatePanel ID="updDatePopup" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:Button ID="btnDatePopup" runat="server" Text="Button" CssClass="DisplayNone" />
                <asp:ModalPopupExtender ID="mpeDatePopup" runat="server" TargetControlID="btnDatePopup"
                    PopupControlID="pnlDatePopupScroll" BackgroundCssClass="PopupModalBackground" BehaviorID="pnlDatePopupScroll"
                    RepositionMode="None">
                </asp:ModalPopupExtender>
                <asp:Panel ID="pnlDatePopupScroll" runat="server" Style="display: none;" CssClass="PopupScrollDiv">
                    <asp:Panel ID="pnlDatePopup" runat="server">
                        <iframe id="ifDatePopup" runat="server" class="NyuryokuIframe RadiusIframe" scrolling="no" style="max-width:260px;"></iframe>
                        <asp:Button ID="btnCalendarClose" runat="server" Text="Button" CssClass="DisplayNone" OnClick="btnCalendarClose_Click" />
                        <asp:Button ID="btnCalendarSettei" runat="server" Text="Button" CssClass="DisplayNone" OnClick="btnCalendarSettei_Click" />
                    </asp:Panel>
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>--%>

     <!--ポップアップ画面-->            <asp:UpdatePanel ID="upddatePopup" runat="server" UpdateMode="Conditional">                <ContentTemplate>                    <asp:Button ID="btndatePopup" runat="server" Text="Button" CssClass="DisplayNone" />                    <asp:ModalPopupExtender ID="mpedatePopup" runat="server" TargetControlID="btndatePopup"                        PopupControlID="pnldatePopupScroll" BackgroundCssClass="PopupModalBackground" BehaviorID="pnldatePopupScroll"                        RepositionMode="RepositionOnWindowResize">                    </asp:ModalPopupExtender>                    <asp:Panel ID="pnldatePopupScroll" runat="server" Style="display: none;" CssClass="PopupScrollDiv">                        <asp:Panel ID="pnldatePopup" runat="server">                            <iframe id="ifdatePopup" runat="server" class="NyuryokuIframe RadiusIframe" scrolling="no"  style="max-width: 260px; min-width: 260px; max-height: 365px; min-height: 365px;"></iframe>                            <asp:Button ID="btnCalendarClose" runat="server" Text="Button" CssClass="DisplayNone" OnClick="btnCalendarClose_Click" onClientClick="MasterOverflow()" />                            <asp:Button ID="btnCalendarSettei" runat="server" Text="Button" CssClass="DisplayNone" OnClick="btnCalendarSettei_Click" onClientClick="MasterOverflow()" />                        </asp:Panel>                    </asp:Panel>                </ContentTemplate>            </asp:UpdatePanel>

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
                        <asp:Button ID="btnClose" runat="server" Text="Button" Style="display: none" OnClick="btnClose_Click" onClientClick="MasterOverflow()" />
                        <asp:Button ID="btnKyotanSelect" runat="server" Text="Button" Style="display: none" OnClick="btnKyotenSelect_Click" onClientClick="MasterOverflow()" />
                         <asp:Button ID="btnshiarailistSelect" runat="server" Text="Button" Style="display: none" OnClick="btnshiarailistSelect_Click" onClientClick="MasterOverflow()" />
                        <asp:Button ID="btnYukoKigenListSelect" runat="server" Text="Button" Style="display: none" OnClick="btnYukoKigenListSelect_Click" onClientClick="MasterOverflow()" />
                        <asp:Button ID="btnTokuisakiSelect" runat="server" Text="Button" Style="display: none" OnClick="btnTokuisakiSelect_Click" onClientClick="MasterOverflow()" />
                        <asp:Button ID="btnSeikyusakiSelect" runat="server" Text="Button" Style="display: none" OnClick="btnSeikyusakiSelect_Click" onClientClick="MasterOverflow()" />
                        <asp:Button ID="btnTokuisakiTantouSelect" runat="server" Text="Button" Style="display: none" OnClick="btnTokuisakiTantouSelect_Click" onClientClick="MasterOverflow()" />
                        <asp:Button ID="btnSeikyusakiTantouSelect" runat="server" Text="Button" Style="display: none" OnClick="btnSeikyusakiTantouSelect_Click" onClientClick="MasterOverflow()" />
                        <asp:Button ID="btnJishaTantouSelect" runat="server" Text="Button" Style="display: none" OnClick="btnJishaTantouSelect_Click" onClientClick="MasterOverflow()" />

                    </asp:Panel>
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>

     <%-- 20220120 MiMi Added--%>
     <asp:UpdatePanel ID="updShinkiPopup" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:Button ID="btnShinkiPopup" runat="server" Text="Button" Style="display: none" />
                    <asp:ModalPopupExtender ID="mpeShinkiPopup" runat="server" TargetControlID="btnShinkiPopup"
                        PopupControlID="pnlShinkiPopupScroll" BackgroundCssClass="PopupModalBackground" BehaviorID="pnlShinkiPopupScroll"
                        RepositionMode="RepositionOnWindowResize">
                    </asp:ModalPopupExtender>
                    <asp:Panel ID="pnlShinkiPopupScroll" runat="server" Style="display: none;height:100%;overflow:hidden;" CssClass="PopupScrollDiv">
                        <asp:Panel ID="pnlShinkiPopup" runat="server">
                            <iframe id="ifShinkiPopup" runat="server" scrolling="yes"  style="height:100vh;width:100vw;"></iframe>
                            <%--<asp:Button ID="btn_CloseMitumoriSearch" runat="server" Text="Button" CssClass="DisplayNone" OnClick="btn_CloseMitumoriSearch_Click" />--%>
                            <asp:Button ID="btn_CloseTokuisakiSentaku" runat="server" Text="Button" CssClass="DisplayNone" OnClick="btn_CloseTokuisakiSentaku_Click" onClientClick="MasterOverflow()" />
                            </asp:Panel>
                    </asp:Panel>
                </ContentTemplate>
            </asp:UpdatePanel>
     <%-- 20220120 MiMi Added--%>

     

</body>
    <script type="text/javascript">
        function process(input) {
            let value = input.value;
            let numbers = value.replace(/[^0-9]/g, "");
            input.value = numbers;
        }
        </script>
 <%--<script src="../Scripts/colResizable-1.6.min.js"></script><script src="../Scripts/cookie.js"></script>
     <script type="text/javascript">
         $(function () {
             if ($.cookie('colWidthmUriage') != null) {
                 var columns = $.cookie('colWidthmUriage').split(',');
                 var i = 0;
                 $('.GridViewStyle th').each(function () {
                     $(this).width(columns[i++]);
                 });
             }
             else {
                 var columns = [30, 95, 95, 160, 160, 160, 160, 95, 110, 72, 165];
                 var i = 0;
                 $('.GridViewStyle th').each(function () {
                     $(this).width(columns[i++]);
                 });
             }

             if ($.cookie('colWidthmSyohin') != null) {
                 var columns = $.cookie('colWidthmSyohin').split(',');
                 var i = 0;
                 $('.GridViewStyleSyohin th').each(function () {
                     $(this).width(columns[i++]);
                 });
             }
             else {
                 var columns = [20, 30, 30, 30, 95, 30, 300, 70, 58, 115, 115, 115, 115, 55, 115, 115, 115, 30, 25];
                 var i = 0;
                 $('.GridViewStyleSyohin th').each(function () {
                     $(this).width(columns[i++]);
                 });
             }


             $(".GridViewStyle").colResizable({
                 liveDrag: true,
                 resizeMode: 'overflow',
                 postbackSafe: true,
                 partialRefresh: true,
                 flush: true,
                 disabledColumns: ['10'],
                 gripInnerHtml: "<div class='grip'></div>",
                 draggingClass: "dragging",
                 onResize: onSampleResized
             });

             $(".GridViewStyleSyohin").colResizable({
                 liveDrag: true,
                 resizeMode: 'overflow',
                 postbackSafe: true,
                 partialRefresh: true,
                 flush: true,
                 disabledColumns: ['10'],
                 gripInnerHtml: "<div class='grip'></div>",
                 draggingClass: "dragging",
                 onResize: SyohinResized
             });

         });


         var onSampleResized = function (e) {
             var columns = $(e.currentTarget).find("th");
             var msg = "";
             var date = new Date();
             date.setTime(date.getTime() + (60 * 20000));
             columns.each(function () {
                 msg += $(this).width() + ",";
             })
             $.cookie("colWidthmUriage", msg, { expires: date }); // expires after 20 minutes
         };

         var SyohinResized = function (e) {
             var columns = $(e.currentTarget).find("th");
             var msg = "";
             var date = new Date();
             date.setTime(date.getTime() + (60 * 20000));
             columns.each(function () {
                 msg += $(this).width() + ",";
             })
             $.cookie("colWidthmSyohin", msg, { expires: date }); // expires after 20 minutes
         };


         var prm = Sys.WebForms.PageRequestManager.getInstance();
         if (prm != null) {
             prm.add_endRequest(function (sender, e) {
                 if (sender._postBackSettings.panelsToUpdate != null) {
                     if ($.cookie('colWidthmUriage') != null) {
                         var columns = $.cookie('colWidthmUriage').split(',');
                         var i = 0;
                         $('.GridViewStyle th').each(function () {
                             $(this).width(columns[i++]);
                         });
                     }
                     else {
                         var columns = [30, 95, 95, 160, 160, 160, 160, 95, 110, 72, 165];
                         var i = 0;
                         $('.GridViewStyle th').each(function () {
                             $(this).width(columns[i++]);
                         });
                     }

                     if ($.cookie('colWidthmSyohin') != null) {
                         var columns = $.cookie('colWidthmSyohin').split(',');
                         var i = 0;
                         $('.GridViewStyleSyohin th').each(function () {
                             $(this).width(columns[i++]);
                         });
                     } else {
                         var columns = [20, 30, 30, 30, 95, 30, 300, 70, 58, 115, 115, 115, 115, 55, 115, 115, 115, 30, 25];
                         var i = 0;
                         $('.GridViewStyleSyohin th').each(function () {
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

                     $(".GridViewStyleSyohin").colResizable({
                         liveDrag: true,
                         resizeMode: 'overflow',
                         postbackSafe: true,
                         partialRefresh: true,
                         flush: true,
                         disabledColumns: ['10'],
                         gripInnerHtml: "<div class='grip'></div>",
                         draggingClass: "dragging",
                         onResize: SyohinResized
                     });
                 }
             });
         };
</script>--%>
</html>
    </label>
 </asp:Content>
