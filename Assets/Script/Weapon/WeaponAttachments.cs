using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAttachments : MonoBehaviour
{
    public GameObject theWeapon;

    public GameObject originalMagOB;
    public GameObject extendedMagOB;
    public GameObject orignalFirepowerOB;
    public GameObject firepowerOB;
    public GameObject fastReloadOB;
    public GameObject fireRateOB;

    public int ExtMagSize = 25;
    public float firepowerMultiplyer = 50;
    public float newRate = 5;
    public float reloadTime = 2;

    public bool extendedMag;
    public bool firepower;
    public bool fastReload;
    public bool fireRate;

    private bool ExtMagOff;
    private bool ExtMagOn;

    private bool fpOff;
    private bool fpOn;

    private bool fastMagOff;
    private bool fastMagOn;

    private bool fireRateOff;
    private bool fireRateOn;

    void Start()
    {
        extendedMag = false;
        firepower = false;
        fastReload = false;

        ExtMagOff = true;
        ExtMagOn = false;

        fpOff = true;
        fpOn = false;

        fireRateOff = true;
        fireRateOn = false;

        fastMagOff = true;
        fastMagOn = false;
    }

    void Update()
    {
        if (extendedMagOB.activeInHierarchy)
        {
            originalMagOB.SetActive(false);
        }

        if (firepowerOB.activeInHierarchy)
        {
            orignalFirepowerOB.SetActive(false);
        }

        if (extendedMagOB.activeInHierarchy && ExtMagOff)
        {
            extendedMag = true;
        }

        if (extendedMag && ExtMagOff)
        {
            AddAmmo();
        }

        if (firepowerOB.activeInHierarchy && fpOff)
        {
            firepower = true;
        }

        if (firepower)
        {
            AddDamage();
        }

        if (fireRateOB.activeInHierarchy && fireRateOff)
        {
            fireRate = true;
        }

        if (fireRate)
        {
            AddFireRate();
        }

        if (fastReloadOB.activeInHierarchy && fastMagOff)
        {
            fastReload = true;
        }

        if (fastReload)
        {
            AddFastReload();
        }
    }

    public void AddAmmo()
    {
        ExtMagOff = false;
        ExtMagOn = false;

        GunSystem gun = theWeapon.GetComponent<GunSystem>();

        gun.magazineSize = ExtMagSize;
        gun.ammoNeeded = 0;  // now accessible because ammoNeeded is public
    }

    public void AddDamage()
    {
        fpOff = false;
        fpOn = true;
        GunSystem gun = theWeapon.GetComponent<GunSystem>();

        gun.damage += firepowerMultiplyer;
    }

    public void AddFastReload()
    {
        fastMagOff = false;
        fastMagOn = true;

        GunSystem gun = theWeapon.GetComponent<GunSystem>();

        gun.reloadTime = reloadTime;
    }

    public void AddFireRate()
    {
        fireRateOff = false;
        fireRateOn = true;

        GunSystem gun = theWeapon.GetComponent<GunSystem>();

        gun.fireRate = newRate;
    }
}
