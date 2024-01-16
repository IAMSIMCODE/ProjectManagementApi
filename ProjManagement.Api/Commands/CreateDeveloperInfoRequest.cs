using Entities.Dtos.Request;
using Entities.Dtos.Response;
using MediatR;

namespace ProjManagement.Api.Commands
{
    public class CreateDeveloperInfoRequest : IRequest<GetDeveloperResponse>
    {
        public CreateDeveloperRequest Developer { get;}

        public CreateDeveloperInfoRequest(CreateDeveloperRequest developer)
        {
            Developer = developer;
        }
    }
}
