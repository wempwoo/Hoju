using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 魔法による投射物
/// </summary>
public class ProjectileBase : Entity
{
    /// <summary>
    /// 投射物の寿命
    /// </summary>
    public Seconds Lifespan { get; set; } = new Seconds(3);

    /// <summary>
    /// 投射角度の分散（例えば10度なら±5度）
    /// </summary>
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
