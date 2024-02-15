using DynamicRepository.Models;
using DynamicServices.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicServices
{
    public interface ITemplateService
    {
        Task<IEnumerable<OptionStatement>> GetFindings();
        Task<IEnumerable<Record>> SaveRecords(SaveTemplateDataModel record);
        Task<RecordsFindingsJsonResult> GetRecords(int userId, int templateId);
        Task<IEnumerable<DynamicField>> GetDynamicFields(int templateId, int userId);

    }
}
