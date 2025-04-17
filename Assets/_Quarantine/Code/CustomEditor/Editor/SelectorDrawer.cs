#if UNITY_EDITOR
using System.IO;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(ItemIDSelectorAttribute))]
public class SelectorDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        if (property.propertyType != SerializedPropertyType.String)
        {
            EditorGUI.HelpBox(position, "FileDropdown works only with string fields!", MessageType.Error);
            return;
        }

        var fileDropdownAttribute = (ItemIDSelectorAttribute)attribute;
        var folderPath = fileDropdownAttribute.FolderPath;

        // Получаем список файлов
        string[] filePaths = GetFilesAtPath(folderPath);
        string[] fileNames = GetFileNamesWithoutExtensions(filePaths);

        if (fileNames == null || fileNames.Length == 0)
        {
            EditorGUI.PropertyField(position, property, label);
            EditorGUI.HelpBox(position, $"No files found in: {folderPath}", MessageType.Warning);
            return;
        }

        // Находим текущий выбранный индекс
        int selectedIndex = GetSelectedIndex(fileNames, property.stringValue);
        
        EditorGUI.BeginChangeCheck();
        selectedIndex = EditorGUI.Popup(position, label.text, selectedIndex, fileNames);
        if (EditorGUI.EndChangeCheck())
        {
            property.stringValue = fileNames[selectedIndex];
        }
    }

    private string[] GetFilesAtPath(string folderPath)
    {
        if (!AssetDatabase.IsValidFolder(folderPath))
        {
            Debug.LogError($"Invalid folder path: {folderPath}");
            return null;
        }

        // Ищем все файлы в папке (исключая .meta)
        string[] guids = AssetDatabase.FindAssets("", new[] { folderPath });
        string[] paths = new string[guids.Length];
        
        for (int i = 0; i < guids.Length; i++)
        {
            string path = AssetDatabase.GUIDToAssetPath(guids[i]);
            if (!path.EndsWith(".meta")) // Игнорируем мета-файлы
            {
                paths[i] = path;
            }
        }

        return paths;
    }

    private string[] GetFileNamesWithoutExtensions(string[] filePaths)
    {
        if (filePaths == null) return null;
        
        var result = new System.Collections.Generic.List<string>();
        
        foreach (string path in filePaths)
        {
            if (string.IsNullOrEmpty(path)) continue;
            
            string fileName = Path.GetFileNameWithoutExtension(path);
            if (!string.IsNullOrEmpty(fileName))
            {
                result.Add(fileName);
            }
        }

        return result.ToArray();
    }

    private int GetSelectedIndex(string[] options, string currentValue)
    {
        for (int i = 0; i < options.Length; i++)
        {
            if (options[i] == currentValue)
            {
                return i;
            }
        }
        return 0;
    }
}
#endif