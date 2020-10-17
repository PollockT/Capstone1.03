using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ServiceDesk.Models
{
    public class TechnicianTicketTime
    {
        /// <summary>
        /// The id.
        /// </summary>
        [Key]
        public Guid Id { get; set; }

        /// <summary>
        /// The technician id
        /// </summary>
        public string TechnicianId { get; set; }

        /// <summary>
        /// The ticket id
        /// </summary>
        public Guid TicketId { get; set; }

        /// <summary>
        /// The start time
        /// </summary>
        public DateTime Start { get; set; }

        /// <summary>
        /// The end time
        /// </summary>
        public DateTime End { get; set; }
    }
}
