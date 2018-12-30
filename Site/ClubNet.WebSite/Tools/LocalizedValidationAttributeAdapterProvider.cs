namespace ClubNet.WebSite.Tools
{
    using ClubNet.WebSite.Resources;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc.DataAnnotations;
    using Microsoft.Extensions.Localization;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Threading.Tasks;

    /// <summary>
    /// Localized the error messaged
    /// </summary>
    class LocalizedValidationAttributeAdapterProvider : IValidationAttributeAdapterProvider
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
            _stringLocalizerFactory = stringLocalizerFactory;
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
                stringLocalizer = new StringLocalizer<ErrorMessages>(_stringLocalizerFactory);
            }

            return _originalProvider.GetAttributeAdapter(attribute, stringLocalizer);
        }

        #endregion
    }
}
