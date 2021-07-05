using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Sirenix.OdinInspector;
using UnityEngine.UI;
public class InventoryScript : MonoBehaviour
{
    public static InventoryScript Instance { get; private set; }


    public enum MenuTab { Inventory, Status, Calendar, Settings }
    [EnumToggleButtons] public MenuTab tab;
    [FoldoutGroup("Components")] public GameObject statusDefault;
    [FoldoutGroup("Components")] public GameObject calendarDefault;
    [FoldoutGroup("Components")] public GameObject settingsDefault;
    public bool inventoryOpen;
    public int usedCapacity;
    public int inventoryCapacity;
    [FoldoutGroup("Components")] public GameObject inventoryMenu;
    [FoldoutGroup("Components")] public GameObject inventoryTab;
    [FoldoutGroup("Components")] public GameObject statusTab;
    [FoldoutGroup("Components")] public GameObject calendarTab;
    [FoldoutGroup("Components")] public GameObject settingsTab;
    public TextMeshProUGUI titleTabText;
    public TextMeshProUGUI leftTabText;
    public TextMeshProUGUI rightTabText;

    [FoldoutGroup("Components")] public Transform heldItemTransform;
    [FoldoutGroup("Components")] public InventorySlot mainHand, offHand, activeItem;

    [FoldoutGroup("Components")] public InteractScript interactScript;
    [FoldoutGroup("Components")] public Transform interactBox;
    [FoldoutGroup("Components")] public List<ItemSO> heldItemSO;
    [FoldoutGroup("Components")] public List<int> quantities;
    [FoldoutGroup("Components")] public List<InventorySlot> inventorySlots;
    [FoldoutGroup("Components")] public Status status;
    [FoldoutGroup("Components")] public EquipmentScript equipmentScript;
    [FoldoutGroup("Components")] List<InventoryData> inventoryData;
    DataManager dataManager;
    ItemDatabase itemDatabase;
    [FoldoutGroup("Components")] public DescriptionWindow window;

    [FoldoutGroup("Components")] public TextMeshProUGUI indicatorText;
    [FoldoutGroup("Components")] public GameObject useItemIndicator;
    [FoldoutGroup("Components")] public GameObject furniturePrompt;
    [FoldoutGroup("Components")] public SkillHandler skillHandler;
    [FoldoutGroup("Components")] public SkillExp skillExp;
    [FoldoutGroup("FX")] public GameObject eatSFX;
    [FoldoutGroup("FX")] public GameObject openInventorySFX;
    [FoldoutGroup("FX")] public GameObject closeInventorySFX;

    [FoldoutGroup("UI")] public Image equipImage;
    [FoldoutGroup("UI")] public Image activeImage;
    [FoldoutGroup("UI")] public TextMeshProUGUI activeQuantity;
    [FoldoutGroup("UI")] public StatUpdateUI itemStatUI;

    [FoldoutGroup("Quickslot")] public GameObject quickslotTab;
    [FoldoutGroup("Quickslot")] public InventorySlot highlightedSlot;
    [FoldoutGroup("Quickslot")] public InventorySlot[] quickslots;


    public List<Item> pepeList = new List<Item>();
    private void Awake()
    {
        Instance = this;
        dataManager = GameObject.FindGameObjectWithTag("Manager").GetComponent<DataManager>();
        itemDatabase = dataManager.gameObject.GetComponent<ItemDatabase>();
        inventoryData = new List<InventoryData>();



        dataManager.saveDataEvent += SaveInventoryData;
        dataManager.loadDataEvent += LoadInventoryData;

        TimeManager.Instance.dayPassEvent += AgeInventory;
    }

    // Start is called before the first frame update
    void Start()
    {
        InputManager.Instance.R1input += SwitchTabRight;
        InputManager.Instance.L1input += SwitchTabLeft;
        InputManager.Instance.startInput += ToggleMenu;
        //     InputManager.Instance.northInput += RemoveActiveButton;
        InputManager.Instance.eastInput += CloseMenu;
        InputManager.Instance.northInput += CloseMenu;
        InputManager.Instance.L2input += OpenQuickslotTab;
        InputManager.Instance.L2release += CloseQuickslotTab;
    }

