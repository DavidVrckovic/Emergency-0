using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Timer : MonoBehaviour
{
    //* Main variables for references
    GameObject canvas;
    MainMenu Menu;
    
    void Awake()
    {
        // Set up the references
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



    //* Variables
    [SerializeField] GameObject oxygen30percent;
    [SerializeField] GameObject oxygen10percent;
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] private Slider oxygenSlider;
    public float oxygenRemaining = 360f;
    float elapsedTime;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;
        int elapsedMinutes = Mathf.FloorToInt(elapsedTime / 60);
        int elapsedSeconds = Mathf.FloorToInt(elapsedTime % 60);
        timerText.text = string.Format("{0:00}:{1:00}", elapsedMinutes, elapsedSeconds);

        SetOxygenLevel();
    }



    public void SetOxygenLevel()
    {
        //* Check if the oxygen timer has reached 0
        if (oxygenRemaining > 0)
        {
            oxygenRemaining -= Time.deltaTime;
            
            //* Set the oxygen slider value
            oxygenSlider.value = oxygenRemaining;

            if (!oxygen10percent.activeSelf && oxygenRemaining < 36)
            {
                oxygen30percent.SetActive(false);
                oxygen10percent.SetActive(true);

                //* LOG
                Debug.Log("Oxygen has reached 10%.");
            }
            else if (oxygen10percent.activeSelf && oxygenRemaining > 36)
            {
                oxygen10percent.SetActive(false);
            }
            else if (!oxygen30percent.activeSelf && oxygenRemaining < 108)
            {
                oxygen30percent.SetActive(true);

                //* LOG
                Debug.Log("Oxygen has reached 30%.");
            }
            else if (oxygen30percent.activeSelf && oxygenRemaining > 108)
            {
                oxygen30percent.SetActive(false);
            }
        }
        else if (oxygenRemaining <= 0)
        {
            oxygenRemaining = 0.01f;

            //* Freeze time in a scene
            Time.timeScale = 0;

            //* Show the Death Menu
            Menu.deathMenu.SetActive(true);

            //* Hide the Active Game UI elements
            Menu.activeGameUI.SetActive(false);

            //* LOG
            Debug.Log("Oxygen has reached 0.");
        }
    }
}
