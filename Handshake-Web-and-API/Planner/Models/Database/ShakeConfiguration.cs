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
    // Shakes
    internal class ShakeConfiguration : EntityTypeConfiguration<Shake>
    {
        public ShakeConfiguration(string schema = "dbo")
        {
            ToTable(schema + ".Shakes");
            HasKey(x => new { x.Id, x.UserId, x.ShakeTime, x.Status });

            Property(x => x.Id).HasColumnName("id").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.UserId).HasColumnName("user_id").IsRequired();
            Property(x => x.ShakeTime).HasColumnName("shake_time").IsRequired();
            Property(x => x.Location).HasColumnName("location").IsOptional().HasMaxLength(64);
            Property(x => x.Status).HasColumnName("status").IsRequired().IsFixedLength().HasMaxLength(12);
        }
    }

}
