Feature: Remove EOL .NET Core meta-package references
  As a dependency reviewer
  I want obsolete Microsoft.NETCore.App and Jit PackageReferences gone
  So that the net10.0 project file does not advertise EOL packages

  @R-10.1 @tier:medium
  Scenario: EOL PackageReferences are absent
    Given restitution-app.csproj currently references Microsoft.NETCore.App 2.2.8 and Microsoft.NETCore.Jit 2.0.8
    When the change is merged
    Then those PackageReference elements are removed

  @R-10.2
  Scenario: The API project still builds on net10.0
    Given the PackageReferences were removed
    When `dotnet build` runs on the API project
    Then the build succeeds targeting net10.0
