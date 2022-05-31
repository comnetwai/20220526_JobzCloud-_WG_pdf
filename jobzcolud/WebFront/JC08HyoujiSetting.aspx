<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="JC08HyoujiSetting.aspx.cs"
    Inherits="cloudjobz_n.JC08HyoujiSetting" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="../Style/StyleJC.css" rel="stylesheet" />
    <link href="../Content/bootstrap.min.css" rel="stylesheet" />
    <script src="../Scripts/bootstrap.min.js"></script>
    <%--<link href="Content/bootstrap.css" rel="stylesheet" />--%>
    <link href="Content/bootstrap.min.css" rel="stylesheet" />
    <asp:PlaceHolder runat="server">
        <%: Scripts.Render("~/bundles/modernizr") %>
        <%: Styles.Render("~/style/StyleBundle1") %>
        <%: Scripts.Render("~/scripts/ScriptBundle1") %>
    </asp:PlaceHolder>
    <webopt:BundleReference runat="server" Path="~/Content/bootstrap" />

    <script type="text/javascript">

        function refreshParent() {//20211027

            //window.parent.location = "JC07Home.aspx"; // The url of the parent page.
            window.parent.location.href = window.parent.location.href;
        }
    </script>
    <script src="Scripts/jquery-3.3.1.js"></script>
    <script type="text/javascript">
        function HideIframe() {
            $('[id*=mpeHyoujiSetPopUp]', window.parent.document).hide();
            $('[id*=pnlHyoujiSetPopUpScroll]', window.parent.document).hide();
        }

    </script>
    
