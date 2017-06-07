@RoatpRegisterDeploymentTests
Feature: RoatpRegisterDeployment
	In order to avoid production bug
	As a Test Analyst
	I want to be told the Roatp Register features are working

Scenario: Roatp Register csv file should have atleast 500 Providers
Given I request for SFA Roatp csv file 
Then I should have atleast 500 Providers

Scenario: Roatp Register csv file should be in sync with Get All providers Api end point
Given I request for SFA Roatp csv file
And I request for SFA Roatp Get All providers Api end point
Then they should expose same details