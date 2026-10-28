# Contributing to Rif Venezuela
 
Thanks for your interest in contributing! This project is a .NET library for handling Venezuelan tax identification numbers (RIF, *Registro Único de Información Fiscal*), distributed as the `RifVenezuela` NuGet package. Bug reports, test cases, documentation fixes, and code are all welcome.
 
*¿Prefieres escribir en español? Los issues y pull requests en español son igualmente bienvenidos.*
 
## Table of contents
 
- [Ways to contribute](#ways-to-contribute)
- [Ground rules](#ground-rules)
- [Getting started](#getting-started)
- [Making changes](#making-changes)
- [Coding guidelines](#coding-guidelines)
- [Testing](#testing)
- [Commit messages](#commit-messages)
- [Submitting a pull request](#submitting-a-pull-request)
- [Reporting bugs and requesting features](#reporting-bugs-and-requesting-features)
- [Security issues](#security-issues)
- [Versioning and releases](#versioning-and-releases)
- [License](#license)

## Ways to contribute
 
- **Report a bug**, such as a RIF that is validated or formatted incorrectly.
- **Add test cases** for edge cases, formats, or RIF types the library doesn't cover yet.
- **Improve the docs.** The README explains how to install and use the package; contributions that make those instructions clearer are welcome.
- **Fix an open issue.** Check the [issue tracker](https://github.com/wilmerferreira/rif-venezuela/issues) and comment on the one you'd like to take so work isn't duplicated.
- **Propose a feature.** Open an issue first so we can agree on the approach before you invest time.

## Ground rules
 
- Be respectful and constructive. Assume good faith, give feedback on the code and not the person, and remember that contributors have different backgrounds and levels of experience.
- Keep pull requests focused. One logical change per PR is much easier to review than a bundle of unrelated ones.
- **Never include real RIFs in issues, tests, or examples.** A RIF can identify a real person or company. Use synthetic values that are clearly made up, or well-known public examples that are documented as such.
- When a change affects how a RIF is parsed, validated, or formatted, **link to an authoritative source** (for example SENIAT documentation or official legislation) in the issue or PR description. "It works this way in my system" isn't enough for behaviour changes.

## Getting started
 
### Prerequisites
 
- The [.NET 10 SDK](https://dotnet.microsoft.com/download/dotnet/10.0), required by the test project. The library itself targets .NET Standard 2.0.
- Git
- Any editor or IDE you like (Visual Studio, JetBrains Rider, VS Code with C# Dev Kit, etc.)

### Setup
 
1. **Fork** the repository on GitHub.
2. **Clone** your fork:

   ```sh
   git clone https://github.com/<your-username>/rif-venezuela.git
   cd rif-venezuela
   ```
 
3. **Add the upstream remote** so you can stay in sync:

   ```sh
   git remote add upstream https://github.com/wilmerferreira/rif-venezuela.git
   ```
 
4. **Restore, build, and test** to confirm everything works before you change anything:

   ```sh
   dotnet restore rif-venezuela.slnx
   dotnet build rif-venezuela.slnx --configuration Release --no-restore
   dotnet test RifVenezuela.Tests/RifVenezuela.Tests.csproj --configuration Release --no-restore
   ```

   To build or test an individual project, use:

   ```sh
   dotnet build RifVenezuela/RifVenezuela.csproj
   dotnet test RifVenezuela.Tests/RifVenezuela.Tests.csproj
   ```
 
### Repository layout
 
| Path                  | Purpose                   |
| --------------------- | ------------------------- |
| `RifVenezuela/`       | The library source        |
| `RifVenezuela.Tests/` | Automated tests           |
| `.github/workflows/`  | CI and release workflows  |
| `rif-venezuela.slnx`  | Solution file             |
 
## Making changes
 
1. Sync with upstream and create a topic branch from `main`:

   ```sh
   git fetch upstream
   git checkout -b my-change upstream/main
   ```
 
> Use a short, descriptive branch name such as `fix/legal-entity-check-digit` or `docs/readme-usage`.
 
2. Make your changes, adding or updating tests as you go.
3. Run the full build and test suite locally (see [Testing](#testing)).
4. Push your branch to your fork and open a pull request against `main`.

## Coding guidelines
 
- Follow standard [.NET coding conventions](https://learn.microsoft.com/dotnet/csharp/fundamentals/coding-style/coding-conventions) and respect any `.editorconfig` in the repo.
- Keep the public API small, intentional, and well named. Anything `public` becomes a compatibility commitment once the package is published.
- Add XML documentation comments to all public types and members.
- Prefer immutable types and pure functions for parsing and validation logic.
- Avoid adding third-party dependencies. If you believe one is justified, discuss it in an issue first, since this library should stay lightweight.
- Don't commit generated files, build output, or secrets (`bin/`, `obj/`, user settings, API keys, and the like).

## Testing
 
All behaviour changes and bug fixes need tests.
 
```sh
dotnet test
```
 
When writing tests:
 
- Cover both **valid** and **invalid** inputs, including boundary cases (empty or whitespace input, wrong length, lowercase letters, missing or extra separators, non-numeric characters, invalid check digits).
- For a bug fix, add a test that **fails without your fix** and passes with it.
- Use clearly synthetic data (see [Ground rules](#ground-rules)).
- Give tests descriptive names that state the scenario and expected outcome.
Pull requests should not reduce test coverage, and CI must be green before merging.
 
## Commit messages
 
Write clear commit messages that explain *what* changed and *why*:
 
- Use the imperative mood in the subject line ("Fix check digit calculation", not "Fixed" or "Fixes").
- Keep the subject line to roughly 72 characters.
- Reference related issues in the body, e.g. `Closes #12`.

If the project adopts [Conventional Commits](https://www.conventionalcommits.org/) for automated versioning (`feat:`, `fix:`, `docs:`, `chore:`, and so on), this section will be updated and you'll be asked to follow it.
 
## Submitting a pull request
 
Before opening your PR, check that:
 
- [ ] The solution builds without warnings you introduced
- [ ] `dotnet test` passes locally
- [ ] New or changed behaviour is covered by tests
- [ ] Public APIs have XML documentation
- [ ] The README or other docs are updated if usage changed
- [ ] The PR is focused on a single change

In the PR description, please include:
 
- **What** the change does and **why** it's needed
- A link to the related issue (if any)
- Sources for any rule or format changes
- Any breaking changes or trade-offs the reviewer should know about

### Review process
 
- A maintainer will review your PR as soon as they can. This is a volunteer-run project, so please be patient.
- You may be asked to make changes. Push follow-up commits to the same branch and the PR will update automatically.
- Once approved and CI passes, a maintainer will merge it. PRs may be squash-merged to keep history tidy.

## Reporting bugs and requesting features
 
Please [open an issue](https://github.com/wilmerferreira/rif-venezuela/issues/new) and search existing issues first to avoid duplicates.
 
**For bug reports, include:**
 
- What you did, what you expected, and what actually happened
- A minimal code snippet or failing test that reproduces the problem
- The input that triggers it (**synthetic data only**)
- Your .NET SDK/runtime version and operating system

**For feature requests, include:**
 
- The problem you're trying to solve (not only the solution you have in mind)
- Example usage showing how you'd like the API to look
- Any relevant references to official RIF specifications

## Security issues
 
If you find a security vulnerability, **please don't open a public issue.** Use GitHub's [private vulnerability reporting](https://github.com/wilmerferreira/rif-venezuela/security) (the *Security* tab of the repository) to report it confidentially. You'll get a response as soon as a maintainer is able to look into it.
 
## Versioning and releases
 
Once the package is published, the project will follow [Semantic Versioning](https://semver.org/):
 
- **MAJOR**: breaking public API changes
- **MINOR**: backwards-compatible new functionality
- **PATCH**: backwards-compatible bug fixes

Releases are cut by the maintainer through the GitHub Actions workflows and published to GitHub Packages and NuGet.org. Contributors don't need to bump version numbers in their PRs unless a maintainer asks.
 
## License
 
By contributing, you agree that your contributions will be licensed under the project's [MIT License](LICENSE).
