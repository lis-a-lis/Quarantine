using UnityEngine;

public class SelectorAttribute : PropertyAttribute
{
    private readonly string _path;

    public SelectorAttribute(string path)
    {
        _path = path;
    }

    public string FolderPath => _path;
}