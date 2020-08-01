using UnityEngine;

[CreateAssetMenu()]
public class WeaponStats: ScriptableObject
{
    public float coolDownRate;
    public float damage;
    public GameObject bulletPrefab;
}