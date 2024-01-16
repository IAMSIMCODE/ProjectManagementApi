using Entities.Dtos.Request;
using MediatR;

namespace ProjManagement.Api.Commands
{
    public class UpdateDeveloperInfoRequest : IRequest<bool>
    {
        public UpdateDeveloperRequest DeveloperRequest { get; }

        public UpdateDeveloperInfoRequest(UpdateDeveloperRequest developerRequest)
        {
            DeveloperRequest = developerRequest;
        }
    }
}
