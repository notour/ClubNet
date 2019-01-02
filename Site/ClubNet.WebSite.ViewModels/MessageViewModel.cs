namespace ClubNet.WebSite.ViewModels
{
    /// <summary>
    /// View model used to display a simple message on the view
    /// </summary>
    public sealed class MessageViewModel
    {
        #region Ctor

        /// <summary>
        /// Initialize a new instance of the class <see cref="MessageViewModel"/>
        /// </summary>
        public MessageViewModel(string message, MessageType type)
        {
            Message = message;
            Type = type;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Get the message value
        /// </summary>
        public string Message { get; }

        /// <summary>
        /// Gets the message typ
        /// </summary>
        public MessageType Type { get; }

        #endregion
    }
}
