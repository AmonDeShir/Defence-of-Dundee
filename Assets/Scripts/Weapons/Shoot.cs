using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public GameObject bullet;
    public FlagTimer canShoot;

    public Transform bulletSpawnPoint;

    public float fireRate;

    [SerializeField]
    private AudioSource sound;

    public void Start() {
        canShoot = new FlagTimer(1/fireRate);
        canShoot.Start();
    }

    public void SpawnBullet() {
        if (canShoot.HasFinishedCounting) {
            Instantiate(bullet, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
            canShoot.Start();
            sound.Play();
        }
    }

    public void Update() {
        canShoot.Update();
    }
}
