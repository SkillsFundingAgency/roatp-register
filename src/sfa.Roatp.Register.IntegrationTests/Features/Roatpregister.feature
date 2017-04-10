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

#10/04/2017 Temp test please remove it / update it for next csv deployment 
Scenario: Roatp Register csv file has updated information
Given I can open roatp website 
When I request for SFA Roatp csv file 
Then I should have a csv file with more than 5 Kb contents
And csv file should contain following information
| UKPRN    |
| 10001457 |
| 10011332 |
| 10058010 |
| 10037375 |
| 10007407 |
| 10005410 |
| 10001196 |
| 10042241 |
| 10004663 |
| 10004736 |
| 10024177 |
| 10030573 |
| 10005967 |
      