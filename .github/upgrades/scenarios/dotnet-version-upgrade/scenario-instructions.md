# .NET Version Upgrade

## Preferences
- **Flow Mode**: Automatic
- **Target Framework**: net10.0

## Source Control
- **Source Branch**: master
- **Working Branch**: dotnet-version-upgrade-net10
- **Commit Strategy**: Single Commit at End
- **Branch Sync**: Auto (Merge)

## Upgrade Options
**Source**: .github/upgrades/scenarios/dotnet-version-upgrade/upgrade-options.md

### Strategy
- Upgrade Strategy: All-at-Once

### Project Structure
- Package Management: Per-Project (defer CPM to post-migration)

### Compatibility
- Unsupported Packages: Resolve Inline (1 incompatible package)
- Unsupported API Handling: Fix Inline

## Strategy
**Selected**: All-At-Once
**Rationale**: 3 SDK-style projects already on modern .NET (net8.0) with shallow dependencies and manageable upgrade risk, so a single coordinated upgrade pass is the best fit.

### Execution Constraints
- Upgrade all projects in one atomic pass (no tiered/phased ordering).
- Apply project TFM updates before package updates, then fix compile/API issues.
- Validate with full solution build and tests only after the atomic upgrade pass completes.
- Defer Central Package Management setup until after migration stabilization.
