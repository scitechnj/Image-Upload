using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ImagesProcessing;

namespace ImageUploading.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult PostImage()
        {
            HttpPostedFileBase file = Request.Files["myImage"];
            file.SaveAs(Server.MapPath("~/Content/UploadedImages/" + file.FileName));
            
            ThumbnailCreator.CreateThumbnail(Server.MapPath("~/Content/UploadedImages/" + file.FileName),
               Server.MapPath("~/Content/UploadedImages/" + "Thumb_" + file.FileName), 150);

            return RedirectToAction("ShowFiles");
        }

        public ActionResult ShowFiles()
        {
            DirectoryInfo directoryInfo =
                new DirectoryInfo(Server.MapPath("~/Content/UploadedImages/"));
            FileInfo[] allFiles = directoryInfo.GetFiles();
            IEnumerable<FileInfo> nonThumbnails = allFiles.Where(f => !f.Name.StartsWith("Thumb"));

            var viewModel = new ContentFileInfoViewModel();
            var list = new List<ContentFileInfo>();
            foreach (FileInfo file in nonThumbnails)
            {
                var info = new ContentFileInfo();
                info.Name = file.Name;
                info.CreatedDate = file.CreationTime;
                info.Size = (int)file.Length;
                info.Extension = file.Extension;
                list.Add(info);
            }

            viewModel.Files = list;

            return View(viewModel);
        }
    }

    public class ContentFileInfo
    {
        public string Name { get; set; }
        public int Size { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Extension { get; set; }
    }

    public class ContentFileInfoViewModel
    {
        public IEnumerable<ContentFileInfo> Files { get; set; }
    }
}
