using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BattleScene
{
    public class BattleDirector : MonoBehaviour
    {
        private readonly Prefab enemyPrefab = new Prefab("BattleScene/EnemyPrefab");

        private float delta = 4;

        // Start is called before the first frame update
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {
            delta += Time.deltaTime;

            if (delta > 2.5f - Random.Range(0, 1.5f))
            {
                delta = 0;
                var newEnemy = enemyPrefab.Instantiate();

                float px = Random.Range(World.Point.Min.x, World.Point.Max.x);
                float y = World.Point.Max.y;
                newEnemy.transform.position = new Vector3(px, y, 0);
            }
        }
    }
}