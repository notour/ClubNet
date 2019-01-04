using System;

namespace ClubNet.Framework.Memory
{
    /// <summary>
    /// Define a implementation of the scope lock patern using the IDispose pattern
    /// </summary>
    public class ScopeLockAction<TState> : Disposable
    {
        #region Fields

        private readonly Action<TState> _disposeAction;
        private TState _state;

        #endregion

        #region Ctor

        /// <summary>
        /// Initialize a new instance of the class <see cref="ScopeLockAction{TState}"/>
        /// </summary>
        public ScopeLockAction(Action<TState> disposeAction, TState state)
        {
            this._disposeAction = disposeAction;
            this._state = state;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Call the dispose action to trigger the close of the scope
        /// </summary>
        protected override void DisposeInitialeAction()
        {
            try
            {
                if (this._disposeAction != null)
                    this._disposeAction(this._state);
            }
            finally
            {
                this._state = default(TState);
            }
            base.DisposeInitialeAction();
        }

        #endregion
    }

    /// <summary>
    /// Define a implementation of the scope lock patern using the IDispose pattern
    /// </summary>
    public class ScopeLockAction : Disposable
    {
        #region Fields

        private readonly Action _disposeAction;

        #endregion

        #region Ctor

        /// <summary>
        /// Initialize a new instance of the class <see cref="ScopeLockAction{TState}"/>
        /// </summary>
        public ScopeLockAction(Action disposeAction)
        {
            this._disposeAction = disposeAction;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Call the dispose action to trigger the close of the scope
        /// </summary>
        protected override void DisposeInitialeAction()
        {
            if (this._disposeAction != null)
                this._disposeAction();

            base.DisposeInitialeAction();
        }

        #endregion
    }
}
