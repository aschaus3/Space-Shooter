using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    public GameObject poi;
    public GameObject[] panels;
    public float scrollSpeed = -30f;
    public float motionMult = .25f;

    private float panelHt; //Height of panels
    private float depth; //Depth of panels

    private void Start()
    {
        panelHt = panels[0].transform.localScale.y;
        depth = panels[0].transform.localScale.z;

        panels[0].transform.position = new Vector3(0, 0, depth);
        panels[1].transform.position = new Vector3(0, panelHt, depth);

    }

    private void Update() //Resposible for the moving background
    {
        float tY, tX = 0;
        tY = Time.time * scrollSpeed % panelHt + (panelHt * .5f);

        if (poi != null)
        {
            tX = -poi.transform.position.x * motionMult;
        }
        panels[0].transform.position = new Vector3(tX, tY, depth);
        if(tY > 0)
            panels[1].transform.position = new Vector3(tX, tY-panelHt, depth);
        else
            panels[1].transform.position = new Vector3(tX, tY + panelHt, depth);
    }
}
