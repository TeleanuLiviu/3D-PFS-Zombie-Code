using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : MonoBehaviour
{
    public WeaponSystem weapon;
    public Animator _anim;
    private void Start()
    {
        GameObject weaponObj = GameObject.Find("Player/Main Camera (1)/WeaponHolder");
        weapon = weaponObj.GetComponent<WeaponSystem>();
        _anim = GetComponentInParent<Animator>();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            weapon.Knife(other);
        }
    }

    private void Update()
    {
        if(transform.parent.gameObject.activeSelf && Input.GetMouseButton(0))
        {
            _anim.SetBool("Knife", true);
        }
        else
        {
            _anim.SetBool("Knife", false);
        }
    }


}