</head>
<body class="fontcss" style="overflow-x: hidden; background-color: #eee">

    <div class="J06Div_hyouji RadiusIframe_hyouji ">
        <form id="form1" runat="server">
            <asp:ScriptManager ID="ScriptManager1" EnablePageMethods="True" runat="server">
                <Scripts>
                    <asp:ScriptReference Name="jquery" />
                    <asp:ScriptReference Name="bootstrap" />
                    <asp:ScriptReference Path="../Scripts/Common/FixFocus.js" />
                </Scripts>
            </asp:ScriptManager>
            <asp:PlaceHolder runat="server">
                <%: Scripts.Render("~/bundles/jqueryui") %>
            </asp:PlaceHolder>

            <asp:UpdatePanel runat="server" ID="updHyoujiSetting" UpdateMode="Conditional" ChildrenAsTriggers="False">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="BtnDefault" EventName="Click" />
                </Triggers>
                <ContentTemplate>

                    <div style="background: white!important">
                        <asp:HiddenField ID="hdnHome" runat="server" />
                        <asp:HiddenField ID="gvName" runat="server" />
                        <asp:Button ID="BtnDefault" runat="server" Text="BtnDefault" Style="display: none;" Width="99px" OnClick="BtnDefault_Click" />
                        <input type="hidden" id="hidCandidateId" runat="server" />
                        <div class="row" style="display: flex; justify-content: center; align-items: center">
                            <asp:Label ID="lbl_header" runat="server" CssClass="titleStyle fw-bold txt-style" />
                            <asp:Button ID="btnHeaderCross" runat="server" Text="✕" CssClass="PopupCloseBtn mr-n3 JC08btnHeaderCross" OnClick="btnCancel_Click" />
                        </div>
                        <div class="titleLine"></div>
                        <div class="row" style="display: flex; justify-content: center">
                            <div class="div_data" id="div_uriage">
                                <asp:GridView ID="gvID" runat="server" DataKeyNames="cHYOUJI" OnRowDataBound="OnRowDataBound"
                                    AutoGenerateColumns="false" CellSpacing="15" GridLines="None" CssClass="JC08sonotapop">
                                    <RowStyle Height="40px" />
                                    <Columns>
                                        <asp:TemplateField ItemStyle-Width="30">
                                            <ItemTemplate>
                                                <input name="cHYOUJI" value='<%# Eval("cHYOUJI") %>' />
                                            </ItemTemplate>
                                            <ItemStyle CssClass="Hidecss" />
                                        </asp:TemplateField>

                                        <asp:BoundField DataField="sHYOUJI" ItemStyle-Width="100"></asp:BoundField>

                                        <asp:TemplateField ItemStyle-Width="100">

                                            <ItemTemplate>
                                                <asp:LinkButton ID="lkbtnDisplay" runat="server" Text='<%# Bind("dis1") %>' CommandName="Display" CommandArgument="<%# Container.DataItemIndex %>"></asp:LinkButton><%--lkbtnDisplay_ug--%>
                                                <%--ForeColor='<%# Convert.ToString(Eval("fHYOUJI")) == "0" ? System.Drawing.Color.Blue: System.Drawing.Color.Gray%>'--%>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField ItemStyle-Width="100">

                                            <ItemTemplate>
                                                <asp:LinkButton ID="lkbtnNotDisplay" runat="server" Text='<%# Bind("dis2") %>' CommandName="NotDisplay"
                                                    CommandArgument="<%# Container.DataItemIndex %>"></asp:LinkButton>
                                                <%--ForeColor='<%# Convert.ToString(Eval("fHYOUJI")) == "1" ? System.Drawing.Color.Blue: System.Drawing.Color.Gray%>'--%>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="center" />
                                        </asp:TemplateField>

                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <%--<span id="Span1" runat="server">Span1 content</span>--%>
                                                <%--<span id="myDrag" runat="server" >&#9776;</span>--%>
                                                <%--<asp:Label ID="meeterNo" class="btnDrag" runat="server" Text="34001" />--%>

                                                <%--                                                <asp:Panel ID="ActivityPanel" runat="server">--%>
                                                <%--<asp:Panel ID="myDrag" runat="server"><span>&#9776;</span></asp:Panel>--%>
                                                <%-- <span runat="server" class="btnDrag" id="myDrag">&#9776;</span>
                                                <span runat='server' id='myId' class='myClass'>some text</span>--%>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="center" />
                                            <ItemStyle Width="50" />
                                        </asp:TemplateField>

                                        <asp:TemplateField ItemStyle-Width="30">
                                            <ItemTemplate>
                                                <asp:TextBox Name="fHYOUJI" ID="fHYOUJI" runat="server" Text='<%# Eval("fHYOUJI") %>'> </asp:TextBox>
                                                <input type="hidden" name="fHYOUJI" value='<%# Eval("fHYOUJI") %>' />
                                            </ItemTemplate>
                                            <ItemStyle CssClass="Hidecss" />
                                        </asp:TemplateField>

                                    </Columns>
                                    <RowStyle CssClass="gridViewRow" />
                                    <AlternatingRowStyle CssClass="gridViewRow" />
                                </asp:GridView>
                            </div>

                        </div>

                        <div class="row justify-content-center align-items-center" style="background-color: #eee; padding: 20px 0">
                            <asp:Button ID="Button1" runat="server" CssClass="BlueBackgroundButton" Text="保存"
                                OnClientClick="javascript:disabledTextChange(this);" Width="99px" OnClick="btnSave_Click" />
                            <asp:Button ID="Button2" runat="server" Text="キャンセル" CssClass="JC10CancelBtn " Width="99px" OnClick="btnCancel_Click" />

                        </div>

                    </div>

                </ContentTemplate>
            </asp:UpdatePanel>
        </form>
    </div>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <link rel="stylesheet" href="https://ajax.googleapis.com/ajax/libs/jqueryui/1.8.24/themes/smoothness/jquery-ui.css" />
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jqueryui/1.8.24/jquery-ui.min.js"></script>
    <script type="text/javascript">

        $(function () {
            let gvname = $('#gvName').val();
            //alert($('#gvName').val());
            if (gvname == "mitsumorisyosai") {
                $("[Id*=gvID]").sortable({
                    items: 'tr:not(.disable-drag)',
                    cursor: 'pointer',
                    axis: 'y',
                    dropOnEmpty: false,
                    start: function (e, ui) {
                        ui.item.addClass("selected");
                    },
                    stop: function (e, ui) {
                        ui.item.removeClass("selected");
                    },
                    receive: function (e, ui) {
                        $(this).find("tbody").append(ui.item);
                    },


                });
            }
            else {
                $("[Id*=gvID]").sortable({
                    items: 'tr:not(tr:first-child)',
                    cursor: 'pointer',
                    axis: 'y',
                    dropOnEmpty: false,
                    start: function (e, ui) {
                        ui.item.addClass("selected");
                    },
                    stop: function (e, ui) {
                        ui.item.removeClass("selected");
                    },
                    receive: function (e, ui) {
                        $(this).find("tbody").append(ui.item);
                    },


                });
            }
            $("[id*=gvID] td").click(function () {
                var txt = $(this).text();
                DisplayDetails($(this).closest("tr"), txt);
                return false;
            });

            function DisplayDetails(row, txt) {
                var name = row.find('td:eq(1)').text();
                var equal = false;
                txt = txt.replace(/\s/g, '')
                //let name = $('#gvName').val();
                //alert(gvname);
                if (gvname == "uriage") {
                    if (name == "売上コード") {
                        equal = true;
                    }
                }
                else {
                    if (name == "商品名" || name == "数量" || name == "単位" || name == "標準単価" || name == "合計金額" || name == "物件名" || name == "得意先名" || name == "見積名") {
                        if (name == "合計金額") {
                            if (gvname == "mitsumorisyosai" || gvname=="mitsumori") {
                                equal = false; /*20220428 Chaw Added*/
                            }
                            else {
                                equal = true;/*20220428 Chaw Added*/
                            }
                        }
                        else {
                            equal = true;
                        }
                    }
                }
                if (txt == "表示") {
                    if (equal == true) {
                        row.find('td:eq(2) a').prop('disabled', true);
                        row.find('td:eq(3) a').prop('disabled', true);
                    }
                    else {
                        row.find('td:eq(5) input').val("0");
                        row.find('td:eq(2) a').css('color', 'blue');
                        row.find('td:eq(3) a').css('color', 'gray');
                    }
                }
                else {
                    if (equal == true) {
                        row.find('td:eq(2) a').prop('disabled', true);
                        row.find('td:eq(3) a').prop('disabled', true);
                        row.find('td:eq(3) a').css('color', '#dddddd');
                    }
                    else {
                        //if (name == "売上コード") {
                        //    row.find('td:eq(3) a').prop('disabled', true);
                        //}
                        //else {
                        row.find('td:eq(5) input').val("1");
                        row.find('td:eq(2) a').css('color', 'gray');
                        row.find('td:eq(3) a').css('color', 'blue');
                        //}
                    }
                }
            }
        });

    </script>


</body>
</html>
