``` ini

BenchmarkDotNet=v0.11.5, OS=Windows 10.0.18362
Intel Core i7-3770 CPU 3.40GHz (Ivy Bridge), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100-preview5-011568
  [Host] : .NET Core 3.0.0-preview5-27626-15 (CoreCLR 4.6.27622.75, CoreFX 4.700.19.22408), 64bit RyuJIT
  Core   : .NET Core 3.0.0-preview5-27626-15 (CoreCLR 4.6.27622.75, CoreFX 4.700.19.22408), 64bit RyuJIT

Job=Core  Runtime=Core  

```
|  Method |    SelectedWorkload |       Mean |       Error |     StdDev |     Median | Ratio | RatioSD | Gen 0 | Gen 1 | Gen 2 | Allocated |
|-------- |-------------------- |-----------:|------------:|-----------:|-----------:|------:|--------:|------:|------:|------:|----------:|
|  **EFCore** |     **SelectEmployees** |   **818.1 us** |    **97.10 us** |   **278.6 us** |   **723.6 us** |  **1.00** |    **0.00** |     **-** |     **-** |     **-** |  **10.06 KB** |
| Linq2Db |     SelectEmployees |   363.8 us |    35.67 us |   100.6 us |   342.6 us |  0.49 |    0.22 |     - |     - |     - |      8 KB |
|         |                     |            |             |            |            |       |         |       |       |       |           |
|  **EFCore** | **SelectEmployeeCount** |   **641.5 us** |    **74.36 us** |   **214.5 us** |   **548.6 us** |  **1.00** |    **0.00** |     **-** |     **-** |     **-** |   **6.05 KB** |
| Linq2Db | SelectEmployeeCount |   360.2 us |    41.15 us |   120.7 us |   341.9 us |  0.63 |    0.30 |     - |     - |     - |   3.75 KB |
|         |                     |            |             |            |            |       |         |       |       |       |           |
|  **EFCore** |      **UpdateEmployee** | **1,527.9 us** |   **146.43 us** |   **408.2 us** | **1,351.9 us** |  **1.00** |    **0.00** |     **-** |     **-** |     **-** |  **30.45 KB** |
| Linq2Db |      UpdateEmployee | 4,879.0 us | 1,261.48 us | 3,719.5 us | 6,325.9 us |  3.22 |    2.73 |     - |     - |     - |  17.02 KB |
