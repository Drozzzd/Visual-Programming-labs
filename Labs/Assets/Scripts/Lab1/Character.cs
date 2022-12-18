using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    Rigidbody2D rb;
    BoxCollider2D legs;
    Animator anim;

    [SerializeField] private float speed = 2f; // обычная скорость
    [SerializeField] private float fastSpeed = 6f; // скорость при беге
    [SerializeField] private float slowSpeed = 1f; // скорость крадучись
    [SerializeField] private float realSpeed; // текущая скорость

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


    // действия с физикой необходимо обрабатывать в FixedUpdate, а не в Update
    void FixedUpdate()
    {
        TypeOfMovement();
        Move();
        SwitchDirection();
        Jump();
    }

    //Метод передвижения, учитывающий бежит/идёт/крадётся ли объект
    void Move()
    {
        float newX = Input.GetAxis("Horizontal") * realSpeed;
        anim.SetFloat("newX", Mathf.Abs(newX));
        rb.velocity = new Vector2(newX, rb.velocity.y);
    }

    // изменение скорости ходьбы, бега и, когда объект крадётся
    // включение/отключение через bool поле или в зависимости от нажатой клавиши
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
        bool onGround = legs.IsTouchingLayers(ground); //проверяет касается ли коллайдер ног персонажа коллайдера земли
        if (onGround) //проверка для того чтобы персонаж не мог прыгать в воздухе
        {
            if (Input.GetButtonDown("Jump"))
            {
                Vector2 jumpVector = new Vector2(rb.velocity.x, jumpforce);
                rb.velocity = jumpVector;
            }
        }
        anim.SetBool("onGround", onGround);
    }

    //смена направления персонажа
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
