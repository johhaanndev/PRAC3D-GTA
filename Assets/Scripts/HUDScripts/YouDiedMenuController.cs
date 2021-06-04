using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YouDiedMenuController : MonoBehaviour
{
    public AudioSource bonusSong;
    public AudioSource normalSong;

    private void Start()
    {
        bonusSong.Stop();
        normalSong.Stop();
    }
}
