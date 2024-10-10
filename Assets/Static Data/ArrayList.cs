using System.Collections.Generic;
using Grid_System.Letter_Related;

namespace Static_Data
{
    public static class ArrayList
    {
        public static readonly Dictionary<string, List<CellData>> CellDataList = 
            new Dictionary<string, List<CellData>>()
            {
                {"word-set-1", 
                    new List<CellData>()
                    {
                    new (0,'S'), new (0,'O'), new (0,'T'), new (0,'E'), new (0,'L'),
                    new (0,'P'), new (0,'E'), new (0,'G'), new (0,'E'), new (0,'Y'),
                    new (0,'E'), new (0,'A'), new (0,'Z'), new (0,'L'), new (0,'R'),
                    new (0,'D'), new (0,'Z'), new (0,'N'), new (0,'E'), new (0,'E'),
                    new (0,'Y'), new (0,'I'), new (0,'L'), new (0,'G'), new (0,'F')
                    }
                }, 
                        
                {"word-set-2", 
                    new List<CellData>() 
                    {
                    new (0,'O'), new (0,'A'), new (0,'I'), new (0,'H'), new (0,'C'),
                    new (0,'Z'), new (0,'W'), new (0,'W'), new (0,'A'), new (0,'T'),
                    new (0,'R'), new (0,'I'), new (0,'I'), new (0,'O'), new (0,'P'),
                    new (0,'A'), new (0,'M'), new (0,'L'), new (0,'N'), new (0,'M'),
                    new (0,'V'), new (0,'I'), new (0,'R'), new (0,'L'), new (0,'G')
                    }
                }
            };
        
        
        
    }
}