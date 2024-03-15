using Fusion;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class XPBar : NetworkBehaviour
{
    public Image slider;
    public TextMeshProUGUI levelText;
    public int maxLevel = 100;
    [Networked, OnChangedRender(nameof(SyncLevel))] public int CurrentLevel {get; set;} = 0;
    private int localLevel;
    [Networked, OnChangedRender(nameof(SyncXp))] public float CurrentXP {get; set;} = 0;
    private float localXp;
    public float[] xpCaps;

    public override void FixedUpdateNetwork() {
        if(!HasStateAuthority) return;

        if(localLevel != maxLevel) levelText.text = $"Lv {localLevel}";
        if(localLevel > maxLevel)
        {
            levelText.text = $"Lv MAX";
            slider.fillAmount = 1;
            return;
        }

        if(localXp >= xpCaps[localLevel])
        {
            if(localLevel == maxLevel)
            {
                FindObjectOfType<PowerUpSelect>().RpcOpenPowerUpSelect();
                CurrentLevel++;
            }
            else
            {
                CurrentLevel++;
                localXp = 0;
                FindObjectOfType<PowerUpSelect>().RpcOpenPowerUpSelect();
                // slider.fillAmount = 0;
            }
        }
        else
        {
            slider.fillAmount = localXp/xpCaps[CurrentLevel];
        }
    }

    private void SyncLevel()
    {
        if(localLevel > maxLevel)
        {
            levelText.text = $"Lv MAX";
            slider.fillAmount = 1;
            return;
        }

        CurrentXP = 0;
        localLevel = CurrentLevel;
        levelText.text = $"Lv {localLevel}";
    }
    private void SyncXp()
    {
        localXp = CurrentXP;
        slider.fillAmount = localXp/xpCaps[localLevel];
    }

    [Rpc(RpcSources.All, RpcTargets.StateAuthority)]
    public void RpcAddXp(float xp)
    {
        if(!HasStateAuthority) return;
        if(CurrentLevel == maxLevel) return;
        CurrentXP += xp;
    }
}
