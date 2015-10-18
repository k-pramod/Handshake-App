using System;
using System.Web.Mvc;
using Planner.Models;
using Planner.Models.Database;

namespace Planner.Controllers
{
    public class HandshakesController : Controller
    {
        [HttpPost]
        public bool Log(long userId)
        {
            Logger.log(userId.ToString());
            makeConnection(userId);
            return true;
        }

        [HttpPost]
        public long findPair(long userId)
        {
            return Handshakes.findPair(userId);
        }

        public bool makeConnection(long userId)
        {
            var other = findPair(userId);
            if (other < 0 || userId < 0)
            {
                return false;
            }
            Handshakes.makeConnection(userId, other);
            Handshakes.makeConnection(other, userId);
            return true;
        }
    }
}