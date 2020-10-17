using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ServiceDesk.Models
{
    public class Ticket
    {
        /// <summary>
        /// The Id for this ticket
        /// </summary>
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        /// <summary>
        /// The Id for the client who owns this ticket
        /// </summary>
        public Guid EmployeeId { get; set; }

        /// <summary>
        /// The title of this ticket
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// The description for this ticket
        /// </summary>
        [DataType(DataType.MultilineText)]
        public string Description { get; set; }

        /// <summary>
        /// The complexity (1-3) of this ticket
        /// </summary>
        public int Complexity { get; set; }

        /// <summary>
        /// Defines if this ticket is urgent
        /// </summary>
        public bool IsUrgent { get; set; }

        /// <summary>
        /// Notes for this ticket
        /// </summary>
        [DataType(DataType.MultilineText)]
        public string Notes { get; set; }

        /// <summary>
        /// The date added
        /// </summary>
        public DateTime DateAdded { get; set; }

        /// <summary>
        /// The date closed
        /// </summary>
        public DateTime DateClosed { get; set; }

        /// <summary>
        /// True if the ticket is open
        /// </summary>
        public bool Open { get; set; }
    }
}
