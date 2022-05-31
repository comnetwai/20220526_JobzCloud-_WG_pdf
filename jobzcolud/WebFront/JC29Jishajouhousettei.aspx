<%@ Page Language="C#" MasterPageFile="~/WebFront/JC99NavBar.Master"  AutoEventWireup="true" 
    CodeBehind="JC29Jishajouhousettei.aspx.cs" Inherits="jobzcolud.WebFront.JC29Jishajouhousettei" ValidateRequest ="False" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server" >
<!DOCTYPE html>

<html>
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
<meta name="google" content="notranslate" />
<meta name="viewport" content="width=device-width, initial-scale=1.0, maximum-scale=1.0, user-scalable=0, shrink-to-fit=no" />
   
<title></title>
    <asp:PlaceHolder runat="server">
        <%: Scripts.Render("~/bundles/modernizr") %>
        <%: Styles.Render("~/style/StyleBundle1") %>
        <%: Scripts.Render("~/scripts/ScriptBundle1") %>
        <%: Styles.Render("~/style/UCStyleBundle") %>

    </asp:PlaceHolder>
    <link href="../Style/StyleJC.css" rel="stylesheet" />
    <link href="../Content/bootstrap.min.css" rel="stylesheet" />
    <style>
        #divContent{
            background-color:transparent!important;
        }
        #dv_container{
            margin-top:30px;
        }
        .seikyuubtn,.mitsumoribtn,.tansuushoribtn {
            width: 140px;
            height:35px;
            background-color: white;
            border-color: LightGrey;
            border-width: 1px;
            border-style: solid;
            font-size:13px;
        }
        .bgClr {
            background-color: lightgray;
        }
        input[type="text"]{
            color:black!important;
        }
        .div_hidden{
            display:none;
        }
        @media (max-width: 768px) {
           #dv_container{
            margin-top:15%;
        } 
        }
        @media (max-width: 375px) {
           #dv_container{
            margin-top:25%;
        } 
        }
    </style>
    
