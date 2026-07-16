# 02-upgrade-all-projects: Upgrade all projects to net10.0 and resolve compatibility issues

Perform a single coordinated upgrade across DTSXDataLoader.Core, DTSXDataLoader.Console, and DTSXDataLoader.Tests. Update TargetFramework values to net10.0, apply recommended package version updates, replace or resolve the incompatible package path, and implement required source changes for the flagged binary-incompatible and behavioral API differences.

Scope includes project file updates, package reference alignment, and code adjustments in the impacted files (notably configuration binder and logging extension usage). Because strategy is All-at-Once, this task executes as one atomic pass and is followed by full validation once compile issues are resolved.

## Scope Inventory

### Projects affected
- `DTSXDataLoader.Core/DTSXDataLoader.Core.csproj`
- `DTSXDataLoader.Console/DTSXDataLoader.Console.csproj`
- `DTSXDataLoader.Tests/DTSXDataLoader.Tests.csproj`

### Distinct concerns
- Retarget all projects from `net8.0` to `net10.0`.
- Upgrade package references in Console/Core to the assessment-recommended `10.0.10` set for Microsoft.Extensions packages and `Newtonsoft.Json` to `13.0.4`.
- Upgrade test package `xunit` from deprecated `2.5.3` to recommended `2.9.3`.
- Resolve `Api.0001` binder call compatibility flags in:
  - `DTSXDataLoader.Console/Program.cs`
  - `DTSXDataLoader.Core/Service/EtlDatabaseService.cs`
- Verify behavioral change warning around `AddConsole()` remains correct under updated package versions.

### Assessment signals used
- Console project: 11 issues (TFM change + 8 package upgrades + 1 API binary incompatibility + 1 behavioral change).
- Core project: 19 issues (TFM change + 8 package upgrades + 10 API binary incompatibilities tied to `ConfigurationBinder.GetValue<T>` usage).
- Tests project: 2 issues (TFM change + deprecated `xunit`).

### Build approach
- All projects are SDK-style and modern .NET, so `dotnet build` / `dotnet test` are the primary validation tools for this task.

**Done when**: All projects target net10.0, recommended package updates are applied, incompatible API/package issues are resolved, solution build succeeds warning-free, and tests pass.
