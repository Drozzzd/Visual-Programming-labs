using UnityEngine;

public class Turret : MonoBehaviour, IShootable
{
    [SerializeField] private float shootDelay = 0.3f;
    [SerializeField] private float shootRange = 10f;

    private float _lastShootTime;

    [SerializeField] private Bullet bulletPrefab;
    [SerializeField] private float bulletSpeed = 5f;
    [SerializeField] private float bulletSize = 0.1f;

    public Bullet BulletPrefab => bulletPrefab;
    public float BulletSpeed => bulletSpeed;
    public float BulletSize => bulletSize;

    private void Update()
    {
        Shoot();
    }

    public void Shoot()
    {
        if (Time.time - _lastShootTime < shootDelay)
            return;
        var t = transform;
        // distance between the turret and the player
        var distance = Vector3.Distance(t.position, Player.Instance.transform.position);
        if (distance > shootRange)
            return;
        var direction = Player.Instance.transform.position - transform.position;
        var bullet = Instantiate(BulletPrefab, t.position, Quaternion.identity);
        StartCoroutine(bullet.Init(BulletSpeed, BulletSize, direction));
        _lastShootTime = Time.time;
    }
}