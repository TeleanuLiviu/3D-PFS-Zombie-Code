using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class M4a1 : MonoBehaviour
{
    // Start is called before the first frame update


    [SerializeField]
    private ParticleSystem _smoke;

    [SerializeField]
    private ParticleSystem _bulletCasing;

    [SerializeField]
    private ParticleSystem _muzzleFlashSide;

    [SerializeField]
    private ParticleSystem _Muzzle_Flash_Front;

    private Animator _anim;

    [SerializeField]
    private AudioClip _gunShotAudioClip;
    [SerializeField]
    public AudioClip ReloadSound;
    [SerializeField]
    private AudioSource _audioSource;

    public bool FullAuto = true;
    public WeaponSystem _weaponHolder;
    void Start()
    {
        _anim = GetComponent<Animator>();
        _weaponHolder = GetComponentInParent<WeaponSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && _weaponHolder._currentAmmoPrimary > 0 && Time.time > _weaponHolder.reloadtime && !UIManager.Instance._menuOpen)
        {

            if (FullAuto == true)
            {
                _anim.SetBool("Automatic_Fire", true);
            }
        }

        if (Input.GetKeyUp(KeyCode.Mouse0) || _weaponHolder._currentAmmoPrimary <= 0)
        {
            if (FullAuto == true)
            {
                _anim.SetBool("Automatic_Fire", false);
            }

        }

        if (Input.GetKeyDown(KeyCode.R) && _weaponHolder._totalAmmoPrimary > 0 )
        {
            _anim.SetTrigger("Reload");
        }
    }

    public void FireGunParticles()
    {
        Debug.Log("Fired gun particles");
        _smoke.Play();
        _bulletCasing.Play();
        _muzzleFlashSide.Play();
        _Muzzle_Flash_Front.Play();
        GunFireAudio();
    }

    public void GunFireAudio()
    {
        _audioSource.pitch = Random.Range(0.9f, 1.1f);
        _audioSource.PlayOneShot(_gunShotAudioClip);
    }

    public void ReloadSoundFunction()
    {
        _audioSource.PlayOneShot(ReloadSound);
    }
}
