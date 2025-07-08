using StarterAssets;
using UnityEngine;

public class Ladder : MonoBehaviour
{
    public GameObject player;                // Le joueur
    public AudioSource sound;                // Le son d'escalade
    public float speed = 3f;                 // Vitesse d'escalade
    private bool inside = false;             // Si le joueur est dans l'échelle

    private FirstPersonController movementScript;   // Référence au script de mouvement du joueur

    void Start()
    {
        // On récupère le script de mouvement une seule fois
        movementScript = player.GetComponent<FirstPersonController>();
        inside = false;
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Ladder"))
        {
            Debug.Log("TouchingLadderTrue");
            if (movementScript != null)
                movementScript.enabled = false;
            inside = true;
        }
    }

    void OnTriggerExit(Collider col)
    {
        if (col.CompareTag("Ladder"))
        {
            Debug.Log("TouchingLadderFalse");
            if (movementScript != null)
                movementScript.enabled = true;
            inside = false;
        }
    }

    void Update()
    {
        if (!inside) return;

        if (Input.GetKey("w"))
        {
            player.transform.position += Vector3.up * speed * Time.deltaTime;

            sound.enabled = true;
            sound.loop = true;
        }
        else if (Input.GetKey("s"))
        {
            player.transform.position += Vector3.down * speed * Time.deltaTime;

            sound.enabled = true;
            sound.loop = true;
        }
        else
        {
            sound.enabled = false;
            sound.loop = false;
        }
    }
}