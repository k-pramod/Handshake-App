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
    // Users
    internal class UserConfiguration : EntityTypeConfiguration<User>
    {
        public UserConfiguration(string schema = "dbo")
        {
            ToTable(schema + ".Users");
            HasKey(x => new { x.Id, x.Email, x.Type, x.Password, x.Tagline });

            Property(x => x.Id).HasColumnName("id").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.Email).HasColumnName("email").IsRequired().HasMaxLength(128);
            Property(x => x.Type).HasColumnName("type").IsRequired().HasMaxLength(128);
            Property(x => x.Password).HasColumnName("password").IsRequired().HasMaxLength(128);
            Property(x => x.Tagline).HasColumnName("tagline").IsRequired().HasMaxLength(128);
            Property(x => x.FullName).HasColumnName("full_name").IsOptional().HasMaxLength(128);
        }
    }

}