    public void UpdateMenuTab()
    {
        if (!inventoryOpen) return;
        inventoryTab.SetActive(false);
        statusTab.SetActive(false);
        calendarTab.SetActive(false);
        settingsTab.SetActive(false);


        titleTabText.text = "" + tab;

        if (tab == MenuTab.Inventory)
        {
            inventoryTab.SetActive(true);
            UIManager.Instance.SetActive(inventorySlots[0].gameObject);
            leftTabText.text = "" + (MenuTab.Settings);
            rightTabText.text = "" + (tab + 1);
        }
        if (tab == MenuTab.Status)
        {

            statusTab.SetActive(true);
            UIManager.Instance.SetActive(statusDefault);
            leftTabText.text = "" + (tab - 1);
            rightTabText.text = "" + (tab + 1);
        }
        if (tab == MenuTab.Calendar)
        {

            calendarTab.SetActive(true);
            //UIManager.Instance.SetActive(calendarDefault);
            leftTabText.text = "" + (tab - 1);
            rightTabText.text = "" + (tab + 1);
        }
        if (tab == MenuTab.Settings)
        {
            settingsTab.SetActive(true);
            UIManager.Instance.SetActive(settingsDefault);
            leftTabText.text = "" + (tab - 1);
            rightTabText.text = "" + MenuTab.Inventory;
        }
    }


    public void SetTab(int i)
    {
        if (!inventoryOpen) return;
        tab = (MenuTab)i;
        UpdateMenuTab();
    }

    public void SwitchTabRight()
    {
        if (!inventoryOpen) return;
        tab = tab + 1;
        if ((int)tab > 3) tab = 0;
        UpdateMenuTab();
    }

    public void SwitchTabLeft()
    {
        if (!inventoryOpen) return;
        tab = tab - 1;
        if ((int)tab < 0) tab = (MenuTab)3;
        UpdateMenuTab();
    }

    public bool InventoryFull()
    {
        if (heldItemSO.Count >= inventoryCapacity)
        {
            print("Inventory full");
            UIManager.Instance.InventoryFull();
            return false;
        }
        else return true;
    }

    public bool PickupItem(Item other)
    {
        ItemSO SO = other.SO;
        int pickupQuantity = other.quantity;

        if (!heldItemSO.Contains(SO))
        {

            if (heldItemSO.IndexOf(null) != -1)
            {
                int tempID = heldItemSO.IndexOf(null);
                heldItemSO[tempID] = SO;
                quantities[tempID] = 0;
            }
            else
            {
                if (heldItemSO.Count >= inventoryCapacity)
                {
                    print("Inventory full");
                    UIManager.Instance.InventoryFull();
                    return false;
                }
                heldItemSO.Add(SO);
                quantities.Add(0);
            }
        }

        int i = heldItemSO.IndexOf(SO);


        if (pickupQuantity == 0) pickupQuantity = 1;
        for (int z = 0; z < pickupQuantity; z++)
        {
            if (SO.quantityLimit <= quantities[i])
            {
                int y = 1;
                bool solution = false;
                while (!solution)
                {
                    if (heldItemSO.IndexOf(SO, y) == -1)
                    {
                        if (heldItemSO.IndexOf(null) != -1)
                        {
                            i = heldItemSO.IndexOf(null);
                            heldItemSO[i] = SO;
                            quantities[i] = 0;
                        }
                        else
                        {
                            heldItemSO.Add(SO);
                            quantities.Add(0);
                            i = heldItemSO.LastIndexOf(SO);
                        }
                        solution = true;
                    }
                    else if (SO.quantityLimit <= quantities[heldItemSO.IndexOf(SO, y)])
                    {
                        y++;
                        if (y >= inventorySlots.Count) { print("Fail"); UIManager.Instance.InventoryFull(); return false; }
                    }
                    else
                    {
                        i = heldItemSO.IndexOf(SO, y);
                        solution = true;
                    }
                }
            }
            int averageQuality = (quantities[i] * inventorySlots[i].quality + other.quality) / (quantities[i] + 1);
            quantities[i]++;
            if (!SO.found)
            {
                SO.found = true;
                skillHandler.SkillEXP(skillExp.skill, skillExp.experience);
                UIManager.Instance.FoundItem(other);
            }
            Item tempItem = new Item(SO, quantities[i], averageQuality);
            inventorySlots[i].UpdateSlot(tempItem);
        }
        //print("Adding to " + i);
        UIManager.Instance.DisplayPickedItem(other);

        return true;
    }

