targetScope = 'subscription'

param location string = 'westeurope'
param resourceGroupName string = 'usermanagement'

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

module appService './appService.bicep' = {
  name: 'appService'
  scope: resourceGroup
  params: {
    location: location
  }
}

output appServiceName string = appService.outputs.appServiceName
output storageAccountName string = storageAccount.outputs.storageAccountName
