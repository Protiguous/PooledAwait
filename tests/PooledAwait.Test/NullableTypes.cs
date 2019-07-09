﻿using System;
using System.Collections.Generic;
using Xunit;
using System.Linq;
using PooledAwait.TaskBuilders;
using PooledAwait.Internal;

namespace PooledAwait.Test
{
    public class NullableTypes
    {
        [Theory]
        [InlineData(typeof(FireAndForget))]
        [InlineData(typeof(PooledTask))]
        [InlineData(typeof(PooledTask<>))]
        [InlineData(typeof(PooledValueTask))]
        [InlineData(typeof(PooledValueTask<>))]
        [InlineData(typeof(PooledValueTaskSource))]
        [InlineData(typeof(PooledValueTaskSource<>))]
        [InlineData(typeof(ValueTaskCompletionSource<>))]
        [InlineData(typeof(FireAndForgetBuilder))]
        [InlineData(typeof(PooledTaskBuilder))]
        [InlineData(typeof(PooledTaskBuilder<>))]
        [InlineData(typeof(PooledValueTaskBuilder))]
        [InlineData(typeof(PooledValueTaskBuilder<>))]
        [InlineData(typeof(Nothing))]
        [InlineData(typeof(Counters.Counter))]
        public void ValueTypesOverrideAllMethods(Type type)
        {
            Assert.Same(type, type.GetMethod(nameof(ToString), Type.EmptyTypes).DeclaringType);
            Assert.Same(type, type.GetMethod(nameof(GetHashCode), Type.EmptyTypes).DeclaringType);
            Assert.Same(type, type.GetMethod(nameof(Equals), new Type[] { typeof(object) }).DeclaringType);
        }

    }
}