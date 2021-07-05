using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class AppaisalWindow : MonoBehaviour
{
    public TextMeshProUGUI title;
    public Image image;
    public TextMeshProUGUI waterText;
    public TextMeshProUGUI qualityText;
    public TextMeshProUGUI quantityText;
    public TextMeshProUGUI maturityText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void DisplayUI(PlantScript plant) {
        title.text = plant.SO.title + " Plant";
        image.sprite = plant.SO.sprite;
        waterText.text = "" + plant.CheckPlantWater() + "%";
        qualityText.text = "" +  plant.quality;
        quantityText.text = "" + plant.quantity;
        maturityText.text = 100 * plant.phase/plant.phaseObjects.Length + "%"; 
    }

}
