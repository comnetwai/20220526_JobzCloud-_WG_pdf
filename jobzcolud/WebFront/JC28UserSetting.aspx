<%@ Page Language="C#" MasterPageFile="~/WebFront/JC99NavBar.Master" AutoEventWireup="true" CodeBehind="JC28UserSetting.aspx.cs" Inherits="jobzcolud.WebFront.JC28UserSetting" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%--20211116 Added By Eaindray--%>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <!DOCTYPE html>

    <html>
    <head>
        <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
        <title></title>
        <link href="Content/bootstrap.css" rel="stylesheet" />
        <asp:PlaceHolder runat="server">
            <%: Scripts.Render("~/bundles/modernizr") %>
            <%: Styles.Render("~/style/StyleBundle1") %>
            <%: Scripts.Render("~/scripts/ScriptBundle1") %>
            <%: Styles.Render("~/style/UCStyleBundle") %>

        </asp:PlaceHolder>
        <webopt:BundleReference runat="server" Path="~/Content1/bootstrap" />
        <webopt:BundleReference runat="server" Path="~/Content1/css" />
        <meta name="google" content="notranslate" />
        <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=0, shrink-to-fit=no" />

        <style>
            #txtCode {
                max-width: 900px;
                min-width: 900px;
                margin-left: 40px;
                margin-top: 18px;
            }

            #btnCreate {
                margin-top: 18px;
                margin-left: 10px;
            }

            #CheckBox1 {
                margin-left: 6px;
                margin-top: -12px;
            }

            #lb_leave {
                margin-left: 5px;
                margin-top: -13px;
            }

            #updMitsumoriGrid {
                padding-bottom: 30px;
            }

            .GridPager {
                padding-top: 50px;
            }

            .JC28GridViewDiv {
                /*max-width: 1400px !important; /*20211202 Updated 1350px -> 1400px エインドリ－・プ－プゥ*/
                /*width: 1400px;*/ /*20211202 Updated 1350px -> 1400px エインドリ－・プ－プゥ*/
                */ margin-left: 4px; /*20211202 Updated 15px -> 4px エインドリ－・プ－プゥ*/
                margin-right: 4px;
                　
                /*20211202 Added by エインドリ－・プ－プゥ*/
                overflow: auto;
                overflow-y: hidden;
            }
            /*20211202 Added by エインドリ－・プ－プゥ*/
            .JC28GridTextBox {
                border-radius: 8px;
                padding: 0px;
                border: 0.5px solid rgb(155, 155, 155) !important;
                margin-right: 4px;
                margin-left: 15px;
            }

            .JC28UserGridHeaderStyle {
                text-align: left !important;
                background-color: rgb(242,242,242);
            }

            .JC28gridheader {
                padding-left: 5px;
            }

        </style>
    </head>
    <body>

        <asp:UpdatePanel ID="updHeader" runat="server" UpdateMode="Conditional" RenderMode="Inline">
            <ContentTemplate>
                <%--<asp:ScriptManager ID="ScriptManager1" EnablePageMethods="True" runat="server">--%>
                <scripts>
                        <asp:ScriptReference Name="jquery" />
                        <asp:ScriptReference Name="bootstrap" />
                        <asp:ScriptReference Path="../Scripts/Common/FixFocus.js" />
                    </scripts>
                <%--</asp:ScriptManager>--%>
                <div class="JC28TableWidth">
                    <nav id="nav3" runat="server" class="navbar-expand custom-navbar1 custom-navbar-height" style="position: absolute;">
                        <div class="collapse navbar-collapse JC09navbar JC28TableWidth" id="Div1" runat="server">
                            <label style="font-weight: bold; font-size: 14px; text-align: center; display: inline-block;">ユーザー設定</label>
                            <asp:Label ID="lblLoginUserCode" runat="server" Text="" Visible="false" />
                            <asp:Label ID="lblLoginUserName" runat="server" Text="" Visible="false" />

                        </div>
                    </nav>
                </div>
                <%--<div style="padding-top:66px; padding-bottom:10px; margin-left:auto; margin-right:auto;">--%>
                <div class="container body-content" style="margin:0px;padding:0px;background-color: #d7e4f2; padding-top: 60px; cursor: context-menu;">
                    <%--20220120 Updated エインドリ－・プ－プゥ--%>
                    <table runat="server" id="table" class="JC28TableWidth" align="center" style="cursor: context-menu;">
                        <tr>
                            <td id="td1" runat="server" style="width: 100%; height: 100%;">
                                <asp:Panel runat="server" ID="PanelSyousai" Style="background-color: white;"  CssClass="JC28TableWidth">
                                   
                                    <div style="padding-left: 15px; padding-right: 15px; padding-top: 5px; padding-bottom: 10px;">
                                        <%--20211202 Updated padding right and left 20px -> 15px エインドリ－・プ－プゥ--%>

                                        <div class="form-group row">
                                            <div class="col mt-3" style="min-width: 830px; max-width: 830px; margin-left: 30px;">
                                                <asp:TextBox runat="server" ID="txtCode" MaxLength="20" onkeyup="DeSelectText(this);" Style="min-width: 800px; max-width: 800px;" TextMode="Search"
                                                    placeholder="コード、名前、部署で検索できます。" CssClass="form-control TextboxStyle" onfocus="this.setSelectionRange(this.value.length, this.value.length);" OnTextChanged="txtCode_textChanged" AutoPostBack="true"></asp:TextBox>
                                            </div>
                                            <div style="float: right; text-align: left;" class="col mt-3 ">
                                                <asp:UpdatePanel ID="updUserAdd" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <asp:Button ID="btnCreate" runat="server" Text="ユーザーを追加" type="button " CssClass="BlueBackgroundButton" OnClick="btnCreate_Click" OnClientClick="PopupOpenButton()"/>
                                                        <%--20211116 Updated By Eaindray ( OnClick Method ) OnClientClick="target = '_blank';"--%>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </div>
                                        </div>
                                        <div class="form-group row" style="margin-left: 30px;">
                                            <asp:CheckBox ID="CheckBox1" runat="server" OnCheckedChanged="chk_taisya_CheckedChanged" AutoPostBack="true" />
                                            <asp:Label ID="lb_leave" runat="server" Text="退社のみを表示"></asp:Label>
                                            <%--20211202 Added Cssclass by エインドリ－・プ－プゥ--%>
                                        </div>
                                        <div style="width: 1310px; text-align: right; margin: 0px; padding-right: 55px;">
                                            <table style="margin-top: 7px; display: inline-block;">
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblHyojikensuu" runat="server" Text="1-30/500" Font-Size="13px" CssClass="JC12LblHyojikensuu"></asp:Label>
                                                    </td>
                                                    <td style="padding-right: 10px;">
                                                        <asp:Label ID="lblHyoujikensuuLabel" runat="server" Text="表示件数" Font-Size="13px"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <%-- <asp:UpdatePanel ID="updTantou" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>--%><%--20211202 Update CssClass JC12GridTextBox to JC28GridTextBox--%>
                                                        <asp:DropDownList ID="DDL_Hyojikensuu" runat="server" Width="80px" AutoPostBack="True" CssClass="form-control form-control JC12GridTextBox" Font-Size="13px" OnSelectedIndexChanged="DDL_Hyojikensuu_SelectedIndexChanged" Style="margin: 0px !important;">
                                                            <asp:ListItem Value="20" Selected="True">20件</asp:ListItem>
                                                            <asp:ListItem Value="30">30件</asp:ListItem>
                                                            <asp:ListItem Value="50">50件</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <%--</ContentTemplate>
                                                </asp:UpdatePanel>--%>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>

                                        <%--<div id="Div2" runat="server" class="JC12GridViewDiv" style="margin-left:5px;">--%>
                                        <div id="Div6" runat="server" class=" mt-3">
                                            <div id="Div5" runat="server" style="width: auto; margin-left: 30px; margin-right: 10px;">
                                                <%--20211202 Update class JC12GridViewDiv to JC28GridViewDiv and deleted style エインドリ－・プ－プゥ--%>
                                                <asp:UpdatePanel ID="updTantoushaGrid" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <%--20211202 Updated GridView Width 1230px to 1264px エインドリ－・プ－プゥ--%>
                                                        <asp:GridView runat="server" ID="dg_tantousha" BorderColor="Transparent" AutoGenerateColumns="false" EmptyDataRowStyle-CssClass="JC10NoDataMessageStyle"
                                                            ShowHeader="true" ShowHeaderWhenEmpty="true" CellPadding="0" AllowPaging="True" OnPageIndexChanging="dg_tantousha_PageIndexChanging" OnDataBound="dg_tantousha_DataBound" PageSize="20" DataKeyNames="cTANTOUSHA">
                                                            <EmptyDataRowStyle CssClass="JC10NoDataMessageStyle" />
                                                            <HeaderStyle Height="38px" BackColor="#F2F2F2" HorizontalAlign="Left" />
                                                            <PagerStyle Font-Size="13px" HorizontalAlign="Center" CssClass="GridPager" VerticalAlign="Middle" />
                                                            <%--20211202 Updated PagerFontSize 14px to 13px エインドリ－・プ－プゥ--%>
                                                            <PagerSettings Mode="NumericFirstLast" FirstPageText="&lt;" LastPageText="&gt;" Position="Bottom" />
                                                            <RowStyle Height="43px" CssClass="JC12GridItem" />
                                                            <Columns>
                                                                <asp:TemplateField ItemStyle-CssClass="AlignCenter" HeaderStyle-Font-Size="13px" HeaderText="ログインID" HeaderStyle-CssClass="marginHeader JC10MitumoriGridHeaderStyle fontcss JC28gridheader">
                                                                    <ItemTemplate>
                                                                        <div style="max-width: 280px; text-align: left; padding-top: 4px; padding-left: 10px; overflow: hidden; white-space: nowrap; text-overflow: ellipsis">
                                                                            <asp:Label ID="lblcTantousha" runat="server" CssClass="fontcss" Font-Size="13px" Text='<%# Eval("sMAIL","{0}") %>'></asp:Label>
                                                                            <%--20211202 Added CssClass and FontSize エインドリ－・プ－プゥ--%>
                                                                            <%--<asp:LinkButton runat="server" ID="lnkCode" Text='<%# Eval("cTANTOUSHA","{0}") %>'　 Font-Underline="false" OnClientClick="document.forms[0].target = '_self';"/>--%>
                                                                        </div>
                                                                    </ItemTemplate>

                                                                    <HeaderStyle Width="280px"></HeaderStyle>
                                                                </asp:TemplateField>
                                                                <%--20211202 Updated HeaderStyle-CssClass , CssClass and FontSize エインドリ－・プ－プゥ--%>
                                                                <asp:TemplateField ItemStyle-CssClass="AlignCenter" HeaderText="ユーザー名" HeaderStyle-Width="280px" HeaderStyle-CssClass="JC28UserGridHeaderStyle fontcss">
                                                                    <ItemTemplate>
                                                                        <div style="width: 280px; text-align: left; padding-top: 4px; overflow: hidden; white-space: nowrap; text-overflow: ellipsis">
                                                                            <asp:Label ID="lblsTantousha" runat="server" CssClass="fontcss" Font-Size="13px" Text='<%# Eval("sTANTOUSHA","{0}") %>'></asp:Label>
                                                                        </div>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle Width="280px"></HeaderStyle>
                                                                </asp:TemplateField>
                                                                <%--20211202 Updated HeaderStyle-CssClass and FontSize エインドリ－・プ－プゥ--%>
                                                                <asp:TemplateField ItemStyle-CssClass="AlignCenter" HeaderText="拠点" HeaderStyle-Width="150px" HeaderStyle-CssClass="JC28UserGridHeaderStyle fontcss">
                                                                    <ItemTemplate>
                                                                        <div style="width: 150px; text-align: left; padding-top: 4px; font-size: 13px; overflow: hidden; white-space: nowrap; text-overflow: ellipsis">
                                                                            <%# Eval("sKYOTEN","{0}") %>
                                                                        </div>
                                                                    </ItemTemplate>

                                                                    <HeaderStyle Width="150px" Font-Size="13px"></HeaderStyle>
                                                                </asp:TemplateField>
                                                                <%--20211202 Updated HeaderStyle-CssClass and FontSize エインドリ－・プ－プゥ--%>
                                                                <asp:TemplateField ItemStyle-CssClass="AlignCenter" HeaderText="部門" HeaderStyle-Width="180px" HeaderStyle-CssClass="JC28UserGridHeaderStyle fontcss">
                                                                    <ItemTemplate>
                                                                        <div style="width: 180px; text-align: left; padding-top: 4px; font-size: 13px; overflow: hidden; white-space: nowrap; text-overflow: ellipsis">
                                                                            <%# Eval("sBUMON","{0}") %>
                                                                        </div>
                                                                    </ItemTemplate>

                                                                    <HeaderStyle Width="180px" Font-Size="13px"></HeaderStyle>
                                                                </asp:TemplateField>
                                                                <%--20211202 Updated HeaderStyle-CssClass and FontSize エインドリ－・プ－プゥ--%>
                                                                <asp:TemplateField ItemStyle-CssClass="AlignCenter" HeaderText="権限" HeaderStyle-Width="200px" HeaderStyle-CssClass="JC28UserGridHeaderStyle fontcss">
                                                                    <ItemTemplate>
                                                                        <div style="width: 200px; text-align: left; padding-top: 4px; font-size: 13px; overflow: hidden; white-space: nowrap; text-overflow: ellipsis">
                                                                            <%# Eval("kanrisha","{0}") %>
                                                                        </div>
                                                                    </ItemTemplate>

                                                                    <HeaderStyle Width="200px" Font-Size="13px"></HeaderStyle>
                                                                </asp:TemplateField>
                                                                <%--20211202 Updated HeaderStyle-CssClass and FontSize エインドリ－・プ－プゥ--%>
                                                                <asp:TemplateField ItemStyle-CssClass="AlignCenter" HeaderText="退社日" HeaderStyle-Width="100px" HeaderStyle-CssClass="JC28UserGridHeaderStyle fontcss">
                                                                    <ItemTemplate>
                                                                        <div style="width: 100px; text-align: left; padding-top: 4px; font-size: 13px; overflow: hidden; white-space: nowrap; text-overflow: ellipsis">
                                                                            <%# Eval("dTAISHA","{0}") %>
                                                                        </div>
                                                                    </ItemTemplate>

                                                                    <HeaderStyle Width="100px" Font-Size="13px"></HeaderStyle>
                                                                </asp:TemplateField>

                                                                <asp:TemplateField ItemStyle-CssClass="AlignCenter" HeaderText="" HeaderStyle-Width="30px" HeaderStyle-CssClass="JC28UserGridHeaderStyle fontcss">
                                                                    <ItemTemplate>

                                                                        <div style="width: 30px; overflow: hidden;">
                                                                            <asp:HoverMenuExtender ID="HoverMenuExtender2" runat="server" PopupControlID="PopupMenu"
                                                                                TargetControlID="PopupMenuBtn" PopupPosition="bottom">
                                                                            </asp:HoverMenuExtender>
                                                                            <asp:Panel ID="PopupMenu" runat="server" CssClass="dropdown-menu fontcss " aria-labelledby="dropdownMenuButton" Style="display: none; min-width: 1rem; width: 5rem;">
                                                                                <asp:LinkButton ID="btnEdit" class="dropdown-item" runat="server" Text='編集' Style="margin-right: 10px; font-size: 13px;" CommandArgument="<%# Container.DataItemIndex %>" OnClientClick="PopupOpenButton()" OnClick="btnEdit_Click"></asp:LinkButton>
                                                                                <asp:LinkButton ID="btnDelete" class="dropdown-item " runat="server" Text='削除' Style="margin-right: 10px; font-size: 13px;" CommandArgument="<%# Container.DataItemIndex %>" OnClick="btnDelete_Click"></asp:LinkButton>
                                                                            </asp:Panel>
                                                                            <asp:Panel ID="PopupMenuBtn" runat="server" CssClass="modalPopup" Style="width: 20px;">
                                                                                <button class="btn dropdown-toggle" type="button" id="dropdownMenuButton" data-toggle="dropdown"
                                                                                    aria-haspopup="true" aria-expanded="false" style="border: 1px solid gainsboro; width: 20px; height: 20px; padding: 0px 3px 0px 1px; margin: 0;">
                                                                            </asp:Panel>
                                                                        </div>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle Width="30px" Font-Size="13px"></HeaderStyle>
                                                                </asp:TemplateField>

                                                                 <asp:TemplateField ItemStyle-CssClass="AlignCenter" HeaderText="cTANTOU" HeaderStyle-Width="150px" HeaderStyle-CssClass="JC28UserGridHeaderStyle fontcss" Visible="false">
                                                                    <ItemTemplate>
                                                                        <div style="width: 150px; text-align: left; padding-top: 4px; overflow: hidden; white-space: nowrap; text-overflow: ellipsis">
                                                                            <asp:Label ID="cTantousha" runat="server" CssClass="fontcss" Font-Size="13px" Text='<%# Eval("cTANTOUSHA","{0}") %>'></asp:Label>
                                                                        </div>
                                                                    </ItemTemplate>
                                                                    <HeaderStyle Width="150px"></HeaderStyle>
                                                                </asp:TemplateField>

                                                            </Columns>
                                                        </asp:GridView>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                                <div id="DivNai" runat="server" class="d-flex justify-content-center mt-3">
                                                    <%--20220309 MyatNoe Added--%>
                                                    <asp:Label ID="lblDataNai" runat="server" Style="font-size: 14px; font-weight: normal; margin-left: 100px; cursor: text;" Text="該当するデータがありません"></asp:Label>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </asp:Panel>
                            </td>
                        </tr>
                    </table>
                </div>

                 <asp:Button ID="btnDeleteUser" runat="server" Text="" class="JC09GrayButton DisplayNone" Width="100px" Height="36px" OnClick="btnDeleteUser_Click" />
                <asp:HiddenField ID="HF_UserID" runat="server" />
            </ContentTemplate>
        </asp:UpdatePanel>
        <!--ポップアップ画面-->
        <asp:UpdatePanel ID="updSentakuPopup" runat="server" UpdateMode="Conditional">
            <%--20211116 Added By Eaindray--%>
            <ContentTemplate>
                <asp:Button ID="btnSentakuPopup" runat="server" Text="Button" CssClass="DisplayNone" />
                <asp:ModalPopupExtender ID="mpeSentakuPopup" runat="server" TargetControlID="btnSentakuPopup"
                    PopupControlID="pnlSentakuPopupScroll" BackgroundCssClass="PopupModalBackground" BehaviorID="pnlSentakuPopupScroll">
                </asp:ModalPopupExtender>
                <asp:Panel ID="pnlSentakuPopupScroll" runat="server" Style="display: none; overflow: hidden;" CssClass="PopupScrollDiv">
                    <asp:Panel ID="pnlSentakuPopup" runat="server">
                        <iframe id="ifSentakuPopup" runat="server" scrolling="no" class="NyuryokuIframe" style="border-radius: 0px;"></iframe>
                        <asp:Button ID="btnSave" runat="server" Text="Button" Style="display: none" OnClick="btnSave_Click" onClientClick="MasterOverflow()"/>
                        <asp:Button ID="btnClose1" runat="server" Text="Button" Style="display: none" OnClick="btnClose_Click" onClientClick="MasterOverflow()"/>
                    </asp:Panel>
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>
    </body>
    </html>
</asp:Content>
