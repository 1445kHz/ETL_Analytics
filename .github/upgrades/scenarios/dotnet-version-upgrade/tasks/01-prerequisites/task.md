# 01-prerequisites: Verify SDK and baseline upgrade prerequisites

Validate that the local environment and repository configuration can build and test against net10.0 before project edits begin. This task covers .NET 10 SDK availability, global.json compatibility, and a clean baseline restore/build of the current solution so later failures can be attributed to upgrade changes rather than pre-existing environment drift.

The assessment indicates package updates and API compatibility work in multiple projects, so establishing baseline reproducibility is required before the atomic upgrade task. This task also confirms the current test runner/tooling can execute under the expected SDK path once the TFM is updated.

## Research and Validation Findings

- Target solution: `DTSXDataLoader.sln` with 3 SDK-style projects (`Core`, `Console`, `Tests`) already on modern .NET (`net8.0`), so `dotnet build` is the correct baseline build tool.
- `.NET 10` SDK presence validated with `validate_dotnet_sdk_installation(net10.0)`.
- `validate_dotnet_sdk_in_globaljson` reported no `global.json` in scope, so no SDK pinning conflict exists.
- Baseline workspace build completed successfully before upgrade changes.

**Done when**: .NET 10 SDK is validated, global.json (if present) is compatible, and the current solution restores/builds with no blocking prerequisite issues.
