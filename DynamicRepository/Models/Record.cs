using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicRepository.Models
{

    public class Record
    {
        public int Id { get; set; }
        public int UserId { get; set; } 
        public int FieldId { get; set; }
        public string? FieldValue { get; set; }
        public bool? IsMultipleValues { get; set; }
    }
}