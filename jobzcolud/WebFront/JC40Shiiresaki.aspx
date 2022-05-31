<%@ Page Title="" MasterPageFile="~/WebFront/JC99NavBar.Master" Language="C#" AutoEventWireup="true" CodeBehind="JC40Shiiresaki.aspx.cs" Inherits="jobzcolud.WebFront.ShiresakiMaster" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <!DOCTYPE html>

    <html>
    <head>
        <title>仕入先マスタ</title>
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
        <script src="../Scripts/bootstrap.bundle.min.js"></script>
        <script src="../Scripts/bootstrap-datepicker.min.js"></script>
        <style type="text/css">
            .font {
                font-family: 'Bookman Old Style';
                font-size: 15px;
            }

            table.tableclass tbody tr th {
                white-space: nowrap;
                overflow: hidden;
            }

            .GVFixedHeader {
                font-weight: bold;
                position: absolute;
                top: expression(this.parentNode.parentNode.parentNode.scrollTop);
            }

            .GridViewContainer {
                overflow-y: scroll;
                overflow-x: hidden;
            }

            .noResize {
                resize: none;
            }

            .DisplayNone {
                display: none;
            }
            @media only screen and (max-width: 1366px) {
                .grid_header {
                    overflow: auto;
                    white-space: nowrap;
                }

            }
            .auto-style1 {
                width: 150px;
            }
            .auto-style3{
                width: 250px;
            }
            .JC40HeaderCodeCol{
                min-width:50px;
                line-height: 0px;
                font-size: 13px;
                border: 2px solid white;
            }
            .JC18HeaderCol1 {
                min-width:140px;
                line-height: 0px;
                font-size: 13px;
                border: 2px solid white;
            }
        </style>
        <script type='text/javascript' src="https://netdna.bootstrapcdn.com/bootstrap/3.0.3/js/bootstrap.min.js"></script>
        <script type='text/javascript' src='https://code.jquery.com/jquery-1.8.3.js'></script>
        <script src="https://ajaxzip3.github.io/ajaxzip3.js" charset="UTF-8"></script>

        <script type='text/javascript' src='../Scripts/colResizable-1.6.min.js'></script>
        <script type='text/javascript' src='../Scripts/cookie.js'></script>

        <script type="text/javascript">
            function isNumberKey(evt) {
                var regex = /[一-龠]+|[ぁ-ゔ]+|[ァ-ヴー]+|[a-zA-Z0-9]+|[ａ-ｚＡ-Ｚ０-９]+[々〆〤]+/;

                var charCode = (evt.which) ? evt.which : evt.keyCode;
                if (charCode > 31 && (charCode < 48 || charCode > 57) && regex.test(charCode))
                    return false;
                return true;
            }
            function textclear() {
                document.getElementById("<%=lblPhoneError.ClientID %>").innerText = "";
            }

            function ziptoaddr(t) {
                var juusho = document.getElementById("<%=TB_Juusho1.ClientID %>");
                AjaxZip3.zip2addr(t, '', juusho, juusho);
                textclear();
            }

            function RestrictEnter(event) {
                if (event.keyCode === 13) {
                    event.preventDefault();
                }
            }

            function Confirm() {
                var confirm_value = document.createElement("INPUT");
                confirm_value.type = "hidden";
                confirm_value.name = "confirm_value";
                if (confirm("仕入先を入力されています。保存しますか？ ")) {
                    confirm_value.value = "Yes";
                } else {
                    confirm_value.value = "no";
                }

                document.forms[0].appendChild(confirm_value);
            }
        </script>

    </head>
        
    <body id="BD_Shiiresaki" runat="server" style="background-color: #d7e4f2 !important; width: 100% !important; margin-left: 0px;">
        <scripts>
                <asp:ScriptReference Name="jquery" />
                <asp:ScriptReference Name="bootstrap" />
                <asp:ScriptReference Path="../Scripts/Common/FixFocus.js" />
            </scripts>

        <asp:UpdatePanel ID="updHeader" runat="server" UpdateMode="Conditional" RenderMode="Inline">
            <ContentTemplate>
                <div class="JC40TableWidth" id="div3" runat="server">
                    <nav id="nav3" runat="server" class="navbar-expand custom-navbar1 custom-navbar-height" style="position: absolute;">
                        <div class="collapse navbar-collapse JC09navbar JC40TableWidth" id="Div1" runat="server">
                            <label style="font-weight: bold; font-size: 14px; text-align: center; display: inline-block;">仕入先マスタ</label>
                            <asp:Label ID="lblLoginUserCode" runat="server" Text="" Visible="false" />
                            <asp:Label ID="lblLoginUserName" runat="server" Text="" Visible="false" />

                        </div>
                    </nav>
                </div>
                <div id="Div_Body" runat="server" class="container body-content" style="background-color: #d7e4f2; padding-top: 59px; min-height: 100%; margin-bottom: 15px;">
                    <div class="JC40TableWidth" style="background-color: white;padding: 10px 0px 20px 0px;">
                       <div id="divPopupHeader" runat="server" style="height: 40px; margin-top: -6px;">
                            <asp:Label ID="lblHeader" runat="server" Text="仕入先" CssClass="TitleLabel d-block align-content-left"></asp:Label>
                            <asp:Button ID="btnFusenHeaderCross" runat="server" Text="✕" CssClass="PopupCloseBtn" OnClick="btCancel_Click"/>
                        </div>
                        <div id="DivLine" runat="server" class="Borderline"></div>
                    <table runat="server" id="table" align="center">
                        <tr>
                            <td id="td1" runat="server" style="width: 100%">
                                <asp:Panel runat="server" ID="PanelSyousai" Style="height: 570px;" CssClass="JC40TableWidth">
                                    <div style="height: 740px; background-color: white;">
                                        <table style="border: none; background-color: white;" class="JC40TableWidth">
                                            <tr>
                                                <td class="auto-style2">
                                                    <div runat="server" id="divLabelSyousai" style="border-style: none; border-color: inherit; border-width: medium; display: inline-block; margin-left: -6px;">

                                                        <div id="div_leftSyousai" style="width: 480px; height: 350px;margin-left:20px; float: left;">

                                                             <table style="margin-top: 18px; margin-bottom: 15px;">
                                                                <tr>
                                                                    <td>
                                                                        <asp:Button runat="server" ID="BT_Save" Text="保存" type="button " CssClass="BlueBackgroundButton JC10SaveBtn" Style="margin-left: 10px !important;" OnClick="BT_Save_Click" />
                                                                    </td>
                                                                     <td>
                                                                         <asp:Button ID="btCancel" runat="server" Text="キャンセル" type="button " CssClass="btn text-primary font btn-sm btn btn-outline-primary " Style="width: auto !important; background-color: white; border-radius: 3px; font-size: 13px; padding: 6px 12px 6px 12px; letter-spacing: 1px;" OnClick="btCancel_Click" Visible="False" />                
                                                                    </td>
                                                                    <td>
                                                                        <asp:Button runat="server" ID="BT_Shinki" Text="仕入先を新規作成"
                                                                            type="button " CssClass="BlueBackgroundButton JC10SaveBtn" Style="float: left;" OnClick="BT_Shinki_Click" />
                                                                        <%--<asp:Button runat="server" ID="BT_Delete" Text="削除" CssClass="JC10DeleteBtn" ForeColor="White" Width="90px" />--%>
                                                                    </td>
                                                                </tr>
                                                                 </table>
                                                             <table style="margin-left: 10px;">
                                                                <tr style="height: 40px;">
                                                                    <td>
                                                                        <label>仕入先コード</label>
                                                                    </td>
                                                                    <td>
                                                                        <asp:Label runat="server" ID="LB_ShiresakiCode" Style="display: block;"></asp:Label>
                                                                    </td>
                                                                    <td></td>
                                                                </tr>
                                                                <tr style="height: 40px;">
                                                                    <td class="auto-style1">
                                                                        <label>仕入先名<span class="fw-bold JC02Star-color">*</span></label>
                                                                    </td>
                                                                    <td colspan="3" class="auto-style1">
                                                                        <asp:TextBox runat="server" ID="TB_sShiresaki" CssClass="form-control TextboxStyle JC40MeiTextBox" AutoPostBack="True" MaxLength="40" onkeyup="DeSelectText(this);" onfocus="this.setSelectionRange(this.value.length, this.value.length);" OnTextChanged="TB_sShiresaki_TextChanged"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr style="height: 40px;">
                                                                    <td class="auto-style1">
                                                                        <label>仕入先名カナ</label>
                                                                    </td>
                                                                    <td colspan="3" class="auto-style1">
                                                                        <asp:TextBox runat="server" ID="TB_skShiresaki" CssClass="form-control TextboxStyle JC40MeiTextBox" AutoPostBack="True" MaxLength="100" onkeyup="DeSelectText(this);" onfocus="this.setSelectionRange(this.value.length, this.value.length);" OnTextChanged="TB_skShiresaki_TextChanged"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr style="height: 40px;">
                                                                    <td class="auto-style1">
                                                                        <label>仕入先名略称</label>
                                                                    </td>
                                                                    <td colspan="3" class="auto-style1">
                                                                        <asp:TextBox runat="server" ID="TB_sShiresaki_R" CssClass="form-control TextboxStyle JC40Mei_RTextBox" AutoPostBack="True" MaxLength="10" onkeyup="DeSelectText(this);" onfocus="this.setSelectionRange(this.value.length, this.value.length);" OnTextChanged="TB_sShiresaki_R_TextChanged"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr style="height: 40px;">
                                                                    <td class="auto-style1">
                                                                        <label>郵便番号</label>
                                                                    </td>
                                                                    <td colspan="3" class="auto-style1">
                                                                        <asp:TextBox runat="server" ID="TB_cYuubin" type="text" name="txtname" CssClass="form-control TextboxStyle JC40yubenBangoTextBox" AutoPostBack="True" MaxLength="7" autocomplete="off" onKeyUp="ziptoaddr(this);" onkeypress="return isNumberKey(event)" onfocus="this.setSelectionRange(this.value.length, this.value.length);" OnTextChanged="TB_cYuubin_TextChanged"></asp:TextBox>
                                                                        <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" FilterType="Numbers" TargetControlID="TB_cYuubin" />
                                                                    </td>
                                                                </tr>
                                                                <tr style="height: 40px;">
                                                                    <td class="auto-style1">
                                                                        <label>住所1</label>
                                                                    </td>
                                                                    <td colspan="3" class="auto-style1">
                                                                        <asp:TextBox runat="server" ID="TB_Juusho1" type="text" name="txtname" CssClass="form-control TextboxStyle JC40MeiTextBox" autocomplete="off" onfocus="this.setSelectionRange(this.value.length, this.value.length);" OnTextChanged="TB_Juusho1_TextChanged"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr style="height: 40px;">
                                                                    <td class="auto-style1">
                                                                        <label>住所2</label>
                                                                    </td>
                                                                    <td colspan="3" class="auto-style1">
                                                                        <asp:TextBox runat="server" ID="TB_Juusho2" type="text" name="txtname" CssClass="form-control TextboxStyle JC40MeiTextBox" AutoPostBack="True" onkeyup="DeSelectText(this);" autocomplete="off" onfocus="this.setSelectionRange(this.value.length, this.value.length);" OnTextChanged="TB_Juusho2_TextChanged"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr style="height: 40px;">
                                                                    <td class="auto-style1">
                                                                        <label>電話</label>
                                                                    </td>
                                                                    <td colspan="3" class="auto-style1">
                                                                        <asp:TextBox runat="server" ID="TB_sTel" type="text" name="txtname" CssClass="form-control TextboxStyle JC40denwaTextBox" AutoPostBack="True" onkeypress="return isNumberKey(event)" onkeyup="DeSelectText(this);" onfocus="this.setSelectionRange(this.value.length, this.value.length);" OnTextChanged="TB_sTel_TextChanged"></asp:TextBox>
                                                                        <asp:Label ID="lblPhoneError" runat="server" ForeColor="Red"></asp:Label>
                                                                        <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Numbers" TargetControlID="TB_sTel" />
                                                                    </td>
                                                                </tr>
                                                                <tr style="height: 40px;">
                                                                    <td class="auto-style1">
                                                                        <label>FAX</label>
                                                                    </td>
                                                                    <td colspan="3" class="auto-style1">
                                                                        <asp:TextBox runat="server" ID="TB_sFAX" type="text" name="txtname" CssClass="form-control TextboxStyle JC40denwaTextBox" AutoPostBack="True" MaxLength="20" onkeypress="return isNumberKey(event)" onkeyup="DeSelectText(this);" onfocus="this.setSelectionRange(this.value.length, this.value.length);" OnTextChanged="TB_sFAX_TextChanged"></asp:TextBox>
                                                                       <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" FilterType="Numbers" TargetControlID="TB_sFAX" />
                                                                    </td>
                                                                </tr>
                                                                <tr style="height: 130px;">
                                                                    <td style="vertical-align: top; width: 100px;">
                                                                        <label>特記事項</label>
                                                                    </td>
                                                                    <td colspan="3" class="auto-style1">
                                                                        <asp:TextBox runat="server" ID="TB_sTOKKIJIKOU" TextMode="MultiLine" CssClass="form-control TextboxStyle JC40TextArea" Rows="6" AutoPostBack="True" MaxLength="200" onkeyup="DeSelectText(this);" onfocus="this.setSelectionRange(this.value.length, this.value.length);" OnTextChanged="TB_sTOKKIJIKOU_TextChanged"></asp:TextBox>
                                                                    </td>
                                                                    <td></td>
                                                                </tr>
                                                            </table>
                                                        </div>
                                                        <div id="div_rightSyousai" style="height: 350px;width:400px; float: right;margin-left:100px;">
                                                            <table>
                                                                <tr style="height: 60px;">
                                                                    <td></td>
                                                                </tr>
                                                                <tr style="height: 40px;">
                                                                    <td style="align-content: center; " class="auto-style1">
                                                                        <label style="font-weight: bold;font-size: 14px;">仕入オプション</label></td>
                                                                </tr>
                                                                <tr style="height: 40px;">

                                                                    <td style="vertical-align: top; " class="auto-style3">
                                                                        <label>締め日</label>
                                                                    </td>
                                                                    <td style="vertical-align: top;" class="auto-style1">
                                                                        <asp:UpdatePanel ID="updShimebi" runat="server" UpdateMode="Conditional">
                                                                            <ContentTemplate>
                                                                                <asp:DropDownList ID="DDL_shimebi" runat="server" Width="90px" AutoPostBack="True" Height="26px" CssClass="form-control TextboxStyle JC40ddl2" OnSelectedIndexChanged="DDL_shimebi_SelectedIndexChanged"> 
                                                                                </asp:DropDownList>
                                                                            </ContentTemplate>
                                                                        </asp:UpdatePanel>
                                                                    </td>
                                                                </tr>
                                                                <tr style="height: 40px;">
                                                                    <td class="auto-style1">
                                                                        <label style="font-size: 14px;">基本支払方法</label>
                                                                    </td>
                                                                    <td></td>
                                                                </tr>
                                                                <tr>
                                                                    <td class="auto-style1">
                                                                        <asp:UpdatePanel ID="updkihonshiharai" runat="server" UpdateMode="Conditional">
                                                                            <ContentTemplate>
                                                                                <asp:DropDownList ID="DDL_kihonshiharai" runat="server" Width="170px" AutoPostBack="True" Height="26px" CssClass="form-control TextboxStyle JC40ddl" OnSelectedIndexChanged="DDL_kihonshiharai_SelectedIndexChanged">
                                                                                </asp:DropDownList>
                                                                            </ContentTemplate>
                                                                        </asp:UpdatePanel>
                                                                    </td>
                                                                    <td style="width: 90px;">
                                                                        <asp:TextBox runat="server" ID="TB_kihonvalue" type="text" name="txtname" Text="0" Width="60px" Height="26px" autocomplete="off" CssClass="form-control TextboxStyle JC40ddl1" AutoPostBack="True" MaxLength="3" onkeypress="return isNumberKey(event)" onkeyup="DeSelectText(this);" onfocus="this.setSelectionRange(this.value.length, this.value.length);" OnTextChanged="TB_kihonvalue_TextChanged"></asp:TextBox>
                                                                        <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" FilterType="Numbers" TargetControlID="TB_kihonvalue" />
                                                                      
                                                                    </td>
                                                                </tr>
                                                                <tr style="height: 40px;">
                                                                    <td class="auto-style1">
                                                                        <asp:UpdatePanel ID="updkihonshiharai1" runat="server" UpdateMode="Conditional">
                                                                            <ContentTemplate>
                                                                                <asp:DropDownList ID="DDL_kihonshiharai1" runat="server" Width="170px" AutoPostBack="True" Height="26px" CssClass="form-control TextboxStyle JC40ddl" OnSelectedIndexChanged="DDL_kihonshiharai1_SelectedIndexChanged">
                                                                                </asp:DropDownList>
                                                                            </ContentTemplate>
                                                                        </asp:UpdatePanel>
                                                                    </td>
                                                                    <td style="width: 90px;">
                                                                        <asp:TextBox runat="server" ID="TB_kihonvalue1" type="text" name="txtname" Text="0" Width="60px" Height="26px" autocomplete="off" CssClass="form-control TextboxStyle JC40ddl1" AutoPostBack="True" MaxLength="3" onkeypress="return isNumberKey(event)" onkeyup="DeSelectText(this);" onfocus="this.setSelectionRange(this.value.length, this.value.length);" OnTextChanged="TB_kihonvalue1_TextChanged"></asp:TextBox>
                                                                        <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" FilterType="Numbers" TargetControlID="TB_kihonvalue1" />
                                                                    </td>
                                                                </tr>
                                                                <tr style="height: 40px;">
                                                                    <td class="auto-style1">
                                                                        <label>拡張支払方法</label>
                                                                    </td>
                                                                    <td style="width: 200px;">
                                                                        <asp:CheckBox ID="chk_kakuchoshiharai" runat="server" TextAlign="Left" CssClass="font font-weight-bold text-center" OnCheckedChanged="chk_kakuchoshiharai_Clicked" />
                                                                    </td>
                                                                </tr>
                                                                <tr style="height: 40px;">
                                                                    <td class="auto-style1">
                                                                        <label>適用条件</label>
                                                                    </td>
                                                                    <td style="width: 200px;">
                                                                        <asp:TextBox runat="server" ID="TB_TekiyoJouken" type="text" name="txtname" Text="100" Width="100px" Height="26px" autocomplete="off" CssClass="form-control TextboxStyle JC40ddl1" AutoPostBack="True" MaxLength="9" onkeypress="return isNumberKey(event)" onkeyup="DeSelectText(this);" onfocus="this.setSelectionRange(this.value.length, this.value.length);" OnTextChanged="TB_TekiyoJouken_TextChanged"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                                <tr style="height: 40px;">
                                                                    <td class="auto-style1">
                                                                        <label>円</label>
                                                                    </td>
                                                                    <td style="width: 200px;">
                                                                        <asp:UpdatePanel ID="updyan" runat="server" UpdateMode="Conditional">
                                                                            <ContentTemplate>
                                                                                <asp:DropDownList ID="DDL_Yan" runat="server" Width="170px" AutoPostBack="True" Height="26px" CssClass="form-control TextboxStyle JC40ddl2" OnSelectedIndexChanged="DDL_Yan_SelectedIndexChanged">
                                                                                </asp:DropDownList>
                                                                            </ContentTemplate>
                                                                        </asp:UpdatePanel>
                                                                    </td>
                                                                </tr>
                                                                <tr style="height: 40px;">
                                                                    <td class="auto-style1">
                                                                        <asp:UpdatePanel ID="updkakuchoshiharai" runat="server" UpdateMode="Conditional">
                                                                            <ContentTemplate>
                                                                                <asp:DropDownList ID="DDL_kakuchoshiharai" runat="server" Width="170px" AutoPostBack="True" Height="26px" CssClass="form-control TextboxStyle JC40ddl" OnSelectedIndexChanged="DDL_kakuchoshiharai_SelectedIndexChanged">
                                                                                </asp:DropDownList>
                                                                            </ContentTemplate>
                                                                        </asp:UpdatePanel>
                                                                    </td>
                                                                    <td style="width: 90px;">
                                                                        <asp:TextBox runat="server" ID="TB_kakuchovalue" type="text" name="txtname" Text="0" Width="60px" Height="26px" autocomplete="off" CssClass="form-control TextboxStyle JC40ddl1" AutoPostBack="True" MaxLength="3" onkeypress="return isNumberKey(event)" onkeyup="DeSelectText(this);" onfocus="this.setSelectionRange(this.value.length, this.value.length);" OnTextChanged="TB_kakuchovalue_TextChanged"></asp:TextBox>
                                                                        <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server" FilterType="Numbers" TargetControlID="TB_kakuchovalue" />
                                                                    </td>
                                                                </tr>
                                                                <tr style="height: 40px;">
                                                                    <td class="auto-style1">
                                                                        <asp:UpdatePanel ID="updkakuchoshiharai1" runat="server" UpdateMode="Conditional">
                                                                            <ContentTemplate>
                                                                                <asp:DropDownList ID="DDL_kakuchoshiharai1" runat="server" Width="170px" AutoPostBack="True" Height="26px" CssClass="form-control TextboxStyle JC40ddl" OnSelectedIndexChanged="DDL_kakuchoshiharai1_SelectedIndexChanged">
                                                                                </asp:DropDownList>
                                                                            </ContentTemplate>
                                                                        </asp:UpdatePanel>
                                                                    </td>
                                                                    <td style="width: 90px;">
                                                                        <asp:TextBox runat="server" ID="TB_kakuchovalue1" type="text" name="txtname" Text="0" Width="60px" Height="26px" autocomplete="off" CssClass="form-control TextboxStyle JC40ddl1" AutoPostBack="True" MaxLength="3" onkeypress="return isNumberKey(event)" onkeyup="DeSelectText(this);" onfocus="this.setSelectionRange(this.value.length, this.value.length);" OnTextChanged="TB_kakuchovalue1_TextChanged"></asp:TextBox>
                                                                        <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" runat="server" FilterType="Numbers" TargetControlID="TB_kakuchovalue1" />
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </div>

                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </asp:Panel>
                            </td>
                        </tr>
                        <tr style="height: 30px;">
                            <td>
                                <div class="JC10Borderline">
                        </div>
                            </td>
                        </tr>
                        <tr style="height: 30px;background-color: white;">
                            <td><label style ="margin-left: 20px;font-weight:bold;font-size:14px;" >仕入先担当者</label></td>
                        </tr>
                        <tr style="background-color: white;">
                        <td class="JC40TableWidth">
                            <div id="DIV_GV" runat="server" style="margin-left: 20px; padding-right: 10px;" >
                                 <div style=" overflow-x: auto; width: 100% !important;">
                            <div style=" background-color: white; width: auto; display: inline-block !important">
                                  <asp:UpdatePanel ID="updShiresakiMasterGrid" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                           
                                            <asp:GridView ID="GV_ShiiresakiMaster" runat="server" GridLines="Horizontal" CellPadding="4" BorderStyle="none" EnableTheming="True" BorderColor="Transparent"
                                                BackColor="White" HeaderStyle-CssClass="GVFixedHeader" AutoGenerateColumns="False"
                                                CssClass="tableclass RowHover GridViewStyle grip" ShowHeaderWhenEmpty="True" Style="margin-right: 0px" AllowSorting="True" OnRowCommand="GV_ShiiresakiMaster_RowCommand" OnRowCancelingEdit="GV_ShiiresakiMaster_RowCancelingEdit" OnRowDeleting="GV_ShiiresakiMaster_RowDeleting" OnRowEditing="GV_ShiiresakiMaster_RowEditing" OnRowUpdating="GV_ShiiresakiMaster_RowUpdating" OnPreRender="GV_ShiiresakiMaster_PreRender" OnRowCreated="GV_ShiiresakiMaster_RowCreated" OnRowDataBound="GV_ShiiresakiMaster_RowDataBound" OnSorting="GV_ShiiresakiMaster_Sorting">
                                                <HeaderStyle Height="39px" BackColor="#F2F2F2" ForeColor="Black" CssClass="grid_header" />
                                                <RowStyle CssClass="GridRow" Height="37px" />
                                                <SelectedRowStyle BackColor="#EBEBF5" />
                                                 <Columns>
                                                <asp:TemplateField HeaderText="No" SortExpression="NJUNBAN">
                                                    <ItemTemplate>
                                                        <div class="grip" style="text-align: left; padding-right: 4px; display: -webkit-box; -webkit-line-clamp: 1; -webkit-box-orient: vertical; word-break: break-all; text-overflow: ellipsis">
                                                           
                                                            <asp:Label ID="lblnjun" Text=' <%# Eval("NJUNBAN","{0}") %>' CommandArgument='<%# Container.DataItemIndex %>' runat="server" Style="margin-left: 10px;" />
                                                        </div>
                                                    </ItemTemplate>
                                                    <HeaderStyle CssClass="JC40HeaderCodeCol AlignLeft" BorderColor="White" BorderWidth="2px" />
                                                    <ItemStyle CssClass=" AlignLeft" />
                                                    <FooterStyle CssClass=" AlignLeft" />
                                                    <EditItemTemplate>
                                                        <div class="grip" style="text-align: left; padding-right: 4px; overflow: hidden; display: -webkit-box; -webkit-line-clamp: 1; -webkit-box-orient: vertical; word-break: break-all;">
                                                           
                                                            <asp:Label ID="lblnjunban" Text=' <%# Eval("NJUNBAN","{0}") %>' runat="server" Style="font-size: 14px; font-weight: normal; margin-left: 10px;" />
                                                        </div>
                                                    </EditItemTemplate>
                                                    <FooterTemplate>
                                                        <div class="grip" style="text-align: left; padding-right: 4px; overflow: hidden; display: -webkit-box; -webkit-line-clamp: 1; -webkit-box-orient: vertical; word-break: break-all;">

                                                            <asp:Label ID="lblnjunbanFooter" Text=' <%# Eval("NJUNBAN","{0}") %>' runat="server" Style="font-size: 14px; font-weight: normal; margin-left: 10px; margin-top: 5px;" />
                                                        </div>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                                
                                                <asp:TemplateField HeaderText="部門" SortExpression="SBUMON">
                                                    <ItemTemplate>
                                                        <div class="grip" style="text-align: left; padding-right: 4px; overflow: hidden; display: -webkit-box; -webkit-line-clamp: 1; -webkit-box-orient: vertical; word-break: break-all;">
                                                            <asp:Label runat="server" class="JC19txtTantou" Text=' <%# Eval("SBUMON","{0}") %>' CommandArgument='<%# Container.DataItemIndex %>' ID="SBUMON" />
                                                        </div>
                                                    </ItemTemplate>
                                                    <HeaderStyle CssClass="JC18HeaderCol AlignLeft" BorderColor="White" BorderWidth="2px" />
                                                    <ItemStyle CssClass="AlignLeft" />
                                                    <EditItemTemplate>
                                                        <div class="grip" style="text-align: left; padding-right: 4px; overflow: hidden; display: -webkit-box; -webkit-line-clamp: 1; -webkit-box-orient: vertical; word-break: break-all;">
                                                            <asp:TextBox ID="txtsBumon" runat="server" Text=' <%# Eval("SBUMON","{0}") %>' CommandArgument='<%# Container.DataItemIndex %>' CssClass="form-control TextboxStyle JC10GridTextBox" Style="font-size: 14px; font-weight: normal; height: 25px;"></asp:TextBox>
                                                        </div>
                                                    </EditItemTemplate>
                                                    <FooterTemplate>
                                                        <div class="grip" style="text-align: left; padding-right: 4px; overflow: hidden; display: -webkit-box; -webkit-line-clamp: 1; -webkit-box-orient: vertical; word-break: break-all;">
                                                            <asp:TextBox ID="txtsBumonFooter" runat="server" CssClass="form-control TextboxStyle JC10GridTextBox" Style="font-size: 14px; font-weight: normal; height: 25px; margin-top: 5px;" />
                                                        </div>
                                                    </FooterTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="役職" SortExpression="SYAKUSHOKU">
                                                    <ItemTemplate>
                                                        <div class="grip" style="text-align: left; padding-right: 4px; overflow: hidden; display: -webkit-box; -webkit-line-clamp: 1; -webkit-box-orient: vertical; word-break: break-all;">
                                                            <asp:Label runat="server" Text=' <%# Eval("SYAKUSHOKU","{0}") %>' CommandArgument='<%# Container.DataItemIndex %>' ID="SYAKUSHOKU" />
                                                        </div>
                                                    </ItemTemplate>
                                                    <HeaderStyle CssClass="JC18HeaderCol" BorderColor="White" BorderWidth="2px" />
                                                    <ItemStyle CssClass="AlignLeft" />
                                                    <EditItemTemplate>
                                                        <div class="grip" style="text-align: left; padding-right: 4px; overflow: hidden; display: -webkit-box; -webkit-line-clamp: 1; -webkit-box-orient: vertical; word-break: break-all;">
                                                            <asp:TextBox ID="txtsYakushoku" runat="server" MaxLength="20" Text=' <%# Eval("SYAKUSHOKU","{0}") %>' CommandArgument='<%# Container.DataItemIndex %>' CssClass="form-control TextboxStyle JC10GridTextBox" Style="font-size: 14px; font-weight: normal; height: 25px;"></asp:TextBox>
                                                        </div>
                                                    </EditItemTemplate>
                                                    <FooterTemplate>
                                                        <div class="grip" style="text-align: left; padding-right: 4px; overflow: hidden; display: -webkit-box; -webkit-line-clamp: 1; -webkit-box-orient: vertical; word-break: break-all;">
                                                            <asp:TextBox ID="txtsYakushokuFooter" runat="server" MaxLength="20" CssClass="form-control TextboxStyle JC10GridTextBox" Style="font-size: 14px; font-weight: normal; height: 25px; margin-top: 5px;" />
                                                        </div>
                                                    </FooterTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="電話番号" HeaderStyle-Width="190px" HeaderStyle-Height="37px" SortExpression="STEL">
                                                    <ItemTemplate>
                                                        <div class="grip" style="text-align: left; padding-right: 4px; overflow: hidden; display: -webkit-box; -webkit-line-clamp: 1; -webkit-box-orient: vertical; word-break: break-all;">
                                                            <asp:Label runat="server" Text=' <%# Eval("STEL","{0}") %>' CommandArgument='<%# Container.DataItemIndex %>' ID="STEL" />
                                                        </div>
                                                    </ItemTemplate>
                                                    <HeaderStyle CssClass="JC18HeaderCol" BorderColor="White" BorderWidth="2px" />
                                                    <ItemStyle CssClass="AlignLeft" />
                                                    <EditItemTemplate>
                                                         <%-- 20220325 テタ Updated FilteredTextBox--%>
                                                        <div class="grip" style="text-align: left; padding-right: 4px; overflow: hidden; display: -webkit-box; -webkit-line-clamp: 1; -webkit-box-orient: vertical; word-break: break-all;">
                                                            <asp:TextBox ID="txtsTel" runat="server" Text=' <%# Eval("STEL","{0}") %>' CommandArgument='<%# Container.DataItemIndex %>' MaxLength="20" onkeypress="return isNumberKey(event)" CssClass="form-control TextboxStyle JC10GridTextBox" Style="font-size: 14px; font-weight: normal; height: 25px;"></asp:TextBox>
                                                             <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" FilterType="Numbers" TargetControlID="txtsTel" />
                                                        </div>
                                                    </EditItemTemplate>
                                                    <FooterTemplate>
                                                        <div class="grip" style="text-align: left; padding-right: 4px; overflow: hidden; display: -webkit-box; -webkit-line-clamp: 1; -webkit-box-orient: vertical; word-break: break-all;">
                                                            <asp:TextBox ID="txtsTelFooter" runat="server" MaxLength="20" CssClass="form-control TextboxStyle JC10GridTextBox " onkeypress="return isNumberKey(event)" Style="font-size: 14px; font-weight: normal; height: 25px; margin-top: 5px;" />
                                                            <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server" FilterType="Numbers" TargetControlID="txtsTelFooter" />
                                                        </div>
                                                    </FooterTemplate>
                                                </asp:TemplateField>

                                                     <asp:TemplateField HeaderText="担当者名" SortExpression="STANTOU">
                                                    <ItemTemplate>
                                                        <div class="grip" style="text-align: left; padding-right: 4px; overflow: hidden; display: -webkit-box; -webkit-line-clamp: 1; -webkit-box-orient: vertical; word-break: break-all;">
                                                            <asp:Label runat="server" Text=' <%# Eval("STANTOU","{0}") %>' CommandArgument='<%# Container.DataItemIndex %>' ID="STANTOU" />
                                                        </div>
                                                    </ItemTemplate>
                                                  
                                                    <HeaderStyle CssClass="JC18HeaderCol AlignLeft" BorderColor="White" BorderWidth="2px" />
                                                    <ItemStyle CssClass="AlignLeft" />
                                                    <EditItemTemplate>
                                                        <div class="grip" style="text-align: left; padding-right: 4px; overflow: hidden; display: -webkit-box; -webkit-line-clamp: 1; -webkit-box-orient: vertical; word-break: break-all;">
                                                            <asp:TextBox ID="txtsTantousha" runat="server" Text=' <%# Eval("STANTOU","{0}") %>' CssClass="form-control TextboxStyle JC10GridTextBox" Style="font-size: 14px; font-weight: normal; height: 25px;"></asp:TextBox>
                                                        </div>
                                                    </EditItemTemplate>
                                                    <FooterTemplate>
                                                        <div class="grip" style="text-align: left; padding-right: 4px; overflow: hidden; display: -webkit-box; -webkit-line-clamp: 1; -webkit-box-orient: vertical; word-break: break-all;">
                                                            <asp:TextBox ID="txtsTantoushaFooter" runat="server" CssClass="form-control TextboxStyle JC10GridTextBox" Width="100%" Style="font-size: 14px; font-weight: normal; height: 25px; margin-top: 5px;" />
                                                        </div>
                                                    </FooterTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="">
                                                    <ItemTemplate>
                                                         <%-- 20220325 テタ Updated--%>
                                                        <div class="grip" style="min-width: 160px; display: flex; align-content: center; align-items: center; align-items: center; justify-content: center; z-index: 2;">
                                                         <%--   <asp:HoverMenuExtender ID="HoverMenuExtender2" runat="server" PopupControlID="PopupMenu"
                                                                TargetControlID="PopupMenuBtn" PopupPosition="left">
                                                            </asp:HoverMenuExtender>
                                                            <asp:Panel ID="PopupMenu" runat="server" CssClass="dropdown-menu fontcss " aria-labelledby="dropdownMenuButton" Style="display: none; min-width: 1rem; width: 5rem;">
                                                                <asp:LinkButton ID="btnEdit" CommandName="Edit" class="dropdown-item" runat="server" Text='編集' Style="margin-right: 10px; font-size: 13px;"></asp:LinkButton>
                                                                <asp:LinkButton ID="btnDelete" CommandName="Delete" class="dropdown-item " runat="server" Text='削除' Style="margin-right: 10px; font-size: 13px;"></asp:LinkButton>
                                                            </asp:Panel>
                                                            <asp:Panel ID="PopupMenuBtn" runat="server" CssClass="modalPopup" Style="width: 20px;">
                                                                <button class="btn dropdown-toggle" type="button" id="dropdownMenuButton" data-toggle="dropdown"
                                                                    aria-haspopup="true" aria-expanded="false" style="border: 1px solid gainsboro; width: 20px; height: 20px; padding: 0px 3px 0px 1px; margin: 0; margin-top: -3px;">
                                                            </asp:Panel>--%>
                                                            <asp:Button ID="btnEdit" runat="server" CommandName="Edit" Text="編集" CssClass="JC09GridGrayBtn" Width="50px" Height="28px" />
                                                            <asp:Button ID="btnDelete" runat="server" CommandName="Delete" Text="削除" float="left" CssClass="JC09GridGrayBtn" Style="margin-left: 10px;" Width="50px" Height="28px" />
                                                        </div>

                                                    </ItemTemplate>
                                                    <HeaderStyle CssClass="JC18HeaderCol1 AlignRight" BorderColor="White" BorderWidth="2px" />
                                                    <ItemStyle CssClass="JC09DropDown AlignCenter" />
                                                    <EditItemTemplate>
                                                        <div>
                                                            <asp:Button ID="btnUpdate" runat="server" CommandName="Update" Text="更新" CssClass="JC09GridGrayBtn" Width="50px" Height="28px" />
                                                            <asp:Button ID="btnCancel" runat="server" CommandName="Cancel" Text="キャンセル" float="left" CssClass="JC09GridGrayBtn" Width="90px" Height="28px" />
                                                        </div>
                                                    </EditItemTemplate>
                                                    <FooterTemplate>
                                                        <div style="text-align: center;">
                                                            <asp:Button ID="btnSave" runat="server" CommandName="Save" Text="追加" CssClass="JC09GridGrayBtn" Width="50px" Height="28px" Style="margin-top: 5px;" />
                                                            <asp:Button ID="btnCancel" runat="server" CommandName="Cancel" Text="キャンセル" float="left" CssClass="JC09GridGrayBtn" Width="90px" Height="28px" Style="margin-top: 5px;" />
                                                        </div>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                                <FooterStyle ForeColor="#D7DBDD" Height="40px" CssClass="GridRow" />
                                            <PagerStyle ForeColor="Black" HorizontalAlign="Center" Width="100px" BackColor="White" Font-Size="Small" Font-Strikeout="False" Font-Underline="True" CssClass="page" Font-Names="ＭＳ ゴシック" Height="50px" />
                                            <RowStyle BackColor="White" ForeColor="Black" HorizontalAlign="Left" VerticalAlign="Middle" BorderColor="#D7DBDD" BorderStyle="None" />
                                            <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                                            <SortedAscendingCellStyle BackColor="#EDF6F6" />
                                            <SortedAscendingHeaderStyle BackColor="#0D4AC4" />
                                            <SortedDescendingCellStyle BackColor="#D6DFDF" />
                                            <SortedDescendingHeaderStyle BackColor="#002876" />
                                            </asp:GridView>
                                        </ContentTemplate>
                                       <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="BT_ColumnWidth" EventName="Click" />
                                    </Triggers>
                                    </asp:UpdatePanel>
                                </div>
                                 </div>
                            </div>
                        </td>
                    </tr>
                        <tr style="height:40px;background-color: white;">
                            <td>
                                <asp:Button ID="btnaddNewrow" runat="server" Text="＋　仕入先担当者を追加" CssClass="JC40shiiresakiNewBtn BlueBackgroundButton" Width="185px" Height="36px" OnClick="btnaddNewrow_Click" />
                            </td>
                        </tr>
                    </table>
                        </div>
                          <script type="text/javascript">　
                    Sys.Application.add_load(function () {
                        function getShiftJISByteLength(s) {
                            return s.replace(/[^\x00-\x80&#63728;｡｢｣､･ｦｧｨｩｪｫｬｭｮｯｰｱｲｳｴｵｶｷｸｹｺｻｼｽｾｿﾀﾁﾂﾃﾄﾅﾆﾇﾈﾉﾊﾋﾌﾍﾎﾏﾐﾑﾒﾓﾔﾕﾖﾗﾘﾙﾚﾛﾜﾝ ﾞ ﾟ&#63729;&#63730;&#63731;]/g, 'xx').length;
                        }

                        $('#<%= TB_sShiresaki . ClientID%>').on('keyup keydown', function (e) {
                                var text = $(this).val();
                                var byteAmount = getShiftJISByteLength(text);
                                if (e.keyCode != "Backspace" && e.key != "Delete") {
                                    while (byteAmount > 60) {
                                        text = text.substring(0, text.length - 1);
                                        byteAmount = getShiftJISByteLength(text);
                                        document.getElementById("<%= TB_sShiresaki . ClientID%>").value = text;
                                    }
                                }
                            });

                            $('#<%= TB_sShiresaki . ClientID%>').on('change', function (e) {
                                var text = $(this).val();
                                var byteAmount = getShiftJISByteLength(text);
                                while (byteAmount > 60) {
                                    text = text.substring(0, text.length - 1);
                                    byteAmount = getShiftJISByteLength(text);
                                    document.getElementById("<%= TB_sShiresaki . ClientID%>").value = text;
                                }
                        });

                        $('#<%= TB_skShiresaki . ClientID%>').on('keyup keydown', function (e) {
                                var text = $(this).val();
                                var byteAmount = getShiftJISByteLength(text);
                                if (e.keyCode != "Backspace" && e.key != "Delete") {
                                    while (byteAmount > 60) {
                                        text = text.substring(0, text.length - 1);
                                        byteAmount = getShiftJISByteLength(text);
                                        document.getElementById("<%= TB_skShiresaki . ClientID%>").value = text;
                                    }
                                }
                            });

                            $('#<%= TB_skShiresaki . ClientID%>').on('change', function (e) {
                                var text = $(this).val();
                                var byteAmount = getShiftJISByteLength(text);
                                while (byteAmount > 60) {
                                    text = text.substring(0, text.length - 1);
                                    byteAmount = getShiftJISByteLength(text);
                                    document.getElementById("<%= TB_skShiresaki . ClientID%>").value = text;
                                }
                        });

                        $('#<%= TB_sShiresaki_R . ClientID%>').on('keyup keydown', function (e) {
                                var text = $(this).val();
                                var byteAmount = getShiftJISByteLength(text);
                                if (e.keyCode != "Backspace" && e.key != "Delete") {
                                    while (byteAmount > 20) {
                                        text = text.substring(0, text.length - 1);
                                        byteAmount = getShiftJISByteLength(text);
                                        document.getElementById("<%= TB_sShiresaki_R . ClientID%>").value = text;
                                    }
                                }
                            });

                            $('#<%= TB_sShiresaki_R . ClientID%>').on('change', function (e) {
                                var text = $(this).val();
                                var byteAmount = getShiftJISByteLength(text);
                                while (byteAmount > 20) {
                                    text = text.substring(0, text.length - 1);
                                    byteAmount = getShiftJISByteLength(text);
                                    document.getElementById("<%= TB_sShiresaki_R . ClientID%>").value = text;
                                }
                            });

                            $('#<%= TB_Juusho1 . ClientID%>').on('keyup keydown', function (e) {
                                var text = $(this).val();
                                var byteAmount = getShiftJISByteLength(text);
                                if (e.keyCode != "Backspace" && e.key != "Delete") {
                                    while (byteAmount > 40) {
                                        text = text.substring(0, text.length - 1);
                                        byteAmount = getShiftJISByteLength(text);
                                        document.getElementById("<%= TB_Juusho1 . ClientID%>").value = text;
                                    }
                                }
                            });

                            $('#<%= TB_Juusho1 . ClientID%>').on('change', function (e) {
                                var text = $(this).val();
                                var byteAmount = getShiftJISByteLength(text);
                                while (byteAmount > 40) {
                                    text = text.substring(0, text.length - 1);
                                    byteAmount = getShiftJISByteLength(text);
                                    document.getElementById("<%= TB_Juusho1 . ClientID%>").value = text;
                                }
                            });

                            $('#<%= TB_Juusho2 . ClientID%>').on('keyup keydown', function (e) {
                                var text = $(this).val();
                                var byteAmount = getShiftJISByteLength(text);
                                if (e.keyCode != "Backspace" && e.key != "Delete") {
                                    while (byteAmount > 40) {
                                        text = text.substring(0, text.length - 1);
                                        byteAmount = getShiftJISByteLength(text);
                                        document.getElementById("<%= TB_Juusho2 . ClientID%>").value = text;
                                    }
                                }
                            });

                            $('#<%= TB_Juusho2 . ClientID%>').on('change', function (e) {
                                var text = $(this).val();
                                var byteAmount = getShiftJISByteLength(text);
                                while (byteAmount > 40) {
                                    text = text.substring(0, text.length - 1);
                                    byteAmount = getShiftJISByteLength(text);
                                    document.getElementById("<%= TB_Juusho2 . ClientID%>").value = text;
                                }
                            });



                            $('#<%= TB_sTOKKIJIKOU . ClientID%>').on('keyup keydown', function (e) {
                                var text = $(this).val();
                                var byteAmount = getShiftJISByteLength(text);
                                if (e.keyCode != "Backspace" && e.key != "Delete") {
                                    while (byteAmount > 522) {
                                        text = text.substring(0, text.length - 1);
                                        byteAmount = getShiftJISByteLength(text);
                                        document.getElementById("<%= TB_sTOKKIJIKOU . ClientID%>").value = text;
                                    }
                                }
                            });

                            $('#<%= TB_sTOKKIJIKOU . ClientID%>').on('change', function (e) {
                                var text = $(this).val();
                                var byteAmount = getShiftJISByteLength(text);
                                while (byteAmount > 522) {
                                    text = text.substring(0, text.length - 1);
                                    byteAmount = getShiftJISByteLength(text);
                                    document.getElementById("<%= TB_sTOKKIJIKOU . ClientID%>").value = text;
                            }
                        });
                      
                        $('[id*=txtsTantousha]').on('keyup keydown', function (e) {
                            var text = $(this).val();
                            var byteAmount = getShiftJISByteLength(text);
                            if (e.keyCode != "Backspace" && e.key != "Delete") {
                                while (byteAmount > 32) {
                                    text = text.substring(0, text.length - 1);
                                    byteAmount = getShiftJISByteLength(text);
                                    $("[id*=txtsTantousha]").val(text);
                                }
                            }
                        });

                      
                        $('[id*=txtsBumon]').on('keyup keydown', function (e) {
                            var text = $(this).val();
                            var byteAmount = getShiftJISByteLength(text);
                            if (e.keyCode != "Backspace" && e.key != "Delete") {
                                while (byteAmount > 36) {
                                    text = text.substring(0, text.length - 1);
                                    byteAmount = getShiftJISByteLength(text);
                                    $("[id*=txtsBumon]").val(text);
                                }
                            }
                        });

                        

                        $('[id*=txtsTantoushaFooter]').on('keyup keydown', function (e) {
                            var text = $(this).val();
                            var byteAmount = getShiftJISByteLength(text);
                            if (e.keyCode != "Backspace" && e.key != "Delete") {
                                while (byteAmount > 32) {
                                    text = text.substring(0, text.length - 1);
                                    byteAmount = getShiftJISByteLength(text);
                                    $("[id*=txtsTantoushaFooter]").val(text);
                                }
                            }
                        });

                        $('[id*=txtsBumonFooter]').on('keyup keydown', function (e) {
                            var text = $(this).val();
                            var byteAmount = getShiftJISByteLength(text);
                            if (e.keyCode != "Backspace" && e.key != "Delete") {
                                while (byteAmount > 36) {
                                    text = text.substring(0, text.length - 1);
                                    byteAmount = getShiftJISByteLength(text);
                                    $("[id*=txtsBumonFooter]").val(text);
                                }
                            }
                        });

                    });
                </script>

                <script type="text/javascript">
                   <%--$(function () {

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
                    }--%>
                </script>
                </div>
                 <asp:UpdatePanel runat="server" ID="updLabelSave" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div class="success JCSuccess" id="divLabelSave" runat="server">
                            <asp:Label ID="LB_Save" Text="保存しました。" runat="server" ForeColor="White" Font-Size="13px"></asp:Label>
                            <asp:Button ID="BT_LBSaveCross" Text="✕" runat="server" Style="background-color: white; border-style: none; right: 10px; position: absolute;" OnClick="BT_LBSaveCross_Click" />
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
                 <asp:HiddenField ID="HF_Save" runat="server" />
                 <asp:HiddenField ID="HF_isChange" runat="server" />
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
                        <iframe id="ifShinkiPopup" runat="server" scrolling="yes" style="height: 100vh; width: 100vw;"></iframe>
                         <asp:Button ID="btnClose" runat="server" Text="Button" Style="display: none" OnClick="btnClose_Click" />
                        <asp:Button ID="btnToLogin" runat="server" Text="Button" Style="display: none" OnClick="btnToLogin_Click"/>
                    </asp:Panel>
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>

        <asp:Button ID="btnYes" runat="server" Text="はい" class="BlueBackgroundButton DisplayNone" OnClick="btnYes_Click" Width="100px" Height="36px" />
        <asp:Button ID="btnNo" runat="server" Text="いいえ" class="BlueBackgroundButton DisplayNone" OnClick="btnNo_Click" Width="100px" Height="36px" />
        <asp:Button ID="btnCancel" runat="server" Text="キャンセル" class="JC09GrayButton DisplayNone" Width="100px" Height="36px" OnClientClick="return false;" />
        <asp:Button ID="btnmojiOK" runat="server" Text="はい" class="BlueBackgroundButton DisplayNone" Width="100px" Height="36px" />
        <asp:Button ID="BT_ColumnWidth" runat="server" Text="Button" OnClick="BT_ColumnWidth_Click" Style="display: none;" />
       
        <asp:HiddenField ID="hdnHome" runat="server" />
        <asp:HiddenField ID="HF_flag" runat="server" />
        <asp:HiddenField ID="HF_GridSize" runat="server" />
        <asp:HiddenField ID="HF_Grid" runat="server" />
    </body>
    </html>
</asp:Content>
