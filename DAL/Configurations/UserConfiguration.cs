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
                .HasForeignKey(b => b.UserGroupId);

            builder
                .HasOne(b => b.UserState)
                .WithMany(i => i.Users)
                .HasForeignKey(b => b.UserStateId);

            builder.ToTable("User");
        }
    }
}