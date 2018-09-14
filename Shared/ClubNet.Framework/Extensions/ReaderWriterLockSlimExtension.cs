using ClubNet.Framework.Memory;

namespace System.Threading
{
    /// <summary>
    /// Extend the class <see cref="ReaderWriterLockSlim"/>
    /// </summary>
    public static class ReaderWriterLockSlimExtension
    {
        #region Fields

        private static readonly TimeSpan s_defaultTimeout;

        #endregion

        #region Ctor

        /// <summary>
        /// Initialize the class <see cref="ReaderWriterLockSlimExtension"/>
        /// </summary>
        static ReaderWriterLockSlimExtension()
        {
            s_defaultTimeout = TimeSpan.FromMinutes(1);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Scope lock pattern that lock the current <see cref="ReaderWriterLockSlim"/> in read mode
        /// </summary>
        public static IDisposable LockRead(this ReaderWriterLockSlim locker, TimeSpan timeout = default(TimeSpan))
        {
            if (timeout == default(TimeSpan))
                timeout = s_defaultTimeout;

            var isLocked = locker.TryEnterReadLock(timeout);

            if (isLocked)
                return new ScopeLockAction<ReaderWriterLockSlim>(l => l.ExitReadLock(), locker);

            return Disposable.Disposed;
        }

        /// <summary>
        /// Scope lock pattern that lock the current <see cref="ReaderWriterLockSlim"/> in read mode
        /// </summary>
        public static IDisposable LockWrite(this ReaderWriterLockSlim locker, TimeSpan timeout = default(TimeSpan))
        {
            if (timeout == default(TimeSpan))
                timeout = s_defaultTimeout;

            var isLocked = locker.TryEnterWriteLock(timeout);

            if (isLocked)
                return new ScopeLockAction<ReaderWriterLockSlim>(l => l.ExitWriteLock(), locker);

            return Disposable.Disposed;
        }

        #endregion
    }
}
