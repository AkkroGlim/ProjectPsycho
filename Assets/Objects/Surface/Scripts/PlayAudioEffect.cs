using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Impact System/Play Audio Effect", fileName = "PlayAudioEffect")]

public class PlayAudioEffect : ScriptableObject
{
    public AudioSource AudioSourcePrefab;
    public List<AudioClip> AudioClips = new List<AudioClip>();
    public Vector2 VolumeRange = new Vector2(0, 1);
}
