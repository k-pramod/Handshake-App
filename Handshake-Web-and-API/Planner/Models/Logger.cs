using System;
using System.Data.Entity.Validation;
using Planner.Models.Database;

namespace Planner.Models
{
    public class Logger
    {
        public static long log(string message)
        {
            using (var dbContext = new PlannerDbContext())
            {
                var log = new Log
                {
                    Message = message,
                    Datetime = DateTime.Now
                };
                dbContext.Logs.Add(log);
                try
                {
                    dbContext.SaveChanges();
                    return log.Id;
                }
                catch (DbEntityValidationException exception)
                {
                    return -1;
                }
            }
        }
    }
}