using System;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityEngine.Animations;
using Unity.Burst;

[BurstCompile]
public struct SteppedJob : IWeightedAnimationJob
{
    public FloatProperty jobWeight { get; set; }

    public ReadWriteTransformHandle driven;
    public ReadWriteTransformHandle target;

    public AffineTransform offset;
    public FloatProperty fps;
    public FloatProperty deltaTime;

    public float rateAhead;

    public void ProcessRootMotion(AnimationStream stream) { }

    public void ProcessAnimation(AnimationStream stream)
    {
        float w = jobWeight.Get(stream);
        if (w > 0f) {
            rateAhead += deltaTime.Get(stream);
            if (rateAhead > (1 / fps.Get(stream))) {
                driven.SetLocalPosition(stream, target.GetLocalPosition(stream));
                driven.SetRotation(stream, target.GetRotation(stream));

                rateAhead = 0;
            }
        }
        else
            AnimationRuntimeUtils.PassThrough(stream, driven);
    }
}

[Serializable]
public struct SteppedData : IAnimationJobData
{
    [SyncSceneToStream] public Transform source;
    [SyncSceneToStream] public Transform target;
    [HideInInspector][SyncSceneToStream] public float deltaTime;

    [SyncSceneToStream]  public float fps;

    [SyncSceneToStream] public bool maintainOffset;

    public bool IsValid()
    {
        return source != null && target != null;
    }

    public void SetDefaultValues()
    {
        source = null;
        target = null;
        maintainOffset = false;
        fps = 60;
    }
}

public class SteppedBinder : AnimationJobBinder<SteppedJob, SteppedData>
{
    public override SteppedJob Create(Animator animator, ref SteppedData data, Component component)
    {
        var job = new SteppedJob();

        job.driven = ReadWriteTransformHandle.Bind(animator, data.source);
        job.target = ReadWriteTransformHandle.Bind(animator, data.target);

        job.offset = AffineTransform.identity;

        job.deltaTime = FloatProperty.Bind(animator, component, PropertyUtils.ConstructConstraintDataPropertyName(nameof(data.deltaTime)));
        job.fps = FloatProperty.Bind(animator, component, PropertyUtils.ConstructConstraintDataPropertyName(nameof(data.fps)));

        if (data.maintainOffset) {
            job.offset.translation = data.source.position - data.target.position;
        }

        return job;
    }

    public override void Update(SteppedJob job, ref SteppedData data)
    {
        base.Update(job, ref data);
        data.deltaTime = Time.deltaTime;
    }

    public override void Destroy(SteppedJob job)
    {

    }
}

[DisallowMultipleComponent]
public class SteppedConstraint : RigConstraint<SteppedJob, SteppedData, SteppedBinder>
{ }