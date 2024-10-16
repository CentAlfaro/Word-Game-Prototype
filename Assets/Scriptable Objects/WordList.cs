using System.Collections.Generic;
using UnityEngine;

namespace Scriptable_Objects
{
    [CreateAssetMenu(fileName = "Word List", menuName = "Word System/ Word List", order = 0)]
    public class WordList : ScriptableObject
    {
        public string wordListID;
        public List<string> wordList = new List<string>();
    }
}