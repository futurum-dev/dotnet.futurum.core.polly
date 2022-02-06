using System;
using System.Threading.Tasks;

using FluentAssertions;

using Futurum.Core.Result;
using Futurum.Test.Result;

using Polly;
using Polly.Retry;

using Xunit;

namespace Futurum.Core.Polly.Tests;

public class ResultThenTryPollyTests
{
    private const string ERROR_MESSAGE = "ERROR MESSAGE";

    public class sync_input
    {
        public class result_non_generic
        {
            public class to_non_generic
            {
                public class input_success
                {
                    [Fact]
                    public async Task to_success()
                    {
                        var value = 10;
                        var count = 0;
                        var retryCount = 3;

                        var input = Result.Result.Ok();

                        var func = () => Task.CompletedTask;

                        var result = await input.ThenTryAsync(func, () => ERROR_MESSAGE, RetryPolicy(() => count++, retryCount));

                        result.ShouldBeSuccess();
                        count.Should().Be(0);
                    }

                    [Fact]
                    public async Task to_exception()
                    {
                        var value = 10;
                        var count = 0;
                        var retryCount = 3;

                        var input = Result.Result.Ok();

                        var func = () =>
                        {
                            throw new Exception(ERROR_MESSAGE);

                            return Task.CompletedTask;
                        };

                        var result = await input.ThenTryAsync(func, () => ERROR_MESSAGE, RetryPolicy(() => count++, retryCount));

                        result.ShouldBeFailureWithError($"{ERROR_MESSAGE};{ERROR_MESSAGE}");
                        count.Should().Be(retryCount);
                    }
                }

                public class input_failure
                {
                    [Fact]
                    public async Task to_success()
                    {
                        var value = 10;
                        var count = 0;
                        var retryCount = 3;

                        var input = Result.Result.Fail(ERROR_MESSAGE);

                        var func = () => Task.CompletedTask;

                        var result = await input.ThenTryAsync(func, () => ERROR_MESSAGE, RetryPolicy(() => count++, retryCount));

                        result.ShouldBeFailureWithError(ERROR_MESSAGE);
                        count.Should().Be(0);
                    }

                    [Fact]
                    public async Task to_exception()
                    {
                        var value = 10;
                        var count = 0;
                        var retryCount = 3;

                        var input = Result.Result.Fail(ERROR_MESSAGE);

                        var func = () =>
                        {
                            throw new Exception(ERROR_MESSAGE);

                            return Task.CompletedTask;
                        };

                        var result = await input.ThenTryAsync(func, () => ERROR_MESSAGE, RetryPolicy(() => count++, retryCount));

                        result.ShouldBeFailureWithError(ERROR_MESSAGE);
                        count.Should().Be(0);
                    }
                }
            }

            public class to_generic
            {
                public class input_success
                {
                    [Fact]
                    public async Task to_success()
                    {
                        var value = 10;
                        var count = 0;
                        var retryCount = 3;

                        var input = Result.Result.Ok();

                        var func = () => Task.FromResult(value);

                        var result = await input.ThenTryAsync(func, () => ERROR_MESSAGE, RetryPolicy<int>(() => count++, retryCount));

                        result.ShouldBeSuccessWithValue(value);
                        count.Should().Be(0);
                    }

                    [Fact]
                    public async Task to_exception()
                    {
                        var value = 10;
                        var count = 0;
                        var retryCount = 3;

                        var input = Result.Result.Ok();

                        var func = () =>
                        {
                            throw new Exception(ERROR_MESSAGE);

                            return Task.FromResult(value);
                        };

                        var result = await input.ThenTryAsync(func, () => ERROR_MESSAGE, RetryPolicy<int>(() => count++, retryCount));

                        result.ShouldBeFailureWithError($"{ERROR_MESSAGE};{ERROR_MESSAGE}");
                        count.Should().Be(retryCount);
                    }
                }

                public class input_failure
                {
                    [Fact]
                    public async Task to_success()
                    {
                        var value = 10;
                        var count = 0;
                        var retryCount = 3;

                        var input = Result.Result.Fail(ERROR_MESSAGE);

                        var func = () => Task.FromResult(value);

                        var result = await input.ThenTryAsync(func, () => ERROR_MESSAGE, RetryPolicy<int>(() => count++, retryCount));

                        result.ShouldBeFailureWithError(ERROR_MESSAGE);
                        count.Should().Be(0);
                    }

                    [Fact]
                    public async Task to_exception()
                    {
                        var value = 10;
                        var count = 0;
                        var retryCount = 3;

                        var input = Result.Result.Fail(ERROR_MESSAGE);

                        var func = () =>
                        {
                            throw new Exception(ERROR_MESSAGE);

                            return Task.FromResult(value);
                        };

                        var result = await input.ThenTryAsync(func, () => ERROR_MESSAGE, RetryPolicy<int>(() => count++, retryCount));

                        result.ShouldBeFailureWithError(ERROR_MESSAGE);
                        count.Should().Be(0);
                    }
                }
            }

            public class to_result_non_generic
            {
                public class input_success
                {
                    [Fact]
                    public async Task to_success()
                    {
                        var value = 10;
                        var count = 0;
                        var retryCount = 3;

                        var input = Result.Result.Ok();

                        var func = () => Result.Result.OkAsync();

                        var result = await input.ThenTryAsync(func, () => ERROR_MESSAGE, ResultRetryPolicy(() => count++, retryCount));

                        result.ShouldBeSuccess();
                        count.Should().Be(0);
                    }

