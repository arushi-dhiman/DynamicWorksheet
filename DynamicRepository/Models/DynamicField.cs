using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicRepository.Models
{
    public class DynamicField
    {
        public int Id { get; set; }
        public int DynamicFieldId { get; set; }
        public int FieldId { get; set; }
        public string? FieldName { get; set; }
        public string? Label { get; set; }
        public bool IsDynamic { get; set;}
        public int UserId {  get; set; }
        

    }
}
