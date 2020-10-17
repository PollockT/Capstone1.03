using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using ServiceDesk.Models;
using ServiceDesk.Models.EmployeeViewModels;
using ServiceDesk.Data;

namespace ServiceDesk.Controllers
{
    /// <summary>
    /// Controller for Clients
    /// </summary>
    [Authorize]
    public partial class EmployeeController : Controller
    {
        private ServiceDeskContext _context;

        /// <summary>
        /// Initializes _context
        /// </summary>
        /// <param name="context">context of client</param>
        public EmployeeController(ServiceDeskContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Shows all employees
        /// </summary>
        /// <returns>clients page</returns>
        [HttpGet]
        public async Task<IActionResult> All()
        {
            var employees = await _context.Employee.GroupJoin(_context.Ticket.Where(ticket => ticket.Open), employee => employee.Id, 
                ticket => ticket.EmployeeId, (employee, ticket) => new EmployeeDetails { Employee = employee, Ticket = ticket, OpenTicketCount = ticket.Count() }
                ).OrderByDescending(details => details.Ticket.Count()).ToListAsync();
            return View(employees);
        }

        /// <summary>
        /// Opens the employee's details
        /// </summary>
        /// <param name="id">The id of the employee</param>
        /// <returns>The employee</returns>
        [HttpGet]
        public async Task<IActionResult> Open([FromRoute] Guid id)
        {
            var employee = await _context.Employee.FindAsync(id);
            var tickets = await _context.Ticket.Where(ticket => ticket.EmployeeId == id).ToListAsync();

            var details = new EmployeeDetails
            {
                Employee = employee,
                OpenTicketCount = tickets.Count,
                Ticket = tickets
            };
            return View(details);
        }

        /// <summary>
        /// Gets the add employee view
        /// </summary>
        /// <returns>The add employee view.</returns>
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        /// <summary>
        /// Adds a client to the database
        /// </summary>
        /// <param name="employee">The employee to add</param>
        /// <returns>The added employee</returns>
        [HttpPost]
        public async Task<IActionResult> Add([FromForm] Employee employee)
        {
            employee.DateAdded = DateTime.Now;
            _context.Employee.Add(employee);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Open), new { id = employee.Id });
        }

        /// <summary>
        /// Gets view for adding a ticket.
        /// </summary>
        /// <returns>The view.</returns>
        [HttpGet]
        public IActionResult AddTicket([FromRoute] Guid id)
        {
            return View(new Ticket { EmployeeId = id });
        }

        /// <summary>
        /// Adds a ticket
        /// </summary>
        /// <param name="ticket">The ticket to be added</param>
        /// <returns>The added ticket</returns>
        [HttpPost]
        public async Task<IActionResult> AddTicket([FromForm] Ticket ticket)
        {
            ticket.DateAdded = DateTime.Now;
            ticket.IsUrgent = false;
            ticket.Open = true;

            _context.Ticket.Add(ticket);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(TicketController.Open), "Tickets", new { id = ticket.Id });
        }
    }
}
