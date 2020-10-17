using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceDesk.Models.TicketsViewModels
{
    public class TechnicianTime
    {
        /// <summary>
        /// Get's technician
        /// </summary>
        /// <returns>technician</returns>
        public Technician Technician { get; set; }

        /// <summary>
        /// get's time
        /// </summary>
        /// <returns>technician's ticket time</returns>
        public TechnicianTicketTime Time { get; set; }
    }
}
