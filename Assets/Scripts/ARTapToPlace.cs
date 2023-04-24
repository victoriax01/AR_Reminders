using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using System;
using TMPro;

public class ARTapToPlace : MonoBehaviour
{
    /*
    Script attached PlaceObject which is an empty GameObject that controls what objects/prefabs to place 

    Some of the code used can be found in this ARCore tutorial: https://codelabs.developers.google.com/arcore-unity-ar-foundation#0
    */
    public GameObject placementIndicator; // get reference to the placement indicator
    public GameObject[] objects; // list of prefabs of objects that can be placed
    public GameObject[] indicators; // list of prefabs of colour indicators
    public GameObject textPrefab; // empty text prefab used for reminders including text

    private ARRaycastManager arRaycastManager;

    private Pose placementPose; // position and orientation of the placement indicator as a marker for where objects are to be placed
    private bool placementPoseIsValid = false;
    private bool bedPlaced = false;
    private Pose bedPose; // position and orientation of the bed set by the user

    private List<int> onBed; // list of objects currently on the bed

    private List<GameObject> placedObjects; // list of objects placed in AR

    void Start()
    {
        onBed = new List<int>();
        placedObjects = new List<GameObject>();
        arRaycastManager = FindObjectOfType<ARRaycastManager>();
    }

    void Update()
    {
        // every frame, the we update 'placementPose' and move the placement indicator to placementpose
        UpdatePlacementPose();
        UpdatePlacementIndicator();
    }

    public void PlaceObject(int objId, int urgencyId, string text)
    {
        /*
        Places an object where the placement indicator is

        objId: integer which contains the index of what object is being placed (0: tea, 1: pill, 2: paper, 3: toilet, 4: water, 5: exclaimation mark, 6: sleeping icon, 7: cross, 8: tick)
        urgencyId: integer which contains the urgency (0: red, 1: yellow, 2: green)], will be -1 if urgency is not set
        text: string which contains the text associated with the reminder, can be empty
        */
        GameObject obj = Instantiate(objects[objId], placementPose.position, placementPose.rotation); // spawn AR object
        Vector3 temp = new Vector3(placementPose.position.x, placementPose.position.y+0.05f, placementPose.position.z); // temporary Vector3 so text is not spawned on top of the object
        if (urgencyId != -1)
        {
            GameObject urgencyIndicator = Instantiate(indicators[urgencyId], temp, placementPose.rotation); // if user adds a colour, spawn the colour 
            urgencyIndicator.transform.parent = obj.transform; // set the colour object as a child of the main object
        }
        if (text != "")
        {
            // spawn the text similarly to colour above
            temp = new Vector3(placementPose.position.x, placementPose.position.y+0.12f, placementPose.position.z);
            GameObject textObj = Instantiate(textPrefab);
            textObj.GetComponent<RectTransform>().position = temp;
            textObj.GetComponent<RectTransform>().rotation = placementPose.rotation;
            textObj.GetComponent<TMP_Text>().text = text;
            textObj.transform.parent = obj.transform;
        }
        placedObjects.Add(obj);
    }

    public void DestroyAllObjects()
    {
        // destroys all spawned objects
        foreach (GameObject obj in placedObjects)
        {
            Destroy(obj);
        }
    }

    public void PlaceBed()
    {
        // sets the pose of the bed
        bedPlaced = true;
        bedPose = placementPose;
    }

    public void PlaceObjectOnBed(int id)
    {
        // if the object is not already spawned on the bed, then spawn it 
        if (!onBed.Contains(id))
        {
            Vector3 objectPos = bedPose.position;
            // switch case to ensure objects don't overlap with each other
            switch(onBed.Count)
            {
                case 0:
                    objectPos.x -= 0.1f;
                    break;
                case 1:
                    break;
                case 2:
                    objectPos.x += 0.1f;
                    break;
            }
            onBed.Add(id);
            Instantiate(objects[id], objectPos, bedPose.rotation);
        }
    }

    private void UpdatePlacementPose()
    {   
        // uses raycast from the center of the screen to set a valid placementPose
        var screenCenter = Camera.current.ViewportToScreenPoint(new Vector3(0.5f, 0.5f));
        var hits = new List<ARRaycastHit>();
        arRaycastManager.Raycast(screenCenter, hits, TrackableType.Planes);

        placementPoseIsValid = hits.Count > 0;
        if (placementPoseIsValid) {
            placementPose = hits[0].pose;

            var cameraFoward = Camera.current.transform.forward;
            var cameraBearing = new Vector3(cameraFoward.x, 0, cameraFoward.z).normalized;
            placementPose.rotation = Quaternion.LookRotation(cameraBearing);
        }
    }

    private void UpdatePlacementIndicator()
    {
        // if placementPose is valid, then move the placement indicator object to the placementPose. Otherwise hide the indicator
        if (placementPoseIsValid) {
            placementIndicator.SetActive(true);
            placementIndicator.transform.SetPositionAndRotation(placementPose.position, placementPose.rotation);
        }
        else {
            placementIndicator.SetActive(false);
        }
    }
}