                    [Fact]
                    public async Task to_exception()
                    {
                        var value = 10;
                        var count = 0;
                        var retryCount = 3;

                        var input = Result.Result.Ok();

                        var func = () => Result.Result.FailAsync(ERROR_MESSAGE);

                        var result = await input.ThenTryAsync(func, () => ERROR_MESSAGE, ResultRetryPolicy(() => count++, retryCount));

                        result.ShouldBeFailureWithError($"{ERROR_MESSAGE};{ERROR_MESSAGE}");
                        count.Should().Be(retryCount);
                    }
                }

                public class input_failure
                {
                    [Fact]
                    public async Task to_success()
                    {
                        var value = 10;
                        var count = 0;
                        var retryCount = 3;

                        var input = Result.Result.Fail(ERROR_MESSAGE);

                        var func = () => Result.Result.OkAsync();

                        var result = await input.ThenTryAsync(func, () => ERROR_MESSAGE, ResultRetryPolicy(() => count++, retryCount));

                        result.ShouldBeFailureWithError(ERROR_MESSAGE);
                        count.Should().Be(0);
                    }

                    [Fact]
                    public async Task to_exception()
                    {
                        var value = 10;
                        var count = 0;
                        var retryCount = 3;

                        var input = Result.Result.Fail(ERROR_MESSAGE);

                        var func = () => Result.Result.FailAsync(ERROR_MESSAGE);

                        var result = await input.ThenTryAsync(func, () => ERROR_MESSAGE, ResultRetryPolicy(() => count++, retryCount));

                        result.ShouldBeFailureWithError(ERROR_MESSAGE);
                        count.Should().Be(0);
                    }
                }
            }

            public class to_result_generic
            {
                public class input_success
                {
                    [Fact]
                    public async Task to_success()
                    {
                        var value = 10;
                        var count = 0;
                        var retryCount = 3;

                        var input = Result.Result.Ok();

                        var func = () => Result.Result.OkAsync(value);

                        var result = await input.ThenTryAsync(func, () => ERROR_MESSAGE, ResultRetryPolicy<int>(() => count++, retryCount));

                        result.ShouldBeSuccessWithValue(value);
                        count.Should().Be(0);
                    }

                    [Fact]
                    public async Task to_exception()
                    {
                        var value = 10;
                        var count = 0;
                        var retryCount = 3;

                        var input = Result.Result.Ok();

                        var func = () => Result.Result.FailAsync<int>(ERROR_MESSAGE);

                        var result = await input.ThenTryAsync(func, () => ERROR_MESSAGE, ResultRetryPolicy<int>(() => count++, retryCount));

                        result.ShouldBeFailureWithError($"{ERROR_MESSAGE};{ERROR_MESSAGE}");
                        count.Should().Be(retryCount);
                    }
                }

                public class input_failure
                {
                    [Fact]
                    public async Task to_success()
                    {
                        var value = 10;
                        var count = 0;
                        var retryCount = 3;

                        var input = Result.Result.Fail(ERROR_MESSAGE);

                        var func = () => Result.Result.OkAsync(value);

                        var result = await input.ThenTryAsync(func, () => ERROR_MESSAGE, ResultRetryPolicy<int>(() => count++, retryCount));

                        result.ShouldBeFailureWithError(ERROR_MESSAGE);
                        count.Should().Be(0);
                    }

                    [Fact]
                    public async Task to_exception()
                    {
                        var value = 10;
                        var count = 0;
                        var retryCount = 3;

                        var input = Result.Result.Fail(ERROR_MESSAGE);

                        var func = () => Result.Result.FailAsync<int>(ERROR_MESSAGE);

                        var result = await input.ThenTryAsync(func, () => ERROR_MESSAGE, ResultRetryPolicy<int>(() => count++, retryCount));

                        result.ShouldBeFailureWithError(ERROR_MESSAGE);
                        count.Should().Be(0);
                    }
                }
            }
        }

        public class result_generic
        {
            public class to_non_generic
            {
                public class input_success
                {
                    [Fact]
                    public async Task to_success()
                    {
                        var inputValue = 10;
                        var value = 20;
                        var count = 0;
                        var retryCount = 3;

                        var inputReceived = 0;

                        var input = Result.Result.Ok(inputValue);

                        var func = (int x) =>
                        {
                            inputReceived = x;
                            return Task.CompletedTask;
                        };

                        var result = await input.ThenTryAsync(func, () => ERROR_MESSAGE, RetryPolicy(() => count++, retryCount));

                        result.ShouldBeSuccess();
                        count.Should().Be(0);
                        inputReceived.Should().Be(inputValue);
                    }

                    [Fact]
                    public async Task to_exception()
                    {
                        var inputValue = 10;
                        var value = 20;
                        var count = 0;
                        var retryCount = 3;

                        var inputReceived = 0;

                        var input = Result.Result.Ok(inputValue);

                        var func = (int x) =>
                        {
                            inputReceived = x;

                            throw new Exception(ERROR_MESSAGE);

                            return Task.CompletedTask;
                        };

                        var result = await input.ThenTryAsync(func, () => ERROR_MESSAGE, RetryPolicy(() => count++, retryCount));

                        result.ShouldBeFailureWithError($"{ERROR_MESSAGE};{ERROR_MESSAGE}");
                        count.Should().Be(retryCount);
                        inputReceived.Should().Be(inputValue);
                    }
                }

