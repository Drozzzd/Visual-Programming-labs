using System.Collections.Generic;
using System.IO;
using System.Linq;
using TMPro;
using UnityEngine;


public class Explorer : MonoBehaviour
{
    // TextMeshPro — это идеальное текстовое решение для Unity.
    [SerializeField] private TMP_InputField inputField; //поле ввода

    private static List<string> Folders(string path) //список папок
    {
        var info = new DirectoryInfo(path);
        var dirInfo = info.GetDirectories(); //получает список подкаталогов в каталоге path

        return dirInfo.Select(f => f.Name).ToList(); //добавление в список
    }

    private static List<string> Files(string path) //список файлов
    {
        var info = new DirectoryInfo(path);
        var fileInfo = info.GetFiles(); //получает список файлов в каталоге path

        return fileInfo.Select(f => f.Name).ToList(); //добавление в список
    }

    private static void PrintFolders(string path) 
    {
        if (!Directory.Exists(path)) //проверка на существование директории
        {
            Debug.Log("Directory not found");
            return;
        }

        Debug.Log($"Folders: {string.Join(", ", Folders(path))}");
    }

    private static void PrintFiles(string path) 
    {
        if (!Directory.Exists(path)) //проверка на существование директории
        {
            Debug.Log("Directory not found");
            return;
        }

        Debug.Log($"Files: {string.Join(", ", Files(path))}");
    }

    public void PrintAllFolders() => PrintFolders(inputField.text);
    public void PrintAllFiles() => PrintFiles(inputField.text);
}