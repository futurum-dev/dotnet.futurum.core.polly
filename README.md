# Futurum.Core.Polly

![license](https://img.shields.io/github/license/futurum-dev/dotnet.futurum.core?style=for-the-badge)
![CI](https://img.shields.io/github/actions/workflow/status/futurum-dev/dotnet.futurum.core.polly/ci.yml?branch=main&style=for-the-badge)
[![Coverage Status](https://img.shields.io/coveralls/github/futurum-dev/dotnet.futurum.core.polly?style=for-the-badge)](https://coveralls.io/github/futurum-dev/dotnet.futurum.core.polly?branch=main)
[![NuGet version](https://img.shields.io/nuget/v/futurum.core.polly?style=for-the-badge)](https://www.nuget.org/packages/futurum.core.polly)

Small dotnet library, allowing you to use [Polly](https://github.com/App-vNext/Polly) with Futurum.Core, based on the concepts behind 'Railway Oriented Programming'.

### Try
Try to run func, using the Polly policy. If the policy fails, the failing result will be returned as a failure.

```csharp
var result = await ResultPolly.TryAsync(func, () => ERROR_MESSAGE, pollyPolicy);
```

### ThenTry
Combines Result.Then with ResultPolly.Try

```csharp
var outputResult = await inputResult.ThenTryAsync(func, () => ERROR_MESSAGE, pollyPolicy);
```

### HandleResult
Create a policy to handle Result

```csharp
Policy.Handle<Exception>()
      .HandleResult()
```

### HandleResult&lt;T&gt;
Create a policy to handle Result&lt;T&gt;

```csharp
Policy.Handle<Exception>()
      .HandleResult<string>()
```

### DelegateResult extensions
#### GetErrorMessage

Get the error message either from the Exception or the Result / Result&lt;T&gt;

```csharp
var errorMessage = delegateResult.GetErrorMessage()
```

```csharp
_pollyPolicy = Policy.Handle<Exception>()
                     .HandleResult<string>()
                     .WaitAndRetryAsync(new []{TimeSpan.FromSeconds(5), },
                                        (delegateResult, timeSpan, retryCount, context) =>
                                        {
                                            var error = delegateResult.GetErrorMessage();
                                            
                                            _logger.LogWarning("Retry - retryCount: '{RetryCount}'. Error : '{Error}'", retryCount, error);
                                        });
```

#### DelegateResult GetErrorMessageSafe
Get the safe error message either from the Exception or the Result / Result&lt;T&gt;

```csharp
var errorMessage = delegateResult.GetErrorMessageSafe()
```

```csharp
_pollyPolicy = Policy.Handle<Exception>()
                     .HandleResult<string>()
                     .WaitAndRetryAsync(new []{TimeSpan.FromSeconds(5), },
                                        (delegateResult, timeSpan, retryCount, context) =>
                                        {
                                            var error = delegateResult.GetErrorMessageSafe();
                                            
                                            _logger.LogWarning("Retry - retryCount: '{RetryCount}'. Error : '{Error}'", retryCount, error);
                                        });
```