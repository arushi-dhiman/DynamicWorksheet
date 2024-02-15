using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicRepository.Models
{
    public class Finding
    {
        public int FindingId { get; set; }
        public string? FindingTitle { get; set; }
         public int FindingOrder { get; set; }
        public List<Statement>? Statements { get; set; }

    }

    public class OptionStatement
    {
        public int OptionStatementId { get; set; }
        public int FieldOptionId { get; set; }
        public int StatementId { get; set; }
        public string Statement { get; set; }
        public List<FindingStatement> Findings { get; set; }

    }
    public class FindingStatement
    {
        public int FindingId { get; set; }
        public string FindingTitle { get; set; }
        public int FindingOrder { get; set; }

    }
}
