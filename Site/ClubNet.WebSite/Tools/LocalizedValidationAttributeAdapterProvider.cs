namespace ClubNet.WebSite.Tools
{
    using System.ComponentModel.DataAnnotations;
    using ClubNet.WebSite.Resources;
    using Microsoft.AspNetCore.Mvc.DataAnnotations;
    using Microsoft.Extensions.Localization;

    /// <summary>
    /// Localized the error messaged
    /// </summary>
    internal class LocalizedValidationAttributeAdapterProvider : IValidationAttributeAdapterProvider
    {
        #region Fields

        private readonly ValidationAttributeAdapterProvider _originalProvider = new ValidationAttributeAdapterProvider();
        private readonly IStringLocalizerFactory _stringLocalizerFactory;

        #endregion

        #region Ctor

        /// <summary>
        /// Initialize a new instance of the class <see cref="LocalizedValidationAttributeAdapterProvider"/>
        /// </summary>
        /// <param name="contextAccessor"></param>
        public LocalizedValidationAttributeAdapterProvider(IStringLocalizerFactory stringLocalizerFactory)
        {
            this._stringLocalizerFactory = stringLocalizerFactory;
        }

        #endregion

        #region Fields

        /// <summary>
        /// Provide the error message adaptor
        /// </summary>
        public IAttributeAdapter GetAttributeAdapter(ValidationAttribute attribute, IStringLocalizer stringLocalizer)
        {
            attribute.ErrorMessage = attribute.GetType().Name.Replace("Attribute", string.Empty);
            if (attribute is DataTypeAttribute dataTypeAttribute)
                attribute.ErrorMessage += "_" + dataTypeAttribute.DataType;

            if (stringLocalizer == null)
            {
                stringLocalizer = new StringLocalizer<ErrorMessages>(this._stringLocalizerFactory);
            }

            return this._originalProvider.GetAttributeAdapter(attribute, stringLocalizer);
        }

        #endregion
    }
}
