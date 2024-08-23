using UnityEngine;
using UnityEngine.SceneManagement;


public class sceneChange : MonoBehaviour
{

    public void ChangeScene(string sceneName) {
        SceneManager.LoadScene(sceneName);
    }

}