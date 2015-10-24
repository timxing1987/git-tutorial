using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Reflection;
using Cedar.Core;
using Cedar.Core.ApplicationContexts;

namespace Cedar.Framwork.AuditTrail
{
    /// <summary>
    ///     This class represents a single audit log entry.
    /// </summary>
    public class AuditLogEntry
    {
        /// <summary>
        /// </summary>
        /// <param name="functionName"></param>
        /// <param name="transactionId"></param>
        /// <param name="userid"></param>
        /// <param name="appVersion"></param>
        /// <param name="logTime"></param>
        public AuditLogEntry(string functionName, string transactionId = null, string userid = null,
            string appVersion = null, DateTime? logTime = null)
        {
            FunctionName = functionName;
            TransactionId = (string.IsNullOrEmpty(transactionId)
                ? ApplicationContext.Current.TransactionId
                : transactionId);
            UserId = (string.IsNullOrEmpty(userid) ? ApplicationContext.Current.UserId : userid);
            AppVersion = (string.IsNullOrEmpty(appVersion) ? GetProjectAssemblyVersion() : appVersion);
            Items = new Collection<AuditLogEntryItem>();
            LogTime = (logTime.HasValue ? logTime.Value : TimeConverter.CurrentDateTime);
        }

        /// <summary>
        ///     Gets the app version.
        /// </summary>
        /// <value>
        ///     The app version.
        /// </value>
        public string AppVersion { get; private set; }

        /// <summary>
        ///     Gets the name of the function.
        /// </summary>
        /// <value>
        ///     The name of the function.
        /// </value>
        public string FunctionName { get; private set; }

        /// <summary>
        ///     Gets the transaction id.
        /// </summary>
        /// <value>
        ///     The transaction id.
        /// </value>
        public string TransactionId { get; private set; }

        /// <summary>
        ///     Gets the name of the user.
        /// </summary>
        /// <value>
        ///     The name of the user.
        /// </value>
        public string UserId { get; private set; }

        /// <summary>
        ///     Gets the log time.
        /// </summary>
        /// <value>
        ///     The log time.
        /// </value>
        public DateTime LogTime { get; private set; }

        /// <summary>
        ///     Gets or sets the action.
        /// </summary>
        /// <value>
        ///     The action.
        /// </value>
        public string Action { get; set; }

        /// <summary>
        ///     Gets or sets the action description.
        /// </summary>
        /// <value>
        ///     The action description.
        /// </value>
        public string ActionDescription { get; set; }

        /// <summary>
        ///     Gets or sets the InputsParams.
        /// </summary>
        /// <value>
        ///     The InputsParams
        /// </value>
        public string InputsParams { get; set; }

        /// <summary>
        ///     Gets or sets the action remarks.
        /// </summary>
        /// <value>
        ///     The action remarks.
        /// </value>
        public dynamic ActionRemarks { get; set; }

        /// <summary>
        ///     Gets the audit log entry items.
        /// </summary>
        /// <value>
        ///     The audit log entry items.
        /// </value>
        public ICollection<AuditLogEntryItem> Items { get; private set; }

        /// <summary>
        ///     Gets the project assembly version.
        /// </summary>
        /// <returns>The version no of current project assembly version. </returns>
        protected virtual string GetProjectAssemblyVersion()
        {
            if (Assembly.GetEntryAssembly() != null)
            {
                return Assembly.GetEntryAssembly().GetName().Version.ToString();
            }
            var frames = new StackTrace().GetFrames();
            var array = frames;
            for (var i = 0; i < array.Length; i++)
            {
                var stackFrame = array[i];
                var name = stackFrame.GetMethod().Module.Assembly.GetName();
                if (name.Name != typeof (AuditLogEntry).Assembly.GetName().Name)
                {
                    return name.Version.ToString();
                }
            }
            return Assembly.GetExecutingAssembly().GetName().Version.ToString();
        }
    }
}