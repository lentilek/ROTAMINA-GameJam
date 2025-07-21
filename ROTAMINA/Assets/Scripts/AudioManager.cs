using System.Collections;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [SerializeField] private AudioSource audioSource, audioVibration;
    [SerializeField] private AudioClip matched, nope, unmatched, success, receiveMessage, sendMessage;
    [SerializeField] private AudioClip[] uiClick;
    private void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(transform.gameObject);
            Instance = this;
            return;
        }
        Destroy(gameObject);
    }
    private void Update()
    {
        audioVibration.volume = audioSource.volume / 4;
    }
    public void PlaySound(string clip)
    {
        switch (clip)
        {
            case "matched":
                audioSource.PlayOneShot(matched);
                break;
            case "nope":
                audioSource.PlayOneShot(nope);
                break;
            case "unmatched":
                audioSource.PlayOneShot(unmatched);
                break;
            case "success":
                audioSource.PlayOneShot(success);
                break;
            case "receive":
                audioSource.PlayOneShot(receiveMessage);
                break;
            case "send":
                audioSource.PlayOneShot(sendMessage);
                break;
            default: break;
        }
    }
    public void ClickSound()
    {
        audioSource.PlayOneShot(uiClick[Random.Range(0, uiClick.Length)]);
    }
    /*public IEnumerator Vibrations()
    {
        audioVibration.Play();
        yield return new WaitForSeconds(1.03f);
        StartCoroutine(Vibrations());
    }*/
    public void Vibrations()
    {
        audioVibration.Play();
    }
    public void VibrationsStop()
    {
        StopAllCoroutines();
        audioVibration.Stop();
    }
}