                public class input_failure
                {
                    [Fact]
                    public async Task to_success()
                    {
                        var value = 20;
                        var count = 0;
                        var retryCount = 3;

                        var inputReceived = 0;

                        var input = Result.Result.Fail<int>(ERROR_MESSAGE);

                        var func = (int x) =>
                        {
                            inputReceived = x;

                            return Task.CompletedTask;
                        };

                        var result = await input.ThenTryAsync(func, () => ERROR_MESSAGE, RetryPolicy(() => count++, retryCount));

                        result.ShouldBeFailureWithError(ERROR_MESSAGE);
                        count.Should().Be(0);
                        inputReceived.Should().Be(0);
                    }

                    [Fact]
                    public async Task to_exception()
                    {
                        var value = 10;
                        var count = 0;
                        var retryCount = 3;

                        var inputReceived = 0;

                        var input = Result.Result.Fail<int>(ERROR_MESSAGE);

                        var func = (int x) =>
                        {
                            inputReceived = x;

                            throw new Exception(ERROR_MESSAGE);

                            return Task.CompletedTask;
                        };

                        var result = await input.ThenTryAsync(func, () => ERROR_MESSAGE, RetryPolicy(() => count++, retryCount));

                        result.ShouldBeFailureWithError(ERROR_MESSAGE);
                        count.Should().Be(0);
                        inputReceived.Should().Be(0);
                    }
                }
            }

            public class to_generic
            {
                public class input_success
                {
                    [Fact]
                    public async Task to_success()
                    {
                        var inputValue = 10;
                        var value = 10;
                        var count = 0;
                        var retryCount = 3;

                        var inputReceived = 0;

                        var input = Result.Result.Ok(inputValue);

                        var func = (int x) =>
                        {
                            inputReceived = x;
                            return Task.FromResult(value);
                        };

                        var result = await input.ThenTryAsync(func, () => ERROR_MESSAGE, RetryPolicy<int>(() => count++, retryCount));

                        result.ShouldBeSuccessWithValue(value);
                        count.Should().Be(0);
                        inputReceived.Should().Be(inputValue);
                    }

                    [Fact]
                    public async Task to_exception()
                    {
                        var inputValue = 10;
                        var value = 10;
                        var count = 0;
                        var retryCount = 3;

                        var inputReceived = 0;

                        var input = Result.Result.Ok(inputValue);

                        var func = (int x) =>
                        {
                            inputReceived = x;
                            throw new Exception(ERROR_MESSAGE);

                            return Task.FromResult(value);
                        };

                        var result = await input.ThenTryAsync(func, () => ERROR_MESSAGE, RetryPolicy<int>(() => count++, retryCount));

                        result.ShouldBeFailureWithError($"{ERROR_MESSAGE};{ERROR_MESSAGE}");
                        count.Should().Be(retryCount);
                        inputReceived.Should().Be(inputValue);
                    }
                }

                public class input_failure
                {
                    [Fact]
                    public async Task to_success()
                    {
                        var value = 10;
                        var count = 0;
                        var retryCount = 3;

                        var input = Result.Result.Fail<int>(ERROR_MESSAGE);

                        var func = (int x) => Task.FromResult(value);

                        var result = await input.ThenTryAsync(func, () => ERROR_MESSAGE, RetryPolicy<int>(() => count++, retryCount));

                        result.ShouldBeFailureWithError(ERROR_MESSAGE);
                        count.Should().Be(0);
                    }

                    [Fact]
                    public async Task to_exception()
                    {
                        var value = 10;
                        var count = 0;
                        var retryCount = 3;

                        var input = Result.Result.Fail<int>(ERROR_MESSAGE);

                        var func = (int x) =>
                        {
                            throw new Exception(ERROR_MESSAGE);

                            return Task.FromResult(value);
                        };

                        var result = await input.ThenTryAsync(func, () => ERROR_MESSAGE, RetryPolicy<int>(() => count++, retryCount));

                        result.ShouldBeFailureWithError(ERROR_MESSAGE);
                        count.Should().Be(0);
                    }
                }
            }

            public class to_result_non_generic
            {
                public class input_success
                {
                    [Fact]
                    public async Task to_success()
                    {
                        var inputValue = 10;
                        var value = 10;
                        var count = 0;
                        var retryCount = 3;

                        var inputReceived = 0;

                        var input = Result.Result.Ok(inputValue);

                        var func = (int x) =>
                        {
                            inputReceived = x;
                            return Result.Result.OkAsync();
                        };

                        var result = await input.ThenTryAsync(func, () => ERROR_MESSAGE, ResultRetryPolicy(() => count++, retryCount));

                        result.ShouldBeSuccess();
                        count.Should().Be(0);
                        inputReceived.Should().Be(inputValue);
                    }

