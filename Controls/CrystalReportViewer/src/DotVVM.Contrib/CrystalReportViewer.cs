using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using DotVVM.Framework.Binding;
using DotVVM.Framework.Controls;
using DotVVM.Framework.Hosting;

namespace DotVVM.Contrib
{
    /// <summary>
    /// Renders a page inside iframe which should contain a .aspx page with CrystalReportViewer control inside
    /// </summary>
    public class CrystalReportViewer : HtmlGenericControl
    {
        public CrystalReportViewer() : base("div")
        {
        }

        /// <summary>
        /// Route to the WebForm page which contains the CrystalReportViewer control
        /// </summary>
        [MarkupOptions(AllowBinding = false)]
        public string ReportPageUrl
        {
            get { return (string)GetValue(ReportPageUrlProperty); }
            set { SetValue(ReportPageUrlProperty, value); }
        }

        public static readonly DotvvmProperty ReportPageUrlProperty
            = DotvvmProperty.Register<string, CrystalReportViewer>(c => c.ReportPageUrl, "CrystalReportViewerPage.aspx");

        /// <summary>
        /// Toggles the toolbar on and off.
        /// </summary>
        [MarkupOptions(AllowBinding = false)]
        public bool DisplayToolbar
        {
            get { return (bool)GetValue(DisplayToolbarProperty); }
            set { SetValue(DisplayToolbarProperty, value); }
        }

        public static readonly DotvvmProperty DisplayToolbarProperty
            = DotvvmProperty.Register<bool, CrystalReportViewer>(c => c.DisplayToolbar, true);

        /// <summary>
        /// Toggles the page on and off.
        /// </summary>
        [MarkupOptions(AllowBinding = false)]
        public bool DisplayPage
        {
            get { return (bool)GetValue(DisplayPageProperty); }
            set { SetValue(DisplayPageProperty, value); }
        }

        public static readonly DotvvmProperty DisplayPageProperty
            = DotvvmProperty.Register<bool, CrystalReportViewer>(c => c.DisplayPage, true);

        /// <summary>
        /// Toggles the statusbar on and off.
        /// </summary>
        [MarkupOptions(AllowBinding = false)]
        public bool DisplayStatusbar
        {
            get { return (bool)GetValue(DisplayStatusbarProperty); }
            set { SetValue(DisplayStatusbarProperty, value); }
        }

        public static readonly DotvvmProperty DisplayStatusbarProperty
            = DotvvmProperty.Register<bool, CrystalReportViewer>(c => c.DisplayStatusbar, true);

        /// <summary>
        /// Contains url to a css file.
        /// </summary>
        [MarkupOptions(AllowBinding = false)]
        public string ExtraCssFileUrl
        {
            get { return (string)GetValue(ExtraCssFileUrlProperty); }
            set { SetValue(ExtraCssFileUrlProperty, value); }
        }

        public static readonly DotvvmProperty ExtraCssFileUrlProperty
            = DotvvmProperty.Register<string, CrystalReportViewer>(c => c.ExtraCssFileUrl);

        /// <summary>
        /// Toggles the BestFitPage property of the CrystalReportViewer WebForm control
        /// </summary>
        [MarkupOptions(AllowBinding = false)]
        public bool BestFitPage
        {
            get { return (bool)GetValue(BestFitPageProperty); }
            set { SetValue(BestFitPageProperty, value); }
        }

        public static readonly DotvvmProperty BestFitPageProperty
            = DotvvmProperty.Register<bool, CrystalReportViewer>(c => c.BestFitPage, true);

        /// <summary>
        /// Sets the width of the CrystalReportViewer control when BestFitPage is set to false.
        /// </summary>
        [MarkupOptions(AllowBinding = false)]
        public string Width
        {
            get { return (string)GetValue(WidthProperty); }
            set { SetValue(WidthProperty, value); }
        }

        public static readonly DotvvmProperty WidthProperty
            = DotvvmProperty.Register<string, CrystalReportViewer>(c => c.Width);

        /// <summary>
        /// Sets the height of the CrystalReportViewer control when BestFitPage is set to false.
        /// </summary>
        [MarkupOptions(AllowBinding = false)]
        public string Height
        {
            get { return (string)GetValue(HeightProperty); }
            set { SetValue(HeightProperty, value); }
        }

        public static readonly DotvvmProperty HeightProperty
            = DotvvmProperty.Register<string, CrystalReportViewer>(c => c.Height);


        /// <summary>
        /// Specifies the path to the Crystal Report file.
        /// </summary>
        [MarkupOptions(Required = true)]
        public string CrystalReportFile
        {
            get { return (string)GetValue(CrystalReportFileProperty); }
            set { SetValue(CrystalReportFileProperty, value); }
        }

        public static readonly DotvvmProperty CrystalReportFileProperty
            = DotvvmProperty.Register<string, CrystalReportViewer>(c => c.CrystalReportFile);

        protected override void AddAttributesToRender(IHtmlWriter writer, IDotvvmRequestContext context)
        {
            ExtraCssFileUrlProperty.IsSet(this);

            writer.AddAttribute("class", "dotvvm-crystal-report-viewer-container");
            writer.AddAttribute("report-page-url", WebUtility.HtmlEncode(ReportPageUrl));
            writer.AddAttribute("display-toolbar", DisplayToolbar.ToString());
            writer.AddAttribute("display-page", DisplayPage.ToString());
            writer.AddAttribute("display-statusbar", DisplayStatusbar.ToString());
            writer.AddAttribute("best-fit-page", BestFitPage.ToString());
            if (ExtraCssFileUrlProperty.IsSet(this))
            {
                writer.AddAttribute("extra-css-file-url", WebUtility.HtmlEncode(ExtraCssFileUrl));
            }
            if (WidthProperty.IsSet(this))
            {
                writer.AddAttribute("width", Width);
            }
            if (HeightProperty.IsSet(this))
            {
                writer.AddAttribute("height", Height);
            }

            writer.AddKnockoutDataBind("crystalReportFile", this, CrystalReportFileProperty, () =>
            {
                writer.AddKnockoutDataBind("crystalReportFile", KnockoutHelper.MakeStringLiteral(CrystalReportFile));
            });

            base.AddAttributesToRender(writer, context);
        }

        protected override void OnInit(IDotvvmRequestContext context)
        {
            var iframe = new HtmlGenericControl("iframe");
            iframe.Attributes.Add("class", "dotvvm-crystal-report-viewer");

            Children.Add(iframe);

            base.OnInit(context);
        }
        protected override void OnPreRender(IDotvvmRequestContext context)
        {
            context.ResourceManager.AddRequiredResource("dotvvm.contrib.CrystalReportViewer");

            base.OnPreRender(context);
        }
    }
}
