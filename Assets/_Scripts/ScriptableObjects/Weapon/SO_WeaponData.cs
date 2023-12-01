using UnityEngine;

namespace _Scripts.ScriptableObjects.Weapon
{
    [CreateAssetMenu(fileName = "newWeaponData", menuName = "Data/Weapon Data/Weapon")]
    public class SoWeaponData : ScriptableObject
    {
        protected int AmountOfAttacks { get; set; }
        protected float[] MovementSpeed { get; set; }
    }
}