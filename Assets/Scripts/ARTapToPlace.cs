using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using System;

public class ARTapToPlace : MonoBehaviour
{
    // private ARSessionOrigin arOrigin;
    public GameObject placementIndicator;
    public GameObject[] objects;

    public GameObject[] indicators;
    // public GameObject objectToPlace;

    private ARRaycastManager arRaycastManager;

    private Pose placementPose;
    private bool placementPoseIsValid = false;
    private bool bedPlaced = false;
    private Pose bedPose;

    void Start()
    {
        // arOrigin = FindObjectOfType<ARSessionOrigin>();
        arRaycastManager = FindObjectOfType<ARRaycastManager>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePlacementPose();
        UpdatePlacementIndicator();

        // if (placementPoseIsValid && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began) {
        //     PlaceObject();
        // }
    }

    // public void ChangeObject(int id) {
    //     objectToPlace = objects[id];
    //     Debug.Log(objectToPlace);
    // }

    public void PlaceObject(int id)
    {
        Instantiate(objects[id], placementPose.position, placementPose.rotation);
    }

    public void PlaceColour(int id)
    {
        Vector3 temp = new Vector3(placementPose.position.x, placementPose.position.y+0.05f, placementPose.position.z);
        Instantiate(indicators[id], temp, placementPose.rotation);
    }

    public void PlaceBed()
    {
        bedPlaced = true;
        bedPose = placementPose;
    }

    public void PlaceObjectOnBed(int id)
    {
        Vector3 addedRandomness = new Vector3(bedPose.position.x + UnityEngine.Random.Range(-0.3f, 0.3f), bedPose.position.y + UnityEngine.Random.Range(0f, 0.3f), bedPose.position.z + UnityEngine.Random.Range(-0.3f, 0.3f));
        Instantiate(objects[id], addedRandomness, bedPose.rotation);
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
