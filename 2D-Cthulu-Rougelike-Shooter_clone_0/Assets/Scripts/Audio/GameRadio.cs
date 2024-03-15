using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameRadio : MonoBehaviour
{
    private AudioManager am;
    public GameObject inGameUi, menuUi;
    // Start is called before the first frame update
    private void Start() {
        am = AudioManager.instance;
    }

    private void Update() {
        if(menuUi.activeSelf)
        {
            am.Stop("game_bgm");
            PlaySound("menu_bgm");
        }

        if(!menuUi.activeSelf)
        {
            am.Stop("menu_bgm");
            PlaySound("game_bgm");
        }
    }

    private void PlaySound(string sound)
    {
        if(!am.IsPlaying(sound))
        {
            am.Play(sound);
        }
    }
}
