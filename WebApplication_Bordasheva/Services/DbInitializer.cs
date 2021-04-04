using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using WebLab4.Data;
using WebLab4.Entities;

namespace WebApplication_Bordasheva.Services
{
    public class DbInitializer
    {
        public static async Task Seed(ApplicationDbContext context,
                            UserManager<ApplicationUser> userManager,
                            RoleManager<IdentityRole> roleManager)
        {
            // создать БД, если она еще не создана 
            context.Database.EnsureCreated();

            // проверка наличия ролей 
            if (!context.Roles.Any())
            {
                var roleAdmin = new IdentityRole
                {
                    Name = "admin",
                    NormalizedName = "admin"
                };
                // создать роль admin 
                await roleManager.CreateAsync(roleAdmin);
            }

            // проверка наличия пользователей 
            if (!context.Users.Any())
            {
                // создать пользователя user@mail.ru 
                var user = new ApplicationUser
                {
                    Email = "user@mail.ru",
                    UserName = "user@mail.ru"
                };
                await userManager.CreateAsync(user, "123456");
                // создать пользователя admin@mail.ru 
                var admin = new ApplicationUser
                {
                    Email = "admin@mail.ru",
                    UserName = "admin@mail.ru"
                };
                await userManager.CreateAsync(admin, "123456");
                // назначить роль admin 
                admin = await userManager.FindByEmailAsync("admin@mail.ru");
                await userManager.AddToRoleAsync(admin, "admin");
            }
            //проверка наличия групп объектов 
            if (!context.PhoneGroups.Any())
            {
                context.PhoneGroups.AddRange(
                new List<PhoneGroup>
                {
                    new PhoneGroup {GroupName="Apple"},
                    new PhoneGroup {GroupName="Honor"},
                    new PhoneGroup {GroupName="Pixel"},
                    new PhoneGroup {GroupName="Samsung"},
                    new PhoneGroup {GroupName="Xiaomi"},
                    new PhoneGroup {GroupName="Nokia"}
                });
                await context.SaveChangesAsync();
            }

            // проверка наличия объектов 
            if (!context.Phones.Any())
            {
                context.Phones.AddRange(
                new List<Phone>
                {
                    new Phone {PhoneName="Apple iPhone 11 64GB",
                        Description="Apple iOS, экран 6.1 IPS (828x1792)",
                        Price =1840, PhoneGroupId=1, Image="Apple.png" },
                    new Phone {PhoneName="HONOR 10X Lite",
                        Description="Android, экран 6.67 IPS (1080x2400)",
                        Price =529, PhoneGroupId=2, Image="Honor.png" },
                    new Phone {PhoneName="Google Pixel 4a",
                        Description="Android, экран 5.8 OLED (1080x2340)",
                        Price =1290, PhoneGroupId=3, Image="Pixel.png" },
                    new Phone {PhoneName="Samsung Galaxy M31",
                        Description="Android, экран 6.4 AMOLED (1080x2340)",
                        Price =660, PhoneGroupId=4, Image="Samsung.png" },
                    new Phone {PhoneName="Xiaomi Redmi 9",
                        Description="Android, экран 6.53 IPS (1080x2340)",
                        Price =470, PhoneGroupId=5, Image="Xiaomi.png" },
                    new Phone {PhoneName="Nokia 105",
                        Description="экран 1.77 TFT (120x160)",
                        Price =49, PhoneGroupId=6, Image="Nokia.png" }
                });
                await context.SaveChangesAsync();
            }


        }

    }
}
