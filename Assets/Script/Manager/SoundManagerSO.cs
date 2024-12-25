using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
[CreateAssetMenu(fileName = "DoorSO", menuName = "Scriptable Objects/SoundManagerSO")]

public class SoundManagerSO : ScriptableObject
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public static AudioSource MusicSource;
    public AudioSource SFXSource;

    public SoundLibary Sfx;
    public static SoundLibary Music;

    public AudioMixer AudioMixer;
    private float _pitchSfxChange = 0.1f;

    public static float musicFadeDurrationSec = 1;
    public void PlaySound(string soundName)
    {

        SFXSource.pitch = Random.Range(1 - _pitchSfxChange, 1 + _pitchSfxChange);
        SFXSource.PlayOneShot(Sfx.GetAudioClipsFromName(soundName));
    }
    public static void PlayMusic(string musicName)
    {
        GameObject dummyGO = new GameObject("DUMMY GAME OBJECT");
        dummyGO.AddComponent<Dummy>().StartCoroutine(CrossFadeMusic(musicName));
    }
    static IEnumerator CrossFadeMusic(string musicName)
    {
        AudioSource musicSource = Instantiate(MusicSource);



        float percent = 0;
        while (percent < 1)
        {
            percent += Time.deltaTime * 1 / musicFadeDurrationSec;

            MusicSource.volume = Mathf.Lerp(1f, 0, percent);
            yield return null;

        }
        Debug.Log("musicSource.volume" + musicSource.volume);
        musicSource.clip = Music.GetAudioClipsFromName(musicName);
        musicSource.Play();
        percent = 0;
        yield return null;

        while (percent < 1)
        {
            percent += Time.deltaTime * 1 / musicFadeDurrationSec;

            musicSource.volume = Mathf.Lerp(0, 1f, percent);
            yield return null;

        }
    }
    public void UpdateMusicVol(float vol)
    {
        Debug.Log("vol" + vol);
        AudioMixer.SetFloat(AudioMixName.MUSIC, vol);
    }
    public void UpdateSfxVol(float vol)
    {
        AudioMixer.SetFloat(AudioMixName.SFX, vol);
    }
    public void SaveMixer()
    {
        if (AudioMixer == null) return;
        AudioMixer.GetFloat(AudioMixName.MUSIC, out float musicVol);
        // audioMixer.GetFloat(AudioMixName.SFX, out float sfxVol);

        PlayerPrefs.SetFloat("musicVol", musicVol);
        // PlayerPrefs.SetFloat("sfxVol", sfxVol);
        PlayerPrefs.Save();
    }
    void LoadMixer()
    {
        float BMGVol = PlayerPrefs.GetFloat("musicVol", 1);
        // float sfxVol = PlayerPrefs.GetFloat("sfxVol", 1);
        UpdateMusicVol(BMGVol);
        // UpdateSfxVol(sfxVol);

    }
}
public class Dummy : MonoBehaviour
{

}
