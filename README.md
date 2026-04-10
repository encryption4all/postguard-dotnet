# postguard-dotnet

PostGuard .NET SDK — identity-based encryption for secure file sharing.

**Scope:** B2C sending side only — encryption with API key signing. Decryption is handled by the receiving side via [postguard.eu](https://postguard.eu) or the mail plugins.

## Usage

```csharp
using E4A.PostGuard;
using E4A.PostGuard.Models;

var pg = new PostGuard(new PostGuardConfig
{
    PkgUrl = "https://pkg.staging.postguard.eu",
    CryptifyUrl = "https://fileshare.staging.postguard.eu"
});

var sealed = pg.Encrypt(new EncryptInput
{
    Files = [
        new PgFile("report.txt", fileStream)
    ],
    Recipients = [
        pg.Recipient.Email("citizen@example.com"),
        pg.Recipient.EmailDomain("info@org.nl")
    ],
    Sign = pg.Sign.ApiKey("PG-API-xxx")
});

// Upload only — returns UUID for custom email distribution
var result = await sealed.UploadAsync();
Console.WriteLine(result.Uuid);

// Upload with email notification
var result = await sealed.UploadAsync(new UploadOptions
{
    Notify = new NotifyOptions
    {
        Message = "Your documents",
        Language = "EN"
    }
});

// Or get raw sealed bytes (no upload)
byte[] bytes = await sealed.ToBytesAsync();
```

## Building

### Prerequisites

- .NET 8.0+ SDK
- Rust toolchain (`rustup`)

### Build native library

The `pg-ffi` crate lives in the [postguard](https://github.com/encryption4all/postguard) repo:

```bash
cd ../postguard/pg-ffi
./build.sh
```

This compiles the Rust FFI crate and copies the native library to `src/runtimes/`.

### Build .NET solution

```bash
dotnet build E4A.PostGuard.slnx
```

### Run example

See [postguard-examples/pg-dotnet](https://github.com/encryption4all/postguard-examples/tree/main/pg-dotnet).

## Architecture

```
PostGuard (C#)
  ├── pg.Encrypt() → Sealed (lazy builder)
  │     ├── .UploadAsync()   → seal + upload to Cryptify
  │     └── .ToBytesAsync()  → seal only
  ├── PkgClient   → GET /v2/parameters, POST /v2/irma/sign/key
  ├── CryptifyClient → chunked upload protocol
  ├── ZipHelper → System.IO.Compression
  └── Native (P/Invoke) → libpg_ffi.dylib
        └── pg-core (Rust) → IBE encryption + IBS signing
```
