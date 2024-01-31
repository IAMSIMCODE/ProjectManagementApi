using Asp.Versioning;
using AutoMapper;
using DataService.Repositories.Interfaces;
using Entities.DbSet;
using Entities.Dtos.Request;
using Entities.Dtos.Response;
using Microsoft.AspNetCore.Mvc;

namespace ProjManagement.Api.Controllers.V1
{
    [ApiVersion(1, Deprecated = true)]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class AchievementController : BaseController
    {
        public AchievementController(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor contextAccessor)
            : base(unitOfWork, mapper, contextAccessor)
        {
        }

        [HttpGet]
        [Route("{developerId:guid}")]
        public async Task<IActionResult> GetDeveloperAchievements(Guid developerId)
        {
            var devAchievements = await _unitOfWork.Achievements.GetDeveloperAchievementAsync(developerId);

            if (devAchievements == null) { return NotFound("Developer Achievement not found"); }

            var result = _mapper.Map<DeveloperAchievementResponse>(devAchievements);
            return Ok(result);
        }

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> AddAchievement([FromBody] CreateDevAchievementRequest achievementRequest)
        {
            if (!ModelState.IsValid) { return BadRequest("Not a valid request"); }

            var result = _mapper.Map<Achievement>(achievementRequest);

            await _unitOfWork.Achievements.Add(result);
            await _unitOfWork.CompleteAsync();

            return CreatedAtAction(nameof(GetDeveloperAchievements), new { developerId = result.DeveloperId }, result);
        }

        [HttpPut]
        [Route("")]
        public async Task<IActionResult> UpdateAchievements([FromBody] UpdateDevAchievementRequest updateDevAchievement)
        {
            if (!ModelState.IsValid) { return BadRequest("Not a valid request"); }

            var result = _mapper.Map<Achievement>(updateDevAchievement);

            await _unitOfWork.Achievements.Update(result);
            await _unitOfWork.CompleteAsync();

            return NoContent();
        }
    }
}
