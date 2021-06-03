using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShootingController : MonoBehaviour
{
    [SerializeField]
    [Range(0.2f, 1.5f)]
    private float fireRate = 0.2f;

    [SerializeField]
    [Range(1, 10)]
    private int damage = 1;

    [SerializeField]
    private float maxShootDistance = 100f;

    public Transform firepoint;
    public ParticleSystem muzzleParticle;
    public GameObject bulletPrefab;
    public AudioSource assaultShootAudio;
    public float bulletSpeed = 40f;

    private PlayerHealth playerHealth;

    [Header("Ammo Management")]
    public int currentTotalAmmo;
    public int currentChamberAmmo;
    public int totalChamberAmmo;
    private int substractAmmo;

    public Text currentAmmoText;
    public Text remainingAmmoText;

    public AudioSource reloadClip;
    public AudioSource ammoClip;

    private float timer;

    // Start is called before the first frame update
    void Start()
    {
        playerHealth = GetComponent<PlayerHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateHUD();
        //if (!playerHealth.isDead)
        //{
            timer += Time.deltaTime;
            if (timer >= fireRate)
            {
                if (Input.GetButton("Fire1"))
                {
                    timer = 0f;
                   
                    AmmoManagement();
                    substractAmmo = totalChamberAmmo - currentChamberAmmo;
                }
            }
            if (Input.GetKeyDown(KeyCode.R))
            {
                Reload();
            }
        //}
    }

    private void AmmoManagement()
    {
        if (currentChamberAmmo > 0)
        {
            currentChamberAmmo--;
            var bullet = (GameObject)Instantiate(bulletPrefab, firepoint.position, firepoint.rotation);
            bullet.GetComponent<Rigidbody>().velocity = bullet.transform.forward * bulletSpeed;
            FireGun();
        }
        else
        {
            if (currentTotalAmmo > 0)
            {
                Reload();
            }
        }
    }

    private void FireGun()
    {
        muzzleParticle.Play();
        assaultShootAudio.Play();
    }

    private void Reload()
    {
        if (currentTotalAmmo <= 0)
        {
            // Write message "No ammunition" in center of screen
        }
        else
        {
            if (currentChamberAmmo == totalChamberAmmo)
            {
                Debug.Log("Full ammo, can't reload");
            }
            else
            {
                Debug.Log("Reload");
                reloadClip.Play();
                if (currentTotalAmmo <= totalChamberAmmo)
                {

                    if (substractAmmo >= currentTotalAmmo)
                    {
                        currentChamberAmmo += currentTotalAmmo;
                        currentTotalAmmo = 0;
                    }
                    else
                    {
                        currentTotalAmmo -= substractAmmo;
                        currentChamberAmmo += substractAmmo;
                    }
                }
                else
                {
                    currentTotalAmmo -= substractAmmo;
                    currentChamberAmmo += substractAmmo;

                }
                substractAmmo = 0;
            }
        }
    }

    public void AddAmmo(int ammo)
    {
        ammoClip.Play();
        currentTotalAmmo += ammo;
    }

    private void UpdateHUD()
    {
        currentAmmoText.text = currentChamberAmmo.ToString();
        remainingAmmoText.text = "/" + currentTotalAmmo.ToString();
    }
}
