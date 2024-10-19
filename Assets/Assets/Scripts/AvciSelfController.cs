using System.Collections;
using UnityEngine;

public class AvciSelfController : MonoBehaviour
{
    public float delay = Constants.hunter_delay; // Time before destruction
    private Coroutine destructionCoroutine;
    private float damage = Constants.hunter_damage;  // Damage amount that the hunter will inflict
    private float health = Constants.hunter_health;  // Default health value
    void Start()
    {
        StartDestructionTimer();
        
    }

    public void StartDestructionTimer()
    {
        if (destructionCoroutine != null)
            StopCoroutine(destructionCoroutine);

        destructionCoroutine = StartCoroutine(DestructionTimer());
    }

    private IEnumerator DestructionTimer()
    {
        while (true)
        {
            yield return new WaitForSeconds(delay);
            health = health - damage;
            if (health <= 0)
            {
                Destroy(gameObject); // Call the Die method if health drops to zero or below
                StopCoroutine(DestructionTimer());
            }
        }


    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        AvSelfController preyHealth = collision.collider.GetComponent<AvSelfController>();
        if (preyHealth != null)
        {
            preyHealth.ChangeHealth(-damage);
        }
    }

    public void ChangeHealth(float amount)
    {
        health += amount;
        if (health <= 0)
        {
            Destroy(gameObject); // Call the Die method if health drops to zero or below
        }
    }
}
