using UnityEngine;
using System.Collections;

public class Mass : MonoBehaviour
{
    public GravityAttractor Attractor;
    private Rigidbody Body;
    public float DistanceFromAttractor;

    // Use this for initialization
    void Start()
    {
        this.Body = GetComponent<Rigidbody>();
        this.Body.useGravity = false;
        this.DistanceFromAttractor = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        DistanceFromAttractor = Attractor.Attract(this.transform);
    }
}
