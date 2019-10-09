using System;
using System.Collections.Generic;

namespace Feedback.Models
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
