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
    
    float nextShotTime = 0f;
    float timerforitem = 0f;
    float MAX = 10f;

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

    void Update()
    {
     timerforitem = timerforitem + Time.deltaTime;
    
    if (timerforitem >= MAX)
    {
        eatCandy(false);
        eatApple(false);
    }


    }

    void OnTriggerEnter (Collider collidedObject)
    {
    switch (collidedObject.tag) 
        {
        case "Candy":
            eatCandy(true);
            break;

        case "Apple":
            eatApple(true);
            break;
        }


    }

    void eatCandy(bool boolen)
    {
        float first;

        if (boolen == true)
        {
            Debug.Log("사탕과 충돌");
            first = msBetweenShots;
            msBetweenShots = 300;
            timerforitem = 0;
        }

        else
        {
            msBetweenShots = 500;
        }
    }

    void eatApple(bool boolen)
    {
        if (boolen == true)
        {
            Debug.Log("사과와 충돌");
            EquipProjectile(appleProjectile);
            timerforitem = 0;
        }

        else
        {
            EquipProjectile(startingProjectile);
        }
    }

}