                    [Fact]
                    public async Task to_exception()
                    {
                        var inputValue = 10;
                        var value = 10;
                        var count = 0;
                        var retryCount = 3;

                        var inputReceived = 0;

                        var input = Result.Result.Ok(inputValue);

                        var func = (int x) =>
                        {
                            inputReceived = x;

                            throw new Exception(ERROR_MESSAGE);

                            return Result.Result.FailAsync(ERROR_MESSAGE);
                        };

                        var result = await input.ThenTryAsync(func, () => ERROR_MESSAGE, ResultRetryPolicy(() => count++, retryCount));

                        result.ShouldBeFailureWithError($"{ERROR_MESSAGE};{ERROR_MESSAGE}");
                        count.Should().Be(retryCount);
                        inputReceived.Should().Be(inputValue);
                    }
                }

                public class input_failure
                {
                    [Fact]
                    public async Task to_success()
                    {
                        var value = 10;
                        var count = 0;
                        var retryCount = 3;

                        var inputReceived = 0;

                        var input = Result.Result.Fail<int>(ERROR_MESSAGE);

                        var func = (int x) =>
                        {
                            inputReceived = x;
                            return Result.Result.OkAsync();
                        };

                        var result = await input.ThenTryAsync(func, () => ERROR_MESSAGE, ResultRetryPolicy(() => count++, retryCount));

                        result.ShouldBeFailureWithError(ERROR_MESSAGE);
                        count.Should().Be(0);
                        inputReceived.Should().Be(0);
                    }

                    [Fact]
                    public async Task to_exception()
                    {
                        var value = 10;
                        var count = 0;
                        var retryCount = 3;

                        var inputReceived = 0;

                        var input = Result.Result.Fail<int>(ERROR_MESSAGE);

                        var func = (int x) =>
                        {
                            inputReceived = x;
                            return Result.Result.FailAsync(ERROR_MESSAGE);
                        };

                        var result = await input.ThenTryAsync(func, () => ERROR_MESSAGE, ResultRetryPolicy(() => count++, retryCount));

                        result.ShouldBeFailureWithError(ERROR_MESSAGE);
                        count.Should().Be(0);
                        inputReceived.Should().Be(0);
                    }
                }
            }

            public class to_result_generic
            {
                public class input_success
                {
                    [Fact]
                    public async Task to_success()
                    {
                        var inputValue = 10;
                        var value = 10;
                        var count = 0;
                        var retryCount = 3;

                        var inputReceived = 0;

                        var input = Result.Result.Ok(inputValue);

                        var func = (int x) =>
                        {
                            inputReceived = x;
                            return Result.Result.OkAsync(value);
                        };

                        var result = await input.ThenTryAsync(func, () => ERROR_MESSAGE, ResultRetryPolicy<int>(() => count++, retryCount));

                        result.ShouldBeSuccessWithValue(value);
                        count.Should().Be(0);
                        inputReceived.Should().Be(inputValue);
                    }

                    [Fact]
                    public async Task to_exception()
                    {
                        var inputValue = 10;
                        var value = 10;
                        var count = 0;
                        var retryCount = 3;

                        var inputReceived = 0;

                        var input = Result.Result.Ok(inputValue);

                        var func = (int x) =>
                        {
                            inputReceived = x;
                            return Result.Result.FailAsync<int>(ERROR_MESSAGE);
                        };

                        var result = await input.ThenTryAsync(func, () => ERROR_MESSAGE, ResultRetryPolicy<int>(() => count++, retryCount));

                        result.ShouldBeFailureWithError($"{ERROR_MESSAGE};{ERROR_MESSAGE}");
                        count.Should().Be(retryCount);
                        inputReceived.Should().Be(inputValue);
                    }
                }

                public class input_failure
                {
                    [Fact]
                    public async Task to_success()
                    {
                        var value = 10;
                        var count = 0;
                        var retryCount = 3;

                        var inputReceived = 0;

                        var input = Result.Result.Fail<int>(ERROR_MESSAGE);

                        var func = (int x) =>
                        {
                            inputReceived = x;
                            return Result.Result.OkAsync(value);
                        };

                        var result = await input.ThenTryAsync(func, () => ERROR_MESSAGE, ResultRetryPolicy<int>(() => count++, retryCount));

                        result.ShouldBeFailureWithError(ERROR_MESSAGE);
                        count.Should().Be(0);
                        inputReceived.Should().Be(0);
                    }

                    [Fact]
                    public async Task to_exception()
                    {
                        var value = 10;
                        var count = 0;
                        var retryCount = 3;

                        var inputReceived = 0;

                        var input = Result.Result.Fail<int>(ERROR_MESSAGE);

                        var func = (int x) =>
                        {
                            inputReceived = x;
                            return Result.Result.FailAsync<int>(ERROR_MESSAGE);
                        };

                        var result = await input.ThenTryAsync(func, () => ERROR_MESSAGE, ResultRetryPolicy<int>(() => count++, retryCount));

                        result.ShouldBeFailureWithError(ERROR_MESSAGE);
                        count.Should().Be(0);
                        inputReceived.Should().Be(0);
                    }
                }
            }
        }
    }

    public class async_input
    {
        public class result_non_generic
        {
            public class to_non_generic
            {
                public class input_success
                {
                    [Fact]
                    public async Task to_success()
                    {
                        var value = 10;
                        var count = 0;
                        var retryCount = 3;

                        var input = Result.Result.OkAsync();

                        var func = () => Task.CompletedTask;

                        var result = await input.ThenTryAsync(func, () => ERROR_MESSAGE, RetryPolicy(() => count++, retryCount));

                        result.ShouldBeSuccess();
                        count.Should().Be(0);
                    }

