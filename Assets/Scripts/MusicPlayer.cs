using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
 
[RequireComponent(typeof(AudioSource))]
public class MusicPlayer : MonoBehaviour
{
    public AudioClip[] sounds;

    protected Queue<AudioClip> queue;

    protected AudioSource player;

    void Start()
    {
        player = GetComponent<AudioSource>();
        queue = new(sounds);
        
        player.clip = queue.First();
        player.Play();
    }

    void Update()
    {
        if (!player.isPlaying) {
            queue.Append(queue.First());
            queue.Peek();

            player.clip = queue.First();
            player.Play();
        }
    }
}
