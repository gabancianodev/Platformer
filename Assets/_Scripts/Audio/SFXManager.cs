using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour {

    [Header("Player")]
    public AudioSource player_hit;
    public AudioSource player_jump;
    public AudioSource player_shoot;
    [Space]
    [Header("Player to World")]
    public AudioSource player_pickup_coin;
    public AudioSource player_pickup_health;

}
