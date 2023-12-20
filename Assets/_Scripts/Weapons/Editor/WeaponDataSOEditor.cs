using System;
using System.Collections.Generic;
using System.Linq;
using _Scripts.ScriptableObjectsScript;
using _Scripts.Weapons.Components;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;

namespace _Scripts.Weapons
{
    [CustomEditor(typeof(WeaponDataSo))]
    public class WeaponDataSOEditor : Editor
    {
        private static List<Type> dataCompTypes = new List<Type>();
        private WeaponDataSo _dataSo;

        private void OnEnable()
        {
            _dataSo = target as WeaponDataSo;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            foreach (var dataCompType in dataCompTypes)
            {
                if (GUILayout.Button(dataCompType.Name))
                {
                    var comp = Activator.CreateInstance(dataCompType) as ComponentData;
                    if (comp == null) return;
                    
                    _dataSo.AddData(comp);
                }
            }
        }

        [DidReloadScripts]
        private static void OnRecompile()
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            var types = assemblies.SelectMany(assembly => assembly.GetTypes());
            var filteredTypes = types.Where(
                type => type.IsSubclassOf(typeof(ComponentData)) && !type.ContainsGenericParameters && type.IsClass
            );

            dataCompTypes = filteredTypes.ToList();
        }
    }
}