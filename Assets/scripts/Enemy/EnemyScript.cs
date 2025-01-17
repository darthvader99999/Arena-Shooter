﻿using UnityEngine;

/// <summary>
/// Enemy generic behavior
/// </summary>
public class EnemyScript : MonoBehaviour
{
	private bool hasSpawn;
	private EnemyMoveScript moveScript;
	private WeaponScript[] weapons;
	private Collider2D coliderComponent;
	private SpriteRenderer rendererComponent;

	void Awake()
	{
		// Retrieve the weapon only once
		weapons = GetComponentsInChildren<WeaponScript>();

		// Retrieve scripts to disable when not spawn
		moveScript = GetComponent<EnemyMoveScript>();

		coliderComponent = GetComponent<Collider2D>();

		rendererComponent = GetComponent<SpriteRenderer>();
	}

	// 1 - Disable everything
	void Start()
	{
		hasSpawn = false;

		// Disable everything
		// -- collider
		coliderComponent.enabled = false;
		// -- Moving
		moveScript.enabled = false;
		// -- Shooting
		foreach (WeaponScript weapon in weapons)
		{
			weapon.enabled = false;
		}
	}

	void Update()
	{
		// 2 - Check if the enemy has spawned.
		if (hasSpawn == false)
		{
			if (rendererComponent.IsVisibleFrom(Camera.main))
			{
				Spawn();
			}
		}
		else
		{

			// 4 - Out of the camera ? Destroy the game object.
			if (rendererComponent.IsVisibleFrom(Camera.main) == false)
			{
				Destroy(gameObject);
			}
		}
		foreach (WeaponScript weapon in weapons)
		{
			// Auto-fire
			if (weapon != null && weapon.CanAttack)
			{
				weapon.Attack(true);
			}
		}
	}

	// 3 - Activate itself.
	private void Spawn()
	{
		hasSpawn = true;

		// Enable everything
		// -- Collider
		coliderComponent.enabled = true;
		// -- Moving
		moveScript.enabled = true;
		// -- Shooting
		foreach (WeaponScript weapon in weapons)
		{
			weapon.enabled = true;
		}
	}
}