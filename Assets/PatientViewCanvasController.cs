using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatientViewCanvasController : MonoBehaviour
{
    List<GameObject> childObjects = new List<GameObject>();
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
                if (child.gameObject.name == "Text (TMP)")
                {
                    continue;
                }
                childObjects.Add(child.gameObject);
                child.gameObject.SetActive(false);
            }           
        }
        Debug.Log(childObjects.Count);
    }

    public void ToggleView()
    {
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

    // Update is called once per frame
    void Update()
    {
        
    }
}
