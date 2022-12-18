using System.Collections;
using UnityEngine;
using UnityEngine.Events;


public class ScreamerTrigger : MonoBehaviour
{
    [SerializeField] private GameObject screamerImage; //изображение скримера
    [SerializeField, Range(0.1f, 5f)] private float screamerActiveTime = 3f; //время показа скримера

    // событие OnTrapActivateEvent
    public static event UnityAction OnTrapActivateEvent;

    private void OnEnable()
    {
        OnTrapActivateEvent += ActivateScreamer;
    }

    private void OnDisable()
    {
        OnTrapActivateEvent -= ActivateScreamer;
    }

    //вызывается когда коллайдер персонажа входит в триггер
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Triggered");
        if (collision.gameObject.CompareTag("Player")) OnTrapActivateEvent?.Invoke();
    }

    //вызов скримера
    private void ActivateScreamer()
    {
        StartCoroutine(ShowScreamer());
    }

    // корутина
    // используется чтобы скример был на экране некоторе время
    private IEnumerator ShowScreamer()
    {
        screamerImage.SetActive(true);
        yield return new WaitForSeconds(screamerActiveTime);
        screamerImage.SetActive(false);
    }
}