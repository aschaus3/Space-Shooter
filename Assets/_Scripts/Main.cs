using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Main : MonoBehaviour
{
    public static Main S;
    public GameObject[] prefabEnemies; //Array for enemies
    public float enemiesSpawnPerSecond; //The number of enemy per second
    public float enemyDefaultPadding; //For position

    private BoundsCheck boundCheck;

    private void Awake()
    {
        S = this;
        boundCheck = GetComponent<BoundsCheck>();
        Invoke("Spawn", 1f / enemiesSpawnPerSecond);
    }

    public void Spawn()
    {
        int choose = Random.Range(0, prefabEnemies.Length);
        GameObject go = Instantiate<GameObject>(prefabEnemies[choose]);
            //Picks a random enemy and then instantiates it

        float enemyPadding = enemyDefaultPadding;
        if (go.GetComponent<BoundsCheck>() != null)
            enemyPadding = Mathf.Abs(go.GetComponent<BoundsCheck>().radius);
            //Randomly choose an x postition for the new enemy

        Vector3 pos = Vector3.zero;
        float xMin = -boundCheck.camWidth + enemyPadding;
        float xMax = boundCheck.camWidth - enemyPadding;
        pos.x = Random.Range(xMin, xMax);
        pos.y = boundCheck.camHeight + enemyPadding;
        go.transform.position = pos;

        //Invokes Spawn again so enemies keep spawning
        Invoke("Spawn", 1f / enemiesSpawnPerSecond);
    }

    public void DelayedRestart(float delay)
    {
        Invoke("Restart", delay);
    }

    public void Restart() //Resets the game
    {
        SceneManager.LoadScene("SampleScene");
    }

}
