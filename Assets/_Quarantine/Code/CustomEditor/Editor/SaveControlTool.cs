using UnityEditor;
using UnityEngine;
using System.IO;

public class SaveControlTool : EditorWindow
{
    private static string _filePath = Application.persistentDataPath + "/" + "_save_Quarantine.json"; 

    [MenuItem("Tools/Save Control")]
    private static void Init()
    {
        // Создаем окно (опционально)
        var window = GetWindow<SaveControlTool>();
        window.titleContent = new GUIContent("Save Controller");
        window.Show();
    }

    private void OnGUI()
    {
        GUILayout.Label("Save controlling tool", EditorStyles.boldLabel);

        // Кнопка удаления
        if (GUILayout.Button("Delete File"))
        {
            DeleteFile();
        }
    }

    private static void DeleteFile()
    {
        if (File.Exists(_filePath))
        {
            File.Delete(_filePath);
            //AssetDatabase.Refresh(); // Обновляем Asset Database
            Debug.Log($"File deleted: {_filePath}");
        }
        else
        {
            Debug.LogWarning($"File not found: {_filePath}");
        }
    }
}