﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ClubNet.WebSite.Resources {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "16.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class ErrorMessages {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal ErrorMessages() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("ClubNet.WebSite.Resources.ErrorMessages", typeof(ErrorMessages).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to To many login attempt locked you account. Plz contact the administrator..
        /// </summary>
        internal static string AccountLocked {
            get {
                return ResourceManager.GetString("AccountLocked", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The password and the confirmation password are not identical..
        /// </summary>
        internal static string ConfirmationPasswordNotEquals {
            get {
                return ResourceManager.GetString("ConfirmationPasswordNotEquals", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to A user already exists with this email address.
        /// </summary>
        internal static string Conflict_CreateAsync {
            get {
                return ResourceManager.GetString("Conflict_CreateAsync", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to You need to validate your email before behing able to connect..
        /// </summary>
        internal static string EmailConfirmationRequired {
            get {
                return ResourceManager.GetString("EmailConfirmationRequired", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The code provide to confirme the email address is in valid or the user already validate his email address..
        /// </summary>
        internal static string InvalidEmailConfirmationCode {
            get {
                return ResourceManager.GetString("InvalidEmailConfirmationCode", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to An error have been logged. Plz contact the administartor with the current Id : {0}.
        /// </summary>
        internal static string Logged_Default {
            get {
                return ResourceManager.GetString("Logged_Default", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Login attempt failed..
        /// </summary>
        internal static string LoginFailed {
            get {
                return ResourceManager.GetString("LoginFailed", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The password rules required a lower alphabetic value..
        /// </summary>
        internal static string PasswordRequiresLower {
            get {
                return ResourceManager.GetString("PasswordRequiresLower", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The password rules required a non alphanumeric value..
        /// </summary>
        internal static string PasswordRequiresNonAlphanumeric {
            get {
                return ResourceManager.GetString("PasswordRequiresNonAlphanumeric", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The password rules required a upper alphabetic value..
        /// </summary>
        internal static string PasswordRequiresUpper {
            get {
                return ResourceManager.GetString("PasswordRequiresUpper", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The recaptcha validation failed..
        /// </summary>
        internal static string RecaptchaFailed {
            get {
                return ResourceManager.GetString("RecaptchaFailed", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The {0} is required..
        /// </summary>
        internal static string RequiredAttribute {
            get {
                return ResourceManager.GetString("RequiredAttribute", resourceCulture);
            }
        }
    }
}
