using System;
using System.Collections.Generic;

namespace UnityFeedback.Models
{
    public partial class Floppybird
    {
        public int Id { get; set; }
        public long? Score { get; set; }
        public DateTime? Time { get; set; }
        public string Comment { get; set; }
        public bool? Enjoyed { get; set; }
    }
}
