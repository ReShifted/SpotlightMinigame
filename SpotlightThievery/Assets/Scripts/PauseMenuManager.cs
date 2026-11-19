using UnityEngine;
using UnityEngine.InputSystem;

public class PauseMenuManager : MonoBehaviour
{
    public GameObject container;
    public PlayerInput playerInput;

    private InputAction pauseAction;

    private void Awake()
    {
        pauseAction = playerInput.actions.FindAction("Player/PauseGame", true);
    }

    private void Start()
    {
        container.SetActive(false);
    }

    private void Update()
    {
        if (pauseAction.WasPressedThisFrame())
        {
            Debug.Log("Pause pressed!");

            if (Time.timeScale == 0f)
                Resume();
            else
                Pause();
        }
    }

    public void Pause()
    {
        container.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Resume()
    {
        container.SetActive(false);
        Time.timeScale = 1f;
    }
}