using System.Collections.Generic;
using UnityEngine;

namespace _Quarantine.Code.Items.DatabaseTable
{
    [CreateAssetMenu(fileName = "Item Database Table", menuName = "Items/Item Database Table", order = 1)]
    public class ItemDatabaseTable : ScriptableObject
    {
        [SerializeField] private List<ItemDatabaseTableCell> _cells = new List<ItemDatabaseTableCell>();
        
        public List<ItemDatabaseTableCell> Cells => _cells;
    }
}