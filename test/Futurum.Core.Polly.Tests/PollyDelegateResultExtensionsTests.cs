using System;

using FluentAssertions;

using Polly;

using Xunit;

namespace Futurum.Core.Polly.Tests;

public class PollyDelegateResultExtensionsTests
{
    private const string ErrorMessage = "ERROR_MESSAGE1";

    public class GetErrorMessage
    {
        public class non_generic
        {
            [Fact]
            public void exception()
            {
                var delegateResult = new DelegateResult<Result.Result>(new Exception(ErrorMessage));

                delegateResult.GetErrorMessage().Should().Be($"System.Exception: {ErrorMessage}");
            }

            [Fact]
            public void result_success()
            {
                var delegateResult = new DelegateResult<Result.Result>(Result.Result.Ok());

                delegateResult.GetErrorMessage().Should().BeEmpty();
            }

            [Fact]
            public void result_failure()
            {
                var delegateResult = new DelegateResult<Result.Result>(Result.Result.Fail(ErrorMessage));

                delegateResult.GetErrorMessage().Should().Be(ErrorMessage);
            }
        }
        
        public class generic
        {
            [Fact]
            public void exception()
            {
                var delegateResult = new DelegateResult<Result.Result<string>>(new Exception(ErrorMessage));

                delegateResult.GetErrorMessage().Should().Be($"System.Exception: {ErrorMessage}");
            }

            [Fact]
            public void result_success()
            {
                var delegateResult = new DelegateResult<Result.Result<string>>(Result.Result.Ok(Guid.NewGuid().ToString()));

                delegateResult.GetErrorMessage().Should().BeEmpty();
            }

            [Fact]
            public void result_failure()
            {
                var delegateResult = new DelegateResult<Result.Result<string>>(Result.Result.Fail<string>(ErrorMessage));

                delegateResult.GetErrorMessage().Should().Be(ErrorMessage);
            }
        }
    }

    public class GetErrorMessageSafe
    {
        public class non_generic
        {
            [Fact]
            public void exception()
            {
                var delegateResult = new DelegateResult<Result.Result>(new Exception(ErrorMessage));

                delegateResult.GetErrorMessageSafe().Should().Be(ErrorMessage);
            }

            [Fact]
            public void result_success()
            {
                var delegateResult = new DelegateResult<Result.Result>(Result.Result.Ok());

                delegateResult.GetErrorMessageSafe().Should().BeEmpty();
            }

            [Fact]
            public void result_failure()
            {
                var delegateResult = new DelegateResult<Result.Result>(Result.Result.Fail(ErrorMessage));

                delegateResult.GetErrorMessageSafe().Should().Be(ErrorMessage);
            }
        }
        
        public class generic
        {
            [Fact]
            public void exception()
            {
                var delegateResult = new DelegateResult<Result.Result<string>>(new Exception(ErrorMessage));

                delegateResult.GetErrorMessageSafe().Should().Be(ErrorMessage);
            }

            [Fact]
            public void result_success()
            {
                var delegateResult = new DelegateResult<Result.Result<string>>(Result.Result.Ok(Guid.NewGuid().ToString()));

                delegateResult.GetErrorMessageSafe().Should().BeEmpty();
            }

            [Fact]
            public void result_failure()
            {
                var delegateResult = new DelegateResult<Result.Result<string>>(Result.Result.Fail<string>(ErrorMessage));

                delegateResult.GetErrorMessageSafe().Should().Be(ErrorMessage);
            }
        }
    }
}