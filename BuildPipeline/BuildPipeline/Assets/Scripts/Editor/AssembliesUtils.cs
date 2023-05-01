using System;
using System.Collections.Generic;
using System.Linq;

namespace BlackRefactory.Utils
{
    public static class AssembliesUtils
    {

        public static IEnumerable<T> GetSubclassesOfBaseClass<T>()
        {
            return AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(assembly => assembly.GetTypes())
                .Where(type => type.IsSubclassOf(typeof(T)))
                .Select(type => (T)Activator.CreateInstance(type));
        }
    }
}

