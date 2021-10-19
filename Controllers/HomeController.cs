using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace AudioMvc.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public string Index(HttpPostedFileBase audio)
        {
            // Verify that the user selected a file
            if (audio != null && audio.ContentLength > 0)
            {
                // extract only the filename
                var fileName = Path.GetFileName(audio.FileName);
                string fileName1 = DateTime.Now.Ticks.ToString();
                fileName = fileName1 + ".mp3";
                var path = Path.Combine(Server.MapPath("~/UploadedFiles"), fileName);
                audio.SaveAs(path);
            }

            return audio.FileName;
        }
        [HttpGet]
        public List<FileContentResult> About()
        {
            ViewBag.Message = "Your application description page.";
            //Fetch all files in the Folder (Directory).
            string[] filePaths = Directory.GetFiles(Server.MapPath("~/UploadedFiles/"));

            //Copy File names to Model collection.
            List<FileContentResult> files = new List<FileContentResult>();
            foreach (string filePath in filePaths)
            {
                byte[] bytes = System.IO.File.ReadAllBytes(filePath);
              FileContentResult f=   File(bytes, "application/octet-stream", filePath);
                files.Add(f);
                
            }
          
            return View(files);
            
        }
        public FileResult PlayAudio(string Name)
        {
            //Build the File Path.
            string path = Server.MapPath("~/UploadedFiles/") + Name;

            //Read the File data into Byte Array.
            byte[] bytes = System.IO.File.ReadAllBytes(path);

            //Send the File to Download.
           return  File(bytes, "application/octet-stream", Name);
        }
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
           
            //FileStream fs = new FileStream("~/uploadedFiles", );
            return View();
        }
    }
   
}