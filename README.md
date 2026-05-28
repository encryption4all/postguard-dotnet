# <p align="center"><img src="./img/pg_logo.svg" height="128px" alt="PostGuard" /></p>

> For full documentation, visit [docs.postguard.eu](https://docs.postguard.eu/repos/postguard-dotnet).

.NET SDK for PostGuard encryption, published as `E4A.PostGuard` on NuGet. Allows .NET applications to encrypt files for recipients using identity-based encryption. This is a sending-side SDK: it handles encryption and upload, while decryption is handled by the recipient through [postguard.eu](https://postguard.eu) or the mail plugins.

## Quick Start

```csharp
using E4A.PostGuard;
using E4A.PostGuard.Models;

var pg = new PostGuard(new PostGuardConfig
{
    PkgUrl = "https://pkg.postguard.eu",
    CryptifyUrl = "https://fileshare.postguard.eu"
});

var encrypted = pg.Encrypt(new EncryptInput
{
    Files = [new PgFile("report.txt", fileStream)],
    Recipients = [pg.Recipient.Email("citizen@example.com")],
    Sign = pg.Sign.ApiKey("PG-API-xxx")
});

var result = await encrypted.UploadAsync();
Console.WriteLine(result.Uuid);
```

See the [full API reference](https://docs.postguard.eu/repos/postguard-dotnet) for all encryption options, recipient types, and upload/notification methods.

## Development

### Prerequisites

- .NET 8.0+ SDK
- Rust toolchain ([rustup](https://rustup.rs/))

### Build native library

The `pg-ffi` crate lives in the [postguard](https://github.com/encryption4all/postguard) repo. Build it first:

```bash
cd ../postguard/pg-ffi
./build.sh
```

This compiles the Rust FFI crate and copies the native library to `src/runtimes/`.

### Build the .NET solution

```bash
dotnet build E4A.PostGuard.slnx
```

### Run the example

See [postguard-examples/pg-dotnet](https://github.com/encryption4all/postguard-examples/tree/main/pg-dotnet).

## Releasing

Releases are automated with [release-please](https://github.com/googleapis/release-please). When changes land on `main`, release-please opens a release PR. Merging that PR triggers CI to download pre-built `pg-ffi` native libraries and publish the package to NuGet.

## License

MIT
