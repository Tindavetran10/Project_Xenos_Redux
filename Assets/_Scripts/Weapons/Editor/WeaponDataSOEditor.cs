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
    public class WeaponDataSoEditor : Editor
    {
        private static List<Type> _dataCompTypes = new();
        private WeaponDataSo _dataSo;

        private bool showForceUpdateButtons;
        private bool showAddComponentButtons;

        private void OnEnable()
        {
            _dataSo = target as WeaponDataSo;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (GUILayout.Button("Set Number of Attacks"))
            {
                foreach (var item in _dataSo.ComponentData)
                {
                    item.InitializeAttackData(_dataSo.NumberOfAttacks);
                }
            }


            showAddComponentButtons = EditorGUILayout.Foldout(showAddComponentButtons, "Add Components");

            if (showAddComponentButtons)
            {
                foreach (var comp in from dataCompType in _dataCompTypes 
                         where GUILayout.Button(dataCompType.Name) 
                         select Activator.CreateInstance(dataCompType) as ComponentData)
                {
                    if (comp == null) return;
                
                    comp.InitializeAttackData(_dataSo.NumberOfAttacks);
                    
                    _dataSo.AddData(comp);
                }
            }
            
            showForceUpdateButtons = EditorGUILayout.Foldout(showForceUpdateButtons, "Force Update Components");

            if (showForceUpdateButtons)
            {
                if (GUILayout.Button("Force Update Component Names"))
                {
                    foreach (var item in _dataSo.ComponentData)
                    {
                        item.SetComponentName();
                    }
                }
            
                if (GUILayout.Button("Force Update Attack Names"))
                {
                    foreach (var item in _dataSo.ComponentData)
                    {
                        item.SetAttackDataNames();
                    }
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

            _dataCompTypes = filteredTypes.ToList();
        }
    }
}