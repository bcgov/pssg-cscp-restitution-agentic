Feature: Align container base images with net10.0 and pin digests
  As a platform reviewer
  I want the deploy Dockerfile path to match net10 and use immutable base refs
  So that we do not ship net10 binaries on a net8 runtime or float on moving tags

  @R-12.1 @tier:medium
  Scenario: Misleading net8 OpenShift Dockerfile is removed or superseded
    Given openshift/Dockerfile.ubi8.net8_customized currently uses a .NET 8 runtime base
    When the change is merged
    Then that file is deleted or clearly marked as unused/superseded
    And CD continues to build the API from the net10 Dockerfile under restitution-app/

  @R-12.2
  Scenario: Active API Dockerfile pins .NET 10 base images by digest
    Given the active API Dockerfile uses aspnet/sdk 10 images
    When the change is merged
    Then runtime and SDK FROM lines include digest pins (or equivalent immutable refs)
    And the images remain .NET 10 family
