using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pusher : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Rigidbody2D target = collision.collider.GetComponent<Rigidbody2D>();
        if(target != null)
        {
            Debug.LogError("here");
            target.AddForce(Vector2.up * 100);
        }
    }
}
