using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{
    static public Hero S;

    public float speed;
    public float rollMult;
    public float pitchMult;
    public float restartDelay;
    public GameObject projectilePrefab;
    public float projectileSpeed;
    [SerializeField]
    private float _healthLevel = 1;
    //Can be changed directly in Unity

    private GameObject lastTriggerGo = null;

    private void Awake()
    {
        if(S == null)
        {
            S = this;
        }
        else
        {
            Debug.LogError("Hero.Awake() - Attempting to assign a second Hero.S");
        }
    }

    private void Update()
    {
        float xAxis = Input.GetAxis("Horizontal");
        float yAxis = Input.GetAxis("Vertical");

        Vector3 pos = transform.position;
        pos.x += xAxis * speed * Time.deltaTime;
        pos.y += yAxis * speed * Time.deltaTime;
        transform.position = pos;

        transform.rotation = Quaternion.Euler(xAxis * pitchMult, yAxis * rollMult,0);
            //Slightly rotates the ship

        if(Input.GetKeyDown(KeyCode.Space)) //Fire bullet if the space bar is pressed
        {
            TempFire();
        }

        void TempFire() //Code for the bullet
        {
            GameObject projGO = Instantiate<GameObject>(projectilePrefab);
            projGO.transform.position = transform.position;
            Rigidbody rigidB = projGO.GetComponent<Rigidbody>();
            rigidB.velocity = Vector3.up * projectileSpeed;
        }
    }

    private void OnTriggerEnter(Collider other)  //Kills the hero ship if it collides with an enemy
    {
        Transform rootT = other.gameObject.transform.root;
        GameObject go = rootT.gameObject;

        if (go == lastTriggerGo)
            return;

        lastTriggerGo = go;

        if (go.tag == "Enemy")
        {
            healthLevel--;
            Destroy(go);
        }
        else
            print("Triggered by non-Enemy: " + go.name);
    }

    public float healthLevel //Keeps record of the hero health level
    {
        get
        {
            return (_healthLevel);
        }
        set
        {
            _healthLevel = Mathf.Min(value, 4);
            if (value < 1)
            {
                Destroy(this.gameObject);
                Main.S.DelayedRestart(restartDelay); //Calls method in main, restarts the game
            }
 
        }
    }
}
