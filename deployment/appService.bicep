param appServicePlanName string = 'user-management-plan-${take(uniqueString(resourceGroup().id), 3)}'
param appServiceName string = 'user-management-api-${take(uniqueString(resourceGroup().id), 3)}'
param location string = resourceGroup().location

resource appServicePlan 'Microsoft.Web/serverFarms@2021-03-01' = {
  name: appServicePlanName
  location: location
  sku: {
    name: 'F1'
  }
}

resource appServiceApp 'Microsoft.Web/sites@2021-03-01' = {
  name: appServiceName
  location: location
  properties: {
    serverFarmId: appServicePlan.id
    httpsOnly: true
  }
}

output appServiceName string = appServiceName
