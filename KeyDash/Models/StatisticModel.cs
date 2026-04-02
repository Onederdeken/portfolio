using System;
using System.Collections.Generic;
using System.Text;

namespace KeyDash.Models
{
    public class StatisticModel
    {
        public int CurrentTyped = 0;
        public int CorrectCount = 0;
        public int TotalTyped = 0;
        public List<InputEntry> Characters = new List<InputEntry>();
    }
    public struct InputEntry
    {
        public int index;
        public char Char;
        public bool IsCorrect;
    }
}
