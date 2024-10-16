using System;

namespace Grid_System.Letter_Related
{
    [Serializable]
    public class CellData
    {
        public int AssignedInt;
        public char Letter;
        
        public CellData(int assignedInt, char letter)
        {
            AssignedInt = assignedInt;
            Letter = letter;
        }
    }
}