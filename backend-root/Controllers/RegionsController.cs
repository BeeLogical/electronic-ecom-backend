using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using backend_root.DTOs;
using backend_root.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace backend_root.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegionsController : ControllerBase
    {
        private readonly IRegionService _regionService;
        public RegionsController(IRegionService regionService)
        {
            _regionService = regionService;
        }
        [Authorize(Roles = "User,Admin")]
        [HttpGet]
        public async Task<IActionResult> GetAllRegions()
        {
            return Ok(await _regionService.GetAllAsync());
        }
        [Authorize(Roles = "User,Admin")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRegionById(int id)
        {
            try
            {
                var region = await _regionService.GetByIdAsync(id);
                return Ok(region);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateRegion([FromBody] RegionDto regionDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                await _regionService.AddAsync(regionDto);
                return Ok();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }
        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRegion(int id, [FromBody] RegionDto regionDto)
        {
            if (id != regionDto.Id || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                await _regionService.UpdateAsync(id, regionDto);
                return Ok();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRegion(int id)
        {
            try
            {
                await _regionService.DeleteAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }   
        }
    }

}
