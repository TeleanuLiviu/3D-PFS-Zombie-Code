using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSystem : MonoBehaviour
{

    public int selectedWeapon = 0;
    [SerializeField] private Gun[] scriptableObjects;
    [SerializeField] private Gun[] currentGunsProperties;
    [SerializeField] private GameObject[] currentGuns;

    [SerializeField]
    private GameObject[] Blood;
    public float fireRate;
    public int damage;
    public float nextShoot;
    [SerializeField]
    private Player _player;
    public bool _knife = false;
    public int _maxAmmoPrimary , _maxAmmoSecondary;
    public int _currentAmmoPrimary, _currentAmmoSecondary;
    public int _totalAmmoPrimary, _totalAmmoSecondary;
    public float wait,reloadtime;
    public TypeofGun Type;
    [SerializeField]
    public AudioSource _audio;
    void Start()
    {
        _audio = GetComponent<AudioSource>();

        SelectedWeapon();
        if (currentGunsProperties[0]!=null)
        {
            _currentAmmoSecondary = currentGunsProperties[0]._currentAmmo;
            _maxAmmoSecondary = currentGunsProperties[0]._maxAmmo;
            _totalAmmoSecondary = currentGunsProperties[0]._totalAmmo;
            UIManager.Instance.UpdateAmmoText(_currentAmmoSecondary, _totalAmmoSecondary);
           
        }
        if (currentGunsProperties[2] != null)
        {
            _currentAmmoPrimary = currentGunsProperties[2]._currentAmmo;
            _maxAmmoPrimary = currentGunsProperties[2]._maxAmmo;
            _totalAmmoPrimary = currentGunsProperties[2]._totalAmmo;
            UIManager.Instance.UpdateAmmoText(_currentAmmoPrimary, _totalAmmoPrimary);
        }
        currentGuns[2] = new GameObject();
    }

    // Update is called once per frame
    void Update()
    {
        int previousSelection = selectedWeapon;

        if(Input.GetAxis("Mouse ScrollWheel")>0)
        {
            if (selectedWeapon >= transform.childCount - 1)
                selectedWeapon = 0;
            else
                selectedWeapon++;
        }

        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            if (selectedWeapon <= 0)
                selectedWeapon = transform.childCount - 1;
            else
                selectedWeapon--;
        }

        if(selectedWeapon!=previousSelection)
        {
            SelectedWeapon();
        }
        if(!_knife)
        {
            if (!UIManager.Instance._menuOpen)
                Shoot();
        }
       
        Reload();

    }

    private void Shoot()
    {
        if (Type == TypeofGun.Primary)
        {
            if (Input.GetMouseButton(0) && Time.time >= nextShoot && _currentAmmoPrimary > 0 && Time.time > reloadtime)
            {
                nextShoot = Time.time + 1f / fireRate;
                if (Type == TypeofGun.Primary)
                {
                    _currentAmmoPrimary--;
                    UIManager.Instance.UpdateAmmoText(_currentAmmoPrimary, _totalAmmoPrimary);
                }
                else if (Type == TypeofGun.Secondary)
                {
                    _currentAmmoSecondary--;
                    UIManager.Instance.UpdateAmmoText(_currentAmmoSecondary, _totalAmmoSecondary);
                }



                Vector3 center = new Vector3(0.5f, 0.5f, 0f);
                Ray rayOrigin = Camera.main.ViewportPointToRay(center);
                RaycastHit hitInfo;

                if (Physics.Raycast(rayOrigin, out hitInfo, Mathf.Infinity, 1 << 6))
                {
                    Health health = hitInfo.collider.GetComponent<Health>();
                    if (health != null)
                    {
                        Instantiate(Blood[Random.Range(0, 3)], hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
                        health.Damage(Random.Range(damage, (damage + 5)));
                        if (health.currentHealth <= 0)
                        {
                            _player._money += Random.Range(40, 100);
                            UIManager.Instance.UpdateMoney();
                        }
                            
                    }

                }
            }
            else if (Input.GetMouseButtonDown(0) && Time.time >= nextShoot && _currentAmmoPrimary <= 0 && Time.time > reloadtime)
            {
                _audio.PlayOneShot(_audio.clip);
            }
        }

        else if (Type == TypeofGun.Secondary)
        {
            if (Input.GetMouseButton(0) && Time.time >= nextShoot && _currentAmmoSecondary > 0 && Time.time > reloadtime)
            {
                nextShoot = Time.time + 1f / fireRate;
                if (Type == TypeofGun.Primary)
                {
                    _currentAmmoPrimary--;
                    UIManager.Instance.UpdateAmmoText(_currentAmmoPrimary, _totalAmmoPrimary);
                }
                else if (Type == TypeofGun.Secondary)
                {
                    _currentAmmoSecondary--;
                    UIManager.Instance.UpdateAmmoText(_currentAmmoSecondary, _totalAmmoSecondary);
                }



                Vector3 center = new Vector3(0.5f, 0.5f, 0f);
                Ray rayOrigin = Camera.main.ViewportPointToRay(center);
                RaycastHit hitInfo;

                if (Physics.Raycast(rayOrigin, out hitInfo, Mathf.Infinity, 1 << 6))
                {
                    Health health = hitInfo.collider.GetComponent<Health>();
                    if (health != null)
                    {
                        Instantiate(Blood[Random.Range(0, 3)], hitInfo.point, Quaternion.LookRotation(hitInfo.normal));
                        health.Damage(Random.Range(damage, (damage + 5)));
                        if (health.currentHealth <= 0)
                        {
                            _player._money += Random.Range(40, 100);
                            UIManager.Instance.UpdateMoney();
                        }
                            
                    }

                }
            }
            else if (Input.GetMouseButtonDown(0) && Time.time >= nextShoot && _currentAmmoSecondary <= 0 && Time.time > reloadtime)
            {
                _audio.PlayOneShot(_audio.clip);
            }
        }
       
    }

    public void Knife(Collision hit)
    {
        if (Input.GetMouseButton(0))
        {
           

            Health health = hit.gameObject.GetComponent<Health>();
            if (health != null)
            {
                Instantiate(Blood[Random.Range(0, 3)], hit.contacts[0].point, Quaternion.LookRotation(hit.contacts[0].normal));
                health.Damage(Random.Range(20, 30));
            }

        }
    }

    public void Reload()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            reloadtime = Time.time + wait;
            if (Type == TypeofGun.Primary)
            {
                if (_totalAmmoPrimary > 0 && _currentAmmoPrimary < _maxAmmoPrimary)
                {
                    int reloadAmount = _maxAmmoPrimary - _currentAmmoPrimary;
                    if (reloadAmount < _totalAmmoPrimary)
                    {
                        _currentAmmoPrimary += reloadAmount;
                        _totalAmmoPrimary -= reloadAmount;
                    }

                    else
                    {
                        _currentAmmoPrimary += _totalAmmoPrimary;
                        _totalAmmoPrimary = 0;
                    }

                    UIManager.Instance.UpdateAmmoText(_currentAmmoPrimary, _totalAmmoPrimary);
                }
            }
            else if (Type == TypeofGun.Secondary)
            {
                if (_totalAmmoSecondary > 0 && _currentAmmoSecondary < _maxAmmoSecondary)
                {
                    int reloadAmount = _maxAmmoSecondary - _currentAmmoSecondary;
                    if (reloadAmount < _totalAmmoSecondary)
                    {
                        _currentAmmoSecondary += reloadAmount;
                        _totalAmmoSecondary -= reloadAmount;
                    }

                    else
                    {
                        _currentAmmoSecondary += _totalAmmoSecondary;
                        _totalAmmoSecondary = 0;
                    }

                    UIManager.Instance.UpdateAmmoText(_currentAmmoSecondary, _totalAmmoSecondary);
                }
            }
            
        }
        
    }
    

    void SelectedWeapon()
    {
        int i= 0;
       
        foreach (Transform weapon in transform)
        {

            if (i == selectedWeapon)
            {
                weapon.gameObject.SetActive(true);
                fireRate = currentGunsProperties[i].fireRate;
                damage = currentGunsProperties[i].damage;
                _knife = currentGunsProperties[i]._knife;
                Type = currentGunsProperties[i].Type;
                UIManager.Instance.UpdateWeaponImage(currentGunsProperties[i].GunImage);

                if (Type == TypeofGun.Primary)
                {
                    UIManager.Instance.UpdateAmmoText(_currentAmmoPrimary, _totalAmmoPrimary);
                   
                }
                else if (Type == TypeofGun.Secondary)
                {
                    UIManager.Instance.UpdateAmmoText(_currentAmmoSecondary, _totalAmmoSecondary);
                }


                if (!currentGunsProperties[i]._knife)
                {

                    UIManager.Instance.HideHudKnife(true);

                }
                else
                {
                    UIManager.Instance.HideHudKnife(false);
                }
                
                wait = currentGunsProperties[i].WaitSwitch;
                reloadtime = Time.time + wait;
            }

            else
            {
                weapon.gameObject.SetActive(false);
            }
                
            i++;
           
         
          
        }
    }


    public void BuyAk()
    {
        if (_player._money >= 4000)
        {

            _player._money -= 4000;
            UIManager.Instance.UpdateMoney();
          
            if (UnityEngine.Object.ReferenceEquals(currentGuns[2],null) || currentGuns[2].gameObject!=null)
            {
                Destroy(currentGuns[2].gameObject);
            }
            currentGuns[2] = Instantiate(scriptableObjects[1].Model,transform);
            currentGunsProperties[2] = scriptableObjects[1];
            _currentAmmoPrimary = currentGunsProperties[2]._currentAmmo;
            _maxAmmoPrimary = currentGunsProperties[2]._maxAmmo;
            _totalAmmoPrimary = currentGunsProperties[2]._totalAmmo;
            Type = currentGunsProperties[2].Type;
            UIManager.Instance.UpdateAmmoText(_currentAmmoPrimary, _totalAmmoPrimary);
            UIManager.Instance.UpdateWeaponImage(currentGunsProperties[2].GunImage);
            currentGuns[0].gameObject.SetActive(false);
            currentGuns[1].gameObject.SetActive(false);
            UIManager.Instance.HideHudKnife(true);
        }
        else
        {
            Debug.Log("NotEnoughMoney");
        }
    }


    public void BuyMP5()
    {
        if (_player._money >= 1500)
        {
            _player._money -= 1500;
            UIManager.Instance.UpdateMoney();
            
            if (currentGuns[2].gameObject != null)
            {
                Destroy(currentGuns[2].gameObject);
            }
            currentGuns[2] = Instantiate(scriptableObjects[3].Model, transform);
            currentGunsProperties[2] = scriptableObjects[3];
            _currentAmmoPrimary = currentGunsProperties[2]._currentAmmo;
            _maxAmmoPrimary = currentGunsProperties[2]._maxAmmo;
            _totalAmmoPrimary = currentGunsProperties[2]._totalAmmo;
            Type = currentGunsProperties[2].Type;
            UIManager.Instance.UpdateAmmoText(_currentAmmoPrimary, _totalAmmoPrimary);
            UIManager.Instance.UpdateWeaponImage(currentGunsProperties[2].GunImage);
            currentGuns[0].gameObject.SetActive(false);
            currentGuns[1].gameObject.SetActive(false);
            UIManager.Instance.HideHudKnife(true);
        }
        else
        {
            Debug.Log("NotEnoughMoney");
        }
    }

    public void BuyM4a1()
    {
        if (_player._money >= 3500)
        {
            _player._money -= 3500;
            UIManager.Instance.UpdateMoney();
           
            if (currentGuns[2].gameObject != null)
            {
                Destroy(currentGuns[2].gameObject);
            }
            currentGuns[2] = Instantiate(scriptableObjects[2].Model, transform);
            currentGunsProperties[2] = scriptableObjects[2];
            _currentAmmoPrimary = currentGunsProperties[2]._currentAmmo;
            _maxAmmoPrimary = currentGunsProperties[2]._maxAmmo;
            _totalAmmoPrimary = currentGunsProperties[2]._totalAmmo;
            Type = currentGunsProperties[2].Type;
            UIManager.Instance.UpdateAmmoText(_currentAmmoPrimary, _totalAmmoPrimary);
            UIManager.Instance.UpdateWeaponImage(currentGunsProperties[2].GunImage);
            currentGuns[0].gameObject.SetActive(false);
            currentGuns[1].gameObject.SetActive(false);
            UIManager.Instance.HideHudKnife(true);
        }
        else
        {
            Debug.Log("NotEnoughMoney");
        }
    }

    public void BuyShotGun()
    {
        if (_player._money >= 2500)
        {
            _player._money -= 3500;
            UIManager.Instance.UpdateMoney();
           
            if (currentGuns[2].gameObject != null)
            {
                Destroy(currentGuns[2].gameObject);
            }

            currentGuns[2] = Instantiate(scriptableObjects[4].Model, transform);
            currentGunsProperties[2] = scriptableObjects[4];
            _currentAmmoPrimary = currentGunsProperties[2]._currentAmmo;
            _maxAmmoPrimary = currentGunsProperties[2]._maxAmmo;
            _totalAmmoPrimary = currentGunsProperties[2]._totalAmmo;
            Type = currentGunsProperties[2].Type;
            UIManager.Instance.UpdateAmmoText(_currentAmmoPrimary, _totalAmmoPrimary);
            UIManager.Instance.UpdateWeaponImage(currentGunsProperties[2].GunImage);
            currentGuns[0].gameObject.SetActive(false);
            currentGuns[1].gameObject.SetActive(false);
            UIManager.Instance.HideHudKnife(true);
        }
        else
        {
            Debug.Log("NotEnoughMoney");
        }
    }

    public void BuyAmmo()
    {
        if (_player._money >= 500)
        {
            _player._money -= 500;
            UIManager.Instance.UpdateMoney();
            if(currentGunsProperties[2]!=null)
            {
                _currentAmmoPrimary = currentGunsProperties[2]._currentAmmo;
                _maxAmmoPrimary = currentGunsProperties[2]._maxAmmo;
                _totalAmmoPrimary = currentGunsProperties[2]._totalAmmo;
            }
            
            _currentAmmoSecondary = currentGunsProperties[0]._currentAmmo;
            _maxAmmoSecondary = currentGunsProperties[0]._maxAmmo;
            _totalAmmoSecondary = currentGunsProperties[0]._totalAmmo;

            if(Type== TypeofGun.Primary)
                UIManager.Instance.UpdateAmmoText(_currentAmmoPrimary, _totalAmmoPrimary);
            else if (Type == TypeofGun.Secondary)
                UIManager.Instance.UpdateAmmoText(_currentAmmoSecondary, _totalAmmoSecondary);



        }
        else
        {
            Debug.Log("NotEnoughMoney");
        }
    }
}
