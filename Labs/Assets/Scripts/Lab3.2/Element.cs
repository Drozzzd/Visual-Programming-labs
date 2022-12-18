using TMPro;
using UnityEngine;

public class Element : MonoBehaviour
{
    public ElementType elementType;

    //создание объекта
    public void Init(string elemName, ElementType elemType)
    {
        elementType = elemType;
        
        var text = GetComponentInChildren<TMP_Text>();
        text.text = elemName;
    }


    //уничтожение объекта
    public void Destroy()
    {
        SiteBuilder.Instance.RemoveElem(this);
        Destroy(gameObject);
    }
}
