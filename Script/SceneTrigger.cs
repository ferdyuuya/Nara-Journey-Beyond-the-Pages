using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneTrigger : MonoBehaviour
{
    //public GameObject confirmationPopup;
    private InteractionSystem interactionSystem; // Reference to InteractionSystem

    private void Start()
    {
        interactionSystem = FindObjectOfType<InteractionSystem>();
    }

    //public void ShowConfirmation()
    //{
    //    if (confirmationPopup != null)
    //    {
    //        confirmationPopup.SetActive(true);
    //    }
    //}
}
