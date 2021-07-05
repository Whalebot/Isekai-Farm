using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolScript : MonoBehaviour
{
    public InteractScript interactScript;
    public InventoryScript inventory;
    public GameObject soil;
    public GameObject interactBox;
    public GameObject tillFX;

    public int waterContent;

    public int waterSize = 10;
    // Start is called before the first frame update
    void Start()
    {
        //  inventory = GetComponentInParent<InventoryScript>();
    }

    public void PlantSeeds()
    {
        //inventory.mainHand.itemSO

    }

    public void RefillWater()
    {
        waterContent = 10;
    }

    public void HammerSoil() {
        Instantiate(tillFX, interactScript.groundPos, transform.rotation);
        if (TerrainScript.Instance != null)
        {
            TerrainScript.Instance.NormalizeTerrain(interactScript.groundPos);

        }
    }


    public void TillSoil()
    {
        // if (interactScript.inSoil && !interactScript.blocked)
        {
            Instantiate(tillFX, interactScript.groundPos, transform.rotation);
            if (TerrainScript.Instance != null)
            {
                TerrainScript.Instance.ChangeTexture(interactScript.groundPos);
               
            }

        }
    }

    public void WaterSoil()
    {
        waterContent--;
        if (TerrainScript.Instance != null)
        {
            TerrainScript.Instance.WaterTerrain(interactScript.groundPos);
            //Instantiate(tillFX, interactScript.groundPos, transform.rotation);
        }
    }

    public void Eat()
    {
        inventory.UseActiveItem();
    }

    public void Plant()
    {
        inventory.UseActiveItem();
    }

    public void Equip()
    {
        inventory.RemoveActiveItem();
    }

    public void Unequip()
    {
        inventory.RemoveActiveItem();
    }


    // Update is called once per frame
    void Update()
    {

    }
}
