using UnityEngine;
using System.Collections.Generic;

public class AvciController : MonoBehaviour
{

    public GameObject hunterPrefab; // Reference to the hunter prefab
    public List<GameObject> hunters = new List<GameObject>(); // List to hold all hunters

    private List<GameObject> newHunters = new List<GameObject>();
}
