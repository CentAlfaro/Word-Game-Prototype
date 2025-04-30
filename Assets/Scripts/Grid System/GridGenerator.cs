using System.Collections.Generic;
using Grid_System.Letter_Related;
using Static_Data;
using UnityEngine;
using UnityEngine.Serialization;

namespace Grid_System
{
    public class GridGenerator : MonoBehaviour
    {
        [Header("Grid Items")]
        [SerializeField] private List<LetterScript> letters;
        
        [Header("References")]
        [SerializeField] private GameObject letterPrefab;
        [SerializeField] private GameObject gridContainer;

        public List<LetterScript> Letters => letters;
        
        public void GenerateGrid(string dataID, int xSize, int ySize)
        {
            var xCoordinate = 1;
            var yCoordinate = 1;
            
            foreach (Transform child in gridContainer.transform)
            {
                Destroy(child.gameObject);
            }
            
            foreach (var cellData in ArrayList.CellDataList[dataID])
            {
                var currentLetter = Instantiate(letterPrefab, gridContainer.transform);
                currentLetter.name = $"{cellData.Letter} holder";
                currentLetter.GetComponentInChildren<LetterScript>().SetValues(xCoordinate, yCoordinate, cellData.Letter, cellData.AssignedInt);
                currentLetter.GetComponentInChildren<LetterScript>().gameObject.name = $"Letter {cellData.Letter}";
                letters.Add(currentLetter.GetComponentInChildren<LetterScript>());

                if (xCoordinate < xSize)
                {
                    xCoordinate++;
                }
                else
                {
                    xCoordinate = 1;
                    yCoordinate++;
                }
            }
        }
    }
}
