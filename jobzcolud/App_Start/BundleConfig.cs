using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;
using System.Web.UI;

namespace jobzcolud
{
    public class BundleConfig
    {
        // バンドルの詳細については、https://go.microsoft.com/fwlink/?LinkID=303951 を参照してください
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/WebFormsJs").Include(
                            "~/Scripts/WebForms/WebForms.js",
                            "~/Scripts/WebForms/WebUIValidation.js",
                            "~/Scripts/WebForms/MenuStandards.js",
                            "~/Scripts/WebForms/Focus.js",
                            "~/Scripts/WebForms/GridView.js",
                            "~/Scripts/WebForms/DetailsView.js",
                            "~/Scripts/WebForms/TreeView.js",
                            "~/Scripts/WebForms/WebParts.js"));

            // これらのファイルには明示的な依存関係があり、ファイルが動作するためには順序が重要です
            bundles.Add(new ScriptBundle("~/bundles/MsAjaxJs").Include(
                    "~/Scripts/WebForms/MsAjax/MicrosoftAjax.js",
                    "~/Scripts/WebForms/MsAjax/MicrosoftAjaxApplicationServices.js",
                    "~/Scripts/WebForms/MsAjax/MicrosoftAjaxTimer.js",
                    "~/Scripts/WebForms/MsAjax/MicrosoftAjaxWebForms.js"));

            // 開発に使用し、情報源である開発バージョンの Modernizr を使用します。続いて、
            // 運用の準備が完了したら、https://modernizr.com のビルド ツールを使用し、必要なテストのみを選びます
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                            "~/Scripts/modernizr-*"));

            // script for all pages
            bundles.Add(new ScriptBundle("~/scripts/ScriptBundle1").Include(
                            "~/Scripts/Common/Common.js",
                            "~/Scripts/umd/popper.min.js"));

            // style for popup
            bundles.Add(new StyleBundle("~/style/StyleBundle1").Include(
                            "~/Style/CommonStyle.css",
                            "~/Style/StyleJC.css",
                            "~/Content1/themes/base/jquery-ui.css"));

            // style for master based page
            bundles.Add(new StyleBundle("~/style/StyleBundle2").Include(
                           "~/Style/CommonStyle.css",
                           "~/Style/MainPageStyle.css",
                           "~/Content1/themes/base/jquery-ui.css"));

            bundles.Add(new ScriptBundle("~/scripts/ScriptZip2addrBundle").Include(
                           "~/Scripts/Common/jquery.zip2addr.js"));

            bundles.Add(new ScriptBundle("~/scripts/ScriptDateRangeBundle").Include(
                           "~/Scripts/Common/moment.min.js",
                           "~/Scripts/Common/jquery.daterangepicker.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                 "~/Scripts/jquery-ui-{version}.js",
                 "~/Scripts/jquery.ui.touch-punch.js"));

            // style for UserControl
            bundles.Add(new StyleBundle("~/style/UCStyleBundle").Include(
                            "~/Style/UCStyle.css"));

            // style for 期間を選択画面
            bundles.Add(new StyleBundle("~/style/DateRangeStyleBundle").Include(
                            "~/Style/daterangepicker.min.css"));

            // style for acc create
            bundles.Add(new StyleBundle("~/style/AccCreate").Include(
                            "~/Style/CommonStyle.css",
                            "~/Style/StyleJC.css"));            

            BundleTable.EnableOptimizations = true;
        }
    }
}