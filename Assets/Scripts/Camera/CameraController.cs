using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public static CameraController instance;
    public Transform target;
    public Vector3 offset;

    private void Awake()
    {
        instance = this;
    }
    public void FixedUpdate()
    {
        if (target == null) return;
        transform.position = Vector3.Lerp(transform.position, target.position + offset, 0.05f);
       
    }
    public void Update()
    {
       transform.rotation=  Quaternion.Slerp(transform.rotation,Quaternion.LookRotation(target.position - transform.position),0.05f);
        
        
    }
}
