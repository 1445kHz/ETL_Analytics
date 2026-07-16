# Upgrade Options — DTSXDataLoader

Assessment: 3 SDK-style net8.0 projects with 1 incompatible package and 11 binary-incompatible API issues for net10.0.

## Strategy

### Upgrade Strategy
Small modern-to-modern solution (3 projects) with manageable risk; a single coordinated pass is the default fit.

| Value | Description |
|-------|-------------|
| **All-at-Once** (selected) | Upgrade all projects together in one atomic pass; fastest for small solutions. |
| Top-Down | Upgrade apps first with temporary multi-targeting in shared libraries, then consolidate. |

## Project Structure

### Package Management
The solution has multiple projects with per-project PackageReference and no centralized package management.

| Value | Description |
|-------|-------------|
| Central Package Management (CPM) | Introduce Directory.Packages.props and centralize package versions for consistency. |
| **Per-Project (defer CPM to post-migration)** (selected) | Keep package versions in each project during migration and revisit CPM after stabilization. |

## Compatibility

### Unsupported Packages
Assessment reports one incompatible package for the target framework, requiring explicit resolution.

| Value | Description |
|-------|-------------|
| **Resolve Inline** (selected) | Research and resolve incompatible package usage within the same upgrade task with no deferred stubs. |
| Defer Resolution | Keep builds moving with temporary stubs and create follow-up resolution tasks. |
| Compatibility Mode | Keep compatibility references as a last resort with potential runtime risk. |

### Unsupported API Handling
Assessment reports binary-incompatible API changes that must be addressed during upgrade execution.

| Value | Description |
|-------|-------------|
| **Fix Inline** (selected) | Resolve API changes directly in the task, including complex changes where needed. |
| Defer Complex Changes | Apply simple fixes now and defer complex replacements with temporary stubs. |
