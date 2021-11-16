using AutoMapper;
using CRM.Core.Models;
using CRM.Core.Services;
using CRM_API.Resources;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CRM_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [EnableCors("AllowOrigin")]

    public class VisitReportController : ControllerBase
    {
        public IList<VisitReport> VisitReports;

        private readonly IVisitReportService _VisitReportService;
        private readonly IVisitService _VisitService;

        private readonly IMapper _mapperService;
        public VisitReportController(IVisitReportService VisitReportService, IVisitService VisitService, IMapper mapper)
        {
            _VisitService = VisitService;
            _VisitReportService = VisitReportService;
            _mapperService = mapper;
        }


        [HttpPost]
        public async Task<ActionResult<VisitReport>> CreateVisitReport(SaveVisitReportResource SaveVisitReportResource)
        {
            //*** Mappage ***
            var VisitReport = _mapperService.Map<SaveVisitReportResource, VisitReport>(SaveVisitReportResource);
            var Visit = await _VisitService.GetById(SaveVisitReportResource.IdVisit);
            VisitReport.IdVisit = Visit.IdVisit;
            VisitReport.StatusVisit = Visit.Status;
            VisitReport.VersionVisit = Visit.Version;
            VisitReport.Visit = Visit;
            VisitReport.UpdatedOn = DateTime.UtcNow;
            VisitReport.CreatedOn = DateTime.UtcNow;
            //*** Creation dans la base de données ***
            var NewVisitReport = await _VisitReportService.Create(VisitReport);
            //*** Mappage ***
            var VisitReportResource = _mapperService.Map<VisitReport, VisitReportResource>(NewVisitReport);
            return Ok(VisitReportResource);
        }
        [HttpPost("Range")]
        public async Task<ActionResult<VisitReport>> CreateVisitReport(List<SaveVisitReportResource> SaveVisitReportResource)
        {
            //*** Mappage ***
            var VisitReport = _mapperService.Map<List<SaveVisitReportResource>, VisitReport>(SaveVisitReportResource);
            //*** Creation dans la base de données ***
            var NewVisitReport = await _VisitReportService.Create(VisitReport);
            //*** Mappage ***
            var VisitReportResource = _mapperService.Map<VisitReport, VisitReportResource>(NewVisitReport);
            return Ok(VisitReportResource);
        }





    }
}
