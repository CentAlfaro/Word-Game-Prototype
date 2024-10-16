using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Grid_System.Letter_Related
{
    public class LetterScript : MonoBehaviour
    {
        [Header("Assigned Values")]
        [SerializeField] private char letter = 'A';
        [SerializeField] private int pairId = 0;
        
        [Header("UI References")]
        [SerializeField] private TextMeshProUGUI text;
        [SerializeField] private Image img;

        private bool _isCleared = false;

        public bool IsCleared
        {
            get => _isCleared;
            set => _isCleared = value;
        }

        public char Letter => letter;
        public TextMeshProUGUI Text => text;

        public Image Img => img;

        public void SetValues(char assignedLetter, int assignedInt)
        {
            letter = assignedLetter;
            pairId = assignedInt;
            
            text.text = letter.ToString();
        }
    }
}
