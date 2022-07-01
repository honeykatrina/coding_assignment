param location string = resourceGroup().location
param serviceBusNamespaceName string = 'user-management-queue-${take(uniqueString(resourceGroup().id), 3)}'
param skuName string = 'Basic'

var queueName = 'transactionRequests'

resource serviceBusNamespace 'Microsoft.ServiceBus/namespaces@2022-01-01-preview' = {
  name: serviceBusNamespaceName
  location: location
  sku: {
    name: skuName
  }
}

resource serviceBusQueue 'Microsoft.ServiceBus/namespaces/queues@2022-01-01-preview' = {
  name: queueName
  parent: serviceBusNamespace
  properties: {
    deadLetteringOnMessageExpiration: true
    maxDeliveryCount: 2
  }
}

resource serviceBusQueueAuthorizationRule 'Microsoft.ServiceBus/namespaces/queues/authorizationRules@2022-01-01-preview' = {
  name: '${queueName}Management'
  parent: serviceBusQueue
  properties: {
    rights: [
      'Listen'
      'Manage'
      'Send'
    ]
  }
}

var serviceBusConnectionString = listkeys(serviceBusQueueAuthorizationRule.id, serviceBusQueueAuthorizationRule.apiVersion).primaryConnectionString

output queueName string = queueName
output connectionString string = serviceBusConnectionString
