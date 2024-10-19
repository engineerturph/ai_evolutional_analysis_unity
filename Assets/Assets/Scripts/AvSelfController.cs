using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class AvSelfController : MonoBehaviour
{
    private float health = Constants.prey_health;  // Default health value

    private float healing_to_hunter = Constants.prey_healing_to_hunter;  // Healing amount that the prey will inflict

    private AvController avController; // Reference to the AvController
    void Start()
    {
        // Find the AvController component in the scene
        avController = FindObjectOfType<AvController>();
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        AvciSelfController preyHealth = collision.collider.GetComponent<AvciSelfController>();
        if (preyHealth != null)
        {
            preyHealth.ChangeHealth(healing_to_hunter);
        }
    }

    public void ChangeHealth(float amount)
    {
        health += amount;
        if (health <= 0)
        {
            DestroyPrey(); // Remove from activePrey and destroy the object
        }
    }

    private void DestroyPrey()
    {
        if (avController != null)
        {
            avController.RemovePrey(gameObject); // Remove this object from activePrey
        }
        Destroy(gameObject); // Destroy the game object
    }
}
