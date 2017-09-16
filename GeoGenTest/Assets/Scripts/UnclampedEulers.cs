using UnityEditor;
using UnityEngine;
[AddComponentMenu("Transform/UnclampedEulers")]
public class UnclampedEulers : MonoBehaviour
{
    public Vector3 angles;

    public static Quaternion Lerp(Quaternion start, UnclampedEulers end, float fraction)
    {
        var fractionalEndAngleX = end.angles.x * fraction;
        var fractionalEndAngleY = end.angles.y * fraction;
        var fractionalEndAngleZ = end.angles.z * fraction;

        var endQx = Quaternion.AngleAxis(fractionalEndAngleX, Vector3.right);
        var endQy = Quaternion.AngleAxis(fractionalEndAngleY, Vector3.up);
        var endQz = Quaternion.AngleAxis(fractionalEndAngleZ, Vector3.forward);

        return start * endQx * endQy * endQz;
    }
}

[CustomEditor(typeof(Transform))]
public class UnclampedEulersEdit : Editor
{
    Vector3 eulerAngles;

    public override void OnInspectorGUI()
    {

        var targetTransform = (Transform)target;
        var Ueulers = targetTransform.gameObject.GetComponent<UnclampedEulers>();

        eulerAngles = Ueulers != null ? Ueulers.angles : targetTransform.localRotation.eulerAngles;

        targetTransform.localPosition = EditorGUILayout.Vector3Field("Position", targetTransform.localPosition);
        eulerAngles = EditorGUILayout.Vector3Field("Rotation", eulerAngles);
        targetTransform.localScale = EditorGUILayout.Vector3Field("Scale", targetTransform.localScale);

        if (Ueulers != null) Ueulers.angles = eulerAngles;
        else targetTransform.localRotation = Quaternion.Euler(eulerAngles);
    }

}