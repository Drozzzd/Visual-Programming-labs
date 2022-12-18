using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float _speed;

    private float _lifeTime = 2f; //жизненный цикл пули

    private Vector2 _direction;

    //корутина
    //В ситуациях, когда вы хотите использовать вызов метода,
    //чтобы содержать процедурную анимацию или последовательность событий во времени, вы можете использовать сопрограмму.
    public IEnumerator Init(float speed, float size, Vector2 direction)
    {
        _speed = speed;
        transform.localScale = new Vector3(size, size, 1);
        _direction = direction.normalized;
        while (_lifeTime > 0)
        {
            _lifeTime -= Time.deltaTime; //Time.deltaTime возвращает время в секундах, прошедшее с момента последний кадр завершен.
            transform.position += (Vector3)_direction * (_speed * Time.deltaTime); //движение пули
            yield return null;
        }
        Destroy(gameObject); //уничтожение пули
    }
}