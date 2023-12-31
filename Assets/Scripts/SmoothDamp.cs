using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothDamp : MonoBehaviour
{
    [SerializeField]
    private Transform target;

    public float speed = 10.0f;

    public void LateUpdate()
    {
        Vector3 targetPosition = target.transform.position;
        
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * speed);
        transform.rotation = Quaternion.Euler(target.transform.rotation.eulerAngles);
    }
}
