using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PeterEngland2.Data;
using PeterEngland2.Models;
using System.Collections.Generic;
using System.Linq;

namespace PeterEngland2.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ApplicationDbContext context;
        public static Ordered od;
        List<Ordered> li = new List<Ordered>();
        public CustomerController(ApplicationDbContext context)
        {
            this.context = context;
        }
        public IActionResult Index()
        {
            var l = context.Inventry.ToList();
            var name = HttpContext.Session.GetString("Name");
            ViewBag.Message = "Hello "+name;
            ViewBag.Products = l;
            return View(l);
        }
        [HttpGet]
        public IActionResult Order(int ProductId) 
         {
              var p = context.Inventry.Where(p=> p.ProductId==ProductId).SingleOrDefault();
            return View(p);
        }
        [HttpPost]
        public IActionResult Order(int qty,int id) 
        {
            
                var prod = context.Inventry.Where(p => p.ProductId == id).SingleOrDefault();
            Ordered od = new Ordered();
            if (prod != null)
            {

                od.ProductName = prod.ProductName;
                od.ProductId = prod.ProductId;
                od.ProductQuntity = qty;
                od.ProductPrice = prod.ProductPrice;
                od.ProductTotalBill = od.ProductQuntity * od.ProductPrice;
                od.ProductImageUrl = prod.ProductImageUrl;
                // ViewBag.Order=od;

             
                HttpContext.Session.SetString("data", JsonConvert.SerializeObject(od));
              
                return RedirectToAction("Cart");
            }
            return RedirectToAction("Cart");
            //return View();
        }

        [HttpGet]
        public IActionResult Cart() 
        {
            var  data = HttpContext.Session.GetString("data");
           Ordered o= JsonConvert.DeserializeObject<Ordered>(data);
            ViewBag.obj = o;
            return View(o);
        }
          [HttpPost]
          public IActionResult Cart(Ordered ordered) 
        {
            ordered.UserId= (int)HttpContext.Session.GetInt32("UserId");
            context.Ordered.Add(ordered);
            int r = context.SaveChanges();

            if (r == 1) 
            {
                HttpContext.Session.SetString("msg","<script> alert('Order Placed!') </script>"); 
                return RedirectToAction("Index");
            }
            else
            {
                HttpContext.Session.SetString ("msg","<script> alert('Failed to placed!') </script>");
                return View();
            }
            
        }
        [HttpGet]
        public IActionResult ViewOrder() 
           {
            //int id = (int)HttpContext.Session.GetInt32("UserId");
            //int id = 2;
            int id = (int) HttpContext.Session.GetInt32("Id");
            var orderlist = context.Ordered.Where(o => o.UserId == id).ToList();

            return View(orderlist);

        }

        [HttpGet]
        public IActionResult AddToCart(int ProductId) 
        {
            var id = (int) HttpContext.Session.GetInt32("Id");
           var product = context.Inventry.FirstOrDefault(o => o.ProductId == ProductId);
            Cart cart = new Cart(); 
            if(product != null) 
            {
                cart.ProductId = product.ProductId;
                cart.UserId = id;
                cart.ProductPrice = product.ProductPrice;
                cart.ProductName = product.ProductName;
                cart.ProductQuntity = 1;
                cart.ProductImageUrl= product.ProductImageUrl;
            }

            context.Cart.Add(cart);
            int result = context.SaveChanges();
            if(result == 1)
            {
                 HttpContext.Session.SetString("msg" ,"<script> alert('Product Added to Cart') </script>");
                return RedirectToAction("Index");
            }
            else {
                HttpContext.Session.SetString("msg", "<script> alert('Failed to Add to the cart') </script>");
                return RedirectToAction("Index");
            }
            
        }

        [HttpGet]
        public IActionResult ViewCart() 
        {
            var id = (int)HttpContext.Session.GetInt32("Id");
            var cartlist= context.Cart.Where(c => c.UserId == id).ToList();
            if(cartlist.Count > 0) 
            {
                return View(cartlist);
            }
            else
            {
                ViewBag.Add ="<h4>Cart is Empty</h4>";
                return View();
            }
        }
        [HttpGet]
        public IActionResult Proceed(int CartId) 
        {
            var Prod = context.Cart.Where(c => c.CartId == CartId).SingleOrDefault();
            var p = Prod;

            return View(Prod);
        }

        [HttpPost]
        public IActionResult Proceed(int CartId, int qty) 
        {
            var cart =  context.Cart.Where(c => c.CartId == CartId).SingleOrDefault();
            if(cart != null) 
            {
                Ordered od = new Ordered();
                od.ProductId = cart.ProductId;
                od.UserId = cart.UserId;
                od.ProductPrice = cart.ProductPrice;
                od.ProductName = cart.ProductName;
                od.ProductQuntity = qty;
                od.ProductTotalBill = cart.ProductPrice * qty;
                od.ProductImageUrl=cart.ProductImageUrl;

                HttpContext.Session.SetInt32("ProductId", cart.ProductId);
                context.Ordered.Add(od);
                context.SaveChanges();

                context.Cart.Remove(cart);
                context.SaveChanges();
                HttpContext.Session.SetString("data2", JsonConvert.SerializeObject(od));
                return RedirectToAction("Placed");
            }

            return View();
        }

        [HttpGet]
        public IActionResult Placed() 
        {
            var data = HttpContext.Session.GetString("data2");
            Ordered ordered = JsonConvert.DeserializeObject<Ordered>(data);
            
           
            return View(ordered);
        }
        [HttpPost]



        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }

    }
}
