using Microsoft.AspNetCore.Mvc;

namespace LR7.Services.Interfaces
{
    public interface IVersioningService
    {
        public IActionResult GetV1();
        public IActionResult GetV2();
        public IActionResult GetV3();
    }
}
