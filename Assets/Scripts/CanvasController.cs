using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasController : MonoBehaviour
{
    List<GameObject> childObjects = new List<GameObject>();
    List<GameObject> patientViewObjects = new List<GameObject>();

    private bool patientView = false;
    // Start is called before the first frame update
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
    }
    public void ShowOtherButtons()
    {
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
        foreach (GameObject childObj in childObjects)
        {
            if (childObj.name != "TogglePatientView")
            {
                childObj.SetActive(false);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
