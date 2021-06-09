using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyPrefab;
    private bool _stopSpawning = false;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnEnemyRoutine());//coroutine is a fn that has the ability to pause execution and return control to unity but then to continue where it left off on the folling frame
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SpawnEnemyRoutine()//IEnumerator allows us to use yield return statement
    {
        yield return new WaitForSeconds(1.5f);
        while(_stopSpawning == false)
        {
            //spawning enemy gameobjects every 5 seconds
            //Instantiate(object,position,rotation);
            //instantiate method is used to clone gameobjects
            Instantiate(_enemyPrefab,new Vector3(Random.Range(-9.08f,9.08f),8,0),Quaternion.identity);//spwaning enemy gameobjects in random position of x
            yield return new WaitForSeconds(1.25f);//waits for 5 seconds
            //yield return null,waits for 1sec
        }
    }

    public void OnPlayerDeath()
    {
        _stopSpawning = true;
    }
}