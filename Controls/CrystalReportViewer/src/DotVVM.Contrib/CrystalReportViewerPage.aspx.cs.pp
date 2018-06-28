using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Security;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CrystalDecisions.CrystalReports.Engine;

namespace $rootnamespace$
{
    public partial class CrystalReportViewerPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var crystalReport = new ReportDocument();

            // make sure that the report is available with a problem
            var crystalReportFile = Request.QueryString["CrystalReportFile"];
            var fullPath = Path.Combine(Request.PhysicalApplicationPath, crystalReportFile);
            if (!fullPath.EndsWith(".rpt", StringComparison.OrdinalIgnoreCase) || !fullPath.StartsWith(Request.PhysicalApplicationPath, StringComparison.OrdinalIgnoreCase))
            {
                throw new SecurityException("Cannot load report files with different extension than RPT, or files from outside of application directory!");
            }

            SetCotrolProperties();

            crystalReport.Load(Server.MapPath(crystalReportFile));

			// TODO: set data source for the report
            DataTable table = null;
            crystalReport.SetDataSource(table);
			
            CrystalReportViewer1.ReportSource = crystalReport;
        }

        private void SetCotrolProperties()
        {
            if (Request.QueryString["DisplayToolbar"] != null)
            {
                CrystalReportViewer1.DisplayToolbar = Convert.ToBoolean(Request.QueryString["DisplayToolbar"]);
            }

            if (Request.QueryString["DisplayStatusbar"] != null)
            {
                CrystalReportViewer1.DisplayStatusbar = Convert.ToBoolean(Request.QueryString["DisplayStatusbar"]);
            }

            if (Request.QueryString["DisplayPage"] != null)
            {
                CrystalReportViewer1.DisplayPage = Convert.ToBoolean(Request.QueryString["DisplayPage"]);
            }

            if (Request.QueryString["BestFitPage"] != null)
            {
                CrystalReportViewer1.BestFitPage = Convert.ToBoolean(Request.QueryString["BestFitPage"]);
            }

            if (Request.QueryString["ExtraCssFileUrl"] != null)
            {
                CrystalReportViewer1.ExtraCssFileUrl = WebUtility.HtmlDecode(Request.QueryString["ExtraCssFileUrl"]);
            }
            if (Request.QueryString["Width"] != null)
            {
                CrystalReportViewer1.Width = Unit.Parse(Request.QueryString["Width"]);
            }
            if (Request.QueryString["Height"] != null)
            {
                CrystalReportViewer1.Height = Unit.Parse(Request.QueryString["Height"]);
            }
        }
    }
}