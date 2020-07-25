using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotController : MonoBehaviour
{
    public Transform muzzle;
    public Projectile startingProjectile;
    public float msBetweenShots = 100;
    public float muzzleVelocity = 35;
    Projectile equippedProjectile;
    float nextShotTime = 0;

    void Start() {
        if(startingProjectile != null ) {
            EquipProjectile(startingProjectile);
        }
    }
    public void EquipProjectile(Projectile projectileToEquip) {
        equippedProjectile = projectileToEquip;
    }

    public void Shoot() {
        if(equippedProjectile != null && Time.time > nextShotTime){
            nextShotTime = Time.time + msBetweenShots / 1000;
            Projectile newProjectile = Instantiate(equippedProjectile, muzzle.position, muzzle.rotation);
            newProjectile.SetSpeed(muzzleVelocity);
        }
        
    }
}
