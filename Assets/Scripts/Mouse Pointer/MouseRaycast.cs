using System.Collections.Generic;
using Events;
using Grid_System;
using Grid_System.Letter_Related;
using Unity.Mathematics;
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
        [SerializeField] private string collectedWord;

        [Header("Script References")]
        [SerializeField] private GridGenerator gridGenerator;

        [Header("Color References")] 
        [SerializeField] private Color defaultColor;
        [SerializeField] private Color highlightColor;
        
        [Header("First Letter Positions")]
        [SerializeField] private int firstLetterXPos;
        [SerializeField] private int firstLetterYPos;
        
        [Header("Last Letter Positions")]
        [SerializeField] private int lastLetterXPos;
        [SerializeField] private int lastLetterYPos;
        
        private GraphicRaycaster mRaycaster;
        private PointerEventData mPointerEventData;
        private EventSystem mEventSystem;
        
        private GameObject latestLetter;

        private GameObject firstLetter;
        private GameObject lastLetter;
        

        void Start()
        {
            //Fetch the Raycaster from the GameObject (the Canvas)
            mRaycaster = GetComponent<GraphicRaycaster>();
            //Fetch the Event System from the Scene
            mEventSystem = GetComponent<EventSystem>();
        }

        void Update()
        {
            UpdatePointerData();
        }

        private void UpdatePointerData()
        {
            #region RAYCAST INITIALIZATION
            //Set up the new Pointer Event and set to the mouse position
            mPointerEventData = new PointerEventData(mEventSystem)
            {
                position = Input.mousePosition
            };

            //Create a list of Raycast Results
            List<RaycastResult> results = new List<RaycastResult>();

            //Raycast using the Graphics Raycaster and mouse click position
            mRaycaster.Raycast(mPointerEventData, results);
            #endregion
            
            #region POINTER UPDATE
            //Check if the left Mouse button is just pressed
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                //For every result returned, output the name of the GameObject on the Canvas hit by the Ray
                foreach (RaycastResult result in results)
                {
                    if (!result.gameObject.CompareTag("LetterCell")) return;
                    
                    WordSystemEvents.ON_CREATE_HIGHLIGHT?.Invoke(result.gameObject);
                    firstLetter = result.gameObject;

                    firstLetterXPos = firstLetter.GetComponent<LetterScript>().XCoord;
                    firstLetterYPos = firstLetter.GetComponent<LetterScript>().YCoord;
                }
            }
            //Check if the left Mouse button is held down
            else if (Input.GetKey(KeyCode.Mouse0))
            {
                if (firstLetter)
                {
                    WordSystemEvents.ON_REPOSITION_HIGHLIGHT?.Invoke();
                }
                else
                {
                    return;
                }
                
                //For every result returned, output the name of the GameObject on the Canvas hit by the Ray
                // foreach (RaycastResult result in results)
                // {
                //     if (!result.gameObject.CompareTag("LetterCell")) return;
                //     //checks if the user is trying to repeat the last letter collected
                //     if (latestLetter == result.gameObject) return;
                //     
                //     //checks if the user is trying to access any of the collected letters
                //     foreach (var letters in collectedLetters)
                //     {
                //         if (result.gameObject == letters)
                //         {
                //             return;
                //         }
                //     }
                //     
                //     //each new collected letter will be added to the list
                //     result.gameObject.GetComponent<LetterScript>().Img.color = highlightColor;
                //     collectedLetters.Add(result.gameObject);
                //     latestLetter = result.gameObject;
                //     collectedWord += result.gameObject.GetComponent<LetterScript>().Letter;
                // }
            }
            
            //Check if the left Mouse button is released
            else if (Input.GetKeyUp(KeyCode.Mouse0))
            {
                if (results.Count <= 0)
                {
                    ConvertWordColor(false);
                    return;
                }
                
                //For every result returned, output the name of the GameObject on the Canvas hit by the Ray
                foreach (RaycastResult result in results)
                {
                    if (!result.gameObject.CompareTag("LetterCell"))
                    {
                        WordSystemEvents.ON_DELETE_HIGHLIGHT?.Invoke();
                        return;
                    }

                    if (result.gameObject == firstLetter)
                    {
                        ConvertWordColor(false);
                        return;
                    }
                    
                    WordSystemEvents.ON_RELEASE_HIGHLIGHT?.Invoke(result.gameObject);
                    lastLetter = result.gameObject;
                    
                    lastLetterXPos = lastLetter.GetComponent<LetterScript>().XCoord;
                    lastLetterYPos = lastLetter.GetComponent<LetterScript>().YCoord;
                }
                
                ConstructWord();
                WordSystemEvents.ON_WORD_VALIDATION?.Invoke(collectedWord);
            }
            #endregion
        }

        private void ConstructWord()
        {
            // These are the coordinates for the plot point that "gathers" each letter.
            var validationYPos = firstLetterYPos;
            var validationXPos = firstLetterXPos;
            
            // The difference is calculated and tested for 45-degree lines
            if (lastLetterYPos != firstLetterYPos && lastLetterXPos != firstLetterXPos)
            {
                var yDifference = math.abs(lastLetterYPos - firstLetterYPos);
                var xDifference = math.abs(lastLetterXPos - firstLetterXPos);

                if (yDifference != xDifference)
                {
                    Debug.Log("MISINPUT");
                    return;
                }
            }

            #region DIRECTIONAL_VALIDATION
            //EAST
            if (lastLetterYPos == firstLetterYPos && lastLetterXPos > firstLetterXPos)
            {
                Debug.Log("EAST");
                while (validationXPos <= lastLetterXPos)
                {
                    foreach (var letter in gridGenerator.Letters)
                    {
                        if (letter.XCoord == validationXPos && letter.YCoord == validationYPos)
                        {
                            collectedWord += letter.Letter;
                        }
                    }
                    validationXPos++;
                }
            }
            
            //WEST
            else if (lastLetterYPos == firstLetterYPos && lastLetterXPos < firstLetterXPos)
            {
                Debug.Log("WEST");
                while (validationXPos >= lastLetterXPos)
                {
                    foreach (var letter in gridGenerator.Letters)
                    {
                        if (letter.XCoord == validationXPos && letter.YCoord == validationYPos)
                        {
                            collectedWord += letter.Letter;
                        }
                    }
                    validationXPos--;
                }
            }
            
            //SOUTH
            else if (lastLetterYPos > firstLetterYPos && lastLetterXPos == firstLetterXPos)
            {
                Debug.Log("SOUTH");
                while (validationYPos <= lastLetterYPos)
                {
                    foreach (var letter in gridGenerator.Letters)
                    {
                        if (letter.XCoord == validationXPos && letter.YCoord == validationYPos)
                        {
                            collectedWord += letter.Letter;
                        }
                    }
                    validationYPos++;
                }
            }
            
            //NORTH
            else if (lastLetterYPos < firstLetterYPos && lastLetterXPos == firstLetterXPos)
            {
                Debug.Log("NORTH");
                while (validationYPos >= lastLetterYPos)
                {
                    foreach (var letter in gridGenerator.Letters)
                    {
                        if (letter.XCoord == validationXPos && letter.YCoord == validationYPos)
                        {
                            collectedWord += letter.Letter;
                        }
                    }
                    validationYPos--;
                }
            }
            
            //NORTH-EAST
            else if (lastLetterYPos < firstLetterYPos && lastLetterXPos > firstLetterXPos)
            {
                Debug.Log("NORTH EAST");
                while (validationYPos >= lastLetterYPos && validationXPos <= lastLetterXPos)
                {
                    foreach (var letter in gridGenerator.Letters)
                    {
                        if (letter.XCoord == validationXPos && letter.YCoord == validationYPos)
                        {
                            collectedWord += letter.Letter;
                        }
                    }
                    validationXPos++;
                    validationYPos--;
                }
            }
            
            //SOUTH-EAST
            else if (lastLetterYPos > firstLetterYPos && lastLetterXPos > firstLetterXPos)
            {
                Debug.Log("SOUTH EAST");
                while (validationYPos <= lastLetterYPos && validationXPos <= lastLetterXPos)
                {
                    foreach (var letter in gridGenerator.Letters)
                    {
                        if (letter.XCoord == validationXPos && letter.YCoord == validationYPos)
                        {
                            collectedWord += letter.Letter;
                        }
                    }
                    validationXPos++;
                    validationYPos++;
                }
            }
            
            //NORTH-WEST
            else if (lastLetterYPos < firstLetterYPos && lastLetterXPos < firstLetterXPos)
            {
                Debug.Log("NORTH WEST");
                while (validationYPos >= lastLetterYPos && validationXPos >= lastLetterXPos)
                {
                    foreach (var letter in gridGenerator.Letters)
                    {
                        if (letter.XCoord == validationXPos && letter.YCoord == validationYPos)
                        {
                            collectedWord += letter.Letter;
                        }
                    }
                    validationXPos--;
                    validationYPos--;
                }
            }
            
            //SOUTH-WEST
            else if (lastLetterYPos > firstLetterYPos && lastLetterXPos < firstLetterXPos)
            {
                Debug.Log("SOUTH WEST");
                while (validationYPos <= lastLetterYPos && validationXPos >= lastLetterXPos)
                {
                    foreach (var letter in gridGenerator.Letters)
                    {
                        if (letter.XCoord == validationXPos && letter.YCoord == validationYPos)
                        {
                            collectedWord += letter.Letter;
                        }
                    }
                    validationXPos--;
                    validationYPos++;
                }
            }
            #endregion
            
            Debug.Log(collectedWord);
        }

        private void ConvertWordColor(bool isWordValid)
        {
            //if the collected strings form a valid word, change the cell colors to default and consider them cleared
            if (isWordValid)
            {
                foreach (var obj in collectedLetters)
                {
                    obj.GetComponent<LetterScript>().Img.color = defaultColor;
                    obj.GetComponent<LetterScript>().IsCleared = true;
                }
                
                WordSystemEvents.ON_REORGANIZE_HIGHLIGHT_MATERIALS?.Invoke();
            }
            
            //if the collected strings does not form a valid word, change back the cell colors to white
            else
            {
                foreach (var obj in collectedLetters)
                {
                    obj.GetComponent<LetterScript>().Img.color = defaultColor;
                }
                WordSystemEvents.ON_DELETE_HIGHLIGHT?.Invoke();
            }
            
            ClearTemporaryData();
        }

        private void ClearTemporaryData()
        {
            collectedLetters.Clear();
            latestLetter = null;
            collectedWord = null;
            firstLetter = null;
            lastLetter = null;
            
            firstLetterXPos = 0;
            firstLetterYPos = 0;
            lastLetterXPos = 0;
            lastLetterYPos = 0;
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