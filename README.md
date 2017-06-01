# Register of Apprenticeship Training Providers

|               |               |
| ------------- | ------------- |
|![crest](https://assets.publishing.service.gov.uk/static/images/govuk-crest-bb9e22aff7881b895c2ceb41d9340804451c474b883f09fe1b4026e76456f44b.png) |Register of apprenticeship training providers|
| Web  | https://roatp.apprenticeships.sfa.bis.gov.uk/ |
| Source  | https://github.com/SkillsFundingAgency/roatp-register  |
| Roatp Api Client | [![](https://img.shields.io/nuget/v/SFA.Roatp.Api.Client.svg)](https://www.nuget.org/packages/SFA.Roatp.Api.Client//) |
| Swagger | [![](http://online.swagger.io/validator?url=https://roatp.apprenticeships.sfa.bis.gov.uk/swagger/docs/v1)](https://roatp.apprenticeships.sfa.bis.gov.uk/swagger/docs/v1) |


The [web application](https://roatp.apprenticeships.sfa.bis.gov.uk/) provides a web interface to download Register of apprenticeship training providers in csv format.

A public API from the Educational Skills Funding Agency to provide a list of 
- Roatp Providers

## Consumers
- [Register of apprenticeship training providers](https://github.com/SkillsFundingAgency/roatp-register)

## Architecture

### Other components
- [Roatp Indexer](https://github.com/SkillsFundingAgency/roatp-indexer)

### Dependencies 
- Elasticsearch 2.3.5
- Google Analytics

### Developer Setup
- [Razor Generator Extension](https://visualstudiogallery.msdn.microsoft.com/1f6ec6ff-e89b-4c47-8e79-d2d68df894ec) for Visual Studio 2015


## Usage

### Basic
```c#
using(var client = new RoatpApiClient())
{
   var standard = client.Get(12345678);
}
```

### StructureMap
```c#
For<IRoatpClient>().Use<RoatpApiClient>();
```

