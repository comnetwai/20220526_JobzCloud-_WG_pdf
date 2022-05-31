<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="JC18TokuisakiKensaku.aspx.cs" Inherits="JobzCloud.WebFront.JC18TokuisakiKensaku" EnableEventValidation="false" ValidateRequest="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=0, shrink-to-fit=no" />
    <title></title>
    <asp:PlaceHolder runat="server">
        <%: Scripts.Render("~/bundles/modernizr") %>
        <%: Styles.Render("~/style/StyleBundle1") %>
        <%: Scripts.Render("~/scripts/ScriptBundle1") %>
        <%: Styles.Render("~/style/UCStyleBundle") %>

    </asp:PlaceHolder>
    <link href="../Content1/bootstrap.min.css" rel="stylesheet" />

    <%--20220208 Added By 　エインドリ－・プ－プゥ End --%>
    <%--  <script src="../Scripts/bootstrap.bundle.min.js"></script>

     <link href="../resources/css/bootstrap.min.css" rel="stylesheet" />
    <script src="../resources/js/jquery.min.js"></script>
    <script src="../resources/js/bootstrap.min.js"></script>
    <script src="../resources/js/popper.min.js"></script>--%>
    <style>
        /*.GVFixedHeader {
                font-weight: bold;
                position: absolute;
                top: expression(this.parentNode.parentNode.parentNode.scrollTop);
            }*/

        .right {
            position: absolute;
            right: 120px;
            padding: 2px;
        }

        .marginHeader {
            text-align: center !important;
        }

        th {
            position: -webkit-sticky;
            position: sticky !important;
            top: 0;
            background-color: rgb(242,242,242);
            border-color: rgb(242,242,242);
            border: 0px;
        }

        /*20220210 Added By 　エインドリ－・プ－プゥ*/
        .grid_header {
            overflow: auto;
            white-space: nowrap;
        }

        .GridViewStyle 
            {
                z-index:3 !important;
                overflow:unset !important;
                overflow-x:auto !important;
                
            }

            
            .GridViewStyle tr td
            {
                z-index:3 !important;
                 overflow:unset !important;
                overflow-x:auto !important;
            }

              .GridViewStyle tr th
            {
                z-index:3 !important;
                overflow:unset !important;
               overflow-x:auto !important;
            }
    </style>
