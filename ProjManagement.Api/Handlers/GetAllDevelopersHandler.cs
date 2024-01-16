using AutoMapper;
using DataService.Repositories.Interfaces;
using Entities.Dtos.Response;
using MediatR;
using ProjManagement.Api.Queries;

namespace ProjManagement.Api.Handlers
{
    public class GetAllDevelopersHandler : IRequestHandler<GetAllDevelopersQuery, IEnumerable<GetDeveloperResponse>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllDevelopersHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<GetDeveloperResponse>> Handle(GetAllDevelopersQuery request, CancellationToken cancellationToken)
        {
            var developers = await _unitOfWork.Developers.GetAll();
            return _mapper.Map<IEnumerable<GetDeveloperResponse>>(developers);
        }
    }
}
