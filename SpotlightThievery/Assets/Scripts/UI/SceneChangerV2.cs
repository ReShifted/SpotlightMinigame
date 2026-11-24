using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangerV2 : MonoBehaviour
{
    public void SwitchScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}

