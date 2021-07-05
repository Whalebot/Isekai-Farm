using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemScript : Interactable
{
    public Item item;
    public int quantity;
    public int quality = 10;
    public ItemSO SO;
    public bool picked;
    public bool colliding;
    public GameObject pickupFX;
   protected Collider lastCol;
    protected DataManager dataManager;
    protected ItemData itemData;

    // Start is called before the first frame update
    public virtual void Awake()
    {
        dataManager = GameObject.FindGameObjectWithTag("Manager").GetComponent<DataManager>();
        //       SendItemData();
        dataManager.saveItemEvent += SaveItemData;
        dataManager.loadItemEvent += UnloadObject;

        item = new Item(SO, quantity, quality);
    }

   protected void SaveItemData()
    {


        itemData = new ItemData();
        itemData.sceneID = dataManager.sceneIndex;
        itemData.itemID = SO.ID;
        itemData.position = transform.position;
        itemData.rotation = transform.rotation;
        SendItemData();

    }

    void SendItemData()
    {
        //if(DataManager.isHouse) dataManager.currentSaveData.houseData.Add(itemData);
        //else
        dataManager.currentSaveData.itemData.Add(itemData);
    }

  
   public virtual void UnloadObject()
    {
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        dataManager.saveItemEvent -= SaveItemData;
        dataManager.loadItemEvent -= UnloadObject;
    }

    public ItemSO PickedItem()
    {
        return SO;
    }

    public override void Interact()
    {
        base.Interact();
        Picked();
    }

    public void Picked()
    {
        picked = true;
        Instantiate(pickupFX, transform.position, transform.rotation);
        InventoryScript.Instance.PickupItem(item);
 
        Destroy(gameObject);
    }

    private void OnEnable()
    {
        StartCoroutine("PickupDelay");
    }

    IEnumerator PickupDelay()
    {
        yield return new WaitForSeconds(1);
        picked = false;
    }
}
