using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace ClubNet.Framework.Memory
{
    /// <summary>
    /// Base class that implement the pattern IDispose
    /// </summary>
    public class Disposable : IDisposable
    {
        #region Fields

        private static readonly Disposable s_disposed;
        private long _disposeCount;

        #endregion

        #region Ctor

        /// <summary>
        /// Initialize the class <see cref="Disposable"/>
        /// </summary>
        static Disposable()
        {
            s_disposed = new Disposable();
            s_disposed.Dispose();
        }

        /// <summary>
        /// Finalize an instance of the clas <see cref="Disposable"/>
        /// </summary>
        ~Disposable()
        {
            Dispose(true);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets a value indicating if the current instance if disposed
        /// </summary>
        public bool IsDisposed
        {
            get { return Interlocked.Read(ref _disposeCount) != 0; }
        }

        /// <summary>
        /// Gets an instance already disposed
        /// </summary>
        public static IDisposable Disposed
        {
            get { return s_disposed; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Release resources used by this instance
        /// </summary>
        public void Dispose()
        {
            GC.SuppressFinalize(this);
            Dispose(false);
        }

        /// <summary>
        /// Release resources used by this instance
        /// </summary>
        protected virtual void Dispose(bool fromFinalizer)
        {
            if (Interlocked.Increment(ref _disposeCount) > 1)
            {
                // to prevent long max born to be exceeded
                Interlocked.Decrement(ref _disposeCount);
                return;
            }

            DisposeInitialeAction();

            if (!fromFinalizer)
                DisposeManagedData();

            DisposeUnmanagedData();

            DisposeFinalizer();
        }

        /// <summary>
        /// Called at the end of the dispose process
        /// </summary>
        protected virtual void DisposeFinalizer()
        {
        }

        /// <summary>
        /// Called to release unmanged data
        /// </summary>
        protected virtual void DisposeUnmanagedData()
        {
        }

        /// <summary>
        /// Called to dispose managed data
        /// </summary>
        protected virtual void DisposeManagedData()
        {
        }

        /// <summary>
        /// Called first during the disposing steps
        /// </summary>
        protected virtual void DisposeInitialeAction()
        {
        }

        #endregion

    }
}
