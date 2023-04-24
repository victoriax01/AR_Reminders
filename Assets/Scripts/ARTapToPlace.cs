using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using System;
using TMPro;

public class ARTapToPlace : MonoBehaviour
{
    // private ARSessionOrigin arOrigin;
    public GameObject placementIndicator;
    public GameObject[] objects;

    public GameObject[] indicators;
    public GameObject textPrefab;
    // public GameObject objectToPlace;

    private ARRaycastManager arRaycastManager;

    private Pose placementPose;
    private bool placementPoseIsValid = false;
    private bool bedPlaced = false;
    private Pose bedPose;

    private List<int> onBed;

    private List<GameObject> placedObjects;

    void Start()
    {
        onBed = new List<int>();
        placedObjects = new List<GameObject>();
        arRaycastManager = FindObjectOfType<ARRaycastManager>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePlacementPose();
        UpdatePlacementIndicator();
    }

    public void PlaceObject(int objId, int urgencyId, string text)
    {
        GameObject obj = Instantiate(objects[objId], placementPose.position, placementPose.rotation);
        Vector3 temp = new Vector3(placementPose.position.x, placementPose.position.y+0.05f, placementPose.position.z);
        if (urgencyId != -1)
        {
            GameObject urgencyIndicator = Instantiate(indicators[urgencyId], temp, placementPose.rotation);
            urgencyIndicator.transform.parent = obj.transform;
        }
        if (text != "")
        {
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
        foreach (GameObject obj in placedObjects)
        {
            Destroy(obj);
        }
    }

    public void PlaceBed()
    {
        bedPlaced = true;
        bedPose = placementPose;
    }

    public void PlaceObjectOnBed(int id)
    {
        if (!onBed.Contains(id))
        {
            Vector3 objectPos = bedPose.position;
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
        if (placementPoseIsValid) {
            placementIndicator.SetActive(true);
            placementIndicator.transform.SetPositionAndRotation(placementPose.position, placementPose.rotation);
        }
        else {
            placementIndicator.SetActive(false);
        }
    }
}
