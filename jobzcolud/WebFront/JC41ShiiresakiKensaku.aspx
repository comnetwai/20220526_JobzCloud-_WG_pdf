<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="JC41ShiiresakiKensaku.aspx.cs" Inherits="jobzcolud.WebFront.JC41HyoujunShiiresakiKensaku" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

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
    <link href="../Style/cloudflare-jquery-ui.min.css" rel="stylesheet" />
    <script src="https://code.jquery.com/jquery-3.2.1.min.js"></script>
    <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.min.js"></script>
    <script src="../Scripts/cloudflare-jquery-ui-i18n.min.js"></script>
    <style>
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

        .grid_header {
            overflow: auto;
            white-space: nowrap;
        }

        .GridViewStyle {
            z-index: 3 !important;
            overflow: unset !important;
            overflow-x: auto !important;
        }

            .GridViewStyle tr td {
                z-index: 3 !important;
                overflow: unset !important;
                overflow-x: auto !important;
            }

            .GridViewStyle tr th {
                z-index: 3 !important;
                overflow: unset !important;
                overflow-x: auto !important;
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
    </style>
     <script type="text/javascript">
         function ConfirmationBox(username) {
             var result = confirm('Are you sure you want to delete ' + username + ' Details');
             if (result) {
                 return true;
             }
             else {
                 return false;
             }
         }
    </script>
     
</head>
<body class="bg-transparent">
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager" EnablePageMethods="True" runat="server">
            <Scripts>
                <asp:ScriptReference Name="jquery" />
                <asp:ScriptReference Name="bootstrap" />
                <asp:ScriptReference Path="../Scripts/Common/FixFocus.js" />
                <asp:ScriptReference Path="../Scripts/Common/Common.js" />
                <asp:ScriptReference Path="../Scripts/colResizable-1.6.min.js" />
                <asp:ScriptReference Path="../Scripts/cookie.js" />
            </Scripts>
        </asp:ScriptManager>
         <asp:PlaceHolder runat="server">
                <%: Scripts.Render("~/bundles/jqueryui") %>
            </asp:PlaceHolder>
        <asp:UpdatePanel ID="Updshire" runat="server" UpdateMode="Conditional" RenderMode="Inline">
            <ContentTemplate>
                <div style="max-width: 1160px; background-color: white; margin-left: auto; margin-right: auto; padding: 0; left: 50%; top: 50%; position: absolute; transform: translate(-50%, -50%);">
                    <div style="margin-left: auto; margin-right: auto;">
                        <asp:Label ID="lblHeader" runat="server" Text="仕入先を選択" CssClass="TitleLabel" Style="text-align: left;"></asp:Label>
                        <asp:Button ID="btnShireHeaderCross" runat="server" Text="✕" CssClass="PopupCloseBtn" OnClick="btn_Close_Click" />
                    </div>
                    <div class="Borderline"></div>
                    <div style="margin: 15px 20px 20px 20px;">
                        <table style="width: 100% !important; margin-bottom: 20px;">
                            <tr>
                                <td>
                                    <asp:TextBox runat="server" ID="txtCode" MaxLength="20" placeholder="コード、名前で仕入先できます。" CssClass="form-control TextboxStyle JC18TextBox " onfocus="this.setSelectionRange(this.value.length, this.value.length);" AutoPostBack="true" Width="400px" OnTextChanged="txtCode_TextChanged" TextMode="Search"></asp:TextBox>
                                </td>
                                <td align="right">
                                    <asp:Button ID="btnCreate" runat="server" Text="仕入先を新規作成" CssClass="BlueBackgroundButton JC10SaveBtn" OnClick="btnCreate_Click" Style="margin: 0px !important;" />
                                </td>
                            </tr>
                        </table>
                        <div style="overflow-x: auto; width: 100% !important;">
                                <div style="background-color: white; width: auto;max-height: 424px; overflow-y: auto; overflow-x: auto;" display: inline-block !important;">
                            <asp:GridView ID="dgShiire" runat="server" BorderColor="Transparent" AutoGenerateColumns="false"  HtmlEncode="false"
                                ShowHeader="true" ShowHeaderWhenEmpty="true" CellPadding="0" AllowSorting="True" CssClass="RowHover GridViewStyle" DataKeyNames="cSHIIRESAKI" OnSorting="dgShiire_Sorting1" OnRowCreated="dgShiire_RowCreated" OnRowDeleting="dgShiire_RowDeleting" OnPreRender="dgShiire_PreRender" OnRowDataBound="dgShiire_RowDataBound" OnSelectedIndexChanged="dgShiire_SelectedIndexChanged">
                                <HeaderStyle Height="45px" BackColor="#F2F2F2" ForeColor="Black" CssClass="grid_header" />
                                <RowStyle CssClass="JC12GridItem" Height="43px" />
                                <SelectedRowStyle BackColor="#EBEBF5" />
                                <Columns>
                                    <asp:TemplateField HeaderText="仕入先コード" SortExpression="cSHIIRESAKI">
                                        <ItemTemplate>
                                            <div class="grip" style="text-align: left; padding-right: 4px; overflow: hidden; display: -webkit-box; -webkit-line-clamp: 1; -webkit-box-orient: vertical; word-break: break-all;">
                                                <asp:LinkButton ID="lblcTantousha" runat="server" CommandArgument='<%# Eval("cSHIIRESAKI","{0}") %>' Text='<%# Bind("cSHIIRESAKI","{0}") %>' Font-Underline="false" Font-Size="13px" style="display: none;" OnClick="lblcTantousha_Click"></asp:LinkButton>
                                                 <asp:Label runat="server" ID="lblcT" Text='<%# Eval("cSHIIRESAKI","{0}") %>' Font-Underline="false" Font-Size="13px"  CommandArgument='<%# Container.DataItemIndex %>'  />
                                            </div>
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="JC41CodeHeaderCol" BorderColor="White" BorderWidth="2px" Height="43px"></HeaderStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="仕入先名" SortExpression="sSHIIRESAKI">
                                        <ItemTemplate>
                                            <div class="grip" style="text-align: left; padding-right: 4px; overflow: hidden; display: -webkit-box; -webkit-line-clamp: 1; -webkit-box-orient: vertical; word-break: break-all;">
                                                <asp:Label ID="lblsTantousha" runat="server" Text='<%# Eval("sSHIIRESAKI","{0}") %>' CommandArgument='<%# Container.DataItemIndex %>'></asp:Label>
                                            </div>
                                        </ItemTemplate>
                                        <HeaderStyle CssClass=" JC41HeaderCol" BorderColor="White" BorderWidth="2px"></HeaderStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="仕入先名カナ" SortExpression="skSHIIRESAKI">
                                        <ItemTemplate>
                                            <div class="grip" style="text-align: left; padding-right: 4px; overflow: hidden; display: -webkit-box; -webkit-line-clamp: 1; -webkit-box-orient: vertical; word-break: break-all;">
                                                <asp:Label ID="lblsTantoushakana" runat="server" Text='<%# Eval("skSHIIRESAKI","{0}") %>' CommandArgument='<%# Container.DataItemIndex %>'></asp:Label>
                                            </div>
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="JC41HeaderCol" BorderColor="White" BorderWidth="2px"></HeaderStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="仕入先名略称" SortExpression="sSHIIRESAKI_R">
                                        <ItemTemplate>
                                            <div class="grip" style="text-align: left; padding-right: 4px; overflow: hidden; display: -webkit-box; -webkit-line-clamp: 1; -webkit-box-orient: vertical; word-break: break-all;">
                                                <asp:Label ID="lblsupTantousha" runat="server" Text='<%# Eval("sSHIIRESAKI_R","{0}") %>' CommandArgument='<%# Container.DataItemIndex %>'></asp:Label>
                                            </div>
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="JC41HeaderCol" BorderColor="White" BorderWidth="2px"></HeaderStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="仕入先名略称カナ" SortExpression="skSHIIRESAKI_R">
                                        <ItemTemplate>
                                            <div class="grip" style="text-align: left; padding-right: 4px; overflow: hidden; display: -webkit-box; -webkit-line-clamp: 1; -webkit-box-orient: vertical; word-break: break-all;">
                                                <asp:Label ID="lblsupTantoushakana" runat="server" Text='<%# Eval("skSHIIRESAKI_R","{0}") %>' CommandArgument='<%# Container.DataItemIndex %>'></asp:Label>
                                            </div>
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="JC41HeaderCol" BorderColor="White" BorderWidth="2px"></HeaderStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="郵便番号" SortExpression="cYUUBIN">
                                        <ItemTemplate>
                                            <div class="grip" style="text-align: left; padding-right: 4px; overflow: hidden; display: -webkit-box; -webkit-line-clamp: 1; -webkit-box-orient: vertical; word-break: break-all;">
                                                <asp:Label ID="lblpostTantousha" runat="server" Text='<%# Eval("cYUUBIN","{0}") %>' CommandArgument='<%# Container.DataItemIndex %>'></asp:Label>
                                            </div>
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="JC41HeaderCol" BorderColor="White" BorderWidth="2px"></HeaderStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="電話" SortExpression="sTEL">
                                        <ItemTemplate>
                                            <div class="grip" style="text-align: left; padding-right: 4px; overflow: hidden; display: -webkit-box; -webkit-line-clamp: 1; -webkit-box-orient: vertical; word-break: break-all;">
                                                <asp:Label ID="lblPostcode" runat="server" Text='<%# Eval("sTEL","{0}") %>' CommandArgument='<%# Container.DataItemIndex %>'></asp:Label>
                                            </div>
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="JC41HeaderCol" BorderColor="White" BorderWidth="2px"></HeaderStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="FAX" SortExpression="sFAX">
                                        <ItemTemplate>
                                            <div class="grip" style="text-align: left; padding-right: 4px; overflow: hidden; display: -webkit-box; -webkit-line-clamp: 1; -webkit-box-orient: vertical; word-break: break-all;">
                                                <asp:Label ID="lblFax" runat="server" Text='<%# Eval("sFAX","{0}") %>' CommandArgument='<%# Container.DataItemIndex %>'></asp:Label>
                                            </div>
                                        </ItemTemplate>

                                        <HeaderStyle CssClass="JC41HeaderCol" BorderColor="White" BorderWidth="2px"></HeaderStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="住所1" SortExpression="sJUUSHO1">
                                        <ItemTemplate>
                                            <div class="grip" style="text-align: left; padding-right: 4px; overflow: hidden; display: -webkit-box; -webkit-line-clamp: 1; -webkit-box-orient: vertical; word-break: break-all;">
                                                <asp:Label ID="lblsTe" runat="server" Text='<%# Eval("sJUUSHO1","{0}") %>' CommandArgument='<%# Container.DataItemIndex %>'></asp:Label>
                                            </div>
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="JC41HeaderCol" BorderColor="White" BorderWidth="2px"></HeaderStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="住所2" SortExpression="sJUUSHO2">
                                        <ItemTemplate>
                                            <div class="grip" style="text-align: left; padding-right: 4px; overflow: hidden; display: -webkit-box; -webkit-line-clamp: 1; -webkit-box-orient: vertical; word-break: break-all;">
                                                <asp:Label ID="lblsTe1" runat="server" Text='<%# Eval("sJUUSHO2","{0}") %>' CommandArgument='<%# Container.DataItemIndex %>'></asp:Label>
                                            </div>
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="JC41HeaderCol" BorderColor="White" BorderWidth="2px"></HeaderStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <div class="grip" style="text-align: left; width: 25px; padding-right: 4px; overflow: hidden; display: -webkit-box; -webkit-line-clamp: 1; -webkit-box-orient: vertical; word-break: break-all;">
                                                <cc1:HoverMenuExtender ID="HoverMenuExtender2" runat="server" PopupControlID="PopupMenu1"
                                                    TargetControlID="Panel2" PopupPosition="Bottom">
                                                </cc1:HoverMenuExtender>
                                                <asp:Panel ID="PopupMenu1" runat="server" CssClass="modalPopup dropdown-menu fontcss " aria-labelledby="dropdownMenuButton" Style="display: none; min-width: 1rem; width: 6rem; margin-left: 5px;">
                                                    <asp:LinkButton ID="lnkbtnShiireEdit" class="dropdown-item" runat="server" Text='編集' Style="margin-right: 10px; font-size:13px;" OnClick="lnkbtnShiireEdit_Click"></asp:LinkButton>
                                                    <asp:LinkButton ID="lnkbtnShiireDelete_Click" class="dropdown-item" runat="server" Text='削除' Style="margin-right: 10px;font-size:13px;" OnClick="lnkbtnShiireDelete_Click_Click"></asp:LinkButton>
                                                </asp:Panel>
                                                <asp:Panel ID="Panel2" runat="server" CssClass="modalPopup" Style="padding-left: 5px; width: 20px;">
                                                    <button class="btn dropdown-toggle" type="button" id="dropdownMenuButton" data-toggle="dropdown"
                                                        aria-haspopup="true" aria-expanded="false" style="border: 1px solid gainsboro; width: 20px; height: 20px; padding: 0px 3px 0px 1px; margin: 0;">
                                                    </button>
                                                </asp:Panel>
                                            </div>
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="JC41HeaderCol" BorderColor="White" BorderWidth="2px"></HeaderStyle>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                                      <div style="width: 100%; margin-top: 10px;" align="center">
                                            <asp:Label ID="lblDataNai" runat="server" Style="font-size: 14px; font-weight: normal;" Text="該当するデータがありません"></asp:Label>
                                        </div>
                       </div>
                    </div>
                    </div>
                    <div class="text-center" style="height: 65px; background: #D7DBDD;">
                        <asp:Button ID="btncancel" runat="server" Text="キャンセル" CssClass="btn text-primary font btn-sm btn btn-outline-primary" Style="width: auto !important; background-color: white; margin-left: 10px; border-radius: 3px; font-size: 13px; padding: 6px 12px 6px 12px; letter-spacing: 1px; margin-top: 15px;" OnClick="btn_Close_Click" />
                    </div>
                    <asp:Button ID="btnDeleteShiire" runat="server" Text="" class="JC09GrayButton DisplayNone" Width="100px" Height="36px" OnClick="btnDeleteShiire_Click"/>
                     <asp:Button ID="BT_ColumnWidth" runat="server" Text="Button" OnClick="BT_ColumnWidth_Click" style="display:none;" />

                        <asp:HiddenField ID="hdnHome" runat="server" />
                         <asp:HiddenField ID="HF_GridSize" runat="server" />
                         <asp:HiddenField ID="HF_Grid" runat="server" />
                    <asp:HiddenField ID="HF_fBtn" runat="server" />
                    <asp:HiddenField ID="HF_isChange" runat="server" />
                    <asp:HiddenField ID="HF_cTantousha" runat="server" />
                    </div>
            </ContentTemplate>
        </asp:UpdatePanel>

         <asp:UpdatePanel ID="updShinkiPopup" runat="server" UpdateMode="Conditional" RenderMode="Inline">
            <ContentTemplate>
                <asp:Button ID="btnShinkiPopup" runat="server" Text="Button" Style="display: none" />
                <asp:ModalPopupExtender ID="mpeShinkiPopup" runat="server" TargetControlID="btnShinkiPopup"
                    PopupControlID="pnlShinkiPopupScroll" BackgroundCssClass="PopupModalBackground" BehaviorID="pnlShinkiPopupScroll"
                    RepositionMode="RepositionOnWindowResize">
                </asp:ModalPopupExtender>
                <asp:Panel ID="pnlShinkiPopupScroll" runat="server" Style="display: none; height: 100%; overflow: hidden;" CssClass="PopupScrollDiv">
                    <asp:Panel ID="pnlShinkiPopup" runat="server">
                        <iframe id="ifShinkiPopup" runat="server" scrolling="yes" style="height: 100vh; width: 100vw;"></iframe>
                        <asp:Button ID="btnClose" runat="server" Text="Button" CssClass="DisplayNone" OnClick="btnClose_Click" />
                        <asp:Button ID="btnToLogin" runat="server" Text="Button" Style="display: none" OnClick="btnToLogin_Click" />
                    </asp:Panel>
                </asp:Panel>
               
            </ContentTemplate>
        </asp:UpdatePanel>
       
         <!--ポップアップ画面-->
                <asp:UpdatePanel ID="updShouhinPopup" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:Button ID="btnShouhinPopup" runat="server" Text="Button" Style="display: none" />
                <asp:ModalPopupExtender ID="mpeShouhinPopup" runat="server" TargetControlID="btnShouhinPopup"
                    PopupControlID="pnlShouhinPopupScroll" BackgroundCssClass="PopupModalBackground" BehaviorID="pnlShouhinPopupScroll"
                    RepositionMode="RepositionOnWindowResize">
                </asp:ModalPopupExtender>
                <asp:Panel ID="pnlShouhinPopupScroll" runat="server" Style="display: none; height: 100%; overflow: hidden;" CssClass="PopupScrollDiv">
                    <asp:Panel ID="pnlShouhinPopup" runat="server">
                        <iframe id="ifShouhinPopup" runat="server" scrolling="yes" style="height: 100vh; width: 100vw;"></iframe>
                     <asp:Button ID="btn_Close" runat="server" Text="Button" CssClass="DisplayNone" OnClick="btn_Close_Click" />
                        <asp:Button ID="btnShiresaki_Close" runat="server" Text="Button" CssClass="DisplayNone" OnClick="btnShiresaki_Close_Click" />
                    </asp:Panel>
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>
    </form>
    <script type="text/javascript">
        $(function () {

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

        });

        var onSampleResized = function (e) {
            var columns = $(e.currentTarget).find("th");
            var msg = "";
            columns.each(function () {
                msg += $(this).width() + ",";
            })
            document.getElementById("<%=HF_Grid.ClientID%>").value = $(e.currentTarget).width();
            document.getElementById("<%=HF_GridSize.ClientID%>").value = msg;
            document.getElementById("<%=BT_ColumnWidth.ClientID%>").click();
        };

        var prm = Sys.WebForms.PageRequestManager.getInstance();
        if (prm != null) {
            prm.add_endRequest(function (sender, e) {
                if (sender._postBackSettings.panelsToUpdate != null) {
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
</body>
</html>
