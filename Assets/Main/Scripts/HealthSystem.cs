using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour, IHitable
{
    [SerializeField] EHealthSystemType _healthSystemType = EHealthSystemType.FirstArmor;
    [SerializeField] int _health;
    [SerializeField] int _armor;
    [SerializeField] bool _enableDamage = true;
    [SerializeField] bool _enableArmor = true;

    public int Health => _health;

    public int Armor => _armor;

    public bool EnableDamage => _enableDamage;

    public bool EnableArmor => _enableArmor;

    public void Hit(float damage)
    {
        if (EnableDamage)
        {
            switch (_healthSystemType)
            {
                case EHealthSystemType.FirstArmor:

                    break;
                case EHealthSystemType.ArmorAndHealth:
                    
                    break;
                default:
                    return;
            }
        }
    }
}
