using System;
using System.Collections.Generic;
using System.Text;

namespace KeyDash.Models
{
    public class InputChar
    {
        public string Item;
        public int index;
        public WorkKey workKey;

    }
    public enum WorkKey
    {
        Char,
        BackSpace,
        Space
    }
}
