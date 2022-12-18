using UnityEngine;

public class Tank : MonoBehaviour, IShootable
{
    [SerializeField] private Bullet bulletPrefab;
    [SerializeField] private float bulletSpeed = 50f;
    [SerializeField] private float bulletSize = 0.7f;
    [SerializeField] private float rotationSpeed = 30f;
    [SerializeField] private float movementSpeed = 2f;

    public Bullet BulletPrefab => bulletPrefab;
    public float BulletSpeed => bulletSpeed;
    public float BulletSize => bulletSize;

    private void Update()
    {
        Shoot();
        Move();
    }

    private void Move()
    {
        var deltaspeed = movementSpeed * Time.deltaTime;
        if (Input.GetKey(KeyCode.I))
        {
            Debug.Log("forward");
            transform.Translate(Vector3.up * deltaspeed);
        }
        else if (Input.GetKey(KeyCode.K))
        {
            Debug.Log("backward");
            transform.Translate(Vector3.down * deltaspeed);
        }

        // rotate on J L keys
        var deltaRotation = rotationSpeed * Time.deltaTime;
        if (Input.GetKey(KeyCode.J))
        {
            Debug.Log("Left");
            transform.Rotate(Vector3.forward * deltaRotation);
        }
        else if (Input.GetKey(KeyCode.L))
        {
            Debug.Log("Right");
            transform.Rotate(Vector3.back * deltaRotation);
        }
    }


    public void Shoot()
    {
        if (!Input.GetKeyDown(KeyCode.X)) return;

        var t = transform;
        var direction = t.up;

        var bullet = Instantiate(bulletPrefab, t.position, Quaternion.identity);
        StartCoroutine(bullet.Init(BulletSpeed, BulletSize, direction));
    }
}