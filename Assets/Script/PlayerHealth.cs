using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public GameObject hud;
    public GameObject inv;
    public GameObject deathScreen;
    public GameObject player;

    public float health = 100f;

    // Name of your player controller script class (set this in Inspector)
    public string FirstPersonController = "FirstPersonController";

    void Start()
    {
        deathScreen.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        if (health <= 0)
        {
            // Disable the player controller script dynamically
            var controller = player.GetComponent(FirstPersonController) as MonoBehaviour;
            if (controller != null)
                controller.enabled = false;

            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;

            hud.SetActive(false);
            inv.SetActive(false);
            deathScreen.SetActive(true);
        }

        if (health > 100)
        {
            health = 100;
        }
    }
}