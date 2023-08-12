using IronPdfTest.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using IronPdf;
using IronPdf.Rendering;
using Microsoft.AspNetCore.Http;

namespace IronPdfTest.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public async Task<ActionResult> DownloadPdf()
        {
            IronPdf.Installation.TempFolderPath = $@"C:/irontemp/";

            var html = "<html><head></head><body><div style=\"white-space:nowrap\">Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.</div></body></html>";
            var renderer = new IronPdf.ChromePdfRenderer();

            renderer.RenderingOptions.HtmlHeader = new HtmlHeaderFooter()
            {
                HtmlFragment = @$"<div style=""white-space:nowrap"">Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.</div>"
            };
            renderer.RenderingOptions.HtmlFooter = new HtmlHeaderFooter()
            {
                HtmlFragment = @$"<div style=""white-space:nowrap"">Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua.</div>"
            };
            renderer.RenderingOptions.CustomCssUrl = "";
            renderer.RenderingOptions.CssMediaType = PdfCssMediaType.Print;
            renderer.RenderingOptions.HtmlHeader.LoadStylesAndCSSFromMainHtmlDocument = false;
            renderer.RenderingOptions.EnableJavaScript = false;
            renderer.RenderingOptions.UseMarginsOnHeaderAndFooter = UseMargins.None;


            using var pdfdoc = renderer.RenderHtmlAsPdf(html, "https://localhost:7212/");
            return File(pdfdoc.Stream.ToArray(), "application/pdf", "test.pdf");
        }
    }
}