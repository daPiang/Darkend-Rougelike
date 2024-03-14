using UnityEngine;

[CreateAssetMenu(fileName = "NewGunType", menuName = "Darkend/GunType", order = 0)]
public class GunSO : ScriptableObject
{
    public Sprite gunSprite;
    public string gunName;
    public float range;
    public float fireRate;
    public float damage;
    public string description;
}