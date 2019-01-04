namespace Microsoft.AspNetCore.Mvc.ModelBinding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.Extensions.Logging;
    using Newtonsoft.Json;

    /// <summary>
    /// Helpers to save and restore the model state
    /// </summary>
    public static class ModelStateExtension
    {
        #region Nested

        private class ModelStateTransferValue
        {
            public string Key { get; set; }
            public string AttemptedValue { get; set; }
            public object RawValue { get; set; }
            public IEnumerable<string> ErrorMessages { get; set; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Save the current model state
        /// </summary>
        public static string Save(this ModelStateDictionary modelState)
        {
            var errorList = modelState
                .Select(kvp => new ModelStateTransferValue
                {
                    Key = kvp.Key,
                    AttemptedValue = kvp.Value.AttemptedValue,
                    RawValue = kvp.Value.RawValue,
                    ErrorMessages = kvp.Value.Errors.Select(err => err.ErrorMessage).ToArray(),
                }).ToArray();

            return JsonConvert.SerializeObject(errorList);
        }

        /// <summary>
        /// Restore the saved states
        /// </summary>
        public static void Restore(this ModelStateDictionary state, string serialisedErrorList, ILogger logger)
        {
            try
            {
                var errorList = JsonConvert.DeserializeObject<ModelStateTransferValue[]>(serialisedErrorList);
                var currentModelState = new ModelStateDictionary();

                foreach (var item in errorList)
                {
                    currentModelState.SetModelValue(item.Key, item.RawValue, item.AttemptedValue);
                    foreach (var error in item.ErrorMessages)
                    {
                        currentModelState.AddModelError(item.Key, error);
                    }
                }

                state.Merge(currentModelState);
            }
            catch (Exception ex)
            {
                logger.Log(LogLevel.Error, ex, serialisedErrorList);
            }
        }

        #endregion
    }
}
