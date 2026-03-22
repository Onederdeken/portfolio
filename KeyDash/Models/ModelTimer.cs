using System;
using System.Collections.Generic;
using System.Text;

namespace KeyDash.Models
{
    public class ModelTimer
    {
        public Operation operation { get; set; }
        public int startTime { get; set; }
        public int endTime { get; set; }

    }
    public enum Operation
    {
        Plus,
        Minus
    }

}
