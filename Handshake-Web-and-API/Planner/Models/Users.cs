using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web.Mvc;
using Planner.Models.Database;

namespace Planner.Models
{
    public class Users
    {
        [HttpPost]
        public static long Create(string email, string password, string type, string tagline, string fullName)
        {
            using (var dbContext = new PlannerDbContext())
            {
                var user = new User
                {
                    Email = email,
                    Password = password,
                    Type = type,
                    Tagline = tagline,
                    FullName = fullName
                };
                dbContext.Users.Add(user);
                try
                {
                    dbContext.SaveChanges();
                    return user.Id;
                }
                catch (DbEntityValidationException exception)
                {
                    var s = "";
                    foreach (var eve in exception.EntityValidationErrors)
                    {
                        s +=
                            string.Format(
                                "Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                                eve.Entry.Entity.GetType().Name, eve.Entry.State);
                        foreach (var ve in eve.ValidationErrors)
                        {
                            s += string.Format("- Property: \"{0}\", Error: \"{1}\"",
                                ve.PropertyName, ve.ErrorMessage);
                        }
                    }
                    return -1;
                }
            }
        }

        [HttpPost]
        public static string Login(string email, string password)
        {
            Logger.log("Login now");
            using (var dbContext = new PlannerDbContext())
            {
                var user =
                    dbContext
                        .Set<User>()
                        .FirstOrDefault(_ => _.Email.Equals(email) && _.Password.Equals(password));
                if (user == null)
                {
                    return (-1).ToString();
                }
                return user.Id + ", " + user.Type;
            }
        }

        public static User Get(long userId)
        {
            using (var dbContext = new PlannerDbContext())
            {
                dbContext.Configuration.ProxyCreationEnabled = false;
                return dbContext.Set<User>().FirstOrDefault(_ => _.Id == userId);
            }
        }

        public static File GetFile(long userId)
        {
            using (var dbContext = new PlannerDbContext())
            {
                dbContext.Configuration.ProxyCreationEnabled = false;
                return dbContext.Set<File>().FirstOrDefault(_ => _.UserId == userId);
            }
        }

        /*public static void UpdatePassword(long userId, string newPassword)
        {
            using (var dbContext = new PlannerDbContext())
            {
                using (var dbTransaction = dbContext.Database.BeginTransaction())
                {
                    try
                    {
                        var userPassword = dbContext.Set<UserPassword>().Where(_ => _.UserId == userId).FirstOrDefault();
                        if (userPassword == null)
                        {
                            return;
                        }

                        var salt = Crypt.GenerateSalt();
                        userPassword.PasswordSalt = salt;
                        userPassword.PasswordHash = Crypt.GenerateHash(newPassword, salt);
                        userPassword.Updated = DateTime.UtcNow;

                        dbContext.Entry(userPassword).State = EntityState.Modified;
                        dbContext.SaveChanges();

                        dbTransaction.Commit();
                    }
                    catch (Exception)
                    {
                        dbTransaction.Rollback();
                    }
                }
            }
        }*/

        public static IEnumerable<Handshake> GetOwnedHandshakesFromReciever(long userId)
        {
            using (var dbContext = new PlannerDbContext())
            {
                dbContext.Configuration.ProxyCreationEnabled = false;
                var query = from _ in dbContext.Handshakes
                    where (_.PrimaryUserId == userId)
                    select _;

                return query.ToList();
            }
        }

        public static IEnumerable<Connection> GetConnections(long userId)
        {
            using (var dbContext = new PlannerDbContext())
            {
                dbContext.Configuration.ProxyCreationEnabled = false;
                var query = from _ in dbContext.Connections
                            where (_.UserA == userId)
                            where (_.UserB != userId)
                            select _;

                return query.ToList();
            }
        } 

        public static IEnumerable<Handshake> GetOwnedHandshakesFromSender(long userId)
        {
            using (var dbContext = new PlannerDbContext())
            {
                dbContext.Configuration.ProxyCreationEnabled = false;
                var query = from _ in dbContext.Handshakes
                    where (_.SecondaryUserId == userId)
                    select _;

                return query.ToList();
            }
        }
    }
}