<%@ Page Language="C#" MasterPageFile="~/WebFront/JC99NavBar.Master" AutoEventWireup="true" CodeBehind="JC10MitsumoriTouroku.aspx.cs" Inherits="jobzcolud.WebFront.JC10MitsumoriTouroku" EnableEventValidation = "false" MaintainScrollPositionOnPostback="true" ValidateRequest ="False"%>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server" >
    <!DOCTYPE html>

<html>
<head>
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
   <%-- <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />--%>
    <meta name="google" content="notranslate"/>
    <title></title>
        <asp:PlaceHolder runat="server">
            <%: Scripts.Render("~/bundles/modernizr") %>
            <%: Styles.Render("~/style/StyleBundle1") %>
            <%: Scripts.Render("~/scripts/ScriptBundle1") %>
            <%: Styles.Render("~/style/UCStyleBundle") %>

        </asp:PlaceHolder>
        <webopt:BundleReference runat="server" Path="~/Content1/bootstrap" />
        <webopt:BundleReference runat="server" Path="~/Content1/css" />
        <script src="../Scripts/colResizable-1.6.min.js"></script>
        <script src="../Scripts/cookie.js"></script>
     <script src="../Scripts/jquery-ui-1.12.1.min.js"></script>
    <style>
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
                table-layout:initial !important;
            }
               .GridViewStyleSyohin 
            {
                   z-index:3 !important;
                overflow:unset !important;
               overflow-x:auto !important;
            }

            
            .GridViewStyleSyohin tr td
            {
                   z-index:3 !important;
                overflow:unset !important;
               overflow-x:auto !important;
            }

              .GridViewStyleSyohin tr th
            {
                  z-index:3 !important;
                overflow:unset !important;
               overflow-x:auto !important;
            }
              
    </style>
