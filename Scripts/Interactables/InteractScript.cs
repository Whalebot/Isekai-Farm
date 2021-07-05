using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Sirenix.OdinInspector;
public class InteractScript : MonoBehaviour
{
    public bool grid;
    public float gridSize;
    public bool gridOffset;
    public bool canInteract;

    public TextMeshProUGUI interactText;
    public Transform reference;
    public Transform player;
    public Material baseMaterial;
    public Material pickupMaterial;
    public Material failMaterial;
    public InventoryScript inventoryScript;
    Renderer rend;
    Vector3 nearestGrid;
    public Movement move;
    public Status status;
    public bool placingItem;

    public GameObject itemPrompt;
    public LayerMask blockMask;
    public LayerMask interactMask;
    [FoldoutGroup("Debug")] public bool foundItem;
    [FoldoutGroup("Debug")] public bool foundPlant;
    [FoldoutGroup("Debug")] public bool foundInteractable;
    [FoldoutGroup("Debug")] public bool water;
    [FoldoutGroup("Debug")] public bool blocked;
    [FoldoutGroup("Debug")] public bool inSoil;
    [FoldoutGroup("Debug")] public ItemScript lastItem;
    [FoldoutGroup("Debug")] public PlantScript lastPlant;
    [FoldoutGroup("Debug")] public Interactable lastInteractable;
    [FoldoutGroup("Debug")] public Vector3 groundPos;
    [FoldoutGroup("Debug")] public float rayLength;
    [FoldoutGroup("Debug")] public LayerMask groundMask;
    [FoldoutGroup("Debug")] public Transform previewHolder;
    RaycastHit hit;

    public AppaisalWindow appaisalWindow;



    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponentInChildren<Renderer>();
        status.animationEvent += MyCollisions;
        InputManager.Instance.leftInput += RotateLeft;
        InputManager.Instance.rightInput += RotateRight;
    }

    void RotateLeft()
    {
        previewHolder.Rotate(previewHolder.up, 45);
    }
    void RotateRight()
    {
        previewHolder.Rotate(previewHolder.up, -45);
    }
    private void Update()
    {
    }

    Vector3 FindNearestGrid()
    {
        nearestGrid = reference.position;


        nearestGrid.x = RoundToGridSize(nearestGrid.x);

        nearestGrid.y = Mathf.Round(nearestGrid.y);

        nearestGrid.z = RoundToGridSize(nearestGrid.z);

        return nearestGrid;
    }

    float RoundToGridSize(float f)
    {
        float closest = Mathf.Round(f / gridSize);
        float f1 = closest * gridSize;
        float f2 = closest * gridSize;

        if (gridOffset)
        {
            f1 = closest * gridSize + gridSize / 2;
            f2 = closest * gridSize - gridSize / 2;

        }

        if (Mathf.Abs(f1 - f) > Mathf.Abs(f2 - f))
        {
            return f2;
        }
        else
        {
            return f1;
        }
    }

    void FixedUpdate()
    {
        if (Physics.Raycast(transform.position + Vector3.up * 0.5F, Vector3.down, out hit, rayLength, groundMask))
            groundPos = hit.point;

        rend.enabled = !move.isMoving && status.currentState == Status.State.Neutral && !GameManager.isPaused;
        if (status.currentState == Status.State.Neutral)
        {
            MyCollisions();
        }
    }


    void MyCollisions()
    {
        foundItem = false;
        foundPlant = false;
        foundInteractable = false;
        blocked = false;
        water = false;
        itemPrompt.SetActive(false);
        appaisalWindow.gameObject.SetActive(false);

        if (grid)
            transform.position = FindNearestGrid();
        else { transform.position = reference.position; }

        rend.material = baseMaterial;

        Collider[] col = Physics.OverlapBox(transform.position, Vector3.one * 0.5F, transform.rotation, interactMask);
        foreach (Collider item in col)
        {


            if (item.tag != "Player" && item.tag != "Terrain")
            {
                blocked = true;
            }
            if (item.tag == "Water")
            {
                water = true;
            }
            if (item.tag == "Item")
            {
                foundItem = true;

                lastItem = item.GetComponentInParent<ItemScript>();
                interactText.text = "" + item.GetComponentInParent<Interactable>().type;
                itemPrompt.SetActive(!GameManager.menuOpen && !TutorialManager.Instance.inTutorial);
            }
            else if (item.tag == "Plant")
            {
                lastPlant = item.GetComponentInParent<PlantScript>();
                appaisalWindow.DisplayUI(lastPlant);
                appaisalWindow.gameObject.SetActive(true);
                if (!lastPlant.finished || lastPlant.picked) return;

                foundPlant = true;

                if (lastPlant.type != Interactable.Type.None)
                {
                    interactText.text = "" + item.GetComponentInParent<Interactable>().type;
                    itemPrompt.SetActive(!GameManager.menuOpen && !TutorialManager.Instance.inTutorial);
                }
            }
            else if (item.tag == "Interact")
            {
                foundInteractable = true;

                lastInteractable = item.GetComponent<Interactable>();
                if (item.GetComponent<Interactable>() == null)
                    lastInteractable = item.GetComponentInParent<Interactable>();

                interactText.text = "" + item.GetComponentInParent<Interactable>().type;

                itemPrompt.SetActive(!GameManager.menuOpen && !TutorialManager.Instance.inTutorial);
            }
        }


        if (foundInteractable || foundItem || foundPlant)
        {
            if (!placingItem)
            {

                rend.material = pickupMaterial;
            }
            else rend.material = failMaterial;
        }
        else if (blocked)
        {
            rend.material = failMaterial;
        }
    }


    public void PickItem()
    {
        if (lastItem != null)
            lastItem.Interact();
    }

    public void PickPlant()
    {
        if (!lastPlant.finished) return;
        lastPlant.Interact();
    }


}
