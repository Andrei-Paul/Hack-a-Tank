using UnityEngine;
using System.Collections;

public class GravityAttractor : MonoBehaviour
{
    public float Gravity = Physics.gravity.y;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public float Attract(Transform body)
    {
        Vector3 GravityDirection = (body.position - this.transform.position).normalized;
        Vector3 BodyDirection = body.up;

        (body.GetComponent<Rigidbody>()).AddForce(Gravity * GravityDirection);
        Quaternion targetRotation = Quaternion.FromToRotation(BodyDirection, GravityDirection) * body.rotation;

        body.rotation = Quaternion.Slerp(body.rotation, targetRotation, 50 * Time.deltaTime);

        return (body.position - this.transform.position).magnitude;
    }
}