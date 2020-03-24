using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Csi.ExtensionsConsole
{
    public interface IAnimal
    {
        string MakeSound();
    }

    public class Dog : IAnimal
    {
        ILogger log = null;

        public Dog(ILoggerFactory loggerFactory)
        {
            this.log = loggerFactory.CreateLogger<Dog>();
        }
        public string MakeSound()
        {
            this.log.LogInformation("In MakeSound()");

            return "Bow wow";
        }
    }
}
