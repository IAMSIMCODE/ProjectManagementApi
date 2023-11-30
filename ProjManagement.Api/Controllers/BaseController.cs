using AutoMapper;
using DataService.Repositories.Interfaces;
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

        public BaseController(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor contextAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _contextAccessor = contextAccessor;
        }
    }
}
