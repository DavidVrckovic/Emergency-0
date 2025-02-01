using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    //* Varijable za Unity
    [SerializeField] GameObject pauseMenu;
    [SerializeField] GameObject optionsMenu;

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
        if (optionsMenu.activeSelf == false)
        {
            //* Check for input & check if the game is already paused
            if (Input.GetKeyDown(KeyCode.Escape) && pauseMenu.activeSelf == false)
            {
                //* Freeze time in a scene
                Time.timeScale = 0;

                //* Show the Pause Menu
                pauseMenu.SetActive(true);

                //* LOG
                Debug.Log("Game paused via [Escape] key.");
            }
            else if (Input.GetKeyDown(KeyCode.Escape) && pauseMenu.activeSelf == true)
            {
                //* Unfreeze time in a scene
                Time.timeScale = 1;

                //* Hide the Pause Menu
                pauseMenu.SetActive(false);

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
                pauseMenu.SetActive(true);
                optionsMenu.SetActive(false);
            }
        }
    }

    public void ContinueGame()
    {
        //* Check if the game is already paused
        if (pauseMenu.activeSelf == true)
        {
            //* Unfreeze time in a scene
            Time.timeScale = 1;

            //* Hide the Pause Menu
            pauseMenu.SetActive(false);

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
