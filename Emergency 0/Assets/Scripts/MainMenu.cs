using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    //* Varijable za Unity
    [SerializeField] GameObject mainMenu;
    [SerializeField] GameObject optionsMenu;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayGame()
    {
        //* Load the Game scene
        SceneManager.LoadSceneAsync("Game");

        //* LOG
        Debug.Log("<color=#00ff00ff>Game started.</color>");
    }

    public void QuitGame()
    {
        //* Close the application
        Application.Quit();
    }
}
