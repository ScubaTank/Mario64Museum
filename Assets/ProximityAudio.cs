using System;
using UnityEngine;

public class ProximityAudio : MonoBehaviour {
    public Transform arCamera;         // Drag ARCamera here
    public AudioSource levelMusic;    // Drag your AudioSource here
    
    [Header("Volume Settings")]
    public float maxVolume = 1.0f;
    public float minVolume = 0.0f;
    public float startFadeDist = 0.8f; // Distance where music starts
    public float fullVolumeDist = 0.2f; // Distance for max volume

    private void Awake()
    {
        levelMusic.volume = 0;
    }

    void Update() {
        if (arCamera == null || levelMusic == null) return;

        float dist = Vector3.Distance(arCamera.position, transform.position);

        // Calculate volume based on distance
        // InverseLerp returns 0 at fullVolumeDist and 1 at startFadeDist
        float t = Mathf.InverseLerp(startFadeDist, fullVolumeDist, dist);
        
        // Apply the volume
        levelMusic.volume = Mathf.Lerp(minVolume, maxVolume, t);
        
        // Optimization: Pause audio if too far away
        if (dist > startFadeDist && levelMusic.isPlaying) {
            levelMusic.Pause();
        } else if (dist <= startFadeDist && !levelMusic.isPlaying) {
            levelMusic.UnPause();
        }
    }
}