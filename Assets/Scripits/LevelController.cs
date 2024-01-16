using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    
    [SerializeField] private Platform[] platformPrefabs;
    [SerializeField] private List<Platform> platforms = new List<Platform>();
    [SerializeField] private Transform levelTrash;
    private void Awake()
    {
        CreatePlatform();
        Tiger.onNewPlatformReached += CreatePlatform;
    }
    private void OnDisable()
    {
        Tiger.onNewPlatformReached -= CreatePlatform;
    }
    private void CreatePlatform()
    {
        Platform newPlatform = Instantiate(platformPrefabs[UnityEngine.Random.Range(0,platformPrefabs.Length)],levelTrash);
        
        newPlatform.transform.rotation = GetPlatformRotation();
        newPlatform.transform.position = GetPlatformPosition();
        platforms.Add(newPlatform);
    }
    private Vector3 GetPlatformPosition()
    {
        Vector3 newPos;
        newPos = platforms[platforms.Count - 1].gameObject.transform.position + transform.forward*UnityEngine.Random.Range(3,9);
        Debug.Log(platforms[platforms.Count - 1].gameObject.transform.position);
        return newPos;
    }
    private Quaternion GetPlatformRotation()
    {
        Quaternion newRot;
        newRot = platforms[platforms.Count - 1].gameObject.transform.rotation;
        int rand = UnityEngine.Random.Range(0, 3);
        newRot = new Quaternion(newRot.x, newRot.y, newRot.z,newRot.w);
        return newRot;
    }
}
