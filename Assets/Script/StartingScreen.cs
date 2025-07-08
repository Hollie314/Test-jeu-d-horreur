using System.Collections;
using UnityEngine;

public class StartingScreen : MonoBehaviour
{
    [Header("Références")]
    public GameObject startingScreen;
    private GameObject player;
    private CharacterController controller;

    [Header("Réglages")]
    public float waitTime = 3f;

    void Start()
    {
        // Activer l'écran de démarrage
        startingScreen.SetActive(true);

        // Trouver le joueur par tag et désactiver le mouvement
        player = GameObject.FindWithTag("Player");

        if (player != null)
        {
            controller = player.GetComponent<CharacterController>();
            if (controller != null)
            {
                controller.enabled = false;
            }
            else
            {
                Debug.LogWarning("CharacterController non trouvé sur le joueur !");
            }
        }
        else
        {
            Debug.LogError("Aucun objet avec le tag 'Player' trouvé !");
        }

        StartCoroutine(Starting());
    }

    IEnumerator Starting()
    {
        // Attendre quelques secondes
        yield return new WaitForSeconds(waitTime);

        // Masquer l'écran et réactiver le joueur
        startingScreen.SetActive(false);

        if (controller != null)
        {
            controller.enabled = true;
        }
    }
}