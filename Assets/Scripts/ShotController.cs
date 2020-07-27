using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotController : MonoBehaviour
{
    public Transform muzzle;
    public Projectile startingProjectile;
    public Projectile appleProjectile;
    public float msBetweenShots = 500;
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

    void OnTriggerEnter (Collider collidedObject)
    {
    switch (collidedObject.tag) 
        {
        case "Candy":
            Debug.Log("사탕과 충돌");
            float first = msBetweenShots;
            msBetweenShots = 300;

            // float timer = 0f;
            // float waitingTime = 1f;

            // while (timer>10f){
            //     timer += Time.deltaTime;

            // }
            // Debug.Log("사탕과 충돌 1000f");

            

            // if ( timer >= waitingTime) 
            // {
            //     Debug.Log("사탕과 충돌 1000f");
            //     msBetweenShots = first;
            // }

            break;

        case "Apple":
        Debug.Log("사과와 충돌");
           EquipProjectile(appleProjectile);
            break;
        }


    }

    // void eatCandyreset()
    // {
    //     Debug.Log("사탕과 충돌");
    //     msBetweenShots = msBetweenShots/2;
    // }

    // void eatApple()
    // {
    //     Debug.Log("사과와 충돌");
    //     EquipProjectile(appleProjectile);
    // }

}
