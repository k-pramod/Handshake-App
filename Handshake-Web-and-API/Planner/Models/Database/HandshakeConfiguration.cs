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
    // Handshakes
    internal class HandshakeConfiguration : EntityTypeConfiguration<Handshake>
    {
        public HandshakeConfiguration(string schema = "dbo")
        {
            ToTable(schema + ".Handshakes");
            HasKey(x => x.Id);

            Property(x => x.Id).HasColumnName("id").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.PrimaryUserId).HasColumnName("primary_user_id").IsOptional();
            Property(x => x.SecondaryUserId).HasColumnName("secondary_user_id").IsOptional();
            Property(x => x.TryHsUserId).HasColumnName("try_hs_user_id").IsOptional();
            Property(x => x.HsDatetime).HasColumnName("hs_datetime").IsOptional();
            Property(x => x.Location).HasColumnName("location").IsOptional().HasMaxLength(64);
        }
    }

}
