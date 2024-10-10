using Grid_System.Letter_Related;
using Static_Data;
using UnityEngine;
using UnityEngine.Serialization;

namespace Grid_System
{
    public class GridGenerator : MonoBehaviour
    {
        [SerializeField] private GameObject letterPrefab;
        [SerializeField] private GameObject gridContainer;
        
        public void GenerateGrid(int dataInt)
        {
            foreach (Transform child in gridContainer.transform)
            {
                Destroy(child.gameObject);
            }
            
            foreach (var cellData in ArrayList.CellDataList[dataInt])
            {
                var currentLetter = Instantiate(letterPrefab, gridContainer.transform);
                currentLetter.name = $"Letter {cellData.Letter}";
                currentLetter.GetComponentInChildren<LetterScript>().SetValues(cellData.Letter, cellData.AssignedInt);
            }
        }
    }
}
