using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace ServiceDesk.Models
{
    public class Technician : IdentityUser
    {
        /// <summary>
        /// Is the Technician an Admin?
        /// </summary>
        public bool IsAdmin { get; set; }

        /// <summary>
        /// The first name of the Technician
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// The last name of the Technician
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// The Date the technician was hired
        /// </summary>
        public DateTime DateAdded { get; set; }

        /// <summary>
        /// Gets pay rate for this technician
        /// </summary>
        /// <returns>Pay rate</returns>
        public int GetPayRate()
        {
            return 30 + 10 * (int)((DateTime.Now - DateAdded).TotalDays / 365);
        }
    }
}
