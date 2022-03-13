using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGenerator : MonoBehaviour
{
    private GameObject enemyPrefab;

    private float delta = 4;

    // Start is called before the first frame update
    void Start()
    {
        enemyPrefab = Prefabs.Load("EnemyPrefab");
    }

    // Update is called once per frame
    void Update()
    {
        delta += Time.deltaTime;

        if (delta > 5)
        {
            delta = 0;
            var newEnemy = Instantiate(enemyPrefab);

            float px = Random.Range(World.Point.Min.x, World.Point.Max.x);
            float y = World.Point.Max.y;
            newEnemy.transform.position = new Vector3(px, y, 0);
        }
    }
}