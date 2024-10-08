using System;
using Unity.VisualScripting;
using UnityEngine;

namespace Events
{
    public class WordSystemEvents : MonoBehaviour
    {
        public static Action<String> ON_WORD_VALIDATION;
        public static Action<bool> ON_CONVERT_CELL_COLORS;
    }
}