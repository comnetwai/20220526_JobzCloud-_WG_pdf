<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="JCHidukeSelect.aspx.cs" Inherits="JobzRacoo.WebFront.JCHidukeSelect" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <asp:PlaceHolder runat="server">
        <%: Scripts.Render("~/bundles/modernizr") %>
        <%: Styles.Render("~/style/StyleBundle1") %>
        <%: Scripts.Render("~/scripts/ScriptBundle1") %>
    </asp:PlaceHolder>
    <webopt:BundleReference runat="server" Path="~/Content/bootstrap" />
    <script type="text/javascript">
        // master pageのPopupの大きさのため
        //['load', 'resize'].forEach(function (e) {
        //    window.addEventListener(e, function () {
        //        showHidukePopup();
        //    });
        //});

        //function showHidukePopup() {
        //    var hdnHome = document.getElementById("hdnHome");
        //    if (hdnHome.value == "HOME") {
        //        showPopup('pnlShinkiPopupScroll', 'pnlShinkiPopup', 'ifShinkiPopup');
        //    }
        //    else {
        //        showPopup('pnlSentakuPopupScroll', 'pnlSentakuPopup', 'ifSentakuPopup', 'Popup');
        //    }
        //}
    </script>
</head>
<body class="text-center bg-transparent">
    <div class="JCHDiv">
        <form id="form1" runat="server">
            <%--案件情報入力--%>
            <asp:ScriptManager ID="ScriptManager1" EnablePageMethods="True" runat="server">
                <Scripts>
                    <asp:ScriptReference Name="jquery" />
                </Scripts>
            </asp:ScriptManager>
            <asp:PlaceHolder runat="server">
                <%: Scripts.Render("~/bundles/jqueryui") %>
            </asp:PlaceHolder>
            <asp:HiddenField ID="hdnHome" runat="server" />
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <div class="SentakuHeaderDiv">
                        <asp:Label ID="lblTsuika" runat="server" CssClass="SentakuTitleLbl SentakuHeaderLbl" style="font-size:13px;" Text="日付を選択"></asp:Label>
                        <asp:Button ID="btnSentakuHeaderClose" runat="server" Text="✕" CssClass="SentakuPopupCloseBtn"
                            OnClick="btnSentakuHeaderClose_Click" />
                    </div>
                    <div class="JCHBorderLine"></div>
                    <table>
                        <tr>
                            <td>
                                <div>
                                    <asp:Calendar ID="Calendar" runat="server" DayNameFormat="Shortest" CssClass="JCHCalendar"
                                        BorderStyle="None" OnDayRender="Calendar_DayRender" OnSelectionChanged="Calendar_SelectionChanged"
                                        PrevMonthText="<img src='../Img/img-leftarrow.png' border='0' width=15px height=13px style='vertical-align:unset' />" TodayDayStyle-BackColor="white" TodayDayStyle-ForeColor="DeepSkyBlue"
                                        NextMonthText="<img src='../Img/img-rightarrow.png' border='0' width=15px height=13px style='vertical-align:unset' />">
                                        <DayHeaderStyle CssClass="JCHDayStyle" />
                                        <NextPrevStyle CssClass="JCHButton" ForeColor="Gray" Width="20px" Height="22px" />
                                        <OtherMonthDayStyle ForeColor="lightgray" CssClass="JCHOtherMonthDayStyle" />
                                        <SelectedDayStyle CssClass="JCHSelectedDayStyle" />
                                        <SelectorStyle CssClass="JCHSelectorStyle" />
                                        <TitleStyle CssClass="JCHTitleStyle" BackColor="White" />
                                    </asp:Calendar>
                                </div>
                                <div class="JCHDivJikan">
                                    <table>
                                        <tr>
                                            <td class="JCHTdButtonHeight">
                                                <asp:Button ID="btnKadouJikanSettei" runat="server" Text="設定" CssClass="JCHKadouJikanSetteiBtn BlueBackgroundButton" OnClick="btnKadouJikanSettei_Click" />
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </td>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
        </form>
        <script type="text/javascript">
            $(document).keyup(function (e) {
                EscKeyPress(e, 'btnSentakuHeaderClose');
            });
    </script>
    </div>
</body>
</html>
