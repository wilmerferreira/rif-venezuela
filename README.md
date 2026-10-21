# Rif Venezuela

This eventually will become a NuGet package for handling venezuelan tax identification numbers (RIF - Registro de Información Fiscal).

## How to install the nuget package

Once published, the package will be available on NuGet as [`RifVenezuela`](https://www.nuget.org/packages/RifVenezuela).

### .NET CLI

From the directory containing your project file, run:

```sh
dotnet add package RifVenezuela
```

To install a specific version, use the `--version` option:

```sh
dotnet add package RifVenezuela --version <version>
```

### Visual Studio

1. Right-click the project in **Solution Explorer** and select **Manage NuGet Packages**.
2. Select the **Browse** tab and search for `RifVenezuela`.
3. Select the package published by `Wilmer Ferreira` and click **Install**.

### Package Manager Console

In Visual Studio, open **Tools > NuGet Package Manager > Package Manager Console** and run:

```powershell
Install-Package RifVenezuela
```

After installation, restore packages and build your project to verify the reference:

```sh
dotnet restore
dotnet build
```

## How to use this repo

### Prerequisites

Install [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)

### Build and Test

From the repository root, restore the dependencies and build the solution:

```sh
dotnet restore .\rif-venezuela.slnx
dotnet build .\rif-venezuela.slnx --configuration Release --no-restore
```

Run the test suite with:

```sh
dotnet test .\RifVenezuela.Tests\RifVenezuela.Tests.csproj --configuration Release --no-restore
```

To build or test an individual project, run:

```sh
dotnet build .\RifVenezuela\RifVenezuela.csproj
dotnet test .\RifVenezuela.Tests\RifVenezuela.Tests.csproj
```

## More Information

- [Registro Único de Información Fiscal - Wikipedia](https://es.wikipedia.org/wiki/Registro_%C3%9Anico_de_Informaci%C3%B3n_Fiscal)
