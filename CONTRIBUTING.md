# Contributing

Thanks for your interest in improving eMudhraRDSA.

## Scope

Contributions should target the library's own code in **`RDSA/`**. The
`eSign/` (iText 5), `BouncyCastle/`, and `System/util/` directories are vendored
third-party source — please do not refactor them for style. Only touch them to
fix a concrete integration bug, and keep changes minimal and clearly noted.

## Licensing of contributions

By submitting a contribution you agree that:

- Your contribution to `RDSA/` is licensed under both the **MIT** license
  ([`LICENSE-MIT`](LICENSE-MIT)) and, as part of the combined work, the
  **AGPL-3.0** ([`LICENSE`](LICENSE)).
- You have the right to submit the code under these terms.

Do not add new third-party dependencies without recording them in
[`THIRD-PARTY-NOTICES.md`](THIRD-PARTY-NOTICES.md) and confirming their license
is compatible with AGPL-3.0.

## Building

```powershell
msbuild eMudhraRDSA.sln /p:Configuration=Release
```

Target framework is .NET Framework 4.8 (Visual Studio 2022 / MSBuild). New
source files added under a vendored directory must also be registered in
`eMudhraRDSA.csproj` with a `<Compile Include="..." />` entry.

## Guidelines

- Keep the public API (`RemoteSigning`, `RDSAInput`, `RDSAInputBuilder`,
  `ServiceReturn`, `ReturnDocument`, the appearance types and enums) backward
  compatible where possible; call out breaking changes explicitly.
- Preserve the existing error-handling contract: `RemoteSigning.Sign` returns
  errors via `ServiceReturn` / `ReturnDocument` rather than throwing.
- Never commit secrets, strong-name keys (`*.snk`), or signing material.
- Verify the solution builds in `Release` before opening a pull request.
