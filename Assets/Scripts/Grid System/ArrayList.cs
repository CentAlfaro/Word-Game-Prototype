using System;
using System.Collections.Generic;
using UnityEngine;

namespace Grid_System
{
    public static class ArrayList
    {
        public static readonly List<CellData> CellData = 
            new()
            {
                new (1,'S'), new (2, 'O'), new (3, 'T'), new (4, 'E'), new (5, 'L'),
                new (-1,'P'), new (-1,'E'), new (-1,'G'), new (-1,'E'), new (-1,'Y'),
                new (-1,'E'), new (-1,'A'), new (-1,'Z'), new (-1,'L'), new (-1,'R'),
                new (-1,'D'), new (-1,'Z'), new (-1,'N'), new (-1,'E'), new (-1,'E'),
                new (-1,'Y') ,new (-1,'I') ,new (-1,'L') ,new (-1,'G') ,new (-1,'F')
            };
    }
}