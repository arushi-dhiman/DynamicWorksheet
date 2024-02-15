using DynamicRepository.Models;
using DynamicServices;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace DynamicWorksheet.Controllers
{
    public class TemplateController : Controller
    {
        private readonly ITemplateService _templateService;

        public TemplateController(ITemplateService templateService)
        {
            _templateService = templateService;
        }

        [Route("Finding/GetFindings")]
        [HttpGet]
        public async Task<IActionResult> GetAllFindings()
        {
            var response= await _templateService.GetFindings();
            return Ok(response);

        }

        [Route("Records/GetRecords")]
        [HttpGet]
        public async Task<IActionResult> GetRecords(int userId, int templateId)
        {
            var response = await _templateService.GetRecords(userId, templateId);
            return Ok(response);

        }

        [Route("Records/SaveRecords")]
        [HttpPost]
        public async Task<IActionResult> SaveRecords([FromBody] SaveTemplateDataModel record)
        {
            var response = await _templateService.SaveRecords(record);
            return Ok(response);
            

        }

        [Route("Fields/GetDynamicFields")]
        [HttpGet]
        public async Task<IActionResult> GetDynamicFields(int templateId, int userId)
        {
            var response = await _templateService.GetDynamicFields(templateId, userId);
            return Ok(response);

        }
    }
}
