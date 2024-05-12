using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldUILocation : MonoBehaviour
{

    [SerializeField] Transform playerTransform;
    [SerializeField] TeleportersActive teleporters;

    // Start is called before the first frame update
    void Start()
    {
        this.transform.position = new Vector3(-14.3369999f, 1.28600001f, 58.987999f);
    }

    // Update is called once per frame
    void Update()
    {
        if (playerTransform.position == teleporters.sidewalkTeleporter.transform.position + new Vector3(-0.3f, 1.5f, -0.3f))
        {
            this.transform.position = new Vector3(-2.04900002f, 1.14600003f, 54.2809982f);
            this.transform.rotation = new Quaternion(-0.02183301f, 0.839619637f, 0.0362961665f, 0.541520834f);
            
        }
        else if (playerTransform.position == teleporters.teleporter2.transform.position + new Vector3(-0.3f, 1.5f, -0.3f))
        {
            this.transform.position = new Vector3(-2.08500004f, 1.14600003f, 58.348999f);
            this.transform.rotation = new Quaternion(-0.0423181728f, 0.0113551021f, 0.00180594891f, 0.999038041f);
        } 
        else if (playerTransform.position == teleporters.teleporter3.transform.position + new Vector3(0.2f, 1.5f, 0.0f))
        {
            this.transform.position = new Vector3(0.486999989f, 1.14600003f, 57.3400002f);
            this.transform.rotation = new Quaternion(-0.0361718014f, 0.492896587f, 0.0220384169f, 0.869056344f);
        }
        else if (playerTransform.position == teleporters.teleporter1.transform.position + new Vector3(-0.3f, 1.5f, -0.3f))
        {
            this.transform.position = new Vector3(-2.8269999f, 1.23800004f, 58.348999f);
            this.transform.rotation = new Quaternion(-0.0423181728f, 0.0113551021f, 0.00180594891f, 0.999038041f);
        }
        else
        {
            this.transform.position = playerTransform.position + new Vector3(1.898f, 0f, -0.889f);
            this.transform.rotation = new Quaternion(-0.0164950192f, 0.90760839f, 0.0390128158f, 0.417675734f);
        } 
    }
}
