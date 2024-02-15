using DynamicRepository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicRepository
{
    public interface IDBHelper
    {
        string GetFindings();
        DBResponse SaveRecords(SaveTemplateDataModel record);
        IEnumerable<RecordFinidngModel> GetRecords(int userId, int templateId);
        IEnumerable<DynamicField> GetDynamicFields(int templateId, int userId);

    }
}
