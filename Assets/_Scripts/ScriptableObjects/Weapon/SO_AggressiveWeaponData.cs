using _Scripts.Structs;
using UnityEngine;


namespace _Scripts.ScriptableObjects.Weapon
{
    [CreateAssetMenu(fileName ="newAggressiveWeaponData", menuName ="Data/Weapon Data/Aggressive Weapon")]
    public class SoAggressiveWeaponData : SoWeaponData
    {
        [SerializeField] private WeaponAttackDetails[] attackDetails;
        
        public WeaponAttackDetails[] AttackDetails {get => attackDetails; private set => attackDetails = value;}

        private void OnEnable()
        {
            AmountOfAttacks = attackDetails.Length;
            MovementSpeed = new float[AmountOfAttacks];

            for (var i = 0; i < AmountOfAttacks; i++)
                MovementSpeed[i] = attackDetails[i].movementSpeed;
        }
    }
}