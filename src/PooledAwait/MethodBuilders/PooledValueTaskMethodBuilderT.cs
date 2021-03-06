﻿using PooledAwait.Internal;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

#pragma warning disable CS1591

namespace PooledAwait.MethodBuilders
{
    /// <summary>
    /// This type is not intended for direct usage
    /// </summary>
    [Browsable(false)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public struct PooledValueTaskMethodBuilder<T>
    {
        public override bool Equals(object? obj) => ThrowHelper.ThrowNotSupportedException<bool>();
        public override int GetHashCode() => ThrowHelper.ThrowNotSupportedException<int>();
        public override string ToString() => nameof(PooledValueTaskMethodBuilder);

        private PooledValueTaskSource<T> _source;

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static PooledValueTaskMethodBuilder<T> Create() => default;

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetStateMachine(IAsyncStateMachine _) => Counters.SetStateMachine.Increment();

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetResult(T result)
        {
            if (_source.HasTask) _source.TrySetResult(result);
            else _source = new PooledValueTaskSource<T>(result);
        }

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetException(Exception exception)
        {
            EnsureHasTask();
            _source.TrySetException(exception);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void EnsureHasTask()
        {
            if (!_source.HasTask) _source = PooledValueTaskSource<T>.Create();
        }

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public PooledValueTask<T> Task
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => _source.PooledTask;
        }

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void AwaitOnCompleted<TAwaiter, TStateMachine>(
            ref TAwaiter awaiter, ref TStateMachine stateMachine)
            where TAwaiter : INotifyCompletion
            where TStateMachine : IAsyncStateMachine
        {
            EnsureHasTask();
            StateMachineBox<TStateMachine>.AwaitOnCompleted(ref awaiter, ref stateMachine);
        }

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void AwaitUnsafeOnCompleted<TAwaiter, TStateMachine>(
            ref TAwaiter awaiter, ref TStateMachine stateMachine)
            where TAwaiter : ICriticalNotifyCompletion
            where TStateMachine : IAsyncStateMachine
        {
            EnsureHasTask();
            StateMachineBox<TStateMachine>.AwaitUnsafeOnCompleted(ref awaiter, ref stateMachine);
        }

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Start<TStateMachine>(ref TStateMachine stateMachine)
            where TStateMachine : IAsyncStateMachine => stateMachine.MoveNext();
    }
}
