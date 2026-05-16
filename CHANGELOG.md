# Changelog

## [0.4.0](https://github.com/encryption4all/postguard-dotnet/compare/E4A.PostGuard-v0.3.0...E4A.PostGuard-v0.4.0) (2026-05-16)


### Features

* multi-target net8.0 and net10.0 ([f25fa46](https://github.com/encryption4all/postguard-dotnet/commit/f25fa46029142ebd303011e648b5ee952b47aa93))


### Bug Fixes

* reject http:// URLs in PostGuardConfig to prevent plaintext credential leaks ([3fac2a8](https://github.com/encryption4all/postguard-dotnet/commit/3fac2a883252cbbc0cef75f39d891265df221bd1))
* validate PkgUrl and CryptifyUrl are https:// in PostGuardConfig ([fe6d4eb](https://github.com/encryption4all/postguard-dotnet/commit/fe6d4eb20a795696525fad367a4f241483002d00)), closes [#14](https://github.com/encryption4all/postguard-dotnet/issues/14)

## [0.3.0](https://github.com/encryption4all/postguard-dotnet/compare/E4A.PostGuard-v0.2.1...E4A.PostGuard-v0.3.0) (2026-05-02)


### Features

* **upload:** silent-by-default, opt-in Notify.Recipients / Notify.Sender ([b407942](https://github.com/encryption4all/postguard-dotnet/commit/b407942df70e47f5c7b6d470f0cfd92d7ab792bf))
* **upload:** silent-by-default, opt-in Notify.Recipients/Notify.Sender ([58a3d03](https://github.com/encryption4all/postguard-dotnet/commit/58a3d0392ef28d3c103edca888a517e266479122))

## [0.2.1](https://github.com/encryption4all/postguard-dotnet/compare/E4A.PostGuard-v0.2.0...E4A.PostGuard-v0.2.1) (2026-04-10)


### Bug Fixes

* update pg-ffi native binaries to streaming sealer ([90da661](https://github.com/encryption4all/postguard-dotnet/commit/90da66100162dadf1ab249fea77bc548ae28bbbf))

## [0.2.0](https://github.com/encryption4all/postguard-dotnet/compare/E4A.PostGuard-v0.1.2...E4A.PostGuard-v0.2.0) (2026-04-10)


### Features

* implement PostGuard .NET SDK ([197dcfb](https://github.com/encryption4all/postguard-dotnet/commit/197dcfb2dd97b4977fcf81ad0a9b6bea689ab14d))


### Bug Fixes

* remove invalid release-please action inputs ([588221d](https://github.com/encryption4all/postguard-dotnet/commit/588221d90894ef190533d9eee87c1742cc00bad8))
* set Content-Range as content header, not request header ([417b7e6](https://github.com/encryption4all/postguard-dotnet/commit/417b7e681c29a414f08fcacaa364e9306e40eaeb))
* use gh release download to fetch pg-ffi native libraries ([1c2a544](https://github.com/encryption4all/postguard-dotnet/commit/1c2a544581588943648467893264d5b7be086c40))
* use Node.js 24 for actions and target .NET 10 in CI ([a06e56a](https://github.com/encryption4all/postguard-dotnet/commit/a06e56a32b0e2254bcd2e170b49aa399c6c200b3))
* use None items for native libs to ensure proper NuGet runtime bundling ([3203792](https://github.com/encryption4all/postguard-dotnet/commit/32037921a1900d645f23bc517d153b6a48f85d52))
* use NuGet trusted publishing instead of API key ([b8bbb9c](https://github.com/encryption4all/postguard-dotnet/commit/b8bbb9cf2c26fdd29c082cdee6adcfe32c36d31a))
* use NuGet/login action for trusted publishing OIDC flow ([0d1056f](https://github.com/encryption4all/postguard-dotnet/commit/0d1056f92c4856a92d26a95a6cf5ca26e4d586ec))
* use single-line commands in publish workflow ([f912f72](https://github.com/encryption4all/postguard-dotnet/commit/f912f727cb0260afea9211eb735edc0799d76ae1))

## [0.1.2](https://github.com/encryption4all/postguard-dotnet/compare/v0.1.1...v0.1.2) (2026-04-10)


### Bug Fixes

* set Content-Range as content header, not request header ([417b7e6](https://github.com/encryption4all/postguard-dotnet/commit/417b7e681c29a414f08fcacaa364e9306e40eaeb))
