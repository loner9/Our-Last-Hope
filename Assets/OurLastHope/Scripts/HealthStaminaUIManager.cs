using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class HealthStaminaUIManager : MonoBehaviour
{
    [SerializeField] private Image hBar;
    [SerializeField] private Image sBar;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
         hBar.fillAmount = GameManager.Instance.Player.StatsHid.health / GameManager.Instance.Player.StatsHid.maxHealth;
         sBar.fillAmount = GameManager.Instance.Player.StatsHid.stamina / GameManager.Instance.Player.StatsHid.maxStamina;
    }
}
