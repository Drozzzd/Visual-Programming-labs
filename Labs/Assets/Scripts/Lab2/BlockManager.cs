using TMPro;
using UnityEngine;

public class BlockManager : MonoBehaviour
{
    [SerializeField] private TMP_InputField inputField; // TextMeshPro � ��� ��������� ��������� ������� ��� Unity.
    [SerializeField] private TMP_InputField indexField;
    [SerializeField] private Block blockPrefab;
    [SerializeField] private TMP_InputField block1Input;
    [SerializeField] private TMP_InputField block2Input;
    [SerializeField] private Line linePrefab;

    private Canvas _canvas;

    private void Awake()
    {
        _canvas = FindObjectOfType<Canvas>(); // ��������� ��������� ���� ���������� UI, ��� ������ ��� �������� ���������� ��������� ������ UI
    }

    //���������� ������
    public void SetText()
    {
        var index = int.Parse(indexField.text); // ������ ����� 
        if (index < 0 || index >= Block.Blocks.Count) 
        {
            Debug.LogError("Invalid index");
            return;
        }

        var text = inputField.text; // ���� ����� - ��� ������ ������� ����� ���������� �������� ���������� ��������� ��� ��������������
        var block = Block.Blocks[index];
        block.SetText(text);
    }

    //���������� �����
    public void AddBlock()
    {
        var text = inputField.text; 
        var block = Instantiate(blockPrefab, _canvas.transform); // Instantiate ������� ��������� ����� � �������� ��� � �����
        block.SetText(text); // ���������� ������ � ����
    }

    //����� ������ ������
    public void LinkBlocks()
    {
        var index1 = int.Parse(block1Input.text); // ������� ������, ������� ����� �������
        var index2 = int.Parse(block2Input.text);
        if (index1 < 0 || index1 >= Block.Blocks.Count) // �������� ��������� �� ��������� ������
        {
            Debug.LogError("Invalid index");
            return;
        }

        if (index2 < 0 || index2 >= Block.Blocks.Count) // �������� ��������� �� ��������� ������
        {
            Debug.LogError("Invalid index");
            return;
        }

        var block1 = Block.Blocks[index1];
        var block2 = Block.Blocks[index2];

        var find = Line.Lines.Find(x => x.block1 == block1 && x.block2 == block2); //����� ��������, ���������������� �������� ���������� ���������
        if (find != null)
        {
            Line.Lines.Remove(find);
            Destroy(find.gameObject);
        }
        else
        {
            var line = Instantiate(linePrefab, _canvas.transform); // Instantiate ������� ��������� ����� � �������� � � �����
            line.block1 = block1;
            line.block2 = block2;
        }
    }
}