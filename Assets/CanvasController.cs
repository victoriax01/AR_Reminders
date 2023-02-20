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
                if (child.gameObject.name == "TeaButton" || child.gameObject.name == "PillButton" || child.gameObject.name == "AlertButton" || child.gameObject.name == "TogglePatientView")
                {
                    childObjects.Add(child.gameObject);
                    child.gameObject.SetActive(false);
                }
                else if (child.gameObject.name == "PlaceBedButton")
                {
                    childObjects.Add(child.gameObject);
                    child.gameObject.SetActive(true);
                }

                else if (child.gameObject.name == "Panel" || 
                child.gameObject.name == "Water" || 
                child.gameObject.name == "Meds" || 
                child.gameObject.name == "Toilet" ||
                child.gameObject.name == "RequestButton1" || 
                child.gameObject.name == "RequestButton2" || 
                child.gameObject.name == "RequestButton3")
                {
                    patientViewObjects.Add(child.gameObject);
                    child.gameObject.SetActive(false);
                }
            }
        }
        // Debug.Log(childObjects.Count);
    }

    public void togglePatientView()
    {
        if (patientView)
        {
            ShowOtherButtons();
            patientViewToggle(false);
        }
        else
        {
            HideAllButtonsExceptPatient();
            patientViewToggle(true);

        }
        patientView = !patientView;
    }

    private void patientViewToggle(bool toggle)
    {
        foreach (GameObject childObj in patientViewObjects)
        {
            childObj.SetActive(toggle);
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
