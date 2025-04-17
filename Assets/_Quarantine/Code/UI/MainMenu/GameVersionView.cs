using TMPro;
using UnityEngine;

namespace _Quarantine.Code.UI.MainMenu
{
    public class GameVersionView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _versionText;

        private void Awake()
        {
            _versionText.text = Application.version;
        }
    }
}