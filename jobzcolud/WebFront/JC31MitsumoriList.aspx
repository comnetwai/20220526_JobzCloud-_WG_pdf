<%@ Page Language="C#" MasterPageFile="~/WebFront/JC99NavBar.Master" AutoEventWireup="true" CodeBehind="JC31MitsumoriList.aspx.cs" Inherits="jobzcolud.WebFront.JC31MitsumoriList" ValidateRequest="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <!DOCTYPE html>
    <html>
    <head>
        <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
        <title>見積リスト</title>
        <asp:PlaceHolder runat="server">
            <%: Scripts.Render("~/bundles/modernizr") %>
            <%: Styles.Render("~/style/StyleBundle1") %>
            <%: Scripts.Render("~/scripts/ScriptBundle1") %>
            <%: Styles.Render("~/style/UCStyleBundle") %>

        </asp:PlaceHolder>

        <webopt:BundleReference runat="server" Path="~/Content1/bootstrap" />
        <webopt:BundleReference runat="server" Path="~/Content1/css" />
        <style type="text/css">
            .auto-style1 {
                height: 26px;
            }
            .JC31MitumoriCodeCol {
                width: 100px !important;
                min-width: 100px !important;
                max-width: 100px !important;
            }
            .JC31LblHyojikensuu {
                margin-left: 25px;
            }

            .dropdown-menu {
                margin: 0;
                position: absolute;
                top: 100%;
                left: 0;
                z-index: 1000;
                display: none;
                float: left;
                min-width: 10rem;
                padding: 0.5rem 0;
                font-size: 1rem;
                color: #212529;
                text-align: left;
                list-style: none;
                background-color: #fff;
                background-clip: padding-box;
                border: 1px solid rgba(0,0,0,.15);
                border-radius: 0.25rem;
            }

            .JC31kingaku{
               padding-right:4px;
           }

            /*th:not(:first-child) {
                border-left: 2px solid white;
            }*/
        </style>
        <script src="../Scripts/colResizable-1.6.min.js"></script>
        <script src="../Scripts/cookie.js"></script>
        <%--<script type="text/javascript">
            $(function () {
                if ($.cookie('colWidthMitsumoriList') != null) {
                    var columns = $.cookie('colWidthMitsumoriList').split(',');
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


            var onSampleResized = function (e) {
                var columns = $(e.currentTarget).find("th");
                var msg = "";
                var date = new Date();
                date.setTime(date.getTime() + (60 * 20000));
                columns.each(function () {
                    msg += $(this).width() + ",";
                })
                $.cookie("colWidthMitsumoriList", msg, { expires: date }); // expires after 20 minutes
            };

            var prm = Sys.WebForms.PageRequestManager.getInstance();
            if (prm != null) {
                prm.add_endRequest(function (sender, e) {
                    if (sender._postBackSettings.panelsToUpdate != null) {
                        if ($.cookie('colWidthMitsumoriList') != null) {
                            var columns = $.cookie('colWidthMitsumoriList').split(',');
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
                    }
                });
            };
        </script>--%>
    </head>
    <body>
       <!--20220321 start-->
        <asp:UpdatePanel ID="updbody" runat="server" UpdateMode="Conditional" RenderMode="Inline">
            <ContentTemplate>
          <!--20220321 end-->
        <div class="JC10MitumoriTourokuDiv" id="div3" runat="server" style="margin-top: 28px; min-height: 400px; margin-bottom: 20px;">
            <div class="container-fluid bg-white " runat="server">
                <div class="row mt-2" runat="server">

                    <div class="JC31MitumoriList">
                        <asp:Button ID="BT_mitsumorisakusei" runat="server" Text="見積をダイレクト作成" CssClass="BlueBackgroundButton mt-3 JC07btnpadding" OnClick="BT_mitsumorisakusei_Click" Style="margin-left: 50px;" />
                    </div>
                    <div class="JC31MitumoriKensakuDiv mt-2">

                        <table class="table_less" style="margin-left: 60px; margin-top: 5px;">
                            <tr>
                                <td class="auto-style1">
                                    <asp:Label ID="Syohin" runat="server" Text="商品名" Font-Size="13px"></asp:Label>
                                </td>
                                <td class="auto-style1">
                                    <asp:Label ID="lblTokuisaki" runat="server" Text="得意先" Font-Size="13px"></asp:Label>
                                </td>
                                <td class="auto-style1">
                                    <asp:Label ID="lblsMitumori" runat="server" Text="見積名" Font-Size="13px"></asp:Label>
                                </td>
                                <td class="auto-style1"></td>
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
                                    <asp:UpdatePanel ID="updSyosai" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:Button runat="server" ID="BT_MitsumoriSyosai" Text="詳細条件" CssClass="JC31MitumoriSyosaiButton" Width="120px" OnClick="BT_MitsumoriSyosai_Click" />
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                        </table>
                        <asp:UpdatePanel ID="updbound" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <asp:Panel ID="Panel1" runat="server" Style="background-color: rgb(250, 250, 250); width: 780px; margin-left: 60px; padding-top: 4px; height: 140px; margin-top: 10px;" Visible="false">
                                    <table id="table1" runat="server" style="margin-left: 10px; margin-top: 5px; height: 60px;">
                                        <tr>
                                            <td class="auto-style1">
                                                <asp:Label ID="lblcMitumori" runat="server" Text="見積コード" Font-Size="13px"></asp:Label>
                                            </td>
                                            <td class="auto-style1">
                                                <asp:Label ID="lblMitumoriJoutai" runat="server" Text="見積状態" Font-Size="13px"></asp:Label>
                                            </td>

                                            <td class="auto-style1">
                                                <asp:Label ID="lblTokuisakiTantousha" runat="server" Text="得意先担当者" Font-Size="13px"></asp:Label>
                                            </td>
                                            <td class="auto-style1">
                                                <asp:Label ID="lblJishaTantousha" runat="server" Text="自社担当者" Font-Size="13px"></asp:Label>
                                            </td>
                                            <td class="auto-style1">
                                                <asp:Label ID="lblmemo" runat="server" Text="メモ" Font-Size="13px"></asp:Label> <%--//20220502 Added by phoo--%>
                                            </td>
                                        </tr>
                                        <tr style="margin-bottom: 5px;">
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
                                                        <asp:Button ID="btn_Jyotai" Text="選択なし" runat="server" CssClass="JC31TantouBtn" Width="120px" Height="29px" Style="margin-left: 0px;" OnClick="btn_Jyotai_Click" OnClientClick="PopupOpenButton()" />
                                                        <asp:DropDownList ID="DDL_Jyotai" runat="server" Visible="false"></asp:DropDownList>
                                                        <asp:TextBox ID="txtMitumoriJoutai" runat="server" AutoPostBack="false" MaxLength="100" CssClass="form-control TextboxStyle JC12MitumoriTantousha" Visible="false"
                                                            onkeyup="DeSelectText(this);" onfocus="this.setSelectionRange(this.value.length, this.value.length);" onchange="texboxchange()" TextMode="Search"></asp:TextBox>
                                                    </ContentTemplate>

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
                                                        <asp:Button ID="btn_Tantousya" Text="選択なし" runat="server" CssClass="JC31TantouBtn" Width="120px" Height="29px" Style="margin-left: 0px;" OnClick="btn_Tantousya_Click" OnClientClick="PopupOpenButton()" />
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
                                                            onkeyup="DeSelectText(this);" onfocus="this.setSelectionRange(this.value.length, this.value.length);" onchange="texboxchange()" TextMode="Search"></asp:TextBox>
                                                    </ContentTemplate>

                                                </asp:UpdatePanel>
                                            </td>
                                        </tr>
                                    </table>
                                    <table id="table2" runat="server" style="margin-top: 5px; margin-left: 10px;">
                                        <tr>
                                            <td colspan="3">
                                                <asp:Label ID="lbldMitumori1" runat="server" Text="見積日" Font-Size="13px"></asp:Label>
                                            </td>
                                            <td colspan="3">
                                                <asp:Label ID="lbldJuchuu" runat="server" Text="受注日" Font-Size="13px" Style="margin-left: 10px;"></asp:Label>
                                            </td>
                                            <td colspan="3">
                                                <asp:Label ID="lbldUriageYotei" runat="server" Text="売上予定日" Font-Size="13px" Style="margin-left: 10px;"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr style="margin-bottom: 5px;">
                                            <td>
                                                <asp:UpdatePanel ID="updMitumoriStartDate" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <asp:Button ID="btnMitumoriStart" runat="server" Text="日付を設定" CssClass="JC10GrayButton" onmousedown="getTantouBoardScrollPosition();" Height="30px" Width="90px" OnClientClick="PopupOpenButton()"  OnClick="btnMitumoriStart_Click" />
                                                        <div id="divMitumoriSDate" class="DisplayNone" runat="server" style="width: 110px;">
                                                            <asp:Label ID="lbldMitumoriS" runat="server" Text="" CssClass="GrayLabel"></asp:Label>
                                                            <asp:Label ID="lblMitumoriDateYearS" runat="server" CssClass="DisplayNone"></asp:Label>
                                                            <asp:Button ID="btndMitumoriSCross" CssClass="CrossBtnGray" runat="server" Text="✕" OnClick="btndMitumoriSCross_Click" />

                                                        </div>
                                                    </ContentTemplate>

                                                </asp:UpdatePanel>
                                            </td>
                                            <td>～
                                            </td>
                                            <td>
                                                <asp:UpdatePanel ID="UpdMitumoriEndDate" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <asp:Button ID="btnMitumoriEnd" runat="server" Text="日付を設定" CssClass="JC10GrayButton" onmousedown="getTantouBoardScrollPosition();" Height="30px" Width="90px" OnClientClick="PopupOpenButton()" OnClick="btnMitumoriEnd_Click" />
                                                        <div id="divMitumoriEDate" class="DisplayNone" runat="server" style="width: 110px;">
                                                            <asp:Label ID="lbldMitumoriE" runat="server" Text="" CssClass="GrayLabel"></asp:Label>
                                                            <asp:Label ID="lblMitumoriDateYearE" runat="server" CssClass="DisplayNone"></asp:Label>
                                                            <asp:Button ID="btndMitumoriECross" CssClass="CrossBtnGray" runat="server" Text="✕" OnClick="btndMitumoriECross_Click" />

                                                        </div>
                                                    </ContentTemplate>

                                                </asp:UpdatePanel>
                                            </td>
                                            <td>
                                                <asp:UpdatePanel ID="updJuchuuStartDate" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <asp:Button ID="btnJuchuuStart" runat="server" Text="日付を設定" CssClass="JC10GrayButton" onmousedown="getTantouBoardScrollPosition();" Height="30px" Width="90px" Style="margin-left: 10px;" OnClientClick="PopupOpenButton()" OnClick="btnJuchuuStart_Click" />
                                                        <div id="divJuchuuStart" class="DisplayNone" runat="server" style="width: 110px; margin-left: 10px;">
                                                            <asp:Label ID="lblJuchuuStart" runat="server" Text="" CssClass="GrayLabel"></asp:Label>
                                                            <asp:Label ID="lblJuchuuStartYear" runat="server" CssClass="DisplayNone"></asp:Label>
                                                            <asp:Button ID="btnJuchuuSCross" CssClass="CrossBtnGray" runat="server" Text="✕" OnClick="btnJuchuuSCross_Click" />

                                                        </div>
                                                    </ContentTemplate>

                                                </asp:UpdatePanel>
                                            </td>
                                            <td>～
                                            </td>
                                            <td>
                                                <asp:UpdatePanel ID="updJuchuuEndDate" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <asp:Button ID="btnJuchuuEnd" runat="server" Text="日付を設定" CssClass="JC10GrayButton" onmousedown="getTantouBoardScrollPosition();" Height="30px" Width="90px" OnClientClick="PopupOpenButton()" OnClick="btnJuchuuEnd_Click" />
                                                        <div id="divJuchuuEnd" class="DisplayNone" runat="server" style="width: 110px;">
                                                            <asp:Label ID="lblJuchuuEnd" runat="server" Text="" CssClass="GrayLabel"></asp:Label>
                                                            <asp:Label ID="JuchuuEndYear" runat="server" CssClass="DisplayNone"></asp:Label>
                                                            <asp:Button ID="btnJuchuuECross" CssClass="CrossBtnGray" runat="server" Text="✕" OnClick="btnJuchuuECross_Click" />

                                                        </div>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </td>
                                            <td>
                                                <asp:UpdatePanel ID="updUriageYoteiStartDate" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <asp:Button ID="btnUriageYoteiStartDate" runat="server" Text="日付を設定" CssClass="JC10GrayButton" onmousedown="getTantouBoardScrollPosition();" Height="30px" Width="90px" Style="margin-left: 10px;" OnClientClick="PopupOpenButton()" OnClick="btnUriageYoteiStartDate_Click" />
                                                        <div id="divUriageYoteiStartDate" class="DisplayNone" runat="server" style="width: 110px; margin-left: 10px;">
                                                            <asp:Label ID="lblUriageYoteiStartDate" runat="server" Text="" CssClass="GrayLabel"></asp:Label>
                                                            <asp:Label ID="lblUriageYoteiStartYear" runat="server" CssClass="DisplayNone"></asp:Label>
                                                            <asp:Button ID="btnUriageYoteiSCross" CssClass="CrossBtnGray" runat="server" Text="✕" OnClick="btnUriageYoteiSCross_Click" />

                                                        </div>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </td>
                                            <td>～
                                            </td>
                                            <td>
                                                <asp:UpdatePanel ID="updUriageYoteiEndDate" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <asp:Button ID="btnUriageYoteiEndDate" runat="server" Text="日付を設定" CssClass="JC10GrayButton" onmousedown="getTantouBoardScrollPosition();" Height="30px" Width="90px" OnClientClick="PopupOpenButton()" OnClick="btnUriageYoteiEndDate_Click" />
                                                        <div id="divUriageYoteiEndDate" class="DisplayNone" runat="server" style="width: 110px;">
                                                            <asp:Label ID="lblUriageYoteiEndDate" runat="server" Text="" CssClass="GrayLabel"></asp:Label>
                                                            <asp:Label ID="lblUriageYoteiEndYear" runat="server" CssClass="DisplayNone"></asp:Label>
                                                            <asp:Button ID="btnUriageYoteiECross" CssClass="CrossBtnGray" runat="server" Text="✕" OnClick="btnUriageYoteiECross_Click" />

                                                        </div>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <table style="margin-top: 15px; margin-left: 60px;" hidden="hidden">
                            <tr>
                                <td colspan="3">
                                    <asp:Label ID="Label1" runat="server" Text="商品名" Font-Size="13px"></asp:Label>
                                </td>
                                <td colspan="3">
                                    <%--<asp:Label ID="Label2" runat="server" Text="社内メモ" Font-Size="13px"></asp:Label>--%>
                                </td>
                            </tr>
                            <tr>
                                <td></td>
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
                                <%--<td>
                            <asp:UpdatePanel ID="updSyohin4" runat="server" UpdateMode="Conditional">
                                  <ContentTemplate>
                                       <asp:TextBox ID="txtmemo" runat="server" AutoPostBack="true" MaxLength="1000" CssClass="form-control TextboxStyle" Height="28"
                                                           onkeyup="DeSelectText(this);" onfocus="this.setSelectionRange(this.value.length, this.value.length);" OnTextChanged="TB_MitsuListCode_TextChanged" TextMode="Search" ></asp:TextBox>
                                  </ContentTemplate>
                                  <Triggers>
                                       <asp:AsyncPostBackTrigger ControlID="txtmemo" EventName="TextChanged" />
                                  </Triggers>
                        　　</asp:UpdatePanel>
                       </td>--%>
                            </tr>
                        </table>
                        <div class="JC31ButtonDiv">
                            <asp:Button ID="btnSearch" runat="server" CssClass=" BlueBackgroundButton JC12SearchBtn" Text="絞り込み"
                                UseSubmitBehavior="false" Width="110px" OnClick="btnSearch_Click" />
                            <asp:UpdatePanel ID="updclear" runat="server" UpdateMode="Conditional">
                                <ContentTemplate>
                                    <asp:Button ID="btnClear" runat="server" CssClass="JC31ClearBtn" Text="クリア"
                                UseSubmitBehavior="false" Width="110px" OnClick="btnClear_Click" />
                                </ContentTemplate>
                            </asp:UpdatePanel>
                           <%-- <asp:Button ID="btnClear" runat="server" AutoPostBack="True" CssClass="JC31ClearBtn" Text="クリア"
                                UseSubmitBehavior="false" Width="110px" OnClick="btnClear_Click" OnClientClick="return btnClear_Click()" />--%>
                        </div>
                    </div>

                </div>
                <div class="row" runat="server" style="padding-top: 20px;">
                    <div class="col-sm-8"></div>
                    <div class="container-fluid bg-white">
                        <asp:UpdatePanel ID="updJoken" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <div style="padding-left: 20px; padding-right: 20px; max-width: 1330px !important;">
                                    <asp:Label ID="LB_Jouken" runat="server" Text="検索条件" Font-Size="15px" Font-Bold="true"></asp:Label>
                                    <asp:Table ID="TB_SentakuJouken" runat="server" Style="margin-top: 2px;">
                                        <asp:TableRow>
                                            <asp:TableCell ID="TC_Jouken" runat="server">
                                
                                            </asp:TableCell>
                                        </asp:TableRow>
                                    </asp:Table>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <div class="container-fluid bg-white JC31OptionDiv" align="right">
                        <table style="margin-top: 10px; margin-bottom: 18px;">
                            <tr>
                                <td>
                                    <asp:UpdatePanel ID="updHyojikensuu1" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:Label ID="lblHyojikensuu" runat="server" Text="1-20/" Font-Size="13px"></asp:Label>
                                            <asp:Label ID="LB_Total" runat="server" Text="" Font-Size="13px"></asp:Label>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td>
                                    <asp:Label ID="lblHyoujikensuuLabel" runat="server" CssClass="JC31LblHyojikensuu" Text="表示件数" Font-Size="13px"></asp:Label>
                                </td>
                                <td>
                                    <asp:UpdatePanel ID="updHyojikensuu" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:DropDownList ID="DDL_Hyojikensuu" runat="server" Width="80px" AutoPostBack="True" Height="35px" CssClass="form-control JC12GridTextBox" Font-Size="14px" OnSelectedIndexChanged="DDL_Hyojikensuu_SelectedIndexChanged">
                                                <asp:ListItem Value="20" Selected="True">20件</asp:ListItem>
                                                <asp:ListItem Value="30">30件</asp:ListItem>
                                                <asp:ListItem Value="50">50件</asp:ListItem>
                                            </asp:DropDownList>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td>
                                    <asp:UpdatePanel ID="updhyojipopup" runat="server" AutoPostBack="true" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:Button ID="btnHyoujisetPopUp" runat="server" CssClass="JC07HyojiItemSettingBtn" Text="表示項目を設定"
                                                OnClick="btnHyoujiSetting_Click" OnClientClick="PopupOpenButton()" AutoPostBack="true" />
                                        </ContentTemplate>

                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>

                <div id="Div5" runat="server">
                    <asp:UpdatePanel ID="updMitsumoriGrid" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:TextBox ID="GV_MiRowindex" runat="server" Value="" Style="display: none" />
                             <%-- 20220321 Add Start--%>
                            <asp:GridView runat="server" ID="GV_Mitumori" BorderColor="Transparent" AutoGenerateColumns="false" EmptyDataRowStyle-CssClass="JC31NoDataMessageStyle"
                                ShowHeader="true" ShowHeaderWhenEmpty="true" CellPadding="0" OnPageIndexChanging="GV_Mitumori_PageIndexChanging" CssClass="GridViewStyle" Visible="false" DataKeyNames="見積コード" AllowSorting="True" OnSorting="GV_Mitumori_Sorting">
                               <%-- 20220321 Add End--%>
                                <HeaderStyle  BackColor="#F2F2F2" HorizontalAlign="Left" Height="38px" ForeColor="Black" />
                                <RowStyle Height="43px" CssClass="JC12GridItem" />
                                <Columns>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <%--<div class="grip" style=" text-align: left; padding-right: 4px;  overflow: hidden;  display: -webkit-box;  -webkit-line-clamp: 1; -webkit-box-orient: vertical;word-break: break-all;">--%>
                                            <%--<div class="dropdown" style="padding-left: 5px; width: 25px;">
                                                    <button class="btn dropdown-toggle" type="button" id="dropdownMenuButton" data-toggle="dropdown"
                                                        aria-haspopup="true" aria-expanded="false" style="border: 1px solid gainsboro; width: 20px; height: 20px; padding: 0px 3px 0px 1px; margin: 0;">
                                                    </button>
                                                    <div class="dropdown-menu fontcss " aria-labelledby="dropdownMenuButton" style="min-width: 1rem; width: 9rem; margin-left: 5px;">
                                                        <asp:LinkButton ID="lnkbtnMitsuEdit" class="dropdown-item" runat="server" Text='編集' Style="margin-right: 10px" CommandName="Update" CommandArgument="<%# Container.DataItemIndex %>"></asp:LinkButton>
                                                        <asp:LinkButton ID="lnkbtnMitsuCopy" class="dropdown-item" runat="server" Text='コピーして登録' Style="margin-right: 10px" CommandName="Copy" CommandArgument="<%# Container.DataItemIndex %>"></asp:LinkButton>
                                                        <asp:LinkButton ID="lnkbtnMitsuPDF" class="dropdown-item" runat="server" Text='見積書PDF出力' Style="margin-right: 10px" CommandName="PDF" CommandArgument="<%# Container.DataItemIndex %>"></asp:LinkButton>
                                                        <asp:LinkButton ID="lnkbtnMitsuDelete" class="dropdown-item" runat="server" Text='削除' Style="margin-right: 10px" CommandName="Delete" CommandArgument="<%# Container.DataItemIndex %>"></asp:LinkButton>
                                                    </div>
                                                </div>--%>
                                            <%--</div>--%>
                                            <div class="grip" style="text-align: left; width: 25px; padding-right: 4px; overflow: hidden; display: -webkit-box; -webkit-line-clamp: 1; -webkit-box-orient: vertical; word-break: break-all;">
                                                <cc1:HoverMenuExtender ID="HoverMenuExtender2" runat="server" PopupControlID="PopupMenu1"
                                                    TargetControlID="Panel2" PopupPosition="Bottom">
                                                </cc1:HoverMenuExtender>
                                                <asp:Panel ID="PopupMenu1" runat="server" CssClass="modalPopup dropdown-menu fontcss " aria-labelledby="dropdownMenuButton" Style="display: none; min-width: 1rem; width: 9rem; margin-left: 5px;">
                                                    <asp:LinkButton ID="lnkbtnMitsuEdit" class="dropdown-item fontcss" runat="server" Text='編集' Style="margin-right: 10px" CommandName="Update" CommandArgument="<%# Container.DataItemIndex %>"></asp:LinkButton>
                                                    <asp:LinkButton ID="lnkbtnMitsuCopy" class="dropdown-item fontcss" runat="server" Text='コピーして登録' Style="margin-right: 10px" CommandName="Copy" CommandArgument="<%# Container.DataItemIndex %>"></asp:LinkButton>
                                                    <asp:LinkButton ID="lnkbtnMitsuPDF" class="dropdown-item fontcss" runat="server" Text='見積書PDF出力' Style="margin-right: 10px" CommandName="PDF" CommandArgument="<%# Container.DataItemIndex %>"></asp:LinkButton>                                                   
                                                    <%-- 20220321 Add Start--%>
                                                    <asp:LinkButton ID="lnkbtnMitsuDelete" class="dropdown-item fontcss" runat="server" Text='削除' Style="margin-right: 10px"  CommandArgument="<%# Container.DataItemIndex %>" OnClick="lnkbtnMitsuDelete_Click"></asp:LinkButton>
                                                     <%-- 20220321 Add End--%>
                                                </asp:Panel>
                                                <asp:Panel ID="Panel2" runat="server" CssClass="modalPopup" Style="padding-left: 5px; width: 20px;">
                                                    <button class="btn dropdown-toggle" type="button" id="dropdownMenuButton" data-toggle="dropdown"
                                                        aria-haspopup="true" aria-expanded="false" style="border: 1px solid gainsboro; width: 20px; height: 20px; padding: 0px 3px 0px 1px; margin: 0;">
                                                    </button>
                                                </asp:Panel>
                                            </div>
                                        </ItemTemplate>
                                        <ItemStyle Width="38px" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="見積コード" SortExpression="見積コード">
                                        <ItemTemplate>
                                            <div class="grip" style="text-align: left; padding-right: 4px; overflow: hidden; display: -webkit-box; -webkit-line-clamp: 1; -webkit-box-orient: vertical; word-break: break-all;">
                                                <asp:LinkButton ID="LK_cMitumori" CssClass="JC07Labelcss" runat="server" Font-Underline="False" Text=' <%# Bind("見積コード","{0}") %>' OnClick="LK_cMitumori_Click"></asp:LinkButton>
                                            </div>
                                        </ItemTemplate>
                                       <%-- <HeaderTemplate>
                                            <asp:Label ID="lblcMitumoriHeader" runat="server" Text="見積コード" CssClass="d-inline-block"></asp:Label>

                                        </HeaderTemplate>--%>
                                         <HeaderStyle CssClass="JC31MeiHeaderCol"></HeaderStyle>
                                          <ItemStyle CssClass="JC31MitumoriCodeCol" />
                                        <%--  <HeaderStyle CssClass="JC31MitumoriCodeHeaderCol" />
                                            <ItemStyle CssClass="JC31MitumoriCodeCol" />--%>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="見積名" SortExpression="見積名">
                                        <ItemTemplate>
                                            <div class="grip" style="text-align: left; padding-right: 4px; overflow: hidden; display: -webkit-box; -webkit-line-clamp: 1; -webkit-box-orient: vertical; word-break: break-all;">
                                                <asp:Label ID="lblsMitumori_Grid" runat="server" Text='<%# Server.HtmlEncode((string)Eval("見積名","{0}"))%>' ToolTip='<%# Server.HtmlEncode((string)Eval("見積名","{0}"))%>'></asp:Label>
                                            </div>
                                        </ItemTemplate>
                                        <%--<HeaderTemplate>
                                            <asp:Label ID="lblsMitumoriHeader" runat="server" Text="見積名"></asp:Label>
                                        </HeaderTemplate>--%>
                                        <HeaderStyle CssClass="JC31MeiHeaderCol" />
                                        <%--<ItemStyle CssClass="JC31MitumoriMeiCol " />--%>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="自社担当者" SortExpression="営業担当"> <%--//20220502 Added by phoo--%>
                                        <ItemTemplate>
                                            <div class="grip" style="text-align: left; padding-right: 4px; overflow: hidden; display: -webkit-box; -webkit-line-clamp: 1; -webkit-box-orient: vertical; word-break: break-all;">
                                                <asp:Label ID="lblEigyoTantou_Grid" runat="server" CssClass="JC07Labelcss" Text=' <%# Server.HtmlEncode((string)Eval("営業担当","{0}"))%>' ToolTip=' <%# Server.HtmlEncode((string)Eval("営業担当","{0}"))%>'></asp:Label>
                                                <asp:Label ID="lbl_cEigyoTantou" runat="server" CssClass="JC07Labelcss" Text=' <%# Bind("cEIGYOTANTOSYA","{0}") %>' ToolTip=' <%# Bind("cEIGYOTANTOSYA","{0}") %>' Visible="false"></asp:Label>
                                                <%--20220107 MiMi Added--%>
                                            </div>
                                        </ItemTemplate>
                                        <%--<HeaderTemplate>
                                            <asp:Label ID="lblEigyouTantouHeader" runat="server" Text="営業担当" CssClass="d-inline-block" Style="text-align: left;"></asp:Label>
                                        </HeaderTemplate>--%>
                                        <HeaderStyle CssClass="JC31MeiHeaderCol" />
                                        <%--<ItemStyle CssClass="JC31SakuseiSyaHeaderCol" />--%>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="作成者" SortExpression="作成者">
                                        <ItemTemplate>
                                            <div class="grip" style="text-align: left; padding-right: 4px; overflow: hidden; display: -webkit-box; -webkit-line-clamp: 1; -webkit-box-orient: vertical; word-break: break-all;">
                                                <asp:Label ID="lblsSakuseisya_Grid" runat="server" CssClass="JC07Labelcss" Text=' <%# Bind("作成者","{0}") %>' ToolTip=' <%# Bind("作成者","{0}") %>'></asp:Label>
                                            </div>
                                        </ItemTemplate>
                                        <%--<HeaderTemplate>
                                            <asp:Label ID="lblsSakuseisyaHeader" runat="server" Text="作成者" CssClass="d-inline-block"></asp:Label>
                                        </HeaderTemplate>--%>
                                        <HeaderStyle CssClass="JC31MeiHeaderCol " />
                                        <%--<ItemStyle CssClass="JC31MitumoriMeiCol" />--%>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="見積日" SortExpression="見積日">
                                        <ItemTemplate>
                                            <div class="grip" style="text-align: left; padding-right: 4px; overflow: hidden; display: -webkit-box; -webkit-line-clamp: 1; -webkit-box-orient: vertical; word-break: break-all;">
                                                <asp:Label ID="lbldMitumori_Grid" runat="server" CssClass="JC07Labelcss" Text=' <%# Bind("見積日","{0:yyyy/MM/dd}") %>' ToolTip=' <%# Bind("見積日","{0:yyyy/MM/dd}") %>'></asp:Label>
                                            </div>
                                        </ItemTemplate>
                                       <%-- <HeaderTemplate>
                                            <asp:Label ID="lbldMitumoriHeader" runat="server" Text="見積日" CssClass="d-inline-block"></asp:Label>
                                        </HeaderTemplate>--%>
                                          <HeaderStyle CssClass="JC31MeiHeaderCol " />
                                            <%--<ItemStyle CssClass="JC31MeiHeaderCol" />--%>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="合計金額" SortExpression="合計金額">
                                        <ItemTemplate>
                                            <div class="grip" style="text-align: right; padding-right: 4px; overflow: hidden; display: -webkit-box; -webkit-line-clamp: 1; -webkit-box-orient: vertical; word-break: break-all;">
                                                <asp:Label ID="lblnGokeiKingaku_Grid" runat="server" CssClass="JC07Labelcss" Text=' <%# Bind("合計金額","{0:#,##0.##}") %>' ToolTip=' <%# Bind("合計金額","{0:#,##0.##}") %>'></asp:Label>
                                            </div>
                                        </ItemTemplate>
                                        <%--<HeaderTemplate>
                                            <asp:Label ID="lblnGokeiKingakuHeader" runat="server" Text="合計金額" CssClass="d-inline-block"></asp:Label>
                                        </HeaderTemplate>--%>
                                        <HeaderStyle CssClass="JC31MeiHeaderCol JC31kingaku AlignRight"/>
                                            <%--<ItemStyle CssClass="JC31MitumoriMeiCol" />--%>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="見積状態" SortExpression="見積状態">
                                        <ItemTemplate>
                                            <div class="grip" style="text-align: left; padding-right: 4px; overflow: hidden; display: -webkit-box; -webkit-line-clamp: 1; -webkit-box-orient: vertical; word-break: break-all;">
                                                <asp:Label ID="lblMitumoriJoutai_Grid" runat="server" CssClass="JC07Labelcss" Text=' <%# Bind("見積状態","{0}") %>' ToolTip=' <%# Bind("見積状態","{0}") %>'></asp:Label>
                                            </div>
                                        </ItemTemplate>
                                        <%--<HeaderTemplate>
                                            <asp:Label ID="lblMitumoriJoutaiHeader" runat="server" Text="見積状態" CssClass="d-inline-block"></asp:Label>
                                        </HeaderTemplate>--%>
                                        <HeaderStyle CssClass="JC31MeiHeaderCol " />
                                        <%--<ItemStyle CssClass="JC31MeiHeaderCol" />--%>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="金額粗利" SortExpression="金額粗利">
                                        <ItemTemplate>
                                            <div class="grip" style="text-align: right; padding-right: 4px; overflow: hidden; display: -webkit-box; -webkit-line-clamp: 1; -webkit-box-orient: vertical; word-break: break-all;">
                                                <asp:Label ID="lblnArari_Grid" runat="server" CssClass="JC07Labelcss" Text=' <%# Bind("金額粗利","{0:#,##0.##}") %>' ToolTip=' <%# Bind("金額粗利","{0:#,##0.##}") %>'></asp:Label>
                                            </div>
                                        </ItemTemplate>
                                       <%-- <HeaderTemplate>
                                            <asp:Label ID="lblnArariHeader" runat="server" Text="金額粗利" CssClass="d-inline-block"></asp:Label>
                                        </HeaderTemplate>--%>
                                         <HeaderStyle CssClass="JC31MeiHeaderCol JC31kingaku AlignRight" />
                                             <ItemStyle CssClass="AlignRight" />
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="メモ" SortExpression="社内メモ"> <%--//20220502 Added by phoo--%>
                                        <ItemTemplate>
                                            <div class="grip" style="text-align: left; padding-right: 4px; overflow: hidden; display: -webkit-box; -webkit-line-clamp: 1; -webkit-box-orient: vertical; word-break: break-all;">
                                                <asp:Label ID="lblMemo_Grid" runat="server" Text=' <%# Server.HtmlEncode((string)Eval("社内メモ","{0}"))%>' ToolTip=' <%# Server.HtmlEncode((string)Eval("社内メモ","{0}"))%>'></asp:Label>
                                            </div>
                                        </ItemTemplate>
                                       <%-- <HeaderTemplate>
                                            <asp:Label ID="lblMemoHeader" runat="server" Text="社内メモ" CssClass="d-inline-block"></asp:Label>
                                        </HeaderTemplate>--%>
                                         <HeaderStyle CssClass="JC31MeiHeaderCol " />
                                            <%--<ItemStyle CssClass="JC31MitumoriMeiCol" />--%>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="備考" SortExpression="見積書備考">  <%--//20220502 Added by phoo--%>
                                        <ItemTemplate>
                                            <div class="grip" style="text-align: left; padding-right: 4px; overflow: hidden; display: -webkit-box; -webkit-line-clamp: 1; -webkit-box-orient: vertical; word-break: break-all;">
                                                <asp:Label ID="lblBikou_Grid" runat="server" Text='<%# Server.HtmlEncode((string)Eval("見積書備考","{0}"))%>' ToolTip=' <%# Server.HtmlEncode((string)Eval("見積書備考","{0}"))%>'></asp:Label>
                                            </div>
                                        </ItemTemplate>
                                        <%--<HeaderTemplate>
                                            <asp:Label ID="lblBikouHeader" runat="server" Text="見積書備考" CssClass="d-inline-block"></asp:Label>
                                        </HeaderTemplate>--%>
                                        <HeaderStyle CssClass="JC31MeiHeaderCol " />
                                            <%--<ItemStyle CssClass="JC31MitumoriMeiCol AlignLeft" />--%>
                                    </asp:TemplateField>
                                   <asp:TemplateField HeaderText="得意先" SortExpression="得意先名"> <%--//20220502 Added by phoo--%>
                                        <ItemTemplate>
                                            <div class="grip" style="text-align: left; padding-right: 4px; overflow: hidden; display: -webkit-box; -webkit-line-clamp: 1; -webkit-box-orient: vertical; word-break: break-all;">
                                                <asp:Label ID="lblsTokuisaki_Grid" runat="server" Text=' <%# Server.HtmlEncode((string)Eval("得意先名","{0}"))%>' ToolTip=' <%# Server.HtmlEncode((string)Eval("得意先名","{0}"))%>'></asp:Label>
                                            </div>
                                        </ItemTemplate>
                                       <%-- <HeaderTemplate>
                                            <asp:Label ID="lblsTokuisakiHeader" runat="server" Text="得意先名" CssClass="d-inline-block"></asp:Label>
                                        </HeaderTemplate>--%>
                                       <HeaderStyle CssClass="JC31MeiHeaderCol " />
                                            <%--<ItemStyle CssClass="JC31MitumoriMeiCol AlignLeft" />--%>
                                    </asp:TemplateField>
                                   <asp:TemplateField HeaderText="得意先担当者" SortExpression="得意先担当"> <%--//20220502 Added by phoo--%>
                                        <ItemTemplate>
                                            <div class="grip" style="text-align: left; padding-right: 4px; overflow: hidden; display: -webkit-box; -webkit-line-clamp: 1; -webkit-box-orient: vertical; word-break: break-all;">
                                                <asp:Label ID="lblsTokuisakiTantou_Grid" runat="server" Text='<%# Server.HtmlEncode((string)Eval("得意先担当","{0}"))%>' ToolTip=' <%# Server.HtmlEncode((string)Eval("得意先担当","{0}"))%>'></asp:Label>
                                            </div>
                                        </ItemTemplate>
                                        <%--<HeaderTemplate>
                                            <asp:Label ID="lblsTokuisakiTantouHeader" runat="server" Text="得意先担当" CssClass="d-inline-block"></asp:Label>
                                        </HeaderTemplate>--%>
                                        <HeaderStyle CssClass="JC31MeiHeaderCol " />
                                            <%--<ItemStyle CssClass="JC31MitumoriMeiCol AlignLeft" />--%>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="画像" SortExpression="画像" Visible="false">
                                        <ItemTemplate>
                                            <div class="grip" style="text-align: left; padding-right: 4px; overflow: hidden; display: -webkit-box; -webkit-line-clamp: 1; -webkit-box-orient: vertical; word-break: break-all;">
                                                <cc1:HoverMenuExtender ID="HoverMenuExtender1" runat="server" PopupControlID="PopupMenu"
                                                    TargetControlID="Image1" PopupPosition="Bottom">
                                                </cc1:HoverMenuExtender>
                                                <asp:Panel ID="PopupMenu" runat="server" CssClass="modalPopup" Style="display: none;">
                                                    <asp:Image ID="popupimg" runat="server" CssClass="imagecss"
                                                        Visible='<%# Eval("file64string").ToString() != "../Img/imgerr.png" %>' ImageUrl='<%# Eval("file64string") %>' />
                                                </asp:Panel>

                                                <asp:Image ID="Image1" runat="server" CssClass="popupHover" Width="30px" Height="30px" Visible='<%# Eval("file64string").ToString() != "../Img/imgerr.png"  %>' ImageUrl='<% # Eval("file64string") %>' />

                                            </div>
                                        </ItemTemplate>
                                        <%--<HeaderTemplate>
                                            <asp:Label ID="lblsFileHeader" runat="server" Text="画像" CssClass="d-inline-block"></asp:Label>
                                        </HeaderTemplate>--%>
                                        <HeaderStyle CssClass="JC31MeiHeaderCol " />
                                            <%--<ItemStyle CssClass="JC31MitumoriImgCol AlignLeft" />--%>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>

                            <div class="d-flex justify-content-center" style="background-color: white; padding: 0px 10px 0px 10px;">
                                <div style="overflow-x: auto; width: auto;">
                                    <%-- 20220321 Add Start--%>
                                    <asp:GridView runat="server" ID="GV_Mitumori_Original" BorderColor="Transparent" EmptyDataText="該当するデータがありません。" AutoGenerateColumns="false" EmptyDataRowStyle-CssClass="JC31NoDataMessageStyle"
                                        ShowHeader="true" ShowHeaderWhenEmpty="true" CellPadding="7" DataKeyNames="見積コード" OnRowCreated="GV_Mitumori_Original_RowCreated" OnPageIndexChanging="GV_Mitumori_PageIndexChanging" EnableViewState="false" Visible="true" AllowSorting="True" OnSorting="GV_Mitumori_Sorting" OnRowDataBound="GV_Mitumori_Original_RowDataBound" OnRowCommand="GV_Mitsumori_Original_RowCommand"  
                                        Width="1290px" CssClass="GridViewStyle">
                                       <%-- 20220321 Add Start--%>
                                        <EmptyDataRowStyle CssClass="JC31NoDataMessageStyle" />
                                        <HeaderStyle Height="38px" BackColor="#F2F2F2" HorizontalAlign="Left" ForeColor="Black"/>
                                        <RowStyle Height="43px" CssClass="gvRowStyle" />
                                        <Columns>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <%--<div class="dropdown" style="padding-left: 5px; width: 25px;">
                                                            <button class="btn dropdown-toggle" type="button" id="dropdownMenuButton" data-toggle="dropdown"
                                                                aria-haspopup="true" aria-expanded="false" style="border: 1px solid gainsboro; width: 20px; height: 20px; padding: 0px 3px 0px 1px; margin: 0;">
                                                            </button>
                                                            
                                                            <div class="dropdown-menu fontcss" aria-labelledby="dropdownMenuButton" style="min-width: 1rem; width: 9rem; margin-left: 5px;">
                                                                <asp:LinkButton ID="lnkbtnMitsuEdit" class="dropdown-item" runat="server" Text='編集' Style="margin-right: 10px" CommandName="Update" CommandArgument="<%# Container.DataItemIndex %>"></asp:LinkButton>
                                                                <asp:LinkButton ID="lnkbtnMitsuCopy" class="dropdown-item" runat="server" Text='コピーして登録' Style="margin-right: 10px" CommandName="Copy" CommandArgument="<%# Container.DataItemIndex %>"></asp:LinkButton>
                                                                <asp:LinkButton ID="lnkbtnMitsuPDF" class="dropdown-item" runat="server" Text='見積書PDF出力' Style="margin-right: 10px" CommandName="PDF" CommandArgument="<%# Container.DataItemIndex %>"></asp:LinkButton>
                                                                <asp:LinkButton ID="lnkbtnMitsuDelete" class="dropdown-item" runat="server" Text='削除' Style="margin-right: 10px" CommandName="Delete" CommandArgument="<%# Container.DataItemIndex %>"></asp:LinkButton>
                                                            </div>
                                                        </div>--%>
                                                    <div class="grip" style="text-align: left; width: 25px; padding-right: 4px; overflow: hidden; display: -webkit-box; -webkit-line-clamp: 1; -webkit-box-orient: vertical; word-break: break-all;">
                                                        <cc1:HoverMenuExtender ID="HoverMenuExtender2" runat="server" PopupControlID="PopupMenu1"
                                                            TargetControlID="Panel2" PopupPosition="Bottom">
                                                        </cc1:HoverMenuExtender>
                                                        <asp:Panel ID="PopupMenu1" runat="server" CssClass="modalPopup dropdown-menu fontcss " aria-labelledby="dropdownMenuButton" Style="display: none; min-width: 1rem; width: 9rem; margin-left: 5px;">
                                                            <asp:LinkButton ID="lnkbtnMitsuEdit" class="dropdown-item fontcss" runat="server" Text='編集' Style="margin-right: 10px" CommandName="Update" CommandArgument="<%# Container.DataItemIndex %>"></asp:LinkButton>
                                                            <asp:LinkButton ID="lnkbtnMitsuCopy" class="dropdown-item fontcss" runat="server" Text='コピーして登録' Style="margin-right: 10px" CommandName="Copy" CommandArgument="<%# Container.DataItemIndex %>"></asp:LinkButton>
                                                            <asp:LinkButton ID="lnkbtnMitsuPDF" class="dropdown-item fontcss" runat="server" Text='見積書PDF出力' Style="margin-right: 10px" CommandName="PDF" CommandArgument="<%# Container.DataItemIndex %>"></asp:LinkButton>
                                                           <%-- 20220321 Add Start--%>
                                                            <asp:LinkButton ID="lnkbtnMitsuDelete" class="dropdown-item fontcss" runat="server" Text='削除' Style="margin-right: 10px"  CommandArgument="<%# Container.DataItemIndex %>" OnClick="lnkbtnMitsuDelete_Click"></asp:LinkButton>
                                                            <%-- 20220321 Add End--%>
                                                        </asp:Panel>
                                                        <asp:Panel ID="Panel2" runat="server" CssClass="modalPopup" Style="padding-left: 5px; width: 20px;">
                                                            <button class="btn dropdown-toggle" type="button" id="dropdownMenuButton" data-toggle="dropdown"
                                                                aria-haspopup="true" aria-expanded="false" style="border: 1px solid gainsboro; width: 20px; height: 20px; padding: 0px 3px 0px 1px; margin: 0;">
                                                            </button>
                                                        </asp:Panel>
                                                    </div>
                                                </ItemTemplate>
                                                <ItemStyle Width="38px" />

                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>

                                </div>
                            </div>
                            <asp:GridView runat="server" ID="GridView1" BorderColor="Transparent" AutoGenerateColumns="true" EmptyDataRowStyle-CssClass="JC31NoDataMessageStyle"
                                ShowHeader="false" ShowHeaderWhenEmpty="true" CellPadding="7" AllowPaging="True" OnPageIndexChanging="GV_Mitumori_PageIndexChanging"
                                OnRowDataBound="GV_MitumoriPg_Original_RowDataBound" DataKeyNames="見積コード"
                                Width="1290px">


                                <RowStyle Height="0px" />
                                <PagerStyle Font-Size="14px" HorizontalAlign="Center" CssClass="GridPager" VerticalAlign="Middle" />
                                <PagerSettings Mode="NumericFirstLast" FirstPageText="&lt;" LastPageText="&gt;" />
                            </asp:GridView>
                        </ContentTemplate>



                    </asp:UpdatePanel>
                </div>

                <asp:Button ID="btnDateChange" runat="server" Text="Button" OnClick="btnDateChange_Click" Style="display: none;" />
                <asp:HiddenField ID="hdnHome" runat="server" />
                 <asp:Button ID="btnDeleteMitu" runat="server" Text="" class="JC09GrayButton DisplayNone" Width="100px" Height="36px" OnClick="btnDeleteMitu_Click"/> <!--20220321 added -->
                <asp:HiddenField ID="hdnmituID" runat="server" />
                <div align="center" style="padding: 10px 0px 10px 0px;">
                </div>
                <div style="display: none; position: absolute">
                     <%-- 20220321 Add Start--%>
                    <asp:TextBox ID="HiddenMitsumoriId" runat="server" Value="" Style="display: none;" />
                     <%-- 20220321 Add End--%>
                    <asp:Button ID="btnMitsuHyouji" runat="server" Text="見積表示" Style="display: none;" />
                    <asp:Button ID="btnHyoujiSettingClose" runat="server" CssClass="DisplayNone" />
                </div>
                <%-- 20220107 MiMi Add Start--%>
                <asp:UpdatePanel ID="upd_Hidden" runat="server" UpdateMode="Conditional">
                    <Triggers>
                        <asp:PostBackTrigger ControlID="BT_MitumoriPDF" />
                    </Triggers>
                    <ContentTemplate>
                        <asp:DropDownList ID="DDL_Logo" runat="server" Width="200px" AutoPostBack="True" Height="30px" CssClass="form-control JC10GridTextBox" Style="font-size: 13px;" Visible="False">
                        </asp:DropDownList>
                        <asp:Button ID="BT_MitumoriPDF" runat="server" Text="Button" OnClick="BT_MitumoriPDF_Click" Style="display: none;" />
                        <asp:HiddenField ID="HF_cMitumori" runat="server" />
                        <asp:HiddenField ID="HF_cBukken" runat="server" />
                        <asp:HiddenField ID="HF_ctantousya" runat="server" />
                         <asp:HiddenField ID="HF_sMitumori" runat="server" />
                    </ContentTemplate>
                </asp:UpdatePanel>
                <%-- 20220107 MiMi Add End--%>
            </div>

            <div>
                <!--ポップアップ画面-->


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
                                <asp:Button ID="btnHyoujiSave" runat="server" Text="Button" CssClass="DisplayNone" OnClick="btnSaveHyoujiClose_Click" onClientClick="MasterOverflow()" />
                                <asp:Button ID="btnHyoujiClose" runat="server" Text="Button" CssClass="DisplayNone" OnClick="btnHyoujiSettingClose_Click" onClientClick="MasterOverflow()" />

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
                                <iframe id="ifdatePopup" runat="server" class="NyuryokuIframe RadiusIframe" scrolling="no" style="max-width: 260px; min-width: 260px; max-height: 365px; min-height: 365px;"></iframe>
                                <asp:Button ID="btnCalendarClose" runat="server" Text="Button" CssClass="DisplayNone" OnClick="btnCalendarClose_Click" onClientClick="MasterOverflow()"/>
                                <asp:Button ID="btnCalendarSettei" runat="server" Text="Button" CssClass="DisplayNone" OnClick="btnCalendarSettei_Click" onClientClick="MasterOverflow()"/>
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
                        <asp:Panel ID="pnlSentakuPopupScroll" runat="server" Style="display: none; overflow-x: hidden; overflow-y: hidden;" CssClass="PopupScrollDiv">
                            <asp:Panel ID="pnlSentakuPopup" runat="server">
                                <iframe id="ifSentakuPopup" runat="server" scrolling="no" class="NyuryokuIframe" style="border-radius: 0px;"></iframe>
                                <asp:Button ID="btnClose" runat="server" Text="Button" Style="display: none" OnClick="btnClose_Click" onClientClick="MasterOverflow()"/>
                                <asp:Button ID="btnJishaTantouSelect" runat="server" Text="Button" Style="display: none" OnClick="btnJishaTantouSelect_Click" onClientClick="MasterOverflow()"/>
                                <asp:Button ID="btnClose1" runat="server" Text="Button" Style="display: none" OnClick="btnClose1_Click" onClientClick="MasterOverflow()"/>
                                <asp:Button ID="btnJyoutaiSelect" runat="server" Text="Button" Style="display: none" OnClick="btnJyoutaiSelect_Click" onClientClick="MasterOverflow()"/>
                            </asp:Panel>
                        </asp:Panel>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
        <!--20220321 start-->
            </ContentTemplate>
        </asp:UpdatePanel>
        <!--20220321 end-->
    </body>
    <link href="../Style/cloudflare-jquery-ui.min.css" rel="stylesheet" />
    <%--<script src="https://code.jquery.com/jquery-3.2.1.min.js"></script>
    <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.min.js"></script>--%>
    <script src="../Scripts/cloudflare-jquery-ui-i18n.min.js"></script>


    <script type="text/javascript">
        function ClickPDFButton()  /*20220107 MiMi Added*/ {
            document.getElementById("<%=BT_MitumoriPDF.ClientID %>").click();
        }

    </script>

    <script type="text/javascript">
        Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(function (evt, args) {
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
                    //document.getElementById("<%=btnDateChange.ClientID %>").click();
                    }
                }
            );

        });
    </script>


    <script type="text/javascript">

        function texboxchange() {
            // alert("ok");
            var cMitumori = document.getElementById("<%= TB_MitsuListCode.ClientID %>").value;
            if (cMitumori != "") {
                document.getElementById("<%= TB_MitsuListCode.ClientID %>").value = cMitumori.padStart(10, '0');
            }
            return false; // this will call textbox changed event.
        }
        var Popup; function CreatepopUp(url) {
            Popup = window.open(url, "Popup", 'toolbar=0,scrollbars=0,location=0,statusbar=0,menubar=0,resizable=1,width=420,height=300,left = 490,top = 262');
            Popup.focus();
        }
       function btnClear_Click() {

            document.getElementById("<%= txtSyohin1.ClientID %>").value = "";
            document.getElementById("<%= txtTokuisaki.ClientID %>").value = "";
            document.getElementById("<%= txtsMitumori.ClientID %>").value = "";
            document.getElementById("<%= TB_MitsuListCode.ClientID %>").value = "";
            document.getElementById("<%= btn_Jyotai.ClientID %>").value = "選択なし";

            document.getElementById("<%= btn_Tantousya.ClientID %>").value = "選択なし";
            document.getElementById("<%= txtTokuisakiTantou.ClientID %>").value = "";
            document.getElementById("<%= txtmemo.ClientID %>").value = "";
            document.getElementById("<%= txtSyohin2.ClientID %>").value = "";
            document.getElementById("<%= txtSyohin3.ClientID %>").value = "";

            document.getElementById("<%= lbldMitumoriS.ClientID %>").Text = "";
            document.getElementById("<%= lbldMitumoriS.ClientID %>").value = "";
            document.getElementById("<%= divMitumoriSDate.ClientID %>").style.display = "none";
            document.getElementById("<%= btnMitumoriStart.ClientID %>").style.display = "block";

            document.getElementById("<%= lbldMitumoriE.ClientID %>").Text = "";
            document.getElementById("<%= lbldMitumoriE.ClientID %>").value = "";
            document.getElementById("<%= divMitumoriEDate.ClientID %>").style.display = "none";
            document.getElementById("<%= btnMitumoriEnd.ClientID %>").style.display = "block";

            document.getElementById("<%= lblUriageYoteiStartDate.ClientID %>").Text = "";
            document.getElementById("<%= lblUriageYoteiStartDate.ClientID %>").value = "";
            document.getElementById("<%= divUriageYoteiStartDate.ClientID %>").style.display = "none";
            document.getElementById("<%= btnUriageYoteiStartDate.ClientID %>").style.display = "block";

            document.getElementById("<%= lblUriageYoteiEndDate.ClientID %>").Text = "";
            document.getElementById("<%= lblUriageYoteiEndDate.ClientID %>").value = "";
            document.getElementById("<%= divUriageYoteiEndDate.ClientID %>").style.display = "none";
            document.getElementById("<%= btnUriageYoteiEndDate.ClientID %>").style.display = "block";

            document.getElementById("<%= lblJuchuuStart.ClientID %>").Text = "";
            document.getElementById("<%= lblJuchuuStart.ClientID %>").value = "";
            document.getElementById("<%= divJuchuuStart.ClientID %>").style.display = "none";
            document.getElementById("<%= btnJuchuuStart.ClientID %>").style.display = "block";

            document.getElementById("<%= lblJuchuuEnd.ClientID %>").Text = "";
            document.getElementById("<%= lblJuchuuEnd.ClientID %>").value = "";
            document.getElementById("<%= divJuchuuEnd.ClientID %>").style.display = "none";
            document.getElementById("<%= btnJuchuuEnd.ClientID %>").style.display = "block";

            document.getElementById("<%= lblUriageYoteiStartDate.ClientID %>").Text = "";
            document.getElementById("<%= lblUriageYoteiStartDate.ClientID %>").value = "";
            document.getElementById("<%= divUriageYoteiStartDate.ClientID %>").style.display = "none";
            document.getElementById("<%= btnUriageYoteiStartDate.ClientID %>").style.display = "block";

            document.getElementById("<%= lblUriageYoteiEndDate.ClientID %>").Text = "";
            document.getElementById("<%= lblUriageYoteiEndDate.ClientID %>").value = "";
            document.getElementById("<%= divUriageYoteiEndDate.ClientID %>").style.display = "none";
            document.getElementById("<%= btnUriageYoteiEndDate.ClientID %>").style.display = "block";
                            
            return false;
       }
    </script>
    </html>
</asp:Content>
