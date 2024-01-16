using AutoMapper;
using DataService.Repositories.Interfaces;
using Entities.Dtos.Request;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ProjManagement.Api.Commands;
using ProjManagement.Api.Queries;
using System.Net;

namespace ProjManagement.Api.Controllers
{
    public class DeveloperController : BaseController
    {
        public DeveloperController(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor contextAccessor, IMediator mediator)
            : base(unitOfWork, mapper, contextAccessor, mediator)
        {
            
        }

        [HttpGet]
        public async Task<IActionResult> GetDevelopers()
        {
           //Specify the query for this endpoint 
            var query = new GetAllDevelopersQuery();

            var result = await _mediator.Send(query);
            return Ok(result);
        }

        [HttpGet]
        [Route("{developerId:Guid}")]
        public async Task<IActionResult> GetDeveloper(Guid developerId)
        {
            var query = new GetDeveloperQuery(developerId);
            var result = await _mediator.Send(query);

            if (result == null) { return NotFound(); }
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddDeveloper([FromBody] CreateDeveloperRequest developer)
        {
            if (!ModelState.IsValid) { return BadRequest("ModelState not Valid"); }

            var command = new CreateDeveloperInfoRequest(developer);
            var result = await _mediator.Send(command);

            return CreatedAtAction(nameof(GetDeveloper), new {developerId = result.DeveloperId}, result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateDeveloper([FromBody] UpdateDeveloperRequest updateDeveloper)
        {
            if (!ModelState.IsValid) { return BadRequest("ModelState not Valid"); }

            var command = new UpdateDeveloperInfoRequest(updateDeveloper);
            var result = await _mediator.Send(command);
           
            return result ? NoContent() : BadRequest();
        }

        [HttpDelete]
        [Route("{developerId:guid}")]
        public async Task<IActionResult> DeleteDeveloper(Guid developerId)
        {
            var command = new DeleteDeveloperInfoRequest(developerId);
            var result = await _mediator.Send(command);

            return result ? NoContent() : BadRequest();
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
