using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UrgencyButtonController : MonoBehaviour
{
    public GameObject placeObject;
    List<GameObject> childObjects = new List<GameObject>();
    public int buttonID; // 0: tea, 1: pill, 2: paper
    private bool buttonsShowing = false;

    // Start is called before the first frame update
    void Awake()
    {
        placeObject = GameObject.Find("PlaceObject");
        Transform[] allChildren = GetComponentsInChildren<Transform>(includeInactive: true);
        bool first = true;
        foreach (Transform child in allChildren)
        {
            if (first)
            {
                first = false;
            }
            else
            {   
                child.gameObject.SetActive(false);
                childObjects.Add(child.gameObject);
            }
        }
    }

    public void ToggleButtons(int buttonId)
    {
        if (buttonID != buttonId)
        {
            buttonID = buttonId;
            ShowButtons();
        }
        else
        {
            if (buttonsShowing)
            {
                HideButtons();
            }
            else
            {
                ShowButtons();
            }
        }
    }
    public void ShowButtons()
    {
        buttonsShowing = true;
        foreach (GameObject obj in childObjects)
        {
            obj.SetActive(true);
        }
    }

    public void HideButtons()
    {
        buttonsShowing = false;
        foreach (GameObject obj in childObjects)
        {
            obj.SetActive(false);
        }
    }

    public void RedButton()
    {
        placeObject.GetComponent<ARTapToPlace>().PlaceObject(buttonID);
        placeObject.GetComponent<ARTapToPlace>().PlaceColour(0);
    }

    public void YellowButton()
    {
        placeObject.GetComponent<ARTapToPlace>().PlaceObject(buttonID);
        placeObject.GetComponent<ARTapToPlace>().PlaceColour(1);
    }

    public void GreenButton()
    {
        placeObject.GetComponent<ARTapToPlace>().PlaceObject(buttonID);
        placeObject.GetComponent<ARTapToPlace>().PlaceColour(2);
    }
}
