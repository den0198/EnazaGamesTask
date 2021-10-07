using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Models.Entities;

namespace DAL.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder
                .HasOne(b => b.UserGroup)
                .WithMany(i => i.Users)
                .HasForeignKey(i => i.UserGroupId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.ToTable("User");
        }
    }
}