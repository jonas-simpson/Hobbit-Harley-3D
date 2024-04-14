using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

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
    [SerializeField] public UnityEngine.UI.Image blackScreen;    

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
                feedback_text.GetComponentInChildren<Text>().text = "You’re too far out in the road! If you wait until this point to look for cars, one could hit you. Step back just a little bit so that you are by the nearest headlight of the parked car.";
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
            StartCoroutine(fadeToBlack());
            yield return new WaitForSeconds(1);
            //change viewport: "stop time and change camera to driver's perspective"
            myCarSpeedController.currentMovementSpeed = 0;
            myCarSpeedController.gameObject.SetActive(false);
            incomingCar.SetActive(true);
            hobbit.GetComponent<Transform>().position = playerTransform.position - new Vector3(-0.3f, 0.4f, -0.3f);
            playerTransform.position = incomingCar.GetComponent<Transform>().position + new Vector3(0.3f, 1.15f, -0.55f);
            playerTransform.rotation = new Quaternion(0, 270, 0, 0);
            hobbit.SetActive(true);
            if (this.name == "Teleport2")
            {
                feedback_text.GetComponentInChildren<Text>().text = "Here is the driver’s view. \nThe driver cannot see you clearly due to the parked cars. \nYou should stand away from parked cars to make yourself visible.";
            } else //teleport 3
            {
                feedback_text.GetComponentInChildren<Text>().text = "Here is the driver’s view. \nNow the driver can see you, and you can see the driver.";
            }
            yield return new WaitForSeconds(10);
            StartCoroutine(fadeToBlack());
            yield return new WaitForSeconds(1);
            feedback_text.SetActive(false);
            hobbit.SetActive(false);
            playerTransform.position = hobbit.GetComponent<Transform>().position + new Vector3(0f, 0.4f, 0f);
            playerTransform.rotation = hobbit.GetComponent<Transform>().rotation;
            if (this.name == "Teleport3")
            {
                feedback_text.GetComponentInChildren<Text>().text = "This is the correct place to stand to cross the road. \nLook left, right, and left again to prepare to cross.";
            } else //teleport 2
            {
                feedback_text.GetComponentInChildren<Text>().text = "Teleport back to the sidewalk when you are ready to continue.";
                feedback_text.transform.Find("Background").GetComponent<RectTransform>().localScale = new Vector3(4.80999994f, 0.560000002f, 1.00250006f);
            }
            feedback_text.SetActive(true);
            myCarSpeedController.gameObject.SetActive(true);
            myCarSpeedController.resetSpeed(true);
            incomingCar.SetActive(false);
            readyToContinue = true;
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

    IEnumerator fadeToBlack()
    {
        for (float i = 0; i <= 1; i += Time.deltaTime)
        {
            // set color with i as alpha
            blackScreen.color = new Color(0, 0, 0, i);
            yield return null;
        }
        for (float i = 1; i >= 0; i -= Time.deltaTime)
        {
            // set color with i as alpha
            blackScreen.color = new Color(0, 0, 0, i);
            yield return null;
        }
    }
}
