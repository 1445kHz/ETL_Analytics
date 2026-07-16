# 01-prerequisites: Verify SDK and baseline upgrade prerequisites

Validate that the local environment and repository configuration can build and test against net10.0 before project edits begin. This task covers .NET 10 SDK availability, global.json compatibility, and a clean baseline restore/build of the current solution so later failures can be attributed to upgrade changes rather than pre-existing environment drift.

The assessment indicates package updates and API compatibility work in multiple projects, so establishing baseline reproducibility is required before the atomic upgrade task. This task also confirms the current test runner/tooling can execute under the expected SDK path once the TFM is updated.

**Done when**: .NET 10 SDK is validated, global.json (if present) is compatible, and the current solution restores/builds with no blocking prerequisite issues.
