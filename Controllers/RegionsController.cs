using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NotificationService_Kisen.Data;
using NotificationService_Kisen.DTO;

namespace NotificationService_Kisen.Controllers
{
    [ApiController]
    [Route("api/regions")]
    public class RegionsController : Controller
    {
        private readonly NotificationDbContext _db;
        public RegionsController(NotificationDbContext db) => _db = db;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var regions = await _db.Regions
                .Select(r => new RegionDto
                {
                    RegionId = r.RegionId,
                    Name = r.Name
                })
                .ToListAsync();

            return Ok(regions);
        }
    }
}
