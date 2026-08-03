using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinSync.Application.Features.Dashboard.DTOs
{
    public class RecentCustomerDto
    {
        public int CustomerId { get; set; }

        public string FullName { get; set; } = string.Empty;

        public string MobileNumber { get; set; } = string.Empty;

        public DateTime CreatedDate { get; set; }
    }
}
