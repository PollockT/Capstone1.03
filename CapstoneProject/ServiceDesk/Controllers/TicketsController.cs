﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using ServiceDesk.Data;
using ServiceDesk.Models;
using ServiceDesk.Models.TicketsViewModels;

namespace ServiceDesk.Controllers
{
    /// <summary>
    /// Controller for managing tickets
    /// </summary>
    [Authorize]
    public class TicketsController : Controller
    {
        private ServiceDeskContext _context;

        private UserManager<Technician> _userManager;

        /// <summary>
        /// Initializes private variable _context
        /// </summary>
        /// <param name="context">context of current ticket</param>
        /// <param name="userManager">The user manager</param>
        public TicketsController(ServiceDeskContext context, UserManager<Technician> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        /// <summary>
        /// Used to view all tickets in queue
        /// </summary>
        /// <param name="includeClosed">boolean for including closed tickets</param>
        /// <returns>view list of ordered tickets</returns>
        [HttpGet]
        public async Task<IActionResult> All([FromQuery] bool includeClosed = false)
        {
            var orderedTickets = await _context.Tickets
                .OrderByDescending(ticket => ticket.DateAdded)
                .GroupBy(ticket => ticket.EmployeeId)
                .OrderBy(ticketEmployeeGroup => ticketEmployeeGroup.Count())
                .SelectMany(ticketClientGroup => ticketClientGroup)
                .Where(ticket => ticket.Open || ticket.Open != includeClosed)
                .OrderByDescending(ticket => ticket.IsUrgent)
                .OrderByDescending(ticket => ticket.Open)
                .ToListAsync();

            ViewData["includeClosed"] = includeClosed;

            return View(orderedTickets);
        }

        /// <summary>
        /// Opens a ticket
        /// </summary>
        /// <param name="id">unique id of ticket</param>
        /// <returns>view of the ticket</returns>
        [HttpGet]
        public async Task<IActionResult> Open([FromRoute] Guid id)
        {
            var ticket = await _context.Tickets.FindAsync(id);
            var employee = await _context.Employees.FindAsync(ticket.EmployeeId);
            var times = await _context.TechnicianTicketTimes.Where(time => time.TicketId == ticket.Id).Join(_context.Users, time => time.TechnicianId, tech => tech.UserName, (time, tech) => new TechnicianTime { Technician = tech, Time = time }).ToListAsync();
            return View(new TicketDetails { Ticket = ticket, Employee = employee, Times = times });
        }

        /// <summary>
        /// Open a ticket for editiing
        /// </summary>
        /// <param name="id">unique id of ticket</param>
        /// <returns>view of the ticket to edit</returns>
        [HttpGet]
        public async Task<IActionResult> Edit([FromRoute] Guid id)
        {
            var ticket = await _context.Tickets.FindAsync(id);
            return View(ticket);
        }

        /// <summary>
        /// Updates a ticket.
        /// </summary>
        /// <param name="ticketUpdate">The ticket update.</param>
        /// <returns>The ticket view</returns>
        [HttpPost]
        public async Task<IActionResult> Edit([FromForm] Ticket ticketUpdate)
        {
            var ticket = await _context.Tickets.FindAsync(ticketUpdate.Id);

            ticket.Title = ticketUpdate.Title;
            ticket.Description = ticketUpdate.Description;
            ticket.Complexity = ticketUpdate.Complexity;
            ticket.Notes = ticketUpdate.Notes;
            ticket.Open = ticketUpdate.Open;

            // Ticket is closing
            if (!ticket.Open && ticket.DateClosed == DateTime.MinValue)
            {
                ticket.DateClosed = DateTime.Now;
            }

            // Ticket is re-opening
            if (ticket.Open && ticket.DateClosed != DateTime.MinValue)
            {
                ticket.DateClosed = DateTime.MinValue;
            }

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Open), new { id = ticket.Id });
        }

        /// <summary>
        /// Open the page for adding time to a ticket.
        /// </summary>
        /// <param name="id">The id of the ticket.</param>
        /// <returns>The add time view</returns>
        [HttpGet]
        public async Task<IActionResult> AddTime([FromRoute] Guid id)
        {
            var ticket = await _context.Tickets.FindAsync(id);
            return View(new TicketTime { TicketTitle = ticket.Title, TicketId = ticket.Id });
        }

        /// <summary>
        /// Add time to a ticket
        /// </summary>
        /// <param name="time">The time to add</param>
        /// <returns>Redirect to ticket view</returns>
        [HttpPost]
        public async Task<IActionResult> AddTime([FromForm] TicketTime time)
        {
            _context.TechnicianTicketTimes.Add(new TechnicianTicketTime
            {
                End = time.End,
                Start = time.Start,
                TicketId = time.TicketId,
                TechnicianId = _userManager.GetUserName(User)
            });
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Open), new { id = time.TicketId });
        }

        /// <summary>
        /// Deletes a time
        /// </summary>
        /// <param name="id">The id of the time</param>
        /// <returns>The ticket view</returns>
        [Authorize(Roles = DataConstants.AdministratorRole)]
        [HttpPost]
        public async Task<IActionResult> DeleteTime([FromRoute] Guid id)
        {
            var time = await _context.TechnicianTicketTimes.FindAsync(id);
            _context.TechnicianTicketTimes.Remove(time);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Open), new { id = time.TicketId });
        }

        /// <summary>
        /// Gets bill.
        /// </summary>
        /// <param name="id">The id for the ticket</param>
        /// <returns>The bill</returns>
        [HttpGet]
        public async Task<IActionResult> Bill([FromRoute] Guid id)
        {
            var ticket = await _context.Tickets.FindAsync(id);
            var employee = await _context.Employees.FindAsync(ticket.EmployeeId);
            var times = await _context.TechnicianTicketTimes.Where(time => time.TicketId == ticket.Id).Join(_context.Users, time => time.TechnicianId, tech => tech.UserName, (time, tech) => new TechnicianTime { Technician = tech, Time = time }).ToListAsync();
            return View(new TicketDetails { Ticket = ticket, Employee = employee, Times = times });
        }

        /// <summary>
        /// Toggles urgency of a ticket
        /// </summary>
        /// <param name="id">The id of the ticket</param>
        /// <returns>The ticket</returns>
        [HttpPost]
        public async Task<IActionResult> ToggleUrgent([FromRoute] Guid id)
        {
            var ticket = await _context.Tickets.FindAsync(id);
            ticket.IsUrgent = !ticket.IsUrgent;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Open), new { id = id });
        }
    }
}