                    [Fact]
                    public async Task to_exception()
                    {
                        var value = 10;
                        var count = 0;
                        var retryCount = 3;

                        var input = Result.Result.OkAsync();

                        var func = () =>
                        {
                            throw new Exception(ERROR_MESSAGE);

                            return Task.CompletedTask;
                        };

                        var result = await input.ThenTryAsync(func, () => ERROR_MESSAGE, RetryPolicy(() => count++, retryCount));

                        result.ShouldBeFailureWithError($"{ERROR_MESSAGE};{ERROR_MESSAGE}");
                        count.Should().Be(retryCount);
                    }
                }

                public class input_failure
                {
                    [Fact]
                    public async Task to_success()
                    {
                        var value = 10;
                        var count = 0;
                        var retryCount = 3;

                        var input = Result.Result.FailAsync(ERROR_MESSAGE);

                        var func = () => Task.CompletedTask;

                        var result = await input.ThenTryAsync(func, () => ERROR_MESSAGE, RetryPolicy(() => count++, retryCount));

                        result.ShouldBeFailureWithError(ERROR_MESSAGE);
                        count.Should().Be(0);
                    }

                    [Fact]
                    public async Task to_exception()
                    {
                        var value = 10;
                        var count = 0;
                        var retryCount = 3;

                        var input = Result.Result.FailAsync(ERROR_MESSAGE);

                        var func = () =>
                        {
                            throw new Exception(ERROR_MESSAGE);

                            return Task.CompletedTask;
                        };

                        var result = await input.ThenTryAsync(func, () => ERROR_MESSAGE, RetryPolicy(() => count++, retryCount));

                        result.ShouldBeFailureWithError(ERROR_MESSAGE);
                        count.Should().Be(0);
                    }
                }
            }

            public class to_generic
            {
                public class input_success
                {
                    [Fact]
                    public async Task to_success()
                    {
                        var value = 10;
                        var count = 0;
                        var retryCount = 3;

                        var input = Result.Result.OkAsync();

                        var func = () => Task.FromResult(value);

                        var result = await input.ThenTryAsync(func, () => ERROR_MESSAGE, RetryPolicy<int>(() => count++, retryCount));

                        result.ShouldBeSuccessWithValue(value);
                        count.Should().Be(0);
                    }

                    [Fact]
                    public async Task to_exception()
                    {
                        var value = 10;
                        var count = 0;
                        var retryCount = 3;

                        var input = Result.Result.OkAsync();

                        var func = () =>
                        {
                            throw new Exception(ERROR_MESSAGE);

                            return Task.FromResult(value);
                        };

                        var result = await input.ThenTryAsync(func, () => ERROR_MESSAGE, RetryPolicy<int>(() => count++, retryCount));

                        result.ShouldBeFailureWithError($"{ERROR_MESSAGE};{ERROR_MESSAGE}");
                        count.Should().Be(retryCount);
                    }
                }

                public class input_failure
                {
                    [Fact]
                    public async Task to_success()
                    {
                        var value = 10;
                        var count = 0;
                        var retryCount = 3;

                        var input = Result.Result.FailAsync(ERROR_MESSAGE);

                        var func = () => Task.FromResult(value);

                        var result = await input.ThenTryAsync(func, () => ERROR_MESSAGE, RetryPolicy<int>(() => count++, retryCount));

                        result.ShouldBeFailureWithError(ERROR_MESSAGE);
                        count.Should().Be(0);
                    }

                    [Fact]
                    public async Task to_exception()
                    {
                        var value = 10;
                        var count = 0;
                        var retryCount = 3;

                        var input = Result.Result.FailAsync(ERROR_MESSAGE);

                        var func = () =>
                        {
                            throw new Exception(ERROR_MESSAGE);

                            return Task.FromResult(value);
                        };

                        var result = await input.ThenTryAsync(func, () => ERROR_MESSAGE, RetryPolicy<int>(() => count++, retryCount));

                        result.ShouldBeFailureWithError(ERROR_MESSAGE);
                        count.Should().Be(0);
                    }
                }
            }

            public class to_result_non_generic
            {
                public class input_success
                {
                    [Fact]
                    public async Task to_success()
                    {
                        var value = 10;
                        var count = 0;
                        var retryCount = 3;

                        var input = Result.Result.OkAsync();

                        var func = () => Result.Result.OkAsync();

                        var result = await input.ThenTryAsync(func, () => ERROR_MESSAGE, ResultRetryPolicy(() => count++, retryCount));

                        result.ShouldBeSuccess();
                        count.Should().Be(0);
                    }

                    [Fact]
                    public async Task to_exception()
                    {
                        var value = 10;
                        var count = 0;
                        var retryCount = 3;

                        var input = Result.Result.OkAsync();

                        var func = () => Result.Result.FailAsync(ERROR_MESSAGE);

                        var result = await input.ThenTryAsync(func, () => ERROR_MESSAGE, ResultRetryPolicy(() => count++, retryCount));

                        result.ShouldBeFailureWithError($"{ERROR_MESSAGE};{ERROR_MESSAGE}");
                        count.Should().Be(retryCount);
                    }
                }

