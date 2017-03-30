using Ninject;
using Ninject.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pikit.Shared
{
    public class Kernel
    {
        private static StandardKernel kernel;
        public static void Initialize()
        {
            Dispose();
            kernel = new StandardKernel();
        }

        public static void Load(
            INinjectModule module)
        {
            kernel.Load(module);
        }

        public static object Get(
            Type t)
        {
            if (kernel == null)
                return null;

            return kernel.Get(t);
        }

        public static T Get<T>()
        {
            if (kernel == null)
                return default(T);

            return kernel.Get<T>();
        }

        public static void Dispose()
        {
            if (kernel != null)
            {
                kernel.Dispose();
                kernel = null;
            }
        }
    }
}
