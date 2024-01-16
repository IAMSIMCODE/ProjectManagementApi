using Entities.Dtos.Response;
using MediatR;

namespace ProjManagement.Api.Queries
{
    public class GetDeveloperQuery : IRequest<GetDeveloperResponse>
    {
        public Guid DeveloperId { get; }

        public GetDeveloperQuery(Guid developerId)
        {
            DeveloperId = developerId;
        }
    }
}
