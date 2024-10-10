using System;
using UnityEngine;

namespace Grid_System
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