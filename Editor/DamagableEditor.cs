using System.Collections.Generic;
using System.Linq;
using Core.Utils;
using Damage;
using Editor.Core;
using UnityEditor;
using UnityEngine;

namespace Editor.Damage
{
    [CustomEditor(typeof(Damagable))]
    public class DamagableEditor : DictBaseEditor<global::Damage.Damage.DamageType, Damagable.Resistance>
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
        
            Damagable damagable = (Damagable) target;
        
            if (target == null) return;

            SerializedProperty dictProperty = serializedObject.FindProperty("resistance");
            
            DrawDict(dictProperty, damagable.resistance);

            serializedObject.ApplyModifiedProperties();
        }

        protected override void DrawValue(SerializedProperty valueProperty)
        {
            EditorGUILayout.PropertyField(valueProperty.FindPropertyRelative(nameof(Damagable.Resistance.invulnerable)));
            EditorGUILayout.PropertyField(valueProperty.FindPropertyRelative(nameof(Damagable.Resistance.value)));
        }
    }
}
