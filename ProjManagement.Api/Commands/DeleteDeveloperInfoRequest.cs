using MediatR;

namespace ProjManagement.Api.Commands
{
    public class DeleteDeveloperInfoRequest : IRequest<bool>
    {
        public Guid DeveloperId { get; }

        public DeleteDeveloperInfoRequest(Guid developerId)
        {
            DeveloperId = developerId;
        }
    }
}