                public class input_failure
                {
                    [Fact]
                    public async Task to_success()
                    {
                        var value = 10;
                        var count = 0;
                        var retryCount = 3;

                        var input = Result.Result.FailAsync(ERROR_MESSAGE);

                        var func = () => Result.Result.OkAsync();

                        var result = await input.ThenTryAsync(func, () => ERROR_MESSAGE, ResultRetryPolicy(() => count++, retryCount));

                        result.ShouldBeFailureWithError(ERROR_MESSAGE);
                        count.Should().Be(0);
                    }

                    [Fact]
                    public async Task to_exception()
                    {
                        var value = 10;
                        var count = 0;
                        var retryCount = 3;

                        var input = Result.Result.FailAsync(ERROR_MESSAGE);

                        var func = () => Result.Result.FailAsync(ERROR_MESSAGE);

                        var result = await input.ThenTryAsync(func, () => ERROR_MESSAGE, ResultRetryPolicy(() => count++, retryCount));

                        result.ShouldBeFailureWithError(ERROR_MESSAGE);
                        count.Should().Be(0);
                    }
                }
            }

            public class to_result_generic
            {
                public class input_success
                {
                    [Fact]
                    public async Task to_success()
                    {
                        var value = 10;
                        var count = 0;
                        var retryCount = 3;

                        var input = Result.Result.OkAsync();

                        var func = () => Result.Result.OkAsync(value);

                        var result = await input.ThenTryAsync(func, () => ERROR_MESSAGE, ResultRetryPolicy<int>(() => count++, retryCount));

                        result.ShouldBeSuccessWithValue(value);
                        count.Should().Be(0);
                    }

                    [Fact]
                    public async Task to_exception()
                    {
                        var value = 10;
                        var count = 0;
                        var retryCount = 3;

                        var input = Result.Result.OkAsync();

                        var func = () => Result.Result.FailAsync<int>(ERROR_MESSAGE);

                        var result = await input.ThenTryAsync(func, () => ERROR_MESSAGE, ResultRetryPolicy<int>(() => count++, retryCount));

                        result.ShouldBeFailureWithError($"{ERROR_MESSAGE};{ERROR_MESSAGE}");
                        count.Should().Be(retryCount);
                    }
                }

                public class input_failure
                {
                    [Fact]
                    public async Task to_success()
                    {
                        var value = 10;
                        var count = 0;
                        var retryCount = 3;

                        var input = Result.Result.FailAsync(ERROR_MESSAGE);

                        var func = () => Result.Result.OkAsync(value);

                        var result = await input.ThenTryAsync(func, () => ERROR_MESSAGE, ResultRetryPolicy<int>(() => count++, retryCount));

                        result.ShouldBeFailureWithError(ERROR_MESSAGE);
                        count.Should().Be(0);
                    }

                    [Fact]
                    public async Task to_exception()
                    {
                        var value = 10;
                        var count = 0;
                        var retryCount = 3;

                        var input = Result.Result.FailAsync(ERROR_MESSAGE);

                        var func = () => Result.Result.FailAsync<int>(ERROR_MESSAGE);

                        var result = await input.ThenTryAsync(func, () => ERROR_MESSAGE, ResultRetryPolicy<int>(() => count++, retryCount));

                        result.ShouldBeFailureWithError(ERROR_MESSAGE);
                        count.Should().Be(0);
                    }
                }
            }
        }

        public class result_generic
        {
            public class to_non_generic
            {
                public class input_success
                {
                    [Fact]
                    public async Task to_success()
                    {
                        var inputValue = 10;
                        var value = 20;
                        var count = 0;
                        var retryCount = 3;

                        var inputReceived = 0;

                        var input = Result.Result.OkAsync(inputValue);

                        var func = (int x) =>
                        {
                            inputReceived = x;
                            return Task.CompletedTask;
                        };

                        var result = await input.ThenTryAsync(func, () => ERROR_MESSAGE, RetryPolicy(() => count++, retryCount));

                        result.ShouldBeSuccess();
                        count.Should().Be(0);
                        inputReceived.Should().Be(inputValue);
                    }

                    [Fact]
                    public async Task to_exception()
                    {
                        var inputValue = 10;
                        var value = 20;
                        var count = 0;
                        var retryCount = 3;

                        var inputReceived = 0;

                        var input = Result.Result.OkAsync(inputValue);

                        var func = (int x) =>
                        {
                            inputReceived = x;

                            throw new Exception(ERROR_MESSAGE);

                            return Task.CompletedTask;
                        };

                        var result = await input.ThenTryAsync(func, () => ERROR_MESSAGE, RetryPolicy(() => count++, retryCount));

                        result.ShouldBeFailureWithError($"{ERROR_MESSAGE};{ERROR_MESSAGE}");
                        count.Should().Be(retryCount);
                        inputReceived.Should().Be(inputValue);
                    }
                }