</head>
<body>
    <%--<form id="form1" runat="server">
        
    </form>--%>

    <div class="container" id="dv_container" style="min-width: 1300px; max-width: 1300px;"><%--style="margin-top:9%;"--%>
        <div class="row fw-bold align-items-center" style="height:40px;background-color:rgb(242,242,242)"><span>自社情報設定</span></div>
        <div class="row bg-white">
            <div class="col">
                 <div class="container" style="width:70%"><%--padding:0 10%--%>
                     <div class="row mt-4 mb-2 fw-bold">
                         <div class="col">
                             <span>請求書オプション</span>
                         </div>
                     </div>
                     <div class="row mt-4 mb-2">
                         <div class="col">
                             <div class="container">
                                
                                 <div class="row mb-4">
                                     <div class="col">
                                         <div class="btn-group" role="group" aria-label="Basic example">
                                           
                                             <asp:Button ID="IR1" runat="server" Text="請求書備考１" type="button " CssClass="seikyuubtn" OnClientClick="return seikiyuuChangeColor(this);"/>
                                             <%--<asp:Button ID="IR1" runat="server" Text="請求書備考１" type="button " CssClass="seikyuubtn" OnClick="btnBikou1_Click"/>--%>
                                                    
                                             <asp:Button ID="IR2" runat="server" Text="請求書備考２" type="button " CssClass="seikyuubtn" OnClientClick="return seikiyuuChangeColor(this);"/>
                                             <%--<asp:Button ID="IR2" runat="server" Text="請求書備考２" type="button " CssClass="seikyuubtn" OnClick="btnBikou2_Click"/>--%>
                                                                                      
                                             <asp:Button ID="IR3" runat="server" Text="請求書備考３" type="button " CssClass="seikyuubtn" OnClientClick="return seikiyuuChangeColor(this);"/>
                                             <%--<asp:Button ID="IR3" runat="server" Text="請求書備考３" type="button " CssClass="seikyuubtn" OnClick="btnBikou3_Click"/>--%>
                                                                                      
                                             <asp:Button ID="IR4" runat="server" Text="請求書備考４" type="button " CssClass="seikyuubtn" OnClientClick="return seikiyuuChangeColor(this);"/>
                                             <%--<asp:Button ID="IR4" runat="server" Text="請求書備考４" type="button " CssClass="seikyuubtn" OnClick="btnBikou4_Click"/>--%>
                                             
                                             <asp:Button ID="IR5" runat="server" Text="請求書備考５" type="button " CssClass="seikyuubtn" OnClientClick="return seikiyuuChangeColor(this);"/>
                                             <%--<asp:Button ID="IR5" runat="server" Text="請求書備考５" type="button " CssClass="seikyuubtn" OnClick="btnBikou5_Click"/>--%>

                                         </div>
                                     </div>
                                 </div>
                                 <div class="row mb-2">
                                     <div class="col-4">
                                         <asp:TextBox ID="txt_btnNumber" runat="server" CssClass="form-control TextboxStyle d-none" Style="width:86%;border-color:lightgray" ></asp:TextBox>
                                     
                                     </div>
                                     
                                 </div>
                                  

                                  <div id="div_txt1" class="" runat="server">
                                      <div class="row mb-2">
                                     <div class="col-lg-3">
                                         <label>請求書備考タイトル</label>
                                    </div>
                                     <div class="col-lg-7">
                                         <%--<asp:TextBox ID="txtBikouTitle1" runat="server" OnTextChanged="TB_Bikou1_TextChanged" AutoPostBack="true" CssClass="form-control TextboxStyle" Style="width:86%;border-color:lightgray"></asp:TextBox>--%>
                                         <asp:TextBox ID="txtBikouTitle1" runat="server" CssClass="form-control TextboxStyle txtBikouTitle" Style="width:86%;border-color:lightgray"></asp:TextBox>
                                
                                         <div id="title1_required" class="d-none" runat="server">
                                             <label style="color:red">請求書備考１の請求書備考タイトルが入力されていません。</label>
                                         </div>
                                     </div>
                                 </div>
                                 <div class="row mb-2">
                                     <div class="col">
                                         <label>請求書備考</label>
                                     </div>
                                 </div>
                                 <div class="row mb-2">
                                     <div class="col-lg-10">
                                         <asp:TextBox ID="txtBikou1" runat="server" TextMode="MultiLine" Wrap="true" MaxLength="10000" CssClass="form-control TextboxStyle txtBikou" Style="width:90%;height:105px"></asp:TextBox>
                                
                                         <div id="bikou1_required" class="d-none" runat="server">
                                             <label style="color:red">請求書備考１の請求書備考が入力されていません。</label>
                                         </div>
                                     </div>
                                 </div>
                                 </div>
                                 
                                 <div id="div_txt2" class="" runat="server">
                                      <div class="row mb-2">
                                     <div class="col-lg-3">
                                         <label>請求書備考タイトル</label>
                                    </div>
                                     <div class="col-lg-7">
                                         <%--<asp:TextBox ID="txtBikouTitle2" runat="server" OnTextChanged="TB_Bikou2_TextChanged" AutoPostBack="true" CssClass="form-control TextboxStyle" Style="width:86%;border-color:lightgray"></asp:TextBox>--%>
                                         <asp:TextBox ID="txtBikouTitle2" runat="server" CssClass="form-control TextboxStyle txtBikouTitle" Style="width:86%;border-color:lightgray"></asp:TextBox>
                                
                                         <div id="title2_required" class="d-none" runat="server">
                                             <label style="color:red">請求書備考２の請求書備考タイトルが入力されていません。</label>
                                         </div>
                                     </div>
                                 </div>
                                 <div class="row mb-2">
                                     <div class="col">
                                         <label>請求書備考</label>
                                     </div>
                                 </div>
                                 <div class="row mb-2">
                                     <div class="col-lg-10">
                                         <asp:TextBox ID="txtBikou2" runat="server" TextMode="MultiLine" Wrap="true" MaxLength="10000" CssClass="form-control TextboxStyle txtBikou" Style="width:90%;height:105px"></asp:TextBox>
                                
                                         <div id="bikou2_required" class="d-none" runat="server">
                                             <label style="color:red">請求書備考２の請求書備考が入力されていません。</label>
                                         </div>
                                     </div>
                                 </div>
                                 </div>
                                 
                                 <div id="div_txt3" class="" runat="server">
                                      <div class="row mb-2">
                                     <div class="col-lg-3">
                                         <label>請求書備考タイトル</label>
                                    </div>
                                     <div class="col-lg-7">
                                         <%--<asp:TextBox ID="txtBikouTitle3" runat="server" OnTextChanged="TB_Bikou3_TextChanged" AutoPostBack="true" CssClass="form-control TextboxStyle" Style="width:86%;border-color:lightgray"></asp:TextBox>--%>
                                         <asp:TextBox ID="txtBikouTitle3" runat="server" CssClass="form-control TextboxStyle txtBikouTitle" Style="width:86%;border-color:lightgray"></asp:TextBox>
                                
                                         <div id="title3_required" class="d-none" runat="server">
                                             <label style="color:red">請求書備考3の請求書備考タイトルが入力されていません。</label>
                                         </div>
                                     </div>
                                 </div>
                                 <div class="row mb-2">
                                     <div class="col">
                                         <label>請求書備考</label>
                                     </div>
                                 </div>
                                 <div class="row mb-2">
                                     <div class="col-lg-10">
                                         <asp:TextBox ID="txtBikou3" runat="server" TextMode="MultiLine" Wrap="true" MaxLength="10000" CssClass="form-control TextboxStyle txtBikou" Style="width:90%;height:105px"></asp:TextBox>
                                
                                         <div id="bikou3_required" class="d-none" runat="server">
                                             <label style="color:red">請求書備考3の請求書備考が入力されていません。</label>
                                         </div>
                                     </div>
                                 </div>
                                 </div>
                                 
                                 <div id="div_txt4" class="" runat="server">
                                      <div class="row mb-2">
                                     <div class="col-lg-3">
                                         <label>請求書備考タイトル</label>
                                    </div>
                                     <div class="col-lg-7">
                                         <%--<asp:TextBox ID="txtBikouTitle4" runat="server" OnTextChanged="TB_Bikou4_TextChanged" AutoPostBack="true" CssClass="form-control TextboxStyle" Style="width:86%;border-color:lightgray"></asp:TextBox>--%>
                                         <asp:TextBox ID="txtBikouTitle4" runat="server" CssClass="form-control TextboxStyle txtBikouTitle" Style="width:86%;border-color:lightgray"></asp:TextBox>
                                
                                         <div id="title4_required" class="d-none" runat="server">
                                             <label style="color:red">請求書備考４の請求書備考タイトルが入力されていません。</label>
                                         </div>
                                     </div>
                                 </div>
                                 <div class="row mb-2">
                                     <div class="col">
                                         <label>請求書備考</label>
                                     </div>
                                 </div>
                                 <div class="row mb-2">
                                     <div class="col-lg-10">
                                         <asp:TextBox ID="txtBikou4" runat="server" TextMode="MultiLine" Wrap="true" MaxLength="10000" CssClass="form-control TextboxStyle txtBikou" Style="width:90%;height:105px"></asp:TextBox>
                                
                                         <div id="bikou4_required" class="d-none" runat="server">
                                             <label style="color:red">請求書備考４の請求書備考が入力されていません。</label>
                                         </div>
                                     </div>
                                 </div>
                                 </div>
                                 
                                 <div id="div_txt5" class="" runat="server">
                                      <div class="row mb-2">
                                     <div class="col-lg-3">
                                         <label>請求書備考タイトル</label>
                                    </div>
                                     <div class="col-lg-7">
                                         <%--<asp:TextBox ID="txtBikouTitle5" runat="server" OnTextChanged="TB_Bikou5_TextChanged" AutoPostBack="true" CssClass="form-control TextboxStyle" Style="width:86%;border-color:lightgray"></asp:TextBox>--%>
                                         <asp:TextBox ID="txtBikouTitle5" runat="server" CssClass="form-control TextboxStyle txtBikouTitle" Style="width:86%;border-color:lightgray"></asp:TextBox>
                                
                                         <div id="title5_required" class="d-none" runat="server">
                                             <label style="color:red">請求書備考5の請求書備考タイトルが入力されていません。</label>
                                         </div>
                                     </div>
                                 </div>
                                 <div class="row mb-2">
                                     <div class="col">
                                         <label>請求書備考</label>
                                     </div>
                                 </div>
                                 <div class="row mb-2">
                                     <div class="col-lg-10">
                                         <asp:TextBox ID="txtBikou5" runat="server" TextMode="MultiLine" Wrap="true" MaxLength="10000" CssClass="form-control TextboxStyle txtBikou" Style="width:90%;height:105px"></asp:TextBox>
                                
                                         <div id="bikou5_required" class="d-none" runat="server">
                                             <label style="color:red">請求書備考5の請求書備考が入力されていません。</label>
                                         </div>
                                     </div>
                                 </div>
                                 </div>
                                 
                             </div>
                         </div>
                     </div>
                 
                     <div class="row mb-2 fw-bold">
                         <div class="col">
                             <span>見積計算方法</span>
                         </div>
                     </div>
                     <div class="row mt-4 mb-2">
                         <div class="col">
                             <div class="container">
                                 <div class="row mb-4">
                                     <div class="col">
                                         <div class="btn-group" role="group" aria-label="Basic example">
                                           
                                             <asp:Button ID="btnTotalCost" runat="server" Text="総原価方式" type="button " CssClass="mitsumoribtn" OnClientClick="return mitsumoriChangeColor(this);" /> <%--Height="36px" Width="140px" OnClick="btnTotalCost_Click"--%>
                                                                                            
