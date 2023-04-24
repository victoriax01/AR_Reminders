using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatientViewCanvasController : MonoBehaviour
{
    /*
    Script attached to "PatientView" which is an empty GameObject that is a parent to all UI elements shown on the Patient View
    
    NOTE: Patient View object was disabled on final implementation due to it being implemented in the first place to demonstrate what the patients might see
    */
    List<GameObject> childObjects = new List<GameObject>();
    private bool patientView = false;
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
                if (child.gameObject.name == "Text (TMP)")
                {
                    continue;
                }
                childObjects.Add(child.gameObject);
                child.gameObject.SetActive(false);
            }           
        }
    }

    public void ToggleView()
    {
        // called when Toggle Patient View button is pressed, switches currently active and inactive objects around
        if (patientView)
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
        patientView = !patientView;
    }
}
