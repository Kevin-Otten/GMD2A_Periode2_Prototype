﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RouteSwitch : MonoBehaviour {

    public List<GameObject> rightWaypoints = new List<GameObject>();
    public List<GameObject> leftWaypoints = new List<GameObject>();
    public List<GameObject> backWaypoints = new List<GameObject>();

    public string lockedDir;
    public bool goLeft;
    public bool goRight;
    public bool goBack;

    private TextMesh dirText;
    public GameObject wpTrainCameFrom;

    public bool trainPassing;

    void Start () {
        dirText = GetComponentInChildren<TextMesh>();
        if (goRight)
        {
            dirText.text = "Right";
        }
        else if (goLeft)
        {
            dirText.text = "Left";
        }
    }
	
	// Update is called once per frame
	void Update () {
        //UpdateText();

    }

    void OnCOllisionEnter(Collision col)
    {
        if(col.transform.tag == "Rails")
        {
            Rails rail = col.transform.GetComponent<Rails>();

            if (rail.left)
            {
                foreach(GameObject g in rail.myWaypoints)
                {
                    if (!leftWaypoints.Contains(g))
                    {
                        leftWaypoints.Add(g);
                    }
                    
                }
                
            }
            else if (rail.right)
            {
                foreach (GameObject g in rail.myWaypoints)
                {
                    if (!rightWaypoints.Contains(g))
                    {
                        rightWaypoints.Add(g);
                    }

                }
            }
            if (rail.back)
            {
                foreach (GameObject g in rail.myWaypoints)
                {
                    if (!backWaypoints.Contains(g))
                    {
                        backWaypoints.Add(g);
                    }

                }
            }
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.transform.tag == "Train" || other.transform.tag == "AITrain" || other.transform.tag == "PlayerCart" || other.transform.tag == "AICart")
        {
            trainPassing = true;
        }
    }

    void OnTriggerEnter(Collider other)
    {
       
        
        if(other.transform.tag == "Train" || other.transform.tag == "AITrain")
        {
           
            WaypointsNew trainWayPoints = other.GetComponent<WaypointsNew>();

            if (goRight && lockedDir != "Left")
            {
                foreach (GameObject g in rightWaypoints)
                {
                    trainWayPoints.wp.Add(g);
                }
                trainWayPoints.SetNextSwitch();

                lockedDir = "Left";



            }
            else if (goLeft && lockedDir != "Right")
            {
                foreach (GameObject g in leftWaypoints)
                {
                    trainWayPoints.wp.Add(g);
                }
                trainWayPoints.SetNextSwitch();
                lockedDir = "Right";


            }
            else if(lockedDir == "Right")
            {
                if (goLeft)
                {
                    foreach (GameObject g in leftWaypoints)
                    {
                        trainWayPoints.wp.Add(g);
                    }
                    trainWayPoints.SetNextSwitch();
                }
               else if (goBack)
                {
                    foreach (GameObject g in backWaypoints)
                    {
                        trainWayPoints.wp.Add(g);
                    }
                    trainWayPoints.SetNextSwitch();
                    lockedDir = "Back";
                }
                
            }
            else if (lockedDir == "Left")
            {
                if (goRight)
                {
                    foreach (GameObject g in rightWaypoints)
                    {
                        trainWayPoints.wp.Add(g);
                    }
                    trainWayPoints.SetNextSwitch();
                }
                else if (goBack)
                {
                    foreach (GameObject g in backWaypoints)
                    {
                        trainWayPoints.wp.Add(g);
                    }
                    trainWayPoints.SetNextSwitch();

                    lockedDir = "Back";
                }

            }

        }

        }

    void OnTriggerExit(Collider other)
    {
        if(other.transform.tag == "Train" || other.transform.tag == "AITrain" || other.transform.tag == "PlayerCart" || other.transform.tag == "AICart")
        {
            trainPassing = false;
        }
    }

    public void CheckWaypointBeforeSwitch()
    {
        if(wpTrainCameFrom != null)
        {
            if (wpTrainCameFrom == backWaypoints[0] && lockedDir != "Back" && !goRight)
            {
                goLeft = true;
                goRight = false;
                dirText.text = "Left";
                goBack = false;
                lockedDir = "Back";
            }
            else if (wpTrainCameFrom == rightWaypoints[0] && lockedDir != "Right" && !goLeft)
            {
                goBack = true;
                goLeft = false;
                dirText.text = "Back";
                goRight = false;
                lockedDir = "Right";
            }
            else if (wpTrainCameFrom == leftWaypoints[0] && lockedDir != "Left" && !goBack)
            {
                goRight = true;
                goBack = false;
                dirText.text = "Right";
                goLeft = false;
                lockedDir = "Left";
            }
        }
    }
    }
