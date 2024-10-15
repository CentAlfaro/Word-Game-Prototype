using Events;
using UnityEngine;

namespace Grid_System.Word_Related
{
    public class WordValidator : MonoBehaviour
    {
        [SerializeField] private GameObject wordContainer;

        [Header("Word Count References")]
        [SerializeField] private int TotalWordCount = 0;
        [SerializeField] private int completedWordCount = 0;

        public void InitializeWordCount(int assignedCount)
        {
            TotalWordCount = assignedCount;
        }

        private void RunValidation(string wordToValidate)
        {
            foreach (var child in wordContainer.transform.GetComponentsInChildren<WordScript>())
            {
                if (child.Word == wordToValidate)
                {
                    if (child.IsCleared)
                    {
                        Debug.Log($"{wordToValidate} is already cleared!");
                        WordSystemEvents.ON_CONVERT_CELL_COLORS?.Invoke(false);
                        return;
                    }
                    
                    completedWordCount++;
                    child.ClearWord();
                    WordSystemEvents.ON_CONVERT_CELL_COLORS?.Invoke(true);
                    CheckForCompletion();
                    return;
                    //if the correct word has been found in the list, stop the validation process.
                }
            }
            //if the correct word has not been found in the list, stop the validation process.
            Debug.Log($"{wordToValidate} is an invalid word!");
            WordSystemEvents.ON_CONVERT_CELL_COLORS?.Invoke(false);
        }

        private void CheckForCompletion()
        {
            if (completedWordCount >= TotalWordCount)
            {
                Debug.Log("ALL WORDS ARE CLEARED!");
            }
        }

        private void OnEnable()
        {
            WordSystemEvents.ON_WORD_VALIDATION += RunValidation;
        }

        private void OnDisable()
        {
            WordSystemEvents.ON_WORD_VALIDATION -= RunValidation;
        }
    }
}