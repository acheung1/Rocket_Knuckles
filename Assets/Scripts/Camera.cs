using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{

    private Vector3 offset;
    public Transform playerLocation;

    // Use this for initialization
    void Start()
    {
        offset = this.transform.position - playerLocation.position;

    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = playerLocation.position + offset;
    }
}
