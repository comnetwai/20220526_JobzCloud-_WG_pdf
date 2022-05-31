/************************************************************/
/* function    : DisableEnter                               */
/* description : エンターキーを無効化                       */
/* parameter   : なし                                       */
/* return      : なし                                       */
/************************************************************/
function DisableEnter() {
    if (window.event) {
        keyCode = event.keyCode ? event.keyCode : event.charCode;
        if (keyCode == 13) {
            if (event.srcElement.type != "textarea") {
                event.returnValue = false;
                event.cancelBubble = true;
            }
        }
    }
}

/************************************************************/
/* function    : MasterOverflow                             */
/* description : masterPage Body overflow                   */
/* parameter   : なし                                       */
/* return      : なし                                       */
/************************************************************/
function MasterOverflow() {
    document.getElementById("BD_Master").style.overflow = "auto";
}

/************************************************************/
/* function    : PopupOpenButton                            */
/* description : Popup ボタンクリック時にbackground scroll非表示                 */
/* parameter   : なし                                       */
/* return      : なし                                       */
/************************************************************/
function PopupOpenButton() {
    document.getElementById("BD_Master").style.overflow = "hidden";
}

/************************************************************/
/* function    : disableKey                                 */
/* description :                                            */
/* parameter   : eventListener                              */
/* return      : なし                                       */
/************************************************************/
function setEventListener(eventListener) {
    if (document.addEventListener) document.addEventListener('keydown', eventListener, true);
    else if (document.attachEvent) document.attachEvent('keydown', eventListener);
    else document.onkeydown = eventListener;
}

/************************************************************/
/* function    : disableKey                                 */
/* description : キーを無効化                               */
/* parameter   : window.event                               */
/* return      : false(if check ok)                         */
/************************************************************/
function disableKey(event) {

    var keyCode;
    var isCtrl;
    var isAltLeft;
    var isAlt;
    var forbiddenKeys = new Array('a', 'n',/*'c','x','v',*/'j');//無効にするすべてのCTRL+キーの組み合わせを一覧

    if (window.event) {
        keyCode = event.keyCode ? event.keyCode : event.charCode;     //IE
        if (window.event.ctrlKey)
            isCtrl = true;
        else
            isCtrl = false;

        if (window.event.altKey)
            isAlt = true;
        else
            isAlt = false;
    }
    else {
        keyCode = e.which;     //firefox
        if (e.ctrlKey)
            isCtrl = true;
        else
            isCtrl = false;

        if (e.altKey)
            isAlt = true;
        else
            isAlt = false;
    }

    if (keyCode == 116) {
        window.status = "F5 key detected! Attempting to disabling default response.";
        window.setTimeout("window.status='';", 2000);

        // Standard DOM (firefox):
        if (event.preventDefault) event.preventDefault();
        //IE (exclude Opera with !event.preventDefault):
        if (document.all && window.event && !event.preventDefault) {
            event.cancelBubble = true;
            event.returnValue = false;
            event.keyCode = 0;
        }
        return false;
    }

    if ((keyCode == 122)) {
        window.status = "F11 key detected! Attempting to disabling default response.";
        window.setTimeout("window.status='';", 2000);

        // Standard DOM (firefox):
        if (event.preventDefault) event.preventDefault();

        //IE (exclude Opera with !event.preventDefault):
        if (document.all && window.event && !event.preventDefault) {
            event.cancelBubble = true;
            event.returnValue = false;
            event.keyCode = 0;
        }
        return false;
    }

    //if ctrl is pressed check if other key is in forbidenKeys array
    if (isCtrl) {
        for (i = 0; i < forbiddenKeys.length; i++) {
            //case-insensitive comparation
            if (forbiddenKeys[i].toLowerCase() == String.fromCharCode(keyCode).toLowerCase()) {
                window.status = "Key combination CTRL + " + String.fromCharCode(keyCode) + " has been disabled.";
                window.setTimeout("window.status='';", 2000);
                return false;
            }
        }
    }
    //In Error Page Alt+left and Backspace is disabled. 
    if (document.getElementById("err_main") || document.getElementById("main01")) {
        if ((isAlt && keyCode == 37) || (keyCode == 8)) {
            return false;
        }
    }

    if (document.getElementById("main02")) {
        if (keyCode == 8) {
            if (event.srcElement.type != "text" && event.srcElement.type != "textarea") {
                return false;
            }
        }
    }
}

/************************************************************/
/* function    : ShowErrorMessage                           */
/* description : エラーメッセージを表示する                 */
/* parameter   : メッセージ                                 */
/* return      : なし                                       */
/************************************************************/
function ShowTaguMessage(msg, btnConfirmId, ctrlToFocus) {
    MessageTaguBox(msg, btnConfirmId, ctrlToFocus);
    return false;
}

/************************************************************/
/* function    : BtnTagClick                                */
/* description : テキストボックスにフォーカスする           */
/* parameter   : ctrl                                       */
/* return      : なし                                       */
/************************************************************/
function BtnTagClick(btnID) {
    alert("aa");
    var id = btnID.id;
    document.getElementById(id).click();
}

/************************************************************/
/* function    : CheckIsEnterKeyForSearch                   */
/* description : テキストボックスにEnterをチェックする      */
/* parameter   : ctrl                                       */
/* return      : bool                                       */
/************************************************************/
function CheckIsEnterKeyForSearch(ctrl) {
    if ((event.keyCode != 13)) {
        return true;
    }
    else {
        ctrl.focus();
        return false;
    }
}

/***************************************************************/
/* function    : kikaiListClicked                              */
/* description : 機械リストをクリック                          */
/* parameter   : index                                         */
/* return      : なし                                          */
/***************************************************************/
function kikaiListClicked(index) {
    var btnKikai = document.getElementById("btn" + index);
    btnKikai.textContent = index;
    btnKikai.click();
}

/************************************************************/
/* function    : CheckNumeric                               */
/* description : 数字だけを有効                             */
/* parameter   : onkeypress                                 */
/* return      : true(if check ok)                          */
/************************************************************/
function CheckNumeric() {
    return event.keyCode >= 48 && event.keyCode <= 57;
}

/************************************************************/
/* function    : CheckNumeric                               */
/* description : Enterキーを押しす場合、TabIndexの通り連動  */
/* parameter   : onkeydown                                  */
/* return      : true(if check ok)                          */
/************************************************************/
function enterToTab(e) {
    if (window.event.keyCode == 13) window.event.keyCode = 9;
}

/************************************************************/
/* function    : limit                                      */
/* description : 最大入力チェックをする                     */
/* parameter   : onkeyup、onkeydown                         */
/* return      : なし                                       */
/************************************************************/
function limit(element, max) {
    var max_chars = max;
    if (element.value.length > max_chars) {
        element.value = element.value.substr(0, max_chars);
    }
}

/***************************************************************/
/* function    : SentakuListClicked                              */
/* description : 機械リストをクリック                          */
/* parameter   : index                                         */
/* return      : なし                                          */
/***************************************************************/
function SetPropertyListClicked(index) {
    var btnSentaku = document.getElementById("cphContent_btn" + index);
    btnSentaku.textContent = index;
    btnSentaku.click();
}

/************************************************************/
/* function    : CheckNumeric2                               */
/* description : 数字だけを有効                             */
/* parameter   : onkeypress                                 */
/* return      : true(if check ok)                          */
/************************************************************/
function isNumberKey() {
    return (event.keyCode >= 48 && event.keyCode <= 57) || event.keyCode == 46;
}

