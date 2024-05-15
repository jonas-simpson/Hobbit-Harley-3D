using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class lookAtRoadController : MonoBehaviour
{

    [SerializeField] public GameObject leftHalo;
    [SerializeField] public GameObject rightHalo;
    [SerializeField] CameraLookAt cameraLookAt;
    [SerializeField] GameObject cam;
    [SerializeField] GameObject car;
    [SerializeField] Transform carStop;
    [SerializeField] public CarSpeedController myCarSpeedController;
    [SerializeField] MovementControllerScript myMovementController;
    [SerializeField] GameObject wave2;

    private bool leftDone1 = false;
    private bool leftDone2 = false;
    private bool rightDone = false;
    private bool waving = true;

    // Start is called before the first frame update
    void Start()
    {
        this.transform.position = cam.transform.position;
        cameraLookAt.enabled = true;
        myCarSpeedController.currentMovementSpeed = 0;
        myCarSpeedController.gameObject.SetActive(false);
        car.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        //move car towards stop 
        car.transform.position = Vector3.MoveTowards(car.transform.position, carStop.position, 3.0f * Time.deltaTime);
        
        
        if (!leftDone1 && !rightDone)
        {
            car.GetComponentInChildren<AudioSource>().mute = true;
            //look left
            this.transform.position = Vector3.MoveTowards(this.transform.position, leftHalo.transform.position, 3.0f * Time.deltaTime);
        }
        else if (leftDone1 && !rightDone)
        {
            //look right
            this.transform.position = Vector3.MoveTowards(this.transform.position, rightHalo.transform.position, 3.0f * Time.deltaTime);
        }
        else if (leftDone1 && rightDone && !leftDone2)
        {
            //look left
            this.transform.position = Vector3.MoveTowards(this.transform.position, leftHalo.transform.position, 3.0f * Time.deltaTime);
        }
        else if (leftDone2)
        {
            //looking both ways done
            this.transform.position = Vector3.MoveTowards(this.transform.position, car.transform.position + new Vector3(0.0f ,1.0f, 0.0f), 3.0f * Time.deltaTime);
            if (waving)
            {
                PlayableDirector pd3 = wave2.GetComponent<PlayableDirector>();
                pd3.Play();
                StartCoroutine(crossRoad());
                waving = false;
            }
            
        }
        
        if (this.transform.position == leftHalo.transform.position)
        {
            leftDone1 = true;
            if (rightDone)
            {
                leftDone2 = true;
            }
        }
        else if (this.transform.position == rightHalo.transform.position)
        {
            rightDone = true;
        }

        
    }

    IEnumerator crossRoad()
    {
        yield return new WaitForSeconds(4);
        myMovementController.enabled = true;
        myMovementController.startCrossRoad = true;
        cameraLookAt.enabled = false;
        cameraLookAt.gameObject.transform.localEulerAngles = Vector3.zero;
        yield return new WaitForSeconds(5);
        carStop.position = new Vector3(carStop.position.x, carStop.position.y, -40);
    }
}
