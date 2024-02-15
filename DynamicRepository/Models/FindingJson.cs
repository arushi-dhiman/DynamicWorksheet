using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicRepository.Models
{
    public class Field
    {
        public int FieldId { get; set; }
        public int FieldOptionId { get; set; }
    }

    public class StatementJson
    {
        public int StatementId { get; set; }
        public string? Description { get; set; }
        public List<Field>? FieldIds { get; set; }
    }

    public class FindingJson
    {
        public int FindingId { get; set; }
        public string? FindingTitle { get; set; }
        public int FindingOrder { get; set; }
        public List<StatementJson>? Statements { get; set; }
    }
}
