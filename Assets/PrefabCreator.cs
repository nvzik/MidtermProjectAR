using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class PrefabCreator : MonoBehaviour
{
    [SerializeField] private GameObject dragonPrefab;
    [SerializeField] private Vector3 prefabOffset;

    private GameObject dragon;
    private ARTrackedImageManager arTrackedImageManager;

    private void OnEnable()
    {
        
        arTrackedImageManager = gameObject.GetComponent<ARTrackedImageManager>();

        arTrackedImageManager.trackedImagesChanged += OnImageChanged;
        
    }

    private void OnImageChanged(ARTrackedImagesChangedEventArgs obj)
    {
        foreach (ARTrackedImage image in obj.added)
        {
            dragon = Instantiate(dragonPrefab, image.transform);
            dragon.transform.position += prefabOffset;
            dragon.SetActive(true);
        }
    }
}
