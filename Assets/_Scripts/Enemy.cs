using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    public float health;
    public int score;
    protected BoundsCheck boundsCheck;

    public void Awake()
    {
        boundsCheck = GetComponent<BoundsCheck>();
    }

    public Vector3 pos
    {
        get { return (this.transform.position); }
        set { this.transform.position = value; }
    }

    private void Update()
    {
        Move();

        if(boundsCheck != null && boundsCheck.offDown) //Checking if enemy leaves the screen
            Destroy(gameObject); //Destroys the enemy once it goes off the screen   
    }

    public virtual void Move()
    {
        Vector3 tempPos = pos;
        tempPos.y -= speed * Time.deltaTime;
        pos = tempPos;
    }

    void OnCollisionEnter(Collision coll) //Destory enemy once a bullet hits them
    {
        GameObject otherGo = coll.gameObject;
        if (otherGo.tag == "ProjectileHero")
        {
            Destroy(otherGo);  //Destory Bullet
            Destroy(gameObject); //Destroy Enemy
        }
        else
            print("Enemy hit by non projectile: " + otherGo.name);
    }
        
}
