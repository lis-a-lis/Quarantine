using System.Collections.Generic;
using UnityEngine;

namespace _Quarantine.Code.UI.HUD.DialogueHUD
{
    [CreateAssetMenu(menuName = "Create MilitaryCourierDialogueConfiguration", fileName = "MilitaryCourierDialogueConfiguration", order = 0)]
    public class MilitaryCourierDialogueConfiguration : ScriptableObject
    {
        [SerializeField] private List<MilitaryCourierPhrasesSequence> _courierPhrases;

        public string GetGreetingPhrase() =>
            GetRandomPhrasesSequence().greetingPhrase;

        public string GetChoicePhrase() =>
            GetRandomPhrasesSequence().phraseAboutChoice;

        public string GetFarewellPhrase() =>
            GetRandomPhrasesSequence().farewellPhrase;

        private MilitaryCourierPhrasesSequence GetRandomPhrasesSequence() =>
            _courierPhrases[UnityEngine.Random.Range(0, _courierPhrases.Count)];
    }
}