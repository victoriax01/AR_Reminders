using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ReminderMenuController : MonoBehaviour
{
    public GameObject placeObject;
    public int urgencyId = -1;
    private int objId = -1; // 0: tea, 1: pill, 2: paper
    private List<GameObject> childObjects = new List<GameObject>();
    private bool reminderMenuShowing = false;
    private GameObject inputFieldObject;


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
        string text = inputFieldObject.GetComponent<TMP_InputField>().text; // CHECK IF EMPTY/NULL (no text input)
        placeObject.GetComponent<ARTapToPlace>().PlaceObject(objId, urgencyId, text);
    }
}
