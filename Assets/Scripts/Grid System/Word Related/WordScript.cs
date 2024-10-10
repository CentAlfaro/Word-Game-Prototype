using TMPro;
using UnityEngine;

namespace Grid_System.Word_Related
{
    public class WordScript : MonoBehaviour
    {
        [SerializeField] private string word;
        [SerializeField] private bool isCleared;
        [SerializeField] private TextMeshProUGUI textMesh;
        
        public bool IsCleared
        {
            get => isCleared;
            set => isCleared = value;
        }

        public string Word => word;

        public void SetValues(string assignedWord)
        {
            word = assignedWord;
            textMesh.text = assignedWord;
        }

        public void ClearWord()
        {
            textMesh.fontStyle = FontStyles.Strikethrough;
            isCleared = true;
        }
    }
}