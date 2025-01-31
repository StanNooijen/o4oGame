using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dont_destroy : MonoBehaviour
{
    public static dont_destroy instance;
    public AudioSource MusicSource;
    public AudioSource JumpSource;
    public Transform Music;
    public Transform Jump;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

            Music = transform.Find("Music");
            Jump = transform.Find("Jump");

            MusicSource = Music.GetComponent<AudioSource>();
            JumpSource = Jump.GetComponent<AudioSource>();

            if (MusicSource == null && JumpSource == null)
            {
                JumpSource = gameObject.AddComponent<AudioSource>();
                MusicSource = gameObject.AddComponent<AudioSource>();
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetAudioClip(AudioClip clip, bool AudioSelection, int currentSwitche, List<bool> switchLocked)
    {
        if (currentSwitche > 1 && !switchLocked[currentSwitche - 2])
        {
            if (AudioSelection)
            {
                if (clip == null)
                {
                    JumpSource.Stop();
                    JumpSource.clip = null;
                }
                else
                {
                    JumpSource.clip = clip;
                    JumpSource.PlayOneShot(clip);
                }
            }
            else
            {
                if (clip == null)
                {
                    MusicSource.Stop();
                }
                else
                {
                    if (MusicSource.clip != clip || !MusicSource.isPlaying)
                    {
                        MusicSource.clip = clip;
                        MusicSource.Play();
                    }
                }
            }
        }
        else
        {
            StopSound(AudioSelection);
        }
    }

    public void StopSound(bool AudioSelection)
    {
        if (AudioSelection)
        {
            JumpSource.Stop();
            JumpSource.clip = null;
        }
        else
        {
            MusicSource.Stop();
        }
    }
}