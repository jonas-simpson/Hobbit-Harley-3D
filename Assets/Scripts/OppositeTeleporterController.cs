using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OppositeTeleporterController : MonoBehaviour
{

    [SerializeField] public Material activeMaterial;
    [SerializeField] private Material hoverMaterial;

    [SerializeField] GameObject feedback_text;
    [SerializeField] GameObject[] all_texts;

    AudioSource audioData;

    // Start is called before the first frame update
    void Start()
    {
        audioData = GetComponent<AudioSource>();
        all_texts[0].SetActive(false);
        all_texts[1].SetActive(false);
        all_texts[2].SetActive(false);
        all_texts[3].SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        audioData.PlayDelayed(0);
        all_texts[0].SetActive(false);
        all_texts[1].SetActive(false);
        all_texts[2].SetActive(false);
        all_texts[3].SetActive(false);
        StartCoroutine(doFeedback());

    }

    private void OnMouseEnter()
    {
        GetComponent<MeshRenderer>().material = hoverMaterial;
    }

    private void OnMouseExit()
    {
        GetComponent<MeshRenderer>().material = activeMaterial;
    }

    IEnumerator doFeedback()
    {
        yield return new WaitForSeconds(1);
        feedback_text.SetActive(true);
    }
}
