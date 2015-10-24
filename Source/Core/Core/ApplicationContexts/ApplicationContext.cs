using System;
using System.Globalization;
using System.Threading;
using System.Web;
using Cedar.Core.ApplicationContexts.Configuration;
using Cedar.Core.IoC;
using Cedar.Core.Properties;
using Microsoft.Practices.Unity.Utility;

namespace Cedar.Core.ApplicationContexts
{
    /// <summary>
    ///     supply the basic application context opertions
    /// </summary>
    public sealed class ApplicationContext
    {
        /// <summary>
        ///     define the context header's namespace.
        /// </summary>
        public const string ContextHeaderNamespace = "http://www.Cedar.co";

        /// <summary>
        ///     define the context header's local name.
        /// </summary>
        public const string ContextHeaderLocalName = "Applicationcontext";

        /// <summary>
        ///     The context http header name.
        /// </summary>
        public const string ContextHttpHeaderName = "Cedar.Core.Applicationcontext";

        private const string KeyofUserId = "Cedar.ApplicationContexts.UserId";
        private const string KeyofUserName = "Cedar.ApplicationContexts.UserName";
        private const string KeyofTransactionId = "Cedar.ApplicationContexts.TransactionId";
        private const string KeyofTimeZone = "Cedar.ApplicationContexts.TimeZone";
        private const string KeyofCulture = "Cedar.ApplicationContexts.Culture";
        private const string KeyofUICulture = "Cedar.ApplicationContexts.UICulture";
        private const string KeyofSessionId = "Cedar.ApplicationContexts.SessionId";
        private static ApplicationContext current;

        private ApplicationContext(IContextLocator contextLocator, ContextAttachBehavior contextAttachBehavior)
        {
            Guard.ArgumentNotNull(contextLocator, "contextLocator");
            ContextLocator = contextLocator;
            ContextAttachBehavior = contextAttachBehavior;
        }

        /// <summary>
        ///     get the current application context
        /// </summary>
        public static ApplicationContext Current
        {
            get
            {
                if (current == null)
                {
                    lock (typeof (ApplicationContext))
                    {
                        if (current == null)
                        {
                            current = CreateApplicationContext();
                        }
                    }
                }
                return current;
            }
        }

        /// <summary>
        ///     get or private set the context locator interface.
        /// </summary>
        public IContextLocator ContextLocator { get; }

        /// <summary>
        ///     get or private set the context attach behavior.
        /// </summary>
        public ContextAttachBehavior ContextAttachBehavior { get; private set; }

        /// <summary>
        ///     Gets the <see cref="T:Cedar.Core.ApplicationContexts.ContextItem" /> with the specified key.
        /// </summary>
        /// <value>The <see cref="T:Cedar.Core.ApplicationContexts.ContextItem" />.</value>
        public ContextItem this[string key]
        {
            get { return ContextLocator.GetContextItem(key); }
        }

        /// <summary>
        ///     Get or set the Id of the current user.
        /// </summary>
        /// <value>The the Id of the current user.</value>
        public string UserId
        {
            get
            {
                var value = GetValue<string>(KeyofUserId);
                if (string.IsNullOrEmpty(value))
                {
                    SetContext(KeyofUserId, string.Empty);
                    return string.Empty;
                }
                return GetValue<string>(KeyofUserId);
            }
            set { SetContext(KeyofUserId, value); }
        }

        /// <summary>
        ///     Gets the user id context item.
        /// </summary>
        /// <value>The user id context item.</value>
        public ContextItem UserIdContextItem
        {
            get
            {
                var value = GetValue<string>(KeyofUserId);
                if (string.IsNullOrEmpty(value))
                {
                    SetContext(KeyofUserId, string.Empty);
                }
                return this[KeyofUserId];
            }
        }

        /// <summary>
        ///     Get or set the Id of the current session.
        /// </summary>
        /// <value>The the Id of the current session.</value>
        public string SessionId
        {
            get
            {
                var value = GetValue<string>(KeyofSessionId);
                if (string.IsNullOrEmpty(value))
                {
                    SetContext(KeyofSessionId, string.Empty);
                    return string.Empty;
                }
                return GetValue<string>(KeyofSessionId);
            }
            set { SetContext(KeyofSessionId, value); }
        }

        /// <summary>
        ///     Gets the session id context item.
        /// </summary>
        /// <value>The session id context item.</value>
        public ContextItem SessionIdContextItem
        {
            get
            {
                var value = GetValue<string>(KeyofSessionId);
                if (string.IsNullOrEmpty(value))
                {
                    SetContext(KeyofSessionId, string.Empty);
                }
                return this[KeyofSessionId];
            }
        }

