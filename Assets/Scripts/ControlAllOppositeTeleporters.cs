using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlAllOppositeTeleporters : MonoBehaviour
{
    [SerializeField] OppositeTeleporterController tA;
    [SerializeField] OppositeTeleporterController tB;
    [SerializeField] OppositeTeleporterController tC;
    [SerializeField] OppositeTeleporterController tD;

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
