using UnityEngine;

public enum EHealthSystemType
{
    [InspectorName("First take damage at armor. On armor is 0, take damage on HP")]
    FirstArmor,
    [InspectorName("Take damage at armor and HP")]
    ArmorAndHealth
}