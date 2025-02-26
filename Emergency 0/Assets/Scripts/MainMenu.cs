using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    //* Main variables for references
    [Header("UI menus")]
    public GameObject mainMenu;
    public GameObject pauseMenu;
    public GameObject optionsMenu;
    public GameObject deathMenu;
    [Header("Game UI objects")]
    public GameObject activeGameUI;



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //* Check if Options Menu is active
        if (optionsMenu.activeSelf)
        {
            //* Check for input
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                //* Hide the Options Menu
                optionsMenu.SetActive(false);

                //* Show the Main Menu
                mainMenu.SetActive(true);
            }
        }
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
