using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropScript : MonoBehaviour
{
    public DropItem[] dropItems;
    Status status;

    void Awake()
    {
        status = GetComponent<Status>();
        status.deathEvent += SpawnDrop;
    }

    void SpawnDrop()
    {

        foreach (DropItem drop in dropItems)
        {
            int quantity = drop.guarenteed;

            for (int i = 0; i < drop.quantity; i++)
            {
                if (Random.Range(0, 100) < drop.chance)
                {
                    quantity++;
                    
                    if (drop.GO != null)
                    {
                        Instantiate(drop.GO, transform.position, Quaternion.identity);
                    }
                }
            }

            if (drop.so != null)
            {
                print(quantity);
                //if (drop.quantity <= 0) InventoryScript.Instance.PickupItem(drop.so, 1);
                //else
                if (quantity > 0)
                {
                    Item item = new Item(drop.so, quantity, drop.quality);
                    InventoryScript.Instance.PickupItem(item);
                }
            }
        }
    }

    private void OnDisable()
    {
        // status.deathEvent -= SpawnDrop;
    }
}