<script type="text/javascript">
    Sys.Application.add_load(function () {
        var syouIndex = parseInt(document.getElementById("<%=HF_syohinIndex.ClientID%>").value);
        var dragIndex = parseInt(document.getElementById("<%=HF_dragIndex.ClientID%>").value);
        var dropIndex = parseInt(document.getElementById("<%=HF_dropIndex.ClientID%>").value);

          var strCook = document.cookie;
        if (strCook.indexOf("!~") != 0) {
            var intS = strCook.indexOf("!~");
            var intE = strCook.indexOf("~!");
            var strPos = strCook.substring(intS + 2, intE);
            document.getElementById("<%=Div5.ClientID%>").scrollLeft = strPos;
        }


        function getShiftJISByteLength(s) {
               return s.replace(/[^\x00-\x80&#63728;｡｢｣､･ｦｧｨｩｪｫｬｭｮｯｰｱｲｳｴｵｶｷｸｹｺｻｼｽｾｿﾀﾁﾂﾃﾄﾅﾆﾇﾈﾉﾊﾋﾌﾍﾎﾏﾐﾑﾒﾓﾔﾕﾖﾗﾘﾙﾚﾛﾜﾝ ﾞ ﾟ&#63729;&#63730;&#63731;]/g, 'xx').length;
        }

        $(".txtTani").on('keyup keydown', function (e) {
            var text = $(this).val();
               var byteAmount = getShiftJISByteLength(text);
            if (e.keyCode != "Backspace" && e.key != "Delete")
            {
               while (byteAmount > 4) {
                     text = text.substring(0, text.length - 1);
                   byteAmount = getShiftJISByteLength(text);
                }
                
                document.getElementById("<%=HF_TxtTani.ClientID%>").value = text;
                $(this).val(text);
            }
        });

        $(".txtTani").on('change', function (e) {
              var text = $(this).val();
              var byteAmount = getShiftJISByteLength(text);
            while (byteAmount > 4)
            {
              text = text.substring(0, text.length - 1);
                byteAmount = getShiftJISByteLength(text);
            }
            document.getElementById("<%=HF_TxtTani.ClientID%>").value = text;
        }); 

        $(".txtsSyohin").on('keyup keydown', function (e) {
            var text = $(this).val();
            var txtMaxLength = $(this).attr('maxlength');
            var byteAmount = getShiftJISByteLength(text);
            if (e.key != "Backspace" && e.key != "Delete") {
                while (byteAmount > txtMaxLength) {
                    text = text.substring(0, text.length - 1);
                    byteAmount = getShiftJISByteLength(text);
                   $(this).val(text);
               }
            }
        });

        $(".txtsSyohin").on('change', function (e) {
            var text = $(this).val();
            var txtMaxLength = $(this).attr('maxlength');
            var byteAmount = getShiftJISByteLength(text);
            while (byteAmount > txtMaxLength) {
                    text = text.substring(0, text.length - 1);
                    byteAmount = getShiftJISByteLength(text);
                    $(this).val(text);
               }
        });

       $("#<%= fileUpload1.ClientID %>").change(function () {
           //__doPostBack();
            document.getElementById("<%=BT_ImageUpload.ClientID%>").click();
        }); 
        $("#<%= fileUpload2.ClientID %>").change(function () {
           //__doPostBack();
            document.getElementById("<%=ID=BT_HyoshiImageUpload.ClientID%>").click();
        }); 
        $("#<%= fileUpload3.ClientID %>").change(function () {
           //__doPostBack();
            document.getElementById("<%=BT_Mitumorisyo1ImageUpload.ClientID%>").click();
        }); 
        $("#<%= fileUpload4.ClientID %>").change(function () {
           //__doPostBack();
            document.getElementById("<%=BT_Mitumorisyo2Upload.ClientID%>").click();
        });  

     $(".gvMitumoriSyohin").sortable({
        items: 'tr:not(tr:first-child)',
        cursor: 'pointer',
        //handle:'.dragBtn', /*20220504Myatnoe,Chaw deleted*/
        axis: 'y',
        dropOnEmpty: false,
         start: function (e, ui) {
             ui.item.addClass("selected");
             document.getElementById("<%=HF_beforeSortIndex.ClientID%>").value = ui.item.index();
        },
        stop: function (e, ui) {
            ui.item.removeClass("selected");
            document.getElementById("<%=HF_afterSortIndex.ClientID%>").value = ui.item.index();
            //alert(document.getElementById("<%=HF_beforeSortIndex.ClientID%>").value + ',' + document.getElementById("<%=HF_afterSortIndex.ClientID%>").value);
            document.getElementById("<%=BT_Sort.ClientID%>").click();
            
        },
        receive: function (e, ui) {
            $(this).find("tbody").append(ui.item);
        }
        });

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

                    $(".GridViewStyleSyohin").colResizable({
                        liveDrag: true,
                        resizeMode: 'overflow',
                        partialRefresh: true,
                        postbackSafe: true,
                        disabledColumns: [0, 1, 2, 3, 4,syouIndex, dragIndex, dropIndex],
                        gripInnerHtml: "<div class='grip'></div>",
                        draggingClass: "dragging",
                        onResize: SyohinResized
                    });
    });

    window.onload = function () {
        var intX = 0;
            document.cookie = "xPos=!~" + intX + "~!";
    }

    function SetDivPosition() {
        var intX = document.getElementById("<%=Div5.ClientID%>").scrollLeft;
        document.cookie = "xPos=!~" + intX + "~!";
        }

    function copyToClipBoard()
    {
        var txt = document.getElementById("<%=txtFilePath.ClientID%>");
        txt.select();
        document.execCommand("Copy");
    }

    function onHyoshiPreview()
    {
        if (document.getElementById("<%=HyoshiuploadedImage.ClientID%>").src != "")
        {
            document.getElementById("<%=img01.ClientID%>").src = document.getElementById("<%=HyoshiuploadedImage.ClientID%>").src;
            document.getElementById("<%=modal01.ClientID%>").style.display = "block";
        }
    }  
    function onMitumoriSho1Preview()
    {
        if (document.getElementById("<%=Mitumorisho1uploadedImage.ClientID%>").src != "")
        {
            document.getElementById("<%=img01.ClientID%>").src = document.getElementById("<%=Mitumorisho1uploadedImage.ClientID%>").src;
            document.getElementById("<%=modal01.ClientID%>").style.display = "block";
        }
    }   
    function onMitumoriSho2Preview()
    {
        if (document.getElementById("<%=Mitumorisho2uploadedImage.ClientID%>").src != "")
        {
            document.getElementById("<%=img01.ClientID%>").src = document.getElementById("<%=Mitumorisho2uploadedImage.ClientID%>").src;
            document.getElementById("<%=modal01.ClientID%>").style.display = "block";
        }
    }   

    var DeleteHyoshi = function () {
        event.preventDefault();
        event.stopPropagation();
        document.getElementById("<%=BT_HyoshiImgaeDelete1.ClientID%>").click();
        return false;
    };

    var DeleteMitumorisho1 = function () {
        event.preventDefault();
        event.stopPropagation();
        document.getElementById("<%=BT_Mitumorisho1ImgaeDelete1.ClientID%>").click();
        return false;
    };

    var DeleteMitumorisho2 = function () {
        event.preventDefault();
        event.stopPropagation();
        document.getElementById("<%=BT_Mitumorisho2ImgaeDelete1.ClientID%>").click();
        return false;
    };

    function OnDragEnter(event) {
  event.stopPropagation();
  event.preventDefault();
}

function OnDragOver(event) {
  event.stopPropagation();
  event.preventDefault();
}

    var filename;
    function OnDrop(event,b) {
        event.stopPropagation();
        event.preventDefault();
        var selectedFiles = event.dataTransfer.files;
        var data = new FormData();
        var imageTypes = ['image/png', 'image/PNG', 'image/gif', 'image/bmp', 'image/jpeg', 'image/jpg', 'image/jfif', 'image/JPG', 'image/JPEG'];
        //, 'image/ai', 'image/AI', 'image/PDF', 'image/pdf'
        for (var i = 0; i < selectedFiles.length; i++) {
            //data.append(selectedFiles[i].name, selectedFiles[i]);
            var fileType = selectedFiles[i].type;
            if (imageTypes.includes(fileType)) {
                document.getElementById("<%=HF_ImgSize.ClientID%>").value = selectedFiles[i].size;
                filename = selectedFiles[i].name;
                const reader = new FileReader();
                reader.onloadend = () => {
                    // use a regex to remove data url part
                    const base64String = reader.result
                        .replace("data:", "")
                        .replace(/^.+,/, "");

                    document.getElementById("<%=HF_ImgBase64.ClientID%>").value = base64String;
                    //alert(b);
                    if (b == 0) {
                        document.getElementById("<%=HF_HyoshiFileName.ClientID%>").value = filename;
                        document.getElementById("<%=ID=BT_HyoshiImgaeDrop.ClientID%>").click();
                    }
                    else if (b == 1) {
                        document.getElementById("<%=HF_Gazo1FileName.ClientID%>").value = filename;
                        document.getElementById("<%=ID=BT_Mitumorisho1ImgaeDrop.ClientID%>").click();
                    }
                    else if (b == 2) {
                        document.getElementById("<%=HF_Gazo2FileName.ClientID%>").value = filename;
                        document.getElementById("<%=ID=BT_Mitumorisho2ImgaeDrop.ClientID%>").click();
                    }
                };
                reader.readAsDataURL(selectedFiles[i]);
            }
            else {
                var fileReader = new FileReader();
                var base64String;
                filename = selectedFiles[i].name;
                // Onload of file read the file content
                fileReader.onload = function (fileLoadedEvent) {
                    base64String = fileLoadedEvent.target.result;
                    // Print data in console
                    document.getElementById("<%=HF_ImgBase64.ClientID%>").value = base64String;
                    //alert(b);
                    if (b == 0) {
                        document.getElementById("<%=HF_HyoshiFileName.ClientID%>").value = filename;
                        document.getElementById("<%=ID=BT_HyoshiImgaeDrop.ClientID%>").click();
                    }
                    else if (b == 1) {
                        document.getElementById("<%=HF_Gazo1FileName.ClientID%>").value = filename;
                        document.getElementById("<%=ID=BT_Mitumorisho1ImgaeDrop.ClientID%>").click();
                    }
                    else if (b == 2) {
                        document.getElementById("<%=HF_Gazo2FileName.ClientID%>").value = filename;
                        document.getElementById("<%=ID=BT_Mitumorisho2ImgaeDrop.ClientID%>").click();
                    }
                };
                // Convert data to base64
                fileReader.readAsDataURL(selectedFiles[i]);
            }
        }

        <%--var xhr = new XMLHttpRequest();
        xhr.onreadystatechange = function ()
        {
            if (xhr.readyState == 4 && xhr.status == 200 && xhr.responseText)
            {
                //alert("upload done!");
                document.getElementById("<%=ID=BT_HyoshiImgaeDrop.ClientID%>").click();
            }
            else {
                //alert("fail");
                 }
        };
         //alert("aa");
        xhr.open('POST', "JC10MitsumoriTouroku.aspx");
        //xhr.setRequestHeader("Content-type", "multipart/form-data");
        xhr.send(data);--%>
    //alert(selectedFiles.length);
    }

      function ClickPDFButton()
      {
           document.getElementById("<%=BT_UriagePDF.ClientID %>").click();
    }

    var validSuryoNumber = new RegExp(/^-?[0-9]{0,14}((\.)[0-9]{0,2}){0,1}$/);
        var validRituNumber = new RegExp(/^-?[0-9]{0,8}$/);
        var lastValid_Suryo = '';
        var lastValid_Ritu = '';

        function validateSuryo(elem) {
            if (validSuryoNumber.test(elem.value.replaceAll(',', '')))
            {
                lastValid_Suryo = elem.value;
            } else
            {
                if (lastValid_Suryo == undefined)
                {
                    lastValid_Suryo = "";
                }
                elem.value = lastValid_Suryo;
            }
        }

        function validateRitu(elem) {
            if (validRituNumber.test(elem.value.replaceAll(',', '')))
            {
                lastValid_Ritu = elem.value;
            } else
            {
                if (lastValid_Ritu == undefined)
                {
                    lastValid_Ritu = "";
                }
                elem.value = lastValid_Ritu;
            }
    }

    //20220512 Added Eaindray
        var validJapanese = new RegExp(/^[A-Za-z0-9]*$/);
        var lastValidChar = "";
        function validateJapanese(elem) {
            if (validJapanese.test(elem.value.replaceAll(",", "")))
            {
                 lastValidChar = elem.value;
           }
          else
            {
                if (lastValidChar == undefined) {
                    lastValidChar = "";
                }
                elem.value = lastValidChar;
            }
        }

</script>
    <script type="text/javascript">
     $(function (){

         //$(".GridViewStyle").colResizable({
         //    liveDrag: true,
         //    resizeMode: 'overflow',
         //    postbackSafe: true,
         //    partialRefresh: true,
         //    flush: true,
         //    gripInnerHtml: "<div class='grip'></div>",
         //    draggingClass: "dragging",
         //    onResize: onSampleResized
         //});

         //$(".GridViewStyleSyohin").colResizable({
         //    liveDrag: true,
         //    resizeMode: 'overflow',
         //    postbackSafe: true,
         //    partialRefresh: true,
         //    disabledColumns:[0,1,2,3,5],
         //    gripInnerHtml: "<div class='grip'></div>",
         //    draggingClass: "dragging",
         //    onResize: SyohinResized
         //});

         });


        var onSampleResized = function (e)
        {
             var columns = $(e.currentTarget).find("th");
             var msg = "";
             var date = new Date();
             date.setTime(date.getTime() + (60 * 20000));
            columns.each(function ()
            {
                var txt = $(this).text();
                txt = $.trim(txt.replace(/[\t\n]+/g, ''));
                if (txt == "売上コード") {
                    txt = "cUriage";
                }
                else if (txt == "見積コード") {
                    txt = "cMitumori";
                }
                else if (txt == "請求先名") {
                    txt = "sSeikyusaki";
                }
                else if (txt == "得意先名") {
                    txt = "sTokuisaki";
                }
                else if (txt == "売上件名") {
                    txt = "sUriage";
                }
                else if (txt == "営業担当者") {
                    txt = "sTantou";
                }
                else if (txt == "売上日") {
                    txt = "dUriage";
                }
                else if (txt == "売上会額") {
                    txt = "nUriage";
                }
                else if (txt == "売上状態") {
                    txt = "UriageJoutai";
                }
                else if (txt == "売上社内メモ") {
                    txt = "sMemo";
                }
                else
                {
                    txt = txt;
                }
                msg += txt +","+ $(this).width() + ":";
            })
            document.getElementById("<%=HF_GridUriage.ClientID%>").value = $(e.currentTarget).width();
              document.getElementById("<%=HF_GridSizeUriage.ClientID%>").value = msg;
            document.getElementById("<%=BT_ColumnWidthUriage.ClientID%>").click();
        }; 

        var SyohinResized = function (e)
        {

             var columns = $(e.currentTarget).find("th");
             var msg = "";
            columns.each(function ()
            {
                var txt = $(this).text();
                txt = $.trim(txt.replace(/[\t\n]+/g, ''));
                if (txt == "商品コード") {
                    txt = "cSyouhin";
                }
                else if (txt == "商品名") {

                    txt = "sSyouhin";
                }
                else if (txt == "数量") {
                    
                    txt = "Syouryou";
                }
                else if (txt == "単位") {
                    txt = "tani";
                }
                else if (txt == "標準単価") {
                    txt = "Hyoujuntanka";
                }
                else if (txt == "単価") {
                    txt = "Tanka";
                }
                else if (txt == "合計金額") {
                    txt = "kingaku";
                }
                else if (txt == "原価単価") {
                    txt = "gentanka";
                }
                else if (txt == "掛率") {
                    txt = "ritsu";
                }
                else if (txt == "原価合計") {
                    txt = "genkagokei";
                }
                else if (txt == "粗利") {
                    txt = "arari";
                }
                else if (txt == "粗利率") {
                    txt = "araritsu";
                }
                else
                {
                    txt = txt;
                }
                msg += txt + "," + $(this).width() + ":";
            })
            document.getElementById("<%=HF_GridSyouhin.ClientID%>").value = $(e.currentTarget).width();
            document.getElementById("<%=HF_GridSizeSyouhin.ClientID%>").value = msg;
            document.getElementById("<%=BT_ColumnWidthSyouhin.ClientID%>").click();
        }; 

</script>

</head>
<body style="background-color:#d7e4f2;">
<%--    <form id="form1" runat="server">--%>
       <%-- <asp:ScriptManager ID="ScriptManager1" EnablePageMethods="True" runat="server">--%>
           <%-- <Scripts>
                <asp:ScriptReference Name="jquery" />
                <asp:ScriptReference Name="bootstrap" />
                <asp:ScriptReference Path="../Scripts/Common/FixFocus.js" />
            </Scripts>--%>
      <%--  </asp:ScriptManager>--%>

           <asp:UpdatePanel ID="updHeader" runat="server" UpdateMode="Conditional" RenderMode="Inline">
            <ContentTemplate>
                <div class="JC10MitumoriTourokuDiv" id="div3" runat="server">
                <nav id="nav3" runat="server" class="navbar-expand custom-navbar1 custom-navbar-height" style="z-index:60;position:absolute;">
                    <div class="collapse navbar-collapse JC10navbar" id="Div1" runat="server">
                        <ul class="navbar-nav mr-auto">
                            <li class="nav-item active nav-item-custom1">
                                <asp:LinkButton ID="lkMitumori" runat="server" CssClass="nav-link nav-btn-custom1" Text="見積" ForeColor="Black"  OnClick="lkMitumori_Click"></asp:LinkButton>
                            </li>
                            <li class="nav-item active nav-item-custom1">
                                <asp:LinkButton ID="lkPrint" runat="server" CssClass="nav-link nav-btn-custom1" Text="見積書印刷設定" ForeColor="Black" OnClick="lkPrint_Click"></asp:LinkButton>
                            </li>
                            <li class="nav-item active nav-item-custom1">
                               <asp:LinkButton ID="lkUriage" runat="server" CssClass="nav-link nav-btn-custom1" Text="売上" ForeColor="Black" OnClick="lkUriage_Click"></asp:LinkButton>
                            </li>
                        </ul>
                    </div>
                </nav>
                    </div>
                <asp:Button ID="btnKojinSave" runat="server"  CssClass="DisplayNone" />
        
        <div class="container body-content" style="background-color:#d7e4f2;padding-top:48px;">
        <div class="JC10MitumoriTourokuDiv" id="divMitumoriTorokuP" runat="server" style="margin-bottom:15px;">

                        <table class="JC10MitumoriTourokuWidth JC10MitumoriTourokuTbl" style="width:100% !important;">
                            <tr>
                                <td colspan="2" style="padding-top:12px;">
                                      <div class="JC10DivSave">
                                          <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                        <asp:Button ID="btnMitumoriSave" runat="server" CssClass=" BlueBackgroundButton JC10SaveBtn" Text="見積を保存"
                                            OnClientClick="javascript:disabledTextChange(this);" OnClick="btnMitumoriSave_Click" />
                                        <asp:Button ID="btnBetsuMitumoriSave" runat="server" CssClass=" BlueBackgroundButton JC10SaveBtn" Text="別見積で保存"
                                            OnClientClick="javascript:disabledTextChange(this);" OnClick="btnBetsuMitumoriSave_Click" />
                                        <asp:Button ID="btnNewMitumori" runat="server" CssClass=" BlueBackgroundButton JC10SaveBtn" Text="見積を新規作成"
                                            OnClientClick="javascript:disabledTextChange(this);" OnClick="btnNewMitumori_Click" />
                                        <asp:Button ID="btnTaMitumoriCopy" runat="server" CssClass="BlueBackgroundButton JC10SaveBtn" Text="他見積をコピー"
                                            OnClientClick="PopupOpenButton();" style="margin-right:80px;" OnClick="btnTaMitumoriCopy_Click" />
                                        <asp:Button ID="btnCreateUriage" runat="server" CssClass=" BlueBackgroundButton JC10SaveBtn" Text="売上作成"
                                            OnClientClick="javascript:disabledTextChange(this);" OnClick="btnCreateUriage_Click" Font-Size="14px" Height="35px" />
                                           <asp:Button ID="btnMitumorishoPDF" runat="server" CssClass=" BlueBackgroundButton JC10SaveBtn" Text="見積書PDF出力"
                                            OnClientClick="javascript:disabledTextChange(this);"  OnClick="btnMitumorishoPDF_Click" Font-Size="14px" Height="35px"/>
                                        <asp:Button ID="btnMitumoriDelete" runat="server" CssClass="JC10DeleteBtn" Text="削除"
                                            OnClientClick="javascript:disabledTextChange(this);" OnClick="btnMitumoriDelete_Click" />
                                                            </ContentTemplate>
                                                 <Triggers>
                                                    <asp:PostBackTrigger ControlID="btnMitumorishoPDF" />
                                                     <asp:PostBackTrigger ControlID="btnYes1" />
                                                     <asp:PostBackTrigger ControlID="btnNo1" />
                                                </Triggers>
                                               </asp:UpdatePanel>
                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td class="text-left JC10MitumoriTourokuTd10" valign="top" style="width:50% !important;">
                                    <table>
                                        <%--<tr class="JC10MitumoriTourokuHeightTr3">
                                            <td class="text-left JC11FusenTourokuFirstTd">
                                                <asp:Label ID="lblcMitumoriLabel" runat="server" Text="見積コード"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblcMitumori" runat="server" Text="000000000324" Style="cursor:default;"></asp:Label>
                                                <asp:Label ID="lblcMitumori_Ko" runat="server" Text="" Visible="false"></asp:Label>
                                                <asp:Label ID="lblnHenkou" runat="server" Text="" Visible="false"></asp:Label>
                                            </td>
                                             <td class="text-left JC11FusenTourokuFirstTd">
                                                <asp:Label ID="Label2" runat="server" Text="物件コード"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:LinkButton ID="lnkcBukken" runat="server" Text="000000000324" ForeColor="#557FC9" Style="cursor:pointer;" Font-Underline="False" OnClick="lnkcBukken_Click" Font-Size="13px"></asp:LinkButton>
                                            </td>
                                        </tr>--%>
                                        <tr class="JC10MitumoriTourokuHeightTr3">
                                            <td class="text-left JC10MitumoriTourokuFirstTd1">
                                                <asp:Label ID="lblsMitumoriLabel" runat="server" Text="見積名"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:UpdatePanel ID="updsMitumori" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <asp:TextBox ID="txtsMitumori" runat="server" AutoPostBack="true" MaxLength="100" CssClass="form-control TextboxStyle JC10MitumoriMeiTourokuTextBox"
                                                            onkeyup="DeSelectText(this);" onfocus="this.setSelectionRange(this.value.length, this.value.length);" OnTextChanged="txtsMitumori_TextChanged" ValidateRequestMode="Disabled"></asp:TextBox>
                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="txtsMitumori" EventName="TextChanged" />
                                                    </Triggers>
                                                </asp:UpdatePanel>
                                            </td>
                                        </tr>
                                        <%--<tr class="JC10MitumoriTourokuHeightTr3">
                                            <td class="text-left JC11FusenTourokuFirstTd">
                                                <asp:Label ID="lblJishaTantouLabel" runat="server" Text="自社担当者"></asp:Label>
                                            </td>
                                            <td colspan="3">
                                                <asp:UpdatePanel ID="upd_JISHATANTOUSHA" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <div id="divTantousyaBtn" runat="server">
                                                            <asp:Button runat="server" ID="BT_JisyaTantousya_Add"  Text="追加"　CssClass="JC10GrayButton" Height="30px" onmousedown="getTantouBoardScrollPosition();" OnClick="BT_JisyaTantousya_Add_Click"/>
                                                       </div>
                                                        <div style="float:left;max-width: 300px; display: none;text-overflow:ellipsis;white-space: nowrap;overflow: hidden;padding-bottom:2px;" id="divTantousyaLabel" runat="server">
                                                            <asp:Label ID="lblsJISHATANTOUSHA" runat="server" cssClass="JClinkBtPopup">先小岡太郎</asp:Label>
                                                            <asp:Label ID="lblcJISHATANTOUSHA" runat="server" Visible="false">先小岡太郎</asp:Label>
                                                            <asp:Button ID="BT_sJISHATANTOUSHA_Cross" runat="server" BackColor="White"  Text="✕" style="vertical-align:middle;margin-left:10px; font-weight:bolder; height:30px;border:none;" OnClick="BT_sJISHATANTOUSHA_Cross_Click" />
                                                        </div>
                                                        </ContentTemplate>
                                            </asp:UpdatePanel>
                                            </td>
                                        </tr>
                                        <tr class="JC10MitumoriTourokuHeightTr3">
                                            <td class="text-left JC10MitumoriTourokuFirstTd">
                                                <asp:Label ID="lbldMitumoriLabel" runat="server" Text="見積日"></asp:Label>
                                            </td>
                                            <td class="text-left JC10MitumoriTourokuTd2">
                                                <asp:UpdatePanel ID="updMitumoriDate" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <asp:Button ID="btnMitumoriDate" runat="server" Text="日付を設定" CssClass="JC10GrayButton" onmousedown="getTantouBoardScrollPosition();" Height="30px" Width="90px" OnClick="btnMitumoriDate_Click"/>
                                                        <div id="divMitumoriDate" class="DisplayNone" runat="server">
                                                            <asp:Button ID="btnLeftArrowdMitumori" runat="server" Text="<" CssClass="DateArrowButton" OnClick="btnLeftArrowdMitumori_Click" Visible="false" />
                                                            <asp:Label ID="lbldMitumori" runat="server" Text="" CssClass="GrayLabel"></asp:Label>
                                                            <asp:Label ID="lblMitumoriDateYear" runat="server" CssClass="DisplayNone"></asp:Label>
                                                            <asp:Button ID="btndMitumoriCross" CssClass="CrossBtnGray DisplayNone" runat="server" Text="✕" OnClick="btndMitumoriCross_Click" />
                                                            <asp:Button ID="btnRightArrowdMitumori" runat="server" Text=">" CssClass="DateArrowButton" OnClick="btnRightArrowdMitumori_Click" Visible="false"/>
                                                        </div>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </td>
                                            <td class="text-left JC10MitumoriTourokuTd3">
                                                <asp:Label ID="lblNoukiLabel" runat="server" Text="納期"></asp:Label>
                                            </td>
                                            <td>
                                                 <asp:UpdatePanel ID="updNouki" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <asp:TextBox ID="txtNouki" runat="server" AutoPostBack="true" MaxLength="100" CssClass="form-control TextboxStyle JC10UkewatashibashoTextBox"
                                                            onkeyup="DeSelectText(this);" onfocus="this.setSelectionRange(this.value.length, this.value.length);"></asp:TextBox>
                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="txtNouki" EventName="TextChanged" />
                                                    </Triggers>
                                                </asp:UpdatePanel>
                                            </td>
                                        </tr>
                                        <tr class="JC10MitumoriTourokuHeightTr3">
                                            <td class="text-left JC10MitumoriTourokuFirstTd">
                                                <asp:Label ID="lblKyotenLabel" runat="server" Text="拠点"></asp:Label>
                                            </td>
                                            <td class="text-left JC10MitumoriTourokuTd2">
                                                <asp:UpdatePanel ID="updKyoten" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                         <div id="divKyotenbtn" runat="server">
                                                        <asp:Button ID="btnKyotenAdd" runat="server" Text="追加" Height="30px" CssClass="JC10GrayButton" onmousedown="getTantouBoardScrollPosition();" OnClick="btnKyotenAdd_Click" />
                                                         </div>
                                                        <div style="float: left; max-width: 150px; display: none;text-overflow:ellipsis;white-space: nowrap;overflow: hidden;" id="divKyotenLabel" runat="server">
                                                            <asp:Label ID="lblsKYOTEN" runat="server" cssClass="JClinkBtPopup"></asp:Label>
                                                            <asp:Label ID="lblcKYOTEN" runat="server" Visible="false"></asp:Label>
                                                            <asp:Button ID="BT_sKYOTENLIST_Cross" runat="server" BackColor="White" Text="✕" Style="vertical-align: middle; margin-left: 10px; font-weight: bolder; height: 30px; border: none;" OnClick="BT_sKYOTENLIST_Cross_Click" />
                                                        </div>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </td>
                                            <td class="text-left JC10MitumoriTourokuTd3">
                                                <asp:Label ID="lblYuukou" runat="server" Text="有効期限"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:UpdatePanel ID="updYuukou" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <div id="divYuuKoubtn" runat="server">
                                                        <asp:Button ID="btnYuukou" runat="server" Text="追加" Height="30px" CssClass="JC10GrayButton" onmousedown="getTantouBoardScrollPosition();" OnClick="btnYuukou_Click" />
                                                        </div>
                                                        <div style="float:left;max-width: 150px; display: none;text-overflow:ellipsis;white-space: nowrap;overflow: hidden;" id="divYuukouLabel" runat="server">
                                                            <asp:Label ID="lblsYuukou" runat="server" cssClass="JClinkBtPopup"></asp:Label>
                                                            <asp:Label ID="lblcYuukou" runat="server" Visible="false"></asp:Label>
                                                            <asp:Button ID="btnYuukouCross" runat="server" BackColor="White"  Text="✕" style="vertical-align:middle;margin-left:10px; font-weight:bolder; height:30px;border:none;"  OnClick="btnYuukouCross_Click" />
                                                        </div>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </td>
                                        </tr>
                                        <tr class="JC10MitumoriTourokuHeightTr3">
                                            <td class="text-left JC10MitumoriTourokuFirstTd">
                                                <asp:Label ID="lblShiharaiLabel" runat="server" Text="支払方法"></asp:Label>
                                            </td>
                                            <td class="text-left JC10MitumoriTourokuTd2">
                                                <asp:UpdatePanel ID="updShiharai" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                         <div id="divShiharaibtn" runat="server">
                                                        <asp:Button ID="btnShiharai" runat="server" Text="追加" Height="30px" CssClass="JC10GrayButton" onmousedown="getTantouBoardScrollPosition();" OnClick="btnShiharai_Click" />
                                                        </div>
                                                        <div style="float:left;max-width: 150px; display: none;text-overflow:ellipsis;white-space: nowrap;overflow: hidden;" id="divShiharaiLabel" runat="server">
                                                    <asp:Label ID="lblsShihariai" runat="server" CssClass="JClinkBtPopup"></asp:Label>
                                                   <asp:Label ID="lblcShiharai" runat="server" Visible="false"></asp:Label>
                                                    <asp:Button ID="btnShihariCross" runat="server" BackColor="White"  Text="✕" style="vertical-align:middle;margin-left:10px; font-weight:bolder; height:30px;border:none;"  OnClick="btnShihariCross_Click" />
                                                </div>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </td>
                                            <td class="text-left JC10MitumoriTourokuTd3">
                                                <asp:Label ID="lblUkewatashibashoLabel" runat="server" Text="受渡場所"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:UpdatePanel ID="updUkewatashibasho" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <asp:TextBox ID="txtUkewatashibasho" runat="server" AutoPostBack="true" MaxLength="100" CssClass="form-control TextboxStyle JC10UkewatashibashoTextBox"
                                                            onkeyup="DeSelectText(this);" onfocus="this.setSelectionRange(this.value.length, this.value.length);" OnTextChanged="txtUkewatashibasho_TextChanged"></asp:TextBox>
                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="txtUkewatashibasho" EventName="TextChanged" />
                                                    </Triggers>
                                                </asp:UpdatePanel>
                                            </td>
                                        </tr>
                                        <tr class="JC10MitumoriTourokuHeightTr3">
                                            <td class="text-left JC10MitumoriTourokuFirstTd">
                                                <asp:Label ID="lblJoutaiLabel" runat="server" Text="状態"></asp:Label>
                                            </td>
                                            <td class="text-left JC10MitumoriTourokuTd2" colspan="3">
                                                <asp:UpdatePanel ID="updJoutai" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                         <div id="divJoutaibtn" runat="server">
                                                        <asp:Button ID="btnJoutai" runat="server" Text="追加" Height="30px" CssClass="JC10GrayButton" onmousedown="getTantouBoardScrollPosition();" OnClick="btnJoutai_Click"/>
                                                         </div>
                                                        <div style="float: left; max-width: 150px; display: none;text-overflow:ellipsis;white-space: nowrap;overflow: hidden;" id="divJoutaiLabel" runat="server">
                                                            <asp:Label ID="lblsJoutai" runat="server" ForeColor="#0080C0"></asp:Label>
                                                            <asp:Label ID="lblcJoutai" runat="server" Visible="false"></asp:Label>
                                                            <asp:Button ID="btnJoutaiCross" runat="server" BackColor="White" Text="✕" Style="vertical-align: middle; margin-left: 10px; font-weight: bolder; height: 30px; border: none;" Visible="false"/>
                                                        </div>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </td>
                                        </tr>
                                        <tr class="JC10MitumoriTourokuHeightTr3">
                                            <td class="text-left JC10MitumoriTourokuFirstTd">
                                               <asp:Label ID="lbldJuuchuLabel" runat="server" Text="受注日"></asp:Label>
                                            </td>
                                            <td class="text-left JC10MitumoriTourokuTd2">
                                                <asp:UpdatePanel ID="updJuuchuuDate" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <asp:Button ID="btnJuuchuDate" runat="server" Text="日付を設定" Height="30px" CssClass="JC10GrayButton" onmousedown="getTantouBoardScrollPosition();" Width="90px" OnClick="btnJuuchuDate_Click" />
                                                        <div id="divJuuchuuDate" class="DisplayNone" runat="server">
                                                            <asp:Button ID="btnLeftArrowdJuuchu" runat="server" Text="<" CssClass="DateArrowButton" OnClick="btnLeftArrowdJuuchu_Click" Visible="false" />
                                                            <asp:Label ID="lbldJuuchu" runat="server" Text="" CssClass="GrayLabel"></asp:Label>
                                                            <asp:Label ID="lblJuuchuDateYear" runat="server" CssClass="DisplayNone"></asp:Label>
                                                            <asp:Button ID="btndJuuchuCross" CssClass="CrossBtnGray" runat="server" Text="✕" OnClick="btndJuuchuCross_Click" style="padding-top:2px;"/>
                                                            <asp:Button ID="btnRightArrowdJuuchu" runat="server" Text=">" CssClass="DateArrowButton" OnClick="btnRightArrowdJuuchu_Click" Visible="false"/>
                                                        </div>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </td>
                                            <td class="text-left JC10MitumoriTourokuTd3">
                                                <asp:Label ID="lbldUriageYoteiLabel" runat="server" Text="売上予定日"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:UpdatePanel ID="updUriageYoteiDate" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <asp:Button ID="btnUriageYoteiDate" runat="server" Text="日付を設定" Height="30px" CssClass="JC10GrayButton" onmousedown="getTantouBoardScrollPosition();" Width="90px" OnClick="btnUriageYoteiDate_Click" />
                                                        <div id="divUriageYoteiDate" class="DisplayNone" runat="server">
                                                            <asp:Button ID="btnLeftArrowdUriageYotei" runat="server" Text="<" CssClass="DateArrowButton" OnClick="btnLeftArrowdUriageYotei_Click" visible="false"/>
                                                            <asp:Label ID="lbldUriageYotei" runat="server" Text="" CssClass="GrayLabel"></asp:Label>
                                                            <asp:Label ID="lblUriageYoteiDateYear" runat="server" CssClass="DisplayNone"></asp:Label>
                                                            <asp:Button ID="btndUriageYoteiCross" CssClass="CrossBtnGray" runat="server" Text="✕" OnClick="btndUriageYoteiCross_Click" style="padding-top:2px;"/>
                                                            <asp:Button ID="btnRightArrowdUriageYotei" runat="server" Text=">" CssClass="DateArrowButton" OnClick="btnRightArrowdUriageYotei_Click" visible="false"/>
                                                        </div>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </td>
                                        </tr>
                                        <tr class="JC10MitumoriTourokuHeightTr3">
                                            <td class="text-left JC10MitumoriTourokuFirstTd">
                                                <asp:Label ID="lblJuuchuKingakuLabel" runat="server" Text="受注金額"></asp:Label>
                                            </td>
                                            <td class="text-left JC10MitumoriTourokuTd2" colspan="3">
                                                <asp:UpdatePanel ID="updJuuchuKingaku" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <asp:TextBox ID="txtJuuchuKingaku" runat="server" AutoPostBack="true" MaxLength="100" CssClass="form-control TextboxStyle JC10UkewatashibashoTextBox"
                                                            onkeyup="DeSelectText(this);" onfocus="this.setSelectionRange(this.value.length, this.value.length);" style="text-align:right;" onkeypress="return isNumberKey()" OnTextChanged="txtJuuchuKingaku_TextChanged"></asp:TextBox>
                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="txtJuuchuKingaku" EventName="TextChanged" />
                                                    </Triggers>
                                                </asp:UpdatePanel>
                                            </td>
                                        </tr>--%>
                                    </table>

                                </td>
                                <td class="text-left JC10MitumoriTourokuTd10" valign="top" style="width:50% !important;">
                                    <table>
                                        <tr class="JC10MitumoriTourokuHeightTr3">
                                            <td class="text-left JC10MitumoriTourokuFirstTd1">
                                                <asp:Label ID="lblTokuisakiLabel" runat="server" Text="得意先"></asp:Label>
                                            </td>
                                            <td class="text-left" style="min-width:620px;max-width:620px;">
                                                <asp:UpdatePanel ID="updTokuisaki" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <%--<asp:Button ID="btnTokuisaki" runat="server" Text="追加" CssClass="JC10GrayButton" onmousedown="getTantouBoardScrollPosition();" OnClick="btnTokuisaki_Click" OnClientClick="displayLoadingModal();"/>--%>
                                                   
                                                          <div id="divTokuisakiBtn" runat="server">
                                                 <asp:Button runat="server" ID="btnTokuisaki"  Text="追加" CssClass="JC10GrayButton" onmousedown="getTantouBoardScrollPosition();" OnClientClick="PopupOpenButton()" OnClick="btnTokuisaki_Click" Height="30px"/>
                                       </div>
                                        <div style="float:left;max-width: 300px; display: none;text-overflow:ellipsis;white-space: nowrap;overflow: hidden;padding-top:5px;margin-right:20px;" id="divTokuisakiLabel" runat="server">
                                        <asp:Label ID="lblsTOKUISAKI" runat="server" Text="" cssClass="JClinkBtPopup"></asp:Label>
                                        <asp:Label runat="server" ID="lblcTOKUISAKI" Visible="false"></asp:Label>
                                        <asp:Label runat="server" ID="lblfTokuiSaki" Visible="false"></asp:Label>
                                   </div>
                                       <div id="divTokuisakiSyosai" runat="server" style="display:none;">
                                           <asp:Button runat="server" ID="BT_sTOKUISAKI_Syousai"  Text="詳細"　CssClass="JC10GrayButton" OnClick="BT_sTOKUISAKI_Syousai_Click" Height="30px"/>
                                       </div> 

                                                        </ContentTemplate>
                                                    <Triggers>
                                                       <asp:PostBackTrigger ControlID="BT_sTOKUISAKI_Syousai" />
                                                    </Triggers>
                                                </asp:UpdatePanel>
                                            </td>
                                        </tr>
                                    </table>

                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" style="padding-top:10px;width:100%;">
                                    <asp:Button runat="server" ID="BT_MitsumoriSyosai" Text="見積詳細を表示" CssClass="JC10MitumoriSyosaiButton" Width="145px" OnClick="BT_MitsumoriSyosai_Click"/>
                                    <div style="background-color: rgb(250, 250, 250);padding:30px 30px 10px 30px;margin-bottom:15px;margin-top:-5px;" class="JC10DivMitumoriSyosai" id="DIV_MitsumoriSyosai" runat="server" visible="false">
                                        <table style="width:100% !important;table-layout:fixed;">
                                            <tr>
                                                <td class="text-left JC10MitumoriTourokuTd4" valign="top">
                                                    <table>
                                        <tr class="JC10MitumoriTourokuHeightTr3">
                                            <td class="text-left JC11FusenTourokuFirstTd">
                                                <asp:Label ID="lblcMitumoriLabel" runat="server" Text="見積コード"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblcMitumori" runat="server" Text="000000000324" Style="cursor:default;"></asp:Label>
                                                <asp:Label ID="lblcMitumori_Ko" runat="server" Text="" Visible="false"></asp:Label>
                                                <asp:Label ID="lblnHenkou" runat="server" Text="" Visible="false"></asp:Label>
                                            </td>
                                             <td class="text-left JC11FusenTourokuFirstTd">
                                                <asp:Label ID="Label2" runat="server" Text="物件コード"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:LinkButton ID="lnkcBukken" runat="server" Text="000000000324" ForeColor="#557FC9" Style="cursor:pointer;" Font-Underline="False" OnClick="lnkcBukken_Click" Font-Size="13px" draggable="false"></asp:LinkButton>
                                            </td>
                                        </tr>
                                        <%--<tr class="JC10MitumoriTourokuHeightTr3">
                                            <td class="text-left JC10MitumoriTourokuFirstTd">
                                                <asp:Label ID="Label2" runat="server" Text="見積名"></asp:Label>
                                            </td>
                                            <td colspan="3">
                                                <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <asp:TextBox ID="TextBox1" runat="server" AutoPostBack="true" MaxLength="100" CssClass="form-control TextboxStyle JC10MitumoriMeiTourokuTextBox"
                                                            onkeyup="DeSelectText(this);" onfocus="this.setSelectionRange(this.value.length, this.value.length);" OnTextChanged="txtsMitumori_TextChanged"></asp:TextBox>
                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="txtsMitumori" EventName="TextChanged" />
                                                    </Triggers>
                                                </asp:UpdatePanel>
                                            </td>
                                        </tr>--%>
                                        <tr class="JC10MitumoriTourokuHeightTr3">
                                            <td class="text-left JC11FusenTourokuFirstTd">
                                                <asp:Label ID="lblJishaTantouLabel" runat="server" Text="自社担当者"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:UpdatePanel ID="upd_JISHATANTOUSHA" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <div id="divTantousyaBtn" runat="server">
                                                            <asp:Button runat="server" ID="BT_JisyaTantousya_Add"  Text="追加"　CssClass="JC10GrayButton" Height="30px" OnClientClick="PopupOpenButton()" onmousedown="getTantouBoardScrollPosition();" OnClick="BT_JisyaTantousya_Add_Click"/>
                                                       </div>
                                                        <div style="float:left;max-width: 300px; display: none;text-overflow:ellipsis;white-space: nowrap;overflow: hidden;padding-bottom:2px;" id="divTantousyaLabel" runat="server">
                                                            <asp:Label ID="lblsJISHATANTOUSHA" runat="server" cssClass="JClinkBtPopup">先小岡太郎</asp:Label>
                                                            <asp:Label ID="lblcJISHATANTOUSHA" runat="server" Visible="false">先小岡太郎</asp:Label>
                                                            <asp:Button ID="BT_sJISHATANTOUSHA_Cross" runat="server" BackColor="Transparent"  Text="✕" style="vertical-align:middle;margin-left:10px; font-weight:bolder; height:30px;border:none;" OnClick="BT_sJISHATANTOUSHA_Cross_Click" />
                                                        </div>
                                                        </ContentTemplate>
                                            </asp:UpdatePanel>
                                            </td>
                                            <td class="text-left JC11FusenTourokuFirstTd">
                                                <asp:Label ID="lblJisyaBango" runat="server" Text="自社番号"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:UpdatePanel ID="updJisyaBango" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <asp:TextBox ID="txtJisyaBango" runat="server" AutoPostBack="true" MaxLength="10" CssClass="form-control TextboxStyle" onpaste="return false" AutoCompleteType="Disabled" 
                                                            onkeyup="DeSelectText(this);" onfocus="this.setSelectionRange(this.value.length, this.value.length);" oninput="validateJapanese(this);" Width="88px" Height="28px" style="text-align:right;" OnTextChanged="txtJisyaBango_TextChanged"></asp:TextBox>
                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="txtJisyaBango" EventName="TextChanged" />
                                                    </Triggers>
                                                </asp:UpdatePanel>
                                            </td>
                                        </tr>
                                        <tr class="JC10MitumoriTourokuHeightTr3">
                                            <td class="text-left JC10MitumoriTourokuFirstTd">
                                                <asp:Label ID="lbldMitumoriLabel" runat="server" Text="見積日"></asp:Label>
                                            </td>
                                            <td class="text-left JC10MitumoriTourokuTd2">
                                                <asp:UpdatePanel ID="updMitumoriDate" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <asp:Button ID="btnMitumoriDate" runat="server" Text="日付を設定" CssClass="JC10GrayButton" onmousedown="getTantouBoardScrollPosition();" OnClientClick="PopupOpenButton()" Height="30px" Width="90px" OnClick="btnMitumoriDate_Click"/>
                                                        <div id="divMitumoriDate" class="DisplayNone" runat="server">
                                                            <asp:Button ID="btnLeftArrowdMitumori" runat="server" Text="<" CssClass="DateArrowButton" OnClick="btnLeftArrowdMitumori_Click" Visible="false" />
                                                            <asp:Label ID="lbldMitumori" runat="server" Text="" CssClass="GrayLabel"></asp:Label>
                                                            <asp:Label ID="lblMitumoriDateYear" runat="server" CssClass="DisplayNone"></asp:Label>
                                                            <asp:Button ID="btndMitumoriCross" CssClass="CrossBtnGray DisplayNone" runat="server" Text="✕" OnClick="btndMitumoriCross_Click" />
                                                            <asp:Button ID="btnRightArrowdMitumori" runat="server" Text=">" CssClass="DateArrowButton" OnClick="btnRightArrowdMitumori_Click" Visible="false"/>
                                                        </div>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </td>
                                            <td class="text-left JC10MitumoriTourokuTd3">
                                                <asp:Label ID="lblNoukiLabel" runat="server" Text="納期"></asp:Label>
                                            </td>
                                            <td>
                                                 <asp:UpdatePanel ID="updNouki" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <asp:TextBox ID="txtNouki" runat="server" AutoPostBack="true" MaxLength="100" CssClass="form-control TextboxStyle JC10UkewatashibashoTextBox"
                                                            onkeyup="DeSelectText(this);" onfocus="this.setSelectionRange(this.value.length, this.value.length);"></asp:TextBox>
                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="txtNouki" EventName="TextChanged" />
                                                    </Triggers>
                                                </asp:UpdatePanel>
                                            </td>
                                        </tr>
                                        <tr class="JC10MitumoriTourokuHeightTr3">
                                            <td class="text-left JC10MitumoriTourokuFirstTd">
                                                <asp:Label ID="lblKyotenLabel" runat="server" Text="拠点"></asp:Label>
                                            </td>
                                            <td class="text-left JC10MitumoriTourokuTd2">
                                                <asp:UpdatePanel ID="updKyoten" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                         <div id="divKyotenbtn" runat="server">
                                                        <asp:Button ID="btnKyotenAdd" runat="server" Text="追加" Height="30px" CssClass="JC10GrayButton" onmousedown="getTantouBoardScrollPosition();" OnClientClick="PopupOpenButton()" OnClick="btnKyotenAdd_Click" />
                                                         </div>
                                                        <div style="float: left; max-width: 150px; display: none;text-overflow:ellipsis;white-space: nowrap;overflow: hidden;" id="divKyotenLabel" runat="server">
                                                            <asp:Label ID="lblsKYOTEN" runat="server" cssClass="JClinkBtPopup"></asp:Label>
                                                            <asp:Label ID="lblcKYOTEN" runat="server" Visible="false"></asp:Label>
                                                            <asp:Button ID="BT_sKYOTENLIST_Cross" runat="server" BackColor="Transparent" Text="✕" Style="vertical-align: middle; margin-left: 10px; font-weight: bolder; height: 30px; border: none;display:none;" OnClick="BT_sKYOTENLIST_Cross_Click" />
                                                        </div>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </td>
                                            <td class="text-left JC10MitumoriTourokuTd3">
                                                <asp:Label ID="lblYuukou" runat="server" Text="有効期限"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:UpdatePanel ID="updYuukou" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <div id="divYuuKoubtn" runat="server">
                                                        <asp:Button ID="btnYuukou" runat="server" Text="追加" Height="30px" CssClass="JC10GrayButton" onmousedown="getTantouBoardScrollPosition();"  OnClientClick="PopupOpenButton()"  OnClick="btnYuukou_Click" />
                                                        </div>
                                                        <div style="float:left;max-width: 150px; display: none;text-overflow:ellipsis;white-space: nowrap;overflow: hidden;" id="divYuukouLabel" runat="server">
                                                            <asp:Label ID="lblsYuukou" runat="server" cssClass="JClinkBtPopup"></asp:Label>
                                                            <asp:Label ID="lblcYuukou" runat="server" Visible="false"></asp:Label>
                                                            <asp:Button ID="btnYuukouCross" runat="server" BackColor="Transparent"  Text="✕" style="vertical-align:middle;margin-left:10px; font-weight:bolder; height:30px;border:none;"  OnClick="btnYuukouCross_Click" />
                                                        </div>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </td>
                                        </tr>
                                        <tr class="JC10MitumoriTourokuHeightTr3">
                                            <td class="text-left JC10MitumoriTourokuFirstTd">
                                                <asp:Label ID="lblShiharaiLabel" runat="server" Text="支払方法"></asp:Label>
                                            </td>
                                            <td class="text-left JC10MitumoriTourokuTd2">
                                                <asp:UpdatePanel ID="updShiharai" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                         <div id="divShiharaibtn" runat="server">
                                                        <asp:Button ID="btnShiharai" runat="server" Text="追加" Height="30px" CssClass="JC10GrayButton" onmousedown="getTantouBoardScrollPosition();"  OnClientClick="PopupOpenButton()" OnClick="btnShiharai_Click" />
                                                        </div>
                                                        <div style="float:left;max-width: 150px; display: none;text-overflow:ellipsis;white-space: nowrap;overflow: hidden;" id="divShiharaiLabel" runat="server">
                                                    <asp:Label ID="lblsShihariai" runat="server" CssClass="JClinkBtPopup"></asp:Label>
                                                   <asp:Label ID="lblcShiharai" runat="server" Visible="false"></asp:Label>
                                                    <asp:Button ID="btnShihariCross" runat="server" BackColor="Transparent"  Text="✕" style="vertical-align:middle;margin-left:10px; font-weight:bolder; height:30px;border:none;"  OnClick="btnShihariCross_Click" />
                                                </div>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </td>
                                            <td class="text-left JC10MitumoriTourokuTd3">
                                                <asp:Label ID="lblUkewatashibashoLabel" runat="server" Text="受渡場所"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:UpdatePanel ID="updUkewatashibasho" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <asp:TextBox ID="txtUkewatashibasho" runat="server" AutoPostBack="true" MaxLength="100" CssClass="form-control TextboxStyle JC10UkewatashibashoTextBox"
                                                            onkeyup="DeSelectText(this);" onfocus="this.setSelectionRange(this.value.length, this.value.length);" OnTextChanged="txtUkewatashibasho_TextChanged"></asp:TextBox>
                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="txtUkewatashibasho" EventName="TextChanged" />
                                                    </Triggers>
                                                </asp:UpdatePanel>
                                            </td>
                                        </tr>
                                        <tr class="JC10MitumoriTourokuHeightTr3">
                                            <td class="text-left JC10MitumoriTourokuFirstTd">
                                                <asp:Label ID="lblJoutaiLabel" runat="server" Text="状態"></asp:Label>
                                            </td>
                                            <td class="text-left JC10MitumoriTourokuTd2">
                                                <asp:UpdatePanel ID="updJoutai" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                         <div id="divJoutaibtn" runat="server">
                                                        <asp:Button ID="btnJoutai" runat="server" Text="追加" Height="30px" CssClass="JC10GrayButton"  OnClientClick="PopupOpenButton()" onmousedown="getTantouBoardScrollPosition();" OnClick="btnJoutai_Click"/>
                                                         </div>
                                                        <div style="float: left; max-width: 150px; display: none;text-overflow:ellipsis;white-space: nowrap;overflow: hidden;" id="divJoutaiLabel" runat="server">
                                                            <asp:Label ID="lblsJoutai" runat="server" ForeColor="#0080C0"></asp:Label>
                                                            <asp:Label ID="lblcJoutai" runat="server" Visible="false"></asp:Label>
                                                            <asp:Button ID="btnJoutaiCross" runat="server" BackColor="Transparent" Text="✕" Style="vertical-align: middle; margin-left: 10px; font-weight: bolder; height: 30px; border: none;" Visible="false"/>
                                                        </div>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </td>
                                            <td class="text-left JC11FusenTourokuFirstTd">
                                                 <asp:Label ID="lblKakudo" runat="server" Text="確度"></asp:Label>
                                            </td>
                                            <td>
                                                 <asp:UpdatePanel ID="updKaudo" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                       <asp:DropDownList ID="DDL_Kakudo" runat="server" Width="85px" AutoPostBack="True" Height="28px" CssClass="form-control JC10GridTextBox DisplayNone" style="font-size:13px;padding-top:0px;padding-right:2px;padding-left:2px;margin:0px !important;border-radius:2px;" OnSelectedIndexChanged="DDL_Kakudo_SelectedIndexChanged">
                                                </asp:DropDownList>
                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="DDL_Kakudo" EventName="SelectedIndexChanged" />
                                                    </Triggers>
                                                </asp:UpdatePanel>
                                            </td>
                                        </tr>
                                        <tr class="JC10MitumoriTourokuHeightTr3">
                                            <td class="text-left JC10MitumoriTourokuFirstTd">
                                               <asp:Label ID="lbldJuuchuLabel" runat="server" Text="受注日"></asp:Label>
                                            </td>
                                            <td class="text-left JC10MitumoriTourokuTd2">
                                                <asp:UpdatePanel ID="updJuuchuuDate" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <asp:Button ID="btnJuuchuDate" runat="server" Text="日付を設定" Height="30px" CssClass="JC10GrayButton" onmousedown="getTantouBoardScrollPosition();" Width="90px"  OnClientClick="PopupOpenButton()" OnClick="btnJuuchuDate_Click" />
                                                        <div id="divJuuchuuDate" class="DisplayNone" runat="server">
                                                            <asp:Button ID="btnLeftArrowdJuuchu" runat="server" Text="<" CssClass="DateArrowButton" OnClick="btnLeftArrowdJuuchu_Click" Visible="false" />
                                                            <asp:Label ID="lbldJuuchu" runat="server" Text="" CssClass="GrayLabel"></asp:Label>
                                                            <asp:Label ID="lblJuuchuDateYear" runat="server" CssClass="DisplayNone"></asp:Label>
                                                            <asp:Button ID="btndJuuchuCross" CssClass="CrossBtnGray" runat="server" Text="✕" OnClick="btndJuuchuCross_Click" style="padding-top:2px;"/>
                                                            <asp:Button ID="btnRightArrowdJuuchu" runat="server" Text=">" CssClass="DateArrowButton" OnClick="btnRightArrowdJuuchu_Click" Visible="false"/>
                                                        </div>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </td>
                                            <td class="text-left JC10MitumoriTourokuTd3">
                                                <asp:Label ID="lbldUriageYoteiLabel" runat="server" Text="売上予定日"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:UpdatePanel ID="updUriageYoteiDate" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <asp:Button ID="btnUriageYoteiDate" runat="server" Text="日付を設定" Height="30px" CssClass="JC10GrayButton" onmousedown="getTantouBoardScrollPosition();" Width="90px"  OnClientClick="PopupOpenButton()" OnClick="btnUriageYoteiDate_Click" />
                                                        <div id="divUriageYoteiDate" class="DisplayNone" runat="server">
                                                            <asp:Button ID="btnLeftArrowdUriageYotei" runat="server" Text="<" CssClass="DateArrowButton" OnClick="btnLeftArrowdUriageYotei_Click" visible="false"/>
                                                            <asp:Label ID="lbldUriageYotei" runat="server" Text="" CssClass="GrayLabel"></asp:Label>
                                                            <asp:Label ID="lblUriageYoteiDateYear" runat="server" CssClass="DisplayNone"></asp:Label>
                                                            <asp:Button ID="btndUriageYoteiCross" CssClass="CrossBtnGray" runat="server" Text="✕" OnClick="btndUriageYoteiCross_Click" style="padding-top:2px;"/>
                                                            <asp:Button ID="btnRightArrowdUriageYotei" runat="server" Text=">" CssClass="DateArrowButton" OnClick="btnRightArrowdUriageYotei_Click" visible="false"/>
                                                        </div>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </td>
                                        </tr>
                                        <tr class="JC10MitumoriTourokuHeightTr3">
                                            <td class="text-left JC10MitumoriTourokuFirstTd">
                                                <asp:Label ID="lblJuuchuKingakuLabel" runat="server" Text="受注金額"></asp:Label>
                                            </td>
                                            <td class="text-left JC10MitumoriTourokuTd2">
                                                <asp:UpdatePanel ID="updJuuchuKingaku" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <asp:TextBox ID="txtJuuchuKingaku" runat="server" AutoPostBack="true" MaxLength="100" CssClass="form-control TextboxStyle JC10UkewatashibashoTextBox"
                                                            onkeyup="DeSelectText(this);" onfocus="this.setSelectionRange(this.value.length, this.value.length);" style="text-align:right;" onkeypress="return isNumberKey()" OnTextChanged="txtJuuchuKingaku_TextChanged"></asp:TextBox>
                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="txtJuuchuKingaku" EventName="TextChanged" />
                                                    </Triggers>
                                                </asp:UpdatePanel>
                                            </td>
                                            <td class="text-left JC11FusenTourokuFirstTd">
                                                <asp:Label ID="lblshuryoYoteibi" runat="server" Text="完了予定日"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:UpdatePanel ID="updshuryoYoteibi" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <asp:Button ID="btnshuryoYoteibi" runat="server" Text="日付を設定" Height="30px" CssClass="JC10GrayButton" onmousedown="getTantouBoardScrollPosition();"  OnClientClick="PopupOpenButton()" Width="90px" OnClick="btnshuryoYoteibi_Click" />
                                                        <div id="divshuryoYoteiDate" class="DisplayNone" runat="server">
                                                            <asp:Button ID="divshuryoYoteibi" runat="server" Text="<" CssClass="DateArrowButton" OnClick="btnLeftArrowdJuuchu_Click" Visible="false" />
                                                            <asp:Label ID="lbldshuryoYotei" runat="server" Text="" CssClass="GrayLabel"></asp:Label>
                                                            <asp:Label ID="lblshuryoYoteiDateYear" runat="server" CssClass="DisplayNone"></asp:Label>
                                                            <asp:Button ID="btndshuryoYoteiCross" CssClass="CrossBtnGray" runat="server" Text="✕" OnClick="btndshuryoYoteiCross_Click" style="padding-top:2px;"/>
                                                            <asp:Button ID="Button5" runat="server" Text=">" CssClass="DateArrowButton" OnClick="btnRightArrowdJuuchu_Click" Visible="false"/>
                                                        </div>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </td>
                                        </tr>
                                        <tr class="JC10MitumoriTourokuHeightTr3">
                                            <td class="text-left JC10MitumoriTourokuFirstTd">
                                                  <asp:Label ID="lblJuuchuuYoteibi" runat="server" Text="受注予定日"></asp:Label>
                                            </td>
                                            <td class="text-left JC10MitumoriTourokuTd2">
                                                 <asp:UpdatePanel ID="updJuuchuuYoteibi" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <asp:Button ID="btnJuuchuuYoteibi" runat="server" Text="日付を設定" Height="30px" CssClass="JC10GrayButton" onmousedown="getTantouBoardScrollPosition();"  OnClientClick="PopupOpenButton()" Width="90px" OnClick="btnJuuchuuYoteibi_Click" />
                                                        <div id="divJuuchuuYoteiDate" class="DisplayNone" runat="server">
                                                            <asp:Button ID="divJuuchuuYoteibi" runat="server" Text="<" CssClass="DateArrowButton" OnClick="btnLeftArrowdJuuchu_Click" Visible="false" />
                                                            <asp:Label ID="lbldJuuchuuYotei" runat="server" Text="" CssClass="GrayLabel"></asp:Label>
                                                            <asp:Label ID="lblJuuchuuYoteiDateYear" runat="server" CssClass="DisplayNone"></asp:Label>
                                                            <asp:Button ID="btndJuuchuuYoteiCross" CssClass="CrossBtnGray" runat="server" Text="✕" style="padding-top:2px;" OnClick="btndJuuchuuYoteiCross_Click"/>
                                                            <asp:Button ID="Button4" runat="server" Text=">" CssClass="DateArrowButton" OnClick="btnRightArrowdJuuchu_Click" Visible="false"/>
                                                        </div>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </td>
                                            <td class="text-left JC11FusenTourokuFirstTd">
                                                <asp:Label ID="lblMitumoriKakutei" runat="server" Text="見積確定"></asp:Label>
                                            </td>
                                            <td>
                                                 <asp:UpdatePanel ID="updMitumoriKakuteibi" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <asp:Button ID="btnMitumoriKakuteibi" runat="server" Text="日付を設定" Height="30px" CssClass="JC10GrayButton" onmousedown="getTantouBoardScrollPosition();"  OnClientClick="PopupOpenButton()" Width="90px" OnClick="btnMitumoriKakuteibi_Click" />
                                                        <div id="divMitumoriKakuteiiDate" class="DisplayNone" runat="server">
                                                            <asp:Button ID="Button2" runat="server" Text="<" CssClass="DateArrowButton" OnClick="btnLeftArrowdJuuchu_Click" Visible="false" />
                                                            <asp:Label ID="lbldMitumoriKakutei" runat="server" Text="" CssClass="GrayLabel"></asp:Label>
                                                            <asp:Label ID="lblMitumoriKakuteiDateYear" runat="server" CssClass="DisplayNone"></asp:Label>
                                                            <asp:Button ID="btndMitumoriKakuteiCross" CssClass="CrossBtnGray" runat="server" Text="✕" OnClick="btndMitumoriKakuteiCross_Click" style="padding-top:2px;"/>
                                                            <asp:Button ID="Button6" runat="server" Text=">" CssClass="DateArrowButton" OnClick="btnRightArrowdJuuchu_Click" Visible="false"/>
                                                        </div>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </td>
                                        </tr>
                                    </table>
                                                </td>
                                                <td class="text-left JC10MitumoriTourokuTd4" valign="top">
                                                    <table>
                                                        
                                        <tr class="JC10MitumoriTourokuHeightTr3">
                                            <td class="text-left JC10MitumoriTourokuFirstTd">
                                                <asp:Label ID="lblTokuisakiTantouLabel" runat="server" Text="得意先担当者"></asp:Label>
                                            </td>
                                            <td class="text-left JC10MitumoriTourokuTd2" colspan="3">
                                                <asp:UpdatePanel ID="updTokuisakiTantou" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <%--<asp:Button ID="btnTokuisakiTantou" runat="server" Text="追加" CssClass="JC10GrayButton" onmousedown="getTantouBoardScrollPosition();" />--%>
                                                    
                                                    <div id="divTokuisakiTanBtn" runat="server">
                                                 <asp:Button runat="server" ID="btnTokuisakiTantou"  Text="追加"　CssClass="JC10GrayButton" Height="30px" onmousedown="getTantouBoardScrollPosition();"  OnClientClick="PopupOpenButton()" OnClick="btnTokuisakiTantou_Click" />
                                       </div>
                                        <div style="float:left;max-width: 300px; display: none;text-overflow:ellipsis;white-space: nowrap;overflow: hidden;padding-top:2px;margin-right:17px;" id="divTokuisakiTanLabel" runat="server">
                                              <asp:Label ID="lblsTOKUISAKI_TAN" runat="server" cssClass="JClinkBtPopup"></asp:Label>
                                   <asp:Button ID="BT_sTOKUISAKI_TAN_Cross" runat="server"  Text="✕" BackColor="Transparent" style="vertical-align:middle;margin-left:10px; font-weight:bolder; height:30px;border:none;" OnClick="BT_sTOKUISAKI_TAN_Cross_Click1"/>
                                    <br />  <asp:Label runat="server" ID="lblsTOKUISAKI_TAN_JUN" ForeColor="#0080C0" Visible="false" Text="0"></asp:Label>
                                    
                                        </div>
                                        <div id="divTokuisakiTanSyosai" runat="server" style="display:none;">
                                                 <asp:Button runat="server" ID="BT_sTOKUISAKI_TAN_Syousai"  Text="詳細" Height="30px" CssClass="JC10GrayButton" OnClick="BT_sTOKUISAKI_TAN_Syousai_Click"/>
                                    </div>
                                                    </ContentTemplate>
                                                     <Triggers>
                                                       <asp:PostBackTrigger ControlID="BT_sTOKUISAKI_TAN_Syousai" />
                                                    </Triggers>
                                                </asp:UpdatePanel>
                                            </td>
                                        </tr>
                                        <tr class="JC10MitumoriTourokuHeightTr3">
                                            <td class="text-left JC10MitumoriTourokuFirstTd">
                                                <asp:Label ID="Label3" runat="server" Text="請求先"></asp:Label>
                                            </td>
                                            <td class="text-left JC10MitumoriTourokuTd2" colspan="3">
                                                <asp:UpdatePanel ID="updSEIKYUSAKI" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <%--<asp:Button ID="btnTokuisaki" runat="server" Text="追加" CssClass="JC10GrayButton" onmousedown="getTantouBoardScrollPosition();" OnClick="btnTokuisaki_Click" OnClientClick="displayLoadingModal();"/>--%>
                                                   
                                                          <div id="divSEIKYUSAKIBtn" runat="server">
                                                 <asp:Button runat="server" ID="btnSeikyusaki"  Text="追加" CssClass="JC10GrayButton" Height="30px" onmousedown="getTantouBoardScrollPosition();"  OnClientClick="PopupOpenButton()" OnClick="btnSeikyusaki_Click"/>
                                       </div>
                                        <div style="float:left;max-width: 300px; display: none;text-overflow:ellipsis;white-space: nowrap;overflow: hidden;padding-top:7px;margin-right:20px;" id="divsSEIKYUSAKILabel" runat="server">
                                        <asp:Label ID="lblsSEIKYUSAKI" runat="server" Text="" cssClass="JClinkBtPopup"></asp:Label>
                                        <asp:Label runat="server" ID="lblcSEIKYUSAKI" Visible="false"></asp:Label>
                                   </div>
                                       <div id="divSEIKYUSAKISyosai" runat="server" style="display:none;">
                                           <asp:Button runat="server" ID="BT_sSEIKYUSAKI_Syousai"  Text="詳細" Height="30px"　CssClass="JC10GrayButton" OnClick="BT_sSEIKYUSAKI_Syousai_Click"/>
                                       </div> 

                                                        </ContentTemplate>
                                                    <Triggers>
                                                       <asp:PostBackTrigger ControlID="BT_sSEIKYUSAKI_Syousai" />
                                                    </Triggers>
                                                </asp:UpdatePanel>
                                            </td>
                                        </tr>
                                        <tr class="JC10MitumoriTourokuHeightTr3">
                                            <td class="text-left JC10MitumoriTourokuFirstTd">
                                                <asp:Label ID="lblSeikyusakiTantouLabel" runat="server" Text="請求先担当者"></asp:Label>
                                            </td>
                                            <td class="text-left JC10MitumoriTourokuTd2" colspan="3">
                                                <asp:UpdatePanel ID="updSeikyusakiTantou" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <%--<asp:Button ID="btnTokuisakiTantou" runat="server" Text="追加" CssClass="JC10GrayButton" onmousedown="getTantouBoardScrollPosition();" />--%>
                                                    
                                                    <div id="divSeikyusakiTanBtn" runat="server">
                                                 <asp:Button runat="server" ID="btnSeikyusakiTantou"  Text="追加" Height="30px"　CssClass="JC10GrayButton"  OnClientClick="PopupOpenButton()" onmousedown="getTantouBoardScrollPosition();" OnClick="btnSeikyusakiTantou_Click"/>
                                       </div>
                                        <div style="float:left;max-width: 300px; display: none;text-overflow:ellipsis;white-space: nowrap;overflow: hidden;padding-top:2px;margin-right:17px;" id="divSeikyusakiTanLabel" runat="server">
                                              <asp:Label ID="lblsSEIKYUSAKI_TAN" runat="server" cssClass="JClinkBtPopup"></asp:Label>
                                   <asp:Button ID="BT_sSEIKYUSAKI_TAN_Cross" runat="server"  Text="✕" BackColor="Transparent" style="vertical-align:middle;margin-left:10px; font-weight:bolder; height:30px;border:none;" OnClick="BT_sSEIKYUSAKI_TAN_Cross_Click"/>
                                    <br />  <asp:Label runat="server" ID="lblsSEIKYUSAKI_TAN_JUN" ForeColor="#0080C0" Visible="false" Text="0"></asp:Label>
                                    
                                        </div>
                                        <div id="divSeikyusakiTanSyosai" runat="server" style="display:none;">
                                                 <asp:Button runat="server" ID="BT_sSEIKYUSAKI_TAN_Syousai"  Text="詳細" Height="30px"　CssClass="JC10GrayButton" OnClick="BT_sSEIKYUSAKI_TAN_Syousai_Click"/>
                                    </div>
                                                    </ContentTemplate>
                                                     <Triggers>
                                                       <asp:PostBackTrigger ControlID="BT_sSEIKYUSAKI_TAN_Syousai" />
                                                    </Triggers>
                                                </asp:UpdatePanel>
                                            </td>
                                        </tr>
                                        <tr class="JC10MitumoriTourokuHeightTr4">
                                            <td class="text-left JC10MitumoriTourokuFirstTd">
                                                  <asp:Label ID="lblShanaiMemoLabel" runat="server" Text="メモ"></asp:Label>
                                            </td>
                                            <td class="text-left JC10MitumoriTourokuTd2" colspan="3" valign="bottom">
                                                <asp:UpdatePanel ID="updShanaiMemo" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <asp:TextBox ID="txtShanaiMemo" MaxLength="200" AutoPostBack="true" CssClass="form-control TextboxStyle JC10MitumoriTourokuTextArea" Rows="4" runat="server" TextMode="MultiLine"
                                                            onkeyup="DeSelectText(this);GetCharacterCountLength(this, 200, lblRemainingMemoCount);" onfocus="this.setSelectionRange(this.value.length, this.value.length);" OnTextChanged="txtShanaiMemo_TextChanged"></asp:TextBox>
                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="txtShanaiMemo" EventName="TextChanged" />
                                                    </Triggers>
                                                </asp:UpdatePanel>
                                            </td>
                                        </tr>
                                        <tr class="JC10MitumoriTourokuHeightTr1">
                                            <td class="text-left JC10MitumoriTourokuFirstTd">
                                                <asp:Label ID="lblBikouLabel" runat="server" Text="備考"></asp:Label>
                                            </td>
                                            <td class="text-left JC10MitumoriTourokuTd2" colspan="3">
                                                <asp:UpdatePanel ID="updBikou" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <asp:TextBox ID="txtBikou" MaxLength="200" AutoPostBack="true" CssClass="form-control TextboxStyle JC10MitumoriTourokuTextArea" Rows="4" runat="server" TextMode="MultiLine"
                                                            onkeyup="DeSelectText(this);GetCharacterCountLength(this, 200, lblRemainingMemoCount);" onfocus="this.setSelectionRange(this.value.length, this.value.length);" OnTextChanged="txtBikou_TextChanged"></asp:TextBox>
                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <asp:AsyncPostBackTrigger ControlID="txtBikou" EventName="TextChanged" />
                                                    </Triggers>
                                                </asp:UpdatePanel>
                                            </td>
                                        </tr>
                                        <tr class="JC10MitumoriTourokuHeightTr1" style="display:none;">
                                            <td class="text-left JC10MitumoriTourokuFirstTd">
                                                <asp:Label ID="lblImageLabel" runat="server" Text="画像"></asp:Label>
                                            </td>
                                            <td class="text-left JC10MitumoriTourokuTd2" colspan="3">
                                                <asp:UpdatePanel ID="updImage" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <table>
                                                            <tr>
                                                                <td>
                                                                    <div id="HyoshiDropZone" runat="server" class="JC10dropZoneExternal" onclick="$('#fileUpload2').click();return false;" ondragenter="OnDragEnter(event);" ondragover="OnDragOver(event);" ondrop="OnDrop(event,0);">
                                                                        <div id="HyoshidragZone" align="center" runat="server">
                                                                            <span class="JC10dragZoneText">表紙</span>
                                                                            <br />
                                                                        <img src="../Img/uploadImage.png" style="width:25px;"/>
                                                                        </div>
                                                                        <asp:Button ID="BT_HyoshiImgaeDelete" runat="server" Text="&times;" CssClass="DisplayNone" OnClientClick="DeleteHyoshi();"/>
                                                                        <img id="HyoshiuploadedImage" runat="server" src="" class="JC10DaiHyouImage DisplayNone"/>
                                                                    </div>
                                                                    <div class="JC10ImagePreview">
                                                                        <asp:LinkButton ID="LK_HyoshiImagePreview" runat="server" Text="プレビュー" ForeColor="Black" Font-Underline="false" OnClientClick="onHyoshiPreview();return false;"></asp:LinkButton>
                                                                    </div>
                                                                </td>
                                                                <td>
                                                                    <div id="Mitumorisho1DropZone" class="JC10dropZoneExternal" onclick="$('#fileUpload3').click();return false;" ondragenter="OnDragEnter(event);" ondragover="OnDragOver(event);" ondrop="OnDrop(event,1);">
                                                                        <div id="Mitumorisho1dragZone" runat="server" align="center">
                                                                            <span class="JC10dragZoneText">見積書1枚目</span>
                                                                            <br />
                                                                            <img src="../Img/uploadImage.png" style="width:25px;"/>
                                                                        </div>
                                                                        <asp:Button ID="BT_Mitumorisho1ImgaeDelete" runat="server" Text="&times;" CssClass="DisplayNone" OnClientClick="DeleteMitumorisho1();"/>
                                                                        <img id="Mitumorisho1uploadedImage" runat="server" src="" class="JC10DaiHyouImage DisplayNone"/>
                                                                        <div id="Mitumorisho1dropZone" class="DisplayNone">
                                                                            <span class="dropZoneText">Drop an image here</span>
                                                                        </div>
                                                                    </div>
                                                                    <div class="JC10ImagePreview">
                                                                        <asp:LinkButton ID="LK_Mitumorisho1ImagePreview" runat="server" Text="プレビュー" ForeColor="Black" Font-Underline="false" OnClientClick="onMitumoriSho1Preview();return false;"></asp:LinkButton>
                                                                    </div>
                                                                    
                                                                </td>
                                                                <td>
                                                                    <div id="Mitumorisho2DropZone" class="JC10dropZoneExternal" onclick="$('#fileUpload4').click();return false;" ondragenter="OnDragEnter(event);" ondragover="OnDragOver(event);" ondrop="OnDrop(event,2);">
                                                                        <div id="Mitumorisho2dragZone" runat="server" align="center">
                                                                            <span class="JC10dragZoneText">見積書2枚目</span>
                                                                            <br />
                                                                            <img src="../Img/uploadImage.png" style="width:25px;"/>
                                                                        </div>
                                                                        <asp:Button ID="BT_Mitumorisho2ImgaeDelete" runat="server" Text="&times;" cssClass="DisplayNone" OnClientClick="DeleteMitumorisho2();"/>
                                                                        <img id="Mitumorisho2uploadedImage" runat="server" src="" class="JC10DaiHyouImage DisplayNone"/>
                                                                        <div id="Mitumorisho2dropZone" class="DisplayNone">
                                                                            <span class="dropZoneText">Drop an image here</span>
                                                                        </div>
                                                                    </div>
                                                                    <div class="JC10ImagePreview">
                                                                        <asp:LinkButton ID="LK_Mitumorisho2ImagePreview" runat="server" Text="プレビュー" ForeColor="Black" Font-Underline="false" OnClientClick="onMitumoriSho2Preview();return false;"></asp:LinkButton>
                                                                    </div>
                                                                    
                                                                </td>
                                                            </tr>
                                                        </table>
                                                        <asp:Button ID="BT_HyoshiImgaeDelete1" runat="server" Text="&times;" CssClass="DisplayNone" OnClick="BT_HyoshiImgaeDelete_Click"/>
                                                        <asp:Button ID="BT_Mitumorisho1ImgaeDelete1" runat="server" Text="&times;" CssClass="DisplayNone" OnClick="BT_Mitumorisho1ImgaeDelete_Click"/>
                                                        <asp:Button ID="BT_Mitumorisho2ImgaeDelete1" runat="server" Text="&times;" CssClass="DisplayNone" OnClick="BT_Mitumorisho2ImgaeDelete_Click"/>
                                                        <asp:Button ID="BT_HyoshiImgaeDrop" runat="server" Text="&times;" CssClass="DisplayNone" OnClick="BT_HyoshiImgaeDrop_Click"/>
                                                        <asp:Button ID="BT_Mitumorisho1ImgaeDrop" runat="server" Text="&times;" CssClass="DisplayNone" OnClick="BT_Mitumorisho1ImgaeDrop_Click"/>
                                                        <asp:Button ID="BT_Mitumorisho2ImgaeDrop" runat="server" Text="&times;" CssClass="DisplayNone" OnClick="BT_Mitumorisho2ImgaeDrop_Click"/>
                                                    </ContentTemplate>
                                                    <Triggers>
                                                        <asp:PostBackTrigger ControlID="BT_HyoshiImageUpload" />
                                                        <asp:PostBackTrigger ControlID="BT_Mitumorisyo1ImageUpload" />
                                                        <asp:PostBackTrigger ControlID="BT_Mitumorisyo2Upload" />
                                                        <asp:AsyncPostBackTrigger ControlID="BT_HyoshiImgaeDelete1" EventName="Click"/>
                                                        <asp:AsyncPostBackTrigger ControlID="BT_Mitumorisho1ImgaeDelete" EventName="Click"/>
                                                        <asp:AsyncPostBackTrigger ControlID="BT_Mitumorisho2ImgaeDelete" EventName="Click"/>
                                                        <asp:AsyncPostBackTrigger ControlID="BT_HyoshiImgaeDrop" EventName="Click"/>
                                                         <asp:AsyncPostBackTrigger ControlID="BT_Mitumorisho1ImgaeDrop" EventName="Click"/>
                                                         <asp:AsyncPostBackTrigger ControlID="BT_Mitumorisho2ImgaeDrop" EventName="Click"/>
                                                     </Triggers>
                                                </asp:UpdatePanel>
                                            </td>
                                        </tr>
                                        <tr class="JC10MitumoriTourokuHeightTr2" style="display:none;">
                                            <td class="text-left" colspan="4">
                                                <asp:Panel ID="ImagePanel" runat="server" BorderColor="#41719C" BorderStyle="Solid" BorderWidth="1px" Height="250px" Width="350px" CssClass="JC10ImagePanel">
                                                    <asp:LinkButton ID="lkImageUpload" runat="server" CssClass="JC10DaiHyouImageLink" Font-Underline="False" OnClick="lkImageUpload_Click">代表画像をアップロード</asp:LinkButton>
                                                    <img src="../Img/images.jpg" id="imgDaihyou" class="JC10DaiHyouImage DisplayNone" runat="server"/>
                                                </asp:Panel>
                                            </td>
                                        </tr>
                                        <tr style="height:4px;">
                                            <td class="text-left JC10MitumoriTourokuFirstTd">
                                                
                                            </td>
                                            <td class="text-left JC10MitumoriTourokuTd2">
                                                
                                            </td>
                                            <td class="text-left JC10MitumoriTourokuTd3">
                                                
                                            </td>
                                            <td>
                                                
                                            </td>
                                        </tr>
                                        <tr class="JC10MitumoriTourokuHeightTr5" style="display:none;">
                                            <td class="text-left JC10MitumoriTourokuFirstTd">
                                                <asp:Label ID="lblFilePathLabel" runat="server" Text="ファイルパス"></asp:Label>
                                            </td>
                                            <td class="text-left JC10MitumoriTourokuTd2" colspan="3">
                                                <asp:UpdatePanel ID="updFileSelect" runat="server" UpdateMode="Conditional">
                                                    <ContentTemplate>
                                                        <asp:Button ID="btnFileSelect" runat="server" Text="選択" CssClass="JC10GrayButton" onmousedown="getTantouBoardScrollPosition();" onClientClick="$('#fileUpload1').click();return false;"  />
                                                             
                                                         <asp:Button ID="btnFilePathCopy" runat="server" Text="パスをコピー" CssClass="JC10GrayButton" onmousedown="getTantouBoardScrollPosition();" onClientClick="copyToClipBoard()"/>
                                                          
                                                    </ContentTemplate>
                                                     <Triggers>
                                                         <asp:PostBackTrigger ControlID="BT_ImageUpload" />
                                                     </Triggers>
                                                </asp:UpdatePanel>
                                            </td>
                                        </tr>
                                        <tr style="display:none;">
                                            <td class="text-left JC10MitumoriTourokuFirstTd"></td>
                                            <td class="text-left JC10MitumoriTourokuTd2" colspan="3">
                                              <%--  <asp:Label ID="lblFilePath" runat="server" Text="C:\signjobzimg\000000000324\1212"></asp:Label>--%>
                                                 <asp:TextBox ID="txtFilePath" runat="server" AutoCompleteType="Disabled" AutoPostBack="False" BorderStyle="None" ReadOnly="True" CssClass="JC10txtFilePath" BackColor="#FAFAFA"></asp:TextBox>
                                            </td>
                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                        
                                        
                                    </div>
                                </td>
                                <asp:FileUpload ID="fileUpload2" runat="server" accept="image/*,application/pdf,application/postscript" CssClass="DisplayNone"  ClientIDMode="Static"/>
                                <asp:FileUpload ID="fileUpload3" runat="server" accept="image/*,application/pdf,application/postscript" CssClass="DisplayNone"  ClientIDMode="Static"/>
                                <asp:FileUpload ID="fileUpload4" runat="server" accept="image/*,application/pdf,application/postscript" CssClass="DisplayNone"  ClientIDMode="Static"/>
                                <asp:FileUpload ID="fileUpload1" runat="server" accept="image/*" CssClass="DisplayNone"  ClientIDMode="Static"/>
                            </tr>
                            <tr>
                                <td class="text-left JC10MitumoriTourokuTd10" valign="top" style="width:50% !important;">
                                    <table style="width:100% !important;table-layout:fixed;">
                                        <tr class="JC10MitumoriTourokuHeightTr">
                                            <td class="text-left">
                                                <div style="float: left; width:60px; position: relative; border: 1px solid white;">
                                               <asp:Label ID="lblSakuseibiLabel" runat="server" Text="作成日"></asp:Label>
                                                </div>
                                                <div style="float: left; margin-left: 0px; width: 150px; position: relative; border: 1px solid white;">
                                                <asp:Label ID="lblSakusekibi" runat="server" Text=""></asp:Label>
                                                </div>
                                            </td>
                                            <td>
                                                 <div style="float: left; width: 60px; position: relative; border: 1px solid white;">
                                                 <asp:Label ID="lblSakuseisyaLabel" runat="server" Text="作成者"></asp:Label>
                                                     </div>
                                                <div style="float: left; margin-left: 0px; width: 150px; position: relative; border: 1px solid white;">
                                                <asp:Label ID="lblSakuseisya" runat="server" Text=""></asp:Label>
                                                <asp:Label ID="lblcSakuseisya" runat="server" Visible="false" ></asp:Label>
                                                    </div>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td class="text-left JC10MitumoriTourokuTd10" valign="top" style="width:50% !important;">
                                    <table style="width:100% !important;table-layout:fixed;">
                                        <tr class="JC10MitumoriTourokuHeightTr">
                                            <td class="text-left JC10MitumoriTourokuFirstTd">
                                                 <div style="float: left; width:60px; position: relative; border: 1px solid white;">
                                               <asp:Label ID="lblHenkoubiLabel" runat="server" Text="更新日"></asp:Label>
                                            </div>
                                                <div style="float: left; margin-left: 0px; width: 150px; position: relative; border: 1px solid white;">
                                                <asp:Label ID="lblHenkoubi" runat="server" Text=""></asp:Label>
                                                    </div>
                                            </td>
                                            <td class="text-left JC10MitumoriTourokuTd3">
                                                  <div style="float: left; width:95px; position: relative; border: 1px solid white;">
                                                <asp:Label ID="lblHenkousyaLabel" runat="server" Text="最終更新者"></asp:Label>
                                            </div>
                                                <div style="float: left; margin-left: 0px; width: 150px; position: relative; border: 1px solid white;">
                                                <asp:Label ID="lblHenkousya" runat="server" Text=""></asp:Label>
                                                 <asp:Label ID="lblcHenkousya" runat="server" Visible="false" ></asp:Label>
                                                    </div>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
 
            <div class="JC10Borderline"></div>
            <asp:UpdatePanel ID="updKingaku" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                    <div class="JC10DivMitumoriJouhou">
                        <table class="JC10MitumoriTourokuTbl">
                            <tr>
                                <td rowspan="2" class="JC10MitumoriTourokuTd6" align="left">
                                    <asp:UpdatePanel ID="updMidashitsuika" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:Button ID="btnMidashitsuika" runat="server" Text="見出し追加" CssClass="JC10GrayButton" onmousedown="getTantouBoardScrollPosition();" OnClick="btnMidashitsuika_Click" />
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td rowspan="2" class="JC10MitumoriTourokuTd7" align="left">
                                    <asp:UpdatePanel ID="updShokeitsuika" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:Button ID="btnShokeitsuika" runat="server" Text="小計追加" CssClass="JC10GrayButton" onmousedown="getTantouBoardScrollPosition();" OnClick="btnShokeitsuika_Click" />
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td rowspan="2" class="JC10MitumoriTourokuTd6" align="left">
                                    <asp:UpdatePanel ID="updFukusuCopy" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:Button ID="btnFukusuCopy" runat="server" Text="複数コピー" CssClass="JC10GrayButton" onmousedown="getTantouBoardScrollPosition();" OnClick="btnFukusuCopy_Click" />
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td rowspan="2" class="JC10MitumoriTourokuTd9" align="left">
                                    <asp:UpdatePanel ID="updTamitumoriCopy" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate> <%--20220502 Updated Eaindray 他見積商品をコピー -> 他見積行複写--%>
                                            <asp:Button ID="btnTamitsumoriSyohinCopy" runat="server" Text="他見積行複写" CssClass="JC10GrayButton" OnClientClick="PopupOpenButton()" onmousedown="getTantouBoardScrollPosition();" OnClick="btnTamitsumoriSyohinCopy_Click" />
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td rowspan="2" class="JC10MitumoriTourokuTd_del" align="left">  <%--20220502 Added Eaindray--%>
                                    <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:Button ID="btn_delete" runat="server" Text="削除" CssClass="JC10GrayButton" onmousedown="getTantouBoardScrollPosition();" OnClick="btn_delete_Click"/>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td rowspan="2" class="JC10MitumoriTourokuTd8" align="left">
                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:Button ID="btnAraritsuIkkatsu" runat="server" Text="粗利率一括設定" CssClass="JC10GrayButton" onmousedown="getTantouBoardScrollPosition();"  OnClientClick="PopupOpenButton()" OnClick="btnAraritsuIkkatsu_Click"/>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                                <td class="JC10MitumoriTourokuFirstTd">
                                    <asp:Label ID="lblGokeiKingakuLabel" runat="server" Text="合計金額"></asp:Label>
                                </td>
                                <td align="right" class="JC10KingakuLabel">
                                    <asp:Label ID="lblGokeiKingaku" runat="server" Text="0"></asp:Label>
                                </td>
                                <td class="JC10MitumoriTourokuFirstTd">
                                    <asp:Label ID="lblKingakuLabel" runat="server" Text="金額"></asp:Label>
                                </td>
                                <td align="right" class="JC10KingakuLabel">
                                    <asp:Label ID="lblKingaku" runat="server" Text="0"></asp:Label>
                                </td>
                                <td class="JC10MitumoriTourokuTd3">
                                    <asp:Label ID="lblShohizeiLabel" runat="server" Text="消費税"></asp:Label>
                                </td>
                                <td align="right" class="JC10KingakuLabel">
                                    <asp:Label ID="lblShohizei" runat="server" Text="0"></asp:Label>
                                </td>
                                <td colspan="3" class="JC10HyojikoumoukuTd">
                                    <asp:UpdatePanel ID="updDisplayItemSetting" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:Button ID="btnDisplayItemSetting" runat="server" CssClass="JC07HyojiItemSettingBtn" Text="表示項目を設定"
                                            UseSubmitBehavior="false" Width="124px" OnClick="btnDisplayItemSetting_Click"  OnClientClick="PopupOpenButton()"/>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </td>
                            </tr>
                            <tr>
                                <td class="JC10MitumoriTourokuFirstTd">
                                    <asp:Label ID="lblShusseiNebikiLabel" runat="server" Text="出精値引"></asp:Label>
                                </td>
                                <td style="max-width:200px;padding-right:4px;">
                                    <%--<asp:Label ID="lblShusseiNebiki" runat="server" Text="10,000" ></asp:Label>--%>
                                    <asp:TextBox ID="txtShusseiNebiki" runat="server" Text="0" Width="90px" Height="25px" MaxLength="10" CssClass="form-control TextboxStyle JC10GridTextBox" autocomplete="off" AutoPostBack="true" style="text-align:right;" onkeypress="return isNumberKey()" OnTextChanged="txtShusseiNebiki_TextChanged"></asp:TextBox>
                                     <asp:FilteredTextBoxExtender ID="TBF_ShusseiNebiki" runat="server" FilterType="Numbers" TargetControlID="txtShusseiNebiki" />
                                </td>
                                <td class="JC10MitumoriTourokuFirstTd">
                                    <asp:Label ID="lblTeikaGokeiLabel" runat="server" Text="定価合計"></asp:Label>
                                </td>
                                <td align="right" class="JC10KingakuLabel">
                                    <asp:Label ID="lblTeikaGokei" runat="server" Text="0"></asp:Label>
                                     <asp:Label ID="lblnSIIRE_G" runat="server" Text="" Visible="false"></asp:Label>
                                </td>
                                <td class="JC10MitumoriTourokuTd3">
                                    <asp:Label ID="lblArariLabel" runat="server" Text="粗利"></asp:Label>
                                </td>
                                <td align="right" class="JC10KingakuLabel">
                                    <asp:Label ID="lblArari" runat="server" Text="0"></asp:Label>
                                </td>
                                <td class="JC10MitumoriTourokuTd3">
                                    <asp:Label ID="lblArariRitsuLabel" runat="server" Text="粗利率"></asp:Label>
                                </td>
                                <td align="right" class="JC10KingakuLabel">
                                    <asp:Label ID="lblArariRitsu" runat="server" Text="0.0%"></asp:Label>
                                </td>
                                <td>

                                </td>
                            </tr>
                        </table>
                    </div>
            </ContentTemplate>
                </asp:UpdatePanel>
            <div id="Div5" runat="server" style="margin-left: 20px;margin-right:20px;overflow-x:auto;"  onscroll="SetDivPosition()">
                    <div id="Div7" runat="server" style="width:auto;display:inline-block;">
                        <asp:UpdatePanel ID="updMitsumoriSyohinGrid" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>

                                <asp:GridView runat="server" ID="GV_MitumoriSyohin_Original" BorderColor="Transparent" AutoGenerateColumns="false" EmptyDataRowStyle-CssClass="JC10NoDataMessageStyle" CssClass="gvMitumoriSyohin GridViewStyleSyohin"
                                    ShowHeader="true" ShowHeaderWhenEmpty="true" RowStyle-CssClass="GridRow" CellPadding="0" onrowdatabound="GV_MitumoriSyohin_Original_RowDataBound" Visible="false">
                                    <EmptyDataRowStyle CssClass="JC10NoDataMessageStyle" />
                                    <HeaderStyle Height="37px" BackColor="#F2F2F2" />
                                    <RowStyle CssClass="GridRow" Height="37px" />
                                    <SelectedRowStyle BackColor="#EBEBF5" />
                                    <Columns>
                                        <asp:TemplateField ItemStyle-CssClass="AlignCenter" HeaderStyle-CssClass="JC10MitumoriGridHeaderStyle JC10CheckBox">
                                            <ItemTemplate>
                                                 <div style="text-align: center;padding-left:2px;overflow: hidden;  display: -webkit-box;  -webkit-line-clamp: 1; -webkit-box-orient: vertical;word-break: break-all;">
                                                <asp:CheckBox ID="chkSelectSyouhin" runat="server" AutoPostBack="true" CssClass="M01AnkenGridCheck" OnCheckedChanged="chkSelectSyouhin_CheckedChanged" TabIndex="-1"/>
                                                <asp:Label ID="lblhdnStatus" runat="server" Text='<%#Eval("status") %>' CssClass="DisplayNone"></asp:Label>
                                                <asp:Label ID="lblfgenkatanka" runat="server" Text='<%#Eval("fgentankatanka") %>' CssClass="DisplayNone"></asp:Label>
                                                 <asp:Label ID="lblRowNo" runat="server" Text='<%#Eval("rowNo") %>' CssClass="DisplayNone"></asp:Label>
                                                     <asp:Label ID="lblfjitais" runat="server" Text='<%#Eval("fJITAIS") %>' CssClass="DisplayNone"></asp:Label>
                                                 <asp:Label ID="lblfjitaiq" runat="server" Text='<%#Eval("fJITAIQ") %>' CssClass="DisplayNone"></asp:Label>
                                                     </div>
                                            </ItemTemplate>
                                            <HeaderTemplate>
                                                <div class="grip" style="text-align: center;padding-left:2px;overflow: hidden;  display: -webkit-box;  -webkit-line-clamp: 1; -webkit-box-orient: vertical;word-break: break-all;">
                                                    <asp:Label ID="LB_chk" runat="server" Text="checkbox" style="display:none;"></asp:Label>
                                                    </div>
                                            </HeaderTemplate>
                                            <HeaderStyle CssClass="JC10MitumoriGridHeaderStyle JC10CheckBox" BorderWidth="2px" />
                                            <ItemStyle CssClass="JC10CheckBox AlignCenter" />
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-CssClass="AlignCenter" HeaderStyle-CssClass="JC10MitumoriGridHeaderStyle JC10ButtonCol">
                                            <ItemTemplate>
                                                 <div class="grip" style="padding-top: 0px;vertical-align: middle; text-align: center;">
                                                <asp:UpdatePanel ID="updSyohinAddBtn" runat="server" UpdateMode="Conditional">
                                                    <Triggers>
                                                         <asp:AsyncPostBackTrigger ControlID="btnSyouhinAdd" EventName="Click"/>
                                                     </Triggers>
                                                     <ContentTemplate>
                                                    <asp:Button ID="btnSyouhinAdd" runat="server" Text="＋" CssClass="JC09GridGrayBtn" onmousedown="getTantouBoardScrollPosition();" Width="30px" Height="28px" OnClick="btnSyouhinAdd_Click" TabIndex="-1"/>
                                                   </ContentTemplate>
                                                </asp:UpdatePanel>
                                                     </div>
                                            </ItemTemplate>
                                            <HeaderTemplate>
                                                <div class="grip" style="padding-top: 0px;vertical-align: middle; text-align: center;">
                                                <asp:Label ID="LBAddSyouhin" runat="server" Text="AddSyouhin" style="display:none;"></asp:Label>
                                                    </div>
                                            </HeaderTemplate>
                                            <HeaderStyle CssClass="JC10MitumoriGridHeaderStyle JC10ButtonCol" BorderWidth="2px" />
                                            <ItemStyle CssClass="AlignCenter JC10ButtonCol" />
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-CssClass="AlignCenter" HeaderStyle-CssClass="JC10MitumoriGridHeaderStyle JC10ButtonCol">
                                            <ItemTemplate>
                                                <div class="grip" style="padding-top: 0px;vertical-align: middle; text-align: center;">
                                                    <asp:UpdatePanel ID="updSyohinCopyBtn" runat="server" UpdateMode="Conditional">
                                                    <Triggers>
                                                         <asp:AsyncPostBackTrigger ControlID="btnSyohinCopy" EventName="Click"/>
                                                     </Triggers>
                                                     <ContentTemplate>
                                                     <asp:Button ID="btnSyohinCopy" runat="server" Text="コ" CssClass="JC09GridGrayBtn" onmousedown="getTantouBoardScrollPosition();" Width="30px" Height="28px" TabIndex="-1" OnClick="btnSyohinCopy_Click" />
                                                   </ContentTemplate>
                                                </asp:UpdatePanel>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderTemplate>
                                                  <div class="grip" style="padding-top: 0px;vertical-align: middle; text-align: center;">
                                                <asp:Label ID="LBCopySyouhin" runat="server" Text="CopySyouhin" style="display:none;"></asp:Label>
                                                      </div>
                                            </HeaderTemplate>
                                            <HeaderStyle CssClass="JC10MitumoriGridHeaderStyle JC10ButtonCol" BorderWidth="2px"/>
                                            <ItemStyle CssClass="AlignCenter JC10ButtonCol" />
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-CssClass="AlignCenter" HeaderStyle-CssClass="JC10MitumoriGridHeaderStyle JC10ButtonCol">
                                            <ItemTemplate>
                                                <div class="grip" style="padding-top: 0px;vertical-align: middle; text-align: center;">
                                                <asp:Button ID="btnSyohinShosai" runat="server" Text="詳" CssClass="JC09GridGrayBtn" onmousedown="getTantouBoardScrollPosition();" Width="30px" Height="28px" TabIndex="-1"  OnClientClick="PopupOpenButton()" OnClick="btnSyohinShosai_Click"/>
                                                    <div style="padding-right:3px;">
                                                <asp:TextBox ID="txtMidashi" runat="server" Text='' Height="25px" Width="100%" MaxLength="10" CssClass="form-control TextboxStyle JC10GridTextBox" autocomplete="off" AutoPostBack="true" Visible="false" placeholder="見出しを入力" OnTextChanged="txtsSYOHIN_TextChanged"></asp:TextBox>
                                                <asp:TextBox ID="txtSyokei" runat="server" Text='小計' Height="25px" Width="100%" MaxLength="10" CssClass="form-control TextboxStyle JC10GridTextBox" autocomplete="off" AutoPostBack="true" Visible="false" style="text-align:right;" Enabled="false"></asp:TextBox>
                                                        </div>
                                                 </div>
                                            </ItemTemplate>
                                            <HeaderTemplate>
                                                <div class="grip" style="padding-top: 0px;vertical-align: middle; text-align: center;">
                                                <asp:Label ID="LB_SyouhinSyosai" runat="server" Text="SyouhinSyosai" style="display:none;"></asp:Label>
                                                    </div>
                                            </HeaderTemplate>
                                            <HeaderStyle CssClass="JC10MitumoriGridHeaderStyle JC10ButtonCol" BorderWidth="2px"/>
                                            <ItemStyle CssClass="AlignCenter JC10ButtonCol"/>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-Width="30px">
                                            <ItemTemplate>   
                                                 <div class="grip" style="display:flex;align-content:center;align-items:center;align-items: center;justify-content: center;z-index:2;">
                                                         <asp:HoverMenuExtender ID="HoverMenuExtender" runat="server" PopupControlID="PopupMenuSyo"
                                                    TargetControlID="PopupMenuBtnSyo" PopupPosition="bottom">
                                                </asp:HoverMenuExtender>
                                                <asp:Panel ID="PopupMenuSyo" runat="server" CssClass="dropdown-menu fontcss " aria-labelledby="dropdownMenuButton" Style="display: none; min-width: 1rem; width: 122px !important; z-index:10000;margin-top:0px;padding:0px !important;border-radius:1px;">
                                                    <asp:LinkButton ID="LK_Syou" class="fontcss dropdown-item" runat="server" Text='文字を小さくする' OnClick="LK_Syou_Click" style="margin-right:10px;font-size:12px !important;padding:4px 12px !important;"></asp:LinkButton>
                                                     <asp:LinkButton ID="LK_Kyo" class="fontcss dropdown-item" runat="server" Text='文字を強調する' OnClick="LK_Kyo_Click" style="margin-right:10px;font-size:12px !important;padding:4px 12px !important;"></asp:LinkButton>
                                                </asp:Panel>
                                                <asp:Panel ID="PopupMenuBtnSyo" runat="server" CssClass="modalPopup" Style="width: 20px;">
                                                    <button class="btn dropdown-toggle" type="button" id="dropdownMenuButton" data-toggle="dropdown" 
                                                      aria-haspopup="true" aria-expanded="false" style="border:1px solid gainsboro;width:20px; height:20px;padding:0px 3px 0px 1px;margin:0;margin-top:-3px;">
                                                </asp:Panel>
                                                     </div>
                                            </ItemTemplate>
                                            <HeaderTemplate>
                                                <div class="grip" style="display:flex;align-content:center;align-items:center;align-items: center;justify-content: center;z-index:2;">
                                                     <asp:Label ID="LB_drop" runat="server" Text="dropdown" style="display:none;"></asp:Label>
                                                    </div>
                                            </HeaderTemplate>
                                            <ItemStyle Width="25px" CssClass="JC09DropDown JC10DropCol" />
                                            <HeaderStyle BorderWidth="2px" CssClass="JC09DropDown JC10DropCol" />
                                        </asp:TemplateField> 
                                        <asp:TemplateField ItemStyle-CssClass="JC10MitumoriCheckboxCol AlignCenter" HeaderStyle-CssClass="JC10MitumoriGridCheckboxHeaderCol JC10MitumoriGridHeaderStyle" Visible="false">
                                            <ItemTemplate>
                                                <div class="grip" style="text-align: center;padding-left:2px;overflow: hidden;  display: -webkit-box;  -webkit-line-clamp: 1; -webkit-box-orient: vertical;word-break: break-all;">
                                                    <asp:Label runat="server" ID="lblKubun" Text='<%#Eval("sKUBUN","{0}")%>' ToolTip='<%#Eval("sKUBUN","{0}")%>' style="cursor:default;user-select:none;"></asp:Label>
                                                    <asp:Label runat="server" ID="lblKubun1" Text='' ToolTip='' style="cursor:default;user-select:none;" Visible="false"></asp:Label>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderTemplate>
                                                <div class="grip" style="text-align: center;padding-left:2px;overflow: hidden;  display: -webkit-box;  -webkit-line-clamp: 1; -webkit-box-orient: vertical;word-break: break-all;">
                                                <asp:Label ID="LB_Kubun" runat="server" Text="Kubun"></asp:Label>
                                                    </div>
                                            </HeaderTemplate>
                                            <HeaderStyle CssClass="JC10MitumoriGridCheckboxHeaderCol JC10MitumoriGridHeaderStyle" BorderWidth="2px"/>
                                            <ItemStyle CssClass="JC10MitumoriCheckboxCol AlignCenter" />
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-CssClass="AlignLeft" HeaderStyle-CssClass="JC10MitumoriGridHeaderStyle">
                                            <ItemTemplate>
                                                <div class="grip" style="min-width:95px;text-align: left;padding-right: 4px;padding-left:1px;overflow: hidden;  display: -webkit-box;  -webkit-line-clamp: 1; -webkit-box-orient: vertical;word-break: break-all;">
                                                <asp:UpdatePanel ID="updtxtcSYOUHIN" runat="server" UpdateMode="Conditional">
                                                    <Triggers>
                                                         <asp:AsyncPostBackTrigger ControlID="txtcSYOHIN" EventName="TextChanged"/>
                                                     </Triggers>
                                                    <ContentTemplate>
                                                <asp:TextBox ID="txtcSYOHIN" runat="server" Text=' <%# Bind("cSYOHIN","{0}") %>' Width="100%" Height="25px" MaxLength="10" CssClass="form-control TextboxStyle JC10GridTextBox" autocomplete="off" AutoPostBack="true" onkeypress="return isNumberKey()" onkeydown="gridtextboxKeydown()" OnTextChanged="txtcSYOHIN_TextChanged"></asp:TextBox>
                                                         <asp:FilteredTextBoxExtender ID="TBF_cSYOHIN" runat="server" FilterType="Numbers" TargetControlID="txtcSYOHIN" />
                                                          
                                                        </ContentTemplate>
                                                </asp:UpdatePanel>
                                                    </div>
                                            </ItemTemplate>
                                            <HeaderTemplate>
                                                <div class="grip" style="min-width:95px;text-align: left;padding-right: 4px;padding-left:1px;overflow: hidden;  display: -webkit-box;  -webkit-line-clamp: 1; -webkit-box-orient: vertical;word-break: break-all;">
                                                <asp:Label ID="lblcSyohin" runat="server" Text="商品コード" CssClass="d-inline-block" style="padding-left:4px;"></asp:Label>
                                                    </div>
                                            </HeaderTemplate>
                                            <HeaderStyle CssClass="JC10MitumoriGridHeaderStyle" BorderWidth="2px"/>
                                            <ItemStyle CssClass="AlignLeft" />
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-CssClass="AlignCenter" HeaderStyle-CssClass="JC10MitumoriGridHeaderStyle JC10ButtonCol">
                                            <ItemTemplate>
                                                <div class="grip" style="padding-top: 0px;vertical-align: middle; text-align: center;">
                                                    <asp:UpdatePanel ID="updbtnSyohinSelectn" runat="server" UpdateMode="Conditional">
                                                    <Triggers>
                                                         <asp:AsyncPostBackTrigger ControlID="btnSyohinSelect" EventName="Click"/>
                                                     </Triggers>
                                                     <ContentTemplate>
                                                    <asp:Button ID="btnSyohinSelect" runat="server" Text="商" CssClass="JC09GridGrayBtn" onmousedown="getTantouBoardScrollPosition();" Width="30px" Height="28px" TabIndex="-1" OnClick="btnSyohinSelect_Click" OnClientClick="PopupOpenButton()"/>
                                                         </ContentTemplate>
                                                </asp:UpdatePanel>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderTemplate>
                                                <div class="grip" style="padding-top: 0px;vertical-align: middle; text-align: center;">
                                                <asp:Label ID="LB_Syouhin" runat="server" Text="Syouhin" style="display:none;"></asp:Label>
                                                    </div>
                                            </HeaderTemplate>
                                            <HeaderStyle CssClass="JC10MitumoriGridHeaderStyle JC10ButtonCol" BorderWidth="2px"/>
                                            <ItemStyle CssClass="AlignCenter JC10ButtonCol" />
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-CssClass="AlignLeft" HeaderStyle-CssClass="JC10MitumoriGridHeaderStyle">
                                            <ItemTemplate>
                                                  <div class="grip" style="min-width:50px;text-align: left;padding-right: 2px;padding-left:1px;overflow: hidden;  display: -webkit-box;  -webkit-line-clamp: 1; -webkit-box-orient: vertical;word-break: break-all;">
                                                <asp:UpdatePanel ID="updtxtsSYOHIN" runat="server" UpdateMode="Conditional">
                                                    <Triggers>
                                                         <asp:AsyncPostBackTrigger ControlID="txtsSYOHIN" EventName="TextChanged"/>
                                                     </Triggers>
                                                    <ContentTemplate>
                                                <asp:TextBox ID="txtsSYOHIN" runat="server"  Text=' <%# Bind("sSYOHIN","{0}") %>' Width="100%" Height="25px" MaxLength="10000" CssClass="form-control TextboxStyle JC10GridTextBox txtsSyohin" autocomplete="off" AutoPostBack="true" OnTextChanged="txtsSYOHIN_TextChanged"></asp:TextBox>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                                      </div>
                                            </ItemTemplate>
                                            <HeaderTemplate>
                                                <div class="grip" style="min-width:50px;text-align: left;padding-right: 2px;padding-left:1px;overflow: hidden;  display: -webkit-box;  -webkit-line-clamp: 1; -webkit-box-orient: vertical;word-break: break-all;">
                                                <asp:Label ID="lblsSyohin" runat="server" Text="商品名"  style="padding-left:4px;"></asp:Label>
                                                    </div>
                                            </HeaderTemplate>
                                            <HeaderStyle CssClass="JC10MitumoriGridHeaderStyle" BorderWidth="2px"/>
                                            <ItemStyle CssClass="AlignLeft" />
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-CssClass="AlignRight" HeaderStyle-CssClass="JC10MitumoriGridHeaderStyle">
                                            <ItemTemplate>
                                                <div class="grip" style="min-width:40px;text-align: left;padding-right: 4px;overflow: hidden;  display: -webkit-box;  -webkit-line-clamp: 1; -webkit-box-orient: vertical;word-break: break-all;">
                                                <asp:UpdatePanel ID="updtxtnSURYO" runat="server" UpdateMode="Conditional">
                                                    <Triggers>
                                                         <asp:AsyncPostBackTrigger ControlID="txtnSURYO" EventName="TextChanged"/>
                                                     </Triggers>
                                                    <ContentTemplate>
                                                <asp:TextBox ID="txtnSURYO" runat="server" Text=' <%# Bind("nSURYO","{0}") %>' Width="100%" Height="25px" MaxLength="10" CssClass="form-control TextboxStyle JC10GridTextBox" autocomplete="off" AutoPostBack="true" style="text-align:right;" onkeypress="return isNumberKey()" oninput="validateSuryo(this);" OnTextChanged="txtnSURYO_TextChanged"></asp:TextBox>
                                                     </ContentTemplate>
                                                </asp:UpdatePanel>
                                                    </div>
                                            </ItemTemplate>
                                            <HeaderTemplate>
                                                <div class="grip" style="min-width:40px;text-align: right;padding-right: 4px;overflow: hidden;  display: -webkit-box;  -webkit-line-clamp: 1; -webkit-box-orient: vertical;word-break: break-all;">
                                                <asp:Label ID="lblSyuryo" runat="server" Text="数量" CssClass="d-inline-block"></asp:Label>
                                                    </div>
                                            </HeaderTemplate>
                                            <HeaderStyle CssClass="JC10MitumoriGridHeaderStyle" BorderWidth="2px"/>
                                            <ItemStyle/>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-CssClass="AlignLeft" HeaderStyle-CssClass="JC10MitumoriGridHeaderStyle">
                                            <ItemTemplate>
                                                <%--<asp:DropDownList ID="DDL_cTANI" runat="server" Width="52px" AutoPostBack="True" Height="25px" CssClass="form-control JC10GridTextBox ui-select" style="font-size:13px;padding-top:0px;padding-right:2px;padding-left:2px;border-radius:2px !important;display:none;"  OnSelectedIndexChanged="DDL_cTANI_SelectedIndexChanged">
                                                   </asp:DropDownList>--%>
                                                 <div class="grip" style="text-align: left;overflow: hidden;  display: -webkit-box;  -webkit-line-clamp: 1; -webkit-box-orient: vertical;word-break: break-all;">
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
                                                     </div>
                                            </ItemTemplate>
                                            <HeaderTemplate>
                                                <div class="grip" style="text-align: left;overflow: hidden;  display: -webkit-box;  -webkit-line-clamp: 1; -webkit-box-orient: vertical;word-break: break-all;">
                                                <asp:Label ID="lblTani" runat="server" Text="単位" CssClass="d-inline-block" style="padding-left:4px;"></asp:Label>
                                                    </div>
                                            </HeaderTemplate>
                                            <HeaderStyle CssClass="JC10MitumoriGridHeaderStyle" BorderWidth="2px"/>
                                            <ItemStyle CssClass="AlignCenter" />
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-CssClass="AlignRight" HeaderStyle-CssClass="JC10MitumoriGridHeaderStyle">
                                            <ItemTemplate>
                                                <div class="grip" style="min-width:60px;text-align: left;padding-right: 4px;overflow: hidden;  display: -webkit-box;  -webkit-line-clamp: 1; -webkit-box-orient: vertical;word-break: break-all;">
                                                <asp:UpdatePanel ID="updtxtnTANKA" runat="server" UpdateMode="Conditional">
                                                    <Triggers>
                                                         <asp:AsyncPostBackTrigger ControlID="txtnTANKA" EventName="TextChanged"/>
                                                     </Triggers>
                                                    <ContentTemplate>
                                                <asp:TextBox ID="txtnTANKA" runat="server" Text=' <%# Bind("nTANKA","{0:#,##0.##}") %>' Width="100%" Height="25px" MaxLength="10" CssClass="form-control TextboxStyle JC10GridTextBox" autocomplete="off" AutoPostBack="true" style="text-align:right;" onkeypress="return isNumberKey()" oninput="validateSuryo(this);" OnTextChanged="txtnTANKA_TextChanged"></asp:TextBox>
                                                        </ContentTemplate>
                                                </asp:UpdatePanel>
                                                    </div>
                                            </ItemTemplate>
                                            <HeaderTemplate>
                                                <div class="grip" style="min-width:60px;text-align: right;padding-right: 4px;overflow: hidden;  display: -webkit-box;  -webkit-line-clamp: 1; -webkit-box-orient: vertical;word-break: break-all;">
                                                <asp:Label ID="lblnTankaHeader" runat="server" Text="標準単価" CssClass="d-inline-block"></asp:Label>
                                                    </div>
                                            </HeaderTemplate>
                                            <HeaderStyle CssClass="JC10MitumoriGridHeaderStyle" BorderWidth="2px"/>
                                            <ItemStyle CssClass="AlignRight" />
                                        </asp:TemplateField>
                                        <asp:TemplateField  HeaderStyle-CssClass="JC10MitumoriGridHeaderStyle">
                                            <ItemTemplate>
                                                <div class="grip" style="min-width:40px;text-align: right;padding-right: 4px;overflow: hidden;  display: -webkit-box;  -webkit-line-clamp: 1; -webkit-box-orient: vertical;word-break: break-all;">
                                                <asp:Label runat="server" ID="lblTanka" Text='<%#Eval("nSIKIRITANKA","{0:#,##0.##}")%>' ToolTip='<%#Eval("nSIKIRITANKA","{0:#,##0.##}")%>' style="cursor:default;user-select:none;" TabIndex="-1"></asp:Label>
                                                    </div>
                                            </ItemTemplate>
                                            <HeaderTemplate>
                                                <div class="grip" style="min-width:40px;text-align: right;padding-right: 4px;overflow: hidden;  display: -webkit-box;  -webkit-line-clamp: 1; -webkit-box-orient: vertical;word-break: break-all;">
                                                <asp:Label ID="lblTankaHeader" runat="server" Text="単価" CssClass="d-inline-block"></asp:Label>
                                                    </div>
                                            </HeaderTemplate>
                                            <HeaderStyle CssClass="JC10MitumoriGridHeaderStyle" BorderWidth="2px"/>
                                            <ItemStyle/>
                                        </asp:TemplateField>
                                        <asp:TemplateField  HeaderStyle-CssClass="JC10MitumoriGridHeaderStyle">
                                            <ItemTemplate>
                                                <div class="grip" style="min-width:60px;text-align: right;padding-right: 4px;overflow: hidden;  display: -webkit-box;  -webkit-line-clamp: 1; -webkit-box-orient: vertical;word-break: break-all;">
                                                <asp:Label runat="server" ID="lblTankaGokei" Text='<%#Eval("nTANKAGOUKEI","{0:#,##0.##}")%>' ToolTip='<%#Eval("nTANKAGOUKEI","{0:#,##0.##}")%>' style="cursor:default;user-select:none;" TabIndex="-1"></asp:Label>
                                                    </div>
                                            </ItemTemplate>
                                            <HeaderTemplate>
                                                <div class="grip" style="min-width:60px;text-align: right;padding-right: 4px;overflow: hidden;  display: -webkit-box;  -webkit-line-clamp: 1; -webkit-box-orient: vertical;word-break: break-all;">
                                                <asp:Label ID="lblTankaGokeiHeader" runat="server" Text="合計金額" CssClass="d-inline-block"></asp:Label>
                                                    </div>
                                            </HeaderTemplate>
                                            <HeaderStyle CssClass="JC10MitumoriGridHeaderStyle" BorderWidth="2px"/>
                                            <ItemStyle/>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-CssClass="AlignRight" HeaderStyle-CssClass="JC10MitumoriGridHeaderStyle">
                                            <ItemTemplate>
                                                <div class="grip" style="min-width:60px;text-align: left;padding-right: 2px;overflow: hidden;  display: -webkit-box;  -webkit-line-clamp: 1; -webkit-box-orient: vertical;word-break: break-all;">
                                                <asp:UpdatePanel ID="updtxtnGENKATANKA" runat="server" UpdateMode="Conditional">
                                                    <Triggers>
                                                         <asp:AsyncPostBackTrigger ControlID="txtnGENKATANKA" EventName="TextChanged"/>
                                                     </Triggers>
                                                    <ContentTemplate>
                                                <asp:TextBox ID="txtnGENKATANKA" runat="server" Text=' <%# Bind("nGENKATANKA","{0:#,##0.##}") %>' Width="100%" Height="25px" MaxLength="10" CssClass="form-control TextboxStyle JC10GridTextBox" autocomplete="off" AutoPostBack="true" style="text-align:right;" onkeypress="return isNumberKey()" oninput="validateSuryo(this);" OnTextChanged="txtnGENKATANKA_TextChanged"></asp:TextBox>
                                                        </ContentTemplate>
                                                </asp:UpdatePanel>
                                                    </div>
                                            </ItemTemplate>
                                            <HeaderTemplate>
                                                <div class="grip" style="min-width:60px;text-align: right;padding-right: 2px;overflow: hidden;  display: -webkit-box;  -webkit-line-clamp: 1; -webkit-box-orient: vertical;word-break: break-all;">
                                                <asp:Label ID="lblnGENKATANKA" runat="server" Text="原価単価" CssClass="d-inline-block"></asp:Label>
                                                    </div>
                                            </HeaderTemplate>
                                            <HeaderStyle CssClass="JC10MitumoriGridHeaderStyle" BorderWidth="2px"/>
                                            <ItemStyle CssClass="AlignRight" />
                                        </asp:TemplateField>
                                         <asp:TemplateField ItemStyle-CssClass="AlignRight" HeaderStyle-CssClass="JC10MitumoriGridHeaderStyle">
                                            <ItemTemplate>
                                                <div class="grip" style="min-width:40px;text-align: left;padding-right: 2px;overflow: hidden;  display: -webkit-box;  -webkit-line-clamp: 1; -webkit-box-orient: vertical;word-break: break-all;">
                                                <asp:UpdatePanel ID="updtxtnRITU" runat="server" UpdateMode="Conditional">
                                                    <Triggers>
                                                         <asp:AsyncPostBackTrigger ControlID="txtnRITU" EventName="TextChanged"/>
                                                     </Triggers>
                                                    <ContentTemplate>
                                                <asp:TextBox ID="txtnRITU" runat="server" Text=' <%# Bind("nRITU","{0:#,##0.##}") %>' Width="100%" Height="25px" MaxLength="10" CssClass="form-control TextboxStyle JC10GridTextBox" autocomplete="off" AutoPostBack="true" style="text-align:right;" onkeypress="return isNumberKey()" oninput="validateRitu(this);" OnTextChanged="txtnRITU_TextChanged"></asp:TextBox>
                                                        </ContentTemplate>
                                                </asp:UpdatePanel>
                                                    </div>
                                            </ItemTemplate>
                                            <HeaderTemplate>
                                                <div class="grip" style="min-width:40px;text-align: right;padding-right:2px;overflow: hidden;  display: -webkit-box;  -webkit-line-clamp: 1; -webkit-box-orient: vertical;word-break: break-all;">
                                                <asp:Label ID="lblnRITU" runat="server" Text="掛率" CssClass="d-inline-block"></asp:Label>
                                                    </div>
                                            </HeaderTemplate>
                                            <HeaderStyle CssClass="JC10MitumoriGridHeaderStyle" BorderWidth="2px"/>
                                            <ItemStyle CssClass="AlignRight" />
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-CssClass="AlignRight" HeaderStyle-CssClass="JC10MitumoriGridHeaderStyle">
                                            <ItemTemplate>
                                                <div class="grip" style="min-width:60px;text-align: right;padding-right: 4px;overflow: hidden;  display: -webkit-box;  -webkit-line-clamp: 1; -webkit-box-orient: vertical;word-break: break-all;">
                                                <asp:Label runat="server" ID="lblGenkaGokei" Text='<%#Eval("nGENKAGOUKEI","{0:#,##0.##}")%>' ToolTip='<%#Eval("nGENKAGOUKEI","{0:#,##0.##}")%>' TabIndex="-1" style="cursor:default;user-select:none;"></asp:Label>
                                                    </div>
                                            </ItemTemplate>
                                            <HeaderTemplate>
                                                <div class="grip" style="min-width:60px;text-align: right;padding-right: 4px;overflow: hidden;  display: -webkit-box;  -webkit-line-clamp: 1; -webkit-box-orient: vertical;word-break: break-all;">
                                                <asp:Label ID="lblGenkaGokeiHeader" runat="server" Text="原価合計" CssClass="d-inline-block"></asp:Label>
                                                    </div>
                                            </HeaderTemplate>
                                            <HeaderStyle CssClass="JC10MitumoriGridHeaderStyle" BorderWidth="2px"/>
                                            <ItemStyle CssClass="AlignRight" />
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-CssClass="AlignRight" HeaderStyle-CssClass="JC10MitumoriGridHeaderStyle">
                                            <ItemTemplate>
                                                <div class="grip" style="min-width:40px;text-align: right;padding-right: 4px;overflow: hidden;  display: -webkit-box;  -webkit-line-clamp: 1; -webkit-box-orient: vertical;word-break: break-all;">
                                                <asp:Label runat="server" ID="lblnARARI" Text='<%#Eval("nARARI")%>' ToolTip='<%#Eval("nARARI","{0:#,##0.##}")%>' TabIndex="-1" style="cursor:default;user-select:none;"></asp:Label>
                                                    </div>
                                            </ItemTemplate>
                                            <HeaderTemplate>
                                                 <div class="grip" style="min-width:40px;text-align: right;padding-right: 4px;overflow: hidden;  display: -webkit-box;  -webkit-line-clamp: 1; -webkit-box-orient: vertical;word-break: break-all;">
                                                <asp:Label ID="lblnARARIHeader" runat="server" Text="粗利" CssClass="d-inline-block"></asp:Label>
                                                     </div>
                                            </HeaderTemplate>
                                            <HeaderStyle CssClass="JC10MitumoriGridHeaderStyle" BorderWidth="2px"/>
                                            <ItemStyle CssClass="AlignRight" />
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-CssClass="AlignRight" HeaderStyle-CssClass="JC10MitumoriGridHeaderStyle">
                                            <ItemTemplate>
                                                <div class="grip" style="min-width:50px;text-align: right;padding-right: 4px;overflow: hidden;  display: -webkit-box;  -webkit-line-clamp: 1; -webkit-box-orient: vertical;word-break: break-all;">
                                                <asp:Label runat="server" ID="lblnARARISu" Text='<%#Eval("nARARISu")%>' ToolTip='<%#Eval("nARARISu")%>' TabIndex="-1" style="cursor:default;user-select:none;"></asp:Label>
                                                    </div>
                                            </ItemTemplate>
                                            <HeaderTemplate>
                                                <div class="grip" style="min-width:50px;text-align: right;padding-right: 4px;overflow: hidden;  display: -webkit-box;  -webkit-line-clamp: 1; -webkit-box-orient: vertical;word-break: break-all;">
                                                <asp:Label ID="lblnnARARISuHeader" runat="server" Text="粗利率" CssClass="d-inline-block"></asp:Label>
                                                    </div>
                                            </HeaderTemplate>
                                            <HeaderStyle CssClass="JC10MitumoriGridHeaderStyle" BorderWidth="2px"/>
                                            <ItemStyle CssClass="AlignRight" />
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-CssClass="AlignCenter drag" HeaderStyle-CssClass="JC10MitumoriGridHeaderStyle JC10ButtonCol" Visible="false"><%--20220504Myatnoe,Chaw Added--%>
                                            <ItemTemplate>
                                                <div class="grip" style="padding-top: 0px;vertical-align: middle; text-align: center;" tabindex="-1">
                                                    <span class="dragBtn" tabindex="-1">三</span>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderTemplate>
                                                <div class="grip" style="padding-top: 0px;vertical-align: middle; text-align: center;" tabindex="-1">
                                                <asp:Label ID="LB_drag" runat="server" Text="drag" style="display:none;"></asp:Label>
                                                    </div>
                                            </HeaderTemplate>
                                            <HeaderStyle CssClass="JC10MitumoriGridHeaderStyle JC10ButtonCol" BorderWidth="2px"/>
                                            <ItemStyle CssClass="AlignCenter JC10ButtonCol" />
                                        </asp:TemplateField>
                                       <%-- <asp:TemplateField ItemStyle-CssClass="JC10MitumoriGridPludBtnCol AlignCenter" HeaderStyle-CssClass="JC10MitumoriGridPludBtnHeaderCol JC10MitumoriGridHeaderStyle">
                                            <ItemTemplate>
                                                <div style="display:none;">
                                                    <asp:Button ID="btnSyohinDelete" runat="server" Text="削" CssClass="JC09GridGrayBtn" onmousedown="getTantouBoardScrollPosition();" Width="30px" Height="28px" OnClick="btnSyohinDelete_Click" />
                                                </div>
                                            </ItemTemplate>
                                            <HeaderTemplate>
                                                <asp:Label ID="Label1" runat="server" Text="" CssClass="d-inline-block"></asp:Label>
                                            </HeaderTemplate>
                                            <HeaderStyle CssClass="JC10MitumoriGridPludBtnHeaderCol JC10MitumoriGridHeaderStyle" />
                                            <ItemStyle CssClass="JC10MitumoriGridPludBtnCol AlignCenter" />
                                        </asp:TemplateField>--%>
                                        <asp:TemplateField HeaderStyle-Width="30px" Visible="false">
                                            <ItemTemplate>   
                                                 <div class="grip" style="display:flex;align-content:center;align-items:center;align-items: center;justify-content: center;z-index:2;">
                                                         <asp:HoverMenuExtender ID="HoverMenuExtender2" runat="server" PopupControlID="PopupMenu"
                                                    TargetControlID="PopupMenuBtn" PopupPosition="left">
                                                </asp:HoverMenuExtender>
                                                <asp:Panel ID="PopupMenu" runat="server" CssClass="dropdown-menu fontcss " aria-labelledby="dropdownMenuButton" Style="display: none; min-width: 1rem; width: 5rem; z-index:10000;">
                                                    <asp:LinkButton ID="lnkbtnSyohinDelete" class="dropdown-item" runat="server" Text='削除' style="margin-right:10px;font-size:13px;" OnClick="btnSyohinDelete_Click"></asp:LinkButton>
                                                </asp:Panel>
                                                <asp:Panel ID="PopupMenuBtn" runat="server" CssClass="modalPopup" Style="width: 20px;">
                                                    <button class="btn dropdown-toggle" type="button" id="dropdownMenuButton" data-toggle="dropdown" 
                                                      aria-haspopup="true" aria-expanded="false" style="border:1px solid gainsboro;width:20px; height:20px;padding:0px 3px 0px 1px;margin:0;margin-top:-3px;">
                                                </asp:Panel>
                                                     </div>
                                            </ItemTemplate>
                                            <HeaderTemplate>
                                                <div class="grip" style="display:flex;align-content:center;align-items:center;align-items: center;justify-content: center;z-index:2;">
                                                     <asp:Label ID="LB_drop" runat="server" Text="dropdown" style="display:none;"></asp:Label>
                                                    </div>
                                            </HeaderTemplate>
                                            <ItemStyle Width="25px" CssClass="JC09DropDown JC10DropCol" />
                                            <HeaderStyle BorderWidth="2px" CssClass="JC09DropDown JC10DropCol" />
                                        </asp:TemplateField> 
                                    </Columns>
                                </asp:GridView>

                                <asp:GridView runat="server" ID="GV_MitumoriSyohin" BorderColor="Transparent" AutoGenerateColumns="false" EmptyDataRowStyle-CssClass="JC10NoDataMessageStyle" CssClass="gvMitumoriSyohin GridViewStyleSyohin"
                                    ShowHeader="true" ShowHeaderWhenEmpty="true" RowStyle-CssClass="GridRow" CellPadding="0" onrowdatabound="GV_MitumoriSyohin_RowDataBound"
                                    OnSelectedIndexChanged="GV_MitumoriSyohin_SelectedIndexChanged" >
                                    <EmptyDataRowStyle CssClass="JC10NoDataMessageStyle" />
                                    <HeaderStyle Height="37px" BackColor="#F2F2F2" />
                                    <RowStyle CssClass="GridRow" Height="37px" />
                                    <SelectedRowStyle BackColor="#EBEBF5" />
                                    <Columns>
                                    </Columns>
                                </asp:GridView>

                                

                                
</span>
                                 

                                <asp:GridView runat="server" ID="GV_Syosai" BorderColor="Transparent" AutoGenerateColumns="false" EmptyDataRowStyle-CssClass="JC10NoDataMessageStyle"
                                    ShowHeader="true" ShowHeaderWhenEmpty="true" RowStyle-CssClass="GridRow" CellPadding="0" Visible="false">
                                    <EmptyDataRowStyle CssClass="JC10NoDataMessageStyle" />
                                    <HeaderStyle Height="37px" BackColor="#F2F2F2" />
                                    <RowStyle CssClass="GridRow" Height="37px" />
                                    <SelectedRowStyle BackColor="#EBEBF5" />
                                    <Columns>
                                        <asp:TemplateField ItemStyle-CssClass="JC10MitumoriCheckboxCol AlignCenter" HeaderStyle-CssClass="JC10MitumoriGridCheckboxHeaderCol JC10MitumoriGridHeaderStyle">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkSelectSyouhin" runat="server" AutoPostBack="true" CssClass="M01AnkenGridCheck" />
                                                <asp:Label ID="lblhdnStatus" runat="server" Text='<%#Eval("status") %>' CssClass="DisplayNone"></asp:Label>
                                                <asp:Label ID="lblRowNo" runat="server" Text='<%#Eval("rowNo") %>' CssClass="DisplayNone"></asp:Label>
                                                <asp:Label ID="lblfjitais" runat="server" Text='<%#Eval("fJITAIS") %>' CssClass="DisplayNone"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderTemplate>
                                            </HeaderTemplate>
                                            <HeaderStyle CssClass="JC10MitumoriGridCheckboxHeaderCol JC10MitumoriGridHeaderStyle" />
                                            <ItemStyle CssClass="JC10MitumoriCheckboxCol AlignCenter" />
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-CssClass="JC10MitumoriGridPludBtnCol AlignCenter" HeaderStyle-CssClass="JC10MitumoriGridPludBtnHeaderCol JC10MitumoriGridHeaderStyle">
                                            <ItemTemplate>

                                                <asp:Button ID="btnSyouhinAdd" runat="server" Text="＋" CssClass="JC10GrayButton" onmousedown="getTantouBoardScrollPosition();" Width="30px" Height="28px" OnClick="btnSyouhinAdd_Click" />

                                            </ItemTemplate>
                                            <HeaderTemplate>
                                                <asp:Label ID="Label1" runat="server" Text="" CssClass="d-inline-block"></asp:Label>
                                            </HeaderTemplate>
                                            <HeaderStyle CssClass="JC10MitumoriGridPludBtnHeaderCol JC10MitumoriGridHeaderStyle" />
                                            <ItemStyle CssClass="JC10MitumoriGridPludBtnCol AlignCenter" />
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-CssClass="JC11MitumoriGridSyohinCodeCol AlignCenter" HeaderStyle-CssClass="JC11MitumoriGridSyohinCodeHeaderCol JC10MitumoriGridHeaderStyle">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtcSYOHIN" runat="server" Text=' <%# Bind("cSYOHIN","{0}") %>' Width="91px" Height="25px" MaxLength="10" CssClass="form-control TextboxStyle JC10GridTextBox" autocomplete="off" AutoPostBack="true" ></asp:TextBox>
                                            </ItemTemplate>
                                            <HeaderTemplate>
                                                <asp:Label ID="lblcSyohin" runat="server" Text="商品コード" CssClass="d-inline-block"></asp:Label>
                                            </HeaderTemplate>
                                            <HeaderStyle CssClass="JC11MitumoriGridSyohinCodeHeaderCol JC10MitumoriGridHeaderStyle" />
                                            <ItemStyle CssClass="JC11MitumoriGridSyohinCodeCol AlignCenter" />
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-CssClass="JC10MitumoriGridPludBtnCol AlignCenter" HeaderStyle-CssClass="JC10MitumoriGridPludBtnHeaderCol JC10MitumoriGridHeaderStyle">
                                            <ItemTemplate>
                                                <div>
                                                    <asp:Button ID="btnSyohinSelect" runat="server" Text="商" CssClass="JC10GrayButton" onmousedown="getTantouBoardScrollPosition();" Width="35px" Height="28px" />
                                                </div>
                                            </ItemTemplate>
                                            <HeaderTemplate>
                                                <asp:Label ID="Label1" runat="server" Text="" CssClass="d-inline-block"></asp:Label>
                                            </HeaderTemplate>
                                            <HeaderStyle CssClass="JC10MitumoriGridPludBtnHeaderCol JC10MitumoriGridHeaderStyle" />
                                            <ItemStyle CssClass="JC10MitumoriGridPludBtnCol AlignCenter" />
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-CssClass="JC11MitumoriGridSyohinNameCol AlignCenter" HeaderStyle-CssClass="JC11MitumoriGridSyohinNameHeaderCol JC10MitumoriGridHeaderStyle">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtsSYOHIN" runat="server" Text=' <%# Bind("sSYOHIN","{0}") %>' Width="256px" Height="25px" MaxLength="1000" CssClass="form-control TextboxStyle JC10GridTextBox" autocomplete="off" AutoPostBack="true"></asp:TextBox>
                                            </ItemTemplate>
                                            <HeaderTemplate>
                                                <asp:Label ID="lblsSyohin" runat="server" Text="商品名" CssClass="d-inline-block"></asp:Label>
                                            </HeaderTemplate>
                                            <HeaderStyle CssClass="JC11MitumoriGridSyohinNameHeaderCol JC10MitumoriGridHeaderStyle" />
                                            <ItemStyle CssClass="JC11MitumoriGridSyohinNameCol AlignCenter" />
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-CssClass="JC10MitumoriGridSyuryoCol AlignCenter" HeaderStyle-CssClass="JC10MitumoriGridSyuryoHeaderCol JC10MitumoriGridHeaderStyle">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtnSURYO" runat="server" Text=' <%# Bind("nSURYO","{0}") %>' Width="66px" Height="25px" MaxLength="10" CssClass="form-control TextboxStyle JC10GridTextBox" autocomplete="off" AutoPostBack="true" style="text-align:right;"></asp:TextBox>
                                            </ItemTemplate>
                                            <HeaderTemplate>
                                                <asp:Label ID="lblSyuryo" runat="server" Text="数量" CssClass="d-inline-block"></asp:Label>
                                            </HeaderTemplate>
                                            <HeaderStyle CssClass="JC10MitumoriGridSyuryoHeaderCol JC10MitumoriGridHeaderStyle" />
                                            <ItemStyle CssClass="JC10MitumoriGridSyuryoCol AlignCenter" />
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-CssClass="JC10MitumoriGridSyuryoCol AlignCenter" HeaderStyle-CssClass="JC10MitumoriGridSyuryoHeaderCol JC10MitumoriGridHeaderStyle">
                                            <ItemTemplate>
                                                <asp:Label ID="lblcTANI" runat="server" Text='<%# Eval("cTANI") %>'/>
                                                <asp:DropDownList ID="DDL_cTANI" runat="server" Width="66px" AutoPostBack="True" Height="26px" CssClass="DisplayNone">
                                                </asp:DropDownList>
                                            </ItemTemplate>
                                            <HeaderTemplate>
                                                <asp:Label ID="lblTani" runat="server" Text="単位" CssClass="d-inline-block"></asp:Label>
                                            </HeaderTemplate>
                                            <HeaderStyle CssClass="JC10MitumoriGridSyuryoHeaderCol JC10MitumoriGridHeaderStyle" />
                                            <ItemStyle CssClass="JC10MitumoriGridSyuryoCol AlignCenter" />
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-CssClass="JC10MitumoriGridKingakuCol AlignCenter" HeaderStyle-CssClass="JC10MitumoriGridKingakuHeaderCol JC10MitumoriGridHeaderStyle">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtnTANKA" runat="server" Text=' <%# Bind("nTANKA","{0}") %>' Width="96px" Height="25px" CssClass="form-control TextboxStyle JC10GridTextBox" autocomplete="off" AutoPostBack="true" style="text-align:right;"></asp:TextBox>
                                            </ItemTemplate>
                                            <HeaderTemplate>
                                                <asp:Label ID="lblnTanka" runat="server" Text="標準単価" CssClass="d-inline-block"></asp:Label>
                                            </HeaderTemplate>
                                            <HeaderStyle CssClass="JC10MitumoriGridKingakuHeaderCol JC10MitumoriGridHeaderStyle" />
                                            <ItemStyle CssClass="JC10MitumoriGridKingakuCol AlignCenter" />
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-CssClass="JC10MitumoriGridKingakuCol AlignRight" HeaderStyle-CssClass="JC10MitumoriGridKingakuHeaderCol JC10MitumoriGridHeaderStyle">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblTanka" Text='<%#Eval("nSIKIRITANKA")%>' ToolTip='<%#Eval("nSIKIRITANKA")%>' style="cursor:default;user-select:none;padding-right:4px;"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderTemplate>
                                                <asp:Label ID="lblTankaHeader" runat="server" Text="単価" CssClass="d-inline-block" style="width:91px;padding-right:4px;text-align:right;"></asp:Label>
                                            </HeaderTemplate>
                                            <HeaderStyle CssClass="JC10MitumoriGridKingakuHeaderCol JC10MitumoriGridHeaderStyle" />
                                            <ItemStyle CssClass="JC10MitumoriGridKingakuCol AlignRight" />
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-CssClass="JC10MitumoriGridKingakuCol AlignRight" HeaderStyle-CssClass="JC10MitumoriGridKingakuHeaderCol JC10MitumoriGridHeaderStyle">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblTankaGokei" Text='<%#Eval("nTANKAGOUKEI")%>' ToolTip='<%#Eval("nTANKAGOUKEI")%>' style="cursor:default;user-select:none;padding-right:4px;"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderTemplate>
                                                <asp:Label ID="lblTankaGokeiHeader" runat="server" Text="合計金額" CssClass="d-inline-block" style="width:91px;padding-right:4px;text-align:right;"></asp:Label>
                                            </HeaderTemplate>
                                            <HeaderStyle CssClass="JC10MitumoriGridKingakuHeaderCol JC10MitumoriGridHeaderStyle" />
                                            <ItemStyle CssClass="JC10MitumoriGridKingakuCol AlignRight" />
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-CssClass="JC10MitumoriGridKingakuCol AlignCenter" HeaderStyle-CssClass="JC10MitumoriGridKingakuHeaderCol JC10MitumoriGridHeaderStyle">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtnGENKATANKA" runat="server" Text=' <%# Bind("nGENKATANKA","{0}") %>' Width="96px" Height="25px" MaxLength="10" CssClass="form-control TextboxStyle JC10GridTextBox" autocomplete="off" AutoPostBack="true" style="text-align:right;"></asp:TextBox>
                                            </ItemTemplate>
                                            <HeaderTemplate>
                                                <asp:Label ID="lblnGENKATANKA" runat="server" Text="原価単価" CssClass="d-inline-block"></asp:Label>
                                            </HeaderTemplate>
                                            <HeaderStyle CssClass="JC10MitumoriGridKingakuHeaderCol JC10MitumoriGridHeaderStyle" />
                                            <ItemStyle CssClass="JC10MitumoriGridKingakuCol AlignCenter" />
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-CssClass="JC10MitumoriGridTaniCol AlignCenter" HeaderStyle-CssClass="JC10MitumoriGridTaniHeaderCol JC10MitumoriGridHeaderStyle">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtnRITU" runat="server" Text=' <%# Bind("nRITU","{0:#,##0.##}") %>' Width="52px" Height="25px" MaxLength="10" CssClass="form-control TextboxStyle JC10GridTextBox" autocomplete="off" AutoPostBack="true" style="text-align:right;" OnTextChanged="txtnRITU_TextChanged"></asp:TextBox>
                                            </ItemTemplate>
                                            <HeaderTemplate>
                                                <asp:Label ID="lblnRITU" runat="server" Text="掛率" CssClass="d-inline-block"></asp:Label>
                                            </HeaderTemplate>
                                            <HeaderStyle CssClass="JC10MitumoriGridTaniHeaderCol JC10MitumoriGridHeaderStyle" />
                                            <ItemStyle CssClass="JC10MitumoriGridTaniCol AlignCenter" />
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-CssClass="JC10MitumoriGridKingakuCol AlignRight" HeaderStyle-CssClass="JC10MitumoriGridKingakuHeaderCol JC10MitumoriGridHeaderStyle">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblGenkaGokei" Text='<%#Eval("nGENKAGOUKEI")%>' ToolTip='<%#Eval("nGENKAGOUKEI")%>' style="cursor:default;user-select:none;padding-right:4px;"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderTemplate>
                                                <asp:Label ID="lblGenkaGokeiHeader" runat="server" Text="原価合計" CssClass="d-inline-block" style="width:91px;padding-right:4px;text-align:right;"></asp:Label>
                                            </HeaderTemplate>
                                            <HeaderStyle CssClass="JC10MitumoriGridKingakuHeaderCol JC10MitumoriGridHeaderStyle" />
                                            <ItemStyle CssClass="JC10MitumoriGridKingakuCol AlignRight" />
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-CssClass="JC10MitumoriGridKingakuCol AlignRight" HeaderStyle-CssClass="JC10MitumoriGridKingakuHeaderCol JC10MitumoriGridHeaderStyle">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblnARARI" Text='<%#Eval("nARARI")%>' ToolTip='<%#Eval("nARARI")%>' style="cursor:default;user-select:none;padding-right:4px;"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderTemplate>
                                                <asp:Label ID="lblnARARIHeader" runat="server" Text="粗利" CssClass="d-inline-block" style="width:91px;padding-right:4px;text-align:right;"></asp:Label>
                                            </HeaderTemplate>
                                            <HeaderStyle CssClass="JC10MitumoriGridKingakuHeaderCol JC10MitumoriGridHeaderStyle" />
                                            <ItemStyle CssClass="JC10MitumoriGridKingakuCol AlignRight" />
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-CssClass="JC10MitumoriGridKingakuCol AlignRight" HeaderStyle-CssClass="JC10MitumoriGridKingakuHeaderCol JC10MitumoriGridHeaderStyle">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblnARARISu" Text='<%#Eval("nARARISu")%>' ToolTip='<%#Eval("nARARISu")%>' style="cursor:default;user-select:none;padding-right:4px;"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderTemplate>
                                                <asp:Label ID="lblnnARARISuHeader" runat="server" Text="粗利率" CssClass="d-inline-block" style="width:91px;text-align:right;"></asp:Label>
                                            </HeaderTemplate>
                                            <HeaderStyle CssClass="JC10MitumoriGridKingakuHeaderCol JC10MitumoriGridHeaderStyle" />
                                            <ItemStyle CssClass="JC10MitumoriGridKingakuCol AlignRight" />
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-CssClass="JC11MitumoriGridPludBtnCol AlignCenter" HeaderStyle-CssClass="JC10MitumoriGridPludBtnHeaderCol JC10MitumoriGridHeaderStyle" Visible="false">
                                            <ItemTemplate>
                                               <div>
                                                    <span class="dragBtn">三</span>
                                                </div>
                                            </ItemTemplate>
                                            <HeaderTemplate>
                                                <asp:Label ID="Label1" runat="server" Text="" CssClass="d-inline-block"></asp:Label>
                                            </HeaderTemplate>
                                            <HeaderStyle CssClass="JC10MitumoriGridPludBtnHeaderCol JC10MitumoriGridHeaderStyle" />
                                            <ItemStyle CssClass="JC10MitumoriGridPludBtnCol AlignCenter" />
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-CssClass="JC10MitumoriGridPludBtnCol AlignCenter" HeaderStyle-CssClass="JC10MitumoriGridPludBtnHeaderCol JC10MitumoriGridHeaderStyle">
                                            <ItemTemplate>
                                                <div>
                                                    <asp:Button ID="btnSyohinCopy" runat="server" Text="コ" CssClass="JC10GrayButton" onmousedown="getTantouBoardScrollPosition();" Width="35px" Height="28px" OnClick="btnSyohinCopy_Click" />
                                                </div>
                                            </ItemTemplate>
                                            <HeaderTemplate>
                                                <asp:Label ID="Label1" runat="server" Text="" CssClass="d-inline-block"></asp:Label>
                                            </HeaderTemplate>
                                            <HeaderStyle CssClass="JC10MitumoriGridPludBtnHeaderCol JC10MitumoriGridHeaderStyle" />
                                            <ItemStyle CssClass="JC10MitumoriGridPludBtnCol AlignCenter" />
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-CssClass="JC10MitumoriGridPludBtnCol AlignCenter" HeaderStyle-CssClass="JC10MitumoriGridPludBtnHeaderCol JC10MitumoriGridHeaderStyle">
                                            <ItemTemplate>
                                                <div>
                                                    <asp:Button ID="btnSyohinDelete" runat="server" Text="削" CssClass="JC10GrayButton" onmousedown="getTantouBoardScrollPosition();" Width="35px" Height="28px" OnClick="btnSyohinDelete_Click" />
                                                </div>
                                            </ItemTemplate>
                                            <HeaderTemplate>
                                                <asp:Label ID="Label1" runat="server" Text="" CssClass="d-inline-block"></asp:Label>
                                            </HeaderTemplate>
                                            <HeaderStyle CssClass="JC10MitumoriGridPludBtnHeaderCol JC10MitumoriGridHeaderStyle" />
                                            <ItemStyle CssClass="JC10MitumoriGridPludBtnCol AlignCenter" />
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>

                                <asp:Button ID="BT_SyohinEmptyAdd" runat="server" CssClass="JC10GrayButton DisplayNone"  Text="＋　商品追加" Width="125px" OnClick="BT_SyohinEmptyAdd_Click" style="margin-top:5px;" />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
                    <br />
                    </div>
            <div class="JC10MitumoriTourokuDiv" id="divMitumoriInsatsu" runat="server" style="padding:40px 60px 20px 60px;">
                <asp:Button ID="btnInsatsuSave" runat="server" CssClass=" BlueBackgroundButton JC10SaveBtn" Text="保存"
                                            OnClientClick="javascript:disabledTextChange(this);"  OnClick="btnInsatsuSave_Click" />
                <br />
                    <asp:Label ID="lblHeader" runat="server" Text="印刷項目" Font-Bold="True" Font-Size="15px" style="margin-bottom:10px;display:inline-block;margin-top:15px;"></asp:Label>
                <br />
                <asp:CheckBox ID="CHK_KingakuNotDisplay" runat="server" Text="合計金額非表示" style="margin-left:50px;" CssClass="JC10Chkbox" AutoPostBack="True" OnCheckedChanged="CHK_KingakuNotDisplay_CheckedChanged"/>
                <br />
                 <div class="JC11Borderline"></div>
                <asp:Label ID="Label4" runat="server" Text="印刷詳細項目" Font-Bold="True" Font-Size="15px" style="margin-bottom:5px;display:inline-block;margin-top:10px;"></asp:Label>
                <table style="margin-left:50px;margin-bottom:3px;">
                    <tr style="height:60px;">
                        <td style="width:60px;">
                            <asp:Label ID="lblLogo" runat="server" Text="ロゴ"></asp:Label>
                        </td>
                        <td>
                             <asp:DropDownList ID="DDL_Logo" runat="server" Width="200px" AutoPostBack="True" Height="30px" CssClass="form-control JC10GridTextBox" style="font-size:13px;border-radius:2px;">
                                                </asp:DropDownList>
                        </td>
                    </tr>
                    <tr style="height:60px;">
                        <td colspan="2">
                            <table>
                                <tr>
                                    <td>
                                        <asp:Button ID="btnZeikomi" runat="server" Text="税込み" onmousedown="getTantouBoardScrollPosition();" Width="100px" Height="35px" CssClass="JC10ZeikomiBtnActive" OnClick="btnZeikomi_Click" />
                                    </td>
                                    <td>
                                        <asp:Button ID="btnZeinuKingaku1" runat="server" Text="税抜金額①" onmousedown="getTantouBoardScrollPosition();" Width="100px" Height="35px" CssClass="JC10ZeikomiBtn" OnClick="btnZeinuKingaku1_Click" />
                                    </td>
                                    <td>                            
                                        <asp:Button ID="btnZeinuKingaku2" runat="server" Text="税抜金額②" onmousedown="getTantouBoardScrollPosition();" Width="100px" Height="35px" CssClass="JC10ZeikomiBtn" OnClick="btnZeinuKingaku2_Click" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                            <asp:CheckBox ID="CHK_Midashi" runat="server" Text="見出し" style="margin-left:70px;" CssClass="JC10Chkbox"/>
                            <asp:CheckBox ID="CHK_Meisai" runat="server" Text="明細" style="margin-left:30px;" CssClass="JC10Chkbox"/>
                            <asp:CheckBox ID="CHK_Shosai" runat="server" Text="詳細" style="margin-left:30px;" CssClass="JC10Chkbox"/>
              
                                                                    
            </div>
            <div class="JC10MitumoriTourokuDiv" id="divMitumoriUriage" runat="server" style="padding-top:36px;padding-bottom:20px;padding-left:18px;padding-right:18px;">
                    <table style="margin-bottom:10px;width:100%;">
                        <tr>
                            <td>
                    <asp:Button ID="btnUriage" runat="server" CssClass=" BlueBackgroundButton JC10SaveBtn" Text="売上作成" OnClick="btnCreateUriage_Click"
                         Visible="True" Font-Size="14px" Height="35px"/>
                                </td>
                            <td style="text-align:right;">
                    <asp:UpdatePanel ID="updDisplayUriageItemSetting" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:Button ID="btnDisplayUriageItemSetting" runat="server" CssClass="JC07HyojiItemSettingBtn" Text="表示項目を設定"
                                            UseSubmitBehavior="false" style="margin-right:0px !important;" OnClick="btnDisplayUriageItemSetting_Click" OnClientClick="PopupOpenButton()"/>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                             </td>
                         </tr>
                    </table>
                <%-- <div id="Div6" runat="server" class="d-flex justify-content-center mt-3">--%>
                <div style="overflow-x:auto;width:100% !important;">
                    <div id="Div2" runat="server" style="width:auto;display:inline-block !important;">
                        <asp:UpdatePanel ID="updUriageGrid" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>
                                <asp:GridView runat="server" ID="GV_Uriage" BorderColor="Transparent" AutoGenerateColumns="false" EmptyDataRowStyle-CssClass="JC10NoDataMessageStyle" CssClass="GridViewStyle"
                                    ShowHeader="true" ShowHeaderWhenEmpty="true" RowStyle-CssClass="GridRow" CellPadding="0" Visible="false">
                                    <EmptyDataRowStyle CssClass="JC10NoDataMessageStyle" />
                                    <HeaderStyle Height="37px" BackColor="#F2F2F2" BorderColor="White" BorderWidth="2px" />
                                    <RowStyle CssClass="GridRow" Height="37px" />
                                    <SelectedRowStyle BackColor="#EBEBF5" />
                                    <Columns>
                                        <asp:TemplateField HeaderStyle-Width="30px">
                                            <ItemTemplate>   
                                                 <div class="grip" style="display:flex; align-content:center;align-items:center;align-items: center;justify-content: center;min-width:30px; overflow: hidden;">
                                                 <asp:HoverMenuExtender ID="HoverMenuExtender2" runat="server" PopupControlID="PopupMenu1"
                                                    TargetControlID="Panel2" PopupPosition="Bottom">
                                                </asp:HoverMenuExtender>
                                                <asp:Panel ID="PopupMenu1" runat="server" CssClass="modalPopup dropdown-menu fontcss " aria-labelledby="dropdownMenuButton" Style="display: none;">
                                                    <asp:LinkButton ID="lnkbtnUriageEdit" class="dropdown-item fontcss" runat="server" Text='編集' Style="margin-right: 10px" OnClick="lnkbtnUriageEdit_Click"></asp:LinkButton>
                                                    <asp:LinkButton ID="lnkbtnuriageOutput" class="dropdown-item fontcss" runat="server" Text='伝票PDF出力' Style="margin-right: 10px" CommandName="PDF" CommandArgument="<%# Container.DataItemIndex %>"  ></asp:LinkButton>
                                                    <asp:LinkButton ID="lnkbtnUriageDelete" class="dropdown-item fontcss" runat="server" Text='削除' Style="margin-right: 10px" OnClick="lnkbtnUriageDelete_Click"></asp:LinkButton>
                                                    
                                                </asp:Panel>
                                                <asp:Panel ID="Panel2" runat="server" CssClass="modalPopup" Style="width: 20px;">
                                                    <button class="btn dropdown-toggle" type="button" id="dropdownMenuButton" data-toggle="dropdown"
                                                        aria-haspopup="true" aria-expanded="false" style="border: 1px solid gainsboro; width: 20px; height: 20px; padding: 0px 3px 0px 1px; margin: 0;">
                                                    </button>
                                                </asp:Panel>
                                                     </div>
                                            </ItemTemplate>
                                            <HeaderTemplate>
                                                <div class="grip" style="display:flex; align-content:center;align-items:center;align-items: center;justify-content: center;min-width:30px; overflow: hidden;">
                                                     <asp:Label ID="lbldropdown"  runat="server" Text="LB_drop" style="display:none;"></asp:Label>
                                                    </div>
                                            </HeaderTemplate>
                                            <HeaderStyle BorderWidth="2px"/>
                                            <ItemStyle/>
                                        </asp:TemplateField>  
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                    <div class="grip" style="min-width:89px;text-align: left; padding-right: 4px;padding-left:2px;overflow: hidden;  display: -webkit-box;  -webkit-line-clamp: 1; -webkit-box-orient: vertical;word-break: break-all;">
                                                    <asp:LinkButton runat="server" ID="lnkCodeUriage" Text='<%# Eval("cURIAGE","{0}") %>' CssClass="d-inline-block font" Font-Underline="false" OnClick="lnkCodeUriage_Click" />
                                                     </div>
                                                </ItemTemplate>
                                                <HeaderTemplate>
                                                    <div class="grip" style="min-width:85px;text-align: left; padding-right: 4px;padding-left:2px;overflow: hidden;  display: -webkit-box;  -webkit-line-clamp: 1; -webkit-box-orient: vertical;word-break: break-all;">
                                                    <asp:Label ID="lblUriagecode" runat="server" Text="売上コード" CssClass="d-inline-block" ></asp:Label>
                                                    </div>
                                                </HeaderTemplate>
                                                <HeaderStyle BorderWidth="2px" />
                                                <ItemStyle/>
                                            </asp:TemplateField>

                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <div class="grip" style="min-width:89px;text-align: left; padding-right: 4px;padding-left:2px;overflow: hidden;  display: -webkit-box;  -webkit-line-clamp: 1; -webkit-box-orient: vertical;word-break: break-all;">
                                                    <asp:LinkButton runat="server" ID="lnkcMitumori" Text='<%# Bind("cMITUMORI","{0}") %>' CssClass="d-inline-block font" Font-Underline="false" OnClick="lnkcMitumori_Click"></asp:LinkButton>
                                                        </div>
                                                </ItemTemplate>
                                                <HeaderTemplate>
                                                    <div class="grip" style="min-width:85px;text-align: left; padding-right: 4px;padding-left:2px;overflow: hidden;  display: -webkit-box;  -webkit-line-clamp: 1; -webkit-box-orient: vertical;word-break: break-all;">
                                                    <asp:Label ID="lblMitsumoricode" runat="server" Text="見積コード"  CssClass="d-inline-block"></asp:Label>
                                                        </div>
                                                </HeaderTemplate>
                                                <HeaderStyle BorderWidth="2px"/>
                                                <ItemStyle />
                                            </asp:TemplateField>
                                          
                                        <asp:TemplateField>
                                                <ItemTemplate>
                                                     <div class="grip" style="min-width:60px;text-align: left; padding-right: 4px;padding-left:2px;overflow: hidden;  display: -webkit-box;  -webkit-line-clamp: 1; -webkit-box-orient: vertical;word-break: break-all;">
                                                    <asp:Label ID="lblsSeikyisaki" runat="server" Text='<%# Eval("sSEIKYUSAKI") %>'/>
                                                         </div>
                                                </ItemTemplate>
                                                <HeaderTemplate>
                                                    <div class="grip" style="min-width:60px;text-align: left; padding-right: 4px;padding-left:2px;overflow: hidden;  display: -webkit-box;  -webkit-line-clamp: 1; -webkit-box-orient: vertical;word-break: break-all;">
                                                    <asp:Label ID="lblSeikyusakiMei" runat="server" Text="請求先" CssClass="d-inline-block"></asp:Label>
                                                        </div>
                                                </HeaderTemplate>
                                                <HeaderStyle BorderWidth="2px"/>
                                                <ItemStyle/>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <div class="grip" style="min-width:60px;text-align: left; padding-right: 4px;padding-left:2px;overflow: hidden;  display: -webkit-box;  -webkit-line-clamp: 1; -webkit-box-orient: vertical;word-break: break-all;">
                                                    <asp:Label ID="lblstokuisaki" runat="server" Text='<%# Eval("sTOKUISAKI") %>'/>
                                                         </div>
                                                </ItemTemplate>
                                                <HeaderTemplate>
                                                    <div class="grip" style="min-width:60px;text-align: left; padding-right: 4px;padding-left:2px;overflow: hidden;  display: -webkit-box;  -webkit-line-clamp: 1; -webkit-box-orient: vertical;word-break: break-all;">
                                                    <asp:Label ID="lblTokuisakiMei" runat="server" Text="得意先" CssClass="d-inline-block"></asp:Label>
                                                        </div>
                                                </HeaderTemplate>
                                                <HeaderStyle BorderWidth="2px"/>
                                                <ItemStyle/>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                      <div class="grip" style="min-width:60px;text-align: left; padding-right: 4px;padding-left:2px;overflow: hidden;  display: -webkit-box;  -webkit-line-clamp: 1; -webkit-box-orient: vertical;word-break: break-all;">
                                                    <asp:Label runat="server" ID="lblsnouhin" Text='<%# Eval("snouhin") %>'></asp:Label>
                                                          </div>
                                                </ItemTemplate>
                                                <HeaderTemplate>
                                                    <div class="grip" style="min-width:60px;text-align: left; padding-right: 4px;padding-left:2px;overflow: hidden;  display: -webkit-box;  -webkit-line-clamp: 1; -webkit-box-orient: vertical;word-break: break-all;">
                                                    <asp:Label ID="lblKenmei" runat="server" Text="売上件名" CssClass="d-inline-block"></asp:Label>
                                                        </div>
                                                </HeaderTemplate>
                                                <HeaderStyle BorderWidth="2px"/>
                                                <ItemStyle/>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                       <div class="grip" style="min-width:75px;text-align: left; padding-right: 4px;padding-left:2px;overflow: hidden;  display: -webkit-box;  -webkit-line-clamp: 1; -webkit-box-orient: vertical;word-break: break-all;">
                                                    <asp:Label runat="server" ID="lblsTantousha" Text='<%#Eval("sTANTOUSHA")%>'></asp:Label>
                                                           </div>
                                                </ItemTemplate>
                                                <HeaderTemplate>
                                                    <div class="grip" style="min-width:75px;text-align: left; padding-right: 4px;padding-left:2px;overflow: hidden;  display: -webkit-box;  -webkit-line-clamp: 1; -webkit-box-orient: vertical;word-break: break-all;">
                                                    <asp:Label ID="lblEigyoTantousha" runat="server" Text="自社担当者" CssClass="d-inline-block"></asp:Label>
                                                        </div>
                                                </HeaderTemplate>
                                                <HeaderStyle BorderWidth="2px"/>
                                                <ItemStyle/>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <div class="grip" style="min-width:45px;text-align: left;padding-right: 4px;padding-left:2px;overflow: hidden;  display: -webkit-box;  -webkit-line-clamp: 1; -webkit-box-orient: vertical;word-break: break-all;">
                                                    <asp:Label runat="server" ID="lbldUriage" Text='<%# Bind("売上日","{0}") %>'></asp:Label>
                                                        </div>
                                                </ItemTemplate>
                                                <HeaderTemplate>
                                                    <div class="grip" style="min-width:45px;text-align: left;padding-right: 4px;padding-left:2px;overflow: hidden;  display: -webkit-box;  -webkit-line-clamp: 1; -webkit-box-orient: vertical;word-break: break-all;">
                                                    <asp:Label ID="lbldate" runat="server" Text="売上日" CssClass="d-inline-block"></asp:Label>
                                                        </div>
                                                </HeaderTemplate>
                                                <HeaderStyle BorderWidth="2px"/>
                                                <ItemStyle />
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <div class="grip" style="min-width:60px;text-align: right;padding-right: 4px;padding-left:2px;overflow: hidden;  display: -webkit-box;  -webkit-line-clamp: 1; -webkit-box-orient: vertical;word-break: break-all;">
                                                    <asp:Label runat="server" ID="lblUriageKingaku" Text='<%#Eval("売上会額")%>' ToolTip='<%#Eval("売上会額")%>'></asp:Label>
                                                        </div>
                                                </ItemTemplate>
                                                <HeaderTemplate>
                                                    <div class="grip" style="min-width:60px;text-align: right;padding-right: 4px;padding-left:2px;overflow: hidden;  display: -webkit-box;  -webkit-line-clamp: 1; -webkit-box-orient: vertical;word-break: break-all;">
                                                    <asp:Label ID="lblkingaku" runat="server" Text="売上会額" CssClass="d-inline-block"></asp:Label>
                                                        </div>
                                                </HeaderTemplate>
                                                <HeaderStyle BorderWidth="2px"/>
                                                <ItemStyle/>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <div class="grip" style="min-width:60px;text-align: left;padding-right: 4px;padding-left:2px;overflow: hidden;  display: -webkit-box;  -webkit-line-clamp: 1; -webkit-box-orient: vertical;word-break: break-all;">
                                                    <asp:Label runat="server" ID="lblUriageJoutai" Text='<%#Eval("売上状態")%>' ToolTip='<%#Eval("売上状態")%>'></asp:Label>
                                                        </div>
                                                </ItemTemplate>
                                                <HeaderTemplate>
                                                    <div class="grip" style="min-width:60px;text-align: left;padding-right: 4px;padding-left:2px;overflow: hidden;  display: -webkit-box;  -webkit-line-clamp: 1; -webkit-box-orient: vertical;word-break: break-all;">
                                                    <asp:Label ID="lbljoutai" runat="server" Text="売上状態" CssClass="d-inline-block"></asp:Label>
                                                        </div>
                                                </HeaderTemplate>
                                                <HeaderStyle BorderWidth="2px"/>
                                                <ItemStyle/>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <div class="grip" style="min-width:90px;text-align: left;padding-right: 4px;padding-left:2px;overflow: hidden;  display: -webkit-box;  -webkit-line-clamp: 1; -webkit-box-orient: vertical;word-break: break-all;">
                                                    <asp:Label runat="server" ID="lblsMemo" Text='<%#Eval("sMemo")%>' ToolTip='<%#Eval("sMemo")%>'></asp:Label>
                                                        </div>
                                                </ItemTemplate>
                                                <HeaderTemplate>
                                                    <div class="grip" style="min-width:90px;text-align: left;padding-right: 4px;padding-left:2px;overflow: hidden;  display: -webkit-box;  -webkit-line-clamp: 1; -webkit-box-orient: vertical;word-break: break-all;">
                                                    <asp:Label ID="lblmemo" runat="server" Text="メモ" CssClass="d-inline-block"></asp:Label>
                                                        <div class="grip" style="min-width:60px;text-align: left;padding-right: 4px;padding-left:2px;overflow: hidden;  display: -webkit-box;  -webkit-line-clamp: 1; -webkit-box-orient: vertical;word-break: break-all;">
                                                </HeaderTemplate>
                                                <HeaderStyle BorderWidth="2px"/>
                                                <ItemStyle/>
                                            </asp:TemplateField>

                                    </Columns>
                                </asp:GridView>

                                <asp:GridView runat="server" ID="GV_Uriage_Original" BorderColor="Transparent" AutoGenerateColumns="false" EmptyDataRowStyle-CssClass="JC10NoDataMessageStyle" CssClass="gridLines GridViewStyle" DataKeyNames="cURIAGE"
                                    ShowHeader="true" ShowHeaderWhenEmpty="true" RowStyle-CssClass="GridRow" CellPadding="0" OnRowDataBound="GV_Uriage_RowDataBound" OnRowCommand="GV_Uriage_Original_RowCommand">
                                    <EmptyDataRowStyle CssClass="JC10NoDataMessageStyle" />
                                    <HeaderStyle Height="37px" BackColor="#F2F2F2" BorderColor="White" BorderWidth="2px" />
                                    <RowStyle CssClass="JC34GridItem" Height="37px" />
                                    <SelectedRowStyle BackColor="#EBEBF5" />
                                    <Columns>
                                       <asp:TemplateField HeaderStyle-Width="30px">
                                            <ItemTemplate>   
                                                 <div class="grip" style="display:flex; padding-bottom:26px; align-content:center;align-items:center;align-items: center;justify-content: center;min-width:30px; overflow: hidden;">
                                                     <asp:Panel ID="DropdownPanel" runat="server">
                                                <div class="dropdown" style="position:absolute;">
                                                  <button class="btn dropdown-toggle" type="button" id="dropdownMenuButton" data-toggle="dropdown" 
                                                      aria-haspopup="true" aria-expanded="false" style="border:1px solid gainsboro;width:20px; height:20px;padding:0px 3px 0px 1px;margin:0; z-index:200;">
                                    
                                                  </button>
                                                  <div class="dropdown-menu" aria-labelledby="dropdownMenuButton" >
                                                   <asp:LinkButton ID="lnkbtnUriageEdit" class="dropdown-item font" runat="server" Text='編集' style="margin-right:10px;" OnClick="lnkbtnUriageEdit_Click"></asp:LinkButton>
                                                   <asp:LinkButton ID="lnkbtnuriageOutput" class="dropdown-item font" runat="server" Text='伝票PDF出力' style="margin-right:10px;" CommandName="PDF" CommandArgument="<%# Container.DataItemIndex %>"  ></asp:LinkButton>
                                                   <asp:LinkButton ID="lnkbtnUriageDelete" class="dropdown-item font" runat="server" Text='削除' style="margin-right:10px;" OnClick="lnkbtnUriageDelete_Click" ></asp:LinkButton>
                                                  </div>
                                                </div>
                                                         </asp:Panel>
                                                     </div>
                                            </ItemTemplate>
                                            <HeaderStyle BorderWidth="2px"/>
                                            <ItemStyle/>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                           
                    </div>
                     </div>
                </div>
        </div>  
                    
               <%-- </ContentTemplate>--%>
                <%--</asp:UpdatePanel>--%>
        <asp:HiddenField ID="HF_fHidukeValueChange" runat="server" />
            <asp:HiddenField ID="HF_beforeSortIndex" runat="server" />
            <asp:HiddenField ID="HF_afterSortIndex" runat="server" />
            <asp:Button ID="BT_Sort" runat="server" Text="Button" OnClick="BT_Sort_Click" style="display:none;" />
            <asp:Button ID="BT_ColumnWidthUriage" runat="server" Text="Button" OnClick="BT_ColumnWidthUriage_Click" style="display:none;" />
            <asp:Button ID="BT_ColumnWidthSyouhin" runat="server" Text="Button" OnClick="BT_ColumnWidthSyouhin_Click" style="display:none;" />
            <asp:Button ID="BT_ImageUpload" runat="server" Text="Button" OnClick="BT_ImageUpload_Click" style="display:none;"/>
            <asp:Button ID="BT_HyoshiImageUpload" runat="server" Text="Button" OnClick="BT_HyoshiImageUpload_Click" style="display:none;"/>
            <asp:Button ID="BT_Mitumorisyo1ImageUpload" runat="server" Text="Button" OnClick="BT_Mitumorisyo1ImageUpload_Click" style="display:none;"/>
            <asp:Button ID="BT_Mitumorisyo2Upload" runat="server" Text="Button" OnClick="BT_Mitumorisyo2Upload_Click" style="display:none;"/>
            <asp:HiddenField  ID="HF_checkData" runat="server"/>
                 <asp:HiddenField  ID="HF_maxRowNo" runat="server"/>
                 <asp:HiddenField ID="HF_ImgBase64" runat="server" />
                 <asp:HiddenField ID="HF_ImgSize" runat="server" />
                <asp:HiddenField ID="HF_HyoshiFileName" runat="server" />
                <asp:HiddenField ID="HF_Gazo1FileName" runat="server" />
                <asp:HiddenField ID="HF_Gazo2FileName" runat="server" />
                <asp:HiddenField ID="HF_isChange" runat="server" />
                  <asp:HiddenField ID="HF_fBtn" runat="server" />
                 <asp:HiddenField ID="HF_HyojiBtn" runat="server" />
                <asp:HiddenField ID="HF_fImageUpload" runat="server" />
                <asp:HiddenField ID="HF_cUriage" runat="server" />
                <asp:HiddenField ID="HF_TxtTani" runat="server" />
                 <asp:HiddenField ID="HF_GridSizeUriage" runat="server" />
                <asp:HiddenField ID="HF_GridUriage" runat="server" />
                <asp:HiddenField ID="HF_GridSizeSyouhin" runat="server" />
                <asp:HiddenField ID="HF_GridSyouhin" runat="server" />
                <asp:HiddenField ID="HF_syohinIndex" runat="server"/>
                <asp:HiddenField ID="HF_dragIndex" runat="server"/>
                <asp:HiddenField ID="HF_dropIndex" runat="server"/>
                <asp:HiddenField ID="HF_cJoutai" runat="server"/> 
                <asp:HiddenField ID="HF_fTankaChange" runat="server"/>
                 <asp:HiddenField ID="HF_fGenkaChange" runat="server"/>
                  <asp:HiddenField ID="HF_syouhinRowIndex" runat="server"/> 

                 <asp:Button ID="btnOK" runat="server" Text="OK" class="BlueBackgroundButton DisplayNone" Width="100px" Height="36px" />
                    <asp:UpdatePanel ID="upd_Hidden" runat="server" UpdateMode="Conditional" >
                            <Triggers>
                                 <asp:PostBackTrigger ControlID="BT_UriagePDF" />
                            </Triggers>
                             <ContentTemplate>
                         <asp:Button ID="BT_UriagePDF" runat="server" Text="Button" OnClick="BT_UriagePDF_Click" style="display:none;"/>
                        </ContentTemplate>  
                      </asp:UpdatePanel>
    
                <asp:UpdatePanel runat="server" ID="updLabelSave" UpdateMode="Conditional">
                    <ContentTemplate>
                        <div class="success JCSuccess" id="divLabelSave" runat="server">
                            <asp:Label ID="LB_Save" Text="保存しました。" runat="server" ForeColor="White" Font-Size="13px"></asp:Label>
                            <asp:Button ID="BT_LBSaveCross" Text="✕" runat="server" Style="background-color: white; border-style: none; right: 15px; position: absolute;" OnClick="BT_LBSaveCross_Click" />
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>

                <asp:Button ID="btnYes" runat="server" Text="はい" class="BlueBackgroundButton DisplayNone" Width="100px" Height="36px" OnClick="btnYes_Click" />
                <asp:Button ID="btnNo" runat="server" Text="いいえ" class="BlueBackgroundButton DisplayNone" Width="100px" Height="36px" OnClick="btnNo_Click" />
                <asp:Button ID="btnYes1" runat="server" Text="はい" class="BlueBackgroundButton DisplayNone" Width="100px" Height="36px" OnClick="btnYes1_Click" />
                <asp:Button ID="btnNo1" runat="server" Text="いいえ" class="BlueBackgroundButton DisplayNone" Width="100px" Height="36px" OnClick="btnNo1_Click" />
                <asp:Button ID="btnCancel" runat="server" Text="キャンセル" class="JC09GrayButton DisplayNone" Width="100px" Height="36px" />
                <asp:Button ID="btnmojiOK" runat="server" Text="はい" class="BlueBackgroundButton DisplayNone" Width="100px" Height="36px"/>

                <asp:Button ID="btnDeleteMitumori" runat="server" Text="" class="JC09GrayButton DisplayNone" Width="100px" Height="36px" OnClick="btnDeleteMitumori_Click" />
                <asp:Button ID="btnDeleteUriage" runat="server" Text="" class="JC09GrayButton DisplayNone" Width="100px" Height="36px" OnClick="btnDeleteUriage_Click" />

                 <div id="modal01" runat="server" class="JC10-modal" onclick="this.style.display='none'">
                    <div class="JC10-modal-content JC10-animate-zoom">
                        <img id="img01" style="width:100%;margin-left:auto;margin-right:auto;" runat="server">
                 </div>
                 </div>


   <asp:UpdatePanel ID="updShinkiPopup" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:Button ID="btnShinkiPopup" runat="server" Text="Button" Style="display: none" />
                    <asp:ModalPopupExtender ID="mpeShinkiPopup" runat="server" TargetControlID="btnShinkiPopup"
                        PopupControlID="pnlShinkiPopupScroll" BackgroundCssClass="PopupModalBackground" BehaviorID="pnlShinkiPopupScroll"
                        RepositionMode="RepositionOnWindowResize">
                    </asp:ModalPopupExtender>
                    <asp:Panel ID="pnlShinkiPopupScroll" runat="server" Style="display: none;height:100vh !important;overflow:hidden;" CssClass="PopupScrollDiv">
                        <asp:Panel ID="pnlShinkiPopup" runat="server">
                            <iframe id="ifShinkiPopup" runat="server" scrolling="yes"  style="height:100vh;width:100vw;"></iframe>
                        <asp:Button ID="btn_CloseSearch" runat="server" Text="Button" CssClass="DisplayNone" OnClick="btn_CloseSearch_Click" onClientClick="MasterOverflow()"/>
                        <asp:Button ID="btn_getSyosai" runat="server" Text="Button" CssClass="DisplayNone" OnClick="btn_getSyosai_Click" onClientClick="MasterOverflow()"/>
                            <asp:Button ID="btn_CloseMitumoriSearch" runat="server" Text="Button" CssClass="DisplayNone" OnClick="btn_CloseMitumoriSearch_Click" onClientClick="MasterOverflow()"/>
                            <asp:Button ID="btn_CloseTokuisakiSentaku" runat="server" Text="Button" CssClass="DisplayNone" OnClick="btn_CloseTokuisakiSentaku_Click" onClientClick="MasterOverflow()"/>
                            </asp:Panel>
                    </asp:Panel>
                </ContentTemplate>
            </asp:UpdatePanel>

         <!--ポップアップ画面-->
            <asp:UpdatePanel ID="upddatePopup" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:Button ID="btndatePopup" runat="server" Text="Button" CssClass="DisplayNone" />
                    <asp:ModalPopupExtender ID="mpedatePopup" runat="server" TargetControlID="btndatePopup"
                        PopupControlID="pnldatePopupScroll" BackgroundCssClass="PopupModalBackground" BehaviorID="pnldatePopupScroll"
                        RepositionMode="RepositionOnWindowResize">
                    </asp:ModalPopupExtender>
                    <asp:Panel ID="pnldatePopupScroll" runat="server" Style="display: none;" CssClass="PopupScrollDiv">
                        <asp:Panel ID="pnldatePopup" runat="server">
                            <iframe id="ifdatePopup" runat="server" class="NyuryokuIframe RadiusIframe" scrolling="no"  style="max-width: 260px; min-width: 260px; max-height: 365px; min-height: 365px;"></iframe>
                            <asp:Button ID="btnCalendarClose" runat="server" Text="Button" CssClass="DisplayNone" OnClick="btnCalendarClose_Click" onClientClick="MasterOverflow()"/>
                            <asp:Button ID="btnCalendarSettei" runat="server" Text="Button" CssClass="DisplayNone" OnClick="btnCalendarSettei_Click" onClientClick="MasterOverflow()"/>
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
                <asp:Panel ID="pnlSentakuPopupScroll" runat="server" Style="display: none;height:100%;overflow:hidden;" CssClass="PopupScrollDiv" HorizontalAlign="Center">
                    <asp:Panel ID="pnlSentakuPopup" runat="server">
                        <iframe id="ifSentakuPopup" runat="server" scrolling="no" class="NyuryokuIframe" style="border-radius: 0px;"></iframe>
                        <asp:Button ID="btnClose" runat="server" Text="Button" Style="display: none" OnClick="btnClose_Click" onClientClick="MasterOverflow()"/>
                        <asp:Button ID="btnKyotanSelect" runat="server" Text="Button" Style="display: none" OnClick="btnKyotenSelect_Click" onClientClick="MasterOverflow()"/>
                         <asp:Button ID="btnshiarailistSelect" runat="server" Text="Button" Style="display: none" OnClick="btnshiarailistSelect_Click" onClientClick="MasterOverflow()"/>
                        <asp:Button ID="btnYukoKigenListSelect" runat="server" Text="Button" Style="display: none" OnClick="btnYukoKigenListSelect_Click" onClientClick="MasterOverflow()"/>
                        <asp:Button ID="btnTokuisakiSelect" runat="server" Text="Button" Style="display: none" OnClick="btnTokuisakiSelect_Click" onClientClick="MasterOverflow()"/>
                           <asp:Button ID="btnTokuisakiTantouSelect" runat="server" Text="Button" Style="display: none" OnClick="btnTokuisakiTantouSelect_Click" onClientClick="MasterOverflow()"/>
                        <asp:Button ID="btnJoutaiSelect" runat="server" Text="Button" Style="display: none" OnClick="btnJoutaiSelect_Click" onClientClick="MasterOverflow()"/>
                        <asp:Button ID="btnJishaTantouSelect" runat="server" Text="Button" Style="display: none" OnClick="btnJishaTantouSelect_Click" onClientClick="MasterOverflow()"/>
                        <asp:Button ID="btnSyohinGridSelect" runat="server" Text="Button" Style="display: none" OnClick="btnSyohinGridSelect_Click" onClientClick="MasterOverflow()"/>
                        <asp:Button ID="btnArarisuIkkatsu" runat="server" Text="Button" Style="display: none" OnClick="btnArarisuIkkatsu_Click" onClientClick="MasterOverflow()"/>
                        <asp:Button ID="btnPDFPageChoiceClose" runat="server" Text="Button" Style="display: none" OnClick="btnPDFPageChoiceClose_Click" onClientClick="MasterOverflow()"/>
                         <asp:Button ID="btnToLogin" runat="server" Text="Button" Style="display: none" OnClick="btnToLogin_Click" onClientClick="MasterOverflow()"/>
                    </asp:Panel>
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>

                  <!--ポップアップ画面-->
           

                    <asp:UpdatePanel ID="updHyoujiSet" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:Button ID="btnHyoujiSetting" runat="server" Text="Button" style="display:none" />
                            <asp:ModalPopupExtender ID="mpeHyoujiSetPopUp" runat="server" TargetControlID="btnHyoujiSetting"
                                PopupControlID="pnlHyoujiSetPopUpScroll" BehaviorID="pnlHyoujiSetPopUpScroll" BackgroundCssClass="PopupModalBackground"
                                RepositionMode="RepositionOnWindowResize">
                            </asp:ModalPopupExtender>
                            <asp:Panel ID="pnlHyoujiSetPopUpScroll" runat="server"  CssClass="PopupScrollDiv">
                                <asp:Panel ID="pnlHyoujiSetPopUp" runat="server">
                                    <iframe id="ifpnlHyoujiSetPopUp" runat="server" scrolling="yes" class="HyoujiSettingIframe"  seamless></iframe>
                                       <asp:Button ID="btnHyoujiClose" runat="server" Text="Button" CssClass="DisplayNone" OnClick="btnHyoujiSettingClose_Click" onClientClick="MasterOverflow()"/>
                                       <asp:Button ID="btnHyoujiSave" runat="server" Text="Button" CssClass="DisplayNone" OnClick="btnHyoujiSettingSave_Click" onClientClick="MasterOverflow()"/>
                                </asp:Panel>
                            </asp:Panel>
                        </ContentTemplate>
                    </asp:UpdatePanel>

                   <asp:Label ID="lblLoginUserCode" runat="server" Text="" Visible="false"/>
                        <asp:Label ID="lblLoginUserName" runat="server" Text="" Visible="false"/>
<%--    </form>--%>

                

            </ContentTemplate>
    </asp:UpdatePanel>
</body>

</html>
    </asp:Content>

