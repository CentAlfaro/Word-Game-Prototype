using UnityEngine;

namespace Grid_System
{
    public class GridGenerator : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            foreach (var cellData in ArrayList.CellData)
            {
                Debug.Log($"Letter: {cellData.Letter} || AssignedInt: {cellData.AssignedInt}");
            }
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
