using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ServiceDesk.Models;
using Microsoft.AspNetCore.Identity;
using System.Data.Common;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ServiceDesk.Data
{
    public class ServiceDeskContext : IdentityDbContext<Technician>
    {
        /// <summary>
        /// The collection of clients
        /// </summary>
        public DbSet<Employee> Employees { get; set; }

        /// <summary>
        /// The collection of tickets
        /// </summary>
        public DbSet<Ticket> Tickets { get; set; }

        /// <summary>
        /// The collection of TechnicianTicket pivot models
        /// </summary>
        public DbSet<TechnicianTicketTime> TechnicianTicketTimes { get; set; }

        /// <summary>
        /// The constructor for this context
        /// </summary>
        /// <param name="options">The options to create the context around</param>
        /// <returns>A new instance of this context</returns>

        public ServiceDeskContext(DbContextOptions<ServiceDeskContext> options) : base(options)
        {
        
        }

    }
}