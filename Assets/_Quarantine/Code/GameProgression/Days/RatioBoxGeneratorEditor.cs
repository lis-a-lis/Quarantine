using UnityEditor;
using UnityEngine;

namespace _Quarantine.Code.GameProgression.Days
{
    [CustomEditor(typeof(RatioBoxGenerator))]
    public class RatioBoxGeneratorEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            RatioBoxGenerator generator = (RatioBoxGenerator)target;

            if (GUILayout.Button("Generate Ratio Box"))
            {
                generator.GenerateRatioBoxInspector();
            }
        }
    }
}