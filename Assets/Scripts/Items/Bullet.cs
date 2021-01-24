using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other) {
        Debug.Log($"Bullet collided with {other.gameObject.name}");

        Destroy(gameObject);
    }
}
