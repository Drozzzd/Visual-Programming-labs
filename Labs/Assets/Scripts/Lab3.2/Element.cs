using TMPro;
using UnityEngine;

public class Element : MonoBehaviour
{
    public ElementType elementType;

    //�������� �������
    public void Init(string elemName, ElementType elemType)
    {
        elementType = elemType;
        
        var text = GetComponentInChildren<TMP_Text>();
        text.text = elemName;
    }


    //����������� �������
    public void Destroy()
    {
        SiteBuilder.Instance.RemoveElem(this);
        Destroy(gameObject);
    }
}
