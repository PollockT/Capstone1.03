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
using ServiceDesk.Models.TechniciansViewModels;

namespace ServiceDesk.Controllers
{    /// <summary>
     /// Controller for technicians
     /// </summary>
    [Authorize(Roles = DataConstants.AdministratorRole)]
    public class TechniciansController : Controller
    {
        private ServiceDeskContext _context;

        private UserManager<Technician> _userManager;

        /// <summary>
        /// intializes _context
        /// </summary>
        /// <param name="context">context of the technician</param>
        /// <param name="userManager">the usermanager</param>
        public TechniciansController(ServiceDeskContext context, UserManager<Technician> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        /// <summary>
        /// Gets all technicians
        /// </summary>
        /// <returns>A list of all technicians</returns>
        [HttpGet]
        public async Task<IActionResult> All()
        {
            var technicians = await _context.Users.ToListAsync();
            return View(technicians);
        }

        /// <summary>
        /// Gets the view for adding a technician
        /// </summary>
        /// <returns>The add technician view</returns>
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        /// <summary>
        /// Adds a technician
        /// </summary>
        /// <returns>The technician list</returns>
        [HttpPost]
        public async Task<IActionResult> Add([FromForm] NewTechnician newTechnician)
        {
            var technician = new Technician
            {
                DateAdded = DateTime.Now,
                UserName = $"{newTechnician.FirstName}.{newTechnician.LastName}",
                FirstName = newTechnician.FirstName,
                LastName = newTechnician.LastName,
                IsAdmin = newTechnician.IsAdmin
            };
            await _userManager.CreateAsync(technician, newTechnician.Password);
            if (technician.IsAdmin)
            {
                await _userManager.AddToRoleAsync(technician, DataConstants.AdministratorRole);
            }
            return RedirectToAction(nameof(All));
        }
    }
}
