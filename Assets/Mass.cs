using UnityEngine;
using System.Collections;

public class Mass : MonoBehaviour
{
    public GravityAttractor Attractor;
    private Rigidbody Body;

    // Use this for initialization
    void Start()
    {
        this.Body = GetComponent<Rigidbody>();
        this.Body.useGravity = false;
    }

    // Update is called once per frame
    void Update()
    {
        Attractor.Attract(this.transform);
    }
}
