using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class InventorySystem : MonoBehaviour
{
    [Header("General Fields")]
    //List of collectible items
    public List<GameObject> collectibleItem = new List <GameObject>();
    //Indicates collectible windows is on
    public bool isOpen;
    //Inventory Window
    [Header("UI Collectible")]
    public GameObject inventoryWindow;
    public Text collectible_title;
    public Image[] collectible_image;
    //public Text collectible_text;

    //Add item into collectible list
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.I))
        {
            TogggleInvetory();
        }
    }

    void TogggleInvetory()
    {
        isOpen = !isOpen;
        inventoryWindow.SetActive(isOpen);

    }
    public void AddItem(GameObject item)
    {
        collectibleItem.Add(item);
        Update_UI();
    }

    void Update_UI()
    {
        HideAllInventtory();
        //For each item in the "collectibleItem" list
        //Show it in the inventory window "item_images"
        for (int i = 0; i < collectibleItem.Count; i++)
        {
            collectible_image[i].sprite = collectibleItem[i].GetComponent<SpriteRenderer>().sprite;
            collectible_image[i].gameObject.SetActive(true);
        }
    }

    void HideAllInventtory() { foreach (var i in collectible_image) { i.gameObject.SetActive(false); }} // Hide all items in the inventory window

    public void onClickItem(int index)
    {
        if (index >= 0 && index < collectibleItem.Count)
        {
            GameObject selectedItemObject = collectibleItem[index]; // Get the GameObject of the item
            Item selectedItem = selectedItemObject.GetComponent<Item>(); // Get the Item component from the GameObject

            if (selectedItem != null)
            {
                FindObjectOfType<InteractionSystem>().PopUpWindowCall(selectedItem);
            }
        }
    }
}



