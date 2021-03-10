using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private BoundsCheck bndCheck;

    private void Awake()
    {
        bndCheck = GetComponent<BoundsCheck>();
    }

    private void Update() //Checks if the bullet leaves the top of the screen
    {
        if (bndCheck.offUp)
            Destroy(gameObject);
    }
}
