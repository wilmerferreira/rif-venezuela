# Rif Venezuela

Rif Venezuela is a .NET library for creating, parsing, validating, and formatting Venezuelan tax identification numbers (RIF, *Registro de Información Fiscal*). It supports the taxpayer kinds `V`, `E`, `J`, `C`, `P`, and `G`, calculates the check digit when creating a RIF, and verifies it when parsing one. The library targets .NET Standard 2.0.

## Installation

Install the `RifVenezuela` package from NuGet into your project:

```sh
dotnet add package RifVenezuela
```

In Visual Studio, you can also right-click the project, choose **Manage NuGet Packages**, search for `RifVenezuela`, and install it.

## Usage

Create a RIF with its taxpayer kind and identifier. The check digit is calculated automatically:

```csharp
using RifVenezuela;

var rif = new Rif('V', 12345678);

Console.WriteLine(rif);              // V-12345678-1
Console.WriteLine(rif.ToString("N")); // V123456781
Console.WriteLine(rif.ToString("B")); // {V-12345678-1}
```

The constructor accepts kinds `V`, `E`, `J`, `C`, `P`, and `G` (case-insensitive), and identifiers from 1 to 99,999,999. It throws `ArgumentOutOfRangeException` when either argument is invalid.

Parse a RIF string to validate its format and check digit:

```csharp
var parsed = Rif.Parse("V-12345678-1");
Console.WriteLine(parsed.Kind);       // V
Console.WriteLine(parsed.Identifier); // 12345678
Console.WriteLine(parsed.CheckDigit); // 1
```

`Parse` accepts the standard dashed format (`D`), the compact format (`N`), and the braced format (`B`). Use `ParseExact` to require one format, or `TryParse` / `TryParseExact` when invalid input should be handled without an exception:

```csharp
if (Rif.TryParse("V123456781", out var rif))
{
    Console.WriteLine(rif);
}
```

`Parse` and `ParseExact` throw `FormatException` for invalid RIF values. The format specifiers are:

| Format        | Example           |
| ------------- | ----------------- |
| `D` (default) | `V-12345678-1`    |
| `N`           | `V123456781`      |
| `B`           | `{V-12345678-1}`  |

## More information

- [Contributing to this repository](CONTRIBUTING.md)
- [Registro Único de Información Fiscal - Wikipedia](https://es.wikipedia.org/wiki/Registro_%C3%9Anico_de_Informaci%C3%B3n_Fiscal)
