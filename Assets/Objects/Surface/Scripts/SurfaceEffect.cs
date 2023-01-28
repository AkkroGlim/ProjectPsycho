using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SurfaceEffect", menuName = "Impact System/SurfaceEffect")]

public class SurfaceEffect : ScriptableObject
{
    public List<SpawnObjectEffect> SpawnObjectEffects = new List<SpawnObjectEffect>();
    public List<PlayAudioEffect> PlayAudioEffects = new List<PlayAudioEffect>();
}
