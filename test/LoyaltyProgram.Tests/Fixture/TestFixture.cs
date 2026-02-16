using System;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Threading;

namespace LoyaltyProgram.Tests.Fixture
{
    public class TestFixture : IDisposable
    {
        public string DotnetExecutablePath =
            Environment.GetEnvironmentVariable(nameof(DotnetExecutablePath))
            ?? "%ProgramFiles%\\dotnet\\dotnet.exe";
        public string FunctionHostPath =
            Environment.GetEnvironmentVariable(nameof(FunctionHostPath))
            ?? "C:\\Users\\User\\Documents\\Portable\\dev\\node-v12.11.0-win-x64\\node_modules\\azure-functions-core-tools\\bin\\func.dll";
        public string FunctionApplicationPath = "..\\..\\..\\..\\..\\src\\LoyaltyProgram\\bin\\Debug\\netcoreapp2.2";

        private readonly Process funcHostProcess;

        public TestFixture()
        {
            var dotnetExePath = Environment.ExpandEnvironmentVariables(DotnetExecutablePath);
            var functionHostPath = Environment.ExpandEnvironmentVariables(FunctionHostPath);
            var functionAppFolder = Path.GetRelativePath(Directory.GetCurrentDirectory(), FunctionApplicationPath);

            //funcHostProcess = new Process
            //{
            //    StartInfo =
            //    {
            //        FileName = dotnetExePath,
            //        Arguments = $"\"{functionHostPath}\" start -p {Port}",
            //        WorkingDirectory = functionAppFolder,
            //        //CreateNoWindow = false,
            //        //UseShellExecute = true,
            //        RedirectStandardOutput = true,
            //        WindowStyle = ProcessWindowStyle.Normal
            //    }
            //};

            //funcHostProcess.ErrorDataReceived += new DataReceivedEventHandler((sender, e) =>
            //{
            //    if (!String.IsNullOrEmpty(e.Data))
            //    {
            //        Console.WriteLine("e:" + e.Data);
            //    }
            //});

            //funcHostProcess.OutputDataReceived += new DataReceivedEventHandler((sender, e) =>
            //{
            //    if (!String.IsNullOrEmpty(e.Data))
            //    {
            //        Console.WriteLine(e.Data);
            //    }
            //});

            //Waiting till AF will load.
            Thread.Sleep(5000);

            //var success = funcHostProcess.Start();
            //funcHostProcess.BeginOutputReadLine();

            //if (!success)
            //{
            //    throw new InvalidOperationException("Could not start Azure Functions host.");
            //}
        }

        public int Port { get; } = 7071;

        public string Host { get; } = "http://localhost";

        public HttpClient ConfigureClient(HttpClient client)
        {
            if (client != null)
            {
                client.BaseAddress = new Uri($"{Host}:{Port}");
            }

            return client;
        }

        public virtual void Dispose()
        {
            if (funcHostProcess != null && !funcHostProcess.HasExited)
            {
                funcHostProcess.Kill();
            }

            funcHostProcess?.Dispose();
        }
    }
}