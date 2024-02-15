using DynamicRepository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicServices.Model
{
    public class RecordsFindingsJsonResult
    {
        public List<Record>? Records { get; set; }
        public List<FindingJson>? JsonResult { get; set; }
    }
}
