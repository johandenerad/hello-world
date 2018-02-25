using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Loans.Models
{
    public class Loan
    {
        public int Id { get; set; } = 0;
        public string Customer { get; set; } = "";
        public float repayAmount { get; set; } = 0f;
        public float fundAmount { get; set; } = 0f;
    }  
}
