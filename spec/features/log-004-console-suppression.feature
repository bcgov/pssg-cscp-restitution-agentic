Feature: Production browser console suppression covers error/debug/warn
  As a security reviewer
  I want production builds to suppress console.error, console.debug, and console.warn as well as console.log
  So that diagnostic output does not reach end-user browser consoles

  @R-33.1 @tier:low
  Scenario: Production bootstrap suppresses console log error debug and warn
    Given the ClientApp is running in production mode
    When the application bootstrap applies console suppression
    Then console.log, console.error, console.debug, and console.warn are no-op functions
