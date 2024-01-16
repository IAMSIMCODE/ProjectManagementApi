using AutoMapper;
using DataService.Repositories.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ProjManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        protected readonly IUnitOfWork _unitOfWork;
        protected readonly IMapper _mapper;
        protected readonly IHttpContextAccessor _contextAccessor;
        protected readonly IMediator _mediator;

        public BaseController(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor contextAccessor, IMediator mediator)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _contextAccessor = contextAccessor;
            _mediator = mediator;
        }
    }
}
