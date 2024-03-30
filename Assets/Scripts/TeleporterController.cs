using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TeleporterController : MonoBehaviour
{
    [SerializeField] MovementControllerScript myMovementController;
    [SerializeField] CarSpeedController myCarSpeedController;

    [SerializeField] public Transform playerTransform;

    [SerializeField] private GameObject incomingCar;
    [SerializeField] public GameObject hobbit;

    [SerializeField] public Material inactiveMaterial;
    [SerializeField] public Material activeMaterial;
    [SerializeField] private Material hoverMaterial;

    [SerializeField] private bool doesChangeViewpoint;
    public bool activated;
    public bool done;
    public bool readyToContinue;

    [SerializeField] public GameObject feedback_text;

    // Start is called before the first frame update
    void Start()
    {
        this.activated = false;
        this.done = false;
        this.gameObject.SetActive(false);
        this.feedback_text.SetActive(false);
        GetComponent<MeshRenderer>().material = inactiveMaterial;

        incomingCar.SetActive(false);
        hobbit.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (this.done)
        {
            this.feedback_text.SetActive(false);
        }
    }

    private void OnMouseDown()
    {
        if (activated)
        {
            feedback_text.SetActive(false);
            if (this.name != "Teleport4") //don't teleport into the road
            {
                playerTransform.position = transform.position + new Vector3(-0.3f, 1.5f, -0.3f);
                StartCoroutine(doFeedback());
            } else
            {
                feedback_text.GetComponentInChildren<Text>().text = "This is not safe! Do not stand in the road where no parked cars are blocking you from oncoming traffic.";
                feedback_text.SetActive(true);
                StartCoroutine(waitTeleport4());
            }
          
        }
        
    }

    private void OnMouseEnter()
    {
        if (activated)
        {
            GetComponent<MeshRenderer>().material = hoverMaterial;
        }
    }

    private void OnMouseExit()
    {
        if (activated)
        {
            GetComponent<MeshRenderer>().material = activeMaterial;
        }
    }

    IEnumerator doFeedback()
    {
        yield return new WaitForSeconds(2);
        if (doesChangeViewpoint) //change to car 
        {
            feedback_text.SetActive(true);
            yield return new WaitForSeconds(5);

            //change viewport: "stop time and change camera to driver's perspective"
            myCarSpeedController.currentMovementSpeed = 0;
            myCarSpeedController.gameObject.SetActive(false);
            incomingCar.SetActive(true);
            hobbit.GetComponent<Transform>().position = playerTransform.position - new Vector3(-0.3f, 0.0f, -0.3f);
            playerTransform.position = incomingCar.GetComponent<Transform>().position + new Vector3(0.3f, 1.15f, -0.55f);
            playerTransform.rotation = new Quaternion(0, 270, 0, 0);
            hobbit.SetActive(true);
            if (this.name == "Teleport2")
            {
                feedback_text.GetComponentInChildren<Text>().text = "Here is the driver’s view. The driver cannot see you clearly due to the parked cars. You should stand away from parked cars to make yourself visible.";
            } else //teleport 3
            {
                feedback_text.GetComponentInChildren<Text>().text = "Here is the driver’s view. Now the driver can see you, and you can see the driver.";
            }
            yield return new WaitForSeconds(10);
            feedback_text.SetActive(false);
            hobbit.SetActive(false);
            playerTransform.position = hobbit.GetComponent<Transform>().position;
            playerTransform.rotation = hobbit.GetComponent<Transform>().rotation;
            feedback_text.GetComponentInChildren<Text>().text = "Teleport back to the sidewalk when you are ready to continue.";
            myCarSpeedController.gameObject.SetActive(true);
            myCarSpeedController.resetSpeed(true);
            incomingCar.SetActive(false);
            readyToContinue = true;
            feedback_text.SetActive(true);
        }
        else //only show text
        {
            feedback_text.SetActive(true);
            readyToContinue = true;
        }
    }

    IEnumerator waitTeleport4()
    {
        yield return new WaitForSeconds(10);
        feedback_text.SetActive(false);
        readyToContinue = true;
    }
}
