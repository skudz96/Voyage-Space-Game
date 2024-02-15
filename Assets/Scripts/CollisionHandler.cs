using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    void OnCollisionEnter(Collision other)
    {
        switch (other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("You bumped into a friendly!");
                break;
            case "Enemy":
                Debug.Log("You bumped into an enemy!");
                break;
            case "Fuel":
                Debug.Log("You've found some fuel!");
                break;
            default:
                break;
        }
    }
}
