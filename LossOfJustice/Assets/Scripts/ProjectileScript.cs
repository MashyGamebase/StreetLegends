using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    private void Start()
    {
        Invoke(nameof(DestroyObject), 2f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameHandler.instance.UpdateHealth(-1);
        }

        DestroyObject();
    }

    void DestroyObject()
    {
        Destroy(gameObject);
    }
}