<%--                                             <asp:TextBox ID="txtTotalCost" runat="server" CssClass="form-control TextboxStyle"></asp:TextBox>--%>
 
                                             <asp:Button ID="btnUnitPrice" runat="server" Text="単価原価方式" type="button " CssClass="mitsumoribtn" OnClientClick="return mitsumoriChangeColor(this);" /> <%--Height="36px" Width="140px" OnClick="btnUnitPrice_Click"--%>
                                            
                                         </div>
                                         <div class="div_hidden">                                    
                                             <asp:TextBox ID="txtMitsumori" runat="server" CssClass="form-control TextboxStyle"></asp:TextBox>

                                         </div>
                                     </div>
                                 </div>
                             </div>
                         </div>
                     </div>
                  
                     <div class="row mb-2 fw-bold">
                         <div class="col">
                             <span>端数処理</span>
                         </div>
                     </div>
                     <div class="row mt-4 mb-2">
                         <div class="col">
                             <div class="container">
                                 <div class="row mb-4">
                                     <div class="col">
                                         <div class="btn-group" role="group" aria-label="Basic example">
                                           
                                             <asp:Button ID="btnTruncate" runat="server" Text="切り捨" type="button " CssClass="tansuushoribtn" OnClientClick="return tansuushoriChangeColor(this);" /> <%--Height="36px" Width="140px" OnClick="btnTruncate_Click" --%>
                                                    
                                             <asp:Button ID="btnRounding" runat="server" Text="四捨五入" type="button " CssClass="tansuushoribtn" OnClientClick="return tansuushoriChangeColor(this);"  /><%--Height="36px" Width="140px" OnClick="btnRounding_Click" --%>
                                             
                                             <asp:Button ID="btnRoundUp" runat="server" Text="切り上げ" type="button " CssClass="tansuushoribtn" OnClientClick="return tansuushoriChangeColor(this);" /> <%--Height="36px" Width="140px" OnClick="btnRoundUp_Click" --%>

                                         </div>
                                     </div>
                                     <div class="div_hidden">     
                                         <asp:TextBox ID="txtTansuushori" runat="server" CssClass="form-control TextboxStyle"></asp:TextBox>
                         
                                     </div>
                                 </div>
                             </div>
                         </div>
                     </div>
                  
                     <div class="row mt-4 mb-2 fw-bold">
                         <div class="col">
                             <span>帳票タイトル</span>
                         </div>
                     </div>
                     <div class="row mt-4 mb-2">
                         <div class="col">
                             <div class="container">
                                 <div class="row mb-2">
                                     <div class="col-lg-2">
                                         <label>納品書</label>
                                    </div>
                                     <div class="col-lg-7">
                                         <asp:TextBox ID="txtNouhinsho" runat="server" MaxLength="7" CssClass="form-control TextboxStyle" Style="width:102%;border-color:lightgray"></asp:TextBox>
                                
                                     </div>
                                 </div>
                                  <div class="row mb-2">
                                     <div class="col-lg-2">
                                         <label>請求書</label>
                                    </div>
                                     <div class="col-lg-7">
                                         <asp:TextBox ID="txtSeikyuusho" runat="server" MaxLength="7" CssClass="form-control TextboxStyle" Style="width:102%;border-color:lightgray"></asp:TextBox>
                                
                                     </div>
                                 </div>
                                  <div class="row mb-2">
                                     <div class="col-lg-2">
                                         <label>納品書兼請求書</label>
                                    </div>
                                     <div class="col-lg-7">
                                         <asp:TextBox ID="txtNouhinshouken" runat="server" MaxLength="7" CssClass="form-control TextboxStyle" Style="width:102%;border-color:lightgray"></asp:TextBox>
                                
                                     </div>
                                 </div>
                             </div>
                         </div>
                    
                         
                     </div>
                 
                     <div class="row mt-4 mb-5">
                             <div class="col-lg-9">
                                 <div class="d-flex justify-content-center">
                                     <asp:Button ID="btnSave" runat="server" Text="保存"  CssClass="btnSave BlueBackgroundButton" Height="36px" Width="140px"  OnClick="btnSave_Click"/><%--Style="background-color:rgba(91,155,213,1) !important;"--%>
                                    
                                 </div> 
                             </div>                                         

                     </div>
                 </div>     
            </div>
        </div>
    </div>
   <script src="../Scripts/jquery-3.3.1.js"></script>
    <script type="text/javascript">

        $(".btnSave").on("click", function () {
            
            var btnVal = document.getElementById('<%=txt_btnNumber.ClientID%>').value;

            if (btnVal == 1) {
                
                var title1 = document.getElementById('<%=txtBikouTitle1.ClientID%>').value;
                var bikou1 = document.getElementById('<%=txtBikou1.ClientID%>').value;
                
                if (title1 == "" && bikou1 != "") {
                    $("#MainContent_title1_required").removeClass("d-none");
                    $("#MainContent_bikou1_required").addClass("d-none");
                    event.preventDefault();
                }
                else if (title1 != "" && bikou1 == "") {
                    $("#MainContent_title1_required").addClass("d-none");
                    $("#MainContent_bikou1_required").removeClass("d-none");
                    event.preventDefault();
                }
            }
            else if (btnVal == 2) {
                
                var title2 = document.getElementById('<%=txtBikouTitle2.ClientID%>').value;
                var bikou2 = document.getElementById('<%=txtBikou2.ClientID%>').value;
                
                if (title2 == "" && bikou2 != "") {
                    $("#MainContent_title2_required").removeClass("d-none");
                    $("#MainContent_bikou2_required").addClass("d-none");
                    event.preventDefault();
                }
                else if (title2 != "" && bikou2 == "") {
                    $("#MainContent_title2_required").addClass("d-none");
                    $("#MainContent_bikou2_required").removeClass("d-none");
                    event.preventDefault();
                }
            }
            else if (btnVal == 3) {
                
                var title3 = document.getElementById('<%=txtBikouTitle3.ClientID%>').value;
                var bikou3 = document.getElementById('<%=txtBikou3.ClientID%>').value;
                
                if (title3 == "" && bikou3 != "") {
                    $("#MainContent_title3_required").removeClass("d-none");
                    $("#MainContent_bikou3_required").addClass("d-none");
                    event.preventDefault();
                }
                else if (title3 != "" && bikou3 == "") {
                    $("#MainContent_title3_required").addClass("d-none");
                    $("#MainContent_bikou3_required").removeClass("d-none");
                    event.preventDefault();
                }
            }
            else if (btnVal == 4) {
                
                var title4 = document.getElementById('<%=txtBikouTitle4.ClientID%>').value;
                var bikou4 = document.getElementById('<%=txtBikou4.ClientID%>').value;
                
                if (title4 == "" && bikou4 != "") {
                    $("#MainContent_title4_required").removeClass("d-none");
                    $("#MainContent_bikou4_required").addClass("d-none");
                    event.preventDefault();
                }
                else if (title4 != "" && bikou4 == "") {
                    $("#MainContent_title4_required").addClass("d-none");
                    $("#MainContent_bikou4_required").removeClass("d-none");
                    event.preventDefault();
                }
            }
            else if (btnVal == 5) {
                
                var title5 = document.getElementById('<%=txtBikouTitle5.ClientID%>').value;
                var bikou5 = document.getElementById('<%=txtBikou5.ClientID%>').value;
                
                if (title5 == "" && bikou5 != "") {
                    $("#MainContent_title5_required").removeClass("d-none");
                    $("#MainContent_bikou5_required").addClass("d-none");
                    event.preventDefault();
                }
                else if (title5 != "" && bikou5 == "") {
                    $("#MainContent_title5_required").addClass("d-none");
                    $("#MainContent_bikou5_required").removeClass("d-none");
                    event.preventDefault();
                }
            }
        });
        
        function getShiftJISByteLength(s) {
               return s.replace(/[^\x00-\x80&#63728;｡｢｣､･ｦｧｨｩｪｫｬｭｮｯｰｱｲｳｴｵｶｷｸｹｺｻｼｽｾｿﾀﾁﾂﾃﾄﾅﾆﾇﾈﾉﾊﾋﾌﾍﾎﾏﾐﾑﾒﾓﾔﾕﾖﾗﾘﾙﾚﾛﾜﾝ ﾞ ﾟ&#63729;&#63730;&#63731;]/g, 'xx').length;
        }
        <%--function copyText() {
            var str = document.getElementById('<%=txtBikouTitle1.ClientID%>').value;
            var byteAmount = getShiftJISByteLength(str);
            while (byteAmount > 5) {
                str = str.substring(0, str.length - 1);
                byteAmount = getShiftJISByteLength(str);
            }
            document.getElementById("<%=txtBikouTitle1.ClientID%>").value = str;

        }--%>

        $(".txtBikouTitle").on('keyup keydown', function (e) {
              //var str = document.getElementById('<%=txtBikouTitle1.ClientID%>').value;
              var str = $(this).val();
            var idVal = document.getElementById('<%=txt_btnNumber.ClientID%>').value;
            //alert(idVal);
            var byteAmount = getShiftJISByteLength(str);
            while (byteAmount > 14) {
                str = str.substring(0, str.length - 1);
                byteAmount = getShiftJISByteLength(str);
            }

            if (idVal == 1) {
                $("#MainContent_title1_required").addClass("d-none");
                document.getElementById("<%=txtBikouTitle1.ClientID%>").value = str;
            }
            else if (idVal == 2) {
                $("#MainContent_title2_required").addClass("d-none");
                document.getElementById("<%=txtBikouTitle2.ClientID%>").value = str;
            }
            else if (idVal == 3) {
                $("#MainContent_title3_required").addClass("d-none");
                document.getElementById("<%=txtBikouTitle3.ClientID%>").value = str;
            }
            else if (idVal == 4) {
                $("#MainContent_title4_required").addClass("d-none");
                document.getElementById("<%=txtBikouTitle4.ClientID%>").value = str;
            }
            else if (idVal == 5) {
                $("#MainContent_title5_required").addClass("d-none");
                document.getElementById("<%=txtBikouTitle5.ClientID%>").value = str;
            }
        }); 

        $(".txtBikou").on('keyup keydown', function (e) {

            var idVal = document.getElementById('<%=txt_btnNumber.ClientID%>').value;
            
            if (idVal == 1) {
                $("#MainContent_bikou1_required").addClass("d-none");
            }
            else if (idVal == 2) {
                $("#MainContent_bikou2_required").addClass("d-none");
            }
            else if (idVal == 3) {
                $("#MainContent_bikou3_required").addClass("d-none");
            }
            else if (idVal == 4) {
                $("#MainContent_bikou4_required").addClass("d-none");
            }
            else if (idVal == 5) {
                $("#MainContent_bikou5_required").addClass("d-none");
            }
        }); 

        function seikiyuuChangeColor(e) {
             
            let btnID = e.id;
            //alert(btnID);
            var sekiyuuBtn = document.querySelectorAll(".seikyuubtn");   
            //console.log(elements);
            for (var i = 0; i < sekiyuuBtn.length; i++) {
                let dd = i + 1;
                if (btnID === sekiyuuBtn[i].id) {
                    document.getElementById('<%=txt_btnNumber.ClientID %>').value = dd;
                    e.style.backgroundColor = "lightGray";
                    //alert(btnID);
                    if ($("#MainContent_div_txt"+dd).hasClass("d-none")) {
                        $("#MainContent_div_txt" + dd).removeClass("d-none");
                    }
                }
                else {
                    $("#MainContent_div_txt"+dd).addClass("d-none")
                    $("#" + sekiyuuBtn[i].id).css("background-color", "white");
                }
                // console.log(elements[i].id);
            }
            return false;
        }

        function mitsumoriChangeColor(e) {
            let btnID = e.id;
            var mitsumoriBtn = document.querySelectorAll(".mitsumoribtn");   
            //console.log(elements);
            for (var i = 0; i < mitsumoriBtn.length; i++) {
                
                if (btnID === mitsumoriBtn[i].id) {
                    e.style.backgroundColor = "lightGray";

                    if (btnID == "MainContent_btnTotalCost") {
                        $("#MainContent_txtMitsumori").val("1");
                    }
                    else {
                        $("#MainContent_txtMitsumori").val("0");
                    }
                }
                else {
                    $("#"+mitsumoriBtn[i].id).css("background-color", "white");
                }
              // console.log(elements[i].id);
            } 
            return false;
        }

        function tansuushoriChangeColor(e) {
            let btnID = e.id;
            var tansuushoriBtn = document.querySelectorAll(".tansuushoribtn");   
            //console.log(elements);
            for (var i = 0; i < tansuushoriBtn.length; i++) {
                
                if (btnID === tansuushoriBtn[i].id) {
                    e.style.backgroundColor = "lightGray";

                    if (btnID == "MainContent_btnTruncate") {
                        $("#MainContent_txtTansuushori").val("0");
                    }
                    else if (btnID == "MainContent_btnRounding") {
                        $("#MainContent_txtTansuushori").val("1");
                    }
                    else {
                        $("#MainContent_txtTansuushori").val("2");
                    }
                }
                else {
                    $("#"+tansuushoriBtn[i].id).css("background-color", "white");
                }
              // console.log(elements[i].id);
            } 

            return false;
        }
</script>
</body>
</html>
</asp:Content>