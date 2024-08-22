using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Projection.Identity.Domain.Entities;

namespace Projection.Identity.Infrastructure.Data;

internal static partial class Masterdata
{
    public static void SeedUsingMigration(ModelBuilder builder)
    {
        builder.SeedDefaultIdentityRoles();
        builder.SeedDefaultUsers();
        builder.SeedDefaultUserRoles();
    }


    /// <summary>
    /// Creates and saves default users
    /// </summary>
    /// <param name="builder"></param>
    private static void SeedDefaultUsers(this ModelBuilder builder)
    {
        PasswordHasher<ApplicationUser> _passwordHasher = new();

        List<ApplicationUser> users =
        [
            new ApplicationUser(createdDate: new DateTime(2023, 1, 1, 0, 0, 0, DateTimeKind.Utc))
            {
                FirstName = "SysAdmin",
                LastName = "@Projection",
                DateOfBirth = new DateTime(2023, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                Email = "admin.projection@hotmail.com",
                UserName = "admin.projection@hotmail.com",
                PhoneNumber = "+919888888888",
                NormalizedUserName = "admin.projection@hotmail.com".ToUpper(),
                NormalizedEmail = "admin.projection@hotmail.com".ToUpper(),
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                PasswordHash = "projection@2023", // This is actual password. DO NOT use this password directly. We need to hash it before storing it.
                SecurityStamp = "124d2334-c5f6-4163-922c-7c6cf18833e1",
                ConcurrencyStamp = "59067e57-3de6-4034-a2b6-5525d5836a63",
                Id = "c0aab6ba-cd71-4010-a9dc-e246997d6183",
            },
        ];

        users.ForEach(user =>
        {
            user.PasswordHash = _passwordHasher.HashPassword(user, user.PasswordHash!);
            builder.Entity<ApplicationUser>().HasData(user);
        });
    }

    private static void SeedDefaultIdentityRoles(this ModelBuilder builder)
    {
        List<IdentityRole> roles =
        [
            new IdentityRole
            {
                Id = "117dc0ff-4d12-4a15-8c6b-77e9b903cd63",
                Name = "Administrator",
                NormalizedName = "ADMINISTRATOR",
                ConcurrencyStamp = "b4ce6f74-e846-49bf-ae69-88d0cd2de9a0",

            },
            new IdentityRole
            {
                Id = "01ce8033-ff7b-49d4-b948-d024771649b9",
                Name = "Owner",
                NormalizedName = "OWNER",
                ConcurrencyStamp = "7078cee2-f743-4edc-b273-8bea3f6b3a54",
            },
            new IdentityRole
            {
                Id = "16d881f1-e7ee-4735-85b1-c21d2ba8459c",
                Name = "User",
                NormalizedName = "USER",
                ConcurrencyStamp = "6a96165a-2781-4d5a-bbdd-a65157f7be6e",
            },
            new IdentityRole
            {
                Id = "227f91b9-b8ad-4bdc-b061-02f0c7e67c70",
                Name = "Sysadmin",
                NormalizedName = "SYSADMIN",
                ConcurrencyStamp = "26b43777-014f-45dc-ab30-20f0a115e41f",
            }
        ];

        roles.ForEach(role => builder.Entity<IdentityRole>().HasData(role));
    }

    private static void SeedDefaultUserRoles(this ModelBuilder builder)
    {
        List<IdentityUserRole<string>> userRoles =
        [
            new IdentityUserRole<string>
            {
                UserId = "c0aab6ba-cd71-4010-a9dc-e246997d6183",
                RoleId = "117dc0ff-4d12-4a15-8c6b-77e9b903cd63",
            },
            new IdentityUserRole<string>
            {
                UserId = "c0aab6ba-cd71-4010-a9dc-e246997d6183",
                RoleId = "227f91b9-b8ad-4bdc-b061-02f0c7e67c70",
            }
        ];

        userRoles.ForEach(userRole => builder.Entity<IdentityUserRole<string>>().HasData(userRole));
    }

}