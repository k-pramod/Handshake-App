// ReSharper disable RedundantUsingDirective
// ReSharper disable DoNotCallOverridableMethodsInConstructor
// ReSharper disable InconsistentNaming
// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable PartialMethodWithSinglePart
// ReSharper disable RedundantNameQualifier
// TargetFrameworkVersion = 4.51
#pragma warning disable 1591    //  Ignore "Missing XML Comment" warning

using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Data.Entity.ModelConfiguration;
using System.Threading;
using System.Threading.Tasks;
using DatabaseGeneratedOption = System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption;

namespace Planner.Models.Database
{
    public class PlannerDbContext : DbContext, IPlannerDbContext
    {
        public IDbSet<Connection> Connections { get; set; } // Connections
        public IDbSet<File> Files { get; set; } // Files
        public IDbSet<Handshake> Handshakes { get; set; } // Handshakes
        public IDbSet<Log> Logs { get; set; } // Logs
        public IDbSet<Shake> Shakes { get; set; } // Shakes
        public IDbSet<sys_DatabaseFirewallRule> sys_DatabaseFirewallRule { get; set; } // database_firewall_rules
        public IDbSet<User> Users { get; set; } // Users
        
        static PlannerDbContext()
        {
            System.Data.Entity.Database.SetInitializer<PlannerDbContext>(null);
        }

        public PlannerDbContext()
            : base("Name=Planner")
        {
        }

        public PlannerDbContext(string connectionString) : base(connectionString)
        {
        }

        public PlannerDbContext(string connectionString, System.Data.Entity.Infrastructure.DbCompiledModel model) : base(connectionString, model)
        {
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Configurations.Add(new ConnectionConfiguration());
            modelBuilder.Configurations.Add(new FileConfiguration());
            modelBuilder.Configurations.Add(new HandshakeConfiguration());
            modelBuilder.Configurations.Add(new LogConfiguration());
            modelBuilder.Configurations.Add(new ShakeConfiguration());
            modelBuilder.Configurations.Add(new sys_DatabaseFirewallRuleConfiguration());
            modelBuilder.Configurations.Add(new UserConfiguration());
        }

        public static DbModelBuilder CreateModel(DbModelBuilder modelBuilder, string schema)
        {
            modelBuilder.Configurations.Add(new ConnectionConfiguration(schema));
            modelBuilder.Configurations.Add(new FileConfiguration(schema));
            modelBuilder.Configurations.Add(new HandshakeConfiguration(schema));
            modelBuilder.Configurations.Add(new LogConfiguration(schema));
            modelBuilder.Configurations.Add(new ShakeConfiguration(schema));
            modelBuilder.Configurations.Add(new sys_DatabaseFirewallRuleConfiguration(schema));
            modelBuilder.Configurations.Add(new UserConfiguration(schema));
            return modelBuilder;
        }
        
        // Stored Procedures
    }
}
