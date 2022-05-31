<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="JC20TokuisakiTantouKensaku.aspx.cs" Inherits="JobzCloud.WebFront.JC20TokuisakiTantouKensaku" EnableEventValidation="false" ValidateRequest="False" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=0, shrink-to-fit=no" /> <%--20220307 Added エインドリ－--%>
    <title></title>
    <asp:PlaceHolder runat="server">
        <%: Scripts.Render("~/bundles/modernizr") %>
        <%: Styles.Render("~/style/StyleBundle1") %>
        <%: Scripts.Render("~/scripts/ScriptBundle1") %>
        <%: Styles.Render("~/style/UCStyleBundle") %>

    </asp:PlaceHolder>
    <link href="../Content1/bootstrap.min.css" rel="stylesheet" />
    <%--    <script src="../Scripts/bootstrap.bundle.min.js"></script>--%>
    <style type="text/css">
        .font {
            font-family: "Hiragino Sans", "Open Sans", "Meiryo", "Hiragino Kaku Gothic Pro", "メイリオ", "MS ゴシック", "sans-serif";
            font-size: 13px;
        }
        /* Rounded border */
        hr.rounded {
            border-top: 8px solid #bbb;
            border-radius: 5px;
        }

        /*.GVFixedHeader {
                font-weight: bold;
                position: absolute;
                top: expression(this.parentNode.parentNode.parentNode.scrollTop);
        }*/
        .right {
            position: absolute;
            right: 404px;
            padding: 2px;
        }

        .marginHeader {
            padding-left: 20px;
        }

        @media only screen and (max-width: 600px) {
        }

        th {
            position: -webkit-sticky;
            position: sticky !important;
            top: 0;
            background-color: rgb(242,242,242);
            border-color: rgb(242,242,242);
            border: 0px;
        }
        /*20220222 Added By プ－プゥ*/
        .grid_header {
            overflow: auto;
            white-space: nowrap;
        }

        .JC20HeaderCol {
            line-height: 0px;
            font-size: 13px;
        }
        /*20220303 Added エインドリ－*/
        .JC20HeaderCol th:first-child{
            /*border-top:2px solid white;
            border-left:2px solid pink;
            border-bottom:3px solid white;
             border-right:2px solid white;*/
        }
          #gdvtokuisaki{
    border-color:white;
        margin-left:3px;
    margin-right:3px;
  }

    </style>
