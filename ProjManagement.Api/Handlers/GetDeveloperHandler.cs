using AutoMapper;
using DataService.Repositories.Interfaces;
using Entities.Dtos.Response;
using MediatR;
using ProjManagement.Api.Queries;

namespace ProjManagement.Api.Handlers
{
    public class GetDeveloperHandler : IRequestHandler<GetDeveloperQuery, GetDeveloperResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetDeveloperHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<GetDeveloperResponse> Handle(GetDeveloperQuery request, CancellationToken cancellationToken)
        {
            var developer = await _unitOfWork.Developers.GetById(request.DeveloperId);

            if (developer == null) { return null!; };
            return _mapper.Map<GetDeveloperResponse>(developer);
        }
    }
}
