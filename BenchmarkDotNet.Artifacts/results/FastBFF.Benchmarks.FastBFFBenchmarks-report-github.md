``` ini

BenchmarkDotNet=v0.12.1, OS=Windows 10.0.19041.388 (2004/?/20H1)
AMD Ryzen 9 3950X, 1 CPU, 24 logical and 24 physical cores
.NET Core SDK=5.0.100-rc.1.20414.5
  [Host]     : .NET Core 3.1.7 (CoreCLR 4.700.20.36602, CoreFX 4.700.20.37001), X64 RyuJIT
  Job-ZWJDYK : .NET Core 3.1.7 (CoreCLR 4.700.20.36602, CoreFX 4.700.20.37001), X64 RyuJIT
  Job-LSENZC : .NET Core 5.0.0 (CoreCLR 5.0.20.40416, CoreFX 5.0.20.40416), X64 RyuJIT


```
|                    Method |        Job |       Runtime |    Toolchain |       Mean |    Error |   StdDev |     Median | Ratio | RatioSD |       Gen 0 |       Gen 1 |     Gen 2 |  Allocated |
|-------------------------- |----------- |-------------- |------------- |-----------:|---------:|---------:|-----------:|------:|--------:|------------:|------------:|----------:|-----------:|
|           GetJsonNetClass | Job-ZWJDYK | .NET Core 3.1 | netcoreapp31 | 2,888.0 ms | 30.44 ms | 28.47 ms | 2,887.1 ms |  1.00 |    0.00 | 377000.0000 | 187000.0000 |         - | 2782.62 MB |
|    GetSystemTextJsonClass | Job-ZWJDYK | .NET Core 3.1 | netcoreapp31 | 1,852.3 ms | 15.09 ms | 13.37 ms | 1,850.9 ms |  0.64 |    0.01 | 193000.0000 |  92000.0000 |         - | 1477.24 MB |
|          GetJsonNetStruct | Job-ZWJDYK | .NET Core 3.1 | netcoreapp31 | 3,362.9 ms | 47.42 ms | 42.04 ms | 3,361.5 ms |  1.16 |    0.01 | 558000.0000 | 277000.0000 | 1000.0000 |  4150.5 MB |
|       WriteToResponseBody | Job-ZWJDYK | .NET Core 3.1 | netcoreapp31 | 1,346.9 ms | 25.11 ms | 23.48 ms | 1,345.0 ms |  0.47 |    0.01 | 129000.0000 |  46000.0000 |         - |  972.89 MB |
|         WriteToBodyWriter | Job-ZWJDYK | .NET Core 3.1 | netcoreapp31 | 1,077.1 ms | 10.83 ms |  9.60 ms | 1,077.8 ms |  0.37 |    0.01 |  93000.0000 |  33000.0000 |         - |  717.85 MB |
|        RawGetJsonNetClass | Job-ZWJDYK | .NET Core 3.1 | netcoreapp31 |   774.9 ms | 15.42 ms | 19.50 ms |   771.4 ms |  0.27 |    0.01 | 271000.0000 | 129000.0000 |         - | 2029.45 MB |
| RawGetSystemTextJsonClass | Job-ZWJDYK | .NET Core 3.1 | netcoreapp31 |   340.2 ms |  6.65 ms |  7.66 ms |   341.2 ms |  0.12 |    0.00 |  98000.0000 |  32000.0000 |         - |  722.59 MB |
|       RawGetJsonNetStruct | Job-ZWJDYK | .NET Core 3.1 | netcoreapp31 | 1,222.9 ms | 24.42 ms | 35.03 ms | 1,211.7 ms |  0.43 |    0.02 | 455000.0000 | 227000.0000 |         - | 3413.42 MB |
|    RawWriteToResponseBody | Job-ZWJDYK | .NET Core 3.1 | netcoreapp31 |   186.7 ms |  3.70 ms |  6.47 ms |   183.4 ms |  0.06 |    0.00 |  59000.0000 |  28666.6667 |         - |  443.29 MB |
|      RawWriteToBodyWriter | Job-ZWJDYK | .NET Core 3.1 | netcoreapp31 |   128.8 ms |  2.36 ms |  2.21 ms |   128.5 ms |  0.04 |    0.00 |  27000.0000 |  13250.0000 |         - |  213.65 MB |
|           GetJsonNetClass | Job-LSENZC | .NET Core 5.0 | netcoreapp50 | 2,826.2 ms | 28.19 ms | 24.99 ms | 2,819.7 ms |  0.98 |    0.01 | 372000.0000 | 182000.0000 |         - | 2731.49 MB |
|    GetSystemTextJsonClass | Job-LSENZC | .NET Core 5.0 | netcoreapp50 | 1,793.6 ms | 16.52 ms | 14.64 ms | 1,793.0 ms |  0.62 |    0.01 | 188000.0000 |  84000.0000 |         - | 1433.62 MB |
|          GetJsonNetStruct | Job-LSENZC | .NET Core 5.0 | netcoreapp50 | 3,267.5 ms | 60.28 ms | 59.20 ms | 3,244.4 ms |  1.13 |    0.02 | 552000.0000 | 275000.0000 |         - | 4111.61 MB |
|       WriteToResponseBody | Job-LSENZC | .NET Core 5.0 | netcoreapp50 | 1,403.5 ms | 15.21 ms | 14.23 ms | 1,404.9 ms |  0.49 |    0.01 | 121000.0000 |  41000.0000 |         - |   911.4 MB |
|         WriteToBodyWriter | Job-LSENZC | .NET Core 5.0 | netcoreapp50 | 1,035.5 ms | 20.41 ms | 20.04 ms | 1,034.8 ms |  0.36 |    0.01 |  86000.0000 |  30000.0000 |         - |  670.53 MB |
|        RawGetJsonNetClass | Job-LSENZC | .NET Core 5.0 | netcoreapp50 |   799.5 ms | 15.63 ms | 23.87 ms |   793.3 ms |  0.28 |    0.01 | 271000.0000 | 129000.0000 |         - |  2026.4 MB |
| RawGetSystemTextJsonClass | Job-LSENZC | .NET Core 5.0 | netcoreapp50 |   306.9 ms |  5.49 ms |  4.87 ms |   304.7 ms |  0.11 |    0.00 |  98000.0000 |  32000.0000 |         - |  728.64 MB |
|       RawGetJsonNetStruct | Job-LSENZC | .NET Core 5.0 | netcoreapp50 | 1,216.7 ms | 22.48 ms | 40.53 ms | 1,207.3 ms |  0.43 |    0.02 | 455000.0000 | 226000.0000 |         - | 3410.37 MB |
|    RawWriteToResponseBody | Job-LSENZC | .NET Core 5.0 | netcoreapp50 |   181.5 ms |  3.41 ms |  6.89 ms |   177.6 ms |  0.06 |    0.00 |  59000.0000 |  29000.0000 |         - |  443.29 MB |
|      RawWriteToBodyWriter | Job-LSENZC | .NET Core 5.0 | netcoreapp50 |   127.8 ms |  1.94 ms |  1.72 ms |   127.7 ms |  0.04 |    0.00 |  27250.0000 |  13250.0000 |         - |  213.65 MB |
