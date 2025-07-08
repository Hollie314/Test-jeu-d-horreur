using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GunSystem : MonoBehaviour
{
    public float damage = 10f;
    public float range = 100f;
    public float fireRate = 15;

    public GameObject fpsCam;
    public ParticleSystem muzzleFlash;
    public GameObject impactEffect;
    public GameObject bulletCasing;
    public Transform casinglocation;
    public AudioSource weaponSound;
    public AudioSource noAmmoSound;
    public AudioSource reloadSound;

    public Animator anim;
    public Vector3 reloading;
    public float reloadTime = 3;
    public Vector3 upRecoil;
    Vector3 originalRotation;

    public float amount;
    public float maxAmount;
    public float smoothAmount;

    private Vector3 initialPosition;

    public GameObject ammoText;

    private int currentAmmo;
    public int magazineSize = 10;
    public int ammoCache = 20;
    private int maxAmmo;
    public int ammoNeeded;  // <-- Changed from private to public

    public bool semi;
    public bool auto;
    public bool melee;

    public bool casingForward;
    public bool casingBackwards;

    private bool isreloading;
    private bool canShoot;

    private float nextTimeToFire = 0f;

    void Start()
    {
        currentAmmo = magazineSize;
        maxAmmo = magazineSize;

        isreloading = false;
        canShoot = true;

        originalRotation = transform.localEulerAngles;
        initialPosition = transform.localPosition;
    }

    void Update()
    {
        // Semi auto fire
        if (Input.GetButtonDown("Fire1") && Time.time >= nextTimeToFire && semi && magazineSize > 0 && canShoot)
        {
            AddRecoil();
            nextTimeToFire = Time.time + 1f / fireRate;
            anim.SetBool("shoot", true);
            Invoke("setboolback", .5f);
            Shoot();
        }

        // Melee attack
        if (Input.GetButtonDown("Fire1") && Time.time >= nextTimeToFire && melee)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            anim.SetBool("melee", true);
            Invoke("setboolback", .5f);
            Melee();
        }

        // Auto fire
        if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire && magazineSize > 0 && auto && canShoot)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            anim.SetBool("shoot", true);
            Invoke("setboolback", .5f);
            AddRecoilAuto();
            Shoot();
        }

        // No ammo sound
        if (Input.GetButton("Fire1") && magazineSize == 0)
        {
            noAmmoSound.Play();
        }
        else if (Input.GetButtonUp("Fire1") && canShoot)
        {
            StopRecoil();
        }

        // Ammo UI and animation for empty mag
        if (magazineSize == 0)
        {
            ammoText.GetComponent<Text>().text = "Reload";
            anim.SetBool("empty", true);
        }
        else
        {
            anim.SetBool("empty", false);
        }

        // Reloading logic
        if (Input.GetButtonDown("reload") && magazineSize == 0 && ammoCache > 0)
        {
            canShoot = false;
            ammoCache -= ammoNeeded;
            magazineSize += ammoNeeded;
            ammoNeeded = 0;
            isreloading = true;
            StartCoroutine(ReloadTimer());
        }
        else if (isreloading)
        {
            return;
        }
        if (Input.GetButtonDown("reload") && ammoCache == 0)
        {
            return;
        }

        // Update ammo UI text
        ammoText.GetComponent<Text>().text = magazineSize + " / " + ammoCache;

        // Weapon sway
        float movementX = -Input.GetAxis("Mouse X") * amount;
        float movementY = -Input.GetAxis("Mouse Y") * amount;
        movementX = Mathf.Clamp(movementX, -maxAmount, maxAmount);
        movementY = Mathf.Clamp(movementY, -maxAmount, maxAmount);

        Vector3 finalPosition = new Vector3(movementX, movementY, 0);
        transform.localPosition = Vector3.Lerp(transform.localPosition, finalPosition + initialPosition, Time.deltaTime * smoothAmount);
    }

    void Shoot()
    {
        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            magazineSize--;
            ammoNeeded++;

            muzzleFlash.Play();
            weaponSound.Play();

            GameObject casing = Instantiate(bulletCasing, casinglocation.position, casinglocation.rotation);
            Destroy(casing, 2f);

            Target target = hit.transform.GetComponent<Target>();
            if (target != null)
            {
                target.TakeDamage(damage);
            }

            GameObject impactOB = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impactOB, 2f);

            if (casingForward)
            {
                casing.GetComponent<Rigidbody>().AddForce(transform.forward * 250);
            }

            if (casingBackwards)
            {
                casing.GetComponent<Rigidbody>().AddForce(transform.forward * -250);
            }
        }
    }

    void Melee()
    {
        weaponSound.Play();
        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            Target target = hit.transform.GetComponent<Target>();
            if (target != null)
            {
                target.TakeDamage(damage);
            }
        }
    }

    public void setboolback()
    {
        anim.SetBool("shoot", false);
        anim.SetBool("melee", false);
    }

    public void AddRecoil()
    {
        if (canShoot)
        {
            transform.localEulerAngles += upRecoil;
            StartCoroutine(StopRecoilSemi());
        }
    }

    public void AddRecoilAuto()
    {
        if (canShoot)
        {
            transform.localEulerAngles += upRecoil;
            StartCoroutine(StopRecoilSemi());
        }
    }

    public void StopRecoil()
    {
        transform.localEulerAngles = originalRotation;
    }

    IEnumerator StopRecoilSemi()
    {
        yield return new WaitForSeconds(.1f);
        transform.localEulerAngles = originalRotation;
    }

    IEnumerator ReloadTimer()
    {
        reloadSound.Play();
        transform.localEulerAngles += reloading;
        yield return new WaitForSeconds(reloadTime);
        isreloading = false;
        canShoot = true;
        transform.localEulerAngles = originalRotation;
    }
}
