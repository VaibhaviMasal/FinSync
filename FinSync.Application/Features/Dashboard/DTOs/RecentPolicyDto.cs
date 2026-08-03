using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinSync.Application.Features.Dashboard.DTOs
{
    public class RecentPolicyDto
    {
        public int PolicyId { get; set; }

        public string PolicyNumber { get; set; } = string.Empty;

        public string CustomerName { get; set; } = string.Empty;

        public string CompanyName { get; set; } = string.Empty;

        public DateTime IssueDate { get; set; }
    }
}
