using System;
using _Quarantine.Code.Items.Configuration;
using _Quarantine.Code.Items.Implementation;
using UnityEngine;

namespace _Quarantine.Code.Items.DatabaseTable
{
    [Serializable]
    public class ItemDatabaseTableCell
    {
        [SerializeField] private string _name;
        [SerializeField] private string _description;
        [SerializeField] private Sprite _icon;
        [SerializeField] private ItemConfiguration _configuration;
        [SerializeField] private Item _prefab;
        
        public string ID => _configuration.name;
        public string Name => _name;
        public string Description => _description;
        public Sprite Icon => _icon;
        public Item Prefab => _prefab;
        public ItemConfiguration Configuration => _configuration;
    }
}