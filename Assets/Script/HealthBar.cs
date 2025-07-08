using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private Image healthBarIM;
    public float CurrentHeath;
    private float MaxHealth = 100f;
    PlayerHealth player;

    void Start()
    {
        healthBarIM = GetComponent<Image>();
        player = FindObjectOfType<PlayerHealth>();

        if (player == null)
        {
            Debug.LogError("PlayerHealth script not found in scene!");
        }
    }

    void Update()
    {
        if (player != null)
        {
            CurrentHeath = player.health;
            healthBarIM.fillAmount = CurrentHeath / MaxHealth;
        }
    }
}