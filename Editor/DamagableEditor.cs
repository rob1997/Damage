using System.Collections.Generic;
using System.Linq;
using Core.Utils;
using Damage;
using UnityEditor;
using UnityEngine;

namespace Editor.Damage
{
    [CustomEditor(typeof(Damagable))]
    public class DamagableEditor : UnityEditor.Editor
    {
        private bool _resistanceFoldOut;

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
        
            Damagable damagable = (Damagable) target;
        
            if (target == null) return;

            _resistanceFoldOut = EditorGUILayout.Foldout(_resistanceFoldOut, nameof(Damagable.Resistance));

            if (!_resistanceFoldOut) return;

            int margin = 10;
        
            GUIStyle marginStyle = new GUIStyle(GUI.skin.label) {margin = new RectOffset(margin, margin, 0, 0)};
            // do whatever you want with this style, e.g.:

            EditorGUILayout.BeginVertical(marginStyle);

            SerializedProperty pairListProperty = serializedObject.FindProperty("resistance").FindPropertyRelative("serializedList");

            foreach (SerializedProperty property in pairListProperty)
            {
                SerializedProperty keyProperty = property.FindPropertyRelative("Key");

                if (keyProperty.enumValueIndex == -1) continue;

                EditorGUILayout.BeginVertical("box");
            
                EditorGUILayout.LabelField(Utils.GetDisplayName($"{(global::Damage.Damage.DamageType) keyProperty.enumValueIndex}"), EditorStyles.boldLabel);
            
                SerializedProperty valueProperty = property.FindPropertyRelative("Value");
            
                EditorGUILayout.PropertyField(valueProperty.FindPropertyRelative(nameof(Damagable.Resistance.invulnerable)));
                EditorGUILayout.PropertyField(valueProperty.FindPropertyRelative(nameof(Damagable.Resistance.value)));
            
                EditorGUILayout.EndVertical();
            }
            
            List<global::Damage.Damage.DamageType> damageTypes = Utils.GetEnumValues<global::Damage.Damage.DamageType>().ToList();
            
            damageTypes.ForEach(d => { if (!damagable.resistance.ContainsKey(d)) damagable.resistance.Add(d, new Damagable.Resistance()); });
        
            damagable.resistance.RemoveAll(pair => !damageTypes.Exists(d => d == pair.Key));
        
            EditorGUILayout.EndVertical();

            serializedObject.ApplyModifiedProperties();
        }
    }
}
