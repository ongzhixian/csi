using System;
using System.Collections.Generic;
using System.Linq;

namespace Csi.Helpers
{
    public class AssemblyHelper
    {
        public static Type[] GetPublicTypes(System.Reflection.Assembly asm)
        {
            return asm.GetExportedTypes();
        }

        public static IEnumerable<Type> GetPublicTypes(System.Reflection.Assembly asm, Func<Type, bool> pred)
        {
            return asm.GetExportedTypes().Where(pred);
        }
    
    }
}