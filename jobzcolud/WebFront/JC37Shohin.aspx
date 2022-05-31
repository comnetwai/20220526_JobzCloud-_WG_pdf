<%@ Page Title="" Language="C#" MasterPageFile="~/WebFront/JC99NavBar.Master" AutoEventWireup="true" 
    CodeBehind="JC37Shohin.aspx.cs" Inherits="jobzcolud.WebFront.JC37Shohin" ValidateRequest="false" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server" >
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
    <script type="text/javascript">
        
        function isNumber(evt) {
            evt = (evt) ? evt : window.event;
            var charCode = (evt.which) ? evt.which : evt.keyCode;
            if (charCode > 31 && ((charCode < 48 || charCode > 57) || (charCode > 64 && charCode < 91) || (charCode > 96 && charCode < 123) || charCode == 8 || charCode == 32)) {
                return false;
            }
            return true;
        }
         function textclear()
        {
            //LB_Yuubenbangouerror.innerText = "";
            //LB_DenwaError.innerText = "";
            //LB_mailerror.innerText = "";
        }

        function RestrictEnter(event) {
            if (event.keyCode === 13) {
                event.preventDefault();
            }
        }
       <%-- function RestrictInt(val)
    {
            if (isNaN(val)) {
                alert("ok");
                val = val.substring(0, val.length - 1);
                val = val.replace(/[^\0-9]/ig, "");
                document.getElementById("<%= TB_Junban.ClientID %>").value = "";
                alert(val);
    return false;
    }
    return true;
    }--%>
        
