using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{
    private static UIManager _instance;
    public static UIManager Instance { get { return _instance; } }
    [SerializeField]
    public Text AmmoText;
    [SerializeField]
    public Image GunImage;
    [SerializeField]
    public GameObject BuyWheel;
    [SerializeField]
    public Text MoneyText,Health;
    Player _player;
    public GameObject HudBullets;
    public bool _menuOpen;
    public GameObject DiedCutscene;
    [SerializeField]
    private GameObject PressBText;
    
    void Awake()
    {
        _instance = this;
    }

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        PressBText.SetActive(true);
    }


    void Update()
    {
        if(Input.GetKeyDown(KeyCode.B) || (BuyWheel.activeSelf && Input.GetKeyDown(KeyCode.Escape)))
        {
            if(BuyWheel.activeSelf)
            {
                _menuOpen = false;
                BuyWheel.SetActive(false);
            }
            else
            {
                _menuOpen = true;
                BuyWheel.SetActive(true);
            }

            PressBText.SetActive(false);
        }
    }

    public void UpdateMoney()
    {
        MoneyText.text = "$" + _player._money.ToString();
    }

    public void UpdateWeaponImage(Sprite sprite)
    {
        GunImage.sprite = sprite;
    }

    public void UpdateAmmoText(int _currentAmmo, int _totalAmmo)
    {
        AmmoText.text = _currentAmmo.ToString() + "/" + _totalAmmo.ToString();
    }

    public void HideHudKnife(bool hide)
    {
        HudBullets.SetActive(hide);
    }

    public void UpdateHealth(int health)
    {
        Health.text = health.ToString();
        if(health<=0)
            Health.text = "0";
    }

    public void DiedCutsceneActivation()
    {
        DiedCutscene.SetActive(true);
        
    }
    
}
