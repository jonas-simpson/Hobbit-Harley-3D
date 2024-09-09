using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class TeleporterController : MonoBehaviour
{
    [SerializeField] MovementControllerScript myMovementController;
    [SerializeField] CarSpeedController myCarSpeedController;

    [SerializeField] public Transform playerTransform;

    [SerializeField] private GameObject incomingCar;
    [SerializeField] public GameObject hobbit;

    [SerializeField] private GameObject d_car;

    [SerializeField] public Material inactiveMaterial;
    [SerializeField] public Material activeMaterial;
    [SerializeField] private Material hoverMaterial;

    [SerializeField] private bool doesChangeViewpoint;
    public bool activated;
    public bool done;
    public bool readyToContinue;

    [SerializeField] public GameObject feedback_text;
    [SerializeField] public UnityEngine.UI.Image blackScreen;

    AudioSource audioData;

    [SerializeField] XRRayInteractor leftController;
    [SerializeField] XRRayInteractor rightController;

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
        d_car.SetActive(false);

        audioData = GetComponent<AudioSource>();
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
            activated = false;
            audioData.PlayDelayed(0);
            feedback_text.SetActive(false);
            if (this.name != "Teleport4") //don't teleport into the road
            {
                if (this.name == "Teleport3")
                {
                    playerTransform.position = transform.position + new Vector3(0.2f, 1.5f, 0.0f);
                } else
                {
                    playerTransform.position = transform.position + new Vector3(-0.3f, 1.5f, -0.3f);
                }
                StartCoroutine(doFeedback());
            } else
            {
                feedback_text.GetComponentInChildren<Text>().text = "You’re <color=red>too far out in the road</color>! If you wait until this point to look for cars, one could hit you. <color=red>Step back just a little bit</color> so that you are by the nearest headlight of the parked car.";
                feedback_text.SetActive(true);
                StartCoroutine(waitTeleport4());
            }
          
        }
        
    }

    public void teleporterClicked()
    {
        if (activated)
        {
            activated = false;
            audioData.PlayDelayed(0);
            feedback_text.SetActive(false);
            if (this.name != "Teleport4") //don't teleport into the road
            {
                if (this.name == "Teleport3")
                {
                    playerTransform.position = transform.position + new Vector3(0.2f, 1.5f, 0.0f);
                }
                else
                {
                    playerTransform.position = transform.position + new Vector3(-0.3f, 1.5f, -0.3f);
                }
                StartCoroutine(doFeedback());
            }
            else
            {
                feedback_text.GetComponentInChildren<Text>().text = "You’re <color=red>too far out in the road</color>! If you wait until this point to look for cars, one could hit you. <color=red>Step back just a little bit</color> so that you are by the nearest headlight of the parked car.";
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

    public void teleporterHoverEnter()
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

    public void teleporterHoverExit()
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
            leftController.enabled = false;
            rightController.enabled = false;
            //time for voiceovers:
            if (this.name == "Teleport2")
            {
                yield return new WaitForSeconds(12);
            }
            else //teleport 3
            {
                yield return new WaitForSeconds(9);
            }

            StartCoroutine(fadeToBlack());
            yield return new WaitForSeconds(1);
            //change viewport: "stop time and change camera to driver's perspective"
            myCarSpeedController.currentMovementSpeed = 0;
            myCarSpeedController.gameObject.SetActive(false);
            incomingCar.SetActive(true);
            hobbit.GetComponent<Transform>().position = playerTransform.position - new Vector3(-0.2f, 0.4f, -0.3f);
            playerTransform.position = incomingCar.GetComponent<Transform>().position + new Vector3(0.3f, 1.3f, -0.75f);
            playerTransform.rotation = new Quaternion(0, 270, 0, 0);
            hobbit.SetActive(true);
            if (this.name == "Teleport2")
            {
                feedback_text.GetComponentInChildren<Text>().text = "Here is the driver’s view. \nThe driver <color=red>cannot see you clearly</color> due to the parked cars. \nYou should stand away from parked cars to make yourself visible.";
                feedback_text.transform.Find("Background").GetComponent<RectTransform>().localScale = new Vector3(5.01000023f,1.03833818f,1.00250006f);
                yield return new WaitForSeconds(13);

            }
            else //teleport 3
            {
                feedback_text.GetComponentInChildren<Text>().text = "Here is the driver’s view. \nNow the driver can <color=red>see you</color>, and you can <color=red>see the driver</color>.";
                feedback_text.transform.Find("Background").GetComponent<RectTransform>().localScale = new Vector3(4.13999987f,0.850000024f,1.00250006f);
                yield return new WaitForSeconds(8);
            }
            
            StartCoroutine(fadeToBlack());
            yield return new WaitForSeconds(1);
            hobbit.SetActive(false);
            playerTransform.position = hobbit.GetComponent<Transform>().position + new Vector3(0f, 0.4f, 0f);
            playerTransform.rotation = hobbit.GetComponent<Transform>().rotation;
            if (this.name == "Teleport2")
            {
                feedback_text.GetComponent<PlayDingDong>().enabled = false;
                feedback_text.GetComponentInChildren<Text>().text = "Teleport back to the sidewalk when you are ready to continue.";
                feedback_text.transform.Find("Background").GetComponent<RectTransform>().localScale = new Vector3(4.80999994f, 0.560000002f, 1.00250006f);
            }
            myCarSpeedController.gameObject.SetActive(true);
            myCarSpeedController.resetSpeed(true);
            incomingCar.SetActive(false);
            leftController.enabled = true;
            rightController.enabled = true;
            readyToContinue = true;
        }
        else //only show text
        {
            feedback_text.SetActive(true);
            readyToContinue = true;
        }
        activated = true;
    }

    IEnumerator waitTeleport4()
    {
        //show car going over teleport
        myCarSpeedController.currentMovementSpeed = 0;
        myCarSpeedController.gameObject.SetActive(false);
        d_car.SetActive(true);

        yield return new WaitForSeconds(13); //time to read feedback
        feedback_text.SetActive(false);
        myCarSpeedController.gameObject.SetActive(true);
        myCarSpeedController.resetSpeed(true);
        d_car.SetActive(false);
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
