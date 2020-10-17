using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ServiceDesk.Data
{
    public static class DataConstants
    {
        /// <summary>
        /// The role of administrator.
        /// </summary>
        public const string AdministratorRole = "administrator";

        /// <summary>
        /// Administrator username
        /// </summary>
        public const string RootUsername = "admin";
    }
}