# MySimpleDictionary

This project contains a simple implementation of a custom dictionary class and a benchmark test class.  

## Classes

### `MySimpleDictionary<TKey, TValue>`
A custom dictionary with the following methods:
- `Add(TKey key, TValue value)` – Adds a new key-value pair
- `Count()` – Returns number of elements
- `Remove(TKey key)` – Removes a pair by key
- `dict[key] = value` – Retrieves the value associated with the key
- `ContainsKey(TKey key)` – Checks if the key exists
- `ContainsValue(TValue)` – Checks if the value exists
- `Keys` – Returns all keys
- `Values` – Returns all values
- `Clear()` - deletes content of a dictionary
- `Resize()` - resizes a dictionary 
- Iterating


### BenchmarkTests
Contains performance tests for measuring execution time of operations on:
- `MySimpleDictionary`
- Built-in `Dictionary<TKey, TValue>`