                public class input_failure
                {
                    [Fact]
                    public async Task to_success()
                    {
                        var value = 20;
                        var count = 0;
                        var retryCount = 3;

                        var inputReceived = 0;

                        var input = Result.Result.FailAsync<int>(ERROR_MESSAGE);

                        var func = (int x) =>
                        {
                            inputReceived = x;

                            return Task.CompletedTask;
                        };

                        var result = await input.ThenTryAsync(func, () => ERROR_MESSAGE, RetryPolicy(() => count++, retryCount));

                        result.ShouldBeFailureWithError(ERROR_MESSAGE);
                        count.Should().Be(0);
                        inputReceived.Should().Be(0);
                    }

                    [Fact]
                    public async Task to_exception()
                    {
                        var value = 10;
                        var count = 0;
                        var retryCount = 3;

                        var inputReceived = 0;

                        var input = Result.Result.FailAsync<int>(ERROR_MESSAGE);

                        var func = (int x) =>
                        {
                            inputReceived = x;

                            throw new Exception(ERROR_MESSAGE);

                            return Task.CompletedTask;
                        };

                        var result = await input.ThenTryAsync(func, () => ERROR_MESSAGE, RetryPolicy(() => count++, retryCount));

                        result.ShouldBeFailureWithError(ERROR_MESSAGE);
                        count.Should().Be(0);
                        inputReceived.Should().Be(0);
                    }
                }
            }

            public class to_generic
            {
                public class input_success
                {
                    [Fact]
                    public async Task to_success()
                    {
                        var inputValue = 10;
                        var value = 10;
                        var count = 0;
                        var retryCount = 3;

                        var inputReceived = 0;

                        var input = Result.Result.OkAsync(inputValue);

                        var func = (int x) =>
                        {
                            inputReceived = x;
                            return Task.FromResult(value);
                        };

                        var result = await input.ThenTryAsync(func, () => ERROR_MESSAGE, RetryPolicy<int>(() => count++, retryCount));

                        result.ShouldBeSuccessWithValue(value);
                        count.Should().Be(0);
                        inputReceived.Should().Be(inputValue);
                    }

                    [Fact]
                    public async Task to_exception()
                    {
                        var inputValue = 10;
                        var value = 10;
                        var count = 0;
                        var retryCount = 3;

                        var inputReceived = 0;

                        var input = Result.Result.OkAsync(inputValue);

                        var func = (int x) =>
                        {
                            inputReceived = x;
                            throw new Exception(ERROR_MESSAGE);

                            return Task.FromResult(value);
                        };

                        var result = await input.ThenTryAsync(func, () => ERROR_MESSAGE, RetryPolicy<int>(() => count++, retryCount));

                        result.ShouldBeFailureWithError($"{ERROR_MESSAGE};{ERROR_MESSAGE}");
                        count.Should().Be(retryCount);
                        inputReceived.Should().Be(inputValue);
                    }
                }

                public class input_failure
                {
                    [Fact]
                    public async Task to_success()
                    {
                        var value = 10;
                        var count = 0;
                        var retryCount = 3;

                        var input = Result.Result.FailAsync<int>(ERROR_MESSAGE);

                        var func = (int x) => Task.FromResult(value);

                        var result = await input.ThenTryAsync(func, () => ERROR_MESSAGE, RetryPolicy<int>(() => count++, retryCount));

                        result.ShouldBeFailureWithError(ERROR_MESSAGE);
                        count.Should().Be(0);
                    }

                    [Fact]
                    public async Task to_exception()
                    {
                        var value = 10;
                        var count = 0;
                        var retryCount = 3;

                        var input = Result.Result.FailAsync<int>(ERROR_MESSAGE);

                        var func = (int x) =>
                        {
                            throw new Exception(ERROR_MESSAGE);

                            return Task.FromResult(value);
                        };

                        var result = await input.ThenTryAsync(func, () => ERROR_MESSAGE, RetryPolicy<int>(() => count++, retryCount));

                        result.ShouldBeFailureWithError(ERROR_MESSAGE);
                        count.Should().Be(0);
                    }
                }
            }

            public class to_result_non_generic
            {
                public class input_success
                {
                    [Fact]
                    public async Task to_success()
                    {
                        var inputValue = 10;
                        var value = 10;
                        var count = 0;
                        var retryCount = 3;

                        var inputReceived = 0;

                        var input = Result.Result.OkAsync(inputValue);

                        var func = (int x) =>
                        {
                            inputReceived = x;
                            return Result.Result.OkAsync();
                        };

                        var result = await input.ThenTryAsync(func, () => ERROR_MESSAGE, ResultRetryPolicy(() => count++, retryCount));

                        result.ShouldBeSuccess();
                        count.Should().Be(0);
                        inputReceived.Should().Be(inputValue);
                    }

                    [Fact]
                    public async Task to_exception()
                    {
                        var inputValue = 10;
                        var value = 10;
                        var count = 0;
                        var retryCount = 3;

                        var inputReceived = 0;

                        var input = Result.Result.OkAsync(inputValue);

                        var func = (int x) =>
                        {
                            inputReceived = x;

                            throw new Exception(ERROR_MESSAGE);

                            return Result.Result.FailAsync(ERROR_MESSAGE);
                        };

                        var result = await input.ThenTryAsync(func, () => ERROR_MESSAGE, ResultRetryPolicy(() => count++, retryCount));

                        result.ShouldBeFailureWithError($"{ERROR_MESSAGE};{ERROR_MESSAGE}");
                        count.Should().Be(retryCount);
                        inputReceived.Should().Be(inputValue);
                    }
                }

