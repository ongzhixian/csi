using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Csi.Helpers;

namespace Csi.DemoConsole.SamplePrograms
{
    public class Example2 : ISampleProgram
    {
        public void DoWork()
        {
            Console.WriteLine("This example list all the public classes that can be found in the Csi.DemoConsole.SamplePrograms namespace");

            IEnumerable<Type> publicTypes = AssemblyHelper.GetPublicTypes(
                Assembly.GetEntryAssembly(),
                t => 
                (t.Namespace == "Csi.DemoConsole.SamplePrograms") 
                && t.GetInterfaces().Contains(typeof(ISampleProgram))
                && t.IsClass
            );

            string exec = "HelloWorld";

            Console.WriteLine(publicTypes.Count());
            foreach (var t in publicTypes)
            {
                Console.WriteLine(t.Name);
                
            }

            Type runtimeType = publicTypes.Where(r => r.Name == exec).FirstOrDefault();
            ISampleProgram p = Activator.CreateInstance(runtimeType) as ISampleProgram;
            

            if (p == null)
            {
                Console.WriteLine("p is null");
            }
            else
            {
                p.DoWork();
            }
            //p.DoWork();

            // Type.GetType()
            // object o = Activator.CreateInstance(null, "Csi.DemoConsole.SamplePrograms.HelloWorld");
            // HelloWorld x = o as HelloWorld;
            // x.DoWork();



        }
    }
}