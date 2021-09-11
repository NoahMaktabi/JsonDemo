using System;
using System.Threading.Tasks;

namespace JsonDemo
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            ApiHandler handler = new();
            await handler.StartProgram();
        }
    }
}
