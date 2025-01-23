using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// enum décrivant les différents sons possibles
/// </summary>
public enum Sounds
{
    Explosion,
    Spawn,
    Damage,
    Death
}

public class AudioManager : MonoBehaviour
{
    [SerializeField] int _poolSize;
    [SerializeField] AudioSource _SFXObject;
    [SerializeField] List<Clip> _clips;

    public Queue<AudioSource> AudioPool = new Queue<AudioSource>();

    //singleton
    private static AudioManager instance = null;
    public static AudioManager Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject go = new GameObject("Audio Manager");
                instance = go.AddComponent<AudioManager>();
            }
            return instance;
        }
    }
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        for (int i = 0; i < _poolSize; i++)
        {
            CreatePoolItem();
        }
    }

    AudioSource CreatePoolItem()
    {
        AudioSource source = Instantiate(_SFXObject);
        source.gameObject.SetActive(false);
        AudioPool.Enqueue(source);
        return source;
    }

    /// <summary>
    /// sors de la pool/queue une audiosource et active son gameObject
    /// </summary>
    /// <returns></returns>
    AudioSource UseFromPool()
    {
        AudioSource source;
        if (AudioPool.Count == 0)
        {
            source = CreatePoolItem();
        }
        else
        {
            source = AudioPool.Dequeue();
        }

        source.gameObject.SetActive(true);
        return source;
    }

    /// <summary>
    /// renvoie l'audioSource dans la pool et désactive son gameObject
    /// </summary>
    /// <param name="source"></param>
    public void BackToPool(AudioSource source)
    {
        source.gameObject.SetActive(false);
        AudioPool.Enqueue(source);
    }

    /// <summary>
    /// chosit une liste dans une liste via un enum et choisit ensuite un audioclip au hasard dans ce dernier pour le jouer.
    /// </summary>
    /// <param name="choosenSound"></param>
    /// <param name="volume"></param>
    /// <param name="pitch"></param>
    public void PlaySFXClip(Sounds choosenSound, float volume = 1f, float pitch = 1f)
    {
        if (_clips[(int)choosenSound].ClipList.Count == 0) // cas si l'array n'a pas été remplie correctement
        {
            Debug.LogError("list is empty");
            return;
        }

        AudioClip audioClip = _clips[(int)choosenSound].ClipList[Random.Range(0, _clips[(int)choosenSound].ClipList.Count)]; // choisit l'array correcte en fonction de l'enum en paramètre et y choisit un clip au hasard

        AudioSource audioSource = UseFromPool();
        if (audioSource == null) return;

        audioSource.clip = audioClip; // assigne le clip random à l'auriosource
        audioSource.volume = volume; // assigne le volume à l'audiosource
        audioSource.pitch = pitch; // assigne le pitch aà l'audiosource
        audioSource.spatialBlend = 0; // désactive la spatialisation
        audioSource.bypassEffects = false;

        audioSource.Play(); // joue le son

        float clipLength = audioSource.clip.length; // détermine la longueur du son

        StartCoroutine(HandleSoundEnd(audioSource, clipLength));
    }

    /// <summary>
    /// chosit une liste dans une liste via un enum et choisit ensuite un audioclip au hasard dans ce dernier pour le jouer. le son sera placé a un endroit donné et prendra en compte ou non les effets
    /// </summary>
    /// <param name="choosenSound"></param>
    /// <param name="position"></param>
    /// <param name="bypassesEffects"></param>
    /// <param name="volume"></param>
    /// <param name="pitch"></param>
    public void PlaySFXClipAtPosition(Sounds choosenSound, Vector3 position, bool bypassesEffects = false, float volume = 1f, float pitch = 1f)
    {
        if (_clips[(int)choosenSound].ClipList.Count == 0) // cas si la liste n'a pas été remplie correctement
        {
            print("y'a pas de sons là");
            return;
        }

        AudioClip audioClip = _clips[(int)choosenSound].ClipList[Random.Range(0, _clips[(int)choosenSound].ClipList.Count)]; // choisit la liste correcte en fonction de l'enum en paramètre et y choisit un clip au hasard

        AudioSource audioSource = UseFromPool();
        audioSource.gameObject.transform.position = position;

        audioSource.clip = audioClip;
        audioSource.volume = volume; // assigne le volume à l'audiosource
        audioSource.pitch = pitch; // assigne le pitch à l'audiosource
        audioSource.spatialBlend = 1; // gère bien le blend rapport a l'audiolistener
        audioSource.bypassEffects = bypassesEffects; // gère le bypass ou non des effets

        audioSource.Play(); // joue le son

        float clipLength = audioSource.clip.length; // détermine la longueur du son

        StartCoroutine(HandleSoundEnd(audioSource, clipLength));
    }

    /// <summary>
    /// utilise une audiosource sortie de l'audiopool pour jouer un son. renvoie cette audiosource afin quelle soit mofifiée en temps réel dans le code
    /// </summary>
    /// <param name="choosenSound"></param>
    /// <returns></returns>
    public AudioSource PlaySFXClipModulableSource(Sounds choosenSound)
    {
        AudioClip audioClip = _clips[(int)choosenSound].ClipList[Random.Range(0, _clips[(int)choosenSound].ClipList.Count)];
        AudioSource audioSource = UseFromPool();
        audioSource.clip = audioClip;
        audioSource.Play();
        return audioSource;
    }

    /// <summary>
    /// gère le retour a la pool d'une audiosource s'arrêtant apres la fin de son clip
    /// </summary>
    /// <param name="source"></param>
    /// <param name="clipLength"></param>
    /// <returns></returns>
    IEnumerator HandleSoundEnd(AudioSource source, float clipLength)
    {
        yield return new WaitForSecondsRealtime(clipLength);
        BackToPool(source);
    }
}
