targetScope = 'subscription'

param location string = 'westeurope'
param resourceGroupName string = 'usermanagement'

var serviceBusQueueName = serviceBus.outputs.queueName
var serviceBusConnectionString = serviceBus.outputs.connectionString
var endpoint = storageAccount.outputs.storageAccountEndpoint

resource resourceGroup 'Microsoft.Resources/resourceGroups@2021-04-01' = {
  name: resourceGroupName
  location: location
}

module storageAccount './storageAccount.bicep' = {
  name: 'storageAccount'
  scope: resourceGroup
  params: {
    location: location
  }
}

module serviceBus './serviceBus.bicep' = {
  name: 'serviceBus'
  scope: resourceGroup
  params: {
    location: location
  }
}

module appService './appService.bicep' = {
  name: 'appService'
  scope: resourceGroup
  dependsOn: [
    serviceBus
    storageAccount
  ]
  params: {
    location: location
    serviceBusQueueName: serviceBusQueueName
    serviceBusConnectionString: serviceBusConnectionString
    staticSiteEndpoint: endpoint
  }
}

output appServiceName string = appService.outputs.appServiceName
output storageAccountName string = storageAccount.outputs.storageAccountName
