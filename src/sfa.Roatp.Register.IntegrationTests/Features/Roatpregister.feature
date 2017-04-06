Feature: Roatpregister
	
Scenario: Can download Roatp Register csv file
Given I can open roatp website 
When I request for SFA Roatp csv file 
Then I should have a csv file with contents.