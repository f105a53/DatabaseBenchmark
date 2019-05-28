``` ini

BenchmarkDotNet=v0.11.5, OS=Windows 10.0.18362
Intel Core i7-3770 CPU 3.40GHz (Ivy Bridge), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100-preview5-011568
  [Host] : .NET Core 3.0.0-preview5-27626-15 (CoreCLR 4.6.27622.75, CoreFX 4.700.19.22408), 64bit RyuJIT
  Core   : .NET Core 3.0.0-preview5-27626-15 (CoreCLR 4.6.27622.75, CoreFX 4.700.19.22408), 64bit RyuJIT

Job=Core  Runtime=Core  

```
|  Method |                   W |        Mean |      Error |     StdDev |      Median | Ratio | RatioSD |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|-------- |-------------------- |------------:|-----------:|-----------:|------------:|------:|--------:|-------:|------:|------:|----------:|
|  **EFCore** |     **SelectEmployees** |   **606.52 us** |  **54.267 us** | **154.828 us** |   **581.10 us** |  **1.00** |    **0.00** |      **-** |     **-** |     **-** |   **10232 B** |
| Linq2Db |     SelectEmployees |   311.19 us |  32.800 us |  91.974 us |   306.40 us |  0.54 |    0.21 |      - |     - |     - |    8120 B |
|     SQL |     SelectEmployees |   100.89 us |   1.952 us |   1.826 us |   100.70 us |  0.17 |    0.04 | 1.0986 |     - |     - |    4632 B |
|         |                     |             |            |            |             |       |         |        |       |       |           |
|  **EFCore** | **SelectEmployeeCount** |   **585.83 us** |  **53.900 us** | **152.027 us** |   **527.95 us** |  **1.00** |    **0.00** |      **-** |     **-** |     **-** |    **6128 B** |
| Linq2Db | SelectEmployeeCount |   349.02 us |  41.240 us | 120.949 us |   328.30 us |  0.63 |    0.25 |      - |     - |     - |    3768 B |
|     SQL | SelectEmployeeCount |    88.08 us |   1.710 us |   1.969 us |    88.27 us |  0.17 |    0.03 | 0.2441 |     - |     - |    1208 B |
|         |                     |             |            |            |             |       |         |        |       |       |           |
|  **EFCore** |      **UpdateEmployee** | **1,419.76 us** | **102.134 us** | **286.394 us** | **1,337.90 us** |  **1.00** |    **0.00** |      **-** |     **-** |     **-** |   **31008 B** |
| Linq2Db |      UpdateEmployee | 1,016.61 us |  78.143 us | 220.405 us |   972.00 us |  0.74 |    0.21 |      - |     - |     - |   17360 B |
|     SQL |      UpdateEmployee |   208.99 us |   4.030 us |   3.958 us |   208.88 us |  0.16 |    0.01 |      - |     - |     - |     640 B |
