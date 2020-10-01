using System.Web.Mvc;
using OnlineShopping.Controllers.Helpers;
using OnlineShopping.Models;
using OnlineShopping.DbUtil;
using System;
using System.Collections.Generic;

namespace OnlineShopping.Controllers.Home
{
    [RoutePrefix("account")]
    public class UserController : Controller
    {
        readonly AccountUtil accountUtil = new AccountUtil();

        public UserController()
        {
            CategoriesUtil categoriesUtil = new CategoriesUtil();
            ViewBag.AllCategories = categoriesUtil.List();
        }

        [GuestUserAuthorized]
        [Route("login")]
        public ActionResult Login()
        {
            return View();
        }
        
        [GuestUserAuthorized]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("login")]
        public ActionResult Login(Users users)
        {
            if (accountUtil.UserLogin(users.Email, users.Password))
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.ErrorMsg = "Invalid Credientials";
                return View();
            }
        }

        public ActionResult Logout()
        {
            Session.Remove(AppEnv.UserSessionKey);
            return RedirectToAction("Index", "Home");
        }

        [GuestUserAuthorized]
        [Route("register")]
        public ActionResult Register()
        {
            return View();
        }

        [GuestUserAuthorized]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("register")]
        public ActionResult Register(Users users)
        {
            if (accountUtil.AddUser(users))
            {
                return RedirectToAction("Login", "User");
            }
            else
            {
                ViewBag.ErrorMsg = "Error Occured!";
                return View();
            }
        }

        [UserAuthorized]
        [Route]
        public ActionResult MyAccount()
        {
            return View(accountUtil.GetUserByID((int)Session[AppEnv.UserSessionKey]));
        }

        [UserAuthorized]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route]
        public ActionResult MyAccount(Users users)
        {
            users.ID = (int) Session[AppEnv.UserSessionKey];
            if (accountUtil.UpdateUser(users))
            {
                Session["Home_Flash_Success"] = "Details updated successfully!";
            }
            else
            {
                Session["Home_Flash_Error"] = "Details updated failed!<br>Please try again later";
            }
            return RedirectToAction("MyAccount", "User");
        }
        
        [UserAuthorized]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword(FormCollection formCollection)
        {
            string OldPassword = Convert.ToString(formCollection["OldPassword"]);
            string NewPassword = Convert.ToString(formCollection["NewPassword"]);

            AccountUtil account = new AccountUtil();
            Users user = account.GetUserByID(Convert.ToInt32(Session["StudentID"]));

            if (user.Password == OldPassword)
            {
                if (account.UpdateUserPassword(NewPassword, user.ID))
                {
                    Session["Home_Flash_Success"] = "Password updated successfully!";
                }
                else
                {
                    Session["Home_Flash_Error"] = "Password update failed!<br>Please try again later";
                }
            }
            else
            {
                Session["Home_Flash_Warning"] = "Incorrect passsword!";
            }
            return RedirectToAction("MyAccount", "User");
        }

        [UserAuthorized]
        [Route("my-orders")]
        public ActionResult MyOrders()
        {
            OrdersUtil ordersUtil = new OrdersUtil();
            return View(ordersUtil.UsersList((int)Session[AppEnv.UserSessionKey]));
        }
        
        [UserAuthorized]
        [Route("my-cart")]
        public ActionResult MyCart()
        {
            CartUtil cartUtil = new CartUtil();
            return View(cartUtil.List((int) Session[AppEnv.UserSessionKey]));
        }

        [UserAuthorized]
        [Route("add-cart/{ID}")]
        public ActionResult AddToCart(int ID)
        {
            CartUtil cartUtil = new CartUtil();
            if (cartUtil.Insert(new Cart { ProdID = ID, UserID = (int)Session[AppEnv.UserSessionKey] }))
            {
                Session["Home_Flash_Success"] = "Product added to cart successfully!";
            }
            else
            {
                Session["Home_Flash_Error"] = "Product add to cart failed!<br>Please try again later";
            }
            return RedirectToAction("MyCart", "User");
        }
        
        [UserAuthorized]
        [Route("remove-cart/{ID}")]
        public ActionResult RemoveFromCart(int ID)
        {
            CartUtil cartUtil = new CartUtil();
            if (cartUtil.Delete(ID))
            {
                Session["Home_Flash_Success"] = "Product removed from cart successfully!";
            }
            else
            {
                Session["Home_Flash_Error"] = "Product removed from cart failed!<br>Please try again later";
            }
            return RedirectToAction("MyCart", "User");
        }

        [UserAuthorized]
        [Route("buy-now/{ID}")]
        public ActionResult BuyNow(int ID)
        {
            ProductsUtil productsUtil = new ProductsUtil();
            return View(productsUtil.GetByID(ID));
        }
        
        [UserAuthorized]
        [Route("buy-now/{ID}")]
        [HttpPost]
        public ActionResult BuyNowSubmit(int ID)
        {
            ProductsUtil productsUtil = new ProductsUtil();
            Products products = productsUtil.GetByID(ID);
            if (products.Quantity > 0)
            {
                OrdersUtil ordersUtil = new OrdersUtil();
                Orders orders = new Orders
                {
                    UserID = Convert.ToInt32(Session[AppEnv.UserSessionKey]),
                    ProdID = products.ID,
                    Status = 1,
                    Created_at = DateTime.Now,
                    Updated_at = DateTime.Now,
                };

                if (ordersUtil.Insert(orders))
                {
                    if (productsUtil.UpdateQuantity(products.ID, products.Quantity-1))
                    {
                        Session["Home_Flash_Success"] = "Order placed successfully!";
                    }
                    else
                    {
                        Session["Home_Flash_Error"] = "Error Occured!";
                    }
                }
            }
            else
            {
                Session["Home_Flash_Error"] = "Product out of stock";
            }

            return RedirectToAction("MyOrders", "User");
        }

        [UserAuthorized]
        [Route("cancel/{ID}")]
        public ActionResult CancelOrder(int ID)
        {
            ProductsUtil productsUtil = new ProductsUtil();

            OrdersUtil ordersUtil = new OrdersUtil();
            Orders orders = ordersUtil.GetByID(ID);

            Products products = productsUtil.GetByID(orders.ProdID);

            if (ordersUtil.UpdateStatus(orders.ID, 4))
            {
                if (productsUtil.UpdateQuantity(products.ID, products.Quantity+1))
                {
                    Session["Home_Flash_Success"] = "Order cancelled successfully!";
                }
                else
                {
                    Session["Home_Flash_Error"] = "Error Occured!";
                }
            }

            return RedirectToAction("MyOrders", "User");
        }

        [UserAuthorized]
        [Route("checkout")]
        public ActionResult Checkout()
        {
            ProductsUtil productsUtil = new ProductsUtil();
            CartUtil cartUtil = new CartUtil();
            OrdersUtil ordersUtil = new OrdersUtil();

            int UserID = (int)Session[AppEnv.UserSessionKey];

            List<Cart> UsersCart = cartUtil.List(UserID);

            int CartSize = UsersCart.Count;
            int InsertSize = 0;

            if (UsersCart.Count > 0)
            {
                foreach (Cart item in UsersCart)
                {
                    Products products = productsUtil.GetByID(item.ProdID);
                    if (products.Quantity > 0)
                    {
                        Orders orders = new Orders
                        {
                            UserID = UserID,
                            ProdID = products.ID,
                            Status = 1,
                            Created_at = DateTime.Now,
                            Updated_at = DateTime.Now,
                        };
                        if (ordersUtil.Insert(orders))
                        {
                            productsUtil.UpdateQuantity(products.ID, products.Quantity-1);
                            cartUtil.Delete(item.ID);
                            InsertSize++;
                        }
                    }
                }
                if (InsertSize == 0)
                {
                    Session["Home_Flash_Warning"] = "Sorry the products you are looking for are out of stock!";
                }
                else if (InsertSize < CartSize)
                {
                    Session["Home_Flash_Warning"] = " Few orders could not be placed as the were not in stock";
                }
                else if (InsertSize == CartSize)
                {
                    Session["Home_Flash_Success"] = "Orders placed successfully!";
                }
            }
            else
            {
                Session["Home_Flash_Error"] = "Sorry you cart is empty";
            }
            return RedirectToAction("MyCart", "User");
        }

    }
}