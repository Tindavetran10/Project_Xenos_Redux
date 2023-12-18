using _Scripts.Weapons.Components.ComponentData;
using UnityEngine;

namespace _Scripts.Weapons.Components
{
    public class WeaponSprite : WeaponComponent
    {
        private SpriteRenderer _baseSpriteRenderer;
        private SpriteRenderer _weaponSpriteRenderer;
        
        private int _currentWeaponSpriteIndex;

        private WeaponSpriteData _data;

        protected override void HandleEnter()
        {
            base.HandleEnter();
            _currentWeaponSpriteIndex = 0;
        }

        private void HandleBaseSpriteChange(SpriteRenderer spriteRenderer)
        {
            if (!IsAttackActive)
            {
                _weaponSpriteRenderer.sprite = null;
                return;
            }

            var currentAttackSprites = _data.AttackData[Weapon.CurrentAttackCounter].Sprites;

            if (_currentWeaponSpriteIndex >= currentAttackSprites.Length)
            {
                Debug.LogWarning($"{Weapon.name} weapon sprites length mismatch");
                return;
            }
            
            _weaponSpriteRenderer.sprite = currentAttackSprites[_currentWeaponSpriteIndex];

            _currentWeaponSpriteIndex++;
        }

        protected override void Awake()
        {
            base.Awake();

            _baseSpriteRenderer = transform.Find("Base").GetComponent<SpriteRenderer>();
            _weaponSpriteRenderer = transform.Find("WeaponSprite").GetComponent<SpriteRenderer>();

            _data = Weapon.Data.GetData<WeaponSpriteData>();

            /*baseSpriteRenderer = _weapon.BaseGameObject.GetComponent<SpriteRenderer>();
            weaponSpriteRenderer = _weapon.WeaponSpriteGameObject.GetComponent<SpriteRenderer>();*/
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            _baseSpriteRenderer.RegisterSpriteChangeCallback(HandleBaseSpriteChange);

            Weapon.OnEnter += HandleEnter;
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            _baseSpriteRenderer.UnregisterSpriteChangeCallback(HandleBaseSpriteChange);
            
            Weapon.OnEnter -= HandleEnter;
        }
    }
}