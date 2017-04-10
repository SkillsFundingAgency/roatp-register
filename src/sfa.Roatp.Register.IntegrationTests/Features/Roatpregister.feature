Feature: Roatpregister
	As a user
	I would like to download a roatp register in csv format

Scenario: Can download Roatp Register csv file
Given I can open roatp website 
When I request for SFA Roatp csv file 
Then I should have a csv file with more than 5 Kb contents

#Ignored the scenario as the web request are timing out causing the test to fail
#TODO : implement asyc request and responce.
@Ignore
Scenario: All link on Roatp Register Page should be accessible
Given I can open roatp website 
Then All links should be accessible