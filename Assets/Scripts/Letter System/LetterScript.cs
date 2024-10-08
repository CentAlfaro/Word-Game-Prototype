using System;
using TMPro;
using UnityEngine;

namespace Letter_System
{
    public class LetterScript : MonoBehaviour
    {
        [SerializeField] private char letter;
        [SerializeField] private TextMeshProUGUI text;

        private bool _isCleared = false;

        public bool IsCleared
        {
            get => _isCleared;
            set => _isCleared = value;
        }

        public char Letter => letter;
        public TextMeshProUGUI Text => text;
        
        private void Start()
        {
            text.text = letter.ToString();
        }
    }
}