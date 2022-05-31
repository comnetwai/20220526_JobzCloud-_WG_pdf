<%@ Page Language="C#" MasterPageFile="~/WebFront/JC99NavBar.Master" AutoEventWireup="true" CodeBehind="JC19TokuisakiSyousai.aspx.cs" Inherits="JobzCloud.WebFront.JC19TokuisakiSyousai" ValidateRequest="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <!DOCTYPE html>
    <html>
    <head>
        <meta http-equiv="Content-Type" content="text/html; charset=utf-8;" />
        <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=0, shrink-to-fit=no" />
        <meta name="google" content="notranslate" />

        <title></title>
        <asp:PlaceHolder runat="server">
            <%: Scripts.Render("~/bundles/modernizr") %>
            <%: Styles.Render("~/style/StyleBundle1") %>
            <%: Scripts.Render("~/scripts/ScriptBundle1") %>
            <%: Styles.Render("~/style/UCStyleBundle") %>

        </asp:PlaceHolder>
        <webopt:BundleReference runat="server" Path="~/Content1/bootstrap" />
        <webopt:BundleReference runat="server" Path="~/Content1/css" />
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

            /*.JC19GrayButton {
                width: 65px;
                background: rgb(242, 242, 242);
                font-size: 13px;
                height: 30px;
                border-radius: 6px;
                border: none;
            }*/

            .JC19GrayButton:hover {
                background: rgb(209,205,205);
            }

            .JC19GridPanel {
                width: 1330px;
                min-width: 1330px;
                max-width: 1330px;
            }

            .JC19GridEditDelteCol {
                width: 300px;
                min-width: 300px;
                max-width: 300px;
            }

            @media only screen and (max-width: 1366px) {
                .JC19GridPanel {
                    width: 1270px;
                    min-width: 1270px;
                    max-width: 1270px;
                }

                .JC19GridEditDelteCol {
                    width: 220px;
                    min-width: 220px;
                    max-width: 220px;
                }
                /*20220218 Added By 　エインドリ－*/
                .grid_header {
                    overflow: auto;
                    white-space: nowrap;
                }

                .JC19DropDown {
                    z-index: 100 !important;
                    overflow: unset !important;
                }
            }
            /*.table-responsive {
            overflow-y: visible !important;
            }
            @media (max-width: 767px) {
            .table-responsive .dropdown-menu {
            position: static !important;
            }
                 }
            @media (min-width: 768px) {
            .table-responsive {
            overflow: inherit;
            }
                }*/
        </style>
        <script type='text/javascript' src="https://netdna.bootstrapcdn.com/bootstrap/3.0.3/js/bootstrap.min.js"></script>
        <script type='text/javascript' src='https://code.jquery.com/jquery-1.8.3.js'></script>
        <script src="../Scripts/bootstrap-datepicker.min.js"></script>
        <script src="https://ajaxzip3.github.io/ajaxzip3.js" charset="UTF-8"></script>

        <script type='text/javascript' src='../Scripts/colResizable-1.6.min.js'></script>
        <%--20220218 Added エインドリ－--%>
        <script type='text/javascript' src='../Scripts/cookie.js'></script>
        <%--20220218 Added エインドリ－--%>

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
                var juusho = document.getElementById("<%=txtsJUUSHO1.ClientID %>");
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
                if (confirm("得意先を入力されています。保存しますか？ ")) {
                    confirm_value.value = "Yes";
                } else {
                    confirm_value.value = "no";
                }

                document.forms[0].appendChild(confirm_value);
            }


        //20211124 Added By エインドリ－.プ－プゥ
        //function EnforceFieldLengthMax(txt) {
        //    var count = getShiftJISByteLength(txt.value);
        //    var str = txt.value;
        //    while (count > 10) {
        //        str = str.substring(0, str.length - 1);
        //        count = getShiftJISByteLength(str);
        //        //document.getElementById("txtsTouisaki").value = str;
        //        return false;
        //    }
        //}
        //$('#txtsTouisaki').on('keyup keydown', function (e) {
        //    //var byteAmount = getShiftJISByteLength(txt.value);
        //    //alert(byteAmount);

        //});
        // //20211124 Added By エインドリ－.プ－プゥ
        //function getShiftJISByteLength(s)
        //{
        //    return s.replace(/[^\x00-\x80&#63728;｡｢｣､･ｦｧｨｩｪｫｬｭｮｯｰｱｲｳｴｵｶｷｸｹｺｻｼｽｾｿﾀﾁﾂﾃﾄﾅﾆﾇﾈﾉﾊﾋﾌﾍﾎﾏﾐﾑﾒﾓﾔﾕﾖﾗﾘﾙﾚﾛﾜﾝ ﾞ ﾟ&#63729;&#63730;&#63731;]/g, 'xx').length;
        //}
        </script>

    </head>
    <body id="BD_Tokuisaki" runat="server" style="background-color: #d7e4f2;">
        <%-- <form id="form1" runat="server">
          <asp:ScriptManager ID="ScriptManager1" EnablePageMethods="True" runat="server">--%>
        <scripts>
                <asp:ScriptReference Name="jquery" />
                <asp:ScriptReference Name="bootstrap" />
                <asp:ScriptReference Path="../Scripts/Common/FixFocus.js" />
            </scripts>
        <%--  </asp:ScriptManager>--%>

        <asp:UpdatePanel ID="updHeader" runat="server" UpdateMode="Conditional">
            <ContentTemplate>

                <div class="JC19TableWidth" id="div3" runat="server">
                    <nav id="nav3" runat="server" class="navbar-expand custom-navbar1 custom-navbar-height" style="position: absolute;">
                        <div class="collapse navbar-collapse JC09navbar JC19TableWidth" id="Div1" runat="server">
                            <label style="font-weight: bold; font-size: 14px; text-align: center; display: inline-block;">得意先</label>
                            <asp:Label ID="lblLoginUserCode" runat="server" Text="" Visible="false" />
                            <asp:Label ID="lblLoginUserName" runat="server" Text="" Visible="false" />

                        </div>
                    </nav>
                </div>
                <div id="Div_Body" runat="server" style="background-color: #d7e4f2; padding-top: 59px; min-height: 100%; margin-bottom: 15px;">
                    <div class="JC19TableWidth" style="background-color: white; padding: 10px 0px 20px 0px;">
                        <%--<div class="form-group row">
                            <div class="col-sm-auto mt-3" style="margin-left:0px;margin-right:0px !important;padding-right:0px !important;">
                                
                            </div>
                            <div class="col-sm-auto mt-3"  style="margin-left:0px;margin-right:0px !important;padding-right:0px !important;">
                                
                            </div>
                        </div>--%>

                        <%--20220201 Added By MyatNoe //「保存しました」green bar message --%>
                        <asp:UpdatePanel runat="server" ID="updLabelSave" UpdateMode="Conditional">
                            <ContentTemplate>
                                <div class="success JCSuccess" id="divLabelSave" runat="server">
                                    <asp:Label ID="LB_Save" Text="保存しました。" runat="server" ForeColor="White" Font-Size="13px"></asp:Label>
                                    <asp:Button ID="BT_LBSaveCross" Text="✕" runat="server" Style="background-color: white; border-style: none; right: 10px; position: absolute;" OnClick="BT_LBSaveCross_Click" />
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>

                        <div id="divPopupHeader" runat="server" style="height: 40px; margin-top: -6px;">
                            <asp:Label ID="lblHeader" runat="server" Text="得意先" CssClass="TitleLabel d-block align-content-left"></asp:Label>
                            <asp:Button ID="btnFusenHeaderCross" runat="server" Text="✕" CssClass="PopupCloseBtn" OnClick="btCancel_Click" />
                        </div>
                        <div id="DivLine" runat="server" class="Borderline"></div>
                        <table style="margin-top: 20px; margin-bottom: 10px;">
                            <tr>
                                <td>
                                    <asp:Button ID="btnhozon" runat="server" Text="保存" type="button " CssClass="BlueBackgroundButton JC10SaveBtn" Style="margin-left: 20px !important;" OnClick="btnhozon_Click" />
                                </td>
                                <td>
                                    <asp:Button ID="btCancel" runat="server" Text="キャンセル" type="button " CssClass="btn text-primary font btn-sm btn btn-outline-primary " Style="width: auto !important; background-color: white; border-radius: 3px; font-size: 13px; padding: 6px 12px 6px 12px; letter-spacing: 1px;" OnClick="btCancel_Click" Height="35px" />
                                </td>
                                <td>
                                    <asp:Button ID="btnshinki" runat="server" Text="得意先を新規作成" type="button " CssClass="BlueBackgroundButton JC10SaveBtn" Style="float: left;" OnClick="btnshinki_Click" />
                                </td>
                            </tr>
                        </table>
                        <table style="margin-left: 20px;">
                            <tr style="height: 40px;">
                                <td style="width: 100px !important;min-width: 100px !important;max-width: 100px !important;">
                                    <label>コード</label>
                                </td>
                                <td style="width: 450px !important;min-width: 450px !important;max-width: 450px !important;">
                                    <asp:Label ID="lblcode" runat="server" Text="" />
                                </td>
                                <td style="width: 100px;width: 100px !important;min-width: 100px !important;max-width: 100px !important;">
                                    <label>請求先</label>
                                </td>
                                <td>
                                    <asp:UpdatePanel ID="upd_TOKUISAKI" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>

                                            <div id="divTokuisakiBtn" runat="server">
                                                <asp:Button ID="btn_tokuisaki" runat="server" Text="追加" CssClass="JC10GrayButton" OnClick="btn_tokuisaki_Click" Height="30px" OnClientClick="PopupOpenButton()"/>
                                            </div>
                                            <div style="float: left; width: 150px; display: none;" id="divTokuisakiLabel" runat="server">
                                                <asp:Label ID="lblsShiiresaki" runat="server" Text="" CssClass="JClinkBtPopup"></asp:Label>
                                                <asp:Label runat="server" ID="lblcShiiresaki" Visible="false"></asp:Label>
                                                <asp:Button ID="BT_sTOKUISAKI_Cross" runat="server" BackColor="White" Text="✕" Style="vertical-align: middle; margin-left: 10px; font-weight: bolder; height: 30px; border: none;" OnClick="BT_sTOKUISAKI_Cross_Click" />
                                            </div>

                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                            <tr style="height: 40px;">
                                <td style="width: 75px;">
                                    <label>得意先名 <span class="fw-bold JC02Star-color">*</span></label>
                                <td style="width: 400px;">
                                    <%--20211124 Added By エインドリ－.プ－プゥ　onkeypress--%> <%--20211130 deleted onkeypress--%>
                                    <asp:TextBox ID="txtsTouisaki" runat="server" type="text" name="txtname" CssClass="form-control TextboxStyle" onfocus="this.setSelectionRange(this.value.length, this.value.length);" Height="26px" OnTextChanged="txtsTouisaki_TextChanged"></asp:TextBox>
                                </td>
                                <td style="width: 75px;">
                                    <label>取引開始日</label>
                                </td>
                                <td>
                                    <asp:UpdatePanel ID="updTokuisakiDate" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:Button ID="btnTokuisakiDate" runat="server" Text="日付を設定" onmousedown="getTantouBoardScrollPosition();" CssClass="JC10GrayButton" Width="90px" OnClick="btnTokuisakiDate_Click" OnClientClick="PopupOpenButton()" Height="30px" />
                                            <div id="divTokuisakiDate" class="DisplayNone" runat="server" style="padding-bottom: 4px;">
                                                <asp:Label ID="lbldTokuisaki" runat="server" Text="" CssClass="GrayLabel"></asp:Label>
                                                <asp:Label ID="lblTokuisakiDateYear" runat="server" CssClass="DisplayNone"></asp:Label>
                                                <asp:Button ID="btndTokuisakiCross" CssClass="CrossBtnGray" runat="server" Text="✕" OnClick="btndTokuisakiCross_Click" />
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                            <tr style="height: 40px;">
                                <td style="width: 70px;">
                                    <label>郵便番号</label>
                                </td>
                                <td style="width: 400px;"><%--20211130 Added MaxLength="7" - エインドリ－・プ－プゥ--%>
                                    <asp:TextBox ID="txtcYUUBIN" runat="server" type="text" name="txtname" MaxLength="7" CssClass="form-control TextboxStyle" autocomplete="off" onKeyUp="ziptoaddr(this);"
                                        onkeypress="return isNumberKey(event)" onfocus="this.setSelectionRange(this.value.length, this.value.length);" Height="26px" Width="100px" OnTextChanged="txtcYUUBIN_TextChanged"></asp:TextBox>
                                </td>
                                <td style="width: 75px;">
                                    <label>営業担当者</label>
                                </td>
                                <td>
                                    <asp:UpdatePanel ID="upd_JISHATANTOUSHA" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>

                                            <div id="divTantousyaBtn" runat="server">
                                                <asp:Button runat="server" ID="BT_JisyaTantousya_Add" Text="追加" CssClass="JC10GrayButton" OnClick="BT_JisyaTantousya_Add_Click" OnClientClick="PopupOpenButton()" Height="30px" />
                                            </div>
                                            <div style="float: left; min-width: 400px; display: none;" id="divTantousyaLabel" runat="server">
                                                <asp:Label ID="lblsJISHATANTOUSHA" runat="server" CssClass="JClinkBtPopup"></asp:Label>
                                                <asp:Label ID="lblcJISHATANTOUSHA" runat="server" Visible="false"></asp:Label>
                                                <asp:Button ID="BT_sJISHATANTOUSHA_Cross" runat="server" BackColor="White" Text="✕" Style="vertical-align: middle; margin-left: 10px; font-weight: bolder; height: 30px; border: none;" OnClick="BT_sJISHATANTOUSHA_Cross_Click" />
                                            </div>
                                            <asp:LinkButton ID="LB_cJISHATANTOUSHA" runat="server" ForeColor="#0080C0" Font-Underline="false" Visible="false">9999</asp:LinkButton>


                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                            <tr style="height: 40px;">
                                <td style="width: 70px;">
                                    <label>住所1</label>
                                </td>
                                <td style="width: 400px;"><%--20211130 - Deleted onkeypress method - エインドリ－・プ－プゥ--%>
                                    <asp:TextBox ID="txtsJUUSHO1" runat="server" type="text" name="txtname" CssClass="form-control TextboxStyle" autocomplete="off" onfocus="this.setSelectionRange(this.value.length, this.value.length);" Height="26px" OnTextChanged="txtsJUUSHO1_TextChanged"></asp:TextBox>
                                    <%--<asp:TextBox ID="txtsJUUSHO1" runat="server" type="text" name="txtname" CssClass="form-control TextboxStyle" autocomplete="off" onkeypress="return isNumberKey(event)" AutoPostBack="true" onfocus="this.setSelectionRange(this.value.length, this.value.length);" Height="26px"></asp:TextBox>--%>
                                </td>
                                <td style="width: 75px;">
                                    <label>特記事項</label>
                                </td>
                                <td rowspan="3">
                                    <asp:TextBox ID="txtsTOKKIJIKOU" runat="server" CssClass="form-control TextboxStyle noResize" TextMode="MultiLine" Rows="5" Columns="20" onfocus="this.setSelectionRange(this.value.length, this.value.length);" Width="350px" OnTextChanged="txtsTOKKIJIKOU_TextChanged"></asp:TextBox>
                                </td>
                            </tr>
                            <tr style="height: 40px;">
                                <td style="width: 70px;">
                                    <label>住所2</label>
                                </td>
                                <td style="width: 400px;"><%--20211130 - Deleted onkeypress method - エインドリ－・プ－プゥ--%>
                                    <asp:TextBox ID="txtsJUUSHO2" runat="server" type="text" name="txtname" CssClass="form-control TextboxStyle" autocomplete="off" onfocus="this.setSelectionRange(this.value.length, this.value.length);" Height="26px" OnTextChanged="txtsJUUSHO2_TextChanged"></asp:TextBox>
                                    <%--<asp:TextBox ID="txtsJUUSHO2" runat="server" type="text" name="txtname" CssClass="form-control TextboxStyle" autocomplete="off" onkeypress="return isNumberKey(event)" AutoPostBack="true" onfocus="this.setSelectionRange(this.value.length, this.value.length);" Height="26px"></asp:TextBox>--%>
                                </td>
                                <td style="width: 75px;"></td>
                            </tr>
                            <tr style="height: 40px;">
                                <td style="width: 70px;">
                                    <label>電話</label>
                                </td>
                                <td style="width: 400px;" colspan="2"><%--20211130 Added MaxLength="20" - エインドリ－・プ－プゥ--%>
                                    <asp:TextBox ID="txtPhone" runat="server" type="text" name="txtname" MaxLength="20" CssClass="form-control TextboxStyle" autocomplete="off" onkeypress="return isNumberKey(event);" onkeydown="RestrictEnter(event);" onkeyup="textclear();" OnTextChanged="txtPhone_TextChanged" onfocus="this.setSelectionRange(this.value.length, this.value.length);" Height="26px" Width="150px"></asp:TextBox>
                                    <asp:Label ID="lblPhoneError" runat="server" ForeColor="Red"></asp:Label>
                                </td>
                                <td style="width: 75px;"></td>
                            </tr>
                            <tr style="height: 40px;">
                                <td style="width: 70px;">
                                    <label>FAX</label>
                                </td>
                                <td style="width: 400px;"><%--20211130 Added MaxLength="20" - エインドリ－・プ－プゥ--%>
                                    <asp:TextBox ID="txtsFAX" runat="server" type="text" name="txtname" MaxLength="20" CssClass="form-control TextboxStyle" autocomplete="off" onkeypress="return isNumberKey(event)" onfocus="this.setSelectionRange(this.value.length, this.value.length);" Height="26px" Width="150px" OnTextChanged="txtsFAX_TextChanged"></asp:TextBox>
                                </td>
                                <td style="width: 75px;">
                                    <label>支払方法</label>
                                </td>
                                <td>
                                    <asp:UpdatePanel ID="updShiharaiHouhou" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>

                                            <div id="divShiharaibtn" runat="server">
                                                <asp:Button runat="server" ID="btnshiharai" Text="追加" CssClass="JC10GrayButton" OnClick="btnshiharai_Click" OnClientClick="PopupOpenButton()" Height="30px" />
                                            </div>
                                            <div style="float: left; min-width: 400px; display: none;" id="divShiharaiLabel" runat="server">
                                                <asp:Label ID="lblsShihariai" runat="server" CssClass="JClinkBtPopup"></asp:Label>
                                                <asp:Label ID="lblcShiharai" runat="server" Visible="false"></asp:Label>
                                                <asp:Button ID="btnShihariCross" runat="server" BackColor="White" Text="✕" Style="vertical-align: middle; margin-left: 10px; font-weight: bolder; height: 30px; border: none;" OnClick="btnShihariCross_Click" />
                                            </div>


                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                            <tr style="height: 40px;">
                                <td style="width: 70px;">
                                    <label>削除</label>
                                </td>
                                <td style="width: 400px;">
                                    <asp:CheckBox ID="chkdelete" runat="server" TextAlign="Left" CssClass="font font-weight-bold text-center" OnCheckedChanged="chkdelete_Clicked" />
                                </td>
                                <td style="width: 75px;"></td>
                                <td></td>
                            </tr>
                        </table>
                        <div class="JC10Borderline">
                        </div>
                        <div style="display: block;" align="left">
                            <label style="margin-left: 20px; font-weight: bold; margin-top: 10px; font-size: 14px;">得意先担当者</label>
                        </div>
                        <div style="margin-left: 20px; margin-right: 20px;"> <%--20220308 Updated style--%>
                        <div style=" overflow-x: auto; width: 100% !important;">
                            <div style=" background-color: white; width: auto; display: inline-block !important">
                                <%--<div class="d-flex flex-column" style="background-color: white;">--%>
                                <%--<div style="width: auto; overflow-x: auto; margin-left: 20px; margin-right: 20px;">--%>
                                <%--class="JC19GridDiv" delete by MyatNoe //20220202--%>
                                <%--<asp:Panel ID="Panel1" runat="server" CssClass="JC19GridPanel" Style="margin-bottom: 10px; padding-right: 20px;">--%>
                                <%-- margin-left: 20px; delete by Phoo //20220301--%>
                                <%--20220218 Added CssClass エインドリ－--%>

                                <asp:UpdatePanel ID="updMitsumoriSyosaiGrid" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:GridView ID="GridView1" runat="server" CellPadding="0" BorderStyle="none" BorderColor="Transparent" GridLines="Horizontal"
                                            BackColor="White" HeaderStyle-CssClass="GVFixedHeader" AutoGenerateColumns="False"
                                            CssClass="tableclass RowHover GridViewStyle grip" ShowHeaderWhenEmpty="True" OnRowCommand="GridView1_RowCommand" OnRowEditing="GridView1_RowEditing" OnRowCancelingEdit="GridView1_RowCancelingEdit"
                                            OnRowDeleting="GridView1_RowDeleting" OnRowUpdated="GridView1_RowUpdated" OnRowUpdating="GridView1_RowUpdating" AllowSorting="True" OnSorting="GridView1_Sorting" OnRowDataBound="GridView1_RowDataBound1" OnRowCreated="grid_RowCreated">
                                            <HeaderStyle Height="39px" BackColor="#F2F2F2" ForeColor="Black" CssClass="grid_header" />
                                            <RowStyle CssClass="GridRow" Height="37px" />
                                            <SelectedRowStyle BackColor="#EBEBF5" />
                                            <Columns>
                                                <asp:TemplateField HeaderText="No." SortExpression="NJUNBAN">
                                                    <ItemTemplate>
                                                        <div class="grip" style="width:50px !important;min-width:50px !important;max-width:50px !important;text-align: left; padding-right: 2px; display: -webkit-box; -webkit-line-clamp: 1; -webkit-box-orient: vertical; word-break: break-all; text-overflow: ellipsis">
                                                            <%--20220218 エインドリ－ Added Style --%>
                                                            <asp:Label ID="lblnjun" Text=' <%# Eval("NJUNBAN","{0}") %>' CommandArgument='<%# Container.DataItemIndex %>' runat="server" Style="margin-left: 2px;" />
                                                        </div>
                                                    </ItemTemplate>
                                                    <HeaderStyle CssClass="JC18HeaderCol AlignLeft" Width="50px"/>
                                                    <ItemStyle CssClass=" AlignLeft" />
                                                    <%--<FooterStyle CssClass="JC19GridNo AlignLeft" />--%>
                                                    <FooterStyle CssClass=" AlignLeft" />
                                                    <EditItemTemplate>
                                                        <div class="grip" style="width:50px !important;min-width:50px !important;max-width:50px !important;text-align: left; padding-right: 2px; overflow: hidden; display: -webkit-box; -webkit-line-clamp: 1; -webkit-box-orient: vertical; word-break: break-all;">
                                                            <%--20220218 エインドリ－ Added Style --%>
                                                            <%--<asp:TextBox ID="txtnjunban" runat="server" CssClass="form-control TextboxStyle" Style="font-size: 14px; font-weight: normal; width: 58px; height: 25px;" ReadOnly="true" Text=' <%# Eval("NJUNBAN","{0}") %>'></asp:TextBox>--%>
                                                            <%--<asp:TextBox ID="txtnjunban" runat="server" CssClass="form-control TextboxStyle JC10GridTextBox" Style="font-size: 14px; font-weight: normal; height: 25px;" ReadOnly="true" Text=' <%# Eval("NJUNBAN","{0}") %>'></asp:TextBox>--%>
                                                            <asp:Label ID="lblnjunban" Text=' <%# Eval("NJUNBAN","{0}") %>' runat="server" Style="font-size: 14px; font-weight: normal; margin-left: 2px;" />
                                                        </div>
                                                    </EditItemTemplate>
                                                    <FooterTemplate>
                                                        <div class="grip" style="height:39px;width:50px !important;min-width:50px !important;max-width:50px !important;text-align: left; padding-right: 2px; overflow: hidden; display: -webkit-box; -webkit-line-clamp: 1; -webkit-box-orient: vertical; word-break: break-all;padding-top:4px;">
                                                            <%--20220218 エインドリ－ Added Style --%>
                                                            <%--<asp:TextBox ID="txtnjunbanFooter" runat="server" CssClass="form-control TextboxStyle" Style="font-size: 14px; font-weight: normal; width: 58px; height: 25px;" ReadOnly="true" />--%>
                                                            <%--<asp:TextBox ID="txtnjunbanFooter" runat="server" CssClass="form-control TextboxStyle JC10GridTextBox" Style="font-size: 14px; font-weight: normal; height: 25px;" ReadOnly="true" />--%>
                                                            <asp:Label ID="lblnjunbanFooter" Text=' <%# Eval("NJUNBAN","{0}") %>' runat="server" Style="font-size: 14px; font-weight: normal; margin-left: 2px;" />
                                                            <%--20220225 Added エインドリ－--%> <%--20220228 Added Phoo－--%>
                                                        </div>
                                                    </FooterTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="担当者名" SortExpression="STANTOU">
                                                    <ItemTemplate>
                                                        <%--<div class="JC19txtTantou" style="width: 318px; overflow: hidden; white-space: nowrap; text-overflow: ellipsis; display: block; height: 37px; padding-top: 18px;">--%>
                                                        <div class="grip" style="width:180px !important;min-width:180px !important;max-width:180px !important;text-align: left; padding-right: 4px; overflow: hidden; display: -webkit-box; -webkit-line-clamp: 1; -webkit-box-orient: vertical; word-break: break-all;">
                                                            <%--20220218 エインドリ－ Updated Style --%>
                                                            <asp:Label runat="server" Text=' <%# Eval("STANTOU","{0}") %>' CommandArgument='<%# Container.DataItemIndex %>' ID="STANTOU" />
                                                        </div>
                                                    </ItemTemplate>
                                                    <%-- <HeaderStyle CssClass="JC19TantouNamHeaderCol JC19TokuiGridHeaderStyle AlignLeft" />--%>
                                                    <HeaderStyle CssClass="JC18HeaderCol AlignLeft" Width="180px"/>
                                                    <ItemStyle CssClass="AlignLeft" />
                                                    <EditItemTemplate>
                                                        <div class="grip" style="width:180px !important;min-width:180px !important;max-width:180px !important;text-align: left; padding-right: 4px; overflow: hidden; display: -webkit-box; -webkit-line-clamp: 1; -webkit-box-orient: vertical; word-break: break-all;">
                                                            <%--20220218 エインドリ－ Added Style --%>
                                                            <asp:TextBox ID="txtsTantousha" runat="server" Text=' <%# Eval("STANTOU","{0}") %>' CssClass="form-control TextboxStyle JC10GridTextBox" Style="font-size: 14px; font-weight: normal; height: 25px;"></asp:TextBox>
                                                        </div>
                                                    </EditItemTemplate>
                                                    <FooterTemplate>
                                                        <div class="grip" style="height:39px;width:180px !important;min-width:180px !important;max-width:180px !important;text-align: left; padding-right: 4px; overflow: hidden; display: -webkit-box; -webkit-line-clamp: 1; -webkit-box-orient: vertical; word-break: break-all;">
                                                           
                                                            <asp:TextBox ID="txtsTantoushaFooter" runat="server" CssClass="form-control TextboxStyle JC10GridTextBox" Width="100%" Style="font-size: 14px; font-weight: normal; height: 25px;margin-top: 7px;" />
                                                           
                                                        </div>
                                                    </FooterTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="部門" SortExpression="SBUMON">
                                                    <ItemTemplate>
                                                        <%--<div class="JC19txtTantou" style="overflow: hidden; white-space: nowrap; text-overflow: ellipsis; display: block; height: 37px; padding-top: 18px;">--%>
                                                        <div class="grip" style="width:260px !important;min-width:260px !important;max-width:260px !important;text-align: left; padding-right: 4px; overflow: hidden; display: -webkit-box; -webkit-line-clamp: 1; -webkit-box-orient: vertical; word-break: break-all;">
                                                            <asp:Label runat="server" class="JC19txtTantou" Text=' <%# Eval("SBUMON","{0}") %>' CommandArgument='<%# Container.DataItemIndex %>' ID="SBUMON" />
                                                        </div>
                                                    </ItemTemplate>
                                                    <%--<HeaderStyle CssClass="JC19TantouNamHeaderCol JC19TokuiGridHeaderStyle AlignLeft" />--%>
                                                    <HeaderStyle CssClass="JC18HeaderCol AlignLeft" Width="260px"/>
                                                    <ItemStyle CssClass="AlignLeft" />
                                                    <EditItemTemplate>
                                                        <div class="grip" style="width:260px !important;min-width:260px !important;max-width:260px !important;text-align: left; padding-right: 4px; overflow: hidden; display: -webkit-box; -webkit-line-clamp: 1; -webkit-box-orient: vertical; word-break: break-all;">
                                                            <asp:TextBox ID="txtsBumon" runat="server" Text=' <%# Eval("SBUMON","{0}") %>' CommandArgument='<%# Container.DataItemIndex %>' CssClass="form-control TextboxStyle JC10GridTextBox" Style="font-size: 14px; font-weight: normal; height: 25px;"></asp:TextBox>
                                                        </div>
                                                    </EditItemTemplate>
                                                    <FooterTemplate>
                                                        <div class="grip" style="height:39px;width:260px !important;min-width:260px !important;max-width:260px !important;text-align: left; padding-right: 4px; overflow: hidden; display: -webkit-box; -webkit-line-clamp: 1; -webkit-box-orient: vertical; word-break: break-all;">
                                                           <asp:TextBox ID="txtsBumonFooter" runat="server" CssClass="form-control TextboxStyle JC10GridTextBox" Style="font-size: 14px; font-weight: normal; height: 25px; margin-top: 7px;" />
                                                          </div>
                                                    </FooterTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="役職" SortExpression="SYAKUSHOKU">
                                                    <ItemTemplate>
                                                        <%--<div style="width: 210px; overflow: hidden; white-space: nowrap; text-overflow: ellipsis; display: block; height: 37px; padding-top: 18px;">--%>
                                                        <div class="grip" style="width:130px !important;min-width:130px !important;max-width:130px !important;text-align: left; padding-right: 4px; overflow: hidden; display: -webkit-box; -webkit-line-clamp: 1; -webkit-box-orient: vertical; word-break: break-all;">
                                                           <asp:Label runat="server" Text=' <%# Eval("SYAKUSHOKU","{0}") %>' CommandArgument='<%# Container.DataItemIndex %>' ID="SYAKUSHOKU" />
                                                        </div>
                                                    </ItemTemplate>
                                                    <HeaderStyle CssClass="JC18HeaderCol" Width="130px"/>
                                                    <ItemStyle CssClass="AlignLeft" />
                                                    <EditItemTemplate>
                                                        <div class="grip" style="width:130px !important;min-width:130px !important;max-width:130px !important;text-align: left; padding-right: 4px; overflow: hidden; display: -webkit-box; -webkit-line-clamp: 1; -webkit-box-orient: vertical; word-break: break-all;">
                                                            <%--<asp:TextBox ID="txtsYakushoku" runat="server" MaxLength="20" Text=' <%# Eval("SYAKUSHOKU","{0}") %>' CssClass="form-control TextboxStyle" Style="font-size: 14px; font-weight: normal; width: 210px; height: 25px;"></asp:TextBox>--%>
                                                            <asp:TextBox ID="txtsYakushoku" runat="server" MaxLength="20" Text=' <%# Eval("SYAKUSHOKU","{0}") %>' CommandArgument='<%# Container.DataItemIndex %>' CssClass="form-control TextboxStyle JC10GridTextBox" Style="font-size: 14px; font-weight: normal; height: 25px;"></asp:TextBox>
                                                        </div>
                                                    </EditItemTemplate>
                                                    <FooterTemplate>
                                                        <div class="grip" style="height:39px;width:130px !important;min-width:130px !important;max-width:130px !important;text-align: left; padding-right: 4px; overflow: hidden; display: -webkit-box; -webkit-line-clamp: 1; -webkit-box-orient: vertical; word-break: break-all;">
                                                            <%--20220218 エインドリ－ Added Style --%>
                                                            <%--<asp:TextBox ID="txtsYakushokuFooter" runat="server" MaxLength="20" CssClass="form-control TextboxStyle" Style="font-size: 14px; font-weight: normal; width: 210px; height: 25px;" />--%>
                                                            <asp:TextBox ID="txtsYakushokuFooter" runat="server" MaxLength="20" CssClass="form-control TextboxStyle JC10GridTextBox" Style="font-size: 14px; font-weight: normal; height: 25px; margin-top: 7px;" />
                                                            <%--20220228 Added Phoo－--%>
                                                        </div>
                                                    </FooterTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="電話番号"  HeaderStyle-Height="37px" SortExpression="STEL">
                                                    <ItemTemplate>
                                                        <%--<div style="width: 210px; overflow: hidden; white-space: nowrap; text-overflow: ellipsis; display: block; height: 37px; padding-top: 18px;">--%>
                                                        <div class="grip" style="width:135px !important;min-width:135px !important;max-width:135px !important;text-align: left; padding-right: 4px; overflow: hidden; display: -webkit-box; -webkit-line-clamp: 1; -webkit-box-orient: vertical; word-break: break-all;">
                                                            <asp:Label runat="server" Text=' <%# Eval("STEL","{0}") %>' CommandArgument='<%# Container.DataItemIndex %>' ID="STEL" />
                                                        </div>
                                                    </ItemTemplate>
                                                    <HeaderStyle CssClass="JC18HeaderCol" Width="135px"/>
                                                    <ItemStyle CssClass="AlignLeft" />
                                                    <EditItemTemplate>
                                                        <%--20211201 Added MaxLength & onkeypress エインドリ－・プ－プゥ--%>
                                                        <div class="grip" style="width:135px !important;min-width:135px !important;max-width:135px !important;text-align: left; padding-right: 4px; overflow: hidden; display: -webkit-box; -webkit-line-clamp: 1; -webkit-box-orient: vertical; word-break: break-all;">
                                                            <%--20220218 エインドリ－ Added Style --%>
                                                            <%--<asp:TextBox ID="txtsTel" runat="server" Text=' <%# Eval("STEL","{0}") %>' MaxLength="20" onkeypress="return isNumberKey(event)" CssClass="form-control TextboxStyle" Style="font-size: 14px; font-weight: normal; width: 188px; height: 25px;"></asp:TextBox>--%>
                                                            <asp:TextBox ID="txtsTel" runat="server" Text=' <%# Eval("STEL","{0}") %>' CommandArgument='<%# Container.DataItemIndex %>' MaxLength="20" onkeypress="return isNumberKey(event)" CssClass="form-control TextboxStyle JC10GridTextBox" Style="font-size: 14px; font-weight: normal; height: 25px;"></asp:TextBox>
                                                        </div>
                                                    </EditItemTemplate>
                                                    <FooterTemplate>
                                                        <%--20211130 Added MaxLength="20" - エインドリ－・プ－プゥ--%><%--20211130 Added onkeypress エインドリ－・プ－プゥ--%>
                                                        <div class="grip" style="height:39px;width:135px !important;min-width:135px !important;max-width:135px !important;text-align: left; padding-right: 4px; overflow: hidden; display: -webkit-box; -webkit-line-clamp: 1; -webkit-box-orient: vertical; word-break: break-all;">
                                                            <%--20220218 エインドリ－ Added Style --%>
                                                            <%--<asp:TextBox ID="txtsTelFooter" runat="server" MaxLength="20" CssClass="form-control TextboxStyle" onkeypress="return isNumberKey(event)" Style="font-size: 14px; font-weight: normal; width: 188px; height: 25px;" />--%>
                                                            <asp:TextBox ID="txtsTelFooter" runat="server" MaxLength="20" CssClass="form-control TextboxStyle JC10GridTextBox " onkeypress="return isNumberKey(event)" Style="font-size: 14px; font-weight: normal; height: 25px; margin-top: 7px;" />
                                                            <%--20220228 Added Phoo－--%>
                                                        </div>
                                                    </FooterTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="敬称" SortExpression="SKEISHOU">
                                                    <ItemTemplate>
                                                        <%--<div style="width: 100px; overflow: hidden; white-space: nowrap; text-overflow: ellipsis; display: block; height: 37px; padding-top: 18px;">--%>
                                                        <div class="grip" style="width:90px !important;min-width:90px !important;max-width:90px !important;text-align: left; padding-right: 4px; overflow: hidden; display: -webkit-box; -webkit-line-clamp: 1; -webkit-box-orient: vertical; word-break: break-all;">
                                                            
                                                            <asp:Label runat="server" Text=' <%# Eval("SKEISHOU","{0}") %>' CommandArgument='<%# Container.DataItemIndex %>' ID="SKEISHOU" />
                                                        </div>
                                                    </ItemTemplate>
                                                    <HeaderStyle CssClass="JC18HeaderCol" Width="90px"/>
                                                    
                                                    <ItemStyle CssClass="AlignLeft" />
                                                    <EditItemTemplate>
                                                        <div class="grip" style="width:90px !important;min-width:90px !important;max-width:90px !important;text-align: left; padding-right: 4px; overflow: hidden; display: -webkit-box; -webkit-line-clamp: 1; -webkit-box-orient: vertical; word-break: break-all;">
                                                            
                                                            <asp:TextBox ID="txtsKeishou" runat="server" Text=' <%# Eval("SKEISHOU","{0}") %>' CommandArgument='<%# Container.DataItemIndex %>' CssClass="form-control TextboxStyle JC10GridTextBox" Style="font-size: 14px; font-weight: normal; height: 25px;"></asp:TextBox>
                                                        </div>
                                                    </EditItemTemplate>
                                                    <FooterTemplate>
                                                        <div class="grip" style="height:39px;width:90px !important;min-width:90px !important;max-width:90px !important;text-align: left; padding-right: 4px; overflow: hidden; display: -webkit-box; -webkit-line-clamp: 1; -webkit-box-orient: vertical; word-break: break-all;">
                                                            <%--20220218 エインドリ－ Added Style --%>
                                                            <%--<asp:TextBox ID="txtsKeishouFooter" runat="server" CssClass="form-control TextboxStyle" Style="font-size: 14px; font-weight: normal;  width: 98px;height: 25px;" />--%>
                                                            <asp:TextBox ID="txtsKeishouFooter" runat="server" CssClass="form-control TextboxStyle JC10GridTextBox" Style="font-size: 14px; font-weight: normal; height: 25px; margin-top: 7px;" />
                                                            <%--20220228 Added Phoo－--%>
                                                        </div>
                                                    </FooterTemplate>
                                                </asp:TemplateField>

                                                <asp:TemplateField HeaderText="">
                                                    <ItemTemplate>
                                                        <%-- 20220304 MyatNoe Updated--%>
                                                         <div style="text-align: center;min-width: 160px !important; width:160px !important;max-width:160px !important;">
                                                           <%-- <asp:HoverMenuExtender ID="HoverMenuExtender2" runat="server" PopupControlID="PopupMenu"
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
                                                            <asp:Button ID="btnDelete" runat="server" CommandName="Delete" Text="削除" float="left" CssClass="JC09GridGrayBtn" Width="50px" Height="28px" />
                                                        </div>

                                                    </ItemTemplate>
                                                    <HeaderStyle CssClass="JC18HeaderCol AlignRight" Width="160px"/>
                                                    <ItemStyle CssClass="JC09DropDown AlignCenter" />
                                                    <EditItemTemplate>
                                                        <div style="text-align: center;min-width: 160px !important; width:160px !important;max-width:160px !important;">
                                                            <asp:Button ID="btnUpdate" runat="server" CommandName="Update" Text="更新" CssClass="JC09GridGrayBtn" Width="50px" Height="28px" />
                                                            <asp:Button ID="btnCancel" runat="server" CommandName="Cancel" Text="キャンセル" float="left" CssClass="JC09GridGrayBtn" Width="90px" Height="28px" />
                                                        </div>
                                                    </EditItemTemplate>
                                                    <FooterTemplate>
                                                        <div style="text-align: center;min-width: 160px !important; width:160px !important;max-width:160px !important;height:39px;">
                                                            <%--20220225 Myat Noe Added Style--%> <%--20220228 Updated (margin-top) エインドリ－--%>
                                                            <asp:Button ID="btnSave" runat="server" CommandName="Save" Text="追加" CssClass="JC09GridGrayBtn" Width="50px" Height="28px" Style="margin-top: 5px;" />
                                                            <asp:Button ID="btnCancel" runat="server" CommandName="Cancel" Text="キャンセル" float="left" CssClass="JC09GridGrayBtn" Width="90px" Height="28px" Style="margin-top: 5px;" />
                                                        </div>
                                                    </FooterTemplate>
                                                </asp:TemplateField>
                                            </Columns>

                                            <FooterStyle ForeColor="#D7DBDD" Height="39px" CssClass="GridRow" />
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
                                <%--</asp:Panel>--%>
                            </div>
                        </div>
                              </div>
                        <div class="d-flex" style="padding-bottom: 5px; padding-top: 7px;">
                            <div style="margin-left: 20px;">
                                <asp:Button ID="btnaddcust" runat="server" Text="＋　得意先担当者を追加" CssClass="BlueBackgroundButton" Width="200px" Height="36px" OnClick="btnaddcust_Click1" />
                            </div>
                        </div>

                    </div>
                <script type="text/javascript">　//20211130 - Added by エインドリ－・プ－プゥ　(start)
                    Sys.Application.add_load(function () {
                        function getShiftJISByteLength(s) {
                            return s.replace(/[^\x00-\x80&#63728;｡｢｣､･ｦｧｨｩｪｫｬｭｮｯｰｱｲｳｴｵｶｷｸｹｺｻｼｽｾｿﾀﾁﾂﾃﾄﾅﾆﾇﾈﾉﾊﾋﾌﾍﾎﾏﾐﾑﾒﾓﾔﾕﾖﾗﾘﾙﾚﾛﾜﾝ ﾞ ﾟ&#63729;&#63730;&#63731;]/g, 'xx').length;
                        }

                        $('#<%= txtsTouisaki . ClientID%>').on('keyup keydown', function (e) {
                                var text = $(this).val();
                                var byteAmount = getShiftJISByteLength(text);
                                if (e.keyCode != "Backspace" && e.key != "Delete") {
                                    while (byteAmount > 60) {
                                        text = text.substring(0, text.length - 1);
                                        byteAmount = getShiftJISByteLength(text);
                                        document.getElementById("<%= txtsTouisaki . ClientID%>").value = text;
                                    }
                                }
                            });

                            $('#<%= txtsTouisaki . ClientID%>').on('change', function (e) {
                                var text = $(this).val();
                                var byteAmount = getShiftJISByteLength(text);
                                while (byteAmount > 60) {
                                    text = text.substring(0, text.length - 1);
                                    byteAmount = getShiftJISByteLength(text);
                                    document.getElementById("<%= txtsTouisaki . ClientID%>").value = text;
                                }
                            });

                            $('#<%= txtsJUUSHO1 . ClientID%>').on('keyup keydown', function (e) {
                                var text = $(this).val();
                                var byteAmount = getShiftJISByteLength(text);
                                if (e.keyCode != "Backspace" && e.key != "Delete") {
                                    while (byteAmount > 40) {
                                        text = text.substring(0, text.length - 1);
                                        byteAmount = getShiftJISByteLength(text);
                                        document.getElementById("<%= txtsJUUSHO1 . ClientID%>").value = text;
                                    }
                                }
                            });

                            $('#<%= txtsJUUSHO1 . ClientID%>').on('change', function (e) {
                                var text = $(this).val();
                                var byteAmount = getShiftJISByteLength(text);
                                while (byteAmount > 40) {
                                    text = text.substring(0, text.length - 1);
                                    byteAmount = getShiftJISByteLength(text);
                                    document.getElementById("<%= txtsJUUSHO1 . ClientID%>").value = text;
                                }
                            });

                            $('#<%= txtsJUUSHO2 . ClientID%>').on('keyup keydown', function (e) {
                                var text = $(this).val();
                                var byteAmount = getShiftJISByteLength(text);
                                if (e.keyCode != "Backspace" && e.key != "Delete") {
                                    while (byteAmount > 40) {
                                        text = text.substring(0, text.length - 1);
                                        byteAmount = getShiftJISByteLength(text);
                                        document.getElementById("<%= txtsJUUSHO2 . ClientID%>").value = text;
                                    }
                                }
                            });

                            $('#<%= txtsJUUSHO2 . ClientID%>').on('change', function (e) {
                                var text = $(this).val();
                                var byteAmount = getShiftJISByteLength(text);
                                while (byteAmount > 40) {
                                    text = text.substring(0, text.length - 1);
                                    byteAmount = getShiftJISByteLength(text);
                                    document.getElementById("<%= txtsJUUSHO2 . ClientID%>").value = text;
                                }
                            });



                            $('#<%= txtsTOKKIJIKOU . ClientID%>').on('keyup keydown', function (e) {
                                var text = $(this).val();
                                var byteAmount = getShiftJISByteLength(text);
                                if (e.keyCode != "Backspace" && e.key != "Delete") {
                                    while (byteAmount > 522) {
                                        text = text.substring(0, text.length - 1);
                                        byteAmount = getShiftJISByteLength(text);
                                        document.getElementById("<%= txtsTOKKIJIKOU . ClientID%>").value = text;
                                    }
                                }
                            });

                            $('#<%= txtsTOKKIJIKOU . ClientID%>').on('change', function (e) {
                                var text = $(this).val();
                                var byteAmount = getShiftJISByteLength(text);
                                while (byteAmount > 522) {
                                    text = text.substring(0, text.length - 1);
                                    byteAmount = getShiftJISByteLength(text);
                                    document.getElementById("<%= txtsTOKKIJIKOU . ClientID%>").value = text;
                            }
                        });
                        //20211201 Added By エインドリ－・プ－プゥ
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

                        //20211201 Added By エインドリ－・プ－プゥ
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

                        //20211201 Added By エインドリ－・プ－プゥ
                        $('[id*=txtsKeishou]').on('keyup keydown', function (e) {
                            var text = $(this).val();
                            var byteAmount = getShiftJISByteLength(text);
                            if (e.keyCode != "Backspace" && e.key != "Delete") {
                                while (byteAmount > 4) {
                                    text = text.substring(0, text.length - 1);
                                    byteAmount = getShiftJISByteLength(text);
                                    $("[id*=txtsKeishou]").val(text);
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

                        $('[id*=txtsKeishouFooter]').on('keyup keydown', function (e) {
                            var text = $(this).val();
                            var byteAmount = getShiftJISByteLength(text);
                            if (e.keyCode != "Backspace" && e.key != "Delete") {
                                while (byteAmount > 4) {
                                    text = text.substring(0, text.length - 1);
                                    byteAmount = getShiftJISByteLength(text);
                                    $("[id*=txtsKeishouFooter]").val(text);
                                }
                            }
                        });
                    });
                </script>

                <%--20220218 エインドリ－ Added Start --%>
                <%--$(function () {
                            if ($.cookie('colWidthTokuisakiSyousai') != null) {
                                var columns = $.cookie('colWidthTokuisakiSyousai').split(',');
                                var i = 0;
                                $('.GridViewStyle th').each(function () {
                                    $(this).width(columns[i++]);
                                    //$("[id*=txtsTantoushaFooter]").css('width', $(this).width(columns[1]));
                                });

                            }--%>
                <%--20220301 Phoo Added  --%>
                <%--else {
                                //var columns = [30, 33, 95, 450, 140, 140, 95, 110, 100, 110, 30];

                                var columns = [30, 655, 255, 200, 180, 150, 160];
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
                            date.setTime(date.getTime() + (1*60*1000));
                            columns.each(function () {
                                msg += $(this).width() + ",";
                            })
                            $.cookie("colWidthTokuisakiSyousai", msg, { expires: date }); // expires after 20 minutes
                        };

                        var prm = Sys.WebForms.PageRequestManager.getInstance();
                        if (prm != null) {
                            prm.add_endRequest(function (sender, e) {
                                if (sender._postBackSettings.panelsToUpdate != null) {
                                    if ($.cookie('colWidthTokuisakiSyousai') != null) {
                                        var columns = $.cookie('colWidthTokuisakiSyousai').split(',');
                                        var i = 0;
                                        $('.GridViewStyle th').each(function () {
                                            $(this).width(columns[i++]);
                                            //$("[id*=txtsTantoushaFooter]").css('width', $(this).width(columns[1]));
                                        });
                                    }--%>
                <%-- Updated by Phoo //20220301--%>

                <%--else {
                                        //var columns = [30, 33, 95, 450, 140, 140, 95, 110, 100, 110, 30];
                                        //var columns = [30,650, 250, 200, 170, 110, 30];
                                        var columns = [30, 655, 255, 200, 180, 150, 160];
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
                        };--%>

                <%--// $('.table-responsive').on('show.bs.dropdown', function () {
                       // $('.table-responsive').css( "overflow", "inherit" );
                       // });

                       // $('.table-responsive').on('hide.bs.dropdown', function () {
                       // $('.table-responsive').css( "overflow", "auto" );
                       //})--%>
                <%--20220218 エインドリ－ Added End --%>

                <%--<script type="text/javascript">
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
                </script>--%>

                <%--20211130 - Added by エインドリ－・プ－プゥ　(end)--%>
                </div>
                <asp:Button ID="btn_hide" runat="server" Text="Button" CssClass="DisplayNone" />

                <asp:HiddenField ID="HF_isChange" runat="server" />
                <asp:HiddenField ID="HF_Save" runat="server" />
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
                        <asp:Button ID="btn_CloseMitumoriSearch" runat="server" Text="Button" CssClass="DisplayNone" OnClick="btn_CloseMitumoriSearch_Click" onClientClick="MasterOverflow()"/>
                        <asp:Button ID="btn_CloseTokuisakiSentaku" runat="server" Text="Button" CssClass="DisplayNone" OnClick="btn_CloseTokuisakiSentaku_Click" onClientClick="MasterOverflow()"/>
                    </asp:Panel>
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>


        <!--Date-->
        <asp:UpdatePanel ID="updDatePopup" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:Button ID="btnDatePopup" runat="server" Text="Button" CssClass="DisplayNone" />
                <asp:ModalPopupExtender ID="mpeDatePopup" runat="server" TargetControlID="btnDatePopup"
                    PopupControlID="pnlDatePopupScroll" BackgroundCssClass="PopupModalBackground" BehaviorID="pnlDatePopupScroll"
                    RepositionMode="None">
                </asp:ModalPopupExtender>
                <asp:Panel ID="pnlDatePopupScroll" runat="server" Style="display: none;" CssClass="PopupScrollDiv">
                    <asp:Panel ID="pnlDatePopup" runat="server">
                        <iframe id="ifDatePopup" runat="server" class="NyuryokuIframe RadiusIframe" scrolling="no" style="max-width: 260px; min-width: 260px; max-height: 365px; min-height: 365px;"></iframe>
                        <asp:Button ID="btnCalendarClose" runat="server" Text="Button" CssClass="DisplayNone" OnClick="btnCalendarClose_Click" onClientClick="MasterOverflow()"/>
                        <asp:Button ID="btnCalendarSettei" runat="server" Text="Button" CssClass="DisplayNone" OnClick="btnCalendarSettei_Click" onClientClick="MasterOverflow()"/>
                        <asp:Button ID="btnTokuisakiClose" runat="server" Text="Button" Style="display: none" onClientClick="MasterOverflow()"/>
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
                <asp:Panel ID="pnlSentakuPopupScroll" runat="server" Style="display: none;overflow:hidden;" CssClass="PopupScrollDiv">
                    <asp:Panel ID="pnlSentakuPopup" runat="server">
                        <iframe id="ifSentakuPopup" runat="server" scrolling="no" class="NyuryokuIframe" style="border-radius: 0px;"></iframe>
                        <asp:Button ID="btnClose" runat="server" Text="Button" Style="display: none" OnClick="btnClose_Click" onClientClick="MasterOverflow()"/>
                        <asp:Button ID="btnTokuisakiSelect" runat="server" Text="Button" Style="display: none" OnClick="btnTokuisakiSelect_Click" onClientClick="MasterOverflow()"/>
                        <asp:Button ID="btnJishaTantouSelect" runat="server" Text="Button" Style="display: none" OnClick="btnJishaTantouSelect_Click" onClientClick="MasterOverflow()"/>
                        <asp:Button ID="btnshiarailistSelect" runat="server" Text="Button" Style="display: none" OnClick="btnshiarailistSelect_Click" onClientClick="MasterOverflow()"/>
                        <asp:Button ID="btnToLogin" runat="server" Text="Button" Style="display: none" OnClick="btnToLogin_Click" onClientClick="MasterOverflow()"/>
                    </asp:Panel>
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>

        <%--20220201 Added By MyatNoe //kakunin messagebox btn --%>
        <asp:Button ID="btnYes" runat="server" Text="はい" class="BlueBackgroundButton DisplayNone" OnClick="btnYes_Click" Width="100px" Height="36px" />
        <asp:Button ID="btnNo" runat="server" Text="いいえ" class="BlueBackgroundButton DisplayNone" OnClick="btnNo_Click" Width="100px" Height="36px" />
        <asp:Button ID="btnCancel" runat="server" Text="キャンセル" class="JC09GrayButton DisplayNone" Width="100px" Height="36px" OnClientClick="return false;" />
        <asp:Button ID="btnmojiOK" runat="server" Text="はい" class="BlueBackgroundButton DisplayNone" Width="100px" Height="36px" />
        <asp:Button ID="BT_ColumnWidth" runat="server" Text="Button" OnClick="BT_ColumnWidth_Click" Style="display: none;" />

        <asp:HiddenField ID="hdnHome" runat="server" />
        <asp:HiddenField ID="HF_flag" runat="server" />
        <asp:HiddenField ID="HF_GridSize" runat="server" />
        <asp:HiddenField ID="HF_Grid" runat="server" />
        <%-- </form>--%>
    </body>
    </html>
</asp:Content>
