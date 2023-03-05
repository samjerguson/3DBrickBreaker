using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BolStuff : MonoBehaviour
{
    private Collider[] colliders;
    // Start is called before the first frame update
    void Start()
    {
        colliders = FindObjectsOfType<Collider>(); // Get all Colliders in the scene
        foreach (Collider collider in colliders)
        {
            if (collider != GetComponent<Collider>()) // Check if the collider is not the object's own Collider
            {
                Physics.IgnoreCollision(GetComponent<Collider>(), collider); // Ignore collisions between the object's Collider and the other Collider
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        print("yo");
        foreach (Collider collider in colliders)
        {
            if (collider != GetComponent<Collider>()) // Check if the collider is not the object's own Collider
            {
                Physics.IgnoreCollision(GetComponent<Collider>(), collider, false); // Reallow collisions
            }
        }  
    }
}
