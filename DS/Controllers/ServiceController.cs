using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DS.Models;
using System.IO;
using DS.Lib;
using DS.POCO;
using System.Data.Entity;

namespace DS.Controllers
{
    public class ServiceController : Controller
    {
        DsDbContext context = new DsDbContext();
        public ActionResult Index()
        {
           var services = from s in context.services.OrderBy(x => x.ServiceId).Skip(0).Take(10) select s;
            ViewBag.TotalService = context.services.Count();

            return View(services);
        }
        public ActionResult Edit(int Id)
        {
            Service aService = new Service();
            aService = context.services.Find(Id);
            return View(aService);
        }
        [HttpPost]
        public ActionResult Edit(Service aService, HttpPostedFileBase file)
        {
            if (file!=null)
            {
                var fileName = Path.GetFileName(file.FileName);
                ImageUpload imageUploadOriginal = new ImageUpload { Image_Prepend = "org_" };

                // rename, resize, and upload 
                //return object that contains {bool Success,string ErrorMessage,string ImageName}
                ImageResult imageResultOriginal = imageUploadOriginal.RenameUploadFile(file);

                //thumb Image
                ImageUpload imageUploadThumb = new ImageUpload { Width = 150, Height = 100, Image_Prepend = "thumb_" };

                // rename, resize, and upload 
                //return object that contains {bool Success,string ErrorMessage,string ImageName}
                ImageResult imageResultThumb = imageUploadThumb.RenameUploadFile(file);

                //Image Mid
                ImageUpload imageUploadMid = new ImageUpload { Height = 400, Width = 600, Image_Prepend = "mid_" };

                // rename, resize, and upload 
                //return object that contains {bool Success,string ErrorMessage,string ImageName}
                ImageResult imageResultMid = imageUploadMid.RenameUploadFile(file);
                aService.ImageOriginal = imageResultOriginal.ImageName;
                aService.ImageThumb = imageResultThumb.ImageName;
                aService.ImageMid = imageResultMid.ImageName;
            }
            context.services.Attach(aService);
            context.Entry(aService).State = EntityState.Modified;
            context.SaveChanges();
            return RedirectToAction("Index", context.services.ToList());
        }

        public ActionResult Search(int id)
        {
            var services = from s in context.services where s.ServiceId.Equals(id) select s;
            return RedirectToAction("Index", "Service", services);
        }
        public ActionResult Delete(int id)
        {
            context.services.Remove(context.services.Find(id));
            context.SaveChanges();
            var LeftServices = context.services.ToList();
            return RedirectToAction("index", LeftServices);
        }
        public ActionResult Details(int id)
        {
            Service aService = new Service();
            aService = context.services.Find(id);
            return View(aService);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Service aService, FormCollection formCollection)
        {
            foreach (string item in Request.Files)
            {
                HttpPostedFileBase file = Request.Files[item] as HttpPostedFileBase;
                if (file.ContentLength == 0)
                    continue;

                if (file.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(file.FileName);
                    // width + height will force size, care for distortion
                    //Exmaple: ImageUpload imageUpload = new ImageUpload { Width = 800, Height = 700 };

                    // height will increase the width proportially 
                    //Example: ImageUpload imageUpload = new ImageUpload { Height= 600 };

                    // width will increase the height proportially 
                    //Original Image
                    ImageUpload imageUploadOriginal = new ImageUpload { Image_Prepend = "org_"};

                    // rename, resize, and upload 
                    //return object that contains {bool Success,string ErrorMessage,string ImageName}
                    ImageResult imageResultOriginal = imageUploadOriginal.RenameUploadFile(file);

                    //thumb Image
                    ImageUpload imageUploadThumb = new ImageUpload { Width = 150, Height=100 ,Image_Prepend="thumb_" };

                    // rename, resize, and upload 
                    //return object that contains {bool Success,string ErrorMessage,string ImageName}
                    ImageResult imageResultThumb = imageUploadThumb.RenameUploadFile(file);

                    //Image Mid
                    ImageUpload imageUploadMid = new ImageUpload { Height = 400, Width=600, Image_Prepend="mid_" };

                    // rename, resize, and upload 
                    //return object that contains {bool Success,string ErrorMessage,string ImageName}
                    ImageResult imageResultMid = imageUploadMid.RenameUploadFile(file);

                    if (imageResultOriginal.Success && imageResultMid.Success && imageResultThumb.Success)
                    {
                        //TODO: write the filename to the db
                        Console.WriteLine(imageResultOriginal.ImageName);
                        aService.ImageOriginal = imageResultOriginal.ImageName;
                        aService.ImageThumb = imageResultThumb.ImageName;
                        aService.ImageMid = imageResultMid.ImageName;
                        if (aService.Detail.Length > 150)
                        {
                            aService.DisplayDetail = aService.Detail.Substring(0, 150);
                        }
                        context.services.Add(aService);
                        context.SaveChanges(); 
                    }
                    else
                    {
                        //TODO: show view error
                        // use imageResult.ErrorMessage to show the error
                        ViewBag.Error = imageResultOriginal.ErrorMessage;
                    }
                }
                
            }

            return View();
        }
        public ActionResult Pagination(int id)
        {
            var services = from X in context.services.OrderBy(s =>s.ServiceId).Skip(10*(id-1)).Take(10)
            select X;
            ViewBag.TotalService = context.services.Count();
            return View("Index",services);
        }
	}
}