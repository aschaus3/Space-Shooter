using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_2 : Enemy  //Extends Enemy Class
{
    public float waveFrequency;
    public float waveWidth;
    public float waveRotY;

    private float xO;
    private float birthTime;

    private void Start()
    {
        xO = pos.x;
        birthTime = Time.time;
    }

    public override void Move() //Moves the second enemy left and right
    {
        Vector3 tempPos = pos;
        float age = Time.time - birthTime;
        float theta = Mathf.PI * 2 * age / waveFrequency;
        float sin = Mathf.Sin(theta);
        tempPos.x = xO + waveWidth * sin;
        pos = tempPos;

        //Rotate about y
        Vector3 rot = new Vector3(0, sin * waveRotY, 0);
        this.transform.rotation = Quaternion.Euler(rot);

        //Handles the y movments since nothing was changed 
        base.Move();
    }
}
