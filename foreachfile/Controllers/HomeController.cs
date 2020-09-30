using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using foreachfile.Models;
using Microsoft.Extensions.FileProviders;
using System.Text;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace foreachfile.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IFileProvider _fileProvider;
        public HomeController(ILogger<HomeController> logger, IFileProvider fileProvider)
        {
            _logger = logger;
            _fileProvider = fileProvider;
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
        public IActionResult getFilesIFileProvider()
        {
            var contents = _fileProvider.GetDirectoryContents("/MyFiles");
            StringBuilder jsonResult = new StringBuilder();
            jsonResult.Append(@"[");
            int no = 1;
            foreach (var item in contents)
            {
                jsonResult.Append(@"{'id':" + (no) + ",'filename':'" + item.Name + "'},");
                no++;
            }
            jsonResult.Remove(jsonResult.Length - 1, 1);
            jsonResult.Append(@"]");
            string str = jsonResult.ToString();
            return Json(str);
        }

    }
    public class fileInfo { 
        public int id { get; set; }
        public int filename { get; set; }
    }
}
