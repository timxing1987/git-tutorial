using System;
using System.Collections.Generic;
using Microsoft.Practices.Unity.Utility;

namespace Cedar.Framwork.AuditTrail
{
    internal class CompositeAuditLogListener : AuditLogListenerBase
    {
        public CompositeAuditLogListener(IEnumerable<AuditLogListenerBase> listeners)
            : base(Guid.NewGuid().ToString(), null)
        {
            Guard.ArgumentNotNull(listeners, "listeners");
            Listeners = listeners;
        }

        public IEnumerable<AuditLogListenerBase> Listeners { get; }

        protected override void WriteCore(AuditLogEntry logEntry)
        {
            Guard.ArgumentNotNull(logEntry, "logEntry");
            foreach (var current in Listeners)
            {
                current.Write(logEntry);
            }
        }
    }
}