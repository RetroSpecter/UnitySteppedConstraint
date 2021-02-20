using UnityEngine;
using UnityEngine.Animations.Rigging;

public class SteppedConstraintController : MonoBehaviour
{
    public float fps = 60;

    void Update()
    {
        UpdateFramerate(transform.GetChild(0));
    }

    void UpdateFramerate(Transform curBlend)
    {
        if (!curBlend.TryGetComponent(out SteppedConstraint constraint)) {
            Debug.LogError("SteppedConstraint not found");
        }

        constraint.data.fps = fps;
        for (int i = 0; i < curBlend.childCount; i++) {
            UpdateFramerate(curBlend.GetChild(i));
        }        
    }
}
