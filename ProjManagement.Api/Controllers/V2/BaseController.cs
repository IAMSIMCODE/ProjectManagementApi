using AutoMapper;
using DataService.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ProjManagement.Api.Controllers.V2
{
    [ApiController]
    public class BaseController(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor contextAccessor) : ControllerBase
    {
        protected readonly IUnitOfWork _unitOfWork = unitOfWork;
        protected readonly IMapper _mapper = mapper;
        protected readonly IHttpContextAccessor _contextAccessor = contextAccessor;
    }
}
