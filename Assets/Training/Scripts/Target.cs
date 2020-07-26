using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour, IDamageable
{
    public float startingHealth = 1;
	protected float health;
	public bool dead = false;

	protected virtual void Start() {
		health = startingHealth;
	}

	public void TakeHit(float damage, RaycastHit hit) {
		health -= damage;

		if (health <= 0 && !dead) {
			Die();
		}
	}

	public void Die() {
		dead = true;
		GameObject.Destroy (gameObject);
	}
}
