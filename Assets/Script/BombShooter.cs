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
        if (Input.GetKeyDown(KeyCode.Space) && canShoot && currentAmmo > 0)
        {
            Shoot();
            StartCoroutine(Cooldown());
        }

        if (Input.GetKeyDown(KeyCode.R) && canReload && currentAmmo < 5)
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
        canReload = false;

        Reload();

        yield return new WaitForSeconds(2f);

        canReload = true;
    }

    IEnumerator Cooldown()
    {
        canShoot = false;
        yield return new WaitForSeconds(cooldown);
        canShoot = true;
    }

    void Reload()
    {
        if (reloadSound != null)
        {
            audioSource.pitch = Random.Range(0.8f, 1.2f);
            audioSource.PlayOneShot(reloadSound);
        }
        AudioSource.PlayClipAtPoint(reloadSound, transform.position);

        currentAmmo = maxAmmo;
    }
}