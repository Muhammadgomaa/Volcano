using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Volcano.Models;

namespace Volcano.Controllers
{
    public class HomeController : Controller
    {
        DB_Context dB = new DB_Context();

        public ActionResult CheckMember(string Email, int? Member_ID)
        {
            if (Member_ID == null)
            {
                Member member = dB.Members.Where(n => n.Email == Email).FirstOrDefault();

                if (member == null)
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
                Member member = dB.Members.Where(n => n.Email == Email && n.Member_ID != Member_ID).FirstOrDefault();

                if (member == null)
                {
                    return Json(true, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(false, JsonRequestBehavior.AllowGet);
                }
            }
        }

        public ActionResult Login()
        {
            //if coockies founded in pc
            if (Request.Cookies["coockie"] != null)
            {
                Session["memberid"] = Request.Cookies["coockie"].Values["id"];
                int id = int.Parse(Session["memberid"].ToString());

                if (Session["memberid"] != null)
                {
                    Member member = dB.Members.Where(n => n.Member_ID == id).SingleOrDefault();

                    if(member.Role == "Admin")
                    {
                        return RedirectToAction("Dashboard","Admin");
                    }
                    else if(member.Role == "User")
                    {
                        return RedirectToAction("Index");
                    }
                }
                else
                {
                    return RedirectToAction("Login");
                }
            }

            return View();
        }

        [HttpPost]
        public ActionResult Login(Member member , string rememberme)
        {
            Member member1 = dB.Members.Where(n => n.Email == member.Email && n.Password == member.Password).SingleOrDefault();

            if (member1 != null && member1.Role == "Admin")
            {
                Session.Add("memberid", member1.Member_ID);

                //cookie 
                //if checkbox is checked
                if (rememberme == "true")
                {
                    HttpCookie cookie = new HttpCookie("coockie"); //create file
                    cookie.Values.Add("id", member1.Member_ID.ToString()); //save data
                    cookie.Expires = DateTime.Now.AddDays(90); //expire date
                    Response.Cookies.Add(cookie);
                }

                return RedirectToAction("Dashboard","Admin");
            }

            else if (member1 != null && member1.Role == "User")
            {
                Session.Add("memberid", member1.Member_ID);

                //cookie 
                //if checkbox is checked
                if (rememberme == "true")
                {
                    HttpCookie cookie = new HttpCookie("coockie"); //create file
                    cookie.Values.Add("id", member1.Member_ID.ToString()); //save data
                    cookie.Expires = DateTime.Now.AddDays(90); //expire date
                    Response.Cookies.Add(cookie);
                }

                return RedirectToAction("Index");
            }

            else
            {
                ViewBag.status = "Invalid Username or Password";
                return View();
            }
        }

        public ActionResult Logout()
        {
            Session["memberid"] = null;
            Session["cart"] = null;
            HttpCookie cookie = new HttpCookie("coockie"); //create file
            cookie.Expires = DateTime.Now.AddDays(-15); //expire date (to delete coockie)
            Response.Cookies.Add(cookie);
            return RedirectToAction("Login");
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(Member member)
        {
            dB.Members.Add(member);
            dB.SaveChanges();

            return RedirectToAction("Login");
        }

        public ActionResult Index()
        {
            if (Session["memberid"] != null)
            {
                return View();
            }
            else
                return RedirectToAction("Login");
        }

        public ActionResult About()
        {
            if (Session["memberid"] != null)
            {
                return View();
            }
            else
                return RedirectToAction("Login");
        }

        public ActionResult Shop()
        {
            if (Session["memberid"] != null)
            {
                List<Product_Detail> products = dB.Product_Detail.Where(n=>n.Status == "Available").ToList();
                List<Category_Detail> categories = dB.Category_Detail.ToList();

                if (categories.Count != 0 && products.Count != 0 && Session["cart"] == null)
                {
                    ViewBag.Prods = products;
                    ViewBag.Catg = categories;
                    ViewBag.List = new List<Items>();
                    return View();
                }
                else if (categories.Count != 0 && products.Count != 0 && Session["cart"] != null)
                {
                    ViewBag.Prods = products;
                    ViewBag.Catg = categories;
                    ViewBag.List = (List<Items>)Session["cart"];
                    return View();
                }
                else if (categories.Count != 0 && products.Count == 0 && Session["cart"] == null)
                {
                    ViewBag.Prods = new List<Product_Detail>();
                    ViewBag.Catg = categories;
                    ViewBag.List = new List<Items>();
                    return View();
                }
                else if (categories.Count != 0 && products.Count == 0 && Session["cart"] != null)
                {
                    ViewBag.Prods = new List<Product_Detail>();
                    ViewBag.Catg = categories;
                    ViewBag.List = (List<Items>)Session["cart"];
                    return View();
                }
                else
                {
                    ViewBag.Prods = new List<Product_Detail>();
                    ViewBag.Catg = new List<Category_Detail>();
                    return View();
                }
            }
            else
                return RedirectToAction("Login");
        }

        public ActionResult AddCart(int id)
        {
            if (Session["memberid"] != null)
            {

                //First Item Added
                if (Session["cart"] == null)
                {
                    List<Items> Cart = new List<Items>();

                    Product_Detail product = dB.Product_Detail.Where(n => n.Prod_ID == id).SingleOrDefault();

                    Cart.Add(new Items()
                    {
                        Product = product,
                        Quantity = 1
                    });

                    Session["cart"] = Cart;
                }

                else
                {
                    List<Items> Cart = (List<Items>)Session["cart"];

                    Product_Detail product = dB.Product_Detail.Where(n => n.Prod_ID == id).SingleOrDefault();

                    Cart.Add(new Items()
                    {
                        Product = product,
                        Quantity = 1
                    });

                    Session["cart"] = Cart;
                }

                return RedirectToAction("Shop");

            }
            else
                return RedirectToAction("Login");
        }

        public ActionResult RemoveCart(int id)
        {
            if (Session["memberid"] != null)
            {
                List<Items> Cart = (List<Items>)Session["cart"];

                for (int i = 0; i < Cart.Count; i++)
                {
                    if (Cart[i].Product.Prod_ID == id)
                    {
                        Cart.Remove(Cart[i]);
                        break;
                    }
                }
                if (Cart.Count == 0)
                {
                    Session["cart"] = null;
                }
                else
                {
                    Session["cart"] = Cart;
                }

                return RedirectToAction("Shop");
            }
            else
                return RedirectToAction("Login");
        }

        public ActionResult Checkout()
        {
            if (Session["memberid"] != null)
            {
                return View();
            }
            else
                return RedirectToAction("Login");
        }

        public ActionResult IncreaseQuantity(int id)
        {
            if (Session["memberid"] != null)
            {
                if (Session["cart"] != null)
                {
                    List<Items> Cart = (List<Items>)Session["cart"];

                    Product_Detail product = dB.Product_Detail.Where(n => n.Prod_ID == id).SingleOrDefault();

                    foreach (var items in Cart)
                    {
                        if (items.Product.Prod_ID == id)
                        {
                            int prevQty = items.Quantity;
                            Cart.Remove(items);
                            Cart.Add(new Items()
                            {
                                Product = product,
                                Quantity = prevQty + 1
                            });
                            break;
                        }
                    }

                    Session["cart"] = Cart;
                }
                return RedirectToAction("Checkout");
            }
            else
                return RedirectToAction("Login");
        }

        public ActionResult DecreaseQuantity(int id)
        {
           if (Session["memberid"] != null) 
            {
                if (Session["cart"] != null)
                {
                    List<Items> Cart = (List<Items>)Session["cart"];

                    Product_Detail product = dB.Product_Detail.Where(n => n.Prod_ID == id).SingleOrDefault();

                    foreach (var items in Cart)
                    {
                        if (items.Product.Prod_ID == id)
                        {
                            int prevQty = items.Quantity;
                            Cart.Remove(items);
                            Cart.Add(new Items()
                            {
                                Product = product,
                                Quantity = prevQty - 1
                            });
                            break;
                        }
                    }

                    Session["cart"] = Cart;
                }
                return RedirectToAction("Checkout");
            }
            else
                return RedirectToAction("Login");
        }

        public ActionResult CheckoutDetails()
        {
            if (Session["memberid"] != null)
            {
                int id = int.Parse(Session["memberid"].ToString());
                Member member = dB.Members.Where(n => n.Member_ID == id).SingleOrDefault();

                return View(member);
            }
            else
                return RedirectToAction("Login");
        }

        public ActionResult ShippingDetails(Shipping_Detail shipping)
        {

            if (Session["memberid"] != null)
            {
                dB.Shipping_Detail.Add(shipping);
                dB.SaveChanges();

                foreach(Items items in (List<Items>)Session["cart"])
                {
                    double total = items.Quantity * items.Product.Price;

                    Invoice_Detail invoice = new Invoice_Detail();

                    invoice.Shipping_ID = shipping.Shipping_ID;
                    invoice.Prod_ID = items.Product.Prod_ID;
                    invoice.Quantity = items.Quantity.ToString();
                    invoice.Price = total;

                    dB.Invoice_Detail.Add(invoice);
                    dB.SaveChanges();
                }

                Session["cart"] = null;

                return RedirectToAction("Index");
            }
            else
                return RedirectToAction("Login");
        }

        public ActionResult Account()
        {
            if (Session["memberid"] != null)
            {
                return View();
            }
            else
                return RedirectToAction("Login");
        }

        public ActionResult Orders()
        {
            if (Session["memberid"] != null)
            {
                int id = int.Parse(Session["memberid"].ToString());
                ViewBag.Orders = dB.Shipping_Detail.Where(n => n.Member_ID == id).ToList();

                return View();
            }
            else
                return RedirectToAction("Login");
        }

        public ActionResult Invoice(int id)
        {
            if (Session["memberid"] != null)
            {
                ViewBag.Invoice = dB.Invoice_Detail.Where(n => n.Shipping_ID == id).ToList();
                return View();
            }
            else
                return RedirectToAction("Login");
        }

        public ActionResult UpdateInformation()
        {
            if (Session["memberid"] != null)
            {
                int id = int.Parse(Session["memberid"].ToString());
                Member member = dB.Members.Where(n => n.Member_ID == id).FirstOrDefault();

                return View(member);
            }
            else
                return RedirectToAction("Login");
        }

        [HttpPost]
        public ActionResult UpdateInformation(Member member)
        {
            Member member1 = dB.Members.Where(n => n.Member_ID == member.Member_ID).SingleOrDefault();

            member1.First_Name = member.First_Name;
            member1.Last_Name = member.Last_Name;
            member1.Password = member.Password;
            member1.ConfirmPassword = member.ConfirmPassword;
            member1.Email = member.Email;
            member1.Role = member.Role;
            member1.Phone = member.Phone;

            dB.SaveChanges();

            return RedirectToAction("Account");
        }

        public ActionResult UpdatePassword()
        {
            if (Session["memberid"] != null)
            {
                int id = int.Parse(Session["memberid"].ToString());
                Member member = dB.Members.Where(n => n.Member_ID == id).FirstOrDefault();

                return View(member);
            }
            else
                return RedirectToAction("Login");
        }

        [HttpPost]
        public ActionResult UpdatePassword(Member member)
        {
            Member member1 = dB.Members.Where(n => n.Member_ID == member.Member_ID).SingleOrDefault();

            member1.First_Name = member.First_Name;
            member1.Last_Name = member.Last_Name;
            member1.Password = member.Password;
            member1.ConfirmPassword = member.ConfirmPassword;
            member1.Email = member.Email;
            member1.Role = member.Role;
            member1.Phone = member.Phone;

            dB.SaveChanges();

            return RedirectToAction("Account");
        }

        public ActionResult AddMember()
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
        public ActionResult AddMember(Member member)
        {
            dB.Members.Add(member);
            dB.SaveChanges();

            return RedirectToAction("Accounts", "Admin");
        }
    }
}