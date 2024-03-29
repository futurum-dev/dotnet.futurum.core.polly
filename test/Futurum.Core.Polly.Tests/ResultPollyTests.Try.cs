using System;
using System.Threading.Tasks;

using FluentAssertions;

using Futurum.Core.Result;
using Futurum.Test.Result;

using Polly;
using Polly.Retry;

using Xunit;

namespace Futurum.Core.Polly.Tests;

public class ResultTryPollyTests
{
    private const string ERROR_MESSAGE = "ERROR MESSAGE";

    public class non_result
    {
        public class non_generic
        {
            public class sync
            {
                [Fact]
                public void success()
                {
                    var count = 0;
                    var retryCount = 3;

                    var func = () => {};

                    var result = ResultPolly.Try(func, () => ERROR_MESSAGE, RetryPolicy(() => count++, retryCount));

                    result.ShouldBeSuccess();
                    count.Should().Be(0);
                }

                [Fact]
                public void failure()
                {
                    var count = 0;
                    var retryCount = 3;

                    var func = () =>
                    {
                        throw new Exception(ERROR_MESSAGE);
                    };

                    var result = ResultPolly.Try(func, () => ERROR_MESSAGE, RetryPolicy(() => count++, retryCount));

                    result.ShouldBeFailureWithErrorSafe($"{ERROR_MESSAGE};{ERROR_MESSAGE}");
                    count.Should().Be(retryCount);
                }
            }
            
            public class async
            {
                [Fact]
                public async Task success()
                {
                    var count = 0;
                    var retryCount = 3;

                    var func = () => Task.CompletedTask;

                    var result = await ResultPolly.TryAsync(func, () => ERROR_MESSAGE, RetryPolicyAsync(() => count++, retryCount));

                    result.ShouldBeSuccess();
                    count.Should().Be(0);
                }

                [Fact]
                public async Task failure()
                {
                    var count = 0;
                    var retryCount = 3;

                    var func = () =>
                    {
                        throw new Exception(ERROR_MESSAGE);

                        return Task.CompletedTask;
                    };

                    var result = await ResultPolly.TryAsync(func, () => ERROR_MESSAGE, RetryPolicyAsync(() => count++, retryCount));

                    result.ShouldBeFailureWithErrorSafe($"{ERROR_MESSAGE};{ERROR_MESSAGE}");
                    count.Should().Be(retryCount);
                }
            }
        }

        public class generic
        {
            public class sync
            {
                [Fact]
                public void success()
                {
                    var value = 10;
                    var count = 0;
                    var retryCount = 3;

                    var func = () => value;

                    var result = ResultPolly.Try(func, () => ERROR_MESSAGE, RetryPolicy(() => count++, retryCount));

                    result.ShouldBeSuccessWithValue(value);
                    count.Should().Be(0);
                }

                [Fact]
                public void failure()
                {
                    var value = 10;
                    var count = 0;
                    var retryCount = 3;

                    var func = () =>
                    {
                        throw new Exception(ERROR_MESSAGE);

                        return value;
                    };

                    var result = ResultPolly.Try(func, () => ERROR_MESSAGE, RetryPolicy(() => count++, retryCount));

                    result.ShouldBeFailureWithErrorSafe($"{ERROR_MESSAGE};{ERROR_MESSAGE}");
                    count.Should().Be(retryCount);
                }
            }
            
            public class async
            {
                [Fact]
                public async Task success()
                {
                    var value = 10;
                    var count = 0;
                    var retryCount = 3;

                    var func = () => Task.FromResult(value);

                    var result = await ResultPolly.TryAsync(func, () => ERROR_MESSAGE, RetryPolicyAsync(() => count++, retryCount));

                    result.ShouldBeSuccessWithValue(value);
                    count.Should().Be(0);
                }

