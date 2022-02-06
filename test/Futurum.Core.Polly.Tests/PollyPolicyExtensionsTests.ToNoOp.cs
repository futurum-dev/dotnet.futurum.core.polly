using System;

using FluentAssertions;

using Polly;
using Polly.NoOp;

using Xunit;

namespace Futurum.Core.Polly.Tests;

public class PollyPolicyExtensionsToNoOpTests
{
    public class non_generic
    {
        [Fact]
        public void when_not_null()
        {
            var policy = Policy.Handle<Exception>()
                               .RetryAsync(3);

            var returnedPolicy = policy.ToNoOp();

            returnedPolicy.Should().Be(policy);
        }

        [Fact]
        public void when_null()
        {
            IAsyncPolicy? policy = null;

            var returnedPolicy = policy.ToNoOp();

            returnedPolicy.Should().BeOfType<AsyncNoOpPolicy>();
        }
    }

    public class generic
    {
        [Fact]
        public void when_not_null()
        {
            var policy = Policy<int>.Handle<Exception>()
                                    .RetryAsync(3);

            var returnedPolicy = policy.ToNoOp();

            returnedPolicy.Should().Be(policy);
        }

        [Fact]
        public void when_null()
        {
            IAsyncPolicy<int>? policy = null;

            var returnedPolicy = policy.ToNoOp();

            returnedPolicy.Should().BeOfType<AsyncNoOpPolicy<int>>();
        }
    }
}