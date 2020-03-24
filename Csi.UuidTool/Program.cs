using System;

namespace Csi.UuidTool
{
    class Program
    {
        static void Main(string[] args)
        {
            string uuid = string.Empty;

            if (args.Length <= 0)
            {
                uuid = Guid.Empty.ToString();
            }
            else
            {
                uuid = Guid.NewGuid().ToString();
            }

            
            

            Console.WriteLine(uuid);
            
        }
    }
}
