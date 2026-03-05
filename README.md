# UAppToolkit Init Operations

A comprehensive initialization framework for Unity applications that provides a flexible and extensible system for managing application startup operations, progress tracking, and internet connectivity checks.

## Features

- **Synchronous & Asynchronous Operations** - Support for both blocking and non-blocking initialization tasks
- **Progress Tracking** - Monitor the progress of operations with floating-point values
- **Internet Connectivity Checks** - Automatically check internet availability before executing operations
- **Serial & Parallel Execution** - Execute operations sequentially or in parallel based on requirements
- **Lazy Initialization** - Defer initialization until needed with progress support
- **Built-in Logging** - Comprehensive logging system with duration tracking
- **Task-based Support** - Direct support for C# Task-based asynchronous patterns
- **Callback Support** - Execute operations with callback-based completion

## Installation

Add the package to your `manifest.json`:

```json
{
  "dependencies": {
    "com.uapp-toolkit.init-operations": "1.0.0",
    "com.uapp-toolkit.internet-checker": "1.0.0"
  }
}
```

Or use the Git URL in your package manager.

## Quick Start

### Basic Synchronous Operation

```csharp
using UAppToolKit.InitOperations;

var operation = new InitOperation("LoadConfig", () => {
    // Load configuration
    Debug.Log("Config loaded");
});

// Execute in coroutine context
yield return operation;
```

### Asynchronous Operation

```csharp
var operation = new AsyncInitOperation("LoadAssets", LoadAssetsCoroutine());

IEnumerator LoadAssetsCoroutine() {
    // Your async loading code
    yield return null;
}

yield return operation;
```

### Operation with Progress

```csharp
var operation = new AsyncInitOperationWithProgress("DownloadData", DownloadWithProgress());

IEnumerator<float> DownloadWithProgress() {
    for (int i = 0; i < 100; i++) {
        yield return i / 100f; // Return progress (0-1)
        yield return null;
    }
}

yield return operation;
```

### Parallel Operations

```csharp
var operations = new ParallelInitOperations(
    new InitOperation("Task1", () => { /* ... */ }),
    new AsyncInitOperation("Task2", Task2Coroutine()),
    new InitOperation("Task3", () => { /* ... */ })
);

yield return operations;
```

### Internet-Dependent Operations

```csharp
var internetCheckerService = GetComponent<InternetCheckerService>();

var operation = new InitOperationWithInternet(
    "FetchRemoteConfig",
    internetCheckerService,
    () => {
        // This code only executes after internet is verified
        Debug.Log("Fetching from server");
    }
);

yield return operation;
```

### Async Operation with Internet Check

```csharp
var operation = new AsyncInitOperationWithProgressWithInternet(
    "SyncData",
    internetCheckerService,
    SyncDataWithProgress()
);

yield return operation;
```

## Class Reference

### Base Classes

- **`InitOperationBase`** - Abstract base class for all operations implementing `IEnumerator<float>`
- **`InitOperation`** - Synchronous operation executing an `Action`
- **`AsyncInitOperation`** - Asynchronous operation executing an `IEnumerator`
- **`AsyncInitOperations`** - Collection of operations executed sequentially
- **`AsyncInitOperationWithProgress`** - Async operation with progress tracking via `IEnumerator<float>`
- **`LazyInitOperationWithProgress`** - Lazy-loaded operation with progress (deferred execution)
- **`ParallelInitOperations`** - Multiple operations executed in parallel

### Internet-Aware Operations

- **`InitOperationWithInternet`** - Synchronous operation with internet check
- **`InitCallbackOperationWithInternet`** - Callback-based operation with internet check
- **`AsyncInitOperationWithProgressWithInternet`** - Progress-tracking async operation with internet check
- **`InitOperationTaskWithInternet`** - Task-based operation with internet check

### Utilities

- **`AppLoadingLogger`** - Static logging utility with event system
  - `IsEnable` - Enable/disable logging
  - `OnStart` - Event fired when operation starts
  - `OnCompleted` - Event fired when operation completes with duration

## License

MIT

