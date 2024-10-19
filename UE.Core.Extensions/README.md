# UE.Core.Extensions

**UE.Core.Extensions** is a modular library within the **UE.Core** ecosystem that offers a wide range of extension methods to enhance the functionality of existing .NET types. These methods allow developers to write cleaner, more expressive code by extending native .NET classes without modifying their source.

## Features

- **String Extensions**:  
  Useful methods for manipulating and formatting strings, such as `Capitalize()`, `ToSnakeCase()`, and `IsNullOrEmpty()`.

- **Collection Extensions**:  
  Helpers for working with `IEnumerable` and collections, like `ForEach()`, `DistinctBy()`, and `Shuffle()`.

- **DateTime Extensions**:  
  Intuitive methods to work with dates, such as `IsWeekend()`, `ToUnixTimestamp()`, and `NextDay()`.

- **Numeric Extensions**:  
  Extend numeric types with methods like `IsInRange()`, `ToCurrencyFormat()`, and `Clamp()`.

- **Task and Async Extensions**:  
  Simplify async programming with utilities like `WithTimeout()` and `Retry()`.

- **Miscellaneous Utilities**:  
  Additional helpers for Boolean, GUIDs, enums, and more.

## Installation

To install **UE.Core.Extensions**, use the following command:

```bash
dotnet add package UE.Core.Extensions
