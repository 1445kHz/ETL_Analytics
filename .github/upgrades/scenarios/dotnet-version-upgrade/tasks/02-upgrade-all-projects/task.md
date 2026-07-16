# 02-upgrade-all-projects: Upgrade all projects to net10.0 and resolve compatibility issues

Perform a single coordinated upgrade across DTSXDataLoader.Core, DTSXDataLoader.Console, and DTSXDataLoader.Tests. Update TargetFramework values to net10.0, apply recommended package version updates, replace or resolve the incompatible package path, and implement required source changes for the flagged binary-incompatible and behavioral API differences.

Scope includes project file updates, package reference alignment, and code adjustments in the impacted files (notably configuration binder and logging extension usage). Because strategy is All-at-Once, this task executes as one atomic pass and is followed by full validation once compile issues are resolved.

**Done when**: All projects target net10.0, recommended package updates are applied, incompatible API/package issues are resolved, solution build succeeds warning-free, and tests pass.
