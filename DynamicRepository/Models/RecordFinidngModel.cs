using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicRepository.Models
{
    public class RecordFinidngModel
    {
        public List<Record>? Records { get; set; }
        public List<FindingJson>? Findings { get; set; }
    }
}
