using AutoMapper;
using DataService.Repositories.Interfaces;
using Entities.DbSet;
using Entities.Dtos.Response;
using MediatR;
using ProjManagement.Api.Commands;

namespace ProjManagement.Api.Handlers
{
    public class GetDeveloperInfoHandler : IRequestHandler<CreateDeveloperInfoRequest, GetDeveloperResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetDeveloperInfoHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<GetDeveloperResponse> Handle(CreateDeveloperInfoRequest request, CancellationToken cancellationToken)
        {
            var developer = _mapper.Map<Developer>(request.Developer);

            await _unitOfWork.Developers.Add(developer);
            await _unitOfWork.CompleteAsync();

            return _mapper.Map<GetDeveloperResponse>(developer);
        }
    }
}
