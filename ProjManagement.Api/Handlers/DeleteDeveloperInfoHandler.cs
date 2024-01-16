using AutoMapper;
using DataService.Repositories.Interfaces;
using MediatR;
using ProjManagement.Api.Commands;

namespace ProjManagement.Api.Handlers
{
    public class DeleteDeveloperInfoHandler : IRequestHandler<DeleteDeveloperInfoRequest, bool>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DeleteDeveloperInfoHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<bool> Handle(DeleteDeveloperInfoRequest request, CancellationToken cancellationToken)
        {
            var developer = await _unitOfWork.Developers.GetById(request.DeveloperId);

            if (developer == null)
                return false;

            await _unitOfWork.Developers.Delete(request.DeveloperId);
            await _unitOfWork.CompleteAsync();

            return true;
        }
    }
}
