using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ArrowMovement : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 originalPos = GetComponentInParent<Transform>().position;
        
        float y = Mathf.Sin(Time.time);
  
        transform.position = new Vector3(originalPos.x, 4 + y, originalPos.z);
    }
}
