using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class squidGreen : MonoBehaviour {

    private Animator anim;
    private Attacker attacker;

    // Use this for initialization
    void Start() {
        anim = GetComponent<Animator>();
        attacker = GetComponent<Attacker>();
    }

    // Update is called once per frame
    void Update() {

    }

    private void OnTriggerEnter2D(Collider2D collision) {


        GameObject obj = collision.gameObject;

        // Leave the mothod if not coliiding with defender
        if (!obj.GetComponent<Defender>()) {
            return;
        }

        anim.SetBool("isAttacking", true);
        attacker.Attack(obj);

    }

}