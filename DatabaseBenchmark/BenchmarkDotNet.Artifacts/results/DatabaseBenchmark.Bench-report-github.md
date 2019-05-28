``` ini

BenchmarkDotNet=v0.11.5, OS=Windows 10.0.18362
Intel Core i7-3770 CPU 3.40GHz (Ivy Bridge), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100-preview5-011568
  [Host] : .NET Core 3.0.0-preview5-27626-15 (CoreCLR 4.6.27622.75, CoreFX 4.700.19.22408), 64bit RyuJIT
  Core   : .NET Core 3.0.0-preview5-27626-15 (CoreCLR 4.6.27622.75, CoreFX 4.700.19.22408), 64bit RyuJIT

Job=Core  Runtime=Core  

```
|  Method |                   W |        Mean |     Error |     StdDev |      Median | Ratio | RatioSD |  Gen 0 | Gen 1 | Gen 2 | Allocated |
|-------- |-------------------- |------------:|----------:|-----------:|------------:|------:|--------:|-------:|------:|------:|----------:|
|  **EFCore** |     **SelectEmployees** |   **571.95 us** | **48.197 us** | **138.287 us** |   **540.50 us** |  **1.00** |    **0.00** |      **-** |     **-** |     **-** |   **10232 B** |
| Linq2Db |     SelectEmployees |   336.18 us | 37.121 us | 108.285 us |   310.75 us |  0.62 |    0.25 |      - |     - |     - |    8120 B |
|     SQL |     SelectEmployees |   100.59 us |  1.866 us |   1.654 us |   100.18 us |  0.19 |    0.03 | 1.0986 |     - |     - |    4632 B |
|         |                     |             |           |            |             |       |         |        |       |       |           |
|  **EFCore** | **SelectEmployeeCount** |   **578.23 us** | **53.675 us** | **147.836 us** |   **541.85 us** |  **1.00** |    **0.00** |      **-** |     **-** |     **-** |    **6128 B** |
| Linq2Db | SelectEmployeeCount |   418.09 us | 60.182 us | 174.599 us |   350.80 us |  0.77 |    0.42 |      - |     - |     - |    3840 B |
|     SQL | SelectEmployeeCount |    89.94 us |  1.790 us |   2.510 us |    89.20 us |  0.17 |    0.03 | 0.2441 |     - |     - |    1208 B |
|         |                     |             |           |            |             |       |         |        |       |       |           |
|  **EFCore** |      **UpdateEmployee** | **1,343.62 us** | **81.875 us** | **228.236 us** | **1,287.95 us** |  **1.00** |    **0.00** |      **-** |     **-** |     **-** |   **31008 B** |
| Linq2Db |      UpdateEmployee |   939.48 us | 59.664 us | 162.321 us |   893.40 us |  0.72 |    0.17 |      - |     - |     - |   17360 B |
|     SQL |      UpdateEmployee |   209.78 us |  4.122 us |   5.213 us |   209.13 us |  0.15 |    0.02 |      - |     - |     - |     640 B |
