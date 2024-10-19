using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSpawner : MonoBehaviour
{
    public GameObject avci; // Assign your character prefab here
    public GameObject av; // Assign your character prefab here
    private int numberOfCharacters = Constants.spawn_count;
    private int spawnXRadius = Constants.spawnXRadius;
    private int spawnYRadius = Constants.spawnYRadius;

    public GameObject avController; // Assign your spawner prefab here
    public GameObject avciController; // Assign your hunter manager prefab here

    private List<GameObject> spawnedAvci = new List<GameObject>();
    private List<GameObject> spawnedAv = new List<GameObject>();
    private float resetInterval = Constants.reset_interval; // Time interval in seconds for resetting positions

    void Start()
    {
        for (int i = 0; i < numberOfCharacters; i++)
        {
            
            Vector2 randomPosition = new Vector2(Random.Range(-spawnXRadius, spawnXRadius), Random.Range(-spawnYRadius, spawnYRadius)); // Adjust random area as needed
            GameObject newAvci = Instantiate(avci, randomPosition, Quaternion.identity);
            GameObject newAv = Instantiate(av, randomPosition, Quaternion.identity);
            avController.GetComponent<AvController>().activePrey.Add(newAv);
            avciController.GetComponent<AvciController>().hunters.Add(newAvci);
            spawnedAvci.Add(newAvci);
            spawnedAv.Add(newAv);
        }
        // Reset positions
        for (int i = 0; i < spawnedAvci.Count; i++)
        {
            Vector2 randomPosition = new Vector2(Random.Range(-spawnXRadius, spawnXRadius), Random.Range(-spawnYRadius, spawnYRadius));
            spawnedAvci[i].transform.position = randomPosition;
        }

        for (int i = 0; i < spawnedAv.Count; i++)
        {
            Vector2 randomPosition = new Vector2(Random.Range(-spawnXRadius, spawnXRadius), Random.Range(-spawnYRadius, spawnYRadius));
            spawnedAv[i].transform.position = randomPosition;
        }
        StartCoroutine(ResetPositions());
        spawnedAvci.Add(avci);
        spawnedAv.Add(av);
    }

    IEnumerator ResetPositions()
    {
        while (true)
        {
            yield return new WaitForSeconds(resetInterval);

            // Reset positions
            for (int i = 0; i < spawnedAvci.Count; i++)
            {
                Vector2 randomPosition = new Vector2(Random.Range(-spawnXRadius, spawnXRadius), Random.Range(-spawnYRadius, spawnYRadius));
                spawnedAvci[i].transform.position = randomPosition;
            }

            for (int i = 0; i < spawnedAv.Count; i++)
            {
                Vector2 randomPosition = new Vector2(Random.Range(-spawnXRadius, spawnXRadius), Random.Range(-spawnYRadius, spawnYRadius));
                spawnedAv[i].transform.position = randomPosition;
            }
        }
    }
}
