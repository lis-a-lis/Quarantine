using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using _Quarantine.Code.UI.Loading;

namespace _Quarantine.Code.Infrastructure.Root.UI
{
    public class UIRoot : MonoBehaviour
    {
        [SerializeField] private Canvas _canvas;
        [SerializeField] private LoadingScreen _loadingScreen;
        
        private readonly Dictionary<string, GameObject> _existGroups = new Dictionary<string, GameObject>();

        public string[] Groups => _existGroups.Keys.ToArray();

        private void Awake()
        {
            HideLoadingScreen();
        }

        public void ShowLoadingScreen()
        {
            _loadingScreen.Show();
        }

        public void HideLoadingScreen()
        {
            _loadingScreen.Hide();
        }
        
        public void Attach(GameObject uiElement)
        {
            AttachToCanvas(uiElement);
            
            _loadingScreen.transform.SetAsLastSibling();
        }

        public void AttachToGroup(GameObject uiElement, string groupName)
        {
            if (!_existGroups.ContainsKey(groupName))
                CreateGroup(groupName);

            AttachToObjectAnchored(uiElement, _existGroups[groupName]);
            
            _loadingScreen.transform.SetAsLastSibling();
        }

        private void CreateGroup(string groupName)
        {
            GameObject group = new GameObject(groupName);
            AttachToCanvas(group);
            group.transform.localPosition = Vector3.zero;
            _existGroups.Add(groupName, group);
        }

        private void AttachToCanvas(GameObject uiElement)
        {
            AttachToObject(uiElement, _canvas.gameObject);
        }
        
        private void AttachToObject(GameObject child, GameObject parent)
        {
            child.transform.SetParent(parent.transform, false);
            
            child.GetComponent<RectTransform>().anchoredPosition = Vector3.zero;
        }
        
        private void AttachToObjectAnchored(GameObject child, GameObject parent)
        {
            child.transform.SetParent(parent.transform, true);
            
            child.GetComponent<RectTransform>().anchoredPosition = Vector3.zero;
        }
    }
}