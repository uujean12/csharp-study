# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project

A C# algorithm study project targeting .NET 10. Single console application — `Program.cs` is the entry point where algorithms are implemented and exercised.

## Commands

```bash
# Build
dotnet build

# Run
dotnet run

# Run with specific args
dotnet run -- <args>
```

There are no tests yet. When a test project is added, the typical command will be `dotnet test`.

## Structure

- `CsharpAlgorithm.csproj` — targets `net10.0`, nullable enabled, implicit usings enabled
- `Program.cs` — top-level statements; algorithm implementations go here (or in separate files added to this project)

## Conventions

- Nullable reference types are enabled (`<Nullable>enable</Nullable>`); avoid null-forgiveness operators unless necessary
- Implicit usings are on, so `System`, `System.Collections.Generic`, `System.Linq`, etc. are available without explicit `using` directives
