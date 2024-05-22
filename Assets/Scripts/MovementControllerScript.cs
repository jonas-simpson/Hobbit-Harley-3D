using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.XR.Interaction.Toolkit;

public class MovementControllerScript : MonoBehaviour
{
    [SerializeField] Transform playerTransform;
    [SerializeField] GameObject teleporters;
    [SerializeField] GameObject teleporterDirections;
    [SerializeField] OppositeTeleportersActive oppositeTeleporters;

    [SerializeField] float movementSpeed = 1.0f;

    private int currentTargetPos;

    private List<Transform> WayPoints;

    public bool hasCrossed = false;
    public bool fulfilledTest = false;
    public bool startTest = false;
    public bool startTeleportationTest = false;
    public bool startCrossRoad = false;

    private UITaskController myUIController;

    private bool experienceDone = false;

    [SerializeField] XRInteractorLineVisual leftController;
    [SerializeField] XRInteractorLineVisual rightController;


    // Start is called before the first frame update
    void Start()
    {
        WayPoints = new List<Transform>();
        WayPoints.Add(GameObject.Find("WayPoint2").transform); // 0
        WayPoints.Add(GameObject.Find("WayPoint2.5").transform); // 1
        WayPoints.Add(GameObject.Find("WayPoint3").transform); // 2
        WayPoints.Add(GameObject.Find("WayPoint4").transform); // 3
        WayPoints.Add(GameObject.Find("WayPoint5").transform); // 4
 

        currentTargetPos = 0;

        GameObject UIController = GameObject.Find("UI_Checklist");
        myUIController = UIController.GetComponent<UITaskController>();

    }

    // Update is called once per frame
    void Update()
    {
        if (startCrossRoad)
        {
            movementSpeed = 1.5f;
            currentTargetPos = 2;
            playerTransform.position = Vector3.MoveTowards(playerTransform.position, WayPoints[2].position, movementSpeed * Time.deltaTime);
        } else if (experienceDone)
        {
            //done
            //final text enabled in ButtonManager

        } 
        else if (fulfilledTest)
        {
            oppositeTeleporters.deactivateTeleporters();
            currentTargetPos = 3;
            playerTransform.position = Vector3.MoveTowards(playerTransform.position, WayPoints[3].position, movementSpeed * Time.deltaTime);
            if (WayPoints[3].GetComponent<TimelineController>().doAnimation)
            {
                WayPoints[3].GetComponent<TimelineController>().Timeline.GetComponent<PlayableDirector>().Play();
            } else
            {
                WayPoints[3].GetComponent<TimelineController>().Timeline.GetComponent<PlayableDirector>().Stop();
            }

            if (playerTransform.position == WayPoints[3].position)
            {
                experienceDone = true;
            }
        } 
        else
        {
            playerTransform.position = Vector3.MoveTowards(playerTransform.position, WayPoints[currentTargetPos].position, movementSpeed * Time.deltaTime);

            updateTargetPosition();
        }
    }

    void updateTargetPosition()
    {
        if (playerTransform.position == WayPoints[currentTargetPos].position)
        {
            //first move to sidewalk 
            if (currentTargetPos == 0)
            {
                movementSpeed = 2.5f;
                currentTargetPos++;
            }

            if (currentTargetPos == 1) 
            {
                teleporterDirections.SetActive(true);
                StartCoroutine(waitForBall());
            }

            
            else if (fulfilledTest)
            {
                if (currentTargetPos < WayPoints.Count - 1)
                {
                    currentTargetPos++;
                    fulfilledTest = false;
                }
            }
            else
            {
                startTest = true;
                if (!myUIController.ongoingTest && currentTargetPos != 3 && currentTargetPos < 4)
                {
                    myUIController.showObjectives();
                    myUIController.ongoingTest = true;
                }
            }
            
        }


        
    }


    public void setStartTestFalse()
    {
        startCrossRoad = false;
        startTest = false;
        fulfilledTest = true;
        myUIController.ongoingTest = false;
    }

    public int getWaypointsLength()
    {
        return WayPoints.Count;
    }

    public bool getExperienceDone()
    {
        return experienceDone;
    }

    IEnumerator waitForBall()
    {
        yield return new WaitForSeconds(14);
        teleporterDirections.SetActive(false);
        this.enabled = false;
        teleporters.gameObject.SetActive(true);
        leftController.enabled = true;
        rightController.enabled = true;
    }
}