/************************************************************/
/* function    : DisableBackSpace                           */
/* description : BackSpaceキーを無効化                      */
/* parameter   : window.event                               */
/* return      : false(if check ok)                         */
/************************************************************/
function DisableBackSpace(event) {
    if (window.event) {
        if (event.keyCode == 8) {
            return false;
        }
    }
}

/************************************************************/
/* function    : onclicksetFocus                            */
/* description : Gridview でカーソル設定を有効              */
/* parameter   : clickid                                    */
/* return      : なし                                       */
/************************************************************/
function onclicksetFocus(clickid) {
    var hdnIdscript = "";
    document.getElementById("cphContent_hdnId").value = clickid;
}

/************************************************************/
/* function    : ChangeTypeNumber                           */
/* description : テキストボックスのタイプをnumberに変更する */
/* parameter   : control                                    */
/* return      : なし                                       */
/************************************************************/
function ChangeTypeNumber(ctrl) {
    ctrl.setAttribute("type", "number");
}

/************************************************************/
/* function    : BtnClick                                   */
/* description : 現在の画面のボタンクリック                 */
/* parameter   : id                                         */
/* return      : なし                                       */
/************************************************************/
function BtnClick(id) {
    document.getElementById(id).click();
}

/************************************************************/
/* function    : popupCloseClick                            */
/* description : ポップアップのキャンセルボタンクリック     */
/* parameter   : なし                                       */
/* return      : なし                                       */
/************************************************************/
function popupCloseClick(btnID) {
    // 処理中メッセージを表示する
    displayLoadingModal();


    (btnID);
}

/*************************************************************/
/* function    : parentBtnClick                              */
/* description : ポップアップからParentのボタンクリック      */
/* parameter   : なし                                        */
/* return      : なし                                        */
/*************************************************************/
function parentButtonClick(btnHome, value) {
    
    // ホーム画面から表示させる時
    if (value == "Popup") {
        var btnId = btnHome;
    } else if (value == "Master"){
        var btnId = 'MainContent_' + btnHome;
    }
    window.parent.document.getElementById(btnId).click();
}

/************************************************************************/
/* function    : displayLoadingModal                                    */
/* description : 処理中メッセージを表示する                             */
/* parameter   : flgReadCSV                                             */
/* return      : なし                                                   */
/************************************************************************/
function displayLoadingModal(flgReadCSV) {
    var length = $('div[id^=divLoading]').length;
    if (length == 0) {
        // Build loading element 
        var dialogDiv = "<div id='divLoading' class='DialogInnerDiv'>";
        if (flgReadCSV == null) {
            dialogDiv += "<span class='DisplayBlock'>処理中</span>";
        }
        else {
            dialogDiv += "<span class='DisplayBlock'>読込中</span>";
        }
        dialogDiv += "<img src='../Img/loading.gif' height='30' /></div>";
        // Create a loading element and display it
        var myDialog = $(dialogDiv).dialog({
            height: 30,
            width: 30,
            draggable: false,
            resizable: false,
            modal: true,
            dialogClass: "DialogStyle"
        });
        // Hide any titlebars
        $(".ui-dialog-titlebar").hide();
        myDialog.dialog('open');
    }
}

/************************************************************************/
/* function    : closeLoadingModal                                      */
/* description : 処理中メッセージを閉じる                               */
/* parameter   : なし                                                   */
/* return      : なし                                                   */
/************************************************************************/
function closeLoadingModal() {
    var length = $('div[id^=divLoading]').length;
    for (i = 0; i < length; i++) {
        var loadingDiv = $("#divLoading");
        if (loadingDiv != null) {
            $("#divLoading").dialog('destroy').remove();
        }
    }
}

/************************************************************************/
/* function    : checkToShowLoading                                     */
/* description : 処理中メッセージを表示するかチェックする               */
/* parameter   : elemtId                                                */
/* return      : なし                                                   */
/************************************************************************/
function checkToShowLoading(elemtId) {
    var elemtSelect = document.getElementById(elemtId);
    if (elemtSelect.checked == true)
        displayLoadingModal();
}

/************************************************************/
/* function    : ChangeTypeTel                              */
/* description : テキストボックスのタイプをtelに変更する    */
/* parameter   : control                                    */
/* return      : なし                                       */
/************************************************************/
function ChangeTypeTel(ctrl) {
    ctrl.setAttribute("type", "tel");
    ctrl.focus();
    ctrl.setSelectionRange(ctrl.value.length, ctrl.value.length);
}

/************************************************************/
/* function    : CheckLength                                */
/* description : 最大入力できる数字数に変更する             */
/* parameter   : control                                    */
/* return      : なし                                       */
/************************************************************/
function CheckLength(obj, maxLength) {
    ChangeTypeNumber(obj);
    if (obj.value.length >= maxLength) {
        obj.value = obj.value.substring(0, maxLength);
    }
}

/************************************************************/
/* function    : CheckNumberWithLength                      */
/* description : 数字だけを有効                             */
/* parameter   : control, maxlength                         */
/* return      : true(if check ok)                          */
/************************************************************/
function CheckNumberWithLength(obj, maxLength) {
    if (event.keyCode == 9 || event.keyCode == 9 && event.shiftKey || event.keyCode == 8 || event.keyCode == 13
        || event.keyCode == 37 || event.keyCode == 39) {
        return true;
    }

    // delete を押す時 'del'か'.'かチェックする
    if (event.keyCode == 46) {
        var charStr = String.fromCharCode(event.keyCode);
        if (charStr == '.') {
            return false;
        }
        return true;
    }

    // テキストを選択している場合
    if (IsTextSelected(obj)) {
        return event.keyCode >= 48 && event.keyCode <= 57;
    }

    if (obj.value.length >= maxLength) {
        return false;
    }
    return event.keyCode >= 48 && event.keyCode <= 57;
}

/************************************************************/
/* function    : IsTextSelected                             */
/* description : テキストが選択されているかチェックする     */
/* parameter   : control                                    */
/* return      : true(if check ok)                          */
/************************************************************/
function IsTextSelected(ctrl) {
    var selectedText;
    var val = ctrl.value;
    if (ctrl.selectionStart != null) {
        var startPos = ctrl.selectionStart;
        var endPos = ctrl.selectionEnd;
        selectedText = val.substring(startPos, endPos);
    } else if ("selection" in document) {
        ctrl.focus();
        selectedText = document.selection.createRange().text;
    }
    else {
        selectedText = window.getSelection().toString();
    }
    if (selectedText == null || selectedText == "") {
        return false;
    }
    else {
        return true;
    }
}

/************************************************************/
/* function    : showPRTPopup                               */
/* description : 項目によって伸びる画面を表示する           */
/* parameter   : pnlId                                      */
/* return      : なし                                       */
/************************************************************/
function showPopup(outerPnlId, pnlId, iframeId, value, flg, mobile) {
    calculatePopupWidthHeight(outerPnlId, pnlId, iframeId, value, flg, mobile);
}

/************************************************************/
/* function    : calculatePRTPopupWidthHeight               */
/* description : popupの高さと幅設定                        */
/* parameter   : pnlId,iframeIdvalue,value                  */
/* return      : なし                                       */
/************************************************************/
function calculatePopupWidthHeight(outerPnlId, pnlId, iframeId, value, flg, mobile) {
    var body = document.body;
    var html = document.documentElement;

    var height = body.offsetHeight;
    if (mobile) {
        var width = screen.width;
        if (flg == 1) {
            var height = parent.document.documentElement.clientHeight;
        }
    }
    else {
        if (flg == 1) {
            var width = body.offsetWidth;
        }
        else {
            var width = Math.max(body.scrollWidth, body.offsetWidth,
                html.clientWidth, html.scrollWidth, html.offsetWidth);
        }
    }

    if (parent.setPopupWidthHeight != null) {
        parent.setPopupWidthHeight(width, height, outerPnlId, pnlId, iframeId, value, mobile);
    }
}

