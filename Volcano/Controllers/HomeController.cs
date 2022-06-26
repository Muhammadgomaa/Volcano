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
                return RedirectToAction("Index");
            }

            return View();
        }

        [HttpPost]
        public ActionResult Login(Member member , string rememberme)
        {
            Member member1 = dB.Members.Where(n => n.Email == member.Email && n.Password== member.Password).SingleOrDefault();

            if (member1 != null)
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
            return View();
        }

        public ActionResult Shop()
        {
            ViewBag.Prods = dB.Product_Detail.ToList();
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult AddCart(int id)
        {
            //First Item Added
            if(Session["cart"] == null)
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

                //Avoid Duplicate
                List<Items> Cart1 = Cart.Distinct().ToList();

                Product_Detail product = dB.Product_Detail.Where(n => n.Prod_ID == id).SingleOrDefault();

                foreach (var items in Cart1)
                {
                    if (items.Product.Prod_ID == id)
                    {
                        Cart1.Remove(items);
                        Cart1.Add(new Items()
                        {
                            Product = product,
                            Quantity = 1
                        });
                        break;   
                    }
                    else 
                    {
                        Cart1.Add(new Items()
                        {
                            Product = product,
                            Quantity = 1
                        });
                        break; 
                    }
                }

                Session["cart"] = Cart1;
            }

            return RedirectToAction("Shop");
        }

        public ActionResult RemoveCart(int id)
        {
            List<Items> Cart = (List<Items>)Session["cart"];

            for(int i = 0; i < Cart.Count; i++)
            {
                if(Cart[i].Product.Prod_ID == id)
                {
                    Cart.Remove(Cart[i]);
                    break;
                }
            }
            if(Cart.Count == 0)
            {
                Session["cart"] = null;
            }
            else
            {
                Session["cart"] = Cart;
            }

            return RedirectToAction("Shop");
        }

        public ActionResult Checkout()
        {
            return View();
        }

        public ActionResult CheckoutDetails()
        {
            return View();
        }

        public ActionResult IncreaseQuantity(int id)
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

        public ActionResult DecreaseQuantity(int id)
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

        public ActionResult ShippingDetails(Shipping_Detail shipping)
        {

            dB.Shipping_Detail.Add(shipping);
            dB.SaveChanges();

            Session["cart"] = null;

            return RedirectToAction("Index");
        }

        public ActionResult Account()
        {
            return View();
        }

        public ActionResult Orders()
        {
            return View();
        }

        public ActionResult UpdateInformation()
        {
            int id = int.Parse(Session["memberid"].ToString());
            Member member = dB.Members.Where(n => n.Member_ID == id).FirstOrDefault();

            return View(member);
        }

        [HttpPost]
        public ActionResult UpdateInformation(Member member)
        {
            Member member1 = dB.Members.Where(n => n.Member_ID == member.Member_ID).SingleOrDefault();

            member1.First_Name = member.First_Name;
            member1.Last_Name = member.Last_Name;
            member1.Password = member.Password;
            member1.Email = member.Email;
            member1.Role = member.Role;
            member1.Phone = member.Phone;

            dB.SaveChanges();

            return RedirectToAction("Account");
        }

        public ActionResult UpdatePassword()
        {
            int id = int.Parse(Session["memberid"].ToString());
            Member member = dB.Members.Where(n => n.Member_ID == id).FirstOrDefault();

            return View(member);
        }

        [HttpPost]
        public ActionResult UpdatePassword(Member member)
        {
            Member member1 = dB.Members.Where(n => n.Member_ID == member.Member_ID).SingleOrDefault();

            member1.First_Name = member.First_Name;
            member1.Last_Name = member.Last_Name;
            member1.Password = member.Password;
            member1.Email = member.Email;
            member1.Role = member.Role;
            member1.Phone = member.Phone;

            dB.SaveChanges();

            return RedirectToAction("Account");
        }
    }
}