using Entities.Dtos.Response;
using MediatR;

namespace ProjManagement.Api.Queries
{
    public class GetAllDevelopersQuery : IRequest<IEnumerable<GetDeveloperResponse>>
    {
    }
}
