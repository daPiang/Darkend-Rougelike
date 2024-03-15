using System.Collections;
using System.Collections.Generic;
using Fusion;
using UnityEngine.UI;
using UnityEngine;

public class PlayerHealth : NetworkBehaviour
{
    public Image hpBar;
    [Networked, OnChangedRender(nameof(SyncHp))] public float NetworkedHp {get; set;}
    public float localHp;
    public float maxHp = 100;
    private bool tookDamage;
    public float damageTimer = 0.5f;
    private float timer;

    public override void FixedUpdateNetwork()
    {
        if(!HasStateAuthority) return;

        timer += Runner.DeltaTime;
        if(timer > damageTimer) timer = damageTimer;

        hpBar.fillAmount = localHp/maxHp;
    }

    public override void Render()
    {
        // if(transform.root.localScale.x == -1) this.gameObject.transform.localScale = new(1,1,1);

        if(tookDamage)
        {
            GetComponent<SpriteFlasher>().FlashRed();
            tookDamage = false;
        }
    }

    [Rpc(RpcSources.StateAuthority, RpcTargets.StateAuthority)]
    public void Rpc_TakeDamage(float damage)
    {
        if(timer < damageTimer) return;

        timer = 0;
        NetworkedHp -= damage;
        tookDamage = true;
    }

    private void SyncHp()
    {
        localHp = NetworkedHp;
        hpBar.fillAmount = localHp/maxHp;
    }
}
