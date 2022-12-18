using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float startingHealth = 100f;
    [SerializeField] private float currentHealth;

    Animator anim;

    private void Awake()
    {
        currentHealth = startingHealth;
        anim = GetComponent<Animator>();
    }

    public void TakeDamage(float damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - damage, 0, startingHealth);


        if (currentHealth == 0)
        {
            anim.SetBool("isDead", true);
          //  GetComponent<Character.speed = 0
        }
    }
}