/************************************************************************/
/* function    : setPRTPopupWidthHeight                                 */
/* description : 画面を表示する時大きさ調整                             */
/* parameter   : 幅,高さ,pnlId,iframeIdvalue,value                      */
/* return      : なし                                                   */
/************************************************************************/
function setPopupWidthHeight(width, height, outerPnlName, pnlName, iframeName, value, mobile) {
    // マスター画面のスクロールバーを非表示する
    $('body').css('overflow', 'hidden');

    var body = document.body,
        html = document.documentElement;
    var minHeight = Math.min(body.scrollHeight, body.offsetHeight,
        html.clientHeight, html.scrollHeight, html.offsetHeight);
    var minWidth = window.innerWidth;
    var remainHeight = minHeight - height;
    var remainWidth = minWidth - width;
    if (!mobile) {
        var topDistance = remainHeight / 2;
        var leftDistance = remainWidth / 2;
    }
    if (value == 'Popup' || value == 'Master') {
        var pnlId = pnlName;
        var iframeId = iframeName;
    }
    else {
        // ホーム画面から表示させる時
        var pnlId = 'cphContent_' + pnlName;
        var iframeId = 'cphContent_' + iframeName;
    }

    var pnlBackground = outerPnlName + '_backgroundElement';

    if (document.getElementById(pnlBackground) != null) {
        document.getElementById(pnlBackground).style.width = minWidth + 'px';
    }

    if (document.getElementById(pnlId) != null) {
        // ポップアップが表示できる範囲より多くなっている時
        if (topDistance < 0) {
            topDistance = 10;
            document.getElementById(pnlId).style.marginBottom = topDistance + 'px';
        }
        else {
            document.getElementById(pnlId).style.marginBottom = 0 + 'px';
        }

        if (leftDistance < 0) {
            leftDistance = 10;
            document.getElementById(pnlId).style.marginRight = leftDistance + 'px';
        }
        else {
            document.getElementById(pnlId).style.marginRight = 0 + 'px';
        }

        document.getElementById(pnlId).style.top = topDistance + 'px';
        document.getElementById(pnlId).style.left = leftDistance + 'px';
        document.getElementById(pnlId).style.position = 'absolute';
    }

    if (document.getElementById(iframeId) != null) {
        document.getElementById(iframeId).style.height = height + 'px';
        document.getElementById(iframeId).style.width = width + 'px';
        document.getElementById(iframeId).style.borderWidth = '2px';
    }
}

/************************************************************/
/* function    : ShowNotifyMsg                              */
/* description : お知らせメッセージを表示する               */
/* parameter   : msg, parentID                              */
/* return      : なし                                       */
/************************************************************/
function ShowNotifyMsg(msg, parentID, divWidth, lblWidth) {
    var div = document.createElement('div');
    div.className = 'NotiMsgDiv';
    div.style.width = divWidth;
    div.id = 'msgDiv';

    var label = document.createElement('label');
    label.innerText = msg;
    label.setAttribute("class", "NotiLbl");
    label.style.width = lblWidth;

    var button = document.createElement('input');
    button.type = "button";
    button.value = "×";
    button.setAttribute("class", "NotiCloseBtn");
    button.addEventListener("click", CloseNotifyMsg, false);

    div.appendChild(label);
    div.appendChild(button);

    document.getElementById(parentID).appendChild(div);

    // 3秒で自動で消える
    setTimeout(CloseNotifyMsg, 2500);
    closeLoadingModal();
}

/************************************************************/
/* function    : CloseNotifyMsg                             */
/* description : お知らせメッセージを閉じる                 */
/* parameter   : なし                                       */
/* return      : なし                                       */
/************************************************************/
function CloseNotifyMsg() {
    $("#msgDiv").remove();
}

/************************************************************/
/* function    : ShowAnkenChangeConfirmMessage              */
/* description : 確認メッセージを表示する                   */
/* parameter   : メッセージ                                 */
/*             : 隠しボタンのID                             */
/* return      : なし                                       */
/************************************************************/
function DeleteConfirmMessage(msg, btnId) {
    AnkenChangeConfirmBox(msg, btnId);
    closeLoadingModal();
    return false;
}

/************************************************************/
/* function    : AnkenChangeConfirmBox                      */
/* description : 確認メッセージを表示する                   */
/* parameter   : メッセージ                                 */
/* return      : なし                                       */
/************************************************************/
function DeleteConfirmBox(message, btnDeleteYes, btnCancel) {
    var myDialog = $("<div id='msgDialogAlert'><img src='../Img/info_2.png' style='width: 30px;float:left;'/><p id='alertMessage' ></p></div>").dialog({
        autoOpen: false,
        modal: true,
        draggable: false,
        title: "見えるJOBZ",
        width: 350,
        resizable: false,
        closeOnEscape: true,
        dialogClass: "custom-dialog-1",
        buttons: [{
            text: "OK",
            "class": 'BlueBackgroundButton',
            width: 100,
            height: 30,
            click: function () {
                $(this).dialog("close");
                document.getElementById(btnDeleteYes).click();
            }
        },
        {
            text: "キャンセル",
            "class": 'JC09GrayButton',
            width: 100,
            height: 30,
            click: function () {
                $(this).dialog("close");
                document.getElementById(btnCancel).click();
            }
        }],
        create: function () {
            var buttons = $('.ui-dialog-buttonset').children('button');
            buttons.removeClass("ui-button ui-corner-all ui-widget");
        },
        close: function () {
            $("#alertMessage").html("");
        },
        show: {}
    });

    $('[id$=alertMessage]').css({
        "textAlign": "left",
        "font-size": "13px",
        "overflow": "hidden",
        "text-overflow": "ellipsis",
        "vertical- align": "middle",
        "padding- left": "10px",
        "margin": "0.5rem 0px"
    });
    $('[id$=alertMessage]').html(message);
    myDialog.dialog('open');
}

/************************************************************/
/* function    : ShowSaveChangesConfirmMessage              */
/* description : 確認メッセージを表示する                   */
/* parameter   : メッセージ                                 */
/*             : 隠しボタンのID                             */
/* return      : なし                                       */
/************************************************************/
function ShowKoumokuChangesConfirmMessage(msg, btnYes,btnNo,btnCancel) {
    KoumokuChangeConfirmBox(msg, btnYes, btnNo, btnCancel);
    closeLoadingModal();
    return false;
}

/************************************************************/
/* function    : ShowSaveChangesConfirmBox                      */
/* description : 確認メッセージを表示する                   */
/* parameter   : メッセージ                                 */
/*             : 隠しボタンのID                             */
/* return      : なし                                       */
/************************************************************/
function KoumokuChangeConfirmBox(message, btnYes, btnNo, btnCancel) {
    var myDialog = $("<div id='msgDialogAlert'><img src='../Img/info_2.png' style='width: 30px;float:left;'/><p id='alertMessage' ></p></div>").dialog({
        autoOpen: false,
        modal: true,
        draggable: false,
        title: "見えるJOBZ",
        width: 380,
        resizable: false,
        closeOnEscape: true,
        dialogClass: "custom-dialog-1",
        buttons: [{
            text: "はい",
            "class": 'BlueBackgroundButton',
            width: 100,
            height: 30,
            click: function () {
                $(this).dialog("close");
                document.getElementById(btnYes).click();
            }
        },
        {
            text: "いいえ",
            "class": 'BlueBackgroundButton',
             width: 100,
             height: 30,
             click: function () {
                 $(this).dialog("close");
                 document.getElementById(btnNo).click();
             }
        },
        {
            text: "キャンセル",
            "class": 'JC09GrayButton',
            width: 100,
            height: 30,
            click: function () {
                $(this).dialog("close");
                document.getElementById(btnCancel).click();
            }
        }],
        create: function () {
            var buttons = $('.ui-dialog-buttonset').children('button');
            buttons.removeClass("ui-button ui-corner-all ui-widget");
        },
        close: function () {
            $("#alertMessage").html("");
        },
        show: {}
    });

    $('[id$=alertMessage]').css({
        "textAlign": "left",
        "font-size": "13px",
        "overflow": "hidden",
        "text-overflow": "ellipsis",
        "vertical- align": "middle",
        "margin": "0.5rem 0px"
    });
    $('[id$=alertMessage]').html(message);
    myDialog.dialog('open');
}

