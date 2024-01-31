using Asp.Versioning;
using AutoMapper;
using DataService.Repositories.Interfaces;
using Entities.DbSet;
using Entities.Dtos.Request;
using Entities.Dtos.Response;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ProjManagement.Api.Controllers.V2
{
    [ApiVersion(2)]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class DeveloperController : BaseController
    {
        public DeveloperController(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor contextAccessor)
            : base(unitOfWork, mapper, contextAccessor)
        {
        }

        [HttpGet]
        public async Task<IActionResult> GetDevelopers()
        {
            var developers = await _unitOfWork.Developers.GetAll();

            //return Ok(developers);  
            var result = _mapper.Map<IEnumerable<GetDeveloperResponse>>(developers);
            return Ok(result);
        }

        [HttpGet]
        [Route("{developerId:Guid}")]
        public async Task<IActionResult> GetDeveloper(Guid developerId)
        {
            var developer = await _unitOfWork.Developers.GetById(developerId);

            if (developer == null) { return NotFound(); }

            var result = _mapper.Map<GetDeveloperResponse>(developer);
            return Ok(result);
        }


        [HttpPost]
        public async Task<IActionResult> AddDeveloper([FromBody] CreateDeveloperRequest developerRequest)
        {
            if (!ModelState.IsValid) { return BadRequest("ModelState not Valid"); }

            var result = _mapper.Map<Developer>(developerRequest);

            await _unitOfWork.Developers.Add(result);
            await _unitOfWork.CompleteAsync();

            return CreatedAtAction(nameof(GetDeveloper), new { developerId = result.Id }, result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateDeveloper([FromBody] UpdateDeveloperRequest updateDeveloper)
        {
            if (!ModelState.IsValid) { return BadRequest("ModelState not Valid"); }

            var result = _mapper.Map<Developer>(updateDeveloper);

            await _unitOfWork.Developers.Update(result);
            await _unitOfWork.CompleteAsync();

            return NoContent();
        }

        [HttpDelete]
        [Route("{developerId:guid}")]
        public async Task<IActionResult> DeleteDeveloper(Guid developerId)
        {
            var developer = await _unitOfWork.Developers.GetById(developerId);

            if (developer == null)
                return NotFound();

            await _unitOfWork.Developers.Delete(developerId);
            await _unitOfWork.CompleteAsync();

            return NoContent();
        }

        [HttpGet]
        [Route("GetIpAddress")]
        public async Task<IActionResult> GetIpAddress()
        {
            var myIp = "192.168.134.28";

            var ipAddress = _contextAccessor.HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();

            if (ipAddress == null) { return BadRequest(); }

            // check if the ipaddress is local 

            if (ipAddress == "::1" || ipAddress == "0.0.0.1")
            {
                ipAddress = Dns.GetHostEntry(Dns.GetHostName()).AddressList[1].ToString();

                if (ipAddress != myIp) { return BadRequest("IP NOT WHITELISTED"); }
            }

            //ipAddress = Dns.GetHostEntry(Dns.GetHostName()).AddressList[1].ToString();
            return Ok(ipAddress);

        }
    }
}
