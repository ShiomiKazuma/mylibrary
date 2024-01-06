using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] AudioSource _bgmAudioSource;
    [SerializeField] AudioSource _seAudioSource;
    [SerializeField] List<BGMSoundData> _bgmSoundDatas;
    [SerializeField] List<SESoundData> _seSoundDatas;

    public float _masterVolume = 1;
    public float _bgmMasterVolume = 1;
    public float _seMasterVolume = 1;

    public static SoundManager _instance { get; private set; }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayBGM(BGMSoundData.BGM bgm)
    {
        BGMSoundData data = _bgmSoundDatas.Find(data => data._bgm == bgm);
        _bgmAudioSource.clip = data._audioClip;
        _bgmAudioSource.volume = data._volume * _bgmMasterVolume * _masterVolume;
        _bgmAudioSource.Play();
    }


    public void PlaySE(SESoundData.SE se)
    {
        SESoundData data = _seSoundDatas.Find(data => data._se == se);
        _seAudioSource.volume = data._volume * _seMasterVolume * _masterVolume;
        _seAudioSource.PlayOneShot(data._audioClip);
    }
}

[System.Serializable]
public class BGMSoundData
{
    public enum BGM
    {
        //‚±‚±‚Ì•”•ª‚ªƒ‰ƒxƒ‹‚É‚È‚é
        Title,
        Dungeon,

    }

    public BGM _bgm;
    public AudioClip _audioClip;
    [Range(0, 1)] public float _volume = 1;
}

[System.Serializable]
public class SESoundData
{
    public enum SE
    {
        //‚±‚±‚Ì•”•ª‚ªƒ‰ƒxƒ‹‚É‚È‚é
        Attack,
        Damage,

    }

    public SE _se;
    public AudioClip _audioClip;
    [Range(0, 1)] public float _volume = 1;
}
