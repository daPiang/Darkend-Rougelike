using Fusion;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class XPBar : NetworkBehaviour
{
    public Image slider;
    public TextMeshProUGUI levelText;
    public int maxLevel = 100;
    public int currentLevel = 0;
    public float currentXP = 0;
    public float[] xpCaps;

    private void Start()
    {
        if(!HasStateAuthority) return;

        slider.fillAmount = currentXP/xpCaps[currentLevel];
    }

    private void Update() {
        if(!HasStateAuthority) return;

        if(currentLevel != maxLevel) levelText.text = $"Lv {currentLevel}";
        if(currentLevel > maxLevel)
        {
            levelText.text = $"Lv MAX";
            slider.fillAmount = 1;
            return;
        }

        if(currentXP == xpCaps[currentLevel])
        {
            if(currentLevel == maxLevel)
            {
                FindObjectOfType<PowerUpSelect>().OpenPowerUpSelect();
                currentLevel++;
            }
            else
            {
                currentLevel++;
                currentXP = 0;
                FindObjectOfType<PowerUpSelect>().OpenPowerUpSelect();
                // slider.fillAmount = 0;
            }
        }
        else
        {
            slider.fillAmount = currentXP/xpCaps[currentLevel];
        }
    }

    public void AddXp(float xp)
    {
        if(!HasStateAuthority) return;
        if(currentLevel == maxLevel) return;
        currentXP += xp;
    }
}
