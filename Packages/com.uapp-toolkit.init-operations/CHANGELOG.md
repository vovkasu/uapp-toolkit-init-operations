Changelog
=========

[1.0.0] - 2026.03.05
--------------------
* **Added** - Core initialization framework with the following classes:
  - InitOperationBase - Base class for all initialization operations
  - InitOperation - Synchronous initialization operation
  - AsyncInitOperation - Asynchronous initialization operation
  - AsyncInitOperations - Multiple asynchronous operations support
  - AsyncInitOperationWithProgress - Async operation with progress tracking
  - AsyncInitOperationWithProgressWithInternet - Async operation with progress and internet checking
  - InitCallbackOperation - Initialization with callback
  - InitCallbackOperationWithInternet - Callback operation with internet checking
  - InitOperationWithInternet - Operation with internet checking
  - InitOperationTask - Task-based initialization
  - InitOperationTaskWithInternet - Task-based operation with internet checking
  - LazyInitOperationWithProgress - Lazy initialization with progress
  - ParallelInitOperations - Parallel execution of operations
  - AppLoadingLogger - Application loading logger utility
* **Changed** -
* **Fixed** -