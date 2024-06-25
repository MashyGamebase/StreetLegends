using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
{
    Key,
    Special,
    Ammo
}

public abstract class ItemCollectible : MonoBehaviour
{
    public ItemType type;

    public virtual void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GetItem();
        }
    }

    private void GetItem()
    {
        switch (type)
        {
            case ItemType.Key:
                GameHandler.instance.UpdateObjectives(1);
                break;
            case ItemType.Special:
                // If there will be special Items
                break;
            case ItemType.Ammo:
                // For extra ammo
                break;
        }

        Destroy(gameObject);
    }
}