using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBob : MonoBehaviour
{
    [Range(0.001f, 0.1f)]
    public float Amount = 0.05f;
    public float frequency = 10.0f;
    [Range(1f, 100f)]
    public float smooth = 10.0f;
    Vector3 StartPos;


    void Start()
    {
        StartPos = transform.localPosition;
       
    }

    void Update()
    {
        float magnitude = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).magnitude;
        if (magnitude > 0)
        {
            HeadBob();
            StopHeadBob();
        }
    }
    private Vector3 HeadBob()
    {
      
            Vector3 pos = Vector3.zero;
            pos.y = Mathf.Lerp(pos.y, Mathf.Sin(Time.time * frequency) * Amount * 1.4f, smooth * Time.deltaTime);
            pos.x = Mathf.Lerp(pos.y, Mathf.Cos(Time.time * frequency / 2f) * Amount * 1.6f, smooth * Time.deltaTime);
            transform.localPosition += pos;

            return pos;
        
    }

    private void StopHeadBob()
    {
        if (transform.localPosition == StartPos) return;
        transform.localPosition = Vector3.Lerp(transform.localPosition, StartPos, 1 * Time.deltaTime);
    }
}
