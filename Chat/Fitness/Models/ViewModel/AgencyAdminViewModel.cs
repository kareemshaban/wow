using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fitness.Models.ViewModel
{
    public class AgencyAdminViewModel
    {

        public string Id { get; set; }
        public DateTime create_date { get; set; }
        public string FullName { get; set; }
        public decimal TotalBalance { get; set; }
        public decimal InsuranceAmount { get; set; }

    }

}
