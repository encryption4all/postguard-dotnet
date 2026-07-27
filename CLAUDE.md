# Agent notes (migrated from the dobby memory repo)

## Overview
C# SDK for PostGuard (PKG sign + Cryptify upload). xUnit tests. Multi-targets `net8.0` and `net10.0`. Release: release-please.

## Workspace limitation
The Coder workspace ships the net10.0 SDK but not the net8.0 runtime: `dotnet test --framework net8.0` fails with a "framework 'Microsoft.NETCore.App', version '8.0.0' not found" error. `dotnet build` succeeds for both TFMs (build doesn't need the runtime). Run tests on net10.0 locally; CI exercises both. Note this in the PR body if relevant.

## Pinned pg-ffi native binaries
`.github/pg-ffi-version` holds one line: the exact `encryption4all/postguard` release tag both workflows download the native libraries from. Bump that file to move to a newer release; don't reintroduce a "newest `pg-ffi-*` release" lookup, it changes what ships without a commit. The Dobby App cannot push `.github/workflows/`, so workflow edits have to go to a maintainer as a patch in a PR comment.

## Tracked public API surface
`src/PublicAPI.Shipped.txt` and `src/PublicAPI.Unshipped.txt` list every public member of `E4A.PostGuard`, checked by Microsoft.CodeAnalysis.PublicApiAnalyzers during `dotnet build` (no separate CI step).

- Changing the public surface without updating the files fails the build. Add new members to `PublicAPI.Unshipped.txt`; record a removal there as `*REMOVED*<the exact shipped line>`.
- To regenerate a line, build and copy the signature out of the `RS0016` message, which prints it in the file's own format (`Namespace.Type.Member(args) -> ret`). IDEs offer this as a code fix on the diagnostic.
- Severity is raised through `<WarningsAsErrors>` in `src/E4A.PostGuard.csproj`, not `.editorconfig`. RS0017 and friends are reported against `PublicAPI.Shipped.txt`, and path-based `.editorconfig` severity does not reach additional files, so it silently stays a warning there.
- Both TFMs currently produce the same surface (no `#if` in `src/`), so one pair of files covers them. If a member ever becomes TFM-conditional the files have to be split per TFM.
- **At release time**, move the `PublicAPI.Unshipped.txt` entries into `PublicAPI.Shipped.txt` (applying `*REMOVED*` lines as deletions) and leave the unshipped file with just its `#nullable enable` header. release-please does not do this.

## API layout
- `src/Api/PkgClient.cs`: PKG sign-key + MPK fetch.
- `src/Api/CryptifyClient.cs`: chunked Cryptify upload (init / store-chunk / finalize).
- `src/Exceptions/PostGuardException.cs`: exception hierarchy; `NetworkException` is the wire-failure type.
- `HttpResponseMessage.RequestMessage.RequestUri` is automatically set by `HttpClient.SendAsync`, useful when adding context to exception messages without per-call-site plumbing.

## Chunked upload range header
`CryptifyClient`'s chunk `Content-Range` header uses the RFC 9110 §14.4 inclusive end (`bytes 0-1048575/*`, i.e. `end - 1`), not an exclusive end. Any test or client asserting the range format must expect the inclusive form.

## Zip Slip sanitization
`ZipHelper.CreateZip` sanitizes entry names to the bare file component. Do not use `Path.GetFileName` for this, it's platform-dependent (on Linux/macOS it ignores `\`), so Windows-style `..\..\` traversal survives when archives are built on Linux and extracted on Windows. Use `LastIndexOfAny(['/','\\'])` plus the last segment instead. Also reject empty / `.` / `..` and duplicate sanitized names (`ZipArchive` silently allows duplicates, which loses entries on extraction).
