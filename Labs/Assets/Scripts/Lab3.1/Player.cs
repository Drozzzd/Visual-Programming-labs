using UnityEngine;

public class Player : MonoBehaviour, IShootable
{
    public static Player Instance { get; private set; }

    [SerializeField] private float speed = 5f;
    [SerializeField] private Bullet bulletPrefab;
    [SerializeField] private float bulletSpeed = 10f;
    [SerializeField] private float bulletSize = 0.5f;
    private Camera _camera;

    public Bullet BulletPrefab => bulletPrefab;

    public float BulletSpeed => bulletSpeed;

    public float BulletSize => bulletSize;

    private void Awake()
    {
        _camera = Camera.main;
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    private void Update()
    {
        Move();
        Shoot();
    }

    private void Move()
    {
        var horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");
        transform.Translate(new Vector3(horizontal, vertical, 0) * (speed * Time.deltaTime));
    }

    public void Shoot()
    {
        if (!Input.GetMouseButtonDown(0)) return;
        var position = transform.position;
        var direction = _camera.ScreenToWorldPoint(Input.mousePosition) - position;
        var bullet = Instantiate(BulletPrefab, position, Quaternion.identity);
        StartCoroutine(bullet.Init(BulletSpeed, BulletSize, direction));
    }
}