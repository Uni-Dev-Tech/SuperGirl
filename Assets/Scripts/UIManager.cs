using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Image playerHealth;

    static public UIManager instance;

    private void Awake()
    {
        UIManagerInit();
    }

    private void Start()
    {
        if(UIManager.instance != null)
        {
            Destroy(gameObject);
            return;
        }
        UIManager.instance = this;
    }

    private void UIManagerInit()
    {
        playerHealth.fillAmount = 1f;
    }

    public void RenewPlayerHealthInf(float newLifeValue)
    {
        if (newLifeValue < 0)
            newLifeValue = 0;

        playerHealth.fillAmount = newLifeValue;
    }
}
