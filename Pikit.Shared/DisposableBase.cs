using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pikit.Shared
{
    public abstract class DisposableBase
        : IDisposable
    {
        // https://msdn.microsoft.com/en-us/library/ms244737.aspx
        protected bool _disposed;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~DisposableBase()
        {
            Dispose(false);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                DoDispose();
            }

            DoDisposeNative();

            this._disposed = true;
        }

        protected virtual void DoDispose()
        {

        }

        protected virtual void DoDisposeNative()
        {

        }
    }
}
