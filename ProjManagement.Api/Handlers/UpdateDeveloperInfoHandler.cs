using AutoMapper;
using DataService.Repositories.Interfaces;
using Entities.DbSet;
using MediatR;
using ProjManagement.Api.Commands;

namespace ProjManagement.Api.Handlers
{
    public class UpdateDeveloperInfoHandler : IRequestHandler<UpdateDeveloperInfoRequest, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateDeveloperInfoHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }


        public async Task<bool> Handle(UpdateDeveloperInfoRequest request, CancellationToken cancellationToken)
        {
            var result = _mapper.Map<Developer>(request.DeveloperRequest);

            await _unitOfWork.Developers.Update(result);
            await _unitOfWork.CompleteAsync();

            return true;
        }
    }
}
