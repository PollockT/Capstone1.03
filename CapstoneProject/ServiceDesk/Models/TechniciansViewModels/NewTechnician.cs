using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace ServiceDesk.Models.TechniciansViewModels
{
    public class NewTechnician : Technician
    {
        /// <summary>
        /// The password of the technician
        /// </summary>
        public string Password { get; set; }
        
    }
}
