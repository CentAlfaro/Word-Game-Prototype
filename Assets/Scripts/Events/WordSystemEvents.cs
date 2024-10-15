using System;
using Unity.VisualScripting;
using UnityEngine;

namespace Events
{
    public class WordSystemEvents : MonoBehaviour
    {
        public static Action<String> ON_WORD_VALIDATION;
        public static Action<bool> ON_CONVERT_CELL_COLORS;

        public static Action<GameObject> ON_CREATE_HIGHLIGHT;
        public static Action ON_REPOSITION_HIGHLIGHT;
        public static Action<GameObject> ON_RELEASE_HIGHLIGHT;
        public static Action ON_DELETE_HIGHLIGHT;
    }
}