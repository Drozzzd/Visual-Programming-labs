using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    [SerializeField] private float damage = 20f;
    private SpriteRenderer SpriteRend;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        SpriteRend = GetComponent<SpriteRenderer>();
        if (collision.tag == "Character")
        {
            collision.GetComponent<Health>().TakeDamage(damage);
            var color = SpriteRend.color;
            color.a = Mathf.Clamp(color.a + 0.5f, 0, 1);
            SpriteRend.color = color;
        }
    }

}
