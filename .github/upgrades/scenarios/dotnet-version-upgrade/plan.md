# .NET Version Upgrade Plan

## Overview

**Target**: Upgrade DTSXDataLoader solution from .NET 8 to .NET 10 (net10.0).
**Scope**: 3 SDK-style projects, ~2.6k LOC, shallow dependency graph (Core → Console → Tests).

## Tasks

### Selected Strategy
**All-At-Once** — All projects upgraded simultaneously in a single operation.
**Rationale**: 3 projects, all on .NET 8, SDK-style, modern-to-modern upgrade with manageable package/API changes.

### 01-prerequisites: Verify SDK and baseline upgrade prerequisites

Validate that the local environment and repository configuration can build and test against net10.0 before project edits begin. This task covers .NET 10 SDK availability, global.json compatibility, and a clean baseline restore/build of the current solution so later failures can be attributed to upgrade changes rather than pre-existing environment drift.

The assessment indicates package updates and API compatibility work in multiple projects, so establishing baseline reproducibility is required before the atomic upgrade task. This task also confirms the current test runner/tooling can execute under the expected SDK path once the TFM is updated.

**Done when**: .NET 10 SDK is validated, global.json (if present) is compatible, and the current solution restores/builds with no blocking prerequisite issues.

---

### 02-upgrade-all-projects: Upgrade all projects to net10.0 and resolve compatibility issues

Perform a single coordinated upgrade across DTSXDataLoader.Core, DTSXDataLoader.Console, and DTSXDataLoader.Tests. Update TargetFramework values to net10.0, apply recommended package version updates, replace or resolve the incompatible package path, and implement required source changes for the flagged binary-incompatible and behavioral API differences.

Scope includes project file updates, package reference alignment, and code adjustments in the impacted files (notably configuration binder and logging extension usage). Because strategy is All-at-Once, this task executes as one atomic pass and is followed by full validation once compile issues are resolved.

**Done when**: All projects target net10.0, recommended package updates are applied, incompatible API/package issues are resolved, solution build succeeds warning-free, and tests pass.

---
