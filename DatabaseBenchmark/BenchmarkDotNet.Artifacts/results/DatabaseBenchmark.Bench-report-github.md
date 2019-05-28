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
|  **EFCore** |     **SelectEmployees** |   **572.77 us** |  **41.472 us** | **117.648 us** |   **548.00 us** |  **1.00** |    **0.00** |      **-** |     **-** |     **-** |   **10688 B** |
| Linq2Db |     SelectEmployees |   333.60 us |  31.939 us |  91.125 us |   331.45 us |  0.61 |    0.21 |      - |     - |     - |    8736 B |
|     SQL |     SelectEmployees |   100.50 us |   2.101 us |   2.805 us |    99.84 us |  0.20 |    0.04 | 1.0986 |     - |     - |    4664 B |
|         |                     |             |            |            |             |       |         |        |       |       |           |
|  **EFCore** | **SelectEmployeeCount** |   **584.00 us** |  **62.930 us** | **181.568 us** |   **522.30 us** |  **1.00** |    **0.00** |      **-** |     **-** |     **-** |    **6160 B** |
| Linq2Db | SelectEmployeeCount |   335.56 us |  39.758 us | 113.431 us |   333.55 us |  0.64 |    0.33 |      - |     - |     - |    4008 B |
|     SQL | SelectEmployeeCount |    89.66 us |   1.945 us |   3.084 us |    88.08 us |  0.16 |    0.04 | 0.2441 |     - |     - |    1240 B |
|         |                     |             |            |            |             |       |         |        |       |       |           |
|  **EFCore** |      **UpdateEmployee** | **1,348.17 us** |  **67.918 us** | **190.451 us** | **1,320.10 us** |  **1.00** |    **0.00** |      **-** |     **-** |     **-** |   **31008 B** |
| Linq2Db |      UpdateEmployee |   969.08 us |  74.793 us | 203.479 us |   931.65 us |  0.73 |    0.19 |      - |     - |     - |   17360 B |
|     SQL |      UpdateEmployee |   205.11 us |   3.887 us |   3.035 us |   204.39 us |  0.14 |    0.02 |      - |     - |     - |     640 B |
|         |                     |             |            |            |             |       |         |        |       |       |           |
|  **EFCore** |        **CreateDelete** | **3,264.09 us** | **256.630 us** | **748.601 us** | **3,056.35 us** |  **1.00** |    **0.00** |      **-** |     **-** |     **-** |   **68104 B** |
| Linq2Db |        CreateDelete | 1,382.14 us | 114.268 us | 324.158 us | 1,272.50 us |  0.44 |    0.14 |      - |     - |     - |   13872 B |
|     SQL |        CreateDelete |   790.21 us |  15.225 us |  16.922 us |   788.77 us |  0.23 |    0.04 |      - |     - |     - |    2280 B |
