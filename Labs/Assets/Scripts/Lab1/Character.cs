using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    Rigidbody2D rb;
    BoxCollider2D legs;
    Animator anim;

    [SerializeField] private float speed = 2f; // ������� ��������
    [SerializeField] private float fastSpeed = 6f; // �������� ��� ����
    [SerializeField] private float slowSpeed = 1f; // �������� ���������
    [SerializeField] private float realSpeed; // ������� ��������

    [SerializeField] private bool isRun;
    [SerializeField] private bool isSneak;

    [SerializeField] private float jumpforce = 10f;

    [SerializeField] LayerMask ground;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        legs = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
        realSpeed = speed;
    }


    // �������� � ������� ���������� ������������ � FixedUpdate, � �� � Update
    void FixedUpdate()
    {
        TypeOfMovement();
        Move();
        SwitchDirection();
        Jump();
    }

    //����� ������������, ����������� �����/���/������� �� ������
    void Move()
    {
        float newX = Input.GetAxis("Horizontal") * realSpeed;
        anim.SetFloat("newX", Mathf.Abs(newX));
        rb.velocity = new Vector2(newX, rb.velocity.y);
    }

    // ��������� �������� ������, ���� �, ����� ������ �������
    // ���������/���������� ����� bool ���� ��� � ����������� �� ������� �������
    void TypeOfMovement()
    {
        if (Input.GetKey(KeyCode.LeftShift) || isRun)
        {
            realSpeed = fastSpeed;
        }
        else if (Input.GetKey(KeyCode.LeftControl) || isSneak)
        {
            realSpeed = slowSpeed;
        }
        else
        {
            realSpeed = speed;
        }

    }

    void Jump()
    {
        bool onGround = legs.IsTouchingLayers(ground); //��������� �������� �� ��������� ��� ��������� ���������� �����
        if (onGround) //�������� ��� ���� ����� �������� �� ��� ������� � �������
        {
            if (Input.GetButtonDown("Jump"))
            {
                Vector2 jumpVector = new Vector2(rb.velocity.x, jumpforce);
                rb.velocity = jumpVector;
            }
        }
        anim.SetBool("onGround", onGround);
    }

    //����� ����������� ���������
    void SwitchDirection()
    {
        if (rb.velocity.x > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }

        if (rb.velocity.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

}
