using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyWheel : MonoBehaviour
{

    [SerializeField]
    private GameObject[] BuySelection;
    [SerializeField]
    private GameObject[] Models;
    [SerializeField]
    public Text[] Prices;
    private Player _player;

    public void Start()
    {
        _player = GetComponentInParent<Player>();
    }

    public void Update()
    {
        if (UIManager.Instance._menuOpen)
        {
            if(_player._money>=4000)
            {
                Prices[0].color = new Color(1, 1, 1);
            }
            else
            {
                Prices[0].color = new Color(1, 0.5f, 0.5f);
            }

            if (_player._money >= 1500)
            {
                Prices[1].color = new Color(1, 1, 1);
            }
            else
            {
                Prices[1].color = new Color(1, 0.5f, 0.5f);
            }

            if (_player._money >= 2500)
            {
                Prices[2].color = new Color(1, 1, 1);
            }
            else
            {
                Prices[2].color = new Color(1, 0.5f, 0.5f);
            }

            if (_player._money >= 3500)
            {
                Prices[3].color = new Color(1, 1, 1);
            }
            else
            {
                Prices[3].color = new Color(1, 0.5f, 0.5f);
            }

            if (_player._money >= 500)
            {
                Prices[4].color = new Color(1, 1, 1);
            }
            else
            {
                Prices[4].color = new Color(1, 0.5f, 0.5f);
            }



        }
    }

    public void HoverEnter(int i)
    {
        switch(i)
        {
            case 0:
                Models[2].SetActive(true);
                Models[0].SetActive(false);
                Models[1].SetActive(false);
                Models[3].SetActive(false);
                Models[4].SetActive(false);
                BuySelection[0].SetActive(true);
                BuySelection[1].SetActive(false);
                BuySelection[2].SetActive(false);
                BuySelection[3].SetActive(false);
                BuySelection[4].SetActive(false);
                BuySelection[5].SetActive(false);
                break;

            case 1:
                
                BuySelection[0].SetActive(false);
                BuySelection[1].SetActive(true);
                BuySelection[2].SetActive(false);
                BuySelection[3].SetActive(false);
                BuySelection[4].SetActive(false);
                BuySelection[5].SetActive(false);
                break;

            case 2:
                Models[2].SetActive(false);
                Models[0].SetActive(false);
                Models[1].SetActive(false);
                Models[3].SetActive(false);
                Models[4].SetActive(true);
                BuySelection[0].SetActive(false);
                BuySelection[1].SetActive(false);
                BuySelection[2].SetActive(true);
                BuySelection[3].SetActive(false);
                BuySelection[4].SetActive(false);
                BuySelection[5].SetActive(false);
                break;

            case 3:
                Models[2].SetActive(false);
                Models[0].SetActive(true);
                Models[1].SetActive(false);
                Models[3].SetActive(false);
                Models[4].SetActive(false);
                BuySelection[0].SetActive(false);
                BuySelection[1].SetActive(false);
                BuySelection[2].SetActive(false);
                BuySelection[3].SetActive(true);
                BuySelection[4].SetActive(false);
                BuySelection[5].SetActive(false);
                break;

            case 4:
                Models[2].SetActive(false);
                Models[0].SetActive(false);
                Models[1].SetActive(true);
                Models[3].SetActive(false);
                Models[4].SetActive(false);
                BuySelection[0].SetActive(false);
                BuySelection[1].SetActive(false);
                BuySelection[2].SetActive(false);
                BuySelection[3].SetActive(false);
                BuySelection[4].SetActive(true);
                BuySelection[5].SetActive(false);
                break;

            case 5:
                Models[2].SetActive(false);
                Models[0].SetActive(false);
                Models[1].SetActive(false);
                Models[3].SetActive(true);
                Models[4].SetActive(false);
                BuySelection[0].SetActive(false);
                BuySelection[1].SetActive(false);
                BuySelection[2].SetActive(false);
                BuySelection[3].SetActive(false);
                BuySelection[4].SetActive(false);
                BuySelection[5].SetActive(true);
                break;
            default:
                Models[2].SetActive(false);
                Models[0].SetActive(false);
                Models[1].SetActive(false);
                Models[3].SetActive(false);
                Models[4].SetActive(false);
                BuySelection[0].SetActive(false);
                BuySelection[1].SetActive(false);
                BuySelection[2].SetActive(false);
                BuySelection[3].SetActive(false);
                BuySelection[4].SetActive(false);
                BuySelection[5].SetActive(false);
                break;
        }
    }

    public void HoverExit()
    {
        BuySelection[0].SetActive(false);
        BuySelection[1].SetActive(false);
        BuySelection[2].SetActive(false);
        BuySelection[3].SetActive(false);
        BuySelection[4].SetActive(false);
        BuySelection[5].SetActive(false);
    }

    
}
