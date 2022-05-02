using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using PeterEngland2.Data;
using PeterEngland2.Models;
using PeterEngland2.ModelView;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace PeterEngland2.Controllers
{
    public class InventryController : Controller
    {
        private readonly ApplicationDbContext context;

        private readonly IWebHostEnvironment webHostEnvironment;

        public InventryController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            this.context = context;
            this.webHostEnvironment = webHostEnvironment;
        }
        public IActionResult AllProductList()
        { 
             var prod = context.Inventry.ToList();
            var cat = context.Category.ToList();
            //  List<Product> products = new List<Product>();
            //  Product product = new Product();
            foreach(Inventry item in prod) 
            {
                foreach (Category c in cat)
                {
                    if (c.CategoryId == item.CategoryId)
                    {
                        item.CategoryName = c.CategoryName;
                    }
                }
            }
            //  {

            //       product.ProductId = item.ProductId;
            //      product.ProductName = item.ProductName;
            //      product.CategoryName = cat.CategoryName;
            //      product.ProductPrice = item.ProductPrice;
            //      products.Add(product);
            //  }

            var products = context.Inventry.ToList();
            ViewBag.Products = products;
            return View();
        }

        [HttpGet]
        public IActionResult Create() 
        {
            var cat = context.Category.ToList();
            ViewBag.Category = new SelectList(cat, "CategoryId", "CategoryName");
            //ViewBag["Category"] = new SelectList(cat);
            return View();
        }
        [HttpPost]
        public IActionResult Create(IFormFile file, [Bind("ProductId,ProductName,CategoryId,ProductPrice,ProductImageUrl") ]Inventry inventry) 
        {

            if (file != null)
            {
                string filename = file.FileName;
                string path = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images"));
                using (var filestream = new FileStream(Path.Combine(path, filename), FileMode.Create))
                {
                    file.CopyTo(filestream);
                   
                }

                inventry.ProductImageUrl = filename;
            }


            context.Inventry.Add(inventry);
                int id = context.SaveChanges();
            if (id == 1)
            {
               ViewBag.Message = "< script > alert('Successfull Added!') </ script > ";
                return RedirectToAction("AllProductList", "Inventry");
            }
            else
            {
                ViewBag.Message = "< script > alert('Failed!') </ script >";
                return View();
            }
        }
        [HttpGet]
        public IActionResult Edit(int ProductId)
        {
            var prod = context.Inventry.Where(p => p.ProductId == ProductId).SingleOrDefault();   
            var cat = context.Category.ToList();
            
          //  var cate = context.Category.Where(c => c.CategoryId == prod.CategoryId);
            ViewBag.Category = new SelectList(cat, "CategoryId", "CategoryName");
            return View(prod);
        }
        [HttpPost]
        public IActionResult Edit(IFormFile file, [Bind("ProductId,ProductName,CategoryId,ProductPrice,ProductImageUrl")] Inventry inventry) 
        {
            if (file != null)
            {
                string filename = file.FileName;
                string path = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\images"));
                using (var filestream = new FileStream(Path.Combine(path, filename), FileMode.Create))
                {
                    file.CopyTo(filestream);

                }

                inventry.ProductImageUrl = filename;
            }

            var prod = context.Inventry.Where(p => p.ProductId == inventry.ProductId).SingleOrDefault();
            if (prod != null) 
            {
                prod.ProductName =inventry.ProductName;
                prod.ProductPrice =inventry.ProductPrice;
                prod.CategoryId = inventry.CategoryId;
                prod.ProductImageUrl = inventry.ProductImageUrl;
                
                context.Update(prod);
                int res = context.SaveChanges();
                if(res == 1) 
                {
                    ViewBag.UpdateMessage = "< script > alert('Updated!') </ script >";
                    return RedirectToAction("AllProductList", "Inventry");
                }
                else
                {
                    ViewBag.UpdateMessage = "< script > alert('Failed!') </ script >";
                    return RedirectToAction("AllProductList", "Inventry");
                }
            }
            else 
            {
                ViewBag.UpdateMessage = "< script > alert('Not FOund!') </ script >";
                return RedirectToAction("AllProductList", "Inventry");
            }
        }
        
        [HttpGet]

        public IActionResult Delete(int ProductId)
        {
            var result = context.Inventry.Where(p => p.ProductId == ProductId).SingleOrDefault();
            var cat =  context.Category.Where(c => c.CategoryId == result.CategoryId).SingleOrDefault();
            
            result.CategoryName = cat.CategoryName;

            return View(result);
        }

        [HttpPost]
        [ActionName("Delete")]
        public IActionResult DeleteConfirm(int ProductId)
        {
            var prod = context.Inventry.Where(p => p.ProductId == ProductId).SingleOrDefault();
            if (prod != null)
            {
                context.Remove(prod);
                int res = context.SaveChanges();
                if (res == 1)
                {
                    ViewBag.DeleteMessage = "< script > alert('Deleted!') </ script >";
                    return RedirectToAction("AllProductList", "Inventry");
                }
                else
                {
                    ViewBag.DeleteMessage = "< script > alert('Failed!') </ script >";
                    return RedirectToAction("AllProductList", "Inventry");
                }

            }
            else
            {
                ViewBag.DeleteMessage = "< script > alert('Not Found!') </ script >";
                return RedirectToAction("AllProductList", "Inventry");
            }
        }

        [HttpGet]
        public IActionResult Logout() 
        {
            return RedirectToAction("Index", "Home");
        }

        public IActionResult AllProductList2()
        {
            var prod = context.Inventry.ToList();
            var cat = context.Category.ToList();
            //  List<Product> products = new List<Product>();
            //  Product product = new Product();
            foreach (Inventry item in prod)
            {
                foreach (Category c in cat)
                {
                    if (c.CategoryId == item.CategoryId)
                    {
                        item.CategoryName = c.CategoryName;
                    }
                }
            }
            //  {

            //       product.ProductId = item.ProductId;
            //      product.ProductName = item.ProductName;
            //      product.CategoryName = cat.CategoryName;
            //      product.ProductPrice = item.ProductPrice;
            //      products.Add(product);
            //  }

            var products = context.Inventry.ToList();
            ViewBag.Products = products;
            return View();
        }

       

        private string UploadFile(InventryModel im)
        {
            string FileName = null;
            if(im.ProductImageUrl != null) 
            {
                string uploadDir = Path.Combine(webHostEnvironment.WebRootPath, "images");
                FileName = Guid.NewGuid().ToString() + "-" + im.ProductImageUrl.FileName;
                string FilePath = Path.Combine(uploadDir, FileName);
                using (var fileStream = new FileStream(FilePath,FileMode.Create))
                {
                    im.ProductImageUrl.CopyTo(fileStream);
                }
            }
            return FileName;

        }
    }
}
