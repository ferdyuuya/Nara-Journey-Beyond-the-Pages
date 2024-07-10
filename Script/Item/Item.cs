using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(BoxCollider2D))]

public class Item : MonoBehaviour
{
    public enum InteractionType
    {
        Collectable,
        MoveToScene,
        Examine
    }
    public InteractionType type;

    [Header("Item Information")]
    public string itemName;
    public string itemDescription;
    //public Sprite itemImage;

    [Header("Events")]
    public UnityEvent onInteract;

    //Collider Trigger
    //Interaction Type
    private void Reset()
    {
        GetComponent<Collider2D>().isTrigger = true;
        gameObject.layer = 6;
    }
    public bool itemCollectedMie;
    public void Interact()
    {
        switch (type)
        {
            case InteractionType.Collectable:
                Debug.Log("Collectable");
                FindObjectOfType<InteractionSystem>().PopUpWindowCall(this);
                //FindObjectOfType<InventorySystem>().AddItem(gameObject);
                GetComponent<SpriteRenderer>().enabled = false; // Nonaktifkan sprite renderer
                GetComponent<Collider2D>().enabled = false;  // Nonaktifkan collider
                break;

            //itemCollectedMie = FindObjectOfType<InventorySystem>().collectibleItem.Contains(gameObject);

            //FindObjectOfType<InteractionSystem>().PopUpWindowCall(this);

            //Note : the collider still there so we need to disable it but i dont know how to do it yet sowwy :(
            //Debug.Log("isCollected before : " + itemCollectedMie);
            //SpriteRenderer spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
            //BoxCollider2D boxCollider = gameObject.GetComponent<BoxCollider2D>();
            //if (itemCollectedMie == false) { 
            //  FindObjectOfType<InventorySystem>().AddItem(gameObject);
            //  spriteRenderer.enabled = false;

            //}
            //itemCollectedMie = true;
            //Debug.Log("isCollected after : " + itemCollectedMie);
            //break;
            case InteractionType.MoveToScene:
                //MoveToScene();
                Debug.Log("MoveToScene");
                break;
            case InteractionType.Examine:
                //Examine();
                FindObjectOfType<InteractionSystem>().PopUpWindowCall(this); //Move to InteractionSystem
                                                                             //Display an UI and Description the Item
                Debug.Log("Examine");
                break;
            default:
                Debug.Log("Interaction Null");
                break;
        }
        //onInteract.Invoke(); Call the Custom Event
        onInteract.Invoke();
    }
}