using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour {

    public GameObject projectile, gun;

    private Animator animator;
    private Spawner myLaneSpawner;
    private GameObject projectileParent;

    private void Start() {
        animator = GetComponent<Animator>();

        projectileParent = GameObject.Find("Projectiles");
        if (!projectileParent) {
            projectileParent = new GameObject("Projectiles");
        }
        SetMyLaneSpawner();
    }

    void Update() {
        if (IsAttackerAheadInLane()) {
            animator.SetBool("isAttacking", true);
        } else {
            animator.SetBool("isAttacking", false);
        }
    }

    //Look through all spawners, and set it 

    void SetMyLaneSpawner() {
        Spawner[] spawnerArray = GameObject.FindObjectsOfType<Spawner>();

        foreach(Spawner spawner in spawnerArray) {
            if(spawner.transform.position.y == transform.position.y) {
                myLaneSpawner = spawner;
                return;
            }
        }
        Debug.LogError(name + " can't find spawner in lane");
    }

    bool IsAttackerAheadInLane() {
        // Exit is no attackers in lane
        if(myLaneSpawner.transform.childCount <= 0) {
            return false;
        }
        // If there are attackers, are they ahead?
        foreach (Transform attacker in myLaneSpawner.transform) {
            if (attacker.transform.position.x > transform.position.x) {
                return true;
            }
        }
    //Attackers in lane but behind us
    return false;
    }

    private void Fire() {

        GameObject newProjectile = Instantiate(projectile) as GameObject;
        newProjectile.transform.parent = projectileParent.transform;
        newProjectile.transform.position = gun.transform.position;

    }
}
