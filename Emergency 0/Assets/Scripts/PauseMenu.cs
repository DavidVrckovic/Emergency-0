using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    //* Main variables for references
    GameObject canvas;
    MainMenu Menu;
    
    void Awake()
    {
        //* Set up the references
        canvas = GameObject.Find("Canvas");

        if (canvas != null)
        {
            Menu = canvas.GetComponent<MainMenu>();
            if (Menu == null)
            {
                Debug.Log("<color=#ff0000ff>Could not find the \"MainMenu\" component on the \"" + canvas.name + "\" GameObject.</color>");
                Debug.Break();
            }
        }
        else
        {
            Debug.Log("<color=#ff0000ff>Could not find the \"Canvas\" GameObject.</color>");
            Debug.Break();
        }
    }



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        PauseGame();
    }



    public void PauseGame()
    {
        //* Check if Options Menu is active
        if (!Menu.optionsMenu.activeSelf && !Menu.deathMenu.activeSelf)
        {
            //* Check for input & check if the game is already paused
            if (Input.GetKeyDown(KeyCode.Escape) && Menu.pauseMenu.activeSelf == false)
            {
                //* Freeze time in a scene
                Time.timeScale = 0;

                //* Show the Pause Menu
                Menu.pauseMenu.SetActive(true);

                //* Hide the Active Game UI elements
                Menu.activeGameUI.SetActive(false);

                //* LOG
                Debug.Log("Game paused via [Escape] key.");
            }
            else if (Input.GetKeyDown(KeyCode.Escape) && Menu.pauseMenu.activeSelf == true)
            {
                //* Unfreeze time in a scene
                Time.timeScale = 1;

                //* Hide the Pause Menu
                Menu.pauseMenu.SetActive(false);

                //* Show the Active Game UI elements
                Menu.activeGameUI.SetActive(true);

                //* LOG
                Debug.Log("Game unpaused via [Escape] key.");
            }
        }
        else
        {
            //* Check for input
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                //* Show the Pause Menu and hide the Options Menu
                Menu.pauseMenu.SetActive(true);
                Menu.optionsMenu.SetActive(false);
            }
        }
    }

    public void ContinueGame()
    {
        //* Check if the game is already paused
        if (Menu.pauseMenu.activeSelf == true)
        {
            //* Unfreeze time in a scene
            Time.timeScale = 1;

            //* Hide the Pause Menu
            Menu.pauseMenu.SetActive(false);

            //* Show the Active Game UI elements
            Menu.activeGameUI.SetActive(true);

            //* LOG
            Debug.Log("Game unpaused via Continue button.");
        }
    }

    public void RestartGame()
    {
        //* Restart the current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        //* Unfreeze time in a scene
        Time.timeScale = 1;

        //* LOG
        Debug.Log("<color=#00ff00ff>Game restarted.</color>");
    }

    public void BackToMainMenu()
    {
        //* Load the Main Menu scene
        SceneManager.LoadScene("Main Menu");

        //* Unfreeze time in a scene
        Time.timeScale = 1;

        //* LOG
        Debug.Log("<color=#00ff00ff>Game stopped. Exited to Main Menu.</color>");
    }
}
