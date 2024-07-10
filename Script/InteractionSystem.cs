using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class InteractionSystem : MonoBehaviour
{
    [Header("Detection Parameters")]
    //Detection Point
    public Transform detectionPoint;
    //Detection Radius
    private const float detectionRadius = 0.2f;
    //Detection Layer
    public LayerMask detectionLayer;
    //Cached Trigger Object
    public GameObject detectedObject;

    //public GameObject UIPrefab;

    [Header("Pop up Window")]   //Pop up Window
    public GameObject item_Window;
    public Image itemImage;
    public Text itemName;
    public Text itemDesc;
    public bool isWindowActive = false;

    private Item currentItem;
        
    //I tried to delete UIPrefab from gameobject and just put item_Window in the script, should and UIPrefab for later use
    //[Header("Others")]
    //List of picked items
    //public List<GameObject> pickItems = new List<GameObject>();

    // Update is called once per frame

    private void Start()
    {
        item_Window = Instantiate(item_Window, transform);
        item_Window.SetActive(false); // Deactivate it initially
    }

    void Update()
    {
        if (DetectObject() && InteractInput())
        {
            Item item = detectedObject.GetComponent<Item>();
            SceneTransition sceneTransition = detectedObject.GetComponent<SceneTransition>();
            Debug.Log("After setting isWindowActive to false: " + isWindowActive);
            if (item != null)
            {
                currentItem = item; // Store the item for later use
                item.Interact();

            }
            else if (sceneTransition != null)
            {
                sceneTransition.TriggerSceneChange();
            }
        }

            if (isWindowActive && InteractInput())
            {
                ClosePopUpWindow(); // Close popup regardless of item type
            }

    }


    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(detectionPoint.position, detectionRadius);
    }
    bool InteractInput()
    {
        return Input.GetKeyDown(KeyCode.E);
    }

    bool DetectObject()
    {
        Collider2D obj = Physics2D.OverlapCircle(detectionPoint.position, detectionRadius, detectionLayer);
        if (obj == null)
        {
            detectedObject = null;
            return false;
        }
        else
        {
            detectedObject = obj.gameObject;
            return true;
        }
    }

    //public void PickupItem(GameObject item)
    //{
    //FindObjectOfType<InventorySystem>().AddItem(item);
    //}

    public void PopUpWindowCall(Item item)
    {
        if (isWindowActive)
        {
            return; // If the window is already active, do nothing
        }
        Debug.Log("Open Examine");
        isWindowActive = true;

        item_Window.SetActive(true); // Make sure it's active
        isWindowActive = true;
        Debug.Log("Is Window Active After Setting: " + isWindowActive);


        //item_Window.SetActive(!item_Window.activeSelf); // Toggle the active state of the popup
        //isWindowActive = item_Window.activeSelf;

        // Find the "Item_Window" child GameObject within the instantiated prefab
        Transform itemWindowTransform = item_Window.transform.Find("Item_Window");
        if (itemImage != null && item != null)
        {
            SpriteRenderer itemSpriteRenderer = item.GetComponent<SpriteRenderer>();
            if (itemSpriteRenderer != null)
            {
                itemImage.sprite = itemSpriteRenderer.sprite;
                itemName.text = item.itemName;
                itemDesc.text = item.itemDescription;
                Debug.Log("Item Name: " + item.itemName);
            }
        }
        else
        {
            Debug.LogError("UI elements or Item Image not found!");
        }
    }

    public void ClosePopUpWindow()
    {
        if (item_Window != null && item_Window.activeSelf)
        {
            item_Window.SetActive(false);
            isWindowActive = false;
        }
    }
}
