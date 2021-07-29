using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour, ILiveObj, ITakeDamage
{
    [SerializeField] private HealthLine healthLine;

    private Animator animator;
    private NotificationManager notificationManager;
    private AudioSource backgroundMusicAudioSource;

    public bool blueKeyCollected { get; set; } = true;
    public bool orangeKeyCollected { get; set; } = true;
    public int maxHP { get; set; }
    public int currentHP { get; set; }
    public bool IsAlive { get; set; }

    private void Awake()
    {
        IsAlive = true;
        maxHP = 100;
        currentHP = maxHP;

        animator = GetComponent<Animator>();
        notificationManager = GetComponent<NotificationManager>();
        Cursor.lockState = CursorLockMode.Locked;

        backgroundMusicAudioSource = GameObject.Find("BackgroundMusic").GetComponent<AudioSource>();
    }

    private void FixedUpdate()
    {
        if (currentHP <= 10)
        {
            Debug.Log("CurrentHP <= 10");
            backgroundMusicAudioSource.pitch = Mathf.Lerp(backgroundMusicAudioSource.pitch, 1.3f, Time.fixedDeltaTime);
            //backgroundMusicAudioSource.pitch = 1.3f;
        }
        else if (currentHP <= 25)
        {
            Debug.Log("CurrentHP <= 25");
            backgroundMusicAudioSource.pitch = Mathf.Lerp(backgroundMusicAudioSource.pitch, 1.2f, Time.fixedDeltaTime);
            //backgroundMusicAudioSource.pitch = 1.2f;
        }
        else if (currentHP <= 50)
        {
            Debug.Log("CurrentHP <= 50");
            backgroundMusicAudioSource.pitch = Mathf.Lerp(backgroundMusicAudioSource.pitch, 1.1f, Time.fixedDeltaTime);
            //backgroundMusicAudioSource.pitch = 1.1f;
        }
        else if (currentHP >= 100)
        {
            Debug.Log("CurrentHP >= 100");
            backgroundMusicAudioSource.pitch = Mathf.Lerp(backgroundMusicAudioSource.pitch, 1f, Time.fixedDeltaTime);
            //backgroundMusicAudioSource.pitch = 1f;
        }
    }

    public void TakeDamage(int damage)
    {
        Debug.Log($"{name} : - {damage} HP!");
        currentHP -= damage;
        healthLine.DecreaseHealthlineValue(damage);

        if (currentHP <= 0 && IsAlive) StartCoroutine(DeathAnimation());
    }

    public void TakeHeal()
    {
        currentHP += 15;
        healthLine.IncreaseHealthlineValue(15);
        if (currentHP >= maxHP) currentHP = maxHP;
        Debug.Log($"{name} : HEALED!!! +15HP");
    }

    IEnumerator DeathAnimation()
    {
        animator.SetTrigger("Death");
        IsAlive = false;
        //gameObject.GetComponent<CapsuleCollider>().enabled = false;
        yield return new WaitForSeconds(2);
        notificationManager.ShowNotification("бШ сахрш!");
    }
}
