using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Configuration
{
    public class RoleConfiguration : IEntityTypeConfiguration<IdentityRole>
    {
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            builder.HasData(
            new IdentityRole
            {
                Id = "1a2b3c4d-0000-0000-0000-000000000001",
                Name = "Manager",
                NormalizedName = "MANAGER"
            },
            new IdentityRole
            {
                Id = "1a2b3c4d-0000-0000-0000-000000000002",
                Name = "Administrator",
                NormalizedName = "ADMINISTRATOR"
            }
            );
        }
    }
}
