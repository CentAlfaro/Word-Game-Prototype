using System;
using System.Collections.Generic;
using Events;
using Grid_System.Letter_Related;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Mouse_Pointer
{
    public class MouseRaycast : MonoBehaviour
    {
        [Header("Temporary Data Variables")]
        [SerializeField] private List<GameObject> collectedLetters = new List<GameObject>();
        [FormerlySerializedAs("word")] [SerializeField] private string collectedWord;
        
        private GraphicRaycaster m_Raycaster;
        private PointerEventData m_PointerEventData;
        private EventSystem m_EventSystem;
        
        private GameObject latestLetter = null;
        

        void Start()
        {
            //Fetch the Raycaster from the GameObject (the Canvas)
            m_Raycaster = GetComponent<GraphicRaycaster>();
            //Fetch the Event System from the Scene
            m_EventSystem = GetComponent<EventSystem>();
        }

        void Update()
        {
            //Check if the left Mouse button is clicked
            if (Input.GetKey(KeyCode.Mouse0))
            {
                //Set up the new Pointer Event and set to the mouse position
                m_PointerEventData = new PointerEventData(m_EventSystem);
                m_PointerEventData.position = Input.mousePosition;

                //Create a list of Raycast Results
                List<RaycastResult> results = new List<RaycastResult>();

                //Raycast using the Graphics Raycaster and mouse click position
                m_Raycaster.Raycast(m_PointerEventData, results);

                //For every result returned, output the name of the GameObject on the Canvas hit by the Ray
                foreach (RaycastResult result in results)
                {
                    if (!result.gameObject.CompareTag("LetterCell")) return;
                    //checks if the user is trying to repeat the last letter collected
                    if (latestLetter == result.gameObject) return;
                    
                    //checks if the user is trying to access any of the collected letters
                    foreach (var letters in collectedLetters)
                    {
                        if (result.gameObject == letters)
                        {
                            return;
                        }
                    }
                    
                    //each new collected letter will be added to the list
                    result.gameObject.GetComponent<LetterScript>().Img.color = Color.red;
                    collectedLetters.Add(result.gameObject);
                    latestLetter = result.gameObject;
                    collectedWord += result.gameObject.GetComponent<LetterScript>().Letter;
                }
            }
            
            //Check if the left Mouse button is released
            else if (Input.GetKeyUp(KeyCode.Mouse0))
            {
                WordSystemEvents.ON_WORD_VALIDATION?.Invoke(collectedWord);
            }
        }

        private void ConvertWordColor(bool isWordValid)
        {
            //if the collected strings form a valid word, change the cell colors to green and consider them cleared
            if (isWordValid)
            {
                foreach (var obj in collectedLetters)
                {
                    obj.GetComponent<LetterScript>().Img.color = Color.green;
                    obj.GetComponent<LetterScript>().IsCleared = true;
                }
            }
            
            //if the collected strings does not form a valid word, change back the cell colors to white
            else
            {
                foreach (var obj in collectedLetters)
                {
                    //if the collected word includes a cleared word, retain its cell's green color
                    if (obj.GetComponent<LetterScript>().IsCleared)
                    {
                        obj.GetComponent<LetterScript>().Img.color = Color.green;
                        continue;
                    }
                    obj.GetComponent<LetterScript>().Img.color = Color.white;
                }
            }
            
            ClearTemporaryData();
        }

        private void ClearTemporaryData()
        {
            collectedLetters.Clear();
            latestLetter = null;
            collectedWord = null;
        }

        private void OnEnable()
        {
            WordSystemEvents.ON_CONVERT_CELL_COLORS += ConvertWordColor;
        }

        private void OnDisable()
        {
            WordSystemEvents.ON_CONVERT_CELL_COLORS -= ConvertWordColor;
        }
    }
}