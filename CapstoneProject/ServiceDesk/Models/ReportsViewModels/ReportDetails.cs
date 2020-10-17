using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServiceDesk.Models.ReportsViewModels
{
    public class ReportDetails
    {
        /// <summary>
        /// Average Wait Time
        /// </summary>
        /// <returns></returns>
        public TimeSpan AverageWait { get; set; }

        /// <summary>
        /// Average queue length
        /// </summary>
        public int AverageQueueLength { get; set; }

        /// <summary>
        /// Empty queue percentage
        /// </summary>
        public double EmptyQueuePercentage { get; set; }

        /// <summary>
        /// Tickets not addressed same day
        /// </summary>
        public int TicketsNotAddressedSameDay { get; set; }

        /// <summary>
        /// Hours each technician was idle
        /// </summary>
        public List<(Technician Technician, int HoursIdle)> TechnicianIdleHours { get; set; }

    }
}
