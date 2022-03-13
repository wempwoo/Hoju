using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBase : Entity
{
    public Seconds Lifespan { get; set; } = new Seconds(3);

    public ArcDegree Dispersion { get; set; } = new ArcDegree(10);

    private Seconds elapsed = new Seconds(0);

    // Start is called before the first frame update
    void Start()
    {
        float dispersionHalf = Dispersion.value / 2;
        ArcDegree directionRev = new ArcDegree(Random.Range(-dispersionHalf, dispersionHalf));
        this.Velocity = ArcDegree.Rotate(this.Velocity, directionRev);
    }

    // Update is called once per frame
    void Update()
    {
        if (this.IsOutside)
        {
            this.Destroy();
            return;
        }

        elapsed += new Seconds(Time.deltaTime);

        if (elapsed > Lifespan)
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
