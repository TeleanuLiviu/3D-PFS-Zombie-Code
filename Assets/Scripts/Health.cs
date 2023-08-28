using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [Header("Health Settings")]
    [SerializeField]
    public int minHealth, maxHealth, currentHealth;
    private Animator _anim;
    public bool died = false;

    private void Start()
    {
        maxHealth = 100;
        minHealth = 1;
        currentHealth = maxHealth;
        _anim = GetComponentInChildren<Animator>();
        if (this.gameObject.tag == "Enemy")
        {
            _anim.SetBool("Died", died);
        }

        if (this.gameObject.tag == "Player")
        {
            UIManager.Instance.UpdateHealth(currentHealth);
        }
    }

    public void Damage (int Damage)
    {
        currentHealth -= Damage;

        if (this.gameObject.tag == "Player")
        {
            UIManager.Instance.UpdateHealth(currentHealth);
        }

        if (currentHealth <minHealth && !died)
        {
            if(this.gameObject.tag=="Enemy")
            {

                died = true;
                _anim.SetBool("Died", died);
                GetComponent<CapsuleCollider>().enabled = false;
                Destroy(this.gameObject, 5.0f);
                Enemy _enemy = GetComponent<Enemy>();
                _enemy._controller.Stop();
                _enemy._controller.velocity = Vector3.zero;
            }

            if(this.gameObject.tag == "Player")
            {
                died = true;
                this.gameObject.SetActive(false);
                UIManager.Instance.DiedCutsceneActivation();
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
           

        }
    }

   
  

}
