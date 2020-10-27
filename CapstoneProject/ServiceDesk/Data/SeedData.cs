using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ServiceDesk.Models;

namespace ServiceDesk.Data
{
    public class SeedData
    {
        private static Technician[] _technicians = {
            new Technician {
                FirstName = "Theodore",
                LastName = "Pollock",
                IsAdmin = true
            },
            new Technician {
                FirstName = "Jason",
                LastName = "Fisher",
                IsAdmin = true
            },
            new Technician {
                FirstName = "Micheal",
                LastName = "McClain",
                IsAdmin = true
            },
            new Technician {
                FirstName = "Rosa",
                LastName = "Lopez",
                IsAdmin = false
            },
            new Technician {
                FirstName = "Nancy",
                LastName = "Mays",
                IsAdmin = false
            },
            new Technician {
                FirstName = "Ashely",
                LastName = "McCormik",
                IsAdmin = false
            },
            new Technician {
                FirstName = "Kiero",
                LastName = "Ashuno",
                IsAdmin = false
            },
            new Technician {
                FirstName = "Joe",
                LastName = "Yaller",
                IsAdmin = false
            },
            new Technician {
                FirstName = "Preston",
                LastName = "Gaul",
                IsAdmin = false
            },
            new Technician {
                FirstName = "Kira",
                LastName = "Yagashi",
                IsAdmin = false
            },
            new Technician {
                FirstName = "Joesph",
                LastName = "Brown",
                IsAdmin = false
            },
            new Technician {
                FirstName = "Ryan",
                LastName = "Wheeland",
                IsAdmin = false
            },
            new Technician {
                FirstName = "Lindsey",
                LastName = "Fencil",
                IsAdmin = false
            },
            new Technician {
                FirstName = "Mark",
                LastName = "Fencil",
                IsAdmin = false
            },
            new Technician {
                FirstName = "Rori",
                LastName = "Flari",
                IsAdmin = false
            }
        };

        private static Employee[] _employee = {
            new Employee {
                Address = "Building 3 Room 11",
                Department = "Billing",
                EmailAddress = "Salgado@TulsaPreTech.com",
                FirstName = "Elizebeth",
                LastName = "Salgado",
                PhoneNumber = "#5311"
            },
            new Employee {
                Address = "Building 3 Room 17",
                Department = "Shipping",
                EmailAddress = "Streater@TulsaPreTech.com",
                FirstName = "Maddie",
                LastName = "Streater",
                PhoneNumber = "#0017"
            },
            new Employee {
                Address = "Building 1 Room 145",
                Department = "Legal",
                EmailAddress = "Noffsinger@TulsaPreTech.com",
                FirstName = "Chrissy",
                LastName = "Noffsinger",
                PhoneNumber = "#6145"
            },
            new Employee {
                Address = "Building 2 Room 489",
                Department = "Development",
                EmailAddress = "Max@TulsaPreTech.com",
                FirstName = "Eufemia",
                LastName = "Max",
                PhoneNumber = "#9499"
            },
            new Employee {
                Address = "Building 3 Room 104 ",
                Department = "Administrative",
                EmailAddress = "Honea@TulsaPreTech.com",
                FirstName = "Teresa",
                LastName = "Honea",
                PhoneNumber = "#8524"
            },
            new Employee {
                Address = "Building 3 Room 105",
                Department = "Administrative",
                EmailAddress = "Haydon@TulsaPreTech.com",
                FirstName = "Kendrick",
                LastName = "Haydon",
                PhoneNumber = "#2636"
            },
            new Employee {
                Address = "Building 2 Room 15",
                Department = "Administrative",
                EmailAddress = "Bernardo@TulsaPreTech.com",
                FirstName = "Napoleon",
                LastName = "Bernardo",
                PhoneNumber = "#7485"
            },
            new Employee {
                Address = "Building 1 Room 323",
                Department = "Human Resources",
                EmailAddress = "Rigdon@TulsaPreTech.com",
                FirstName = "Jule",
                LastName = "Rigdon",
                PhoneNumber = "#6705"
            },
            new Employee {
                Address = "Building 2 Room 25",
                Department = "Human Resources",
                EmailAddress = "Spady@TulsaPreTech.com",
                FirstName = "Michaela",
                LastName = "Spady",
                PhoneNumber = "#2502"
            },
            new Employee {
                Address = "Building 2 Room 23",
                Department = "Human Resources",
                EmailAddress = "Y-Raley@TulsaPreTech.com",
                FirstName = "Derek",
                LastName = "Raley",
                PhoneNumber = "#2437"
            },
            new Employee {
                Address = "Building 3 Room ",
                Department = "Billing",
                EmailAddress = "Messineo@TulsaPreTech.com",
                FirstName = "Lindsy",
                LastName = "Messineo",
                PhoneNumber = "#7653"
            },
            new Employee {
                Address = "Building 2 Room 904",
                Department = "Administrative",
                EmailAddress = "Strohm@TulsaPreTech.com",
                FirstName = "Reggie",
                LastName = "Strohm",
                PhoneNumber = "#3677"
            },
            new Employee {
                Address = "Building 2 Room 151",
                Department = "Administrative",
                EmailAddress = "Troia@TulsaPreTech.com",
                FirstName = "Sheilah",
                LastName = "Troia",
                PhoneNumber = "#9767"
            },
            new Employee {
                Address = "Building 1 Room 298",
                Department = "Legal",
                EmailAddress = "Modesto@TulsaPreTech.com",
                FirstName = "Vivien",
                LastName = "Modesto",
                PhoneNumber = "#7798"
            },
            new Employee {
                Address = "Building 1 Room 287",
                Department = "Legal",
                EmailAddress = "Days@TulsaPreTech.com",
                FirstName = "Evia",
                LastName = "Days",
                PhoneNumber = "#3746"
            }
        };

