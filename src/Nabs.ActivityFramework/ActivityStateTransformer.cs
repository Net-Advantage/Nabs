﻿namespace Nabs.ActivityFramework;

public interface IActivityStateTransformer<TActivityState>
    : IActivityStateBehaviourSync<TActivityState>
    where TActivityState : class, IActivityState;

public abstract class ActivityStateTransformer<TActivityState>
    : IActivityStateTransformer<TActivityState>
    where TActivityState : class, IActivityState
{
    public abstract TActivityState Run(TActivityState activityState);
}