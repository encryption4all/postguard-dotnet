# postguard-dotnet

The .NET/C# SDK for PostGuard, published as `E4A.PostGuard` on NuGet. It wraps
`postguard`'s `pg-ffi` native library through P/Invoke, and it covers one half of
the product: **sending only** — encrypt for a set of recipient identities, zip,
seal, upload to Cryptify. There is no decrypt path here to mirror. Recipients
decrypt through postguard.eu or the mail plugins.

## Where it sits

PostGuard is end-to-end encrypted email and file sending built on Identity-Based
Encryption: a sender needs only the recipient's identity (an email address), and
the recipient proves that identity to a Private Key Generator (PKG) to get a
decryption key. Yivi does that identity check, so PostGuard is a Yivi consumer.

One company, two GitHub orgs. `privacybydesign` is the Yivi/IRMA lineage;
`encryption4all` is the vehicle the PostGuard research project used to apply for
grants, kept as an org after Yivi bought PostGuard to commercialise it. The split
is historical, not organisational — same company, same maintainers, same review
conventions, and we are maintainers on both sides rather than outside
contributors.

## Repos a change here touches

- `encryption4all/postguard` — the root of the family. `pg-ffi` is built there and
  pinned here to one of its releases in `.github/pg-ffi-version`; `pg-pkg` and
  `cryptify/` are the two servers this code talks to. A wire-format or `/v2`
  change starts there, and its `COMPATIBILITY.md` is the contract such a change
  has to argue against.
- `encryption4all/postguard-e2e` — the end-to-end harness, which sweeps the last
  major of `E4A.PostGuard` against a target server. A compatibility break surfaces
  there and not in this repo's tests.
- `encryption4all/postguard-js` — the sibling SDK over the same core. A change to
  the sending flow often belongs in both.
- `encryption4all/postguard-docs` — `docs.postguard.eu/repos/postguard-dotnet`,
  this repo's documentation home. The README points there.

## Where the operational knowledge is

Not in this file.

- **Documentation** — how the SDK is put together, how the tracked public API
  surface works, how a release is cut: `docs.postguard.eu/repos/postguard-dotnet`.
- **Durable checks** — the host narrows a binding-rule bundle to the repo a task is
  in and lands it in the container at `~/dobby-rules.md`. A container that learns
  something durable files a rule; it does not write it here.
- **Repo invariants** — carried by the code and tests that enforce them, with the
  reason written at the point of enforcement: the pinned `pg-ffi` release
  (`PgFfiVersionTests`), the tracked public API surface
  (`PublicApiTrackingTests`), the inclusive `Content-Range` end (`CryptifyClient`),
  zip entry-name sanitization (`ZipHelper`).

This file is orientation, and `ClaudeMdOrientationTests` holds it to 4,000 bytes.

The corpus it used to be is in git history: 3,525 bytes at `40004d5`, the last
revision carrying it (`git show 40004d5:CLAUDE.md`).
