using TMPro;
using UnityEngine;

public class BlockManager : MonoBehaviour
{
    [SerializeField] private TMP_InputField inputField; // TextMeshPro Ч это идеальное текстовое решение дл€ Unity.
    [SerializeField] private TMP_InputField indexField;
    [SerializeField] private Block blockPrefab;
    [SerializeField] private TMP_InputField block1Input;
    [SerializeField] private TMP_InputField block2Input;
    [SerializeField] private Line linePrefab;

    private Canvas _canvas;

    private void Awake()
    {
        _canvas = FindObjectOfType<Canvas>(); // получение корневого узла интерфейса UI, что удобно дл€ создани€ экземпл€ра положени€ панели UI
    }

    //добавление текста
    public void SetText()
    {
        var index = int.Parse(indexField.text); // индекс блока 
        if (index < 0 || index >= Block.Blocks.Count) 
        {
            Debug.LogError("Invalid index");
            return;
        }

        var text = inputField.text; // ѕоле ввода - это способ сделать текст текстового элемента управлени€ доступным дл€ редактировани€
        var block = Block.Blocks[index];
        block.SetText(text);
    }

    //добавление блока
    public void AddBlock()
    {
        var text = inputField.text; 
        var block = Instantiate(blockPrefab, _canvas.transform); // Instantiate создает экземпл€р блока и помещает его в сцену
        block.SetText(text); // добавлени€ текста в блок
    }

    //св€зь блоков линией
    public void LinkBlocks()
    {
        var index1 = int.Parse(block1Input.text); // индексы блоков, которые нужно св€зать
        var index2 = int.Parse(block2Input.text);
        if (index1 < 0 || index1 >= Block.Blocks.Count) // проверка существет ли требуемый индекс
        {
            Debug.LogError("Invalid index");
            return;
        }

        if (index2 < 0 || index2 >= Block.Blocks.Count) // проверка существет ли требуемый индекс
        {
            Debug.LogError("Invalid index");
            return;
        }

        var block1 = Block.Blocks[index1];
        var block2 = Block.Blocks[index2];

        var find = Line.Lines.Find(x => x.block1 == block1 && x.block2 == block2); //поиск элемента, удовлетвор€ющего услови€м указанного предиката
        if (find != null)
        {
            Line.Lines.Remove(find);
            Destroy(find.gameObject);
        }
        else
        {
            var line = Instantiate(linePrefab, _canvas.transform); // Instantiate создает экземпл€р линии и помещает еЄ в сцену
            line.block1 = block1;
            line.block2 = block2;
        }
    }
}