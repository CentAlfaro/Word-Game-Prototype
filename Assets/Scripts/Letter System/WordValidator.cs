using System;
using System.Collections.Generic;
using Events;
using TMPro;
using UnityEngine;

namespace Letter_System
{
    public class WordValidator : MonoBehaviour
    {
        [SerializeField]private List<TextMeshProUGUI> listOfWords = new List<TextMeshProUGUI>();

        private void RunValidation(string wordToValidate)
        {
            foreach (var word in listOfWords)
            {
                if (word.text == wordToValidate)
                {
                    word.fontStyle = FontStyles.Strikethrough;
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