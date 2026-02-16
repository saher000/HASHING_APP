# HASHING_APP

A simple C# console application that hashes text input or file content.

## Requirements

- .NET 8 SDK

## Run

Hash text (defaults to SHA256):

```bash
dotnet run --project src/HashingApp -- "hello world"
```

Hash text with a specific algorithm:

```bash
dotnet run --project src/HashingApp -- "hello world" SHA512
```

Hash a file:

```bash
dotnet run --project src/HashingApp -- --file ./path/to/file.txt SHA256
```

## Supported algorithms

- MD5
- SHA1
- SHA256
- SHA384
- SHA512
