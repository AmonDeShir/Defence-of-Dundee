using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public GameObject bullet;
    public FlagTimer canShoot;

    [SerializeField]
    private Transform bulletSpawnPoint;

    public float fireRate;

    public void Start() {
        canShoot = new FlagTimer(1/fireRate);
        canShoot.Start();
    }

    public void SpawnBullet() {
        if (canShoot.HasFinishedCounting) {
            Instantiate(bullet, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
            canShoot.Start();
        }
    }

    public void Update() {
        canShoot.Update();
    }
}
