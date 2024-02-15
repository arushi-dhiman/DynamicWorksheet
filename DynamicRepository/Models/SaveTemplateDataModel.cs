
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicRepository.Models
{
    public class SaveTemplateDataModel
    {

        public bool IsClear { get; set; }
        public int UserId { get; set; }
        public int TemplateId { get; set; }

        public List<Record>? TemplateFields { get; set; }

    }
}
