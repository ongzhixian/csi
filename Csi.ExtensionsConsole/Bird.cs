using System;
using System.Collections.Generic;
using System.Text;

namespace Csi.ExtensionsConsole
{
    public interface IBird
    {
        string MakeSound();
    }

    public class Crow : IBird
    {
        public string MakeSound()
        {
            return "Caw caw";
        }
    }

    public class Pigeon : IBird
    {
        public string MakeSound()
        {
            return "Coo coo coo";
        }
    }
}