                public class input_failure
                {
                    [Fact]
                    public async Task to_success()
                    {
                        var value = 10;
                        var count = 0;
                        var retryCount = 3;

                        var inputReceived = 0;

                        var input = Result.Result.FailAsync<int>(ERROR_MESSAGE);

                        var func = (int x) =>
                        {
                            inputReceived = x;
                            return Result.Result.OkAsync();
                        };

                        var result = await input.ThenTryAsync(func, () => ERROR_MESSAGE, ResultRetryPolicy(() => count++, retryCount));

                        result.ShouldBeFailureWithError(ERROR_MESSAGE);
                        count.Should().Be(0);
                        inputReceived.Should().Be(0);
                    }

                    [Fact]
                    public async Task to_exception()
                    {
                        var value = 10;
                        var count = 0;
                        var retryCount = 3;

                        var inputReceived = 0;

                        var input = Result.Result.FailAsync<int>(ERROR_MESSAGE);

                        var func = (int x) =>
                        {
                            inputReceived = x;
                            return Result.Result.FailAsync(ERROR_MESSAGE);
                        };

                        var result = await input.ThenTryAsync(func, () => ERROR_MESSAGE, ResultRetryPolicy(() => count++, retryCount));

                        result.ShouldBeFailureWithError(ERROR_MESSAGE);
                        count.Should().Be(0);
                        inputReceived.Should().Be(0);
                    }
                }
            }

            public class to_result_generic
            {
                public class input_success
                {
                    [Fact]
                    public async Task to_success()
                    {
                        var inputValue = 10;
                        var value = 10;
                        var count = 0;
                        var retryCount = 3;

                        var inputReceived = 0;

                        var input = Result.Result.OkAsync(inputValue);

                        var func = (int x) =>
                        {
                            inputReceived = x;
                            return Result.Result.OkAsync(value);
                        };

                        var result = await input.ThenTryAsync(func, () => ERROR_MESSAGE, ResultRetryPolicy<int>(() => count++, retryCount));

                        result.ShouldBeSuccessWithValue(value);
                        count.Should().Be(0);
                        inputReceived.Should().Be(inputValue);
                    }

                    [Fact]
                    public async Task to_exception()
                    {
                        var inputValue = 10;
                        var value = 10;
                        var count = 0;
                        var retryCount = 3;

                        var inputReceived = 0;

                        var input = Result.Result.OkAsync(inputValue);

                        var func = (int x) =>
                        {
                            inputReceived = x;
                            return Result.Result.FailAsync<int>(ERROR_MESSAGE);
                        };

                        var result = await input.ThenTryAsync(func, () => ERROR_MESSAGE, ResultRetryPolicy<int>(() => count++, retryCount));

                        result.ShouldBeFailureWithError($"{ERROR_MESSAGE};{ERROR_MESSAGE}");
                        count.Should().Be(retryCount);
                        inputReceived.Should().Be(inputValue);
                    }
                }

                public class input_failure
                {
                    [Fact]
                    public async Task to_success()
                    {
                        var value = 10;
                        var count = 0;
                        var retryCount = 3;

                        var inputReceived = 0;

                        var input = Result.Result.FailAsync<int>(ERROR_MESSAGE);

                        var func = (int x) =>
                        {
                            inputReceived = x;
                            return Result.Result.OkAsync(value);
                        };

                        var result = await input.ThenTryAsync(func, () => ERROR_MESSAGE, ResultRetryPolicy<int>(() => count++, retryCount));

                        result.ShouldBeFailureWithError(ERROR_MESSAGE);
                        count.Should().Be(0);
                        inputReceived.Should().Be(0);
                    }

                    [Fact]
                    public async Task to_exception()
                    {
                        var value = 10;
                        var count = 0;
                        var retryCount = 3;

                        var inputReceived = 0;

                        var input = Result.Result.FailAsync<int>(ERROR_MESSAGE);

                        var func = (int x) =>
                        {
                            inputReceived = x;
                            return Result.Result.FailAsync<int>(ERROR_MESSAGE);
                        };

                        var result = await input.ThenTryAsync(func, () => ERROR_MESSAGE, ResultRetryPolicy<int>(() => count++, retryCount));

                        result.ShouldBeFailureWithError(ERROR_MESSAGE);
                        count.Should().Be(0);
                        inputReceived.Should().Be(0);
                    }
                }
            }
        }
    }

    private static AsyncRetryPolicy RetryPolicy(Action action, int retryCount) =>
        Policy.Handle<Exception>()
              .RetryAsync(retryCount, (_, _, _) => action());

    private static AsyncRetryPolicy<T> RetryPolicy<T>(Action action, int retryCount) =>
        Policy<T>.Handle<Exception>()
              .RetryAsync(retryCount, (_, _, _) => action());

    private static AsyncRetryPolicy<Result.Result> ResultRetryPolicy(Action action, int retryCount) =>
        Policy.Handle<Exception>()
              .HandleResult()
              .RetryAsync(retryCount, (_, _, _) => action());

    private static AsyncRetryPolicy<Result<T>> ResultRetryPolicy<T>(Action action, int retryCount) =>
        Policy.Handle<Exception>()
              .HandleResult<T>()
              .RetryAsync(retryCount, (_, _, _) => action());
}