        /// <summary>
        ///     Get or set the name of the current user.
        /// </summary>
        /// <value>The name of the current user.</value>
        public string UserName
        {
            get
            {
                var value = GetValue<string>(KeyofUserName);
                if (string.IsNullOrEmpty(value))
                {
                    var value2 = string.Empty;
                    if (HttpContext.Current != null && HttpContext.Current.User != null)
                    {
                        value2 = HttpContext.Current.User.Identity.Name;
                    }
                    if (string.IsNullOrEmpty(value2) && Thread.CurrentPrincipal != null)
                    {
                        value2 = Thread.CurrentPrincipal.Identity.Name;
                    }
                    SetContext(KeyofUserName, value2);
                }
                return GetValue<string>(KeyofUserName);
            }
            set { SetContext(KeyofUserName, value); }
        }

        /// <summary>
        ///     Gets the user name context item.
        /// </summary>
        /// <value>The user name context item.</value>
        public ContextItem UserNameContextItem
        {
            get
            {
                var value = GetValue<string>(KeyofUserName);
                if (string.IsNullOrEmpty(value))
                {
                    SetContext(KeyofUserName, string.Empty);
                }
                return this[KeyofUserName];
            }
        }

        /// <summary>
        ///     Gets or sets the id of the current ambient transaction.
        /// </summary>
        /// <value>The transaction id.</value>
        public string TransactionId
        {
            get
            {
                var value = GetValue<string>(KeyofTransactionId);
                if (string.IsNullOrEmpty(value))
                {
                    SetContext(KeyofTransactionId, string.Empty);
                    return string.Empty;
                }
                return GetValue<string>(KeyofTransactionId);
            }
            set { SetContext(KeyofTransactionId, value); }
        }

        /// <summary>
        ///     Gets the transaction id context item.
        /// </summary>
        /// <value>The transaction id context item.</value>
        public ContextItem TransactionIdContextItem
        {
            get
            {
                var value = GetValue<string>(KeyofTransactionId);
                if (string.IsNullOrEmpty(value))
                {
                    SetContext(KeyofTransactionId, string.Empty);
                }
                return this[KeyofTransactionId];
            }
        }

        /// <summary>
        ///     Gets or sets the time zone.
        /// </summary>
        /// <value>The time zone.</value>
        public TimeZoneInfo TimeZone
        {
            get
            {
                var value = GetValue<string>(KeyofTimeZone);
                if (string.IsNullOrEmpty(value))
                {
                    SetContext(KeyofTimeZone, TimeZoneInfo.Local.ToSerializedString());
                    return TimeZoneInfo.Local;
                }
                return TimeZoneInfo.FromSerializedString(value);
            }
            set
            {
                Guard.ArgumentNotNull(value, "value");
                SetContext(KeyofTimeZone, value.ToSerializedString());
            }
        }

        /// <summary>
        ///     Gets the time zone context item.
        /// </summary>
        /// <value>The time zone context item.</value>
        public ContextItem TimeZoneContextItem
        {
            get
            {
                GetValue<string>(KeyofTimeZone);
                if (string.IsNullOrEmpty(KeyofTimeZone))
                {
                    SetContext(KeyofTimeZone, TimeZoneInfo.Local);
                }
                return this[KeyofTimeZone];
            }
        }

        /// <summary>
        ///     Gets or sets the current culture.
        /// </summary>
        /// <value>The current culture.</value>
        public CultureInfo Culture
        {
            get
            {
                var value = GetValue<string>(KeyofCulture);
                if (string.IsNullOrEmpty(value))
                {
                    SetContext(KeyofCulture, CultureInfo.CurrentCulture.Name);
                    return CultureInfo.CurrentCulture;
                }
                return new CultureInfo(GetValue<string>(KeyofCulture));
            }
            set
            {
                Guard.ArgumentNotNull(value, "value");
                SetContext(KeyofCulture, value.Name);
            }
        }

        /// <summary>
        ///     Gets the culture context item.
        /// </summary>
        /// <value>The culture context item.</value>
        public ContextItem CultureContextItem
        {
            get
            {
                GetValue<string>(KeyofCulture);
                if (string.IsNullOrEmpty(KeyofCulture))
                {
                    SetContext(KeyofCulture, CultureInfo.CurrentCulture.Name);
                }
                return this[KeyofCulture];
            }
        }

