using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class ControlAllOppositeTeleporters : MonoBehaviour
{
    [SerializeField] OppositeTeleporterController tA;
    [SerializeField] OppositeTeleporterController tB;
    [SerializeField] OppositeTeleporterController tC;
    [SerializeField] OppositeTeleporterController tD;

    [SerializeField] XRInteractorLineVisual leftController;
    [SerializeField] XRInteractorLineVisual rightController;

    public bool choseCorrectly = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (choseCorrectly)
        {
            leftController.enabled = false;
            rightController.enabled = false;
            tA.activated = false;
            tA.gameObject.SetActive(false);
            tB.activated = false;
            tB.gameObject.SetActive(false);
            tC.activated = false;
            tD.activated = false;
            tD.gameObject.SetActive(false);
        }
    }
}
