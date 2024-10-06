using UnityEngine;
using UnityEditor;

    [CustomEditor(typeof(Avoider))]
    public class FieldOfViewEditor : Editor
    {
        private void OnSceneGUI()
        {
            Avoider avoider = (Avoider)target;
            if (avoider.toggleGizmos)
            {
                Handles.color = Color.white;
                Handles.DrawWireArc(avoider.transform.position, Vector3.up, Vector3.forward, 360, avoider.radius);

                Vector3 viewAngle01 = DirectionFromAngle(avoider.transform.eulerAngles.y, -avoider.angle / 2);
                Vector3 viewAngle02 = DirectionFromAngle(avoider.transform.eulerAngles.y, avoider.angle / 2);

                Handles.color = Color.yellow;
                Handles.DrawLine(avoider.transform.position, avoider.transform.position + viewAngle01 * avoider.radius);
                Handles.DrawLine(avoider.transform.position, avoider.transform.position + viewAngle02 * avoider.radius);

                if (avoider.canSeePlayer)
                {
                    Handles.color = Color.green;
                    Handles.DrawLine(avoider.transform.position, avoider.player.transform.position);
                }
            }
        }

        private Vector3 DirectionFromAngle(float eulerY, float angleInDegrees)
        {
            angleInDegrees += eulerY;

            return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
        }
    }

