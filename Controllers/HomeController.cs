using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Lab23.Models;

namespace Lab23.Controllers
{
    public class HomeController : Controller
    {
        private CoffeeShopDBEntities ORM = new CoffeeShopDBEntities();
        public ActionResult Index()
        {
            ViewBag.Items = ORM.Items.ToList();
            return View();
        }

        public ActionResult SaveNewUser(User newUser)
        {
            if (ModelState.IsValid)
            {
               
                ORM.Users.Add(newUser);
                
                ORM.SaveChanges();
                
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.ErrorMessage = "Something did not go right.";
                return View("Contact");
            }
        }
        public ActionResult Delete(int ItemId)
        {
            
            Item found = ORM.Items.Find(ItemId);

            
            ORM.Items.Remove(found);

            
            ORM.SaveChanges();

            return RedirectToAction("Index");
        }
        public ActionResult Update(int ItemId)
        {
            
            Item found = ORM.Items.Find(ItemId);

            if (found is null)
            {
                return RedirectToAction("Index");
            }
            
            return View(found);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Add a NEW Item!";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult SaveChanges(Item updatedItem)
        {
            Item OGItem = ORM.Items.Find(updatedItem.Id);

            if (OGItem != null)
            {
                OGItem.Name = updatedItem.Name;
                OGItem.Description = updatedItem.Description;
                OGItem.Quantity = updatedItem.Quantity;
                OGItem.Price = updatedItem.Price;

                ORM.SaveChanges();

                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Update", updatedItem.Id);
            }
        }
        public ActionResult SaveNewItem(Item newItem)
        {
            
            if (ModelState.IsValid)
            {
                
                ORM.Items.Add(newItem);
                
                ORM.SaveChanges();
                
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.ErrorMessage = "Something did not go right.";
                return View("ItemForm");
            }
        }
    }
}