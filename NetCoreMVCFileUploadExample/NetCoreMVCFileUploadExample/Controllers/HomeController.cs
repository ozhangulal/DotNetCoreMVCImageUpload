using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NetCoreMVCFileUploadExample.Models;

namespace NetCoreMVCFileUploadExample.Controllers
{
    public class HomeController : Controller
    {
        IHostingEnvironment _env;
        string directory;
        Random rnd = new Random();
        public HomeController(IHostingEnvironment env)
        {
            _env = env;
            directory = _env.ContentRootPath + "/Images/";
            
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult SingleFile(IFormFile file)
        {
          
            using (var fileStream = new FileStream(Path.Combine(directory,rnd.Next(0,99999).ToString()+".png"),FileMode.Create,FileAccess.Write))
            {
                file.CopyTo(fileStream);
            }
                return RedirectToAction("Index");
        }

        public IActionResult MultipleFiles(IEnumerable<IFormFile> files)
        {
            foreach (var file in files)
            {
                using (var fileStream = new FileStream(Path.Combine(directory, rnd.Next(0, 99999).ToString() + ".png"), FileMode.Create, FileAccess.Write))
                {
                    file.CopyTo(fileStream);
                }
            }
            return RedirectToAction("Index");
        }

    }
}
