using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Volcano.Models;

namespace Volcano.Controllers
{
    public class AdminController : Controller
    {
        DB_Context dB = new DB_Context();
        
        public ActionResult CheckCategory(string Cat_Name , int? Cat_ID)
        {
            if(Cat_ID == null)
            {
                Category_Detail category = dB.Category_Detail.Where(n => n.Cat_Name == Cat_Name).FirstOrDefault();

                if(category == null)
                {
                    return Json(true, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(false, JsonRequestBehavior.AllowGet);
                }

            }
            else
            {
                Category_Detail category = dB.Category_Detail.Where(n => n.Cat_Name == Cat_Name && n.Cat_ID != Cat_ID).FirstOrDefault();

                if (category == null)
                {
                    return Json(true, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(false, JsonRequestBehavior.AllowGet);
                }
            }
        }

        public ActionResult CheckProduct(string Prod_Name, int? Prod_ID)
        {
            if (Prod_ID == null)
            {
                Product_Detail product = dB.Product_Detail.Where(n => n.Prod_Name == Prod_Name).FirstOrDefault();

                if (product == null)
                {
                    return Json(true, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(false, JsonRequestBehavior.AllowGet);
                }

            }
            else
            {
                Product_Detail product = dB.Product_Detail.Where(n => n.Prod_Name == Prod_Name && n.Prod_ID != Prod_ID).FirstOrDefault();

                if (product == null)
                {
                    return Json(true, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(false, JsonRequestBehavior.AllowGet);
                }
            }
        }

        public ActionResult Dashboard()
        {
            return View();
        }
        public ActionResult Category()
        {
            ViewBag.Catg = dB.Category_Detail.ToList();
            return View();
        }

        public ActionResult AddCategory()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddCategory(Category_Detail category)
        {
            dB.Category_Detail.Add(category);
            dB.SaveChanges();

            return RedirectToAction("Category", "Admin");
        }

        public ActionResult DeleteCategory(int id)
        {
            Category_Detail category = dB.Category_Detail.Where(n => n.Cat_ID == id).FirstOrDefault();

            dB.Category_Detail.Remove(category);
            dB.SaveChanges();

            return RedirectToAction("Category", "Admin");
        }

        public ActionResult UpdateCategory(int id)
        {
            Category_Detail category = dB.Category_Detail.Where(n => n.Cat_ID == id).FirstOrDefault();

            return View(category);
        }

        [HttpPost]
        public ActionResult UpdateCategory(Category_Detail category)
        {
            Category_Detail category1 = dB.Category_Detail.Where(n => n.Cat_ID == category.Cat_ID).SingleOrDefault();

            category1.Cat_Name = category.Cat_Name;
            category1.IsActive = category.IsActive;
            category1.IsDelete = category.IsDelete;

            dB.SaveChanges();

            return RedirectToAction("Category", "Admin");
        }

        public ActionResult Product()
        {
            ViewBag.Prods = dB.Product_Detail.ToList();
            return View();
        }

        public ActionResult AddProduct()
        {
            ViewBag.Catg = new SelectList(dB.Category_Detail.ToList(), "Cat_ID", "Cat_Name");
            return View();
        }

        [HttpPost]
        public ActionResult AddProduct(Product_Detail product , HttpPostedFileBase photo)
        {

            //Upload File to Webserver

            photo.SaveAs(Server.MapPath($"~/Attach/{photo.FileName}"));

            //Save Path in DB

            product.Prod_Image = photo.FileName;

            dB.Product_Detail.Add(product);
            dB.SaveChanges();

            return RedirectToAction("Product", "Admin");
        }

        public ActionResult DeleteProduct(int id)
        {
           
            Product_Detail product = dB.Product_Detail.Where(n => n.Prod_ID == id).FirstOrDefault();

            dB.Product_Detail.Remove(product);
            dB.SaveChanges();

            return RedirectToAction("Product", "Admin");
        }

        public ActionResult UpdateProduct(int id)
        {
            Product_Detail product = dB.Product_Detail.Where(n => n.Prod_ID == id).FirstOrDefault();

            return View(product);
        }

        [HttpPost]
        public ActionResult UpdateProduct(Product_Detail product)
        {
            Product_Detail product1 = dB.Product_Detail.Where(n => n.Prod_ID == product.Prod_ID).SingleOrDefault();

            product1.Prod_Name = product.Prod_Name;
            product1.Description = product.Description;
            product1.Cat_ID = product.Cat_ID;
            product1.Prod_Image = product.Prod_Image;
            product1.Quantity = product.Quantity;
            product1.Price = product.Price;
            product1.IsActive = product.IsActive;
            product1.IsDelete = product.IsDelete;
            product1.IsFeatured = product.IsFeatured;

            dB.SaveChanges();

            return RedirectToAction("Product", "Admin");
        }

        public ActionResult Orders()
        {
            ViewBag.Orders = dB.Shipping_Detail.ToList();
            return View();
        }

        public ActionResult Accounts()
        {
            ViewBag.Acc = dB.Members.ToList();
            return View();
        }

        public ActionResult DeleteMember(int id)
        {
            Member member = dB.Members.Where(n => n.Member_ID== id).FirstOrDefault();

            dB.Members.Remove(member);
            dB.SaveChanges();

            return RedirectToAction("Accounts", "Admin");
        }

        public ActionResult UpdateMember(int id)
        {
            Member member = dB.Members.Where(n => n.Member_ID == id).FirstOrDefault();
            return View(member);
        }

        [HttpPost]
        public ActionResult UpdateMember(Member member)
        {
            Member member1 = dB.Members.Where(n => n.Member_ID == member.Member_ID).FirstOrDefault();

            member1.First_Name = member.First_Name;
            member1.Last_Name = member.Last_Name;
            member1.Email = member.Email;
            member1.Role = member.Role;
            member1.Phone = member.Phone;

            dB.SaveChanges();

            return RedirectToAction("Accounts", "Admin");
        }

        public ActionResult Sales()
        {
            return View();
        }

    }
}