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
    [GeneratedCodeAttribute("EF.Reverse.POCO.Generator", "2.14.2.0")]
    public class FakePlannerDbContext : IPlannerDbContext
    {
        public IDbSet<Connection> Connections { get; set; }
        public IDbSet<File> Files { get; set; }
        public IDbSet<Handshake> Handshakes { get; set; }
        public IDbSet<Log> Logs { get; set; }
        public IDbSet<Shake> Shakes { get; set; }
        public IDbSet<sys_DatabaseFirewallRule> sys_DatabaseFirewallRule { get; set; }
        public IDbSet<User> Users { get; set; }

        public FakePlannerDbContext()
        {
            Connections = new FakeDbSet<Connection>();
            Files = new FakeDbSet<File>();
            Handshakes = new FakeDbSet<Handshake>();
            Logs = new FakeDbSet<Log>();
            Shakes = new FakeDbSet<Shake>();
            sys_DatabaseFirewallRule = new FakeDbSet<sys_DatabaseFirewallRule>();
            Users = new FakeDbSet<User>();
        }

        public int SaveChanges()
        {
            return 0;
        }

        public Task<int> SaveChangesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        protected virtual void Dispose(bool disposing)
        {
        }
        
        public void Dispose()
        {
            Dispose(true);
        }
        
        // Stored Procedures
    }
}
