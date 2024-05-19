using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    private void OnCollisionEnter(Collision other) {
        Debug.Log($"Player Ship bumped into {other.gameObject.name}");
    }

    private void OnTriggerEnter(Collider other) {
        Debug.Log($"Player Ship triggered {other.gameObject.name}");
    }
}
