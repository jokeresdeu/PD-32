using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D info)
    {
        Debug.Log(info.name);
        Destroy(gameObject);
    }
}
