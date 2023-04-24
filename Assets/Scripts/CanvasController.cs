using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasController : MonoBehaviour
{
    /*
    Script attached to the Canvas to ensure the correct parts of the canvas are showing
    */
    List<GameObject> childObjects = new List<GameObject>(); // list of all child objects of the canvas

    void Start()
    {
        // on start, create the list of all children and set the correct ones to be active
        Transform[] allChildren = GetComponentsInChildren<Transform>();
        bool first = true;
        foreach (Transform child in allChildren)
        {
            if (first)
            {
                first = false;
            }
            else
            {
                // Scroll-Snap controls the carousel which is used for navigation of icons
                // TogglePatientView is a button which controls the toggling of the patient's view, initially used to prototype what the patient view might look like
                // ClearAllButton is a button which deletes all spawned AR objects
                if (child.gameObject.name == "Scroll-Snap" || child.gameObject.name == "TogglePatientView" || child.gameObject.name == "ClearAllButton")
                {
                    childObjects.Add(child.gameObject);
                    child.gameObject.SetActive(false);
                }
                else if (child.gameObject.name == "PlaceBedButton")
                {
                    childObjects.Add(child.gameObject);
                    child.gameObject.SetActive(true);
                }
            }
        }
        ShowOtherButtons(); // Hides Place Bed button and shows other UI elements. Remove this line if you want to specify the bed location (then a "Place Bed" button will spawn first)
    }
    public void ShowOtherButtons()
    {
        // hides the Place Bed button and shows other UI elements
        foreach (GameObject childObj in childObjects)
        {
            if (childObj.name == "PlaceBedButton")
            {
                childObj.SetActive(false);
            }
            else
            {
                childObj.SetActive(true);
            }
        }
    }

    public void HideAllButtonsExceptPatient()
    {
        // hide all UI elements except the "Toggle Patient View" button
        foreach (GameObject childObj in childObjects)
        {
            if (childObj.name != "TogglePatientView")
            {
                childObj.SetActive(false);
            }
        }
    }
}
