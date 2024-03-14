using System.Collections;
using System.Collections.Generic;
using Fusion;
using UnityEngine;

public class PlayerLoadout : NetworkBehaviour
{
    public List<GunBase> guns;

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
}