        /// <summary>
        ///     Gets or sets the current UI culture.
        /// </summary>
        /// <value>The current UI culture.</value>
        public CultureInfo UICulture
        {
            get
            {
                var value = GetValue<string>(KeyofUICulture);
                if (string.IsNullOrEmpty(value))
                {
                    SetContext(KeyofUICulture, CultureInfo.CurrentUICulture.Name);
                    return CultureInfo.CurrentUICulture;
                }
                return new CultureInfo(GetValue<string>(KeyofUICulture));
            }
            set
            {
                Guard.ArgumentNotNull(value, "value");
                SetContext(KeyofUICulture, value.Name);
            }
        }

        /// <summary>
        ///     Gets the UI culture context item.
        /// </summary>
        /// <value>The UI culture context item.</value>
        public ContextItem UICultureContextItem
        {
            get
            {
                GetValue<string>(KeyofUICulture);
                if (string.IsNullOrEmpty(KeyofUICulture))
                {
                    SetContext(KeyofUICulture, CultureInfo.CurrentUICulture.Name);
                }
                return this[KeyofUICulture];
            }
        }

        private static ApplicationContext CreateApplicationContext()
        {
            ApplicationContextSettings applicationContextSettings;
            if (!ConfigManager.TryGetConfigurationSection(out applicationContextSettings))
            {
                return new ApplicationContext(new CallContextLocator(), ContextAttachBehavior.Clear);
            }
            var service =
                ServiceLocatorFactory.GetServiceLocator(null)
                    .GetService<IContextLocator>(applicationContextSettings.DefaultContextLocator);
            var contextAttachBehavior = applicationContextSettings.ContextAttachBehavior;
            return new ApplicationContext(service, contextAttachBehavior);
        }

        /// <summary>
        ///     Gets the value of the <see cref="T:Cedar.Core.ApplicationContexts.ContextItem" /> with the specified key.
        /// </summary>
        /// <typeparam name="TValue">
        ///     The type of the value of <see cref="T:Cedar.Core.ApplicationContexts.ContextItem" />
        /// </typeparam>
        /// <param name="key">The key of the <see cref="T:Cedar.Core.ApplicationContexts.ContextItem" /> to get.</param>
        /// <returns>The value of the <see cref="T:Cedar.Core.ApplicationContexts.ContextItem" /> to get. </returns>
        public TValue GetValue<TValue>(string key)
        {
            Guard.ArgumentNotNull(key, "key");
            var contextItem = ContextLocator.GetContextItem(key);
            if (contextItem != null)
            {
                return (TValue) contextItem.Value;
            }
            return default(TValue);
        }

        /// <summary>
        ///     Sets the context.
        /// </summary>
        /// <param name="key">The key of the <see cref="T:Cedar.Core.ApplicationContexts.ContextItem" />.</param>
        /// <param name="value">The value of the <see cref="T:Cedar.Core.ApplicationContexts.ContextItem" />.</param>
        /// <exception cref="T:System.InvalidOperationException"></exception>
        public void SetContext(string key, object value)
        {
            Guard.ArgumentNotNull(key, "key");
            var contextItem = ContextLocator.GetContextItem(key);
            if (contextItem != null && contextItem.ReadOnly)
            {
                throw new InvalidOperationException(ResourceUtility.Format(Resources.ExceptionCannotModifyReadonlyValue));
            }
            if (contextItem != null)
            {
                contextItem.Value = value;
                return;
            }
            contextItem = new ContextItem(key, value, false);
            ContextLocator.SetContextItem(contextItem);
        }

        /// <summary>
        ///     Sets the context.
        /// </summary>
        /// <param name="key">The key of the <see cref="T:Cedar.Core.ApplicationContexts.ContextItem" />.</param>
        /// <param name="value">The value of the <see cref="T:Cedar.Core.ApplicationContexts.ContextItem" />.</param>
        /// <param name="isLocal">if set to <c>true</c> [is local].</param>
        /// <exception cref="T:System.InvalidOperationException"></exception>
        public void SetContext(string key, object value, bool isLocal)
        {
            Guard.ArgumentNotNull(key, "key");
            var contextItem = ContextLocator.GetContextItem(key);
            if (contextItem != null && contextItem.ReadOnly)
            {
                throw new InvalidOperationException(ResourceUtility.Format(Resources.ExceptionCannotModifyReadonlyValue));
            }
            if (contextItem != null && contextItem.IsLocal == isLocal)
            {
                contextItem.Value = value;
                return;
            }
            contextItem = new ContextItem(key, value, isLocal);
            ContextLocator.SetContextItem(contextItem);
        }
    }
}