using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web.WebPages;
using Planner.Models.Database;
using Planner.Models.DTO;

namespace Planner.Models
{
    public class Handshakes
    {
        public static DateTime recordShake(long userId)
        {
            var now = DateTime.Now;
            using (var dbContext = new PlannerDbContext())
            {
                var shake = new Shake
                {
                    UserId = userId,
                    ShakeTime = now,
                    Status = "OPEN",
                };
                dbContext.Shakes.Add(shake);
                try
                {
                    dbContext.SaveChanges();
                    return now;
                }
                catch (DbEntityValidationException exception)
                {
                    return "0001-01-01".AsDateTime();
                }
            }
        }

        public static List<Shake> getAllInTimeRange(DateTime start, DateTime end, long userId)
        {
            using (var dbContext = new PlannerDbContext())
            {
                dbContext.Configuration.ProxyCreationEnabled = false;
                var query = from _ in dbContext.Shakes
                            where (_.ShakeTime >= start)
                            where (_.ShakeTime <= end)
                            where (_.Status == "OPEN")
                            where (_.UserId != userId)
                            orderby (_.ShakeTime)
                            select _;

                return query.ToList();
            }
        }

        public static long findPair(long searchingUserId)
        {
            var enterTime = recordShake(searchingUserId);
            var viable = getAllInTimeRange(enterTime.AddSeconds(-5), enterTime.AddSeconds(5), searchingUserId);
            var mostRecentIndex = viable.Count - 1;
            var match = ((mostRecentIndex < 0) ? -1 : viable[mostRecentIndex].UserId);
            return match;
        }

        public static bool makeConnection(long one, long two)
        {
            //setAsPaired(one);
            //setAsPaired(two);
            
            using (var dbContext = new PlannerDbContext())
            {
                var connection = new Connection
                {
                    UserA = one,
                    UserB = two
                };
                dbContext.Connections.Add(connection);
                try
                {
                    dbContext.SaveChanges();
                    return true;
                }
                catch (DbEntityValidationException exception)
                {
                    return false;
                }
            }
        }

        public static void setAsPaired(long userId)
        {
            using (var dbContext = new PlannerDbContext())
            {
                dbContext.Configuration.ProxyCreationEnabled = false;
                var query = from _ in dbContext.Shakes
                            where (_.Status == "OPEN")
                            where (_.UserId != userId)
                            orderby (_.ShakeTime)
                            select _;

                var list = query.ToList();
                var toSetAsPaired = list[list.Count - 1];

                var newShake = new Shake
                {
                    Id = toSetAsPaired.Id,
                    Location = toSetAsPaired.Location,
                    UserId = userId,
                    ShakeTime = toSetAsPaired.ShakeTime,
                    Status = "PAIRED"
                };

                dbContext.Shakes.Attach(newShake);
                dbContext.Entry(newShake).State = EntityState.Modified;
                dbContext.SaveChanges();
            }
        }
    }
}