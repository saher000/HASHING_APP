using System.Security.Cryptography;
using System.Text;

if (args.Length == 0)
{
    Console.WriteLine("Usage:");
    Console.WriteLine("  dotnet run --project src/HashingApp -- <text-to-hash> [algorithm]");
    Console.WriteLine("  dotnet run --project src/HashingApp -- --file <file-path> [algorithm]");
    Console.WriteLine();
    Console.WriteLine("Supported algorithms: MD5, SHA1, SHA256 (default), SHA384, SHA512");
    return;
}

var defaultAlgorithm = "SHA256";
string algorithmName;
byte[] inputBytes;

if (args[0].Equals("--file", StringComparison.OrdinalIgnoreCase))
{
    if (args.Length < 2)
    {
        Console.Error.WriteLine("Error: --file requires a file path.");
        return;
    }

    var filePath = args[1];
    if (!File.Exists(filePath))
    {
        Console.Error.WriteLine($"Error: File not found: {filePath}");
        return;
    }

    inputBytes = File.ReadAllBytes(filePath);
    algorithmName = args.Length > 2 ? args[2] : defaultAlgorithm;
}
else
{
    var text = args[0];
    inputBytes = Encoding.UTF8.GetBytes(text);
    algorithmName = args.Length > 1 ? args[1] : defaultAlgorithm;
}

using var algorithm = CreateAlgorithm(algorithmName);
if (algorithm is null)
{
    Console.Error.WriteLine($"Error: Unsupported algorithm '{algorithmName}'.");
    return;
}

var hashBytes = algorithm.ComputeHash(inputBytes);
var hashHex = Convert.ToHexString(hashBytes).ToLowerInvariant();

Console.WriteLine($"Algorithm : {algorithm.GetType().Name.Replace("CryptoServiceProvider", string.Empty)}");
Console.WriteLine($"Hash      : {hashHex}");

static HashAlgorithm? CreateAlgorithm(string name) =>
    name.ToUpperInvariant() switch
    {
        "MD5" => MD5.Create(),
        "SHA1" => SHA1.Create(),
        "SHA256" => SHA256.Create(),
        "SHA384" => SHA384.Create(),
        "SHA512" => SHA512.Create(),
        _ => null
    };
