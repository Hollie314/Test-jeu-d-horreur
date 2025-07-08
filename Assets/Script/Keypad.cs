using UnityEngine;
using UnityEngine.UI;

public class Keypad : MonoBehaviour
{
    public GameObject player;
    public GameObject keypadUI;
    public GameObject hud;
    public GameObject inventory;

    public Animator animator;
    public Text displayText;
    public string correctCode = "12345";

    public AudioSource buttonSound;
    public AudioSource correctSound;
    public AudioSource wrongSound;

    public bool shouldAnimate;

    void Start()
    {
        keypadUI.SetActive(false);
    }

    public void Number(int number)
    {
        displayText.text += number.ToString();
        buttonSound.Play();
    }

    public void Execute()
    {
        if (displayText.text == correctCode)
        {
            correctSound.Play();
            displayText.text = "Right";
        }
        else
        {
            wrongSound.Play();
            displayText.text = "Wrong";
        }
    }

    public void Clear()
    {
        displayText.text = "";
        buttonSound.Play();
    }

    public void Exit()
    {
        keypadUI.SetActive(false);
        inventory.SetActive(true);
        hud.SetActive(true);

        // player.GetComponent<FirstPersonController>().enabled = false;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // Si le code est correct et qu'on veut animer l'objet
        if (displayText.text == "Right" && shouldAnimate)
        {
            animator.SetBool("animate", true);
            Debug.Log("It's open");
        }

        // Si le clavier est ouvert, désactiver l’HUD et désactiver le contrôle joueur
        if (keypadUI.activeInHierarchy)
        {
            hud.SetActive(false);
            inventory.SetActive(false);

            //player.GetComponent<FirstPersonController>().enabled = false;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }
}