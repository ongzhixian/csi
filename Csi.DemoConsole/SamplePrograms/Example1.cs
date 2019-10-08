using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Csi.Helpers;

namespace Csi.DemoConsole.SamplePrograms
{
    public class Example1 : ISampleProgram
    {
        public void DoWork()
        {
            Console.WriteLine("This example list all the public classes that can be found in the Csi.DemoConsole.SamplePrograms namespace");

            IEnumerable<Type> publicTypes = AssemblyHelper.GetPublicTypes(
                Assembly.GetEntryAssembly(),
                t => (t.Namespace == "Csi.DemoConsole.SamplePrograms") && t.IsClass
            );


            // System.Reflection.Assembly asm = System.Reflection.Assembly.GetEntryAssembly();

            // Type[] publicTypes = asm.GetExportedTypes().Where(t => 
            // t.Namespace == "Csi.DemoConsole.SamplePrograms" && t.IsClass).ToArray();

            Console.WriteLine("{0} public types found.", publicTypes.Count());

            foreach (Type t in publicTypes)
            {
                Console.WriteLine("Name: [{0}], {1} -- {2}", t.Name, t.Namespace, t.AssemblyQualifiedName);
            }

        }
    }
}