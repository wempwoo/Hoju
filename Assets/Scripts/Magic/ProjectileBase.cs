using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBase : Entity
{
    public Lifespan Lifespan { get; set; } = new Lifespan(3);

    private float elapsedSeconds = 0;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (this.IsOutside)
        {
            this.Destroy();
            return;
        }

        elapsedSeconds += Time.deltaTime;

        if (elapsedSeconds > Lifespan.AsSeconds)
        {
            Destroy();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            this.Destroy();

            var enemy = collision.GetComponent<Enemy>();
            enemy.Damaged(30);
        }
    }
}
