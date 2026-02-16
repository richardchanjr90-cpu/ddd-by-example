using System.Text.Json.Serialization;
using AzureExtensions.FunctionToken;
using Loyalty.Domain.Contracts.Interfaces;
using MediatR;

namespace LoyaltyUser.Domain.Handlers.Queries.Commands.User
{
    public class UpdateFirebaseTokenCommand : IRequest<ICommandResult>
    {
        [JsonIgnore] 
        // todo: remove it when
        // Set ReferenceHandling.Preserve to ReferenceHandling property in JsonSerializerOptions
        // is implemented in .NET 5
        // and add it in Logging Pipeline
        // https://github.com/dotnet/runtime/issues/30938
        public FunctionTokenResult Token { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string Email { get; set; }
    }
}