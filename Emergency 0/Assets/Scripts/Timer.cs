using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Timer : MonoBehaviour
{
    //* Varijable za Unity
    [SerializeField] GameObject deathMenu;
    [SerializeField] GameObject activeGameUI;
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] private Slider oxygenSlider;
    [SerializeField] float oxygenTimer = 360f;
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
        if (oxygenTimer > 0)
        {
            oxygenTimer -= Time.deltaTime;
            
            //* Set the oxygen slider value
            oxygenSlider.value = oxygenTimer;
        }
        else if (oxygenTimer <= 0)
        {
            oxygenTimer = 0.01f;

            //* Freeze time in a scene
            Time.timeScale = 0;

            //* Show the Death Menu
            deathMenu.SetActive(true);

            //* Hide the Active Game UI elements
            activeGameUI.SetActive(false);

            //* LOG
            Debug.Log("Oxygen has reached 0.");
        }
    }
}
