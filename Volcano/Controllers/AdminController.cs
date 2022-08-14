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
            if (Session["memberid"] != null)
            {
                int id = int.Parse(Session["memberid"].ToString());
                Member member = dB.Members.Where(n => n.Member_ID == id).SingleOrDefault();

                if (member.Role == "User")
                {
                    return RedirectToAction("Index","Home");
                }
                else if (member.Role == "Admin")
                {
                    return View(member);
                }
                return View();
            }
            else
                return RedirectToAction("Login");
        }

        public ActionResult Category()
        {
            if (Session["memberid"] != null)
            {
                int id = int.Parse(Session["memberid"].ToString());
                Member member = dB.Members.Where(n => n.Member_ID == id).SingleOrDefault();

                if (member.Role == "User")
                {
                    return RedirectToAction("Index", "Home");
                }
                else if (member.Role == "Admin")
                {
                    ViewBag.Catg = dB.Category_Detail.ToList();
                    return View();
                }
                return View();
            }
            else
                return RedirectToAction("Login");
        }

        public ActionResult AddCategory()
        {
            if (Session["memberid"] != null)
            {
                int id = int.Parse(Session["memberid"].ToString());
                Member member = dB.Members.Where(n => n.Member_ID == id).SingleOrDefault();

                if (member.Role == "User")
                {
                    return RedirectToAction("Index", "Home");
                }
                else if (member.Role == "Admin")
                {
                    return View();
                }
                return View();
            }
            else
                return RedirectToAction("Login");
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
            if (Session["memberid"] != null)
            {
                int id1 = int.Parse(Session["memberid"].ToString());
                Member member = dB.Members.Where(n => n.Member_ID == id1).SingleOrDefault();

                if (member.Role == "User")
                {
                    return RedirectToAction("Index", "Home");
                }
                else if (member.Role == "Admin")
                {
                    Category_Detail category = dB.Category_Detail.Where(n => n.Cat_ID == id).FirstOrDefault();
                    return View(category);
                }
                return View();
            }
            else
                return RedirectToAction("Login");
        }

        [HttpPost]
        public ActionResult DeleteCategory(Category_Detail category)
        {
            Category_Detail category1 = dB.Category_Detail.Where(n => n.Cat_ID == category.Cat_ID).SingleOrDefault();
            dB.Category_Detail.Remove(category1);
            dB.SaveChanges();

            return RedirectToAction("Category", "Admin");
        }

        public ActionResult UpdateCategory(int id)
        {
            if (Session["memberid"] != null)
            {
                int id1 = int.Parse(Session["memberid"].ToString());
                Member member = dB.Members.Where(n => n.Member_ID == id1).SingleOrDefault();

                if (member.Role == "User")
                {
                    return RedirectToAction("Index", "Home");
                }
                else if (member.Role == "Admin")
                {
                    Category_Detail category = dB.Category_Detail.Where(n => n.Cat_ID == id).FirstOrDefault();
                    return View(category);
                }
                return View();
            }
            else
                return RedirectToAction("Login");
        }

        [HttpPost]
        public ActionResult UpdateCategory(Category_Detail category)
        {
            Category_Detail category1 = dB.Category_Detail.Where(n => n.Cat_ID == category.Cat_ID).SingleOrDefault();
            category1.Cat_Name = category.Cat_Name;
            dB.SaveChanges();

            return RedirectToAction("Category", "Admin");
        }

        public ActionResult Product()
        {
            if (Session["memberid"] != null)
            {
                int id = int.Parse(Session["memberid"].ToString());
                Member member = dB.Members.Where(n => n.Member_ID == id).SingleOrDefault();

                if (member.Role == "User")
                {
                    return RedirectToAction("Index", "Home");
                }
                else if (member.Role == "Admin")
                {
                    ViewBag.Prods = dB.Product_Detail.ToList();
                    return View();
                }
                return View();
            }
            else
                return RedirectToAction("Login");
        }

        public ActionResult AddProduct()
        {
            if (Session["memberid"] != null)
            {
                int id = int.Parse(Session["memberid"].ToString());
                Member member = dB.Members.Where(n => n.Member_ID == id).SingleOrDefault();

                if (member.Role == "User")
                {
                    return RedirectToAction("Index", "Home");
                }
                else if (member.Role == "Admin")
                {
                    ViewBag.Catg = new SelectList(dB.Category_Detail.ToList(), "Cat_ID", "Cat_Name");
                    return View();
                }
                return View();
            }
            else
                return RedirectToAction("Login");
        }

        [HttpPost]
        public ActionResult AddProduct(Product_Detail product , HttpPostedFileBase photo)
        {
            if(photo != null)
            {
                //Upload File to Webserver

                photo.SaveAs(Server.MapPath($"~/Attach/{photo.FileName}"));

                //Save Path in DB

                product.Prod_Image = photo.FileName;

                dB.Product_Detail.Add(product);
                dB.SaveChanges();

                return RedirectToAction("Product", "Admin");
            }
            else
            {
                dB.Product_Detail.Add(product);
                dB.SaveChanges();

                return RedirectToAction("Product", "Admin");
            }
        }

        public ActionResult DeleteProduct(int id)
        {
            if (Session["memberid"] != null)
            {
                int id1 = int.Parse(Session["memberid"].ToString());
                Member member = dB.Members.Where(n => n.Member_ID == id1).SingleOrDefault();

                if (member.Role == "User")
                {
                    return RedirectToAction("Index", "Home");
                }
                else if (member.Role == "Admin")
                {
                    Product_Detail product = dB.Product_Detail.Where(n => n.Prod_ID == id).FirstOrDefault();
                    return View(product);
                }
                return View();
            }
            else
                return RedirectToAction("Login");
        }

        [HttpPost]
        public ActionResult DeleteProduct(Product_Detail product)
        {  
            Product_Detail product1 = dB.Product_Detail.Where(n => n.Prod_ID == product.Prod_ID).FirstOrDefault();
            System.IO.File.Delete(Server.MapPath($"~/Attach/{product1.Prod_Image}"));
            dB.Product_Detail.Remove(product1);
            dB.SaveChanges();

            return RedirectToAction("Product", "Admin");
        }

        public ActionResult UpdateProduct(int id)
        {
            if (Session["memberid"] != null)
            {
                int id1 = int.Parse(Session["memberid"].ToString());
                Member member = dB.Members.Where(n => n.Member_ID == id1).SingleOrDefault();

                if (member.Role == "User")
                {
                    return RedirectToAction("Index", "Home");
                }
                else if (member.Role == "Admin")
                {
                    Product_Detail product = dB.Product_Detail.Where(n => n.Prod_ID == id).FirstOrDefault();
                    ViewBag.Catg = new SelectList(dB.Category_Detail.ToList(), "Cat_ID", "Cat_Name",product.Cat_ID);
                    return View(product);
                }
                return View();
            }
            else
                return RedirectToAction("Login");
        }

        [HttpPost]
        public ActionResult UpdateProduct(Product_Detail product)
        {
            Product_Detail product1 = dB.Product_Detail.Where(n => n.Prod_ID == product.Prod_ID).SingleOrDefault();

            product1.Prod_Name = product.Prod_Name;
            product1.Cat_ID = product.Cat_ID;
            product1.Prod_Image = product.Prod_Image;
            product1.Price = product.Price;
            product1.Status = product.Status;

            dB.SaveChanges();

            return RedirectToAction("Product", "Admin");
        }

        public ActionResult Orders()
        {
            if (Session["memberid"] != null)
            {
                int id = int.Parse(Session["memberid"].ToString());
                Member member = dB.Members.Where(n => n.Member_ID == id).SingleOrDefault();

                if (member.Role == "User")
                {
                    return RedirectToAction("Index", "Home");
                }
                else if (member.Role == "Admin")
                {
                    ViewBag.Orders = dB.Shipping_Detail.ToList();
                    return View();
                }
                return View();
            }
            else
                return RedirectToAction("Login");
        }

        public ActionResult Refund(int id)
        {
            if (Session["memberid"] != null)
            {
                int id1 = int.Parse(Session["memberid"].ToString());
                Member member = dB.Members.Where(n => n.Member_ID == id1).SingleOrDefault();

                if (member.Role == "User")
                {
                    return RedirectToAction("Index", "Home");
                }
                else if (member.Role == "Admin")
                {
                    Shipping_Detail shipping = dB.Shipping_Detail.Where(n => n.Shipping_ID == id).SingleOrDefault();
                    return View(shipping);
                }
                return View();
            }
            else
                return RedirectToAction("Login");
        }

        [HttpPost]
        public ActionResult Refund(Refund_Detail refund , int id)
        {
            List<Invoice_Detail> invoices = dB.Invoice_Detail.Where(n => n.Shipping_ID == id).ToList();

            for (int i = 0; i < invoices.Count; i++)
            {
                Invoice_Detail invoice = invoices[i];
                dB.Invoice_Detail.Remove(invoice);
                dB.SaveChanges();
            }

            Shipping_Detail shipping = dB.Shipping_Detail.Where(n => n.Shipping_ID == id).FirstOrDefault();
            dB.Shipping_Detail.Remove(shipping);
            dB.SaveChanges();

            dB.Refund_Detail.Add(refund);
            dB.SaveChanges();

            return RedirectToAction("Orders", "Admin");
        }

        public ActionResult Accounts()
        {
            if (Session["memberid"] != null)
            {
                int id = int.Parse(Session["memberid"].ToString());
                Member member = dB.Members.Where(n => n.Member_ID == id).SingleOrDefault();

                if (member.Role == "User")
                {
                    return RedirectToAction("Index", "Home");
                }
                else if (member.Role == "Admin")
                {
                    ViewBag.Acc = dB.Members.ToList();
                    return View();
                }
                return View();
            }
            else
                return RedirectToAction("Login");
        }

        public ActionResult DeleteMember(int id)
        {
            if (Session["memberid"] != null)
            {
                int id1 = int.Parse(Session["memberid"].ToString());
                Member member = dB.Members.Where(n => n.Member_ID == id1).SingleOrDefault();

                if (member.Role == "User")
                {
                    return RedirectToAction("Index", "Home");
                }
                else if (member.Role == "Admin")
                {
                    Member member1 = dB.Members.Where(n => n.Member_ID == id).FirstOrDefault();
                    return View(member1);
                }
                return View();
            }
            else
                return RedirectToAction("Login");
        }

        [HttpPost]
        public ActionResult DeleteMember(Member member)
        {
            Member member1 = dB.Members.Where(n => n.Member_ID == member.Member_ID).FirstOrDefault();
            dB.Members.Remove(member1);
            dB.SaveChanges();

            return RedirectToAction("Accounts", "Admin");
        }

        public ActionResult UpdateMember(int id)
        {
            if (Session["memberid"] != null)
            {
                int id1 = int.Parse(Session["memberid"].ToString());
                Member member = dB.Members.Where(n => n.Member_ID == id1).SingleOrDefault();

                if (member.Role == "User")
                {
                    return RedirectToAction("Index", "Home");
                }
                else if (member.Role == "Admin")
                {
                    Member member1 = dB.Members.Where(n => n.Member_ID == id).FirstOrDefault();
                    return View(member1);
                }
                return View();
            }
            else
                return RedirectToAction("Login");
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
            member1.Password = member.Password;
            member1.ConfirmPassword = member.ConfirmPassword;

            dB.SaveChanges();

            return RedirectToAction("Accounts", "Admin");
        }

        public ActionResult Sales()
        {
            if (Session["memberid"] != null)
            {
                int id = int.Parse(Session["memberid"].ToString());
                Member member = dB.Members.Where(n => n.Member_ID == id).SingleOrDefault();

                if (member.Role == "User")
                {
                    return RedirectToAction("Index", "Home");
                }
                else if (member.Role == "Admin")
                {
                    List<Member> members = dB.Members.Where(n => n.Role == "User").ToList();
                    if(members.Count != 0)
                    {
                        ViewBag.Clients = members.Count;
                    }
                    else
                    {
                        ViewBag.Clients = 0;
                    }

                    List<Member> members1 = dB.Members.Where(n => n.Role == "Admin").ToList();
                    if (members1.Count != 0)
                    {
                        ViewBag.Admins = members1.Count;
                    }
                    else
                    {
                        ViewBag.Admins = 0;
                    }

                    List<Shipping_Detail> orders = dB.Shipping_Detail.ToList();
                    if (orders.Count != 0)
                    {
                        ViewBag.Orders = orders.Count;
                    }
                    else
                    {
                        ViewBag.Orders = 0;
                    }

                    List<Refund_Detail> refunds = dB.Refund_Detail.ToList();
                    if (refunds.Count != 0)
                    {
                        ViewBag.Refund = refunds.Count;
                    }
                    else
                    {
                        ViewBag.Refund = 0;
                    }

                    List<Category_Detail> categories = dB.Category_Detail.ToList();
                    if (categories.Count != 0)
                    {
                        ViewBag.Catg = categories.Count;
                    }
                    else
                    {
                        ViewBag.Catg = 0;
                    }

                    List<Product_Detail> products = dB.Product_Detail.Where(n => n.Status == "Available").ToList();
                    if (products.Count != 0)
                    {
                        ViewBag.Prods = products.Count;
                    }
                    else
                    {
                        ViewBag.Prods = 0;
                    }

                    List<Product_Detail> stock = dB.Product_Detail.Where(n=>n.Status == "Out of Stock").ToList();
                    if (stock.Count != 0)
                    {
                        ViewBag.Stock = stock.Count;
                    }
                    else
                    {
                        ViewBag.Stock = 0;
                    }

                    List<Invoice_Detail> invoices= dB.Invoice_Detail.ToList();
                    if (invoices.Count != 0)
                    {
                        ViewBag.Invoice = invoices.Count;
                    }
                    else
                    {
                        ViewBag.Invoice = 0;
                    }

                    List<double> totalAmount = dB.Shipping_Detail.Select(n => n.AmountPaid).ToList();
                    if (totalAmount.Count != 0)
                    {
                        double MaxValue = dB.Shipping_Detail.Select(n => n.AmountPaid).Max();
                        double MinValue = dB.Shipping_Detail.Select(n => n.AmountPaid).Min();
                        double Total = 0;
                        for (int i = 0; i < totalAmount.Count(); i++)
                        {
                            Total += totalAmount[i];
                        }
                        ViewBag.Sales = Total;
                        ViewBag.Max = MaxValue;
                        ViewBag.Min = MinValue;
                    }
                    else
                    {
                        ViewBag.Sales = 0;
                        ViewBag.Max = 0;
                        ViewBag.Min = 0;
                    }

                    return View();
                }
                return View();
            }
            else
                return RedirectToAction("Login");
        }

    }
}