/*************************************************************/
/* function    : ShowErrorMessage                            */
/* description : エラーメッセージを表示する                  */
/* parameter   : メッセージ                                  */
/* return      : なし                                        */
/*************************************************************/
function ShowErrorMessage(msg) {
    AlertBox(msg);
    closeLoadingModal();
    return false;
}

/*************************************************************/
/* function    : AlertBox                                    */
/* description : エラーメッセージを表示する                  */
/* parameter   : メッセージ                                  */
/* return      : なし                                        */
/*************************************************************/
function AlertBox(message) {
    var myDialog = $("<div id='msgDialogAlert'><img src='../Img/info_2.png' style='width: 30px;float:left;'/><p id='alertMessage' ></p></div>").dialog({
        autoOpen: false,
        modal: true,
        draggable: false,
        title: "見えるJOBZ",
        width: 315,
        resizable: false,
        closeOnEscape: true,
        dialogClass: "custom-dialog-1",
        buttons: [
            {
                text: "OK",
                "class": 'BlueBackgroundButton',
                width: 100,
                height: 30,
                click: function () {
                    $(this).dialog("close");
                    document.getElementById(btnCancel).click();
                }
            }],
        create: function () {
            var buttons = $('.ui-dialog-buttonset').children('button');
            buttons.removeClass("ui-button ui-corner-all ui-widget");
        },
        close: function () {
            $("#alertMessage").html("");
        },
        show: {}
    });

    $('[id$=alertMessage]').css({
        "textAlign": "left",
        "font-size": "14px",
        "overflow": "hidden",
        "text-overflow": "ellipsis",
        "vertical- align": "middle",
        "margin": "0.3rem 2px"
    });
    $('[id$=alertMessage]').html(message);
    myDialog.dialog('open');
}
/*************************************************************/
/* function    : ShowJoutaiErrorMessage                            */
/* description : エラーメッセージを表示する                  */
/* parameter   : メッセージ                                  */
/* return      : なし                                        */
/*************************************************************/
function ShowJoutaiErrorMessage(msg) {
    JoutaiAlertBox(msg);
    closeLoadingModal();
    return false;
}

/*************************************************************/
/* function    : AlertBox                                    */
/* description : エラーメッセージを表示する                  */
/* parameter   : メッセージ                                  */
/* return      : なし                                        */
/*************************************************************/
function JoutaiAlertBox(message) {
    var myDialog = $("<div id='msgDialogAlert'><img src='../Img/info_2.png' style='width: 30px;float:left;'/><p id='alertMessage' ></p></div>").dialog({
        autoOpen: false,
        modal: true,
        draggable: false,
        title: "見えるJOBZ",
        width: 370,
        resizable: false,
        closeOnEscape: true,
        dialogClass: "custom-dialog-1",
        buttons: [
            {
                text: "OK",
                "class": 'BlueBackgroundButton',
                width: 100,
                height: 30,
                click: function () {
                    $(this).dialog("close");
                    document.getElementById(btnCancel).click();
                }
            }],
        create: function () {
            var buttons = $('.ui-dialog-buttonset').children('button');
            buttons.removeClass("ui-button ui-corner-all ui-widget");
        },
        close: function () {
            $("#alertMessage").html("");
        },
        show: {}
    });

    $('[id$=alertMessage]').css({
        "textAlign": "left",
        "font-size": "14px",
        "overflow": "hidden",
        "text-overflow": "ellipsis",
        "vertical- align": "middle",
        "margin": "0.3rem 2px"
    });
    $('[id$=alertMessage]').html(message);
    myDialog.dialog('open');
}

/*************************************************************/
/* function    : ShowErrorMessageWithWidth                   */
/* description : エラーメッセージを表示する                  */
/* parameter   : メッセージ                                  */
/* return      : なし                                        */
/*************************************************************/
function ShowErrorMessageWithWidth(msg, mWidth) {
    AlertBoxWithWidth(msg, mWidth);
    closeLoadingModal();
    return false;
}

/*************************************************************/
/* function    : AlertBox                                    */
/* description : エラーメッセージを表示する                  */
/* parameter   : メッセージ                                  */
/* return      : なし                                        */
/*************************************************************/
function AlertBoxWithWidth(message, mWidth) {
    var myDialog = $("<div id='msgDialogAlert'><img src='../Img/info_2.png' style='width: 30px;float:left;'/><p id='alertMessage' ></p></div>").dialog({
        autoOpen: false,
        modal: true,
        draggable: false,
        title: "見えるJOBZ",
        width:  mWidth,
        resizable: false,
        closeOnEscape: true,
        dialogClass: "custom-dialog-1",
        buttons: [
            {
                text: "OK",
                "class": 'BlueBackgroundButton',
                width: 100,
                height: 30,
                click: function () {
                    $(this).dialog("close");
                    document.getElementById(btnCancel).click();
                }
            }],
        create: function () {
            var buttons = $('.ui-dialog-buttonset').children('button');
            buttons.removeClass("ui-button ui-corner-all ui-widget");
        },
        close: function () {
            $("#alertMessage").html("");
        },
        show: {}
    });

    $('[id$=alertMessage]').css({
        "textAlign": "left",
        "font-size": "14px",
        "overflow": "hidden",
        "text-overflow": "ellipsis",
        "vertical- align": "middle",
        "margin": "0.3rem 2px"
    });
    $('[id$=alertMessage]').html(message);
    myDialog.dialog('open');
}

/*************************************************************/
/* function    : ShowMailKakuninMessage                      */
/* description : メール確認メッセージを表示する              */
/* parameter   : メッセージ                                  */
/* return      : なし                                        */
/*************************************************************/
function ShowMailKakuninMessage(msg, btnOK) {
    AlertBox(msg, btnOK);
    closeLoadingModal();
    return false;
}

/*************************************************************/
/* function    : MailKakuninMsgBox                           */
/* description : メール確認メッセージを表示する              */
/* parameter   : メッセージ                                  */
/* return      : なし                                        */
/*************************************************************/

function MailKakuninMsgBox(message, btnOK) {
    var myDialog = $("<div id='msgDialogAlert'><img src='../Img/info_2.png' style='width: 30px;float:left;'/><p id='alertMessage' ></p></div>").dialog({
        autoOpen: false,
        modal: true,
        draggable: false,
        title: "見えるJOBZ",
        width: 380,
        resizable: false,
        closeOnEscape: true,
        dialogClass: "custom-dialog-1",
        buttons: [
            {
                text: "OK",
                "class": 'BlueBackgroundButton',
                width: 100,
                height: 30,
                click: function () {
                    $(this).dialog("close");
                    document.getElementById(btnOK).click();
                }
            }],
        create: function () {
            var buttons = $('.ui-dialog-buttonset').children('button');
            buttons.removeClass("ui-button ui-corner-all ui-widget");
        },
        close: function () {
            $("#alertMessage").html("");
        },
        show: {}
    });

    $('[id$=alertMessage]').css({
        "textAlign": "left",
        "font-size": "13px",
        "overflow": "hidden",
        "text-overflow": "ellipsis",
        "vertical- align": "middle",
        "margin": "0.5rem 0px"
    });
    $('[id$=alertMessage]').html(message);
    myDialog.dialog('open');
}

