/************************************************************/
/* function    : focusHandler                               */
/* description : フォカースハンドラ                         */
/* parameter   : e                                          */
/* return      : なし                                       */
/************************************************************/
var lastFocusedControlId = "";

function focusHandler(e) {
    document.activeElement = e.originalTarget;
}

/************************************************************/
/* function    : appInit                                    */
/* description : アプリ初期時                               */
/* parameter   : なし                                       */
/* return      : なし                                       */
/************************************************************/
function appInit() {
    if (typeof(window.addEventListener) !== "undefined") {
        window.addEventListener("focus", focusHandler, true);
    }
    Sys.WebForms.PageRequestManager.getInstance().add_pageLoading(pageLoadingHandler);
    Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(pageLoadedHandler);
}

/************************************************************/
/* function    : pageLoadingHandler                         */
/* description : ページロードハンドラ                       */
/* parameter   : sender, args                               */
/* return      : なし                                       */
/************************************************************/
function pageLoadingHandler(sender, args) {
    lastFocusedControlId = typeof(document.activeElement) === "undefined" 
        ? "" : document.activeElement.id;
}

/************************************************************/
/* function    : focusControl                               */
/* description : フォカース制御                             */
/* parameter   : targetControl                              */
/* return      : なし                                       */
/************************************************************/
function focusControl(targetControl) {
    if (Sys.Browser.agent === Sys.Browser.InternetExplorer) {
        var focusTarget = targetControl;
        if (focusTarget && (typeof(focusTarget.contentEditable) !== "undefined")) {
            oldContentEditableSetting = focusTarget.contentEditable;
            focusTarget.contentEditable = false;
        }
        else {
            focusTarget = null;
        }
        targetControl.focus();
        if (focusTarget) {
            focusTarget.contentEditable = oldContentEditableSetting;
        }
    }
    else {
        targetControl.focus();
    }
}

/************************************************************/
/* function    : pageLoadedHandler                          */
/* description : ページロードハンドラ                       */
/* parameter   : sender, args                               */
/* return      : なし                                       */
/************************************************************/
function pageLoadedHandler(sender, args) {
    if (typeof(lastFocusedControlId) !== "undefined" && lastFocusedControlId != "") {
        var newFocused = $get(lastFocusedControlId);
        if (newFocused) {
            focusControl(newFocused);
        }
    }
}

Sys.Application.add_init(appInit);