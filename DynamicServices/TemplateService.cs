using DynamicRepository;
using DynamicRepository.Models;
using DynamicServices.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DynamicServices
{
    public class TemplateService : ITemplateService
    {
        private readonly IDBHelper _dbHelper;

        public TemplateService(IDBHelper dbHelper)
        {
            _dbHelper = dbHelper;
        }

        public async Task<IEnumerable<OptionStatement>> GetFindings()
        {
            List<OptionStatement> findingData = new List<OptionStatement>();

            var findings =  _dbHelper.GetFindings();
            List<OptionStatement> dataList = JsonConvert.DeserializeObject<List<OptionStatement>>(findings);
            findingData.AddRange(dataList);
            return findingData;
        }

        public async Task<RecordsFindingsJsonResult> GetRecords(int userId, int templateId)
        {
            var recordsFindings = _dbHelper.GetRecords(userId, templateId);

            //Aggregating all the common findings and converting DBResponse as per JSON required in UI.
            List<Record> records = recordsFindings.SelectMany(rf => rf.Records ?? Enumerable.Empty<Record>()).ToList();
            List<FindingJson> findings = recordsFindings.SelectMany(rf => rf.Findings ?? Enumerable.Empty<FindingJson>()).ToList();
            var groupedFindings = findings
             .Where(f => f.FindingId != 0)
            .GroupBy(f => new { f.FindingId, f.FindingTitle, f.FindingOrder })
            .Select(group => new FindingJson
            {
                FindingId = group.Key.FindingId,
                FindingTitle = group.Key.FindingTitle,
                FindingOrder = group.Key.FindingOrder,
                Statements = group
              .SelectMany(finding => finding.Statements)

            .GroupBy(s => new { s.StatementId, s.Description })
            .Select(statementGroup => new StatementJson
            {
                StatementId = statementGroup.Key.StatementId,
                Description = statementGroup.Key.Description,
                FieldIds = statementGroup
                    .SelectMany(statement => statement.FieldIds)
                    .ToList()
            })
            .ToList()
            })
    .ToList();

            string jsonResult = JsonConvert.SerializeObject(groupedFindings, Formatting.Indented);


            var result = new RecordsFindingsJsonResult
            {
                Records = records,
                JsonResult = groupedFindings
            };

            return result;
        }

        public async Task<IEnumerable<Record>> SaveRecords(SaveTemplateDataModel record)
        {
            var res = _dbHelper.SaveRecords(record);
            RecordsFindingsJsonResult records = null;
            if(res.Status == "SUCCESS")
            {
                   records =await GetRecords(record.UserId, record.TemplateId);
            }
            return records.Records;
        }

        public async Task<IEnumerable<DynamicField>> GetDynamicFields(int templateId, int userId)
        {
            var dynamicFields = _dbHelper.GetDynamicFields(templateId, userId);
            return dynamicFields;
        }
    }
}
