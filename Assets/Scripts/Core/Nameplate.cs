using UnityEngine;
using UnityEngine.UI;

public class Nameplate : MonoBehaviour
{
    private Camera cam;
    private Transform target;
    [SerializeField] private float viewDistance = 10.0f;
    [SerializeField] private Text text;

    private void Awake()
    {
        cam = Camera.main;
    }

    private void Start()
    {
        target = PlayerManager.instance.player.transform;
    }

    private void LateUpdate()
    {
        //Get distance to player and look at him if in range
        float distance = Vector3.Distance(target.position, transform.position);
        if (distance <= viewDistance)
        {
            transform.LookAt(transform.position + cam.transform.forward);
            text.enabled = enabled;
        }
        else
        {
            text.enabled = false;
        }
    }
}