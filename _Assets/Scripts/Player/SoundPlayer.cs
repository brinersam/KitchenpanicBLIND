using UnityEngine;

[RequireComponent (typeof(AudioSource))]
public class SoundPlayer : MonoBehaviour
{
    [SerializeField] private float _triggerPerSecond = 1;
    [SerializeField] private SoundType _soundType;
    private AudioSource _audioSrc;
    private float runningTime = 0;

    void Start()
    {
        _audioSrc = GetComponent<AudioSource>();
    }

    public void Tick()
    {
        Debug.LogError("LAGG",this);

        if (runningTime >= 1) // todo LAG
        {
            runningTime = 0;
            System_Audio.Instance.PlaySoundOfType(_soundType, _audioSrc);
        }
        runningTime += Time.deltaTime * _triggerPerSecond;   
    }
}
