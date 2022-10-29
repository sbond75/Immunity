using System;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager
{
    static Dictionary<string, AudioClip> clips = new Dictionary<string, AudioClip>();
    public static AudioClip GetClip(string clip)
    {
        if (clips.ContainsKey(clip))
        {
            return clips[clip];
        }
        return (AudioClip)Resources.Load(clip);
    }
}
