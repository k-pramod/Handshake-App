using System.Data.Entity.Validation;
using Planner.Models.Database;

namespace Planner.Models
{
    public class Files
    {
        public static bool Add(string fileAsString, long userId)
        {
            using (var dbContext = new PlannerDbContext())
            {
                var file = new File
                {
                    FileLink = fileAsString,
                    UserId = userId
                };
                dbContext.Files.Add(file);
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
    }
}