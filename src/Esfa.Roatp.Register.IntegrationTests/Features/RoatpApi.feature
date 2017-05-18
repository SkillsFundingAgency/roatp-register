
Feature: RoatpApi
	As a user
	I would like to use roatp api to collect informaion about roatp register


Scenario: Roatp Register csv file should be in sync with Get All providers Api end point
Given I can open roatp website 
When I request for SFA Roatp csv file
And I request for SFA Roatp Get All providers Api end point
Then they should expose same details