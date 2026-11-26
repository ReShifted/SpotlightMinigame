using UnityEngine;
using UnityEngine.SceneManagement;
public class Scenechanger : MonoBehaviour
{
    [SerializeField] private string scene;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void changeScene()
    {
        SceneManager.LoadScene(scene);
    }
    public void OnApplicationQuit()
    {
        Application.Quit();
    }   
}

