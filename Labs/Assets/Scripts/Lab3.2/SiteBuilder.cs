using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Site
{
    private readonly List<GameObject> _components = new(); //������ �����������

    //���������� ���������� � ������
    public void AddComponent(GameObject component)
    {
        if (!_components.Contains(component))
            _components.Add(component);
    }
    
    //����� ������� �����
    public void Show()
    {
        foreach (var component in _components) component.SetActive(true);
    }
    

    //�������� ������� �����
    public void Hide()
    {
        foreach (var component in _components) component.SetActive(false);
    }
}

//Builder - ������ ��������������, ������� ������������� �������� ������� � ��������� ��������� ��� �� ��������� �����.
//�����������
public class SiteBuilder : MonoBehaviour
{
    public static SiteBuilder Instance;

    [SerializeField] private TMP_Dropdown dropdown; //�������� �������, ����� ������� ���������� ������ ����� ���������� � ��������
    [SerializeField] private GameObject content;
    [SerializeField] private Element elementPrefab;

    [Header("Components")] [SerializeField]
    private GameObject mainPage;

    [SerializeField] private GameObject registrationPage;
    [SerializeField] private GameObject faqPage;
    [SerializeField] private GameObject aboutPage;
    [SerializeField] private GameObject forumPage;
    [SerializeField] private GameObject reviewsPage;
    [SerializeField] private GameObject descriptionPage;
    [SerializeField] private GameObject documentationPage;

    private readonly Dictionary<ElementType, GameObject> _components = new();

    private readonly List<Element> _elements = new();

    public float elementHeight = 60f;
    public float offset = 10f;

    private float _currentOffset;
    
    private Site _site;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        _components.Add(ElementType.Main, mainPage);
        _components.Add(ElementType.Registration, registrationPage);
        _components.Add(ElementType.About, aboutPage);
        _components.Add(ElementType.Faq, faqPage);
        _components.Add(ElementType.Forum, forumPage);
        _components.Add(ElementType.Reviews, reviewsPage);
        _components.Add(ElementType.Description, descriptionPage);
        _components.Add(ElementType.Documentation, documentationPage);
        _currentOffset = offset + elementHeight / 2;
    }

    //���������� �������
    public void AddElem()
    {
        var index = dropdown.value;
        var type = (ElementType)index;
        var text = dropdown.options[index].text;

        //Instantiate ��������� ������ original � ��������� �������� ��������� (position) � ������������ �������� (rotation).
        //���������� ������� ����� �� �����, ����� ����� ��� ���������
        var element = Instantiate(elementPrefab, content.transform);
        element.Init(text, type);
        _elements.Add(element);

        var elemRect = element.GetComponent<RectTransform>();
        elemRect.anchoredPosition = new Vector2(0, -_currentOffset);
        _currentOffset += elementHeight + offset; //������

        // ��������� �������
        var rectTransform = content.GetComponent<RectTransform>();
        var size = rectTransform.sizeDelta;
        size.y = _currentOffset;
        rectTransform.sizeDelta = size;
    }

    //�������� �������
    public void RemoveElem(Element element)
    {
        // SiteBuilder.Elements.Remove(this);
        var index = _elements.IndexOf(element);
        _elements.RemoveAt(index);
        for (var i = index; i < _elements.Count; i++)
            _elements[i].transform.position += new Vector3(0, offset + elementHeight, 0);
        _currentOffset -= elementHeight + offset;
    }
    
    //���������� � ����� �� ������ 
    public void BuildAndShow()
    {
        _site?.Hide();
        _site = Build();
        _site.Show();
    }

    //�������� ������� "����"
    private Site Build()
    {
        _site = new Site();
        foreach (var element in _elements)
            _site.AddComponent(_components[element.elementType]);
        return _site;
    }
}