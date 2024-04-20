using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class OppositeTeleporterController : MonoBehaviour
{
    [SerializeField] public Material activeMaterial;
    [SerializeField] private Material hoverMaterial;

    public bool activated = true;
    [SerializeField] ControlAllOppositeTeleporters allOppositeTeleporters;

    [SerializeField] GameObject feedback_text;
    [SerializeField] GameObject instructions;
    [SerializeField] GameObject[] all_texts;

    [SerializeField] Transform playerTransform;
    [SerializeField] MovementControllerScript myMovementController;
    [SerializeField] TestControllerManager testControllerManager;

    AudioSource audioData;

    // Start is called before the first frame update
    void Start()
    {
        audioData = GetComponent<AudioSource>();
        all_texts[0].SetActive(false);
        all_texts[1].SetActive(false);
        all_texts[2].SetActive(false);
        all_texts[3].SetActive(false);
        instructions.SetActive(false);
        activated = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        if (activated)
        {
            audioData.PlayDelayed(0);
            all_texts[0].SetActive(false);
            all_texts[1].SetActive(false);
            all_texts[2].SetActive(false);
            all_texts[3].SetActive(false);
            StartCoroutine(doFeedback());
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
        yield return null;
        feedback_text.SetActive(true);

        //correct answer (C) so teleport to it 
        if (this.name == "TeleportC")
        {
            myMovementController.enabled = false;
            playerTransform.position = transform.position + new Vector3(0.5f, 1.5f, 0f);
            allOppositeTeleporters.choseCorrectly = true;
            yield return new WaitForSeconds(6); //time to read feedback C
            feedback_text.SetActive(false);
            yield return new WaitForSeconds(1);
            testControllerManager.startTest = true;
        }
    }
}
