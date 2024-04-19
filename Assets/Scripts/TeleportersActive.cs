using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TeleportersActive : MonoBehaviour
{
    [SerializeField] public GameObject teleporter1;
    [SerializeField] public GameObject teleporter2;
    [SerializeField] public GameObject teleporter3;
    [SerializeField] public GameObject teleporter4;
    [SerializeField] public GameObject sidewalkTeleporter;

    private TeleporterController teleporterController1;
    private TeleporterController teleporterController2;
    private TeleporterController teleporterController3;
    private TeleporterController teleporterController4;
    private TeleporterController teleporterControllerOG;

    [SerializeField] public GameObject instructions;
    [SerializeField] Camera playerCamera;
    [SerializeField] GameObject leftHalo;
    [SerializeField] GameObject rightHalo;
    [SerializeField] GameObject lookTarget;

    public bool atSidewalk;
    private bool materialChanged;
    private bool materialChanged2;
    private bool allDone;

    // Start is called before the first frame update
    void Start()
    {
        atSidewalk = false;
        materialChanged = false;
        materialChanged2 = false;
        allDone = false;
        teleporterController1 = teleporter1.GetComponent<TeleporterController>();
        teleporterController2 = teleporter2.GetComponent<TeleporterController>();
        teleporterController3 = teleporter3.GetComponent<TeleporterController>();
        teleporterController4 = teleporter4.GetComponent<TeleporterController>();
        teleporterControllerOG = sidewalkTeleporter.GetComponent<TeleporterController>();
        this.gameObject.SetActive(false);
        teleporter1.SetActive(true);
        teleporterController1.activated = true;
        teleporter1.GetComponent<MeshRenderer>().material = teleporterController1.activeMaterial;

        sidewalkTeleporter.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
        foreach(Transform child in transform)
        {
            child.gameObject.SetActive(true);
        }
       

        if (teleporterControllerOG.playerTransform.position == sidewalkTeleporter.transform.position + new Vector3(-0.3f, 1.5f, -0.3f))
        {
            atSidewalk = true;

        } else
        {
            atSidewalk = false;
        }

        if (atSidewalk)
        {
            teleporterController1.feedback_text.SetActive(false);
            teleporterController2.feedback_text.SetActive(false);
            teleporterController3.feedback_text.SetActive(false);
        
        } else
        {
            teleporterControllerOG.feedback_text.SetActive(false);
        }

        if (teleporterController1.done && teleporterController2.done && teleporterController3.done && teleporterController4.done)
        {
            allDone = true;
        }

        //prevent user from teleporting when viewpoint changes
        if (teleporterController3.hobbit.activeSelf)
        {
            teleporterController3.activated = false;
        }
        else if (!teleporterController3.hobbit.activeSelf && teleporterController3.readyToContinue)
        {
            teleporterController3.activated = true;
        }

        if (teleporterController1.activated && teleporterController1.readyToContinue)
        {
            teleporterControllerOG.activated = true;
            if (!materialChanged)
            {
                //to ensure the OnMouseEnter (hover) function works correctly
                teleporterControllerOG.GetComponent<MeshRenderer>().material = teleporterControllerOG.activeMaterial;
                materialChanged = true;
            } 
            if (atSidewalk)
            {
                teleporterController1.done = true;
                teleporterController1.GetComponent<MeshRenderer>().material = teleporterController1.inactiveMaterial;

                teleporterController2.activated = true;
                if (!materialChanged2)
                {
                    teleporterController2.GetComponent<MeshRenderer>().material = teleporterController2.activeMaterial;
                    teleporterControllerOG.activated = false;
                    materialChanged2 = true;
                }
                teleporterController1.readyToContinue = false;
                teleporterController1.activated = false;
     
            } 
        }

        else if (teleporterController2.activated && teleporterController2.readyToContinue)
        {
            teleporterControllerOG.activated = true;
            materialChanged = false;
            if (atSidewalk)
            {
                teleporterController2.done = true;
                teleporterController2.GetComponent<MeshRenderer>().material = teleporterController2.inactiveMaterial;

                teleporterController4.activated = true;
                if (!materialChanged)
                {
                    teleporterController4.GetComponent<MeshRenderer>().material = teleporterController4.activeMaterial;
                    materialChanged = true;
                }
                teleporterController2.readyToContinue = false;
                teleporterController2.activated = false;

            }
        }

        else if (teleporterController4.activated && teleporterController4.readyToContinue)
        {
            materialChanged = false;
            teleporterController4.done = true;
            teleporterController4.GetComponent<MeshRenderer>().material = teleporterController4.inactiveMaterial;

            teleporterController3.activated = true;
            if (!materialChanged)
            {
                teleporterController3.GetComponent<MeshRenderer>().material = teleporterController3.activeMaterial;
                teleporterControllerOG.activated = false;
                teleporterControllerOG.feedback_text.SetActive(true);
                materialChanged = true;
            }
            teleporterController4.readyToContinue = false;
            teleporterController4.activated = false;
        }

        else if (teleporterController3.activated && teleporterController3.readyToContinue)
        {
            teleporterController3.GetComponent<MeshRenderer>().material = teleporterController3.inactiveMaterial;
            teleporterController3.activated = false;
            teleporterControllerOG.GetComponent<MeshRenderer>().material = teleporterControllerOG.inactiveMaterial;
            teleporterControllerOG.activated = false;
            teleporterController3.readyToContinue = false;
            teleporterController3.done = true;
        }

        else if (allDone)
        {
            //look left, look right, look left
            instructions.GetComponentInChildren<Text>().text = "Now, look left, right, and left again to check for cars. \nBe sure to make eye contact and wave to the driver before crossing!";
            instructions.SetActive(true);
            leftHalo.SetActive(true);
            rightHalo.SetActive(true);
            lookTarget.SetActive(true);
        }
       
    }

}