                [Fact]
                public async Task failure()
                {
                    var value = 10;
                    var count = 0;
                    var retryCount = 3;

                    var func = () =>
                    {
                        throw new Exception(ERROR_MESSAGE);

                        return Task.FromResult(value);
                    };

                    var result = await ResultPolly.TryAsync(func, () => ERROR_MESSAGE, RetryPolicyAsync(() => count++, retryCount));

                    result.ShouldBeFailureWithErrorSafe($"{ERROR_MESSAGE};{ERROR_MESSAGE}");
                    count.Should().Be(retryCount);
                }
            }
        }
    }

    public class result
    {
        public class non_generic
        {
            public class sync
            {
                [Fact]
                public void success()
                {
                    var count = 0;
                    var retryCount = 3;

                    var func = () => Result.Result.Ok();

                    var result = ResultPolly.Try(func, () => ERROR_MESSAGE, ResultRetryPolicy(() => count++, retryCount));

                    result.ShouldBeSuccess();
                    count.Should().Be(0);
                }

                [Fact]
                public void failure()
                {
                    var count = 0;
                    var retryCount = 3;

                    var func = () => Result.Result.Fail(ERROR_MESSAGE);

                    var result = ResultPolly.Try(func, () => ERROR_MESSAGE, ResultRetryPolicy(() => count++, retryCount));

                    result.ShouldBeFailureWithError($"{ERROR_MESSAGE};{ERROR_MESSAGE}");
                    count.Should().Be(retryCount);
                }

                [Fact]
                public void exception()
                {
                    var count = 0;
                    var retryCount = 3;

                    var func = () =>
                    {
                        throw new Exception(ERROR_MESSAGE);

                        return Result.Result.Ok();
                    };

                    var result = ResultPolly.Try(func, () => ERROR_MESSAGE, ResultRetryPolicy(() => count++, retryCount));

                    result.ShouldBeFailureWithErrorSafe($"{ERROR_MESSAGE};{ERROR_MESSAGE}");
                    count.Should().Be(retryCount);
                }
            }
            
            public class async
            {
                [Fact]
                public async Task success()
                {
                    var count = 0;
                    var retryCount = 3;

                    var func = () => Result.Result.OkAsync();

                    var result = await ResultPolly.TryAsync(func, () => ERROR_MESSAGE, ResultRetryPolicyAsync(() => count++, retryCount));

                    result.ShouldBeSuccess();
                    count.Should().Be(0);
                }

                [Fact]
                public async Task failure()
                {
                    var count = 0;
                    var retryCount = 3;

                    var func = () => Result.Result.FailAsync(ERROR_MESSAGE);

                    var result = await ResultPolly.TryAsync(func, () => ERROR_MESSAGE, ResultRetryPolicyAsync(() => count++, retryCount));

                    result.ShouldBeFailureWithError($"{ERROR_MESSAGE};{ERROR_MESSAGE}");
                    count.Should().Be(retryCount);
                }

                [Fact]
                public async Task exception()
                {
                    var count = 0;
                    var retryCount = 3;

                    var func = () =>
                    {
                        throw new Exception(ERROR_MESSAGE);

                        return Result.Result.OkAsync();
                    };

                    var result = await ResultPolly.TryAsync(func, () => ERROR_MESSAGE, ResultRetryPolicyAsync(() => count++, retryCount));

                    result.ShouldBeFailureWithErrorSafe($"{ERROR_MESSAGE};{ERROR_MESSAGE}");
                    count.Should().Be(retryCount);
                }
            }
        }

        public class generic
        {
            public class sync
            {
                [Fact]
                public void success()
                {
                    var value = 10;
                    var count = 0;
                    var retryCount = 3;

                    var func = () => Result.Result.Ok(value);

                    var result = ResultPolly.Try(func, () => ERROR_MESSAGE, ResultRetryPolicy<int>(() => count++, retryCount));

                    result.ShouldBeSuccessWithValue(value);
                    count.Should().Be(0);
                }

