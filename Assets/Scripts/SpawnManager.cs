using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public List<GameObject> enemyList = new List<GameObject>();
    public float spawnInterval = 5f;
    public float rangeX = 15;
    public float rangeZ = 6;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnEnemy());
    }

    IEnumerator SpawnEnemy()
    {
        yield return new WaitForSeconds(spawnInterval);
        int randomIndex = Random.Range(0, enemyList.Count);
        GameObject enemy = Instantiate(enemyList[randomIndex], GenerateRandomPos(), enemyList[randomIndex].transform.rotation);
        enemy.transform.SetParent(transform);
        
        StartCoroutine(SpawnEnemy());
    }

    Vector3 GenerateRandomPos()
    {
        Vector3 pos = new Vector3(0,0,0);
        pos.x += Random.Range(-rangeX, rangeX);
        pos.z += Random.Range(-rangeZ, rangeZ);
        return pos;
    }
}