</head>
<body id="BD_Master" runat="server" class="bg-transparent">
    <form id="frmtokuisaki" runat="server">
        <asp:ScriptManager ID="ScriptManager1" EnablePageMethods="True" runat="server">
            <Scripts>
                <asp:ScriptReference Name="jquery" />
                <asp:ScriptReference Name="bootstrap" />
                <asp:ScriptReference Path="../Scripts/Common/FixFocus.js" />
                <asp:ScriptReference Path="../Scripts/colResizable-1.6.min.js" />
                <%--20220222 Added プ－プゥ--%>
                <asp:ScriptReference Path="../Scripts/cookie.js" />
                <%--20220222 Added プ－プゥ--%>
            </Scripts>
        </asp:ScriptManager>
        <asp:UpdatePanel ID="updBody" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <%--<div style="max-width: 1160px;background-color: white; margin-left: auto; margin-right: auto; margin-top:20px; padding: 0; left: 50%; top: 50%; position: absolute; transform: translate(-50%, -50%);">--%>
                  <div id="Div_Body" runat="server" style="background-color: transparent; padding-top: 50px; min-height: 100%;"><%--20220304 Added  エインドリ－--%>
                    <div style="max-width: 1160px; margin-left: auto; margin-right: auto; background-color: white; padding: 10px 0px 0px 0px;"><%--20220304 Added  エインドリ－--%>
              
                    <div style="margin-left: auto; margin-right: auto;">
                        <asp:Label ID="Label1" runat="server" Text="得意先担当者を選択" CssClass="TitleLabel" Style="text-align: left;padding-left:3px;"></asp:Label>
                        <asp:Button ID="btnFusenHeaderCross" runat="server" Text="✕" CssClass="PopupCloseBtn" OnClick="btnFusenHeaderCross_Click" />
                    </div>
                    <div class="Borderline"></div>
                    <div style="margin: 15px 20px 20px 20px;">
                        <table style="width: 100% !important; margin-bottom:15px;"> <%--20220306 Updated エインドリ pb->20 to 15－--%>
                            <tr>
                                 <td style="padding-left:3px;"><%--20220203 Updated エインドリ－--%>
                                    <asp:TextBox runat="server" ID="txt_Code" MaxLength="20"
                                        placeholder="コード、名前、電話で検索できます。" CssClass="form-control TextboxStyle JC18TextBox " onfocus="this.setSelectionRange(this.value.length, this.value.length);" AutoPostBack="true" Width="480px" OnTextChanged="txt_Code_TextChanged" TextMode="Search"></asp:TextBox>
                                </td>
                                <td align="right" style="padding-right:4px"><%--20220203 Updated エインドリ－--%>
                                    <%--<asp:Button ID="btnCreate" runat="server" Text="新規得意先担当者を作成" CssClass="BlueBackgroundButton JC10SaveBtn" Width="170px" OnClick="btn_Create_Click" style="margin:0px !important;"/>--%>
                                    <%--20220110 Updated Text in button by エインドリ－--%>
                                    <asp:Button ID="btnCreate" runat="server" Text="得意先担当者を新規作成" CssClass="BlueBackgroundButton JC10SaveBtn" OnClick="btn_Create_Click" Style="margin: 0px !important;" OnClientClick="PopupOpenButton()"/>
                                </td>
                            </tr>
                        </table>

                            <div style="overflow-x:auto;width:100% !important;max-height:500px;">
                                <div id="DIV_GV" runat="server" style="width:auto;display:inline-block !important;">
                        <asp:Panel ID="Panel1" runat="server" Style="max-width: 1900px; min-width: 1100px; ">
                            <asp:GridView runat="server" ID="gdvtokuisaki" BorderColor="Transparent" AutoGenerateColumns="False" EmptyDataRowStyle-CssClass="JC23NoDataMessageStyle" CssClass="RowHover GridViewStyle grip" ShowHeaderWhenEmpty="True" HeaderStyle-CssClass="GVFixedHeader" CellPadding="0" AllowSorting="True" 
                                OnSorting="gdvtokuisaki_Sorting" OnRowCreated="gdvtokuisaki_RowCreated" OnSelectedIndexChanged="gdvtokuisaki_selectedIndexChanged" OnRowDataBound="gdvtokuisaki_rowDataBound" OnPreRender="gdvtokuisaki_PreRender">
                                <EmptyDataRowStyle CssClass="JC23NoDataMessageStyle" />
                                <%--<HeaderStyle Height="43px" BackColor="#F2F2F2" ForeColor="Black" />--%>
                                <HeaderStyle Height="40px" BackColor="#F2F2F2" ForeColor="Black" CssClass="grid_header" />
                                <%--20220222 Updated Phoo--%>
                                <RowStyle Height="43px" CssClass="JC12GridItem font" />
                                <SelectedRowStyle BackColor="#EBEBF5" />
                                <Columns>
                                    <asp:TemplateField HeaderText="担当者" SortExpression="STANTOU" >　<%--20220304 Updated Width エインドリ－--%><ItemTemplate>

                                            <%--<div style="min-width: 230px; max-width: 230px; text-align: left; overflow: hidden; white-space: nowrap; text-overflow: ellipsis; padding-left: 20px; padding-right: 5px;">--%>
                                            <div class="grip" style="text-align: left; padding-right: 4px; overflow: hidden; display: -webkit-box; -webkit-line-clamp: 1; -webkit-box-orient: vertical; word-break: break-all;">
                                                <%--20220222 Phoo Updated Style --%>
                                                <%--<asp:LinkButton runat="server" ID="lnkCode" Text='<%# Eval("STANTOU","{0}") %>'  OnClick="lnkCode_Click" CommandArgument='<%# Container.DataItemIndex %>'/>--%>
                                                <%--20220110 Added by エインドリ－・プ－プゥ--%>
                                                <asp:Label runat="server" ID="lb_stantou" Text='<%# Eval("STANTOU","{0}") %>' CommandArgument='<%# Container.DataItemIndex %>'></asp:Label>
                                                <asp:Label runat="server" ID="lb_nJun" Text='<%# Eval("NJUNBAN","{0}") %>' Style="display: none;" />
                                                <asp:Label runat="server" ID="lb_bumon" Text='<%# Eval("SBUMON","{0}") %>' Style="display: none;" />
                                                <asp:Label runat="server" ID="lb_yakushoku" Text='<%# Eval("SYAKUSHOKU","{0}") %>' Style="display: none;" />
                                                <asp:Label runat="server" ID="lb_keishou" Text='<%# Eval("SKEISHOU","{0}") %>' Style="display: none;" />
                                            </div>
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="JC20HeaderCol"  BorderColor="White" BorderWidth="2px" Wrap="False"></HeaderStyle> <%--20220307 Updated エインドリ－--%>
                                        <%--20220222 Updated Phoo--%>
                                        <%--<HeaderStyle CssClass="JC12MitumoriCodeHeaderCol JC20NameHeaderCol" Width="230px" Height="43px"></HeaderStyle>--%>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="部門" SortExpression="SBUMON" >　<%--20220304 Updated Width エインドリ－--%><ItemTemplate>
                                            <%--<div style="min-width: 195px; max-width: 195px; text-align: left; overflow: hidden; padding-right: 4px; white-space: nowrap; text-overflow: ellipsis;">--%>
                                            <div class="grip" style="text-align: left; padding-right: 4px; overflow: hidden; display: -webkit-box; -webkit-line-clamp: 1; -webkit-box-orient: vertical; word-break: break-all;">
                                                <%--20220217 Phoo Updated Style --%>
                                                <asp:Label ID="lblsToukuisaki" runat="server" Text='<%# Eval("SBUMON","{0}") %>' CommandArgument='<%# Container.DataItemIndex %>' ></asp:Label>

                                            </div>
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="JC20HeaderCol" BorderColor="White" BorderWidth="2px" Wrap="False"></HeaderStyle>
                                        <%--20220222 Updated Phoo--%>
                                        <%--<HeaderStyle CssClass="JC12MitumoriCodeHeaderCol JC20BumonHeaderCol" Width="195px"></HeaderStyle>--%>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="役職" SortExpression="SYAKUSHOKU">
                                        <ItemTemplate>
                                            <%--<div style="min-width: 150px; max-width: 150px; text-align: left; padding-right: 4px; overflow: hidden; white-space: nowrap; text-overflow: ellipsis">--%>
                                            <div class="grip" style="text-align: left; padding-right: 4px; overflow: hidden; display: -webkit-box; -webkit-line-clamp: 1; -webkit-box-orient: vertical; word-break: break-all;">
                                                <%--20220217 Phoo Updated Style --%>
                                                <asp:Label ID="lblYakushoku" runat="server" Text='<%# Eval("SYAKUSHOKU","{0}") %>' CommandArgument='<%# Container.DataItemIndex %>'></asp:Label>
                                            </div>
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="JC20HeaderCol" BorderColor="White" BorderWidth="2px" Wrap="False"></HeaderStyle>
                                        <%--20220222 Updated Phoo--%>
                                        <%--<HeaderStyle CssClass="JC12MitumoriCodeHeaderCol JC18GyousuHeaderCol AlignLeft" Width="150px"></HeaderStyle>--%>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="TEL" SortExpression="STEL"> <%--20220304 Updated Width エインドリ－--%>
                                        <ItemTemplate>
                                            <%--<div style="width: 120px; text-align: left; padding-right: 4px; overflow: hidden; white-space: nowrap; text-overflow: ellipsis">--%>
                                            <div class="grip" style="text-align: left; padding-right: 4px; overflow: hidden; display: -webkit-box; -webkit-line-clamp: 1; -webkit-box-orient: vertical; word-break: break-all;">
                                                <%--20220217 Phoo Updated Style --%>
                                                <asp:Label ID="lblTel" runat="server" Text=' <%# Eval(" STEL","{0}") %>' CommandArgument='<%# Container.DataItemIndex %>'></asp:Label>
                                            </div>
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="JC20HeaderCol" BorderColor="White" BorderWidth="2px" Wrap="False"></HeaderStyle>
                                        <%--20220222 Updated Phoo--%>
                                        <%--<HeaderStyle Width="120px" CssClass="JC12MitumoriCodeHeaderCol JC18PhoneHeaderCol"></HeaderStyle>--%>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Eメール"  SortExpression="SMAIL">
                                        <ItemTemplate>
                                            <%--<div style="width: 150px; text-align: left; overflow: hidden; white-space: nowrap; text-overflow: ellipsis">--%>
                                            <div class="grip" style="text-align: left; padding-right: 4px; overflow: hidden; display: -webkit-box; -webkit-line-clamp: 1; -webkit-box-orient: vertical; word-break: break-all;">
                                                <%--20220217 Phoo Updated Style --%>
                                                <asp:Label ID="lblEmail" runat="server" Text='<%# Eval(" SMAIL","{0}") %>' CommandArgument='<%# Container.DataItemIndex %>'></asp:Label>
                                            </div>
                                        </ItemTemplate>
                                        <HeaderStyle CssClass="JC20HeaderCol" BorderColor="White" BorderWidth="2px" Wrap="False"></HeaderStyle>
                                        <%--20220222 Updated Phoo--%>
                                        <%--<HeaderStyle CssClass="JC12MitumoriCodeHeaderCol JC18GyousuHeaderCol" Width="150px"></HeaderStyle>--%>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="備考"  SortExpression="SBIKOU"> <%--20220303 Updated width エインドリ－--%>
                                        <ItemTemplate>
                                            <%--<div style="min-width: 195px; max-width: 195px; text-align: center; overflow: hidden; white-space: nowrap; text-overflow: ellipsis">--%>
                                            <div class="grip" style="text-align: left; padding-right: 4px; overflow: hidden; display: -webkit-box; -webkit-line-clamp: 1; -webkit-box-orient: vertical; word-break: break-all;">
                                                <%--20220217 Phoo Updated Style --%>
                                                <asp:Label ID="lblBikou" runat="server" Text='<%# Eval("SBIKOU","{0}") %>' CommandArgument='<%# Container.DataItemIndex %>'></asp:Label>
                                            </div>
                                        </ItemTemplate>
                                        <%--<HeaderStyle CssClass="JC12MitumoriCodeHeaderCol JC20BumonHeaderCol" Width="195px"></HeaderStyle>--%>
                                        <HeaderStyle CssClass="JC20HeaderCol" BorderColor="White" BorderWidth="2px" Wrap="False"></HeaderStyle><%--20220307 Updated エインドリ－--%>
                                        <%--20220222 Updated Phoo--%>
                                    </asp:TemplateField>
                                </Columns>


                            </asp:GridView>
                            <div style="width: 100%; margin-top: 10px;" align="center">
                                <asp:Label ID="lblDataNai" runat="server" Style="font-size: 14px; font-weight: normal;" Text="該当するデータがありません"></asp:Label>
                            </div>
                        </asp:Panel>
                                    </div>
                                </div>
                    </div>

                    <%--<div class="container text-center">
                        <div class="col-md-12  text-center fixed-bottom " style="height: 65px; background: #D7DBDD;">--%> <%--20220223 MyatNoe Update--%>
                    <div class="text-center" style="height: 75px; background: #D7DBDD;">
                        <%--<asp:Button ID="btncancel" runat="server"    Text="キャンセル" type="JC20button " CssClass="btn text-primary font btn-sm btn btn-outline-primary " style ="background-color:white;  margin-top: 20px;" Width="99px" Height="31" OnClick="btncancel_Click"  OnClientClick="document.forms[0].target = '_self';"/>--%>
                        <asp:Button ID="btnKyotenlistCancel" runat="server" Text="キャンセル" CssClass="btn text-primary font btn-sm btn btn-outline-primary" Style="width: auto !important; background-color: white; margin-left: 10px; border-radius: 3px; font-size: 13px; padding: 6px 12px 6px 12px; letter-spacing: 1px; margin-top: 20px;" OnClick="btncancel_Click" />
                    </div>
                <%--</div>--%>
                <asp:HiddenField ID="hdnHome" runat="server" />
                <asp:Button ID="BT_ColumnWidth" runat="server" Text="Button" OnClick="BT_ColumnWidth_Click" style="display:none;" /><%--20220307 Added エインドリ－--%>
                 <asp:HiddenField ID="HF_GridSize" runat="server" /> <%--20220307 Added エインドリ－--%>
                         <asp:HiddenField ID="HF_Grid" runat="server" /><%--20220307 Added エインドリ－--%>
                </div>
                        </div>
                  
 <%--20220222 Added Start --%>  <%--20220301 Move to ContentTemplate エインドリ－--%>
   <%--<script type="text/javascript">
     $(function (){
         if ($.cookie('colWidthTokuisakiTantouKensaku') != null) {
             var columns = $.cookie('colWidthTokuisakiTantouKensaku').split(',');
             var i = 0;
             $('.GridViewStyle th').each(function () {
                 $(this).width(columns[i++]);
             });
         }
         else
         {
            var columns = [250, 195, 150, 150, 150, 195];　/*20220301 Added エインドリ－*/
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
             var date = new Date();
            // date.setTime(date.getTime() + (60 * 20000));
            date.setTime(date.getTime() + (1*60*1000));
            columns.each(function ()
            {
                msg += $(this).width() + ",";
            })
            $.cookie("colWidthTokuisakiTantouKensaku", msg, { expires: date }); // expires after 20 minutes
        }; 

    var prm = Sys.WebForms.PageRequestManager.getInstance();
        if (prm != null)
        {
            prm.add_endRequest(function (sender, e)
            {
                if (sender._postBackSettings.panelsToUpdate != null)
                {
                    if ($.cookie('colWidthTokuisakiTantouKensaku') != null)
                    {
                        var columns = $.cookie('colWidthTokuisakiTantouKensaku').split(',');
                        var i = 0;
                        $('.GridViewStyle th').each(function ()
                        {
                            $(this).width(columns[i++]);
                        });
                    }
                    else
                    {
                       var columns = [250, 195, 150, 150, 150, 195];　/*20220301 Added エインドリ－*/
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
    <%--20220222 Phoo Added End --%>
                <%--20220307 Added エインドリ Start－--%>
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
                <%--20220307 Added エインドリ End－--%>
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
                        <asp:Button ID="btn_Close" runat="server" Text="Button" CssClass="DisplayNone" OnClick="btn_Close_Click" onClientClick="MasterOverflow()" />
                        <asp:Button ID="btnToLogin" runat="server" Text="Button" Style="display: none" OnClick="btnToLogin_Click" onClientClick="MasterOverflow()" />
                              <asp:Button ID="btn_CloseShinkiTokui" runat="server" Text="Button" Style="display: none" OnClick="btn_CloseShinkiTokui_Click" onClientClick="MasterOverflow()" />
                    </asp:Panel>
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>
    </form>
</body>
</html>