    public void DescriptionWindow(Item item)
    {

        if (item == null) window.gameObject.SetActive(false);
        else
        {
            window.gameObject.SetActive(true);
            window.DisplayUI(item);
        }
    }

    //public void DropItem(ItemSO item)
    //{
    //    Instantiate(item.gameObject, interactBox.transform.position, Quaternion.identity);

    //    int i = heldItemSO.IndexOf(item);
    //    quantities[i]--;
    //    if (quantities[i] <= 0) { heldItemSO.Remove(item); }

    //    inventorySlots[i].UpdateSlot(item, quantities[i]);
    //}

    public void DropItem()
    {
        int children = heldItemTransform.childCount;
        for (int i = 0; i < children; ++i)
            Destroy(heldItemTransform.GetChild(i).gameObject);

        Instantiate(activeItem.itemSO.gameObject, interactBox.transform.position, Quaternion.identity);

        activeItem.quantity--;
        if (activeItem.quantity <= 0) { }

        activeItem.UpdateSlot(activeItem.item);
    }


    void DeleteVisualItem()
    {
        int children = heldItemTransform.childCount;
        int children2 = interactScript.previewHolder.childCount;
        furniturePrompt.SetActive(false);
        for (int i = 0; i < children; ++i)
        {
            Destroy(heldItemTransform.GetChild(i).gameObject);
        }
        for (int i = 0; i < children2; ++i)
        {
            Destroy(interactScript.previewHolder.GetChild(i).gameObject);
        }
    }

    public void DeleteItem()
    {
        print(activeItem.item.quantity);
        activeItem.item.quantity--;

        print(activeItem.item.quantity);
        if (activeItem.item.quantity <= 0)
        {
            activeItem.item.SO = null;
            DeleteVisualItem();
        }


        activeItem.UpdateSlot(activeItem.item);
    }
    public void DeleteItem(InventorySlot slot)
    {
        slot.item.quantity--;
        quantities[inventorySlots.IndexOf(slot)]--;
        if (slot.item.quantity <= 0)
        {
            heldItemSO[inventorySlots.IndexOf(slot)] = null;
            slot.item.SO = null;
        }
        slot.UpdateSlot(slot.item);
    }

    public void DeleteSlot(InventorySlot slot)
    {
        heldItemSO[inventorySlots.IndexOf(slot)] = null;
        slot.UpdateSlot(null);
    }


    public void SetActiveItem(InventorySlot thisSlot)
    {
        int j = inventorySlots.IndexOf(thisSlot);
        //print(thisSlot + " " + j);

        ItemSO tempSO = activeItem.itemSO;
        int tempQuantity = activeItem.quantity;
        int tempQuality = activeItem.quality;

        DeleteVisualItem();

        activeItem.UpdateSlot(thisSlot.item);

        if (thisSlot.itemSO.type == ItemType.Furniture)
        {
            interactScript.placingItem = true;

            furniturePrompt.SetActive(true);
            GameObject tempObject = Instantiate(thisSlot.itemSO.gameObject, interactScript.previewHolder);
            Destroy(tempObject.GetComponent<Rigidbody>());
            Destroy(tempObject.GetComponent<ItemScript>());
            Collider[] col = tempObject.GetComponentsInChildren<Collider>();
            foreach (Collider temp in col)
            {
                Destroy(temp);
            }
        }
        else if (thisSlot.itemSO.gameObject != null)
        {
            GameObject tempObject = Instantiate(thisSlot.itemSO.gameObject, heldItemTransform);
            Destroy(tempObject.GetComponent<Rigidbody>());
            Destroy(tempObject.GetComponent<ItemScript>());
            Collider[] col = tempObject.GetComponentsInChildren<Collider>();
            print(col);
            foreach (Collider temp in col)
            {
                Destroy(temp);
            }

        }


        heldItemSO[j] = tempSO;
        quantities[j] = tempQuantity;
        Item item = new Item(tempSO, tempQuantity, tempQuality);
        inventorySlots[j].UpdateSlot(item);
    }