</head>
<body id="BD_Master" runat="server" class="bg-transparent">
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" EnablePageMethods="True" runat="server">
            <Scripts>
                <asp:ScriptReference Name="jquery" />
                <asp:ScriptReference Name="bootstrap" />
                <asp:ScriptReference Path="../Scripts/Common/FixFocus.js" />
                <asp:ScriptReference Path="../Scripts/colResizable-1.6.min.js" />
                <asp:ScriptReference Path="../Scripts/cookie.js" />
            </Scripts>
        </asp:ScriptManager>
        <asp:UpdatePanel ID="updBody" runat="server" UpdateMode="Conditional">
            <ContentTemplate>

                <div id="Div_Body" runat="server" style="background-color: transparent; padding-top: 50px; min-height: 100%;">
                    <div style="max-width: 1140px; margin-left: auto; margin-right: auto; background-color: white; padding: 10px 0px 0px 0px;">
                        <%--20220304 MyatNoe Added and Delete--%>
                        <%-- <div style="max-width: 1160px; background-color: white; margin-left: auto; margin-right: auto; padding: 0; left: 50%; top: 50%; position: absolute; transform: translate(-50%, -50%);">--%>
                        <div style="margin-left: auto; margin-right: auto;">
                            <asp:Label ID="lblHeader" runat="server" Text="得意先を選択" CssClass="TitleLabel" Style="text-align: left;"></asp:Label>
                            <asp:Button ID="btnFusenHeaderCross" runat="server" Text="✕" CssClass="PopupCloseBtn" OnClick="btnFusenHeaderCross_Click" />
                        </div>
                        <div class="Borderline"></div>
                        <div style="margin: 15px 20px 20px 20px;">
                            <table style="width: 100% !important; margin-bottom: 20px; padding-left: 3px;">
                                <tr>
                                    <td>
                                        <asp:TextBox runat="server" ID="txtCode" MaxLength="20" placeholder="コード、名前で検索できます。" CssClass="form-control TextboxStyle JC18TextBox " onfocus="this.setSelectionRange(this.value.length, this.value.length);" AutoPostBack="true" Width="480px" OnTextChanged="txtCode_TextChanged" TextMode="Search"></asp:TextBox>

                                    </td>
                                    <td align="right" style="padding-right: 5px;">
                                        <asp:Button ID="btnCreate" runat="server" Text="得意先を新規作成" CssClass="BlueBackgroundButton JC10SaveBtn" OnClick="btnCreate_Click" Style="margin: 0px !important;" OnClientClick="PopupOpenButton()" />
                                    </td>
                                </tr>
                            </table>

                            <div style="overflow-x: auto; width: 100% !important;">
                                <div style="max-height:500px;background-color: white; width: auto; display: inline-block !important;">
                                    <%--20220225 MyatNoe Added--%>
                                    <%--<div style="width: auto; overflow-x: auto;">--%> <%--max-height: 424px;--%><%-- 20220304 MyatNoe Delete--%>
                                   <%-- <asp:Panel ID="Panel1" runat="server" Style="max-width: 1900px; min-width: 1100px; overflow-x: auto;">--%>

                                        <asp:GridView ID="grid_touisaki" runat="server" BorderColor="Transparent" AutoGenerateColumns="false" EmptyDataRowStyle-CssClass="JC18NoDataMessageStyle" HtmlEncode="false"
                                            ShowHeader="true" ShowHeaderWhenEmpty="true" HeaderStyle-CssClass="GVFixedHeader" RowStyle-CssClass="GridRow" CellPadding="0" AllowSorting="True" CssClass="RowHover GridViewStyle" 
                                            OnSorting="grid_touisaki_Sorting" OnRowCreated="grid_touisaki_RowCreated" OnRowDataBound="grid_touisaki_RowDataBound" OnSelectedIndexChanged="grid_touisaki_SelectedIndexChanged" OnPreRender="grid_touisaki_PreRender">
                                            <EmptyDataRowStyle CssClass="JC18NoDataMessageStyle" />
                                            <HeaderStyle Height="45px" BackColor="#F2F2F2" ForeColor="Black" CssClass="grid_header" />
                                            <%--20220210 Added CssClass & Updated Header Height エインドリ－・プ－プゥ--%>
                                            <RowStyle CssClass="JC12GridItem" Height="43px" />
                                            <SelectedRowStyle BackColor="#EBEBF5" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="コード" SortExpression="cTOKUISAKI">
                                                    <ItemTemplate>
                                                        <div class="grip" style="text-align: left; padding-right: 4px; overflow: hidden; display: -webkit-box; -webkit-line-clamp: 1; -webkit-box-orient: vertical; word-break: break-all;">
                                                            <%--20220217 MiMi Updated Style --%>
                                                            <%--    <asp:LinkButton runat="server" ID="lnkCode" Text='<%# Eval("cTOKUISAKI","{0}") %>' OnClick="lnkCode_Click" Font-Underline="false" Font-Size="13px" CommandArgument='<%# Container.DataItemIndex %>'/>--%>
                                                            <%--20220112 Added By エインドリ－・プ－プゥ--%>
                                                            <asp:Label runat="server" ID="lblcToukuisaki" Text='<%# Eval("cTOKUISAKI","{0}") %>' Font-Underline="false" Font-Size="13px" CommandArgument='<%# Container.DataItemIndex %>' />
                                                        </div>
                                                    </ItemTemplate>
                                                    <HeaderStyle Wrap="False" CssClass="JC18CodeHeaderCol" BorderColor="White" BorderWidth="2px" Height="43px"></HeaderStyle>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="得意先名" SortExpression="sTOKUISAKI1">
                                                    <ItemTemplate>
                                                        <div class="grip" style="min-width: 50px; text-align: left; padding-right: 4px; overflow: hidden; display: -webkit-box; -webkit-line-clamp: 1; -webkit-box-orient: vertical; word-break: break-all;">
                                                            <%--20220217 MiMi Updated Style --%>
                                                            <asp:Label ID="lblsToukuisaki" runat="server" Text='<%# Eval("sTOKUISAKI1","{0}") %>' CommandArgument='<%# Container.DataItemIndex %>'></asp:Label>
                                                        </div>
                                                    </ItemTemplate>
                                                    <HeaderStyle Wrap="False" CssClass=" JC18HeaderCol" BorderColor="White" BorderWidth="2px"></HeaderStyle>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="営業担当者" SortExpression="sTANTOUSHA">
                                                    <ItemTemplate>
                                                        <div class="grip" style="text-align: left; padding-right: 4px; overflow: hidden; display: -webkit-box; -webkit-line-clamp: 1; -webkit-box-orient: vertical; word-break: break-all;">
                                                            <%--20220217 MiMi Updated Style --%>
                                                            <asp:Label ID="lblEigyotantaou" runat="server" Text='<%# Eval("sTANTOUSHA","{0}") %>' CommandArgument='<%# Container.DataItemIndex %>'></asp:Label>
                                                        </div>
                                                    </ItemTemplate>
                                                    <HeaderStyle Wrap="False" CssClass="JC18HeaderCol" BorderColor="White" BorderWidth="2px"></HeaderStyle>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="電話" SortExpression="sTEL" Visible="false">
                                                    <ItemTemplate>
                                                        <div class="grip" style="text-align: left; padding-right: 4px; overflow: hidden; display: -webkit-box; -webkit-line-clamp: 1; -webkit-box-orient: vertical; word-break: break-all;">
                                                            <%--20220217 MiMi Updated Style --%>
                                                            <asp:Label ID="lblTel" runat="server" Text='<%# Eval("sTEL","{0}") %>' CommandArgument='<%# Container.DataItemIndex %>'></asp:Label>
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="FAX" SortExpression="sFAX" Visible="false">
                                                    <ItemTemplate>
                                                        <div class="grip" style="text-align: left; padding-right: 4px; overflow: hidden; display: -webkit-box; -webkit-line-clamp: 1; -webkit-box-orient: vertical; word-break: break-all;">
                                                            <%--20220217 MiMi Updated Style --%>
                                                            <asp:Label ID="lblFax" runat="server" Text='<%# Eval("sFAX","{0}") %>' CommandArgument='<%# Container.DataItemIndex %>'></asp:Label>
                                                        </div>
                                                    </ItemTemplate>

                                                    <HeaderStyle Wrap="False" CssClass="JC18HeaderCol"></HeaderStyle>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="郵便番号" SortExpression="cYUUBIN" Visible="false">
                                                    <ItemTemplate>
                                                        <div class="grip" style="text-align: left; padding-right: 4px; overflow: hidden; display: -webkit-box; -webkit-line-clamp: 1; -webkit-box-orient: vertical; word-break: break-all;">
                                                            <%--20220217 MiMi Updated Style --%>
                                                            <asp:Label ID="lblYubinbago" runat="server" Text='<%# Eval("cYUUBIN","{0}") %>' CommandArgument='<%# Container.DataItemIndex %>'></asp:Label>
                                                        </div>
                                                    </ItemTemplate>

                                                    <HeaderStyle Wrap="False" CssClass="JC18HeaderCol"></HeaderStyle>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="業種" SortExpression="sGYOUSYU">
                                                    <ItemTemplate>
                                                        <div class="grip" style="text-align: left; padding-right: 4px; overflow: hidden; display: -webkit-box; -webkit-line-clamp: 1; -webkit-box-orient: vertical; word-break: break-all;">
                                                            <%--20220217 MiMi Updated Style --%>
                                                            <asp:Label ID="lblGyoShu" runat="server" Text='<%# Eval("sGYOUSYU","{0}") %>' CommandArgument='<%# Container.DataItemIndex %>'></asp:Label>
                                                        </div>
                                                    </ItemTemplate>
                                                    <HeaderStyle Wrap="False" CssClass="JC18HeaderCol" BorderColor="White" BorderWidth="2px"></HeaderStyle>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="種別" SortExpression="sSYUBETU">
                                                    <ItemTemplate>
                                                        <div class="grip" style="text-align: left; padding-right: 4px; overflow: hidden; display: -webkit-box; -webkit-line-clamp: 1; -webkit-box-orient: vertical; word-break: break-all;">
                                                            <%--20220217 MiMi Updated Style --%>
                                                            <asp:Label ID="lblShubetsu" runat="server" Text='<%# Eval("sSYUBETU","{0}") %>' CommandArgument='<%# Container.DataItemIndex %>'></asp:Label>
                                                        </div>
                                                    </ItemTemplate>
                                                    <HeaderStyle Wrap="False" CssClass="JC18HeaderCol" BorderColor="White" BorderWidth="2px"></HeaderStyle>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="掛率" SortExpression="nNEBIKIRITSU">
                                                    <ItemTemplate>
                                                        <div class="grip" style="text-align: left; overflow: hidden; display: -webkit-box; -webkit-line-clamp: 1; -webkit-box-orient: vertical; word-break: break-all;">
                                                            <%--20220217 MiMi Updated Style --%>
                                                            <asp:Label ID="lblRitu" runat="server" Text='<%# Eval("nNEBIKIRITSU","{0}") %>' CommandArgument='<%# Container.DataItemIndex %>'></asp:Label>
                                                        </div>
                                                    </ItemTemplate>
                                                    <HeaderStyle Wrap="False" CssClass="JC18HeaderCol" BorderColor="White" BorderWidth="2px"></HeaderStyle>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="住所" SortExpression="sJUUSHO1">
                                                    <ItemTemplate>
                                                        <div class="grip" style="text-align: left; overflow: hidden; display: -webkit-box; -webkit-line-clamp: 1; -webkit-box-orient: vertical; word-break: break-all;">
                                                            <%--20220217 MiMi Updated Style --%>
                                                            <asp:Label ID="lblJuusho" runat="server" Text='<%# Eval("sJUUSHO1","{0}") %>' CommandArgument='<%# Container.DataItemIndex %>'></asp:Label>
                                                        </div>
                                                    </ItemTemplate>
                                                    <HeaderStyle Wrap="False" CssClass="JC18HeaderCol" BorderColor="white" BorderWidth="0px"></HeaderStyle>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                        <div style="width: 100%; margin-top: 10px;" align="center">
                                            <asp:Label ID="lblDataNai" runat="server" Style="font-size: 14px; font-weight: normal;" Text="該当するデータがありません"></asp:Label>
                                        </div>
                                    <%--</asp:Panel>--%>
                                    <%--</div>--%>
                                </div>
                            </div>
                        </div>
                        <div class="text-center" style="height: 75px; background: #D7DBDD;">
                            <%--<asp:Button ID="btncancel" runat="server" Text="キャンセル" type="JC23button " CssClass="btn text-primary font btn-sm btn btn-outline-primary " Style="background-color: white; margin-top: 20px;" Width="99px" Height="31" OnClick="btncancel_Click" OnClientClick="document.forms[0].target = '_self';"/>--%>
                            <asp:Button ID="btnKyotenlistCancel" runat="server" Text="キャンセル" CssClass="btn text-primary font btn-sm btn btn-outline-primary" Style="width: auto !important; background-color: white; margin-left: 10px; border-radius: 3px; font-size: 13px; padding: 6px 12px 6px 12px; letter-spacing: 1px; margin-top: 22px;" OnClick="btncancel_Click" Height="35px" />
                        </div>
                         <asp:Button ID="BT_ColumnWidth" runat="server" Text="Button" OnClick="BT_ColumnWidth_Click" style="display:none;" />

                        <asp:HiddenField ID="hdnHome" runat="server" />
                         <asp:HiddenField ID="HF_GridSize" runat="server" />
                         <asp:HiddenField ID="HF_Grid" runat="server" />
                    </div>
                </div>
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
                        <iframe id="ifShinkiPopup" runat="server" scrolling="yes" style="height: 100vh; width: 100vw;" ></iframe>
                        <asp:Button ID="btn_Close" runat="server" Text="Button" CssClass="DisplayNone" OnClick="btn_Close_Click" onClientClick="MasterOverflow()" />
                        <asp:Button ID="btnToLogin" runat="server" Text="Button" Style="display: none" OnClick="btnToLogin_Click" onClientClick="MasterOverflow()" />
                          <asp:Button ID="btn_CloseShinkiTokui" runat="server" Text="Button" Style="display: none" OnClick="btn_CloseShinkiTokui_Click" onClientClick="MasterOverflow()" />
                    </asp:Panel>
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>
    </form>

    <%--20220217 MiMi Added Start --%>
    <%--<script type="text/javascript">
        $(function () {
            if ($.cookie('colWidthTokuisakiKensaku') != null) {
                var columns = $.cookie('colWidthTokuisakiKensaku').split(',');
                var i = 0;
                $('.GridViewStyle th').each(function () {
                    $(this).width(columns[i++]);
                });
            }
            else {
                var columns = [70, 335, 125, 125, 100, 100, 214];
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
            //var date = new Date();
            //date.setTime(date.getTime() + (60 * 20000));
            columns.each(function () {
                //msg += $(this).width() + ",";
                 msg += $(this).outerWidth() + ",";
            })
            //$.cookie("colWidthTokuisakiKensaku", msg, { expires: date }); // expires after 20 minutes
            document.getElementById("<%=HF_GridSize.ClientID%>").value = msg;
            document.getElementById("<%=BT_ColumnWidth.ClientID%>").click();
        };

        var prm = Sys.WebForms.PageRequestManager.getInstance();
        if (prm != null) {
            prm.add_endRequest(function (sender, e) {
                if (sender._postBackSettings.panelsToUpdate != null) {
                    if ($.cookie('colWidthTokuisakiKensaku') != null) {
                        var columns = $.cookie('colWidthTokuisakiKensaku').split(',');
                        var i = 0;
                        $('.GridViewStyle th').each(function () {
                            $(this).width(columns[i++]);
                        });
                    }
                    else {
                        var columns = [70, 335, 125, 125, 100, 100, 214];
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
<script type="text/javascript">
     $(function (){

         $(".GridViewStyle").colResizable({
             liveDrag: true,
             resizeMode: 'overflow',
             postbackSafe: true,
             partialRefresh: true,
             flush: true,
             disabledColumns:['10'],
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
                            gripInnerHtml: "<div class='grip'></div>",
                            draggingClass: "dragging",
                            onResize: onSampleResized
                        });
                        
                    };
                });
                }
</script>

    <%--20220217 MiMi Added End --%>
</body>
</html>
