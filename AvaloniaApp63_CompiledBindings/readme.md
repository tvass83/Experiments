``` ini

BenchmarkDotNet=v0.13.2, OS=Windows 10 (10.0.19042.804/20H2/October2020Update)
Intel Core i7-4770 CPU 3.40GHz (Haswell), 1 CPU, 8 logical and 4 physical cores
  [Host]               : .NET Framework 4.8 (4.8.4300.0), X64 RyuJIT VectorSize=256
  .NET 6.0             : .NET 6.0.1 (6.0.121.56705), X64 RyuJIT AVX2
  .NET Framework 4.7.2 : .NET Framework 4.8 (4.8.4300.0), X64 RyuJIT VectorSize=256


```
| Method |                  Job |              Runtime |     Mean |    Error |   StdDev | Ratio |   Gen0 |   Gen1 | Allocated | Alloc Ratio |
|------- |--------------------- |--------------------- |---------:|---------:|---------:|------:|-------:|-------:|----------:|------------:|
|  Test1 |             .NET 6.0 |             .NET 6.0 | 40.41 μs | 0.261 μs | 0.231 μs |  1.00 | 4.3945 | 0.1221 |  18.18 KB |        1.00 |
|  Test2 |             .NET 6.0 |             .NET 6.0 | 38.56 μs | 0.144 μs | 0.120 μs |  0.95 | 4.4556 | 0.1221 |  18.37 KB |        1.01 |
|        |                      |                      |          |          |          |       |        |        |           |             |
|  Test1 | .NET Framework 4.7.2 | .NET Framework 4.7.2 | 71.49 μs | 0.458 μs | 0.358 μs |  1.00 | 4.5166 |      - |  18.51 KB |        1.00 |
|  Test2 | .NET Framework 4.7.2 | .NET Framework 4.7.2 | 72.16 μs | 0.117 μs | 0.097 μs |  1.01 | 4.5166 |      - |  18.92 KB |        1.02 |
