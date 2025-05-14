using System;
using System.Collections.Generic;
using _Quarantine.Code.GameEntities;
using _Quarantine.Code.GameProgression.Days;
using _Quarantine.Code.Infrastructure.Root.UI;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Quarantine.Code.UI.HUD.DialogueHUD
{
    public class MilitaryCourierDialoguePresenter : MonoBehaviour
    {
        [SerializeField] private MilitaryCourierDialogueConfiguration _configuration;
        [SerializeField] private DialogueWheel _dialogueWheel;
        [ItemIDSelector] [SerializeField] private List<string> _dialogues;

        [SerializeField] private GameObject _courierPrefab;
        [SerializeField] private Transform _courierSpawnPoint;

        private bool _spawnActive = true;
        private bool _courierShowed = false;
        private RatioBoxGenerator _generator;
        private DialogueWheel _dialogueWheelInstance;
        private GameObject _courier;

        private void OnTriggerEnter(Collider other)
        {
            if (!_courierShowed)
                return;

            if (other.TryGetComponent<PlayerEntity>(out var player))
                StartDialogue();
        }

        private void Start()
        {
            UIRoot root = FindFirstObjectByType<UIRoot>();

            _generator = FindFirstObjectByType<RatioBoxGenerator>();
            
            _dialogueWheelInstance = Instantiate<DialogueWheel>(_dialogueWheel);
            
            root.Attach(_dialogueWheelInstance.gameObject);
            
            
            SpawnCourier().Forget();
        }

        private void OnDestroy()
        {
            _spawnActive = false;
        }

        public void StartDialogue()
        {
            _courierShowed = true;
            _dialogueWheelInstance.ShowWheel(_dialogues.ToArray(), 7);
            _dialogueWheelInstance.DialogueFinished += StopDialogue;
        }

        private void StopDialogue(string[] selected)
        {
            _courierShowed = false;

            _dialogueWheelInstance.DialogueFinished -= StopDialogue;
            Destroy(_courier.gameObject);
            _generator.GenerateRatioBox(selected, transform.position + -transform.forward);
        }

        private async UniTaskVoid SpawnCourier()
        {
            while (_spawnActive)
            {

                if (_courierShowed)
                {
                    await UniTask.Yield();
                    continue;
                }

                await UniTask.WaitForSeconds(10);

                _courier = Instantiate(_courierPrefab);

                _courier.transform.position = _courierSpawnPoint.position;


                _courierShowed = true;
            }
        }
    }
}