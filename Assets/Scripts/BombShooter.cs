using UnityEngine;
using System.Collections;

public class BombShooter : MonoBehaviour
{
    public GameObject bombPrefab;
    public Transform spawnPoint;

    public int maxAmmo = 5;
    public int currentAmmo;

    public AudioClip reloadSound;
    private AudioSource audioSource;

    public bool canReload = true;
    public bool Reloaded = true;
    private bool isReloading = false;

    public float cooldown = 0.5f;
    private bool canShoot = true;

    void Start()
    {

        audioSource = GetComponent<AudioSource>();

        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();

        currentAmmo = maxAmmo;
    }

    void Update()
    {
        // can't shoot when reload
        if (Input.GetKeyDown(KeyCode.Space) && canShoot && currentAmmo > 0 && !isReloading)
        {
            Shoot();
            StartCoroutine(Cooldown());
        }

        if (Input.GetKeyDown(KeyCode.R) && canReload && currentAmmo < maxAmmo && !isReloading)
        {
            StartCoroutine(ReloadCooldown());
        }
    }

    void Shoot()
    {
        GameObject bomb = Instantiate(bombPrefab, spawnPoint.position, Quaternion.identity);

        // ยิงไปทิศที่หันล่าสุด
        Vector3 dir = transform.forward;

        bomb.GetComponent<Rigidbody>().AddForce(dir * 10f, ForceMode.Impulse);

        currentAmmo--;
    }


    IEnumerator ReloadCooldown()
    {
        isReloading = true;
        canReload = false;

        if (reloadSound != null)
        {
            audioSource.pitch = Random.Range(0.8f, 1.2f);
            audioSource.PlayOneShot(reloadSound);
        }

        yield return new WaitForSeconds(2f);

        currentAmmo = maxAmmo;

        isReloading = false;
        canReload = true;

        canShoot = true;
    }

    IEnumerator Cooldown()
    {
        canShoot = false;

        yield return new WaitForSeconds(cooldown);

        // ❗ ถ้ายัง reload อยู่ ห้ามเปิดยิง
        if (!isReloading)
            canShoot = true;
    }

    void Reload()
    {
        if (reloadSound != null)
        {
            audioSource.pitch = Random.Range(0.8f, 1.2f);
            audioSource.PlayOneShot(reloadSound);
        }
        currentAmmo = maxAmmo;
                    canShoot = true;

    }
}