                [Fact]
                public void failure()
                {
                    var value = 10;
                    var count = 0;
                    var retryCount = 3;

                    var func = () => Result.Result.Fail<int>(ERROR_MESSAGE);

                    var result = ResultPolly.Try(func, () => ERROR_MESSAGE, ResultRetryPolicy<int>(() => count++, retryCount));

                    result.ShouldBeFailureWithError($"{ERROR_MESSAGE};{ERROR_MESSAGE}");
                    count.Should().Be(retryCount);
                }

                [Fact]
                public void exception()
                {
                    var value = 10;
                    var count = 0;
                    var retryCount = 3;

                    var func = () =>
                    {
                        throw new Exception(ERROR_MESSAGE);

                        return Result.Result.Ok(value);
                    };

                    var result = ResultPolly.Try(func, () => ERROR_MESSAGE, ResultRetryPolicy<int>(() => count++, retryCount));

                    result.ShouldBeFailureWithErrorSafe($"{ERROR_MESSAGE};{ERROR_MESSAGE}");
                    count.Should().Be(retryCount);
                }
            }
            
            public class async
            {
                [Fact]
                public async Task success()
                {
                    var value = 10;
                    var count = 0;
                    var retryCount = 3;

                    var func = () => Result.Result.OkAsync(value);

                    var result = await ResultPolly.TryAsync(func, () => ERROR_MESSAGE, ResultRetryPolicyAsync<int>(() => count++, retryCount));

                    result.ShouldBeSuccessWithValue(value);
                    count.Should().Be(0);
                }

                [Fact]
                public async Task failure()
                {
                    var value = 10;
                    var count = 0;
                    var retryCount = 3;

                    var func = () => Result.Result.FailAsync<int>(ERROR_MESSAGE);

                    var result = await ResultPolly.TryAsync(func, () => ERROR_MESSAGE, ResultRetryPolicyAsync<int>(() => count++, retryCount));

                    result.ShouldBeFailureWithError($"{ERROR_MESSAGE};{ERROR_MESSAGE}");
                    count.Should().Be(retryCount);
                }

                [Fact]
                public async Task exception()
                {
                    var value = 10;
                    var count = 0;
                    var retryCount = 3;

                    var func = () =>
                    {
                        throw new Exception(ERROR_MESSAGE);

                        return Result.Result.OkAsync(value);
                    };

                    var result = await ResultPolly.TryAsync(func, () => ERROR_MESSAGE, ResultRetryPolicyAsync<int>(() => count++, retryCount));

                    result.ShouldBeFailureWithErrorSafe($"{ERROR_MESSAGE};{ERROR_MESSAGE}");
                    count.Should().Be(retryCount);
                }
            }
        }
    }

    private static RetryPolicy RetryPolicy(Action action, int retryCount) =>
        Policy.Handle<Exception>()
              .Retry(retryCount, (_, _, _) => action());

    private static RetryPolicy<Result.Result> ResultRetryPolicy(Action action, int retryCount) =>
        Policy.Handle<Exception>()
              .HandleResult()
              .Retry(retryCount, (_, _, _) => action());

    private static RetryPolicy<Result<T>> ResultRetryPolicy<T>(Action action, int retryCount) =>
        Policy.Handle<Exception>()
              .HandleResult<T>()
              .Retry(retryCount, (_, _, _) => action());

    private static AsyncRetryPolicy RetryPolicyAsync(Action action, int retryCount) =>
        Policy.Handle<Exception>()
              .RetryAsync(retryCount, (_, _, _) => action());

    private static AsyncRetryPolicy<Result.Result> ResultRetryPolicyAsync(Action action, int retryCount) =>
        Policy.Handle<Exception>()
              .HandleResult()
              .RetryAsync(retryCount, (_, _, _) => action());

    private static AsyncRetryPolicy<Result<T>> ResultRetryPolicyAsync<T>(Action action, int retryCount) =>
        Policy.Handle<Exception>()
              .HandleResult<T>()
              .RetryAsync(retryCount, (_, _, _) => action());
}