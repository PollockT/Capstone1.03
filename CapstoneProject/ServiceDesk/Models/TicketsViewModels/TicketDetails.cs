using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceDesk.Models.TicketsViewModels
{
    public class TicketDetails
    {
        /// <summary>
        /// get's ticket
        /// </summary>
        /// <returns>the ticket</returns>
        public Ticket Ticket { get; set; }

        /// <summary>
        /// get's Client
        /// </summary>
        /// <returns>the client</returns>
        public Employee Employee { get; set; }

        /// <summary>
        /// List of tech time associated with ticket
        /// </summary>
        /// <returns>list of tech times</returns>
        public List<TechnicianTime> Times { get; set; }
    }
}
