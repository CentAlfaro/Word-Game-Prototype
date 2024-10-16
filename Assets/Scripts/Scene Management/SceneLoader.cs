using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scene_Management
{
    public class SceneLoader : MonoBehaviour
    {
        public void ChangeScene(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
        }

        public void QuitApplication()
        {
            Application.Quit();
        }
    }
}