/*************************************************************/
/* function    : ShowSuccessMessage                          */
/* description : メール確認メッセージを表示する              */
/* parameter   : メッセージ                                  */
/* return      : なし                                        */
/*************************************************************/
function ShowSuccessMessage(msg, btnOK) {
    SuccessMsgBox(msg, btnOK);
    closeLoadingModal();
    return false;
}

/*************************************************************/
/* function    : SuccessMsgBox                               */
/* description : メール確認メッセージを表示する              */
/* parameter   : メッセージ                                  */
/* return      : なし                                        */
/*************************************************************/

function SuccessMsgBox(message, btnOK) {
    var myDialog = $("<div id='msgDialogAlert'><img src='../Img/info_2.png' style='width: 30px;float:left;'/><p id='alertMessage' ></p></div>").dialog({
        autoOpen: false,
        modal: true,
        draggable: false,
        title: "見えるJOBZ",
        width: 315,
        resizable: false,
        closeOnEscape: true,
        dialogClass: "custom-dialog-1",
        buttons: [
            {
                text: "OK",
                "class": 'BlueBackgroundButton',
                width: 100,
                height: 30,
                click: function () {
                    $(this).dialog("close");
                    document.getElementById(btnOK).click();
                }
            }],
        create: function () {
            var buttons = $('.ui-dialog-buttonset').children('button');
            buttons.removeClass("ui-button ui-corner-all ui-widget");
        },
        close: function () {
            $("#alertMessage").html("");
        },
        show: {}
    });

    $('[id$=alertMessage]').css({
        "textAlign": "left",
        "font-size": "13px",
        "overflow": "hidden",
        "text-overflow": "ellipsis",
        "vertical- align": "middle",
        "margin": "0.5rem 0px"
    });
    $('[id$=alertMessage]').html(message);
    myDialog.dialog('open');
}


/************************************************************/
/* function    : ShowSaveConfirmBox                         */
/* description : 確認メッセージを表示する                   */
/* parameter   : メッセージ                                 */
/*             : 隠しボタンのID                             */
/* return      : なし                                       */
/************************************************************/
function ShowSaveConfirmBox(msg, btnId, btnConfirmId) {
    SaveConfirmBox(msg, btnId, btnConfirmId, null);
    closeLoadingModal();
    return false;
}

/************************************************************/
/* function    : ShowMessage                                */
/* description : 確認メッセージを表示する                   */
/* parameter   : メッセージ                                 */
/*             : 隠しボタンのID                             */
/* return      : なし                                       */
/************************************************************/
function ShowMessage(msg, btnId, btnConfirmId) {
    ConfirmBox(msg, btnId, btnConfirmId, null);
    closeLoadingModal();
    return false;
}

/************************************************************/
/* function    : ConfirmBox                                 */
/* description : 確認メッセージを表示する                   */
/* parameter   : メッセージ                                 */
/*             : 隠しボタンのID                             */
/* return      : なし                                       */
/************************************************************/

function ConfirmBox(message, btnConfirmId, btnCancelId, controlToFocus) {
    var myDialog = $("<div id='msgDialogAlert'><p id='alertMessage' ></p></div>").dialog({
        autoOpen: false,
        modal: true,
        draggable: false,
        title: "JOBZRacoo",
        width: 320,
        resizable: false,
        closeOnEscape: true,
        beforeClose: function () {
            if (btnCancelId != null && btnCancelId != "") {
                document.getElementById(btnCancelId).click();
            }
        },
        buttons: [{
            text: "OK",
            click: function () {
                $(this).dialog("close");
                if (btnConfirmId != null && btnConfirmId != "") {
                    document.getElementById(btnConfirmId).click();
                }
            }
        },
        {
            text: "キャンセル",
            click: function () {
                $(this).dialog("close");
                if (btnCancelId != null && btnCancelId != "") {
                    document.getElementById(btnCancelId).click();
                }
            }
        }],
        create: function () {
            var buttons = $('.ui-dialog-buttonset').children('button');
            buttons.removeClass("ui-button ui-corner-all ui-widget");
        },
        close: function () {
            $("#alertMessage").html("" && controlToFocus != "");
            if (controlToFocus != null)
                controlToFocus.focus();
        },
        show: {}
    });

    $('[id$=alertMessage]').css({
        "textAlign": "center",
        "font-size": "13px",
        "overflow": "hidden",
        "text-overflow": "ellipsis",
        "vertical- align": "middle",
        "margin": "0.5rem 0px"
    });
    $('[id$=alertMessage]').html(message);
    myDialog.dialog('open');
}

/************************************************************/
/* function    : SaveConfirmBox                             */
/* description : 確認メッセージを表示する                   */
/* parameter   : メッセージ                                 */
/*             : 隠しボタンのID                             */
/* return      : なし                                       */
/************************************************************/

function SaveConfirmBox(message, btnConfirmId, btnId, controlToFocus) {
    var myDialog = $("<div id='msgDialogAlert'><p id='alertMessage' ></p></div>").dialog({
        autoOpen: false,
        modal: true,
        draggable: false,
        title: "JOBZRacoo",
        width: 335,
        resizable: false,
        closeOnEscape: true,
        buttons: [{
            text: "はい",
            click: function () {
                $(this).dialog("close");
                if (btnConfirmId != null && btnConfirmId != "") {
                    document.getElementById(btnConfirmId).click();
                }
            }
        },
        {
            text: "いいえ",
            click: function () {
                $(this).dialog("close");
                if (btnId != null && btnId != "") {
                    document.getElementById(btnId).click();
                }
            }
        },
        {
            text: "キャンセル",
            click: function () {
                $(this).dialog("close");
            }
        }],
        create: function () {
            var buttons = $('.ui-dialog-buttonset').children('button');
            buttons.removeClass("ui-button ui-corner-all ui-widget");
        },
        close: function () {
        },
        show: {}
    });

    $('[id$=alertMessage]').css({
        "textAlign": "center",
        "font-size": "13px",
        "overflow": "hidden",
        "text-overflow": "ellipsis",
        "vertical- align": "middle",
        "margin": "0.5rem 0px"
    });
    $('[id$=alertMessage]').html(message);
    myDialog.dialog('open');
}

/************************************************************/
/* function    : BtnTagClick                                */
/* description : テキストボックスにフォーカスする           */
/* parameter   : ctrl                                       */
/* return      : なし                                       */
/************************************************************/
function BtnTagClick(btnID) {
    var id = btnID.id;
    document.getElementById(id).click();
}

/***************************************************************/
/* function    : SentakuListClicked                              */
/* description : 選択リストをクリック                          */
/* parameter   : index                                         */
/* return      : なし                                          */
/***************************************************************/
function SentakuListClicked(elmnt, lblName, btnName) {
    var id = elmnt.id;
    var btnId = id.replace(lblName, btnName);
    var btnTokuisaki = document.getElementById(btnId);
    btnTokuisaki.click();
}

