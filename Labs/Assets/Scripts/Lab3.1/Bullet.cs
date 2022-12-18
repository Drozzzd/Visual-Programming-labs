using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float _speed;

    private float _lifeTime = 2f; //��������� ���� ����

    private Vector2 _direction;

    //��������
    //� ���������, ����� �� ������ ������������ ����� ������,
    //����� ��������� ����������� �������� ��� ������������������ ������� �� �������, �� ������ ������������ �����������.
    public IEnumerator Init(float speed, float size, Vector2 direction)
    {
        _speed = speed;
        transform.localScale = new Vector3(size, size, 1);
        _direction = direction.normalized;
        while (_lifeTime > 0)
        {
            _lifeTime -= Time.deltaTime; //Time.deltaTime ���������� ����� � ��������, ��������� � ������� ��������� ���� ��������.
            transform.position += (Vector3)_direction * (_speed * Time.deltaTime); //�������� ����
            yield return null;
        }
        Destroy(gameObject); //����������� ����
    }
}