using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Sirenix.OdinInspector;
public class DevCheats : MonoBehaviour
{
    public ItemSO[] so;
    public int quantity;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    [Button("Give Item")]
    public void GiveItem() {
        foreach (ItemSO itemSO in so)
        {
            Item item = new Item(itemSO, quantity, 100);
            InventoryScript.Instance.PickupItem(item);
        }
      
    }

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current.f1Key.wasPressedThisFrame) GiveItem();
        if (Keyboard.current.digit5Key.wasPressedThisFrame) TimeManager.Instance.clockTime.x = 22;
    }
}
