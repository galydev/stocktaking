using System;

namespace Inventory.Domain.Entities
{
    public class Audit
    {
        public Guid AuditId { get; set; }
        public DateTime AuditDate { get; set; }
        public string User { get; set; }
        public string TableName { get; set; }
        public string Action { get; set; }
        public string KeyValues { get; set; }
        public string OldValues { get; set; }
        public string NewValues { get; set; }
    }
}