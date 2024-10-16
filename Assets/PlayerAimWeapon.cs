using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAimWeapon : MonoBehaviour
{
    public GameObject projectile;
    public Transform shotPoint;
    public ParticleSystem _muzzleFlash;
    public ParticleSystem _backMuzzleFlash;
    public Transform player;

    public static float weaponAngle;

    ObjectPooler objectPooler;

    void Start()
    {
        objectPooler = ObjectPooler.Instance;
    }

    void EmitMuzzleFlash()
    {
        _muzzleFlash.Emit(50);
    }

    void EmitBackMuzzleFlash()
    {
        _backMuzzleFlash.Emit(30);
    }


    public void aimRocket()
    {
        if (weaponAngle < -90 && weaponAngle > -270)
        {
            transform.rotation = Quaternion.Euler(new Vector3(180, 0, -weaponAngle));
        }
        else
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, weaponAngle));
        }

        if(transform.rotation.z > 0)
        {
            player.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
        } else if (transform.rotation.z < 0)
        {
            player.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        }
    }

    public void ShotRocket()
    {
        if (Settings.sfxOn && GameController.fistShot)
        {
            FindObjectOfType<GameController>().Play("Fist Shot");
        } else if (Settings.sfxOn)
        {
            FindObjectOfType<GameController>().Play("Rocket Fire" + Random.Range(1, 3));
        }

        if (GameController.tripleShot == false)
        {
            objectPooler.SpawnFromPool("Rocket", shotPoint.position, transform.rotation);
            StatsText.rocketsFired++;

            EmitMuzzleFlash();
            EmitBackMuzzleFlash();

        }
        else if (GameController.tripleShot == true)
        {
            objectPooler.SpawnFromPool("Rocket", shotPoint.position, transform.rotation);
            objectPooler.SpawnFromPool("Rocket", shotPoint.position, Quaternion.Euler(new Vector3(transform.rotation.x, transform.rotation.y, weaponAngle + 20)));
            objectPooler.SpawnFromPool("Rocket", shotPoint.position, Quaternion.Euler(new Vector3(transform.rotation.x, transform.rotation.y, weaponAngle - 20)));

            StatsText.rocketsFired = StatsText.rocketsFired + 3;

            EmitMuzzleFlash();
            EmitBackMuzzleFlash();
        }
    }
}
