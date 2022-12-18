using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;


// 11. Блок схема
// Описание: Блок схема с текстом внутри
// Сложность: сложная
// Реализовать:
// Возможность поменять текст внутри блок схемы
// Масштабировать блок схему под количество и размер текста
// Возможность связать стрелочкой блок схему с другой блок схемой
public class Block : MonoBehaviour
{
    public static readonly List<Block> Blocks = new();
    private TMP_Text _text;
    private RectTransform _rectTransform;

    private Canvas _canvas;

    private static Block _searchBlock;



    private void Awake()
    {
        Blocks.Add(this);
        _text = GetComponentInChildren<TMP_Text>();
        _rectTransform = GetComponent<RectTransform>();
        UpdateSize();
        _canvas = GetComponentInParent<Canvas>();
    }

    private void UpdateSize()
    {
        _rectTransform.sizeDelta = new Vector2(_rectTransform.sizeDelta.x, _text.preferredHeight + 20);
        _text.rectTransform.sizeDelta = new Vector2(_text.rectTransform.sizeDelta.x, _text.preferredHeight);
    }

    public void SetText(string text)
    {
        _text.text = text;
        UpdateSize();
    }

    // move by mouse
    public void DragHandler(BaseEventData data)
    {
        if (!Input.GetMouseButton(0)) return;
        var pointerData = (PointerEventData)data;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            (RectTransform)_canvas.transform,
            pointerData.position,
            _canvas.worldCamera,
            out var position);
        transform.position = _canvas.transform.TransformPoint(position);
    }
    
}