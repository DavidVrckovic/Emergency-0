using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InteractionController : MonoBehaviour
{
    //* Main variables for references
    GameObject canvas;
    MainMenu Menu;
    Timer Timer;
    
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

            Timer = canvas.GetComponent<Timer>();
            if (Timer == null)
            {
                Debug.Log("<color=#ff0000ff>Could not find the \"Timer\" component on the \"" + canvas.name + "\" GameObject.</color>");
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
    public GameObject oxygenPickupPrompt;
    public float oxygenPickupAmount = 60f;
    private bool isInOxygenRange = false;

    public GameObject generatorInteractPrompt;
    private bool generatorEnabled = false;
    private bool isInGeneratorRange = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        oxygenPickupPrompt.SetActive(false);
        generatorInteractPrompt.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        Interact();
    }



    private void OnTriggerEnter(Collider collision)
    {
        //* Check if the collision is with the right object
        //* NOTE: gameObject.tag returns the tag of an object that is collided with
        //* NOTE: collision.gameObject.tag returns the tag of a colliding object
        if (gameObject.CompareTag("Oxygen"))
        {
            isInOxygenRange = true;

            oxygenPickupPrompt.SetActive(true);

            //* LOG
            Debug.Log("Entered collision area with " + gameObject.name);
        }

        if (gameObject.CompareTag("Generator"))
        {
            if (!generatorEnabled)
            {
                isInGeneratorRange = true;

                generatorInteractPrompt.SetActive(true);
            }

            //* LOG
            Debug.Log("Entered collision area with " + gameObject.name);
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        //* Check if the collision is with the right object
        //* NOTE: gameObject.tag returns the tag of an object that is collided with
        //* NOTE: collision.gameObject.tag returns the tag of a colliding object
        if (gameObject.CompareTag("Oxygen"))
        {
            isInOxygenRange = false;

            oxygenPickupPrompt.SetActive(false);

            //* LOG
            Debug.Log("Exited collision area with " + gameObject.name);
        }

        if (gameObject.CompareTag("Generator"))
        {
            isInGeneratorRange = false;

            generatorInteractPrompt.SetActive(false);

            //* LOG
            Debug.Log("Exited collision area with " + gameObject.name);
        }
    }



    private void Interact()
    {
        if (isInOxygenRange)
        {
            //* Check if Options Menu is active
            if (!Menu.optionsMenu.activeSelf && !Menu.deathMenu.activeSelf)
            {
                //* Check for input & check if the game is already paused
                if (Input.GetKeyDown(KeyCode.E))
                {
                    //* Remove the Oxygen tank object
                    gameObject.transform.parent.gameObject.SetActive(false);

                    //* Increase the remaining oxygen
                    Timer.oxygenRemaining += oxygenPickupAmount;
                    
                    oxygenPickupPrompt.SetActive(false);

                    //* LOG
                    Debug.Log("Oxygen tank picked up.");
                }
            }
        }

        if (isInGeneratorRange && !generatorEnabled)
        {
            //* Check if Options Menu is active
            if (!Menu.optionsMenu.activeSelf && !Menu.deathMenu.activeSelf)
            {
                //* Check for input & check if the game is already paused
                if (Input.GetKeyDown(KeyCode.E))
                {
                    generatorEnabled = true;
                    generatorInteractPrompt.SetActive(false);

                    //* LOG
                    Debug.Log("Generator enabled.");
                }
            }
        }
    }
}
