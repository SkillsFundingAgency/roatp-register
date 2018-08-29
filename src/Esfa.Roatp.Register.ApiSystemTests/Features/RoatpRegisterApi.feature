@ApiSystemTestUsingStubRepo
Feature: RoatpRegisterApi
	As a user
	I would like to use roatp register api

Background: 
Given the following roatp providers are available
| Ukprn    | Name          | ProviderType | ContractedForNonLeviedEmployers | NewOrganisationWithoutFinancialTrackRecord | ParentCompanyGuarantee | StartDate   | EndDate |
| 19992101 | ABC Institute | MainProvider | False                           | True                                       | True                   | 20-Mar-2017 |         |
| 29992101 | DEF Institute | MainProvider | False                           | True                                       | True                   | 20-Mar-2017 |         |
| 39992101 | GHI Institute | MainProvider | False                           | True                                       | True                   | 20-Mar-2017 |         |

Scenario: Roatp Register API should return all providers
When I request for All providers
Then I should get All providers
And returns All Providers UKPRN field
And returns All Providers Name field
And returns All Providers NewOrganisationWithoutFinancialTrackRecord field
And returns All Providers ParentCompanyGuarantee field
And returns All Providers ProviderType field
And returns All Providers StartDate field

Scenario: Roatp Register API should return a provider
When I request for provider with Ukprn 29992101
Then I should get A provider
And returns UKPRN field
And returns Name field
And returns NewOrganisationWithoutFinancialTrackRecord field
And returns ParentCompanyGuarantee field
And returns ProviderType field
And returns StartDate field

Scenario: Roatp Register API should throw error when provider start date is in future
Given A Roatp provider with future start date
Then I should get an exception when i request for a Provider with future start date

Scenario: Roatp Register API should throw error when provider end date is in past
Given A Roatp provider with past end date
Then I should get an exception when i request for a Provider with past end date

Scenario: Roatp Register API should acknowledge existence when provider end date is in future
Given A Roatp provider with future end date
Then I should not get any exception when i request for a Provider with future end date

Scenario: Roatp Register API should throw error for provider not found
When I request for provider with Ukprn 49992101
Then I should get an exception when i request for a Provider which can not be found

Scenario: Roatp Register API should acknowledge existence of a provider
When I request for provider with Ukprn 29992101
Then I should not get any exception when i request for an existence of Provider which can be found

Scenario: Roatp Register API should throw error for provider does not exist
When I request for provider with Ukprn 49992101
Then I should get an exception when i request for an existence of Provider which can not be found