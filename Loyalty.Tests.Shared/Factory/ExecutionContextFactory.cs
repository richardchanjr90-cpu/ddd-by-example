using Microsoft.Azure.WebJobs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Loyalty.Tests.Shared.Factory
{
    public static class ExecutionContextFactory
    {
        public static ExecutionContext Get()
        {
            ExecutionContext context = new ExecutionContext();

            


            return context;
        }
    }
}
