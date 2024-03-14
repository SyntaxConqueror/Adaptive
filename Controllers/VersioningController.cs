using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ClosedXML.Excel;

using System.IO;
using Microsoft.AspNetCore.Authorization;
using LR7.Services.Interfaces;


namespace LR7.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiVersion("1.0", Deprecated = true)]
    [ApiVersion("2.0")]
    [ApiVersion("3.0")]
    public class VersioningController : ControllerBase
    {
        private readonly IVersioningService _versioningService;

        public VersioningController(IVersioningService versioningService)
        {
            _versioningService = versioningService;
        }

        [HttpGet("/v1"), MapToApiVersion("1.0")]
        [Authorize]
        public IActionResult GetV1()
        {
            return _versioningService.GetV1();
        }

        [HttpGet("/v2"), MapToApiVersion("2.0")]
        [Authorize]
        public IActionResult GetV2()
        {
            return _versioningService.GetV2();
        }

        [HttpGet("/v3"), MapToApiVersion("3.0")]
        [Authorize]
        public IActionResult GetV3()
        {
            return _versioningService.GetV3();
        }
    }
}
