using System;

namespace Csi.DemoConsole.SamplePrograms
{
    public class HelloWorld : ISampleProgram
    {
        public HelloWorld()
        {
        }

        public void DoWork()
        {
            Console.WriteLine("Hello world");
        }
    }
}