/************************************************************/
/* function    : ChildBtnClick                              */
/* description : iframe の中にあるボタンをクリック処理      */
/* parameter   : iframeID, btnID                            */
/* return      : なし                                       */
/************************************************************/
function ChildBtnClick(iframeID, btnID) {
    var my_frame = document.getElementById(iframeID);
    var oDoc = my_frame.contentWindow || my_frame.contentDocument;
    if (oDoc.document) {
        oDoc.document.getElementById(btnID).click();
    }
}

/*************************************************************/
/* function    : openPropertyPopup                           */
/* description : 付箋プロパティポップアップを開く            */
/* parameter   : div, btnId                                  */
/* return      : なし                                        */
/*************************************************************/
function openFusenPropertyPopup(obj, btnId) {

    var elmntID = $(event.target).prop("id");
    if (elmntID.indexOf("imgbtnFinish") == -1 && elmntID.indexOf("imgbtnFinishCancel") == -1 && elmntID.indexOf("imgbtnMenu") == -1 && elmntID.indexOf("btnDelete") == -1 && elmntID.indexOf("imgbtnColorCopyDelete") == -1) {
        var id = obj.id;
        var index = id.lastIndexOf("_");
        var btnOpenProperty = id.substring(0, index + 1);
        btnOpenProperty = btnOpenProperty + btnId;
        document.getElementById(btnOpenProperty).click();
    }
}

/************************************************************/
/* function    : CheckIsEnterKeyPress                       */
/* description : テキストボックスにEnterをチェックする      */
/* parameter   : ctrl                                       */
/* return      : bool                                       */
/************************************************************/
function CheckIsEnterKeyPress(ctrl) {
    if ((event.keyCode != 13)) {
        return true;
    }
    else {
        ctrl.focus();
        ctrl.select();
        return false;
    }
}

/************************************************************/
/* function    : CallTextChanged                            */
/* description : テキストを編集する処理を行う               */
/* parameter   : ctrl                                       */
/* return      : なし                                       */
/************************************************************/
function CallTextChangedEvent(ctrl, txtName, btnName) {
    if ((event.keyCode == 13)) {
        var txtId = ctrl.id;
        var btnId = txtId.replace(txtName, btnName);
        document.getElementById(btnId).click();
        ctrl.focus();
        ctrl.select();
    }
}

/************************************************************/
/* function    : CellClick                                  */
/* description : セルをクリック                             */
/* parameter   : ctrl                                       */
/* parameter   : btnId                                      */
/* return      : なし                                       */
/************************************************************/
function CellClick(cell, btnId, hdnDate) {
    var cellId = document.getElementById(cell);

    if (cellId.innerText != "") {
        document.getElementById(hdnDate).value = cellId.id;
        document.getElementById(btnId).click();
    }
}

/************************************************************/
/* function    : startUp                                    */
/* description : 付箋を移動する                             */
/* parameter   : なし                                       */
/* return      : なし                                       */
/************************************************************/
function startUp() {
    var isSameContainer = true;

    $(".UC02FusenInfoDiv").sortable({
        connectWith: ".UC02FusenInfoDiv",
        compareZIndex: true,
        start: function (event, ui) {
            var elmnt = ui.item[0];
            var draggedElmt = elmnt.id;
            document.getElementById("cphContent_hdnSourceId").value = elmnt.id;
        },
        receive: function (event, ui) {
            isSameContainer = false;
            var targetPanelId = event.target.id;
            document.getElementById("cphContent_hdnTargetId").value = event.target.id;
        },
        stop: function (event, ui) {
            var elmnt = ui.item[0];
            var elmntId = elmnt.id;
            if (isSameContainer) {
                var targetId = event.target.id;
                document.getElementById("cphContent_hdnTargetId").value = $(this).attr("id");
            }
            var hdnDragedElmtId = elmntId.replace("divFusenJouhou", "lblLabelId");
            // 付箋IDを取得する
            var dragedLabelId = $(elmnt).find("#" + hdnDragedElmtId).text();

            var hdnTantouId = elmntId.replace("divFusenJouhou", "lblTantouId");
            // 作成者IDを取得する
            var tantouId = $(elmnt).find("#" + hdnTantouId).text();

            var hdnSagyouBi = elmntId.replace("divFusenJouhou", "lblWorkDate");
            // 作業日を取得する
            var sagyouBi = $(elmnt).find("#" + hdnSagyouBi).text();

            var hdnNextSiblingELmtId = '';
            var nextSiblingBranchId = '';

            if (elmnt.nextSibling != null) {
                var nextElmnt = elmnt.nextSibling;
                var nextElmntId = nextElmnt.id;
                if (nextElmntId != null) {
                    if (nextElmntId.indexOf("divFusenJouhou") > -1) {
                        hdnNextSiblingELmtId = nextElmntId.replace("divFusenJouhou", "lblLabelId");
                        nextSiblingBranchId = $(nextElmnt).find("#" + hdnNextSiblingELmtId).text();

                    }
                }
                else {
                    if (nextElmnt.nextSibling != null) {
                        nextElmnt = nextElmnt.nextSibling;
                        var nextElmntId = nextElmnt.id;
                        if (nextElmntId != null) {
                            if (nextElmntId.indexOf("divFusenJouhou") > -1) {
                                hdnNextSiblingELmtId = nextElmntId.replace("divFusenJouhou", "lblLabelId");
                                nextSiblingBranchId = $(nextElmnt).find("#" + hdnNextSiblingELmtId).text();
                            }
                        }
                    }
                }
            }

            document.getElementById('cphContent_hdnFusenId').value = dragedLabelId;
            document.getElementById('cphContent_hdnTantouId').value = tantouId;
            document.getElementById('cphContent_hdnWorkDate').value = sagyouBi;
            document.getElementById('cphContent_hdnNextSiblingFusenId').value = nextSiblingBranchId;
            document.getElementById("cphContent_btnSavePosition").click();
        }
    });
}

/************************************************************/
/* function    : GridViewBtnClick                           */
/* description : 現在の画面のボタンクリック                 */
/* parameter   : id                                         */
/* return      : なし                                       */
/************************************************************/
function GridViewBtnClick(ctrl, lblName, btnName) {
    var lblId = ctrl.id;
    var btnId = lblId.replace(lblName, btnName);
    document.getElementById(btnId).click();
}

/************************************************************************/
/* function    : EnableScrollbar                                        */
/* description : スクロールバーを表示                                   */
/* parameter   : なし                                                   */
/* return      : なし                                                   */
/************************************************************************/
function EnableScrollbar() {
    $('body').css('overflow', 'auto');
}

/************************************************************************/
/* function    : DisableScrollbar                                       */
/* description : スクロールバーを非表示                                 */
/* parameter   : なし                                                   */
/* return      : なし                                                   */
/************************************************************************/
function DisableScrollbar() {
    $('body').css('overflow', 'hidden');
}


/************************************************************/
/* function    : getScrollPosition                          */
/* description : divのScrollPositionを取得する              */
/* parameter   : ctrlID, formType                           */
/* return      : なし                                       */
/************************************************************/
function getScrollPosition(ctrlID, formType) {
    if (formType == "MASTER") {
        ctrlID = "cphContent_" + ctrlID;
    }
    xPos = $get(ctrlID).scrollLeft;
    yPos = $get(ctrlID).scrollTop;
}

/************************************************************/
/* function    : setScrollPosition                          */
/* description : divのScrollPositionを設定する              */
/* parameter   : ctrlID, formType                           */
/* return      : なし                                       */
/************************************************************/
function setScrollPosition(ctrlID, formType) {
    if (formType == "MASTER") {
        ctrlID = "cphContent_" + ctrlID;
    }
    $get(ctrlID).scrollLeft = xPos;
    $get(ctrlID).scrollTop = yPos;
}

