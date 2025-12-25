using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace LoyaltyProgram.Tests.Fixture
{
    public interface IUserCreateFixture
    {
        HttpClient Client { get; }

        Task UpdateTokenAsync();
    }
}
