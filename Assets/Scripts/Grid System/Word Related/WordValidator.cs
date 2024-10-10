using Events;
using UnityEngine;

namespace Grid_System.Word_Related
{
    public class WordValidator : MonoBehaviour
    {
        [SerializeField] private GameObject wordContainer;

        private void RunValidation(string wordToValidate)
        {
            foreach (var child in wordContainer.transform.GetComponentsInChildren<WordScript>())
            {
                if (child.Word == wordToValidate)
                {
                    child.ClearWord();
                    WordSystemEvents.ON_CONVERT_CELL_COLORS?.Invoke(true);
                    return;
                    //if the correct word has been found in the list, stop the validation process.
                }
            }
            
            //if the correct word has not been found in the list, stop the validation process.
            Debug.Log($"{wordToValidate} is an invalid word!");
            WordSystemEvents.ON_CONVERT_CELL_COLORS?.Invoke(false);
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