using System;

using FluentAssertions;

using Futurum.Test.Result;

using Polly;

using Xunit;

namespace Futurum.Core.Polly.Tests;

public class PollyPolicyBuilderExtensionsHandleResultTests
{
    private const string ERROR_MESSAGE = "ERROR MESSAGE";

    public class non_generic
    {
        [Fact]
        public void correctly_identifies_Result_Success()
        {
            var retryCount = 3;
            var retryTried = 0;

            var result = Result.Result.Ok();

            var policy = Policy.Handle<Exception>()
                               .HandleResult()
                               .Retry(retryCount, (_, _, _) => { retryTried++; });

            var returnedResult = policy.Execute(() => result);

            returnedResult.ShouldBeSuccess();

            retryTried.Should().Be(0);
        }

        [Fact]
        public void correctly_identifies_Result_Error()
        {
            var retryCount = 3;
            var retryTried = 0;

            var result = Result.Result.Fail(ERROR_MESSAGE);

            var policy = Policy.Handle<Exception>()
                               .HandleResult()
                               .Retry(retryCount, (_, _, _) => { retryTried++; });

            var returnedResult = policy.Execute(() => result);

            returnedResult.ShouldBeFailureWithError(ERROR_MESSAGE);
            retryTried.Should().Be(retryCount);
        }

        [Fact]
        public void correctly_identifies_exception()
        {
            var retryCount = 3;
            var retryTried = 0;

            var policy = Policy.Handle<Exception>()
                               .HandleResult()
                               .Retry(retryCount, (_, _, _) => { retryTried++; });

            Assert.Throws<Exception>(ReturnedResult).Message.Should().Be(ERROR_MESSAGE);
            retryTried.Should().Be(retryCount);

            object ReturnedResult() =>
                policy.Execute(() =>
                {
                    throw new Exception(ERROR_MESSAGE);
                    
                    return Result.Result.Ok();
                });
        }
    }

    public class generic
    {
        [Fact]
        public void correctly_identifies_Result_Success()
        {
            var retryCount = 3;
            var retryTried = 0;

            var value = 10;

            var result = Result.Result.Ok(value);

            var policy = Policy.Handle<Exception>()
                               .HandleResult<int>()
                               .Retry(retryCount, (_, _, _) => { retryTried++; });

            var returnedResult = policy.Execute(() => result);

            returnedResult.ShouldBeSuccessWithValue(value);

            retryTried.Should().Be(0);
        }

        [Fact]
        public void correctly_identifies_Result_Error()
        {
            var retryCount = 3;
            var retryTried = 0;

            var result = Result.Result.Fail<int>(ERROR_MESSAGE);

            var policy = Policy.Handle<Exception>()
                               .HandleResult<int>()
                               .Retry(retryCount, (_, _, _) => { retryTried++; });

            var returnedResult = policy.Execute(() => result);

            returnedResult.ShouldBeFailureWithError(ERROR_MESSAGE);
            retryTried.Should().Be(retryCount);
        }

        [Fact]
        public void correctly_identifies_exception()
        {
            var retryCount = 3;
            var retryTried = 0;

            var policy = Policy.Handle<Exception>()
                               .HandleResult<int>()
                               .Retry(retryCount, (_, _, _) => { retryTried++; });

            Assert.Throws<Exception>(ReturnedResult).Message.Should().Be(ERROR_MESSAGE);
            retryTried.Should().Be(retryCount);

            object ReturnedResult() =>
                policy.Execute(() =>
                {
                    throw new Exception(ERROR_MESSAGE);
                    
                    return Result.Result.Ok(0);
                });
        }
    }
}