/************************************************************/
/* function    : resetScrollPosition                        */
/* description : divのScrollPositionをリセットする          */
/* parameter   : ctrlID, formType                           */
/* return      : なし                                       */
/************************************************************/
function resetScrollPosition(ctrlID, formType) {
    xPos = 0;
    yPos = 0;
    setScrollPosition(ctrlID, formType);
}

/************************************************************/
/* function    : getTantouBoardScrollPosition               */
/* description : 担当者のボードのScrollPositionを取得する   */
/* parameter   : なし                                       */
/* return      : なし                                       */
/************************************************************/
function getTantouBoardScrollPosition() {
    var divTantouFusen = document.getElementsByClassName("TantouBoardDiv");
    var i;
    xPosBoard = [], yPosBoard = [];
    for (i = 0; i < divTantouFusen.length; i++) {
        xPosBoard[i] = divTantouFusen[i].scrollLeft;
        yPosBoard[i] = divTantouFusen[i].scrollTop;
    }
}

/************************************************************/
/* function    : setTantouBoardScrollPosition               */
/* description : 作業者のボードのScrollPositionを設定する   */
/* parameter   : なし                                       */
/* return      : なし                                       */
/************************************************************/
function setTantouBoardScrollPosition() {
    var divFusen = document.getElementsByClassName("TantouBoardDiv");
    var i;
    if (xPosBoard != null && yPosBoard != null) {
        for (i = 0; i < divFusen.length; i++) {
            divFusen[i].scrollLeft = xPosBoard[i];
            divFusen[i].scrollTop = yPosBoard[i];
        }
    }
}

/****************************************************************/
/* function    : resetTantouBoardScrollPosition                 */
/* description : 担当者のボードのScrollPositionをリセットする   */
/* parameter   : なし                                           */
/* return      : なし                                           */
/****************************************************************/
function resetTantouBoardScrollPosition() {
    var divTantouFusen = document.getElementsByClassName("TantouBoardDiv");
    var i;
    xPosBoard = [], yPosBoard = [];
    for (i = 0; i < divTantouFusen.length; i++) {
        xPosBoard[i] = 0;
        yPosBoard[i] = 0;
    }
    setTantouBoardScrollPosition();
}

/************************************************************/
/* function    : getPendingBoardScrollPosition              */
/* description : 保留中のボードのScrollPositionを取得する   */
/* parameter   : なし                                       */
/* return      : なし                                       */
/************************************************************/
function getPendingBoardScrollPosition() {
    var divPendingFusen = document.getElementsByClassName("PendingBoardDiv");
    if (divPendingFusen.length > 0) {
        xPosPending = divPendingFusen[0].scrollLeft;
        yPosPending = divPendingFusen[0].scrollTop;
    }
}

/************************************************************/
/* function    : setPendingBoardScrollPosition              */
/* description : 保留中のボードのScrollPositionを設定する   */
/* parameter   : なし                                       */
/* return      : なし                                       */
/************************************************************/
function setPendingBoardScrollPosition() {
    var divPendingFusen = document.getElementsByClassName("PendingBoardDiv");
    if (divPendingFusen.length > 0) {
        divPendingFusen[0].scrollLeft = xPosPending;
        divPendingFusen[0].scrollTop = yPosPending;
    }
}

/****************************************************************/
/* function    : resetPendingBoardScrollPosition                */
/* description : 保留中のボードのScrollPositionをリセットする   */
/* parameter   : なし                                           */
/* return      : なし                                           */
/****************************************************************/
function resetPendingBoardScrollPosition() {
    xPosPending = 0;
    yPosPending = 0;
    setPendingBoardScrollPosition();
}

/************************************************************/
/* function    : getAllDivScrollPosition                    */
/* description : 全てのScrollPositionを設定する             */
/* parameter   : ctrlID, formType                           */
/* return      : なし                                       */
/************************************************************/
function getAllDivScrollPosition(ctrlID, formType) {
    getScrollPosition(ctrlID, formType);
    getPendingBoardScrollPosition();
    getTantouBoardScrollPosition();
}

/************************************************************/
/* function    : DeselectText                               */
/* description : テキスト選択解除                           */
/* parameter   : ctrl                                       */
/* return      : なし                                       */
/************************************************************/
function DeSelectText(ctrl) {
    if (event.keyCode == 9) {
        ctrl.setSelectionRange(ctrl.value.length, ctrl.value.length);
    }
}

/************************************************************/
/* function    : EscKeyPress                                */
/* description : EscKeyをクリックして画面を閉じる           */
/* parameter   : event                                      */
/* parameter   : event                                      */
/* return      : なし                                       */
/************************************************************/
function EscKeyPress(event, btnId) {
    if (event.keyCode == 27) {
        event.preventDefault();
        document.getElementById(btnId).click();
    }
}

/************************************************************/
/* function    : FileUploadBtnClick                         */
/* description : FileUploadを表示する                       */
/* parameter   : btnUpload                                  */
/* return      : なし                                       */
/************************************************************/
function FileUploadBtnClick(btnUpload) {
    $('#' + btnUpload).trigger('click');
}

/************************************************************/
/* function    : NewTab                                     */
/* description : 新しいタブを開く                           */
/* parameter   : value                                      */
/* return      : なし                                       */
/************************************************************/
function NewTab(value) {
    var htmlName = value + '.html';
    window.open(htmlName, "_blank");
}

function CheckDataChange() {
    var hdnEdit = document.getElementById("cphContent_hdnIsChange");
    if (hdnEdit.value == 'true') {
        event.preventDefault();
        document.getElementById("cphContent_btnAnkenReadCSV").click();
    }
}

function FileUploadSelect(btnId) {
    document.getElementById(btnId).click();
}

/************************************************************/
/* function    : GetCharacterCountLength                    */
/* description :                                            */
/* parameter   : ctrl, strMax, lblStrLength                 */
/* return      : なし                                       */
/************************************************************/
function GetCharacterCountLength(ctrl, strMax, lblRemainingMemoCount) {
    var result = 0;
    var str = ctrl.value;
    for (var i = 0; i < str.length; i++) {
        // を加算
        result += 1;
    }
    document.getElementById(lblRemainingMemoCount.id).innerText = "残り " + (strMax - result) + " 文字";
}

function OnlyNumericEntry() {
    if ((event.keyCode < 48 || event.keyCode > 57)) {
        event.returnValue = false;
    }
}

function DisableDateEntry() {
    event.returnValue = false;
}

/***************************JC28UriageList売上リスト画面_YG start*************************************/

/************************************************************/
/* function    : ShowMessage1                                */
/* description : 確認メッセージを表示する                   */
/* parameter   : メッセージ                                 */
/*             : 隠しボタンのID                             */
/* return      : なし                                       */
/************************************************************/
function ShowMessage1(msg, btnYes1, btnNo1, btnCancel1) {
    pdfConfirmBox(msg, btnYes1, btnNo1, btnCancel1);
    closeLoadingModal();
    return false;
}

/************************************************************/
/* function    : ShowRogoMessage                            */
/* description : 確認メッセージを表示する                   */
/* parameter   : メッセージ                                 */
/*             : 隠しボタンのID                             */
/* return      : なし                                       */
/************************************************************/
function ShowRogoMessage(msg, btnOK) {
    rogoConfirmBox(msg, btnOK);
    closeLoadingModal();
    return false;
}

