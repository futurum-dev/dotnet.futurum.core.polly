using System;

using FluentAssertions;

using Polly;
using Polly.NoOp;

using Xunit;

namespace Futurum.Core.Polly.Tests;

public class PollyPolicyExtensionsToResultNoOpTests
{
    public class non_generic
    {
        [Fact]
        public void when_not_null()
        {
            var policy = Policy<Result.Result>.Handle<Exception>()
                                              .RetryAsync(3);

            var returnedPolicy = policy.ToResultNoOp();

            returnedPolicy.Should().Be(policy);
        }

        [Fact]
        public void when_null()
        {
            IAsyncPolicy<Result.Result>? policy = null;

            var returnedPolicy = policy.ToResultNoOp();

            returnedPolicy.Should().BeOfType<AsyncNoOpPolicy<Result.Result>>();
        }
    }

    public class generic
    {
        [Fact]
        public void when_not_null()
        {
            var policy = Policy<Result.Result<int>>.Handle<Exception>()
                                                   .RetryAsync(3);

            var returnedPolicy = policy.ToResultNoOp();

            returnedPolicy.Should().Be(policy);
        }

        [Fact]
        public void when_null()
        {
            IAsyncPolicy<Result.Result<int>>? policy = null;

            var returnedPolicy = policy.ToResultNoOp();

            returnedPolicy.Should().BeOfType<AsyncNoOpPolicy<Result.Result<int>>>();
        }
    }
}