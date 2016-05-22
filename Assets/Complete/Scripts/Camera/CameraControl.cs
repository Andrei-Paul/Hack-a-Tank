using UnityEngine;

namespace Complete
{
    public class CameraControl : MonoBehaviour
    {
        public float m_DampTime = 0.2f;                 // Approximate time for the camera to refocus.
        public float m_ScreenEdgeBuffer = 4f;           // Space between the top/bottom most target and the screen edge.
        public float m_MinSize = 6.5f;                  // The smallest orthographic size the camera can be.
        [HideInInspector] public GameObject[] m_Targets; // All the targets the camera needs to encompass.
        public GravityAttractor Ground;

        public Vector3 GravityDirection;
        public float DistanceFromGroundBefore;
        public float DistanceFromGroundAfter;

        private Camera m_Camera;                        // Used for referencing the camera.
        private float m_ZoomSpeed;                      // Reference speed for the smooth damping of the orthographic size.
        private Vector3 m_MoveVelocity;                 // Reference velocity for the smooth damping of the position.
        private Vector3 m_DesiredPosition;              // The position the camera is moving towards.
        private Quaternion m_DesiredRotation;           // The rotation the camera is moving towards.
        private bool Ready = false;


        private void Awake()
        {
            m_Camera = GetComponentInChildren<Camera>();
        }


        private void FixedUpdate()
        {
            m_Targets = GameObject.FindGameObjectsWithTag("Player");

            // Move the camera towards a desired position.
            Move();

            // Change the size of the camera based.
            Zoom();
        }


        private void Move()
        {
            // Find the average position of the targets.
            FindAveragePositionAndRotation();

            DistanceFromGroundAfter = (m_DesiredPosition - Ground.transform.position).magnitude;

            if (Ready == true)
            {
                // Smoothly transition to that position.
                transform.position = Vector3.SmoothDamp(transform.position, m_DesiredPosition, ref m_MoveVelocity, m_DampTime);

                transform.rotation = Quaternion.Slerp(transform.rotation, m_DesiredRotation, m_DampTime);
            }
        }


        private void FindAveragePositionAndRotation()
        {
            Vector3 averagePos = new Vector3();
            int numTargets = 0;

            // Go through all the targets and add their positions together.
            for (int i = 0; i < m_Targets.Length; i++)
            {
                if (m_Targets[i] != null)
                {
                    // If the target isn't active, go on to the next one.
                    if (!m_Targets[i].gameObject.activeSelf)
                        continue;

                    // Add to the average and increment the number of targets in the average.
                    averagePos += m_Targets[i].transform.position;
                    numTargets++;
                }
            }

            // If there are targets divide the sum of the positions by the number of them to find the average.
            if (numTargets > 0)
            {
                averagePos /= numTargets;
                Ready = true;
            }

            // Keep the same y value.
            // averagePos.y = transform.position.y;
            // averagePos.y = (averagePos.y - Ground.transform.position.y);
            DistanceFromGroundBefore = (averagePos - Ground.transform.position).magnitude;
            GravityDirection = (averagePos - Ground.transform.position).normalized;
            averagePos = averagePos + GravityDirection * Ground.transform.lossyScale.y;

            // The desired position is the average position;
            m_DesiredPosition = averagePos;

            m_DesiredRotation = Quaternion.FromToRotation(transform.forward, -GravityDirection) * transform.rotation;
        }


        private void Zoom()
        {
            // Find the required size based on the desired position and smoothly transition to that size.
            float requiredSize = FindRequiredSize();
            if (Ready == true)
            {
                m_Camera.orthographicSize = Mathf.SmoothDamp(m_Camera.orthographicSize, requiredSize, ref m_ZoomSpeed, m_DampTime);
            }
        }


        private float FindRequiredSize ()
        {
            // Find the position the camera rig is moving towards in its local space.
            Vector3 desiredLocalPos = transform.InverseTransformPoint(m_DesiredPosition);

            // Start the camera's size calculation at zero.
            float size = 0f;

            // Go through all the targets...
            for (int i = 0; i < m_Targets.Length; i++)
            {
                // ... and if they aren't active continue on to the next target.
                if (!m_Targets[i].gameObject.activeSelf)
                    continue;

                // Otherwise, find the position of the target in the camera's local space.
                Vector3 targetLocalPos = transform.InverseTransformPoint(m_Targets[i].transform.position);

                // Find the position of the target from the desired position of the camera's local space.
                Vector3 desiredPosToTarget = targetLocalPos - desiredLocalPos;

                // Choose the largest out of the current size and the distance of the tank 'up' or 'down' from the camera.
                size = Mathf.Max(size, Mathf.Abs(desiredPosToTarget.y));

                // Choose the largest out of the current size and the calculated size based on the tank being to the left or right of the camera.
                size = Mathf.Max(size, Mathf.Abs(desiredPosToTarget.x) / m_Camera.aspect);
            }

            // Add the edge buffer to the size.
            size += m_ScreenEdgeBuffer;

            // Make sure the camera's size isn't below the minimum.
            size = Mathf.Max (size, m_MinSize);

            return size;
        }


        public void SetStartPositionAndSize ()
        {
            // Find the desired position.
            FindAveragePositionAndRotation ();

            // Set the camera's position to the desired position without damping.
            transform.position = m_DesiredPosition;

            // Find and set the required size of the camera.
            m_Camera.orthographicSize = FindRequiredSize ();
        }
    }
}