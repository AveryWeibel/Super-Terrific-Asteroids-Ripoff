using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour {

    public static AudioSource astHit;
    public static AudioSource lasShot;
    public static AudioSource enmHit;
    public static AudioSource menuClick;
    public static AudioSource plyrHit;
    public static AudioSource thrusters;
    public static AudioSource pickup;
    public static AudioSource lasHit;

    public AudioSource[] gameAudio = new AudioSource[20];

    void Start () {
        gameAudio = GetComponents<AudioSource>(); //Make sure game music is last always

        astHit = gameAudio[0];
        lasShot = gameAudio[1];
        enmHit = gameAudio[2];
        menuClick = gameAudio[3];
        plyrHit = gameAudio[4];
        thrusters = gameAudio[5];
        pickup = gameAudio[6];
        lasHit = gameAudio[7];
    }
}
