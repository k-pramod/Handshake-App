using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.WebPages;
using Newtonsoft.Json;
using Planner.Models;
using Planner.Models.Database;
using Planner.Models.DTO;

namespace Planner.Controllers
{
    public class UsersController : Controller
    {
        [HttpPost]
        public ActionResult Create(Register register)
        {
            var userId = Users.Create(register.Email, register.Password, register.Type, register.tagline,
                register.fullName);

            /*MemoryStream target = new MemoryStream();
            register.Resume.InputStream.CopyTo(target);
            byte[] data = target.ToArray();

            string fileAsString = data.ToString();*/
            var fileAsString = register.Resume;
            if (Files.Add(fileAsString, userId))
            {
                Session.Add("RegisterMessage", "You have been registered!  Please login...");
            }
            else
            {
                Session.Add("RegisterMessage",
                    "You have been registered, but your resume wasn't uploaded.  Please try again...");
            }

            return RedirectToAction("Index", "Home");
        }

        /*[System.Web.Mvc.HttpPost]
        public ActionResult UpdatePassword(UserForm form)
        {
            /* (V2:) Verify old password
             * HttpPost of new password
             * Update function passing in only the newPassword to the Users model
             
            var newPassword = form.newPassword;
            Users.UpdatePassword((long) Session["UserId"], newPassword);
            return null;
        }*/

        [HttpPost]
        public ActionResult WebLogin(string email, string password)
        {
            string login = Users.Login(email, password);
            if (login != null)
            {
                Session.Add("EmailAddress", email);
                Session.Add("type", login.Split(',')[1].Trim());
                Session.Add("userId", login.Split(',')[0].Trim().AsInt());
            }
            return RedirectToAction("Index", "Home");
        }

        //  Login
        [HttpPost]
        public string Login(string email, string password)
        {
            Logger.log(email + " login here.");
            return Users.Login(email, password);
        }

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        [System.Web.Http.HttpGet]
        public string SessionStatus()
        {
            if (Session["userId"] == null)
            {
                return "false";
            }
            return "true";
        }

        [HttpPost]
        public static List<CardDto> GetConnections(long userId)
        {
            var result = new List<CardDto>();
            foreach (var conn in Users.GetConnections(userId))
            {
                var user = Users.Get(conn.UserB);
                var card = new CardDto();
                card.email = user.Email.ToString();
                card.fullName = user.FullName.ToString();
                card.resumeLink = Users.GetFile(conn.UserB).FileLink;
                card.tagline = user.Tagline.ToString();
                result.Add(card);
            }
            return result;
        }
            
            
        [HttpPost]
        public string GetConnectionsAsString(long userId)
        {
            return serialize(GetConnections(userId));
        }

        public string serialize(List<CardDto> cards)
        {
            var result = "";
            foreach (var card in cards)
            {
                result += card.fullName + "," + card.tagline + "," + card.email + "," + card.resumeLink + "|";
            }
            return result;
        }

        [HttpPost]
        public string GetFile(long userId)
        {
            return Users.GetFile(userId).FileLink;
        }
        
        public static List<CardDto> GetConnectionsMVC(long userId)
        {
            var result = new List<CardDto>();
            foreach (var conn in Users.GetConnections(userId))
            {
                var user = Users.Get(conn.UserB);
                var card = new CardDto();
                card.email = user.Email.ToString();
                card.fullName = user.FullName.ToString();
                card.resumeLink = Users.GetFile(conn.UserB).FileLink;
                card.tagline = user.Tagline.ToString();
                result.Add(card);
            }
            return result;
        }
    }
}