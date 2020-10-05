using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Nameplate : MonoBehaviour
{
    Camera cam;
    Transform target;
    [SerializeField] float viewDistance = 10.0f;
    [SerializeField] Text text;

    private void Awake() {
        cam = Camera.main;
    }

    private void Start() {
        target = PlayerManager.instance.player.transform;
    }

    private void LateUpdate() {
        float distance = Vector3.Distance(target.position, transform.position);
        if(distance <= viewDistance) {
            transform.LookAt(transform.position + cam.transform.forward);
            text.enabled = enabled;
        } else {
            text.enabled = false;
        }
    }
}
