using System;
using UnityEngine;

public enum SoundType
{
    Err_Not_Set,
    Chop,
    Footstep,
    Object_drop,
    Object_pick,
    Warning,
    Trash,
    Ambience_Sizzle,
    Delivery_Success,
    Delivery_Failure,
}

[ExecuteInEditMode]
[RequireComponent (typeof(AudioSource))]
public class System_Audio : MonoBehaviour
{
    [SerializeField] private SoundTypeCategory[] _soundCategoriesArr;
    private AudioSource audioSrc;
    public static System_Audio Instance {get;private set;}
    
    void Awake()
    {
        Instance = this;    
    }

    void Start()
    {
        audioSrc = GetComponent<AudioSource>();   
    }

    public void PlaySoundOfType(SoundType type, AudioSource source = null)
    {
        AudioClip clip = GetSoundInCategory(type, out float volume);

        if (source != null)
        {
            AudioSource.PlayClipAtPoint(clip, source.gameObject.transform.position, volume);
            return;
        }

        source = audioSrc;
        source.PlayOneShot(clip);
    }

    private AudioClip GetSoundInCategory(SoundType type, out float volume)
    {
        volume = _soundCategoriesArr[(int)type].Volume;
        AudioClip[] arr = Instance._soundCategoriesArr[(int)type];
        return arr[UnityEngine.Random.Range(0,arr.Length)];
    }


#if UNITY_EDITOR
    private void OnEnable() 
    {
        if (_soundCategoriesArr is null == false)
            return;

        Array enumValsArr = Enum.GetValues(typeof(SoundType));

        _soundCategoriesArr = new SoundTypeCategory[enumValsArr.Length];

        foreach(int type in enumValsArr)
        {
            _soundCategoriesArr[type] = new SoundTypeCategory((SoundType)type);
        }
    }
#endif
}

[Serializable]
internal struct SoundTypeCategory
{
    [SerializeField] private string _categoryName;
    [SerializeField] private AudioClip[] soundArray;
    [Range(0,1)][SerializeField] private float _volume;
    public readonly float Volume => _volume;

    public static implicit operator AudioClip[] (SoundTypeCategory STA)
    {
        return STA.soundArray;
    }

    public SoundTypeCategory(SoundType type, float vol = 1)
    {
        _volume = vol;
        _categoryName = Enum.GetName(typeof(SoundType), type);
        soundArray = new AudioClip[0];
    }
}