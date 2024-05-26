using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DCarMovement : MonoBehaviour
{

    [SerializeField] Transform carStop;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = Vector3.MoveTowards(this.transform.position, carStop.position, 3.0f * Time.deltaTime);
    }
}
