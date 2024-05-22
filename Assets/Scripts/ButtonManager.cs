using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ButtonManager : MonoBehaviour
{
    [SerializeField]
    private GameObject welcomeElements;

    [SerializeField]
    private GameObject finalElements;

    [SerializeField]
    Transform playerTransform;

    private Transform lastWayPoint;

    private GameObject UIController;
    private UITaskController myUIController;

    [SerializeField] XRInteractorLineVisual leftController;
    [SerializeField] XRInteractorLineVisual rightController;

    // Start is called before the first frame update
    void Start()
    {
        welcomeElements.SetActive(true);
        finalElements.SetActive(false);
        Time.timeScale = 0f;

        lastWayPoint = GameObject.Find("WayPoint4").transform;

        UIController = GameObject.Find("UI_Checklist");
        myUIController = UIController.GetComponent<UITaskController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerTransform.position == lastWayPoint.position)
        {
            finalElements.SetActive(true);
            leftController.enabled = true;
            rightController.enabled = true;
            myUIController.hideObjectives();
            UIController.SetActive(false);
            Time.timeScale = 0f;
        }
    }

    public void startExperience()
    {
        welcomeElements.SetActive(false);
        leftController.enabled = false;
        rightController.enabled = false;
        Time.timeScale = 1f;
    }

    public void endExperience()
    {
        Application.Quit();
    }
}
