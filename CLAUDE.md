# Agent notes (migrated from the dobby memory repo)

## Overview
C# SDK for PostGuard (PKG sign + Cryptify upload). xUnit tests. Multi-targets `net8.0` and `net10.0`. Release: release-please.

## Workspace limitation
The Coder workspace ships the net10.0 SDK but not the net8.0 runtime: `dotnet test --framework net8.0` fails with a "framework 'Microsoft.NETCore.App', version '8.0.0' not found" error. `dotnet build` succeeds for both TFMs (build doesn't need the runtime). Run tests on net10.0 locally; CI exercises both. Note this in the PR body if relevant.

## API layout
- `src/Api/PkgClient.cs`: PKG sign-key + MPK fetch.
- `src/Api/CryptifyClient.cs`: chunked Cryptify upload (init / store-chunk / finalize).
- `src/Exceptions/PostGuardException.cs`: exception hierarchy; `NetworkException` is the wire-failure type.
- `HttpResponseMessage.RequestMessage.RequestUri` is automatically set by `HttpClient.SendAsync`, useful when adding context to exception messages without per-call-site plumbing.

## Chunked upload range header
`CryptifyClient`'s chunk `Content-Range` header uses the RFC 9110 §14.4 inclusive end (`bytes 0-1048575/*`, i.e. `end - 1`), not an exclusive end. Any test or client asserting the range format must expect the inclusive form.

## Zip Slip sanitization
`ZipHelper.CreateZip` sanitizes entry names to the bare file component. Do not use `Path.GetFileName` for this, it's platform-dependent (on Linux/macOS it ignores `\`), so Windows-style `..\..\` traversal survives when archives are built on Linux and extracted on Windows. Use `LastIndexOfAny(['/','\\'])` plus the last segment instead. Also reject empty / `.` / `..` and duplicate sanitized names (`ZipArchive` silently allows duplicates, which loses entries on extraction).
