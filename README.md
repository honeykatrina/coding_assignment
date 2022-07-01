# UserAccountManagement
UserAccountManagement solution is sample dedicated to creation of user accounts.
Solution consists of an .Net API and Angular SPA. 

UserAccountManagement Api contains few projects:

- UserAccountManagement.Users, UserAccountManagement.Transactions are services to manage corresponded domains
- UserAccountManagement.Shared contains some shared models and service bus services
- UserAccountManagement.Tests contains some unit tests

The project data is saved to json files to test solution easier. 

## CI/CD

The CI/CD flow is organised using Github Actions. It builds and tests an API, builds frontend application and deploys artifacts to Azure resources.

Azure infrastructure is defined with Bicep in `/deployment` folder. Created Azure resources:

- App Service to host an API
- Storage Account to host a static web site
- Service Bus to provide communication between .Net application services

## Test solution

As the solution is deployed to Azure, you can test it in the cloud. Navigate to [API Swagger](https://user-management-api-lat.azurewebsites.net/swagger/index.html) to see available API endpoints. Or navigate to [frontend application web site](https://storlatocz3ehqllw.z6.web.core.windows.net/) to test through simple client interface.

> Note: To run solution locally you need to provide Service Bus connection string and a queue name in appsettings.Development.json. 
