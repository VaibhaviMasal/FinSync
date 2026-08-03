using FinSync.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinSync.Application.Features.Dashboard.DTOs
{
    public class RecentPaymentDto
    {
        public int PremiumPaymentId { get; set; }

        public decimal Amount { get; set; }

        public DateOnly DueDate { get; set; }

        public PaymentStatus PaymentStatus { get; set; }
    }
}
