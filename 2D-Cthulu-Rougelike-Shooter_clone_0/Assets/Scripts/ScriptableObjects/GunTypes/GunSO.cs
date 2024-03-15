using UnityEngine;

[CreateAssetMenu(fileName = "NewGunType", menuName = "Darkend/GunType", order = 0)]
public class GunSO : ScriptableObject
{
    public Sprite gunSprite;
    public string gunName;
    public float range;
    public float size;
    public float speed;
    public float fireRate;
    public float damage;
    public bool canPierce;
    public int pierceCount;
    public string description;
    public int index;
}