# Changelog

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
