using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Report.API.Domain.Models;
using Report.API.Domain.Services;
using Report.API.Extensions;
using Report.API.Resources;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Report.API.Controllers
{
    [Produces("application/json")]
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ReportsController : ControllerBase
    {
        private readonly IReportService _reportService;
        private readonly IMapper _mapper;

        public ReportsController(IReportService reportService, IMapper mapper)
        {
            _reportService = reportService;
            _mapper = mapper;
        }

        [SwaggerOperation(
            Summary = "Get All reports",
            Description = "Get All reports already stored",
            Tags = new[] {"Reports"})]
        [HttpGet]
        public async Task<IEnumerable<ReportResource>> GetAllAsync()
        {
            var report = await _reportService.ListAsync();
            var resources = _mapper.Map<IEnumerable<Report>, IEnumerable<ReportResource>>(report);
            return resources;
        }
        
        [SwaggerOperation(
            Summary = "Get a Report by id",
            Description = "Get the report based on the id if it exists",
            Tags = new[] {"Reports"})]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var result = await _reportService.GetByIdAsync(id);
            if (!result.Success)
                return BadRequest(result.Message);
            var reportResource = _mapper.Map<Report, ReportResource>(result.Resource);
            return Ok(reportResource);
        }
        
        [SwaggerOperation(
            Summary = "Register a report",
            Description = "Add a report to the database",
            Tags = new[] {"Reports"})]
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] SaveReportResource resource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());

            var report = _mapper.Map<SaveReportResource, Report>(resource);
            var result = await _reportService.SaveAsync(report);

            if (!result.Success)
                return BadRequest(result.Message);
            
            var reportResource = _mapper.Map<Report, ReportResource>(result.Resource);
            return Ok(reportResource);
        }
        
        [SwaggerOperation(
            Summary = "Delete a report",
            Description = "Delete the information of a report identified by his id.",
            Tags = new[] {"Reports"})]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _reportService.DeleteAsync(id);

            if (!result.Success)
                return BadRequest(result.Message);
            
            var reportResource = _mapper.Map<Report, ReportResource>(result.Resource);
            return Ok(reportResource);
        }
    }
}