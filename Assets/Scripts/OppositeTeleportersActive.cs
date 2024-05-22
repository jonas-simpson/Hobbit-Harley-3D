using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class OppositeTeleportersActive : MonoBehaviour
{
    [SerializeField] GameObject courtSideTeleporters;
    TeleportersActive courtSideTeleportersActive;
    [SerializeField] lookAtRoadController myLookAtRoadController;
    [SerializeField] GameObject myLeftCarController;

    [SerializeField] GameObject instructions;

    [SerializeField] GameObject teleporterA;
    [SerializeField] GameObject teleporterB;
    [SerializeField] GameObject teleporterC;
    [SerializeField] GameObject teleporterD;

    

    // Start is called before the first frame update
    void Start()
    {
        courtSideTeleportersActive = courtSideTeleporters.GetComponent<TeleportersActive>();
        teleporterA.SetActive(false);
        teleporterB.SetActive(false);
        teleporterC.SetActive(false);
        teleporterD.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        courtSideTeleporters.gameObject.SetActive(false);
        courtSideTeleportersActive.instructions.SetActive(false);
        myLookAtRoadController.leftHalo.SetActive(false);
        myLookAtRoadController.rightHalo.SetActive(false);

        myLeftCarController.SetActive(true);
        instructions.SetActive(true);
        StartCoroutine(activateTeleporters());


    }

    IEnumerator activateTeleporters()
    {
        yield return new WaitForSeconds(8);
        instructions.SetActive(false);
        teleporterA.SetActive(true);
        teleporterB.SetActive(true);
        teleporterC.SetActive(true);
        teleporterD.SetActive(true);
        this.gameObject.SetActive(false);
    }

    public void deactivateTeleporters()
    {
        teleporterA.SetActive(false);
        teleporterB.SetActive(false);
        teleporterC.SetActive(false);
        teleporterD.SetActive(false);
    }
}