/************************************************************/
/* function    : rogoConfirmBox                             */
/* description : 確認メッセージを表示する                   */
/* parameter   : メッセージ                                 */
/*             : 隠しボタンのID                             */
/* return      : なし                                       */
/************************************************************/

function rogoConfirmBox(message, btnOK) {
    var myDialog = $("<div id='msgDialogAlert'><img src='../Img/info_2.png' style='width: 30px;float:left;'/><p id='alertMessage' ></p></div>").dialog({
        autoOpen: false,
        modal: true,
        draggable: false,
        title: "見えるJOBZ",
        width: 380,
        resizable: false,
        closeOnEscape: true,
        dialogClass: "custom-dialog-1",
        buttons: [{
            text: "OK",
            "class": 'BlueBackgroundButton',
            width: 100,
            height: 30,
            click: function () {
                $(this).dialog("close");
                document.getElementById(btnOK).click();
            }
        }
        ],
        create: function () {
            var buttons = $('.ui-dialog-buttonset').children('button');
            buttons.removeClass("ui-button ui-corner-all ui-widget");
        },
        close: function () {
            $("#alertMessage").html("");
        },
        show: {}
    });

    $('[id$=alertMessage]').css({
        "textAlign": "left",
        "font-size": "13px",
        "overflow": "hidden",
        "text-overflow": "ellipsis",
        "vertical- align": "middle",
        "margin": "0.5rem 0px"
    });
    $('[id$=alertMessage]').html(message);
    myDialog.dialog('open');
}



/************************************************************/
/* function    : pdfConfirmBox                              */
/* description : 確認メッセージを表示する                   */
/* parameter   : メッセージ                                 */
/*             : 隠しボタンのID                             */
/* return      : なし                                       */
/************************************************************/

function pdfConfirmBox(message, btnYes1, btnNo1, btnCancel1) {
    var myDialog = $("<div id='msgDialogAlert'><img src='../Img/info_2.png' style='width: 30px;float:left;'/><p id='alertMessage' ></p></div>").dialog({
        autoOpen: false,
        modal: true,
        draggable: false,
        title: "見えるJOBZ",
        width: 380,
        resizable: false,
        closeOnEscape: true,
        dialogClass: "custom-dialog-1",
        buttons: [{
            text: "はい",
            "class": 'BlueBackgroundButton',
            width: 100,
            height: 30,
            click: function () {
                $(this).dialog("close");
                document.getElementById(btnYes1).click();
            }
        },
        {
            text: "いいえ",
            "class": 'BlueBackgroundButton',
            width: 100,
            height: 30,
            click: function () {
                $(this).dialog("close");
                document.getElementById(btnNo1).click();
            }
        },
        {
            text: "キャンセル",
            "class": 'JC09GrayButton',
            width: 100,
            height: 30,
            click: function () {
                $(this).dialog("close");
                document.getElementById(btnCancel1).click();
            }
        }],
        create: function () {
            var buttons = $('.ui-dialog-buttonset').children('button');
            buttons.removeClass("ui-button ui-corner-all ui-widget");
        },
        close: function () {
            $("#alertMessage").html("");
        },
        show: {}
    });

    $('[id$=alertMessage]').css({
        "textAlign": "left",
        "font-size": "13px",
        "overflow": "hidden",
        "text-overflow": "ellipsis",
        "vertical- align": "middle",
        "margin": "0.5rem 0px"
    });
    $('[id$=alertMessage]').html(message);
    myDialog.dialog('open');
}


/************************************************************/
/* function    : ShowMojiMessage                             */
/* description : 確認メッセージを表示する                   */
/* parameter   : メッセージ                                 */
/*             : 隠しボタンのID                             */
/* return      : なし                                       */
/************************************************************/
function ShowMojiMessage(msg, btnOK) {
    MojiMsgBox(msg, btnOK);
    closeLoadingModal();
    return false;
}

/************************************************************/
/* function    : MojiMsgBox                                */
/* description : 確認メッセージを表示する                   */
/* parameter   : メッセージ                                 */
/*             : 隠しボタンのID                             */
/* return      : なし                                       */
/************************************************************/

function MojiMsgBox(message, btnOk) {
    var myDialog = $("<div id='msgDialogAlert'><img src='../Img/info_2.png' style='width: 30px;float:left;'/><p id='alertMessage' ></p></div>").dialog({
        autoOpen: false,
        modal: true,
        draggable: false,
        title: "見えるJOBZ",
        width: 480,
        resizable: false,
        closeOnEscape: true,
        dialogClass: "custom-dialog-1",
        buttons: [
            {
                text: "OK",
                "class": 'BlueBackgroundButton',
                width: 100,
                height: 30,
                click: function () {
                    $(this).dialog("close");
                    document.getElementById(btnmojiOK).click();
                }
            }],
        create: function () {
            var buttons = $('.ui-dialog-buttonset').children('button');
            buttons.removeClass("ui-button ui-corner-all ui-widget");
        },
        close: function () {
            $("#alertMessage").html("");
        },
        show: {}
    });

    $('[id$=alertMessage]').css({
        "textAlign": "left",
        "font-size": "13px",
        "overflow": "hidden",
        "text-overflow": "ellipsis",
        "vertical- align": "middle",
        "margin": "0.5rem 0px"
    });
    $('[id$=alertMessage]').html(message);
    myDialog.dialog('open');
}
/***************************JC28UriageList売上リスト画面_YG end*************************************/


/***************************JC38大分類画面_Nan start************************************************/
/************************************************************/
/* function    : ShowDaibunruiNullConfirmMessage            */
/* description : 確認メッセージを表示する                   */
/* parameter   : メッセージ                                 */
/*             : 隠しボタンのID                             */
/* return      : なし                                       */
/************************************************************/
function ShowDaibunruiNullMessage(msg, btnYes, btnCancel) {
    DaibunruiConfirmBox(msg, btnYes, btnCancel);
    closeLoadingModal();
    return false;
}

/************************************************************/
/* function    : ShowDaibunruiConfirmBox                    */
/* description : 確認メッセージを表示する                   */
/* parameter   : メッセージ                                 */
/*             : 隠しボタンのID                             */
/* return      : なし                                       */
/************************************************************/
function DaibunruiConfirmBox(message, btnYes, btnCancel) {
    var myDialog = $("<div id='msgDialogAlert'><img src='../Img/info_2.png' style='width: 30px;float:left;'/><p id='alertMessage' ></p></div>").dialog({
        autoOpen: false,
        modal: true,
        draggable: false,
        title: "見えるJOBZ",
        width: 380,
        resizable: false,
        closeOnEscape: true,
        dialogClass: "custom-dialog-1",
        buttons: [{
            text: "はい",
            "class": 'BlueBackgroundButton',
            width: 100,
            height: 30,
            click: function () {
                $(this).dialog("close");
                document.getElementById(btnYes).click();
            }
        },
        {
            text: "キャンセル",
            "class": 'JC09GrayButton',
            width: 100,
            height: 30,
            click: function () {
                $(this).dialog("close");
                document.getElementById(btnCancel).click();
            }
        }],
        create: function () {
            var buttons = $('.ui-dialog-buttonset').children('button');
            buttons.removeClass("ui-button ui-corner-all ui-widget");
        },
        close: function () {
            $("#alertMessage").html("");
        },
        show: {}
    });

    $('[id$=alertMessage]').css({
        "textAlign": "left",
        "font-size": "13px",
        "overflow": "hidden",
        "text-overflow": "ellipsis",
        "vertical- align": "middle",
        "margin": "0.5rem 0px"
    });
    $('[id$=alertMessage]').html(message);
    myDialog.dialog('open');
}
/***************************JC38大分類画面_Nan end************************************************/