</script>
      <link href="../Style/StyleJC.css" rel="stylesheet" />
    <link href="../Content/bootstrap.min.css" rel="stylesheet" />
    <webopt:BundleReference runat="server" Path="~/Content1/bootstrap" />
    <webopt:BundleReference runat="server" Path="~/Content1/css" />
    <meta name="google" content="notranslate" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=0, shrink-to-fit=no" />
    <style>
        .JC37DivSave {
    margin: 10px 0px 0px 0px;
    height: 60px;
    /*padding-top:15px;*/
}
         .JCShohinDiv {
    max-width: 1390px !important;
    min-width: 1390px !important;
    background-color: #ffffff;
    /*margin-left:20px;
    margin-right:20px;*/
    margin-left: auto;
    margin-right: auto;
}
       
       
         .JC37TableWidth {
    max-width: 1100px !important;
    min-width: 1100px !important;
    margin-left: auto;
    margin-right: auto;
}
     .JC37SyouhinTourokuDiv {
    max-width: 1060px !important;
    min-width: 1060px !important;
    /*background-color: #FFF;*/
    margin-left: auto;
    margin-right: auto;
   
    margin-bottom:10px; margin-left: auto; margin-right: auto;  padding-top: 0px; left: 50%; top: 50%; position: absolute; transform: translate(-50%, -50%);
}
      .JC37SyouhinTourokuDiv_master {
    max-width: 1060px !important;
    min-width: 1060px !important;
    background-color: #FFF;
    margin-left: auto;
    margin-right: auto;
    padding: 10px 0px 5px 0px;
   
}
       .JC37SyouhinTourokunav {
    max-width: 1060px !important;
    min-width: 1060px !important;
    /*margin-left:20px;
    margin-right:20px;*/
    margin-left: auto;
    margin-right: auto;
}  
        
    </style>
    </head>
        <body id="BD_Syouhin" runat="server" style="background-color: #d7e4f2 !important;">
               <Scripts>
                <asp:ScriptReference Name="jquery" />
                <asp:ScriptReference Name="bootstrap" />
                <asp:ScriptReference Path="../Scripts/Common/FixFocus.js" />
            </Scripts>
            <asp:UpdatePanel ID="updHeader" runat="server" UpdateMode="Conditional" RenderMode="Inline">
            <ContentTemplate>
               
                <div class="JC37SyouhinTourokunav" id="div3" runat="server">
                    <nav id="nav3" runat="server" class="navbar-expand custom-navbar1 custom-navbar-height" style="position: absolute;">
                        <div class="collapse navbar-collapse JC09navbar JC37SyouhinTourokunav" id="Div1" runat="server">
                            <label style="font-weight: bold; font-size: 14px; text-align: center; display: inline-block;">商品マスタ</label>
                            <asp:Label ID="lblLoginUserCode" runat="server" Text="" Visible="false" />
                            <asp:Label ID="lblLoginUserName" runat="server" Text="" Visible="false" />

                        </div>
                    </nav>
                </div>
                <%--<div class="container body-content" style="background-color:pink;padding-top:48px;cursor:context-menu;margin-bottom:100px !important;
                  max-width:1390px;min-width:1390px;">--%>
                <div id="Div_Body" runat="server" class="" style="background-color:#d7e4f2;padding-top: 45px;">
       　　　　　　<%-- <div class="JC37SyouhinTourokuDiv" id="divMitumoriTorokuP" runat="server" style="margin-bottom:10px; margin-left: auto; margin-right: auto;  padding-top: 0px; left: 50%; top: 50%; position: absolute; transform: translate(-50%, -50%);">--%>　　　　　　 <div  runat="server" id="divMitumoriToroku" style="background-color: white; ">
                      

                        <div id="divPopupHeader" runat="server" style="height:47px;">
                            <asp:Label ID="lblHeader" runat="server" Text="商品" CssClass="TitleLabel d-block align-content-left" style="padding-top:5px"></asp:Label>
                            <asp:Button ID="btnFusenHeaderCross" runat="server" Text="✕" CssClass="PopupCloseBtn"  OnClick="btnCancel_Click"/>
                        </div>
                        <div id="DivLine" runat="server" class="Borderline"></div>
                         
                      <table class="JC37SyouhinTourokuWidth JC10MitumoriTourokuTbl" style="padding-top:0px;margin-bottom:15px;margin-top:0px;">
                           <tr>
                                <td colspan="2" style="">
                                      <div class="JC37DivSave">
                                        
                                       <asp:Button ID="btnSyouhinSave" runat="server" Text="保存" CssClass="BlueBackgroundButton" Style="width: 100px;" OnClick="BT_Save_Click" />
                                       <asp:Button ID="btCancel" runat="server" CssClass="JC10CancelBtn" Width="100px" Text="キャンセル" OnClick="btnCancel_Click" />
                                     <asp:Button runat="server" ID="BT_Shinki" Text="商品を新規作成"
                                     type="button " CssClass="BlueBackgroundButton JC10SaveBtn" Style="margin-left:10px" OnClick="BT_Shinki_Click" />
                                          </div>
                                </td>
                            </tr>
                          
                          <tr>
                              <td valign="top">
                                   <table>
                                        <tr class="JC10MitumoriTourokuHeightTr3">
                                            <td class="text-left">
                                                <asp:Label ID="lblcSyouhinLabel" runat="server" Text="商品コード"></asp:Label>
                                            </td>
                                           <td class="text-left" style="width:200px;">
                                                 <asp:Label runat="server" ID="LB_SyouhinCode" Style="display: block;"></asp:Label>
                                            </td>
                                             <td colspan="2" class="text-left JC37SyouhinTd3">
                                                  <asp:Button ID="lnkcSyouhin" runat="server" Text="自動コード" CssClass="JC10GrayButton" onmousedown="getTantouBoardScrollPosition();" Width="90px" OnClick="btnSyouhin_Auto_Click" Height="30px" />
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                         <tr class="JC10MitumoriTourokuHeightTr3">
                                            <td class="text-left JC10MitumoriTourokuFirstTd">
                                                <asp:Label ID="lblsSyouhinLabel" runat="server" Text="商品名"></asp:Label>
                                                <label style="color:red;cursor:text;">*</label>
                                            </td>
                                            <td colspan="3" class="text-left" style="width:360px;">
                                                <asp:UpdatePanel ID="updsSyouhin" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <asp:TextBox ID="txtsSyouhin" runat="server" AutoPostBack="true" MaxLength="100" CssClass="form-control TextboxStyle JC37SyouhinMeiTourokuTextBox"
                                                            onkeyup="DeSelectText(this);"  OnTextChanged="txtsSyouhin_TextChanged"
                                                            onfocus="this.setSelectionRange(this.value.length, this.value.length);" ></asp:TextBox>
                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="txtsSyouhin" EventName="TextChanged" />
                                                    </Triggers>
                                                </asp:UpdatePanel>
                                            </td>
                                        </tr>
                                         <tr class="JC10MitumoriTourokuHeightTr3">
                                            <td class="text-left">
                                                <asp:Label ID="lblShohinryakusho" runat="server" Text="商品略称"></asp:Label>
                                            </td>
                                            <td >
                                                <asp:UpdatePanel ID="upd_Shohinryakusho" runat="server" UpdateMode="Conditional">
                                                     <ContentTemplate>
                                                        <asp:TextBox ID="TB_Shohinryakusho" runat="server" AutoPostBack="true" MaxLength="100" 
                                                            CssClass="form-control TextboxStyle JC37SyouhinCodeTourokuTextBox" OnTextChanged="TB_Shohinryakusho_TextChanged"
                                                            onkeyup="DeSelectText(this);" onfocus="this.setSelectionRange(this.value.length, this.value.length);" Height="28px"></asp:TextBox>
                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="TB_Shohinryakusho" EventName="TextChanged" />
                                                    </Triggers>
                                            </asp:UpdatePanel>
                                            </td>
                                             <td class="text-right JC37SyouhinTd3" style="padding-right:10px">
                                                 <asp:Label ID="Label2" runat="server" Text="順番"></asp:Label>
                                             </td>
                                             <td class="text-right" style="">
                                                  <asp:UpdatePanel ID="Upd_Junban" runat="server" UpdateMode="Conditional">
                                                     <ContentTemplate>
                                                        <asp:TextBox ID="TB_Junban" runat="server" AutoPostBack="true" MaxLength="11" CssClass="form-control TextboxStyle " 
                                                        style="width:80px;height:28px"  onkeyup="textclear();"
                                                            OnTextChanged="TB_Junban_TextChanged"
                                                            onkeypress="return isNumber(event);"   onkeydown="RestrictEnter(event);" 
                                                            onfocus="this.setSelectionRange(this.value.length, this.value.length);" ></asp:TextBox>
                                                     
                                                   <asp:Label ID="lbl_JunbanErr" runat="server" Text=" " CssClass="error"></asp:Label>
                                                     </ContentTemplate>
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="TB_Junban" EventName="TextChanged" />
                                                    </Triggers>
                                            </asp:UpdatePanel>
                                             </td>
                                        </tr> 
                                          <tr class="JC10MitumoriTourokuHeightTr3">
                                            <td class="text-left ">
                                                <asp:Label ID="lblTsukamatsuKakaku" runat="server" Text="仕入価格"></asp:Label>
                                            </td>
                                            <td  style="width:200px;max-width:200px">
                                                <asp:UpdatePanel ID="Upd_TsukamatsuKakaku" runat="server" UpdateMode="Conditional">
                                                     <ContentTemplate>
                                                        <asp:TextBox ID="TB_TsukamatsuKakaku" AutoPostBack="true" runat="server"  MaxLength="11" 
                                                            CssClass="form-control TextboxStyle JC37SyouhinCodeTourokuTextBox"
                                                            OnTextChanged="TB_TsukamatsuKakaku_TextChanged"
                                                            onkeypress="return isNumber(event);"  onkeyup="textclear();" onkeydown="RestrictEnter(event);" 
                                                            onfocus="this.setSelectionRange(this.value.length, this.value.length);"   Height="28px"></asp:TextBox>
                                                    <asp:Label ID="lbl_TsukamatsuKakaku" runat="server" Text=" " CssClass="error"></asp:Label>
                                                     </ContentTemplate>
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="TB_TsukamatsuKakaku" EventName="TextChanged" />
                                                    </Triggers>
                                            </asp:UpdatePanel>
                                                 
                                            </td>
                                        </tr>
                                         <tr class="JC10MitumoriTourokuHeightTr3">
                                            <td class="text-left ">
                                                <asp:Label ID="lblHanbaiKakaku" runat="server" Text="販売価格"></asp:Label>
                                            </td>
                                            <td  style="width:200px;max-width:200px">
                                                <asp:UpdatePanel ID="Upd_HanbaiKakaku" runat="server" UpdateMode="Conditional">
                                                     <ContentTemplate>
                                                        <asp:TextBox ID="TB_HanbaiKakaku" AutoPostBack="true" runat="server"  MaxLength="11" CssClass="form-control TextboxStyle JC37SyouhinCodeTourokuTextBox"
                                                             OnTextChanged="TB_HanbaiKakaku_TextChanged"
                                                            onkeypress="return isNumber(event);"  onkeyup="textclear();" onkeydown="RestrictEnter(event);" 
                                                            onfocus="this.setSelectionRange(this.value.length, this.value.length);"  Height="28px"></asp:TextBox>
                                                    <asp:Label ID="lbl_HanbaiKakaku" runat="server" Text=" " CssClass="error"></asp:Label>
                                                     </ContentTemplate>
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="TB_HanbaiKakaku" EventName="TextChanged" />
                                                    </Triggers>
                                            </asp:UpdatePanel>
                                            </td>
                                        </tr>
                                       <tr class="JC10MitumoriTourokuHeightTr3">
                                            <td class="text-left">
                                                <asp:Label ID="lblMitsumoriSyokisu" runat="server" Text="見積初期数"></asp:Label>
                                            </td>
                                            <td >
                                                <asp:UpdatePanel ID="Upd_MitsumoriSyokisu" runat="server" UpdateMode="Conditional">
                                                     <ContentTemplate>
                                                        <asp:TextBox ID="TB_MitsumoriSyokisu" AutoPostBack="true" runat="server"  MaxLength="5" 
                                                            OnTextChanged="TB_MitsumoriSyokisu_TextChanged"
                                                            CssClass="form-control TextboxStyle JC37SyouhinCodeTourokuTextBox"
                                                             onkeypress="return isNumber(event);"  onkeyup="textclear();" onkeydown="RestrictEnter(event);" 
                                                            onfocus="this.setSelectionRange(this.value.length, this.value.length);"  Height="28px"></asp:TextBox>
                                                       
                                                         <asp:Label ID="lbl_MitsumoriSyokisu" runat="server" Text=" " CssClass="error"></asp:Label>
                                                        
                                                         </ContentTemplate>
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="TB_MitsumoriSyokisu" EventName="TextChanged" />
                                                    </Triggers>
                                            </asp:UpdatePanel>
                                            </td>
                                             <td class="text-right JC37SyouhinTd3" style="padding-right:10px">
                                                 <asp:Label ID="Label5" runat="server" Text="単位"></asp:Label>
                                             </td>
                                             <td class="text-right" style="">
                                                  <%--<asp:UpdatePanel ID="Upd_Tani" runat="server" UpdateMode="Conditional">
                                                     <ContentTemplate>--%>
                                                       <%-- <asp:TextBox ID="TB_Tani" runat="server" AutoPostBack="true" MaxLength="100" CssClass="form-control TextboxStyle "
                                                        style="width:80px;height:28px"    onkeyup="DeSelectText(this);" onfocus="this.setSelectionRange(this.value.length, this.value.length);" ></asp:TextBox>--%>
                                                 <%--  <asp:DropDownList ID="DDL_cTANI" runat="server" AutoPostBack="True" CssClass="user_select" style="width:80px;height:28px" >
                                                    </asp:DropDownList>--%>
                                                 <%--</ContentTemplate>
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="TB_Tani" EventName="TextChanged" />
                                                    </Triggers>
                                            </asp:UpdatePanel>--%>
                                                  <asp:UpdatePanel ID="updTani" runat="server" UpdateMode="Conditional"> 
                                                         <Triggers>
                                                            <asp:AsyncPostBackTrigger ControlID="txtTani" EventName="TextChanged"/>
                                                             <asp:AsyncPostBackTrigger ControlID="DDL_cTANI" EventName="SelectedIndexChanged"/>
                                                        </Triggers>
                                                          <ContentTemplate>
                                                <div class="select-editable">
                                                <asp:Label ID="lblcTANI" runat="server" Text='<%# Eval("cTANI") %>' />
                                                    <asp:DropDownList ID="DDL_cTANI" runat="server" AutoPostBack="True" CssClass="user_select" TabIndex="-1" OnSelectedIndexChanged="DDL_cTANI_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                    <asp:TextBox ID="txtTani" runat="server" Text='<%# Eval("cTANI") %>' CssClass="txtTani" autocomplete="off" AutoPostBack="true" OnTextChanged="txtTani_TextChanged"></asp:TextBox> 
                                                </div>      
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                             </td>
                                        </tr> 
                                       <tr class="JC10MitumoriTourokuHeightTr3">
                                            <td class="text-left">
                                                <asp:Label ID="LblSizeX" runat="server" Text="サイズX"></asp:Label>
                                            </td>
                                           <td class="text-left JC37SyouhinTd2" style="width:200px">
                                               <asp:UpdatePanel ID="Upd_SizeX" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <asp:TextBox ID="TB_SizeX" runat="server" AutoPostBack="true" MaxLength="14" 
                                                            CssClass="form-control TextboxStyle JC37SyouhinCodeTourokuTextBox"
                                                            OnTextChanged="TB_SizeX_TextChanged"
                                                            onkeypress="return isNumber(event);"  onkeyup="textclear();" onkeydown="RestrictEnter(event);" 
                                                            onfocus="this.setSelectionRange(this.value.length, this.value.length);" Height="28px"></asp:TextBox>
                                                    <asp:Label ID="Lbl_SizeX" runat="server" Text=" " CssClass="error"></asp:Label>
                                                        </ContentTemplate>
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="TB_SizeX" EventName="TextChanged" />
                                                    </Triggers>
                                                </asp:UpdatePanel>
                                            </td>
                                             <td colspan="2" class="text-left JC37SyouhinTd3">
                                                <asp:DropDownList ID="DD_SizeX" runat="server" Width="60px" AutoPostBack="True" Height="28px" style="font-size:13px;margin:0px !important;border-radius:2px;border:1px solid #9b9b9b;" OnSelectedIndexChanged="DD_SizeX_SelectedIndexChanged">
                                                         <asp:ListItem Value="0" Text="mm" Selected="True"></asp:ListItem>
                                                         <asp:ListItem Value="1" Text="m" ></asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr class="JC10MitumoriTourokuHeightTr3">
                                            <td class="text-left">
                                                <asp:Label ID="lblSizeY" runat="server" Text="サイズY"></asp:Label>
                                            </td>
                                           <td class="text-left JC37SyouhinTd2" style="width:200px">
                                               <asp:UpdatePanel ID="Upd_SizeY" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <asp:TextBox ID="TB_SizeY" runat="server" AutoPostBack="true" MaxLength="100" 
                                                            CssClass="form-control TextboxStyle JC37SyouhinCodeTourokuTextBox"
                                                            OnTextChanged="TB_SizeY_TextChanged"
                                                            onkeypress="return isNumber(event);"  onkeyup="textclear();" onkeydown="RestrictEnter(event);" 
                                                            onfocus="this.setSelectionRange(this.value.length, this.value.length);" Height="28px"></asp:TextBox>
                                                    <asp:Label ID="lbl_SizeY" runat="server" Text=" " CssClass="error"></asp:Label>
                                                        </ContentTemplate>
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="TB_SizeY" EventName="TextChanged" />
                                                    </Triggers>
                                                </asp:UpdatePanel>
                                            </td>
                                             <td colspan="2" class="text-left JC37SyouhinTd3">
                                                <asp:DropDownList ID="DD_SizeY" runat="server" Width="60px" AutoPostBack="True" Height="28px" style="font-size:13px;margin:0px !important;border-radius:2px;border:1px solid #9b9b9b;" OnSelectedIndexChanged="DD_SizeY_SelectedIndexChanged">
                                                          <asp:ListItem Value="0" Text="mm" Selected="True"></asp:ListItem>
                                                         <asp:ListItem Value="1" Text="m" ></asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr class="JC10MitumoriTourokuHeightTr3">
                                            <td class="text-left">
                                                <asp:Label ID="lblSizeZ" runat="server" Text="サイズZ"></asp:Label>
                                            </td>
                                           <td class="text-left JC37SyouhinTd2" style="width:200px">
                                               <asp:UpdatePanel ID="Upd_SizeZ" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <asp:TextBox ID="TB_SizeZ" runat="server" AutoPostBack="true" MaxLength="100" 
                                                             OnTextChanged="TB_SizeZ_TextChanged"
                                                            CssClass="form-control TextboxStyle JC37SyouhinCodeTourokuTextBox"
                                                           onkeypress="return isNumber(event);"  onkeyup="textclear();" onkeydown="RestrictEnter(event);" 
                                                            onfocus="this.setSelectionRange(this.value.length, this.value.length);" Height="28px"></asp:TextBox>
                                                     <asp:Label ID="lbl_SizeZ" runat="server" Text=" " CssClass="error"></asp:Label>
                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="TB_SizeZ" EventName="TextChanged" />
                                                    </Triggers>
                                                </asp:UpdatePanel>
                                            </td>
                                             <td colspan="2" class="text-left JC37SyouhinTd3">
                                                <asp:DropDownList ID="DD_SizeZ" runat="server" Width="60px" AutoPostBack="True" Height="28px" style="font-size:13px;margin:0px !important;border-radius:2px;border:1px solid #9b9b9b;" OnSelectedIndexChanged="DD_SizeZ_SelectedIndexChanged">
                                                          <asp:ListItem Value="0" Text="mm" Selected="True"></asp:ListItem>
                                                         <asp:ListItem Value="1" Text="m" ></asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                       <tr class="JC10MitumoriTourokuHeightTr3">
                                            <td class="text-left">
                                                <asp:Label ID="Label16" runat="server" Text="見出アイテム"></asp:Label>
                                            </td>
                                            <td  class="text-left JC37SyouhinTd2" style="">
                                                 <asp:CheckBox ID="Chk_midashi" runat="server" AutoPostBack="true"  style="" CssClass="JC37CHK" />
                                            </td>
                                        </tr>
                                       <tr class="JC10MitumoriTourokuHeightTr3">
                                            <td class="text-left">
                                                <asp:Label ID="Label17" runat="server" Text="廃盤"></asp:Label>
                                            </td>
                                            <td  class="text-left JC37SyouhinTd2" style="">
                                                 <asp:CheckBox ID="Chk_haiban" runat="server" AutoPostBack="true"  style="" CssClass="JC37CHK"/>
                                            </td>
                                        </tr>
                             </table>
                                       </td>
                              <td valign="top">
                                  <table>
                                      
                                     <tr class="JC10MitumoriTourokuHeightTr3">
                                            <td class="text-left JC10MitumoriTourokuFirstTd">
                                                <asp:Label ID="lblShiyou" runat="server" Text="仕様"></asp:Label>
                                            </td>
                                            <td  class="text-left" style="">
                                                <asp:UpdatePanel ID="Upd_Shiyou" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <asp:TextBox ID="TB_Shiyou" runat="server" AutoPostBack="true" MaxLength="200" 
                                                            CssClass="form-control TextboxStyle JC37SyouhinMeiTourokuTextBox"  OnTextChanged="TB_Shiyou_TextChanged"
                                                            onkeyup="DeSelectText(this);" onfocus="this.setSelectionRange(this.value.length, this.value.length);" ></asp:TextBox>
                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="TB_Shiyou" EventName="TextChanged" />
                                                    </Triggers>
                                                </asp:UpdatePanel>
                                            </td>
                                        </tr>
                                       <tr class="JC10MitumoriTourokuHeightTr3">
                                            <td class="text-left JC10MitumoriTourokuFirstTd">
                                                <asp:Label ID="Label10" runat="server" Text="商品大分類"></asp:Label>
                                            </td>
                                            <td  class="text-left" style="">
                                                 <asp:UpdatePanel ID="Upd_ShohinDaibunri" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                         <div id="Div_ShohinDaibunri" runat="server">
                                                        <asp:Button ID="BT_ShohinDaibunri_Add" runat="server" Text="追加" Height="30px" CssClass="JC10GrayButton" OnClick="BT_ShohinDaibunri_Add_Click" OnClientClick="PopupOpenButton()"/>
                                                         </div>
                                                        <div style="float: left; min-width: 280px;max-width: 100%;  display: none;text-overflow:ellipsis;white-space: nowrap;overflow: hidden;" id="Div_ShohinDaibunriLable" runat="server">
                                                            <asp:Label ID="lblsSYOUHINDAIGRP" runat="server" ForeColor="#0080C0"></asp:Label>
                                                            <asp:Label ID="lblcSYOUHINDAIGRP" runat="server" Visible="false"></asp:Label>
                                                            <asp:Button ID="BT_ShohinDaibunri_Cross" runat="server" BackColor="White" Text="✕" OnClick="BT_ShohinDaibunri_Cross_Click"
                                                                Style="vertical-align: middle; margin-left: 10px; font-weight: bolder; height: 30px; border: none;margin-right:10px;" />
                                                        </div>
                                                         <div id="divShohinDaibunriSyosai" runat="server" style="display: none;padding-bottom:4px;">

                                                         <asp:Button runat="server" ID="BT_ShohinDaibunri_Syousai" Text="詳細" CssClass="JC10GrayButton" OnClick="BT_ShohinDaibunri_Syousai_Click" />
                                                          </div>
                                                     
                                                    </ContentTemplate>
                                                      <Triggers>
                                                                            <asp:PostBackTrigger ControlID="BT_ShohinDaibunri_Syousai" />
                                                                        </Triggers>
                                                </asp:UpdatePanel>
                                            </td>
                                        </tr>
                                       <tr class="JC10MitumoriTourokuHeightTr3">
                                            <td class="text-left JC10MitumoriTourokuFirstTd">
                                                <asp:Label ID="Label11" runat="server" Text="商品中分類"></asp:Label>
                                            </td>
                                            <td  class="text-left" style="">
                                                <asp:UpdatePanel ID="Upd_SYOUHINTYUUbunri" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                         <div id="divBT_ShohinTyuu" runat="server">
                                                        <asp:Button ID="BT_ShohinTyuu_Add" runat="server" Text="追加" Height="30px" CssClass="JC10GrayButton" OnClick="BT_ShohinTyuu_Add_Click" OnClientClick="PopupOpenButton()"/>
                                                         </div>
                                                        <div style="float: left; min-width: 280px;max-width: 100%; display: none;text-overflow:ellipsis;white-space: nowrap;overflow: hidden;" id="Div_ShohinTyuuLable" runat="server">
                                                            <asp:Label ID="lblsSYOUHINTYUUGRP" runat="server" ForeColor="#0080C0"></asp:Label>
                                                            <asp:Label ID="lblcSYOUHINTYUUGRP" runat="server" Visible="false"></asp:Label>
                                                            <asp:Button ID="BT_ShohinTyuu_Cross" runat="server" BackColor="Transparent" Text="✕" OnClick="BT_ShohinTyuu_Cross_Click"
                                                                Style="vertical-align: middle; margin-left: 10px;margin-right:10px; font-weight: bolder; height: 30px; border: none;" />
                                                        </div>
                                                   <div id="divShohinTyuuSyousai" runat="server" style="display: none;padding-bottom:4px;">

                                                         <asp:Button runat="server" ID="BT_ShohinTyuu_Syousai" Text="詳細" CssClass="JC10GrayButton" OnClick="BT_ShohinTyuu_Syousai_Click" />
                                                          </div>
                                                     
                                                    </ContentTemplate>
                                                      <Triggers>
                                                                            <asp:PostBackTrigger ControlID="BT_ShohinTyuu_Syousai" />
                                                                        </Triggers>
                                                    </asp:UpdatePanel>
                                            </td>
                                        </tr>
                                       <tr class="JC10MitumoriTourokuHeightTr3">
                                            <td class="text-left JC10MitumoriTourokuFirstTd">
                                                <asp:Label ID="Label12" runat="server" Text="標準仕入先"></asp:Label>
                                            </td>
                                            <td  class="text-left" style="">
                                                <asp:UpdatePanel ID="upd_SHIIRESAKI" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                         <div id="divSHIIRESAKIKensaku" runat="server">
                                                        <asp:Button ID="BT_SHIIRESAKI_Add" runat="server" Text="追加" Height="30px" CssClass="JC10GrayButton" onmousedown="getTantouBoardScrollPosition();" OnClientClick="PopupOpenButton()" OnClick="BT_SHIIRESAKI_Add_Click"/>
                                                         </div>
                                                        <div style="float: left; max-width: 100%; display: none;text-overflow:ellipsis;white-space: nowrap;overflow: hidden;" id="divSHIIRESAKIKensakuLabel" runat="server">
                                                            <asp:Label ID="lblsSHIIRESAKI" runat="server" ForeColor="#0080C0"></asp:Label>
                                                            <asp:Label ID="lblcSHIIRESAKI" runat="server" Visible="false"></asp:Label>
                                                            <asp:Button ID="BT_SHIIRESAKI_Cross" runat="server" BackColor="Transparent" Text="✕" OnClick="BT_SHIIRESAKI_Cross_Click"
                                                                Style="vertical-align: middle; margin-left: 10px; font-weight: bolder; height: 30px; border: none;margin-right:10px;" />
                                                        </div>
                                                        <div id="divSHIIRESAKI_Syousai" runat="server" style="display: none;padding-bottom:4px;">
                                                         <asp:Button runat="server" ID="BT_SHIIRESAKI_Syousai" Text="詳細" CssClass="JC10GrayButton" OnClick="BT_SHIIRESAKI_Syousai_Click" />
                                                          </div>
                                                    </ContentTemplate>
                                                      <Triggers>
                                                      <asp:PostBackTrigger ControlID="BT_SHIIRESAKI_Syousai" />
                                                      </Triggers>
                                                    </asp:UpdatePanel>
                                            </td>
                                        </tr>
                                      <tr style="height:120px;" valign="top">
                                            <td class="text-left " style="padding-top:5px;" >
                                                <asp:Label ID="lblBikouLabel" runat="server" Text="商品備考"></asp:Label>
                                            </td>
                                            <td class="text-left " valign="top" >
                                                <asp:UpdatePanel ID="updBikou" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <asp:TextBox ID="txtBikou" MaxLength="200" AutoPostBack="true" OnTextChanged="txtBikou_TextChanged"
                                                            CssClass="form-control TextboxStyle JC37SyouhinTextArea" Rows="5" runat="server" TextMode="MultiLine"
                                                            onkeyup="DeSelectText(this);GetCharacterCountLength(this, 200, lblRemainingMemoCount);" onfocus="this.setSelectionRange(this.value.length, this.value.length);" ></asp:TextBox>
                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="txtBikou" EventName="TextChanged" />
                                                    </Triggers>
                                                </asp:UpdatePanel>
                                            </td>
                                        </tr>
                                      <tr class="JC10MitumoriTourokuHeightTr3">
                                            <td class="text-left JC10MitumoriTourokuFirstTd">
                                                <asp:Label ID="lblOmosa" runat="server" Text="重さ"></asp:Label>
                                            </td>
                                            <td  class="text-left" style="">
                                                <asp:UpdatePanel ID="Upd_Omosa" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <asp:TextBox ID="TB_Omosa" runat="server" AutoPostBack="true" MaxLength="12"
                                                            CssClass="form-control TextboxStyle JC37SyouhinMeiTourokuTextBox"
                                                             OnTextChanged="TB_Omosa_TextChanged"
                                                            onkeypress="return isNumber(event);"  onkeyup="textclear();" onkeydown="RestrictEnter(event);" 
                                                            onfocus="this.setSelectionRange(this.value.length, this.value.length);"></asp:TextBox>
                                                    <asp:Label ID="lbl_Omosa" runat="server" Text=" " CssClass="error"></asp:Label>
                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="TB_Omosa" EventName="TextChanged" />
                                                    </Triggers>
                                                </asp:UpdatePanel>
                                            </td>
                                        </tr>
                                       <tr class="JC10MitumoriTourokuHeightTr3">
                                            <td class="text-left JC10MitumoriTourokuFirstTd">
                                                <asp:Label ID="Label14" runat="server" Text="型番"></asp:Label>
                                            </td>
                                            <td  class="text-left" style="">
                                                <asp:UpdatePanel ID="Upd_KataBan" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <asp:TextBox ID="TB_KataBan" runat="server" AutoPostBack="true" OnTextChanged="TB_KataBan_TextChanged"
                                                            MaxLength="100" CssClass="form-control TextboxStyle JC37SyouhinMeiTourokuTextBox"
                                                            onkeyup="DeSelectText(this);" onfocus="this.setSelectionRange(this.value.length, this.value.length);" ></asp:TextBox>
                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="TB_KataBan" EventName="TextChanged" />
                                                    </Triggers>
                                                </asp:UpdatePanel>
                                            </td>
                                        </tr>
                                       <tr class="JC10MitumoriTourokuHeightTr3">
                                            <td class="text-left JC10MitumoriTourokuFirstTd">
                                                <asp:Label ID="Label15" runat="server" Text="メーカー"></asp:Label>
                                            </td>
                                            <td  class="text-left" style="">
                                                <asp:UpdatePanel ID="Upd_Meka" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <asp:TextBox ID="TB_Meka" runat="server" AutoPostBack="true" OnTextChanged="TB_Meka_TextChanged"
                                                            MaxLength="100" CssClass="form-control TextboxStyle JC37SyouhinMeiTourokuTextBox"
                                                            onkeyup="DeSelectText(this);" onfocus="this.setSelectionRange(this.value.length, this.value.length);" ></asp:TextBox>
                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="TB_Meka" EventName="TextChanged" />
                                                    </Triggers>
                                                </asp:UpdatePanel>
                                            </td>
                                        </tr>
                                      <tr class="JC10MitumoriTourokuHeightTr3">
                                            <td class="text-left JC10MitumoriTourokuFirstTd" colspan="2" style="padding-top:10px;">
                                                <asp:UpdatePanel ID="upd_chkshou" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <asp:CheckBox ID="CHK_Shou" runat="server"  AutoPostBack="true"  style="" TextAlign="Left" CssClass="JC37CHK" value="CHK_Shou" Text="見積画面に文字を小さく表示する　" OnCheckedChanged="CHK_Shou_CheckedChanged" />
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </td>
                                        </tr>
                                        
                                  </table>

                              </td>
                          </tr>
                          </table>
                     
                     </div>
                    </div>
                 <asp:UpdatePanel runat="server" ID="updLabelSave" UpdateMode="Conditional">
                            <ContentTemplate>
                                <div class="success JCSuccess" id="divLabelSave" runat="server">
                                    <asp:Label ID="LB_Save" Text="保存しました。" runat="server" ForeColor="White" Font-Size="13px"></asp:Label>
                                    <asp:Button ID="BT_LBSaveCross" Text="✕" runat="server" Style="background-color: white; border-style: none; right: 10px; position: absolute;" />
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                  <asp:Button ID="btnmojiOK" runat="server" Text="はい" class="BlueBackgroundButton DisplayNone" Width="100px" Height="36px"/>

                  <asp:Button ID="btnYes" runat="server" Text="はい" class="BlueBackgroundButton DisplayNone" OnClick="btnYes_Click" Width="100px" Height="36px" />
                  <asp:Button ID="btnNo" runat="server" Text="いいえ" class="BlueBackgroundButton DisplayNone" OnClick="btnNo_Click" Width="100px" Height="36px" />
                  <asp:Button ID="btnCancel" runat="server" Text="キャンセル" class="JC09GrayButton DisplayNone" Width="100px" Height="36px" OnClientClick="return false;" />
       
                 <asp:HiddenField ID="HF_checkData" runat="server" />
                <asp:HiddenField ID="hdnHome" runat="server" />
                <asp:HiddenField ID="HF_flag" runat="server" />
                 <asp:HiddenField ID="HF_isChange" runat="server" />
                 <asp:HiddenField ID="HF_Save" runat="server" />
            <%--テテ added start--%>
           <%-- <asp:UpdatePanel ID="" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:Button ID="" runat="server" Text="Button" CssClass="DisplayNone" />
                        <asp:ModalPopupExtender ID="" runat="server" TargetControlID=""
                            PopupControlID="" BackgroundCssClass="PopupModalBackground" BehaviorID="pnlDaibunruiPopupScroll">
                        </asp:ModalPopupExtender>
                        <asp:Panel ID="" runat="server" Style="display: none; height: 100%; overflow: hidden;" CssClass="PopupScrollDiv" HorizontalAlign="Center">
                            <asp:Panel ID="" runat="server">
                                <iframe id="" runat="server" scrolling="no" class="NyuryokuIframe" style="border-radius: 0px;"></iframe>
                                                            </asp:Panel>
                        </asp:Panel>
                    </ContentTemplate>
                </asp:UpdatePanel>--%>
            <%--テテ added end--%>

                 <asp:UpdatePanel ID="updDaibunruiPopup" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:Button ID="btnDaibunruiPopup" runat="server" Text="Button" Style="display: none" />
                <asp:ModalPopupExtender ID="mpeDaibunruiPopup" runat="server" TargetControlID="btnDaibunruiPopup"
                    PopupControlID="pnlDaibunruiPopupScroll" BackgroundCssClass="PopupModalBackground" BehaviorID="pnlDaibunruiPopupScroll"
                    RepositionMode="RepositionOnWindowResize">
                </asp:ModalPopupExtender>
                <asp:Panel ID="pnlDaibunruiPopupScroll" runat="server" Style="display: none; height: 100%; overflow: hidden;" CssClass="PopupScrollDiv">
                    <asp:Panel ID="pnlDaibunruiPopup" runat="server">
                        <iframe id="ifDaibunruiPopup" runat="server" scrolling="no" style="height: 100vh; width: 100vw;"></iframe>
                        
                         <asp:Button ID="btnMasterDaiPopupClose" runat="server" Text="Button" Style="display: none" OnClick="btnMasterDaiPopupClose_Click" onClientClick="MasterOverflow()"/>
                                <asp:Button ID="btnMasterChuuPopupClose" runat="server" Text="Button" Style="display: none" OnClick="btnMasterChuuPopupClose_Click" onClientClick="MasterOverflow()"/>
                                <asp:Button ID="btnDaiSelect" runat="server" Text="Button" Style="display: none" OnClick="btnDaiSelect_Click" onClientClick="MasterOverflow()"/>
                                <asp:Button ID="btnChuuSelect" runat="server" Text="Button" Style="display: none" OnClick="btnChuuSelect_Click" onClientClick="MasterOverflow()"/>

                         
                    </asp:Panel>
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>

                  <asp:UpdatePanel ID="updSHIIRESAKIKPopup" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:Button ID="btnSHIIRESAKIKPopup" runat="server" Text="Button" Style="display: none" />
                <asp:ModalPopupExtender ID="mpeSHIIRESAKIPopup" runat="server" TargetControlID="btnSHIIRESAKIKPopup"
                    PopupControlID="pnlSHIIRESAKIPopupScroll" BackgroundCssClass="PopupModalBackground" BehaviorID="pnlSHIIRESAKIPopupScroll"
                    RepositionMode="RepositionOnWindowResize">
                </asp:ModalPopupExtender>
                <asp:Panel ID="pnlSHIIRESAKIPopupScroll" runat="server" Style="display: none; height: 100%; overflow: hidden;" CssClass="PopupScrollDiv">
                    <asp:Panel ID="pnlSHIIRESAKIPopup" runat="server">
                        <iframe id="ifSHIIRESAKIPopup" runat="server" scrolling="yes" style="height: 100vh; width: 100vw;"></iframe>
                        
                         <asp:Button ID="btn_SHIIRESAKISelect" runat="server" Text="Button" CssClass="DisplayNone" OnClick="btn_SHIIRESAKISelect_Click" onClientClick="MasterOverflow()"/>
                        <asp:Button ID="btnClose" runat="server" Text="Button" CssClass="DisplayNone" OnClick="btn_Close_Click" onClientClick="MasterOverflow()"/>
                         
                    </asp:Panel>
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>
                </ContentTemplate>
                </asp:UpdatePanel>
        </body>
        </html>
    </asp:Content>