    public void SetActiveItem(Item item)
    {
        activeItem.UpdateSlot(item);

        DeleteVisualItem();

        if (item.SO.type == ItemType.Furniture)
        {

            interactScript.placingItem = true;

            furniturePrompt.SetActive(true);
            GameObject tempObject = Instantiate(item.SO.gameObject, interactScript.previewHolder);
            Destroy(tempObject.GetComponent<Rigidbody>());
            Destroy(tempObject.GetComponent<ItemScript>());
            Collider[] col = tempObject.GetComponentsInChildren<Collider>();
            foreach (Collider temp in col)
            {
                Destroy(temp);
            }
        }
        else if (item.SO.gameObject != null)
        {
            GameObject tempObject = Instantiate(item.SO.gameObject, heldItemTransform);
            Destroy(tempObject.GetComponent<Rigidbody>());
            Destroy(tempObject.GetComponent<ItemScript>());
            Collider[] col = tempObject.GetComponentsInChildren<Collider>();
            foreach (Collider temp in col)
            {
                Destroy(temp);
            }

        }
    }


    public void RemoveActiveItem()
    {
        if (activeItem.itemSO == null) return;
        //Find empty slot
        interactScript.placingItem = false;

        PickupItem(activeItem.item);
        activeItem.itemSO = null;
        activeItem.quantity = 0;
        activeItem.UpdateSlot(null);

        DeleteItem();
    }

    public void RemoveActiveItem(InventorySlot slot)
    {
        if (slot.itemSO == null) return;
        //Find empty slot
        PickupItem(slot.item);
        slot.itemSO = null;
        slot.quantity = 0;
        slot.UpdateSlot(null);

        DeleteItem(slot);
    }

    public void UnequipItem(InventorySlot slot)
    {
        if (slot.itemSO == null) return;
        //Find empty slot
        PickupItem(slot.item);
        slot.itemSO = null;
        slot.quantity = 0;
        slot.UpdateSlot(null);

        DeleteItem();
        equipmentScript.mainHand = null;
        equipmentScript.SetupWeapon();
    }

    public bool CheckForItems(Item[] items)
    {
        foreach (Item item in items)
        {
            List<int> indexes = new List<int>();
            for (int i = 0; i < heldItemSO.Count; i++)
            {
                if (heldItemSO[i] == item.SO)
                {
                    indexes.Add(i);
                }
            }

            int quantity = 0;
            for (int i = 0; i < indexes.Count; i++)
            {
                quantity += quantities[indexes[i]];
            }
            if (quantity >= item.quantity) { }
            else
            {
                return false;
            }
        }

        return true;
    }

    public int CheckQuantity(ItemSO SO)
    {

        List<int> indexes = new List<int>();
        for (int i = 0; i < heldItemSO.Count; i++)
        {
            if (heldItemSO[i] == SO)
            {
                indexes.Add(i);
            }
        }

        int quantity = 0;
        for (int i = 0; i < indexes.Count; i++)
        {
            quantity += quantities[indexes[i]];
        }

        if (activeItem.itemSO == SO)
        {
            quantity += activeItem.quantity;
        }
        if (mainHand.itemSO == SO)
        {
            quantity += mainHand.quantity;
        }
        return quantity;
    }

    public int CheckQuality(ItemSO SO)
    {
        List<int> indexes = new List<int>();
        for (int i = 0; i < heldItemSO.Count; i++)
        {
            if (heldItemSO[i] == SO)
            {
                indexes.Add(i);
            }
        }

        int number = indexes.Count;
        int quality = 0;
        for (int i = 0; i < indexes.Count; i++)
        {
            quality += inventorySlots[indexes[i]].quality;
        }

        if (activeItem.itemSO == SO)
        {
            number++;
            quality += activeItem.quality;
        }
        if (mainHand.itemSO == SO)
        {
            number++;
            quality += mainHand.quality;
        }

        quality = quality / number;
        return quality;
    }

    public int FindSlot(ItemSO SO)
    {


        int j = heldItemSO.LastIndexOf(SO);

        return j;
    }


    public void RemoveItems(Item[] items)
    {
        foreach (Item item in items)
        {
            List<int> indexes = new List<int>();
            for (int i = 0; i < heldItemSO.Count; i++)
            {
                if (heldItemSO[i] == item.SO)
                {
                    indexes.Add(i);
                    print(indexes.Count + " sea " + i);
                }

            }

            int quantity = item.quantity;
            for (int i = indexes.Count; i > 0; i--)
            {
                print(indexes.Count + " " + i);
                if (quantity > quantities[indexes[i - 1]])
                {
                    quantity -= quantities[indexes[i - 1]];
                    quantities[indexes[i - 1]] = 0;
                    inventorySlots[indexes[i - 1]].UpdateSlot(null);
                }
                else
                {
                    quantities[indexes[i - 1]] -= quantity;
                    quantity = 0;

                    inventorySlots[indexes[i - 1]].UpdateSlot(inventorySlots[indexes[i - 1]].item);
                    break;
                }
            }
        }
    }

    public void EquipItem(InventorySlot thisSlot)
    {
        int j = inventorySlots.IndexOf(thisSlot);
        ItemSO tempSO = mainHand.itemSO;
        int tempQuantity = mainHand.quantity;
        int tempQuality = mainHand.quality;

        mainHand.UpdateSlot(thisSlot.item);
        equipmentScript.mainHand = thisSlot.itemSO.equipmentSO;

        heldItemSO[j] = tempSO;
        quantities[j] = tempQuantity;
        Item item = new Item(tempSO, tempQuantity, tempQuality);
        thisSlot.UpdateSlot(item);

        equipmentScript.SetupWeapon();
    }

    public void EquipItem(Item temp)
    {
        UnequipItem(mainHand);

        int children = heldItemTransform.childCount;
        for (int i = 0; i < children; ++i)
            Destroy(heldItemTransform.GetChild(i).gameObject);

        mainHand.UpdateSlot(temp);
        equipmentScript.mainHand = temp.SO.equipmentSO;
        equipmentScript.SetupWeapon();
    }

    public void UseActiveItem()
    {
        if (activeItem.itemSO != null && !GameManager.inventoryMenuOpen)
            UseItem(activeItem.item);
    }


    public void UseItem(InventorySlot slot)
    {
        int j = inventorySlots.IndexOf(slot);

        ItemSO item = slot.itemSO;


        switch (item.itemUsage)
        {
            case ItemSO.ItemUsage.Unusable: break;
            case ItemSO.ItemUsage.Plant:
                if (!interactScript.blocked && TerrainScript.Instance.CheckTexture(interactBox.transform.position))
                {
                    GameObject GO = Instantiate(item.plant, interactScript.groundPos, Quaternion.Euler(0, Random.Range(0, 360), 0));
                    DeleteItem(slot);
                }
                break;
            case ItemSO.ItemUsage.Consume:
                ItemEffect(slot.item);
                if (j >= 0)
                    DeleteItem(slot);
                else DeleteItem();
                break;
        }
    }

    public void SellItem(InventorySlot slot)
    {
        int j = inventorySlots.IndexOf(slot);
        GameManager.Instance.Money += slot.itemSO.baseValue;
        if (slot.itemSO.sellEvent.triggerName != "")
            GameManager.Instance.SetTrigger(slot.itemSO.sellEvent.triggerName);
        //print(slot.itemSO.sellEvent.triggerName);
        if (j >= 0)
            DeleteItem(slot);
        else DeleteItem();
    }


    void ItemEffect(Item tempItem)
    {
        ItemSO item = tempItem.SO;
        Instantiate(eatSFX, transform.position, transform.rotation);
        UIManager.Instance.DisplayUsedItemEffect(tempItem);
        foreach (StatType tempType in item.eventScript.statTypes)
        {
            switch (tempType.type)
            {
                case StatType.Type.MaxHealth:
                    status.baseStats.maxHealth += (int)(tempType.value + tempType.value * (tempItem.quality / 100F));
                    status.baseStats.currentHealth += (int)(tempType.value + tempType.value * (tempItem.quality / 100F));
                    status.rawStats.maxHealth += (int)(tempType.value + tempType.value * (tempItem.quality / 100F));
                    status.Health += (int)(tempType.value + tempType.value * (tempItem.quality / 100F));
                    break;
                case StatType.Type.Health:
                    status.Health += (int)(tempType.value + tempType.value * (tempItem.quality / 100F));
                    break;
                case StatType.Type.Fatigue:
                    status.Fatigue += (int)(tempType.value + tempType.value * (tempItem.quality / 100F));
                    break;
                case StatType.Type.MaxStamina:
                    status.MaxStamina += (int)(tempType.value + tempType.value * (tempItem.quality / 100F));
                    break;
                case StatType.Type.Strength:
                    status.baseStats.damageModifierPercentage += (float)(tempType.value + tempType.value * (tempItem.quality / 100F)) / 100f;
                    status.rawStats.damageModifierPercentage += (float)(tempType.value + tempType.value * (tempItem.quality / 100F)) / 100f;
                    break;

            }
        }

        if (!item.used)
        {
            item.used = true;
            Stats oldStats = new Stats();
            status.ReplaceStats(oldStats, status.baseStats);
            Skill skill = null;
            if (item.firstTimeEvents.Length > 0)
            {
                foreach (EventScript e in item.firstTimeEvents)
                {

                    switch (e.eventType)
                    {
                        case EventScript.EventType.Skill:
                            if (e.skill.level == 0)
                            {
                                skill = e.skill;
                                skillHandler.SkillEXP(e.skill, 0);
                            }
                            break;
                        case EventScript.EventType.Stats:
                            foreach (StatType tempType in e.statTypes)
                            {
                                switch (tempType.type)
                                {
                                    case StatType.Type.MaxHealth:
                                        status.baseStats.maxHealth += tempType.value;
                                        status.baseStats.currentHealth += tempType.value;
                                        status.rawStats.maxHealth += tempType.value;
                                        status.Health += tempType.value;
                                        break;
                                    case StatType.Type.Health:
                                        status.Health += tempType.value;
                                        break;
                                    case StatType.Type.Fatigue:
                                        status.Fatigue += tempType.value;
                                        break;
                                    case StatType.Type.MaxStamina:
                                        status.MaxStamina += tempType.value;
                                        break;
                                    //case StatType.Type.Experience: status.Experience += tempType.value; break;
                                    case StatType.Type.Strength:
                                        status.baseStats.damageModifierPercentage += (float)tempType.value / 100f;
                                        status.rawStats.damageModifierPercentage += (float)tempType.value / 100f;
                                        break;

                                }
                            }



                            break;

                    }
                }

                itemStatUI.SetUI(item, oldStats, skill);
            }
        }
    }



    public void UseItem(Item item)
    {
        switch (activeItem.itemSO.itemUsage)
        {
            case ItemSO.ItemUsage.Unusable: break;
            case ItemSO.ItemUsage.Plant:
                if (!interactScript.blocked)
                {
                    if (TerrainScript.Instance.CheckTexture(interactBox.transform.position) || item.SO.noSoil)
                    {
                        GameObject GO = Instantiate(item.SO.plant, interactScript.groundPos, Quaternion.identity);
                        PlantScript plant = GO.GetComponent<PlantScript>();
                        plant.quality = item.quality;
                        DeleteItem();
                    }
                }
                break;
            case ItemSO.ItemUsage.Consume:
                ItemEffect(item);
                DeleteItem(); break;
            case ItemSO.ItemUsage.Place:
                if (!interactScript.blocked)
                {
                    GameObject GO = Instantiate(item.SO.plant, interactScript.groundPos, interactScript.previewHolder.rotation);
                    ItemScript itemScript = GO.GetComponent<ItemScript>();
                    if (itemScript != null)
                        itemScript.item.quality = item.quality;
                    DeleteItem();
                }
                break;
        }
    }

    void SaveInventoryData()
    {
        dataManager.currentSaveData.active.itemID = 0;
        dataManager.currentSaveData.active.quantity = 0;
        dataManager.currentSaveData.mainHand.itemID = 0;
        dataManager.currentSaveData.mainHand.quantity = 0;
        inventoryData.Clear();
        for (int i = 0; i < inventorySlots.Count; i++)
        {
            InventoryData tempData = new InventoryData();
            if (inventorySlots[i].itemSO != null)
            {
                tempData.itemID = inventorySlots[i].itemSO.ID;
                tempData.quantity = quantities[i];
                tempData.quality = inventorySlots[i].quality;
                tempData.age = inventorySlots[i].item.age;
            }

            inventoryData.Add(tempData);
        }
        if (mainHand.itemSO != null)
        {
            dataManager.currentSaveData.mainHand.itemID = mainHand.itemID;
            dataManager.currentSaveData.mainHand.quantity = mainHand.quantity;
            dataManager.currentSaveData.mainHand.quality = mainHand.quality;
            dataManager.currentSaveData.mainHand.age = mainHand.item.age;
        }
        if (activeItem.itemSO != null)
        {
            dataManager.currentSaveData.active.itemID = activeItem.itemID;
            dataManager.currentSaveData.active.quantity = activeItem.quantity;
            dataManager.currentSaveData.active.quality = activeItem.quality;
            dataManager.currentSaveData.active.age = activeItem.item.age;
        }



        dataManager.currentSaveData.inventoryData = inventoryData;
        SaveQuickslots();
    }

    void SaveQuickslots()
    {

        for (int i = 0; i < quickslots.Length; i++)
        {
            if (quickslots[i].itemSO != null)
                DataManager.Instance.currentSaveData.activeQuickslots[i] = quickslots[i].itemSO.ID;
        }
    }

    void LoadQuickslot()
    {
        for (int i = 0; i < quickslots.Length; i++)
        {
            ItemSO temp = ItemDatabase.Instance.GetItemData(DataManager.Instance.currentSaveData.activeQuickslots[i]);
            Item item = new Item(temp, CheckQuantity(temp), CheckQuality(temp));
            quickslots[i].UpdateSlot(item);
            //quickslots[i].quickslotSO = temp;
            //quickslots[i].quickslotID = FindSlot(temp);
        }

    }

    void LoadInventoryData()
    {
        heldItemSO.Clear();
        quantities.Clear();
        for (int i = 0; i < dataManager.currentSaveData.inventoryData.Count; i++)
        {
            if (dataManager.currentSaveData.inventoryData[i] != null)
            {
                heldItemSO.Add(itemDatabase.GetItemData(dataManager.currentSaveData.inventoryData[i].itemID));
                quantities.Add(dataManager.currentSaveData.inventoryData[i].quantity);
            }
            Item item = new Item(heldItemSO[i], quantities[i], dataManager.currentSaveData.inventoryData[i].quality);

            inventorySlots[i].UpdateSlot(item);

            inventorySlots[i].item.age = dataManager.currentSaveData.inventoryData[i].age;
        }
        if (dataManager.currentSaveData.mainHand.quantity > 0)
        {
            Item item = new Item(itemDatabase.GetItemData(dataManager.currentSaveData.mainHand.itemID), dataManager.currentSaveData.mainHand.quantity, dataManager.currentSaveData.mainHand.quality);
            EquipItem(item);
        }
        if (dataManager.currentSaveData.active.quantity > 0)
        {
            Item item = new Item(itemDatabase.GetItemData(dataManager.currentSaveData.active.itemID), dataManager.currentSaveData.active.quantity, dataManager.currentSaveData.active.quality);
            SetActiveItem(item);
        }
        LoadQuickslot();
    }

    public void OpenQuickslotTab()
    {
        if (GameManager.isPaused || GameManager.menuOpen) return;
        quickslotTab.SetActive(true);
        UIManager.Instance.SetActive(quickslotTab.transform.GetChild(0).gameObject);
    }

    public void CloseQuickslotTab()
    {
        if (GameManager.isPaused || GameManager.menuOpen) return;
        quickslotTab.SetActive(false);
    }


    public void HighlightQuickslot(InventorySlot slot)
    {
        highlightedSlot = slot;
        UIManager.Instance.SetActive(inventorySlots[0].gameObject);
        highlightedSlot.quickslotHighlight.SetActive(true);
    }

    public void AssignQuickslot(InventorySlot slot)
    {
        highlightedSlot.quickslotTarget = slot;
        //highlightedSlot.UpdateSlot(heldItemSO[inventorySlots.IndexOf(slot)], quantities[inventorySlots.IndexOf(slot)]);
        highlightedSlot.UpdateSlot(slot.item);
        highlightedSlot.quickslotSO = slot.itemSO;
        highlightedSlot.quickslotID = FindSlot(slot.itemSO);

        ResetQuickslot();
    }

    public void ResetQuickslot()
    {
        if (highlightedSlot == null) return;
        UIManager.Instance.SetActive(highlightedSlot.gameObject);
        highlightedSlot.quickslotHighlight.SetActive(false);
        highlightedSlot = null;
    }

    void ToggleMenu()
    {
        if (GameManager.shopOpen || StorageScript.inStorage) return;
        inventoryOpen = !inventoryOpen;
        ResetQuickslot();
        inventoryMenu.SetActive(inventoryOpen);
        if (inventoryOpen) Instantiate(openInventorySFX);
        else Instantiate(closeInventorySFX);
        GameManager.inventoryMenuOpen = inventoryOpen;
        UpdateMenuTab();

        //StartCoroutine("WaitFrame");
    }

    void CloseMenu()
    {
        if (!inventoryOpen) return;

        if (highlightedSlot != null)
        {
            ResetQuickslot();
            return;
        }
        Instantiate(closeInventorySFX);
        inventoryOpen = false;
        inventoryMenu.SetActive(false);
        StartCoroutine("WaitFrame");
    }

    IEnumerator WaitFrame()
    {
        yield return new WaitForFixedUpdate();
        GameManager.inventoryMenuOpen = false;
    }

    private void FixedUpdate()
    {
        if (mainHand.itemSO != null)
        {
            equipImage.gameObject.SetActive(true);
            equipImage.sprite = mainHand.itemSO.sprite;
        }
        else equipImage.gameObject.SetActive(false);


        if (activeItem.itemSO != null)
            activeImage.sprite = activeItem.itemSO.sprite;

        activeImage.gameObject.SetActive(activeItem.quantity > 0);
        activeQuantity.gameObject.SetActive(activeItem.quantity > 0);
        activeQuantity.text = "" + activeItem.quantity;


        if (!GameManager.inventoryMenuOpen && !TutorialManager.Instance.inTutorial)
        {

            useItemIndicator.SetActive(activeItem.quantity > 0);
            if (useItemIndicator.activeSelf) indicatorText.text = "" + activeItem.itemSO.itemUsage;
        }
        else useItemIndicator.SetActive(false);
    }

    [Button]
    public void AgeInventory()
    {

        for (int i = 0; i < inventorySlots.Count; i++)
        {
            if (inventorySlots[i].item.SO != null)
            {
                inventorySlots[i].item.age++;
                if (inventorySlots[i].item.SO.ageLimit > 0) {
                    if (inventorySlots[i].item.SO.ageLimit < inventorySlots[i].item.age)
                        DeleteSlot(inventorySlots[i]);
                }
            }
        }

    
    }

    [Button]
    public void PepegaSort()
    {
        pepeList.Clear();
        for (int j = 0; j < 6; j++)
        {
            for (int i = 0; i < inventorySlots.Count; i++)
            {
                if (inventorySlots[i].item.SO != null)
                {
                    print(j + " " + (int)inventorySlots[i].item.SO.type + " " + inventorySlots[i].item.SO.type);
                    if ((int)inventorySlots[i].item.SO.type == j)
                    {
                        Item tempItem = new Item(inventorySlots[i].item.SO, inventorySlots[i].item.quantity, inventorySlots[i].item.quality);
                        pepeList.Add(tempItem);
                    }
                }
            }

        }

        for (int i = 0; i < inventorySlots.Count; i++)
        {
            inventorySlots[i].UpdateSlot(null);
        }
        for (int i = 0; i < pepeList.Count; i++)
        {
            inventorySlots[i].UpdateSlot(pepeList[i]);
        }
    }
}
