using System.Collections.Generic;
using Serialized_Objects;
using UnityEngine;
using UnityEngine.Serialization;

namespace Grid_System.Word_Related
{
    public class WordGenerator : MonoBehaviour
    {
        [Header("Data References")]
        [SerializeField] private List<WordList> wordData;
        [SerializeField] private int rng;
        
        [Header("Object References")]
        [SerializeField] private GameObject wordPrefab;
        [SerializeField] private GameObject listContainer;
        [SerializeField] private GridGenerator gridGenerator;

        private void Start()
        {
            rng = Random.Range(0, wordData.Count);
            Debug.Log($"Generated Word List: {wordData[rng]}");
            foreach (Transform child in listContainer.transform)
            {
                Destroy(child.gameObject);
            }
            
            foreach (var word in wordData[rng].wordList)
            {
                var newWordObj = Instantiate(wordPrefab, listContainer.transform);
                newWordObj.name = $"{word}";
                newWordObj.GetComponent<WordScript>().SetValues(word);
            }
            gridGenerator.GenerateGrid(rng);
        }
    }
}