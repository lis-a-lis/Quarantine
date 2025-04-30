using System;
using System.Collections.Generic;
using UnityEngine;
using Cysharp.Threading.Tasks;
using _Quarantine.Code.Items.Implementation;
using _Quarantine.Code.Infrastructure.Services.ItemDatabase;
using _Quarantine.Code.Infrastructure.Services.AssetsManagement;
using DG.Tweening;

namespace _Quarantine.Code.GameProgression.Days.Loot
{
    [CreateAssetMenu(menuName = "Game Progression/RatioBox Loot By Days", fileName = "RatioBox Loot By Days",
        order = 0)]
    public class RatioBoxLootByDays : ScriptableObject
    {
        [SerializeField] [ItemIDSelector] private string _boxItemId;
        [SerializeField] private List<DailyRatioLootList> _ratioBoxLoot = new List<DailyRatioLootList>();

        public string BoxItemId => "Box";

        public string[] GetDailyLootIdList(int day)
        {
            if (day <= 0 || day >= _ratioBoxLoot.Count)
                throw new ArgumentOutOfRangeException();

            string[] ids = new string[_ratioBoxLoot[day].lootList.Count];

            for (int i = 0; i < _ratioBoxLoot[day].lootList.Count; i++)
                ids[i] = _ratioBoxLoot[day].lootList[i].itemID;

            return ids;
        }
    }

    public class ItemsOrderForm : MonoBehaviour
    {
        [SerializeField] private int _maxChosenItemsAmount = 3;

        public void AddItem()
        {
        }
    }

    public class RatioBoxGenerator
    {
        private const string PathToConfig = "Progression/LootByDays";

        private readonly IItemDatabaseService _itemDatabaseService;
        private RatioBoxLootByDays _lootByDays;

        public RatioBoxGenerator(IAssetsProvider assetsProvider, IItemDatabaseService itemDatabaseService)
        {
            _itemDatabaseService = itemDatabaseService;

            //_lootByDays = assetsProvider.LoadScriptableObject<RatioBoxLootByDays>(PathToConfig);
        }

        public void GenerateRatioBox(string[] itemIds, Action<Item> onBoxGenerated)
        {
            RunRatioBoxPhysicalGeneration(itemIds, Vector3.up * 10, onBoxGenerated).Forget();
        }

        private async UniTaskVoid RunRatioBoxPhysicalGeneration(string[] itemIds, Vector3 generationPosition,
            Action<Item> onBoxGenerated)
        {
            Item box = _itemDatabaseService.CreateItemInstance("Box");
            box.GetComponent<Rigidbody>().isKinematic = true;
            box.transform.position = generationPosition;

            foreach (var itemId in itemIds)
            {
                Item generatedItem = _itemDatabaseService.CreateItemInstance(itemId);
                generatedItem.transform.SetParent(box.transform);
                generatedItem.transform.localPosition = Vector3.up * .5f;

                await UniTask.WaitForSeconds(1);

                generatedItem.GetComponent<Rigidbody>().isKinematic = true;
            }

            onBoxGenerated(box);
        }
    }
}