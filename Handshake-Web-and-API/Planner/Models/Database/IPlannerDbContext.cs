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
    public interface IPlannerDbContext : IDisposable
    {
        IDbSet<Connection> Connections { get; set; } // Connections
        IDbSet<File> Files { get; set; } // Files
        IDbSet<Handshake> Handshakes { get; set; } // Handshakes
        IDbSet<Log> Logs { get; set; } // Logs
        IDbSet<Shake> Shakes { get; set; } // Shakes
        IDbSet<sys_DatabaseFirewallRule> sys_DatabaseFirewallRule { get; set; } // database_firewall_rules
        IDbSet<User> Users { get; set; } // Users

        int SaveChanges();
        Task<int> SaveChangesAsync();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
        
        // Stored Procedures
    }

}