        /// <summary>
        /// Initializes the ticket system with data
        /// </summary>
        /// <param name="context">context</param>
        /// <param name="userManager">admin</param>
        /// <param name="roleManager"></param>
        public static void Initialize(ServiceDeskContext context, UserManager<Technician> userManager, RoleManager<IdentityRole> roleManager)
        {
            context.Database.EnsureDeleted();
            context.Database.Migrate();

            var randGenerator = new Random();

            foreach (var employee in _employee)
            {
                employee.DateAdded = DateTime.Now.AddMonths(randGenerator.Next(-24, -12));
            }

            var role = roleManager.FindByNameAsync(DataConstants.AdministratorRole).Result;
            if (role == null)
            {
                roleManager.CreateAsync(new IdentityRole(DataConstants.AdministratorRole));
            }

            foreach (var technician in _technicians)
            {
                technician.DateAdded = DateTime.Now.AddMonths(randGenerator.Next(-36, -25));
                technician.UserName = $"{technician.FirstName}.{technician.LastName}";
                userManager.CreateAsync(technician, "password").Wait();
                if (technician.IsAdmin)
                {
                    userManager.AddToRoleAsync(technician, DataConstants.AdministratorRole);
                }
            }

            context.Employees.AddRange(_employee);

            context.SaveChanges();

            foreach (var employee in context.Employees)
            {
                var ticketCount = randGenerator.Next(0, 15);
                for (var i = 0; i < ticketCount; i++)
                {
                    context.Tickets.Add(new Ticket
                    {
                        EmployeeId = employee.Id,
                        Title = $"{employee.Department}: Case {i}",
                        Description = $"Super awesome ticket {i}",
                        Complexity = i % 3 + 1,
                        IsUrgent = randGenerator.Next(0, 5) == 0,
                        Notes = "Terrible Employee to work with",
                        Open = randGenerator.Next(0, 2) == 0,
                        DateAdded = DateTime.Now.AddMonths(randGenerator.Next(-24, -12))
                    });
                }
            }

            context.SaveChanges();

            foreach (var ticket in context.Tickets)
            {
                var workTimesCount = randGenerator.Next(0, 10);
                for (var i = 0; i < workTimesCount; i++)
                {
                    var start = ticket.DateAdded.AddHours(randGenerator.Next(1, 60));
                    var end = start.AddMinutes(randGenerator.Next(15, 60));
                    context.TechnicianTicketTimes.Add(new TechnicianTicketTime
                    {
                        Start = start,
                        End = end,
                        TechnicianId = context.Users.OrderBy(t => Guid.NewGuid()).Take(1).First().UserName,
                        TicketId = ticket.Id
                    });
                }
            }

            context.SaveChanges();
        }
    }
}