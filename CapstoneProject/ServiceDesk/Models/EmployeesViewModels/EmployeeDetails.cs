using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceDesk.Models.EmployeeViewModels
{
    public class EmployeeDetails
    {
        /// <summary>
        /// Get's client
        /// </summary>
        /// <returns>the client</returns>
        public Employee Employee { get; set; }

        /// <summary>
        /// List of tickets associated with client
        /// </summary>
        /// <returns>client's ticket list</returns>
        public IEnumerable<Ticket> Ticket { get; set; }

        /// <summary>
        /// Count of open tickets.
        /// </summary>
        public int OpenTicketCount { get; set; }
    }
}
