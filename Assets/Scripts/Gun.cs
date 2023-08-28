using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TypeofGun { Primary, Secondary, Knife };
[CreateAssetMenu(fileName ="Gun", menuName = "Gun")]

public class Gun : ScriptableObject
{
    public TypeofGun Type;
    [Range(10,20)]
    public int damage;
    [Range(5, 30)]
    public float fireRate;
    public GameObject Model;
    public bool _knife;
    public Sprite GunImage;
    [Range(10, 40)]
    public int _maxAmmo;
    [Range(0, 40)]
    public int _currentAmmo;
    [Range(30, 120)]
    public int _totalAmmo;
    [Range(0, 2)]
    public float WaitSwitch;
    

}
