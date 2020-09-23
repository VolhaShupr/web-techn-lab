using lab.DAL.Data;
using lab.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace lab.Services
{
    public class DbInitializer
    {
        public static async Task Seed(ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {

            //создать БД, если она еще не создана 
            context.Database.EnsureCreated();

            //проверка наличия ролей
            if (!context.Roles.Any())
            {
                var roleAdmin = new IdentityRole
                {

                    Name = "admin",
                    NormalizedName = "admin"
                };
                //создать роль admin
                await roleManager.CreateAsync(roleAdmin);
            }

            //проверка наличия пользователей 
            if (!context.Users.Any())
            {
                //создать пользователя user@gmail.com 
                var user = new ApplicationUser
                {

                    Email = "user@gmail.com", UserName = "user@gmail.com"
                };
                await userManager.CreateAsync(user, "111111");

                //создать пользователя admin@gmail.com
                var admin = new ApplicationUser
                {
                    Email = "admin@gmail.com",
                    UserName = "admin@gmail.com"
                };

                await userManager.CreateAsync(admin, "111111");

                //назначить роль admin
                admin = await userManager.FindByEmailAsync("admin@gmail.com");

                await userManager.AddToRoleAsync(admin, "admin");
            }

            ////проверка наличия групп объектов
            if (!context.MusInstrumentGroups.Any())
            {
                context.MusInstrumentGroups.AddRange(new List<MusInstrumentGroup>
                {
                    new MusInstrumentGroup { Name="Guitars" },
                    new MusInstrumentGroup { Name="Keys" },
                    new MusInstrumentGroup { Name="Winds" },
                    new MusInstrumentGroup { Name="Drums" },
                    new MusInstrumentGroup { Name="Strings" },
                    new MusInstrumentGroup { Name="Percursion" }
                });

                await context.SaveChangesAsync();
            }

            //проверка наличия объектов 
            if (!context.MusInstruments.Any())
            {
                context.MusInstruments.AddRange(new List<MusInstrument>
                {
                    new MusInstrument {
                        Name="Kalimba",
                        Description="17 keys kalimba",
                        Brand = "VBH", GroupId=6, Image="kalimba.png",
                        Price = 20
                    },
                    new MusInstrument {
                        Name="Octopus soprano ukulele - sky blue",
                        Description="Fitted with Aquila Nylgut strings",
                        Brand = "Octopus", GroupId=1, Image="ukulele.jpeg",
                        Price = 50.99
                    },
                    new MusInstrument {
                        Name="Hangpan Drum HD1",
                        Description="MEINL Sonic Energy Harmonic Art Handpan instruments are hand made one at a time using top grade german steel.",
                        Brand = "Meinl", GroupId=4, Image="hangpan.png",
                        Price = 1200
                    },
                    new MusInstrument {
                        Name="Hurdy Gurdy w/Gig Bag",
                        Description="Amaze your friends with the mechanical operation of the Hurdy Gurdy",
                        Brand ="hurdfin", GroupId=5, Image="hurdy_gurdy.jpg",
                        Price = 1399
                    },
                    new MusInstrument {
                        Name="Rainstick",
                        Description="The Meinl RS1BK-L Rainstick is a rainstick, made from bamboo, and comes in black. Natural Bamboo makes these rainsticks produce a pleasant effect offering plenty of projection and a long sustain. They can also be played as if they are large shakers.",
                        Brand ="Meinl", GroupId=6, Image="rainstick.jpg",
                        Price = 30
                    }

                });

                await context.SaveChangesAsync();
            }
        }

    }
}
