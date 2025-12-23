using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace LoyaltyProgram.Tests.Fixture
{
    [CollectionDefinition(nameof(FunctionTestCollection))]
    public class FunctionTestCollection : ICollectionFixture<TestFixture>
    {
    }
}
