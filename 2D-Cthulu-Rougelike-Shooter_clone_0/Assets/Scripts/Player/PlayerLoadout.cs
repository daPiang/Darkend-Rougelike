using System.Collections;
using System.Collections.Generic;
using Fusion;
using UnityEngine;

public class PlayerLoadout : NetworkBehaviour
{
    public List<GunBase> guns;
    public bool isHost;

    public override void FixedUpdateNetwork()
    {
        isHost = Runner.IsSharedModeMasterClient;
    }

    public bool IsClient()
    {
        if(HasStateAuthority)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public List<GunBase> GetLoadout()
    {
        return guns;
    }

    public int FindNextAvailableSlot()
    {
        int index = 0;
        foreach(GunBase gun in guns)
        {
            if(!gun.gameObject.activeSelf) return index;
            index++;
        }

        return 0;
    }

    [Rpc(RpcSources.StateAuthority, RpcTargets.All)]
    public void Rpc_ResetLoadout()
    {
        foreach(GunBase gun in guns)
        {
            if(gun.gameObject.activeSelf) gun.gameObject.SetActive(false);
        }
    }
}
