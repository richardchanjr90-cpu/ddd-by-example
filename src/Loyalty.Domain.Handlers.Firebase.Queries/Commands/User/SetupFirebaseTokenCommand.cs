using System.Collections.Generic;
using System.Text.Json.Serialization;
using AzureExtensions.FunctionToken;
using Loyalty.Domain.Contracts.Interfaces;
using Loyalty.Shared.Contracts.Enums;
using MediatR;
using MediatR.Extensions.UnitOfWork.Interface;

namespace Loyalty.Domain.Handlers.Firebase.Queries.Commands.User
{
    public class SetupFirebaseTokenCommand : IRequest<ICommandResult>
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

        public string City { get; set; }

        public VenueUserRole Role { get; set; }

        public string VenueIds { get; set; }
    }
}