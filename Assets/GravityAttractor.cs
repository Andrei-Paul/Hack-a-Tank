using UnityEngine;
using System.Collections;

public class GravityAttractor : MonoBehaviour
{
    public float Gravity = Physics.gravity.y;
    public Vector3 GravityDirection;
    public Vector3 BodyDirection;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Attract(Transform body)
    {
        GravityDirection = (body.position - this.transform.position).normalized;
        BodyDirection = body.up;

        (body.GetComponent<Rigidbody>()).AddForce(Gravity * GravityDirection);
        Quaternion targetRotation = Quaternion.FromToRotation(BodyDirection, GravityDirection) * body.rotation;

        body.rotation = Quaternion.Slerp(body.rotation, targetRotation, 50 * Time.deltaTime);

//        RectTransform canvasBody = body.GetComponent<RectTransform>();
//        Quaternion canvasTargetRotation = Quaternion.FromToRotation(BodyDirection, GravityDirection) * canvasBody.rotation;
//        canvasBody.rotation = Quaternion.Slerp(canvasBody.rotation, targetRotation, 50 * Time.deltaTime);
    }
}