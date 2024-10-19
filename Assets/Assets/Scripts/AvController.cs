using UnityEngine;
using System.Collections.Generic;

public class AvController : MonoBehaviour
{
    public GameObject preyPrefab;
    private float survivalTimeInterval = Constants.prey_survival_time_interval; // Duplicate every 30 seconds

    private float chanceToDuplicate = Constants.prey_chance_to_duplicate; // 30% chance

    private float survivalTimer;
    public List<GameObject> activePrey = new List<GameObject>();

    private List<GameObject> newPreys = new List<GameObject>();

    void Start()
    {
        survivalTimer = survivalTimeInterval;
    }

    void Update()
    {
        survivalTimer -= Time.deltaTime;

        if (survivalTimer <= 0)
        {
            DuplicateSurvivingPrey();
            survivalTimer = survivalTimeInterval;
        }
    }

    void DuplicateSurvivingPrey()
    {
        foreach (GameObject prey in activePrey)
        {
            if (prey != null) // Check if still alive
            {
                if (Random.Range(0.0f, 1.0f) <= chanceToDuplicate)
                {
                    GameObject duplicate = Instantiate(preyPrefab, prey.transform.position, prey.transform.rotation);
                    newPreys.Add(duplicate);
                }
            }
        }
        activePrey.AddRange(newPreys);
        newPreys = new List<GameObject>();
    }
    // Method to remove a prey from the list
    public void RemovePrey(GameObject prey)
    {
        if (activePrey.Contains(prey))
        {
            activePrey.Remove(prey);
        }
    }
}