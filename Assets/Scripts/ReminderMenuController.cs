using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ReminderMenuController : MonoBehaviour
{
    /*
    Script attached to 'ReminderMenu' object which is the parent of all UI elements shownwhen setting a reminder
    */
    public GameObject placeObject; // reference to the GameObject in the scene which contains the script to place an object in AR
    public int urgencyId = -1; // default urgency ID set to -1
    private int objId = -1; // 0: tea, 1: pill, 2: paper
    private List<GameObject> childObjects = new List<GameObject>();
    private bool reminderMenuShowing = false; // is the reminder menu currently showing
    private GameObject inputFieldObject; // InputField object which user can type in 


    void Start()
    {
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
                if (child.gameObject.name == "InputField (TMP)")
                {
                    inputFieldObject = child.gameObject;
                }
                childObjects.Add(child.gameObject);
                child.gameObject.SetActive(false);
            }           
        }
    }

    public void ToggleView()
    {
        // show the reminder menu if it is not currently showing, otherwise hide the menu 
        if (reminderMenuShowing)
        {
            foreach (GameObject childObj in childObjects)
            {
                childObj.SetActive(false);
            }
        }
        else
        {
            foreach (GameObject childObj in childObjects)
            {
                childObj.SetActive(true);
            }
        }
        reminderMenuShowing = !reminderMenuShowing;
    }

    public void HideView()
    {
        // hide the reminder menu
        inputFieldObject.GetComponent<TMP_InputField>().text = "";
        foreach (GameObject childObj in childObjects)
        {
            if (childObj.name == "UrgencyButtons")
            {
                if (!childObj.activeInHierarchy) {
                    reminderMenuShowing = false;
                    break;
                }
                var urgencyButtonController = childObj.GetComponent<UrgencyButtonController>();
                urgencyButtonController.UnselectAllButtons();
            }
            childObj.SetActive(false);
        }
        reminderMenuShowing = false;
    }

    public void SetObjId(int id)
    {
        objId = id;
    }

    public void SetUrgencyId(int id)
    {
        urgencyId = id;
    }

    public void PlaceButtonPress()
    {
        // called when 'Place Reminder' button is pressed
        string text = inputFieldObject.GetComponent<TMP_InputField>().text; // get text from input field
        placeObject.GetComponent<ARTapToPlace>().PlaceObject(objId, urgencyId, text); // call PlaceObject with the object ID, urgency ID and text
    }
}
