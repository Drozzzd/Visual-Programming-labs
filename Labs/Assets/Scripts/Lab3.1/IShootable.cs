// интерейс шутера- ссылочный тип, который может определять некоторый функционал

public interface IShootable
{
    public Bullet BulletPrefab { get; } //префаб пули
    public float BulletSpeed { get; } //скорость пули
    public float BulletSize { get; } //размер пули
    public void Shoot(); //метод стрельбы
}