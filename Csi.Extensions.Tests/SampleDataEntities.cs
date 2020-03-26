using System;
using System.Collections.Generic;
using System.Text;

/// <summary>
/// Some sample data entities for used with unit tests
/// </summary>
namespace Csi.Extensions.Tests.Data
{
    public interface IAnimal
    {
        string MakeSound();
    }

    public class Dog : IAnimal
    {
        public string MakeSound()
        {
            return "woof";
        }
    }

    public class Cat : IAnimal
    {
        public string MakeSound()
        {
            return "meow";
        }
    }

    public class Mouse : IAnimal
    {
        public string MakeSound()
        {
            return "squeak";
        }
    }

    public class Chicken : IAnimal
    {
        public string MakeSound()
        {
            return "cluck";
        }
    }
}
