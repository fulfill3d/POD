#!/bin/bash

# This script automates the process of creating multiple Azure Function Apps, 
# assigning them managed identities, and giving them access to Azure App Configuration 
# and Key Vault. It is highly inefficient and time-consuming to manually click through 
# the Azure portal for each individual app. 
# 
# If you were to use the Azure UI, you would need to:
#   - Click through the portal to create each Function App one-by-one.
#   - Navigate through menus to enable the managed identity.
#   - Assign permissions to Azure App Configuration and Key Vault, again repeating 
#     these steps for each Function App.
# 
# Manually creating dozens of Function Apps and configuring permissions for each
# can lead to mistakes and is unnecessarily laborious. This script simplifies 
# the entire process by automating the creation and configuration of all Function Apps 
# in one go. Run this script once and save yourself from repetitive, error-prone work.

# Define your resource group, location, and storage account
RESOURCE_GROUP="<resource-group-name>"
LOCATION="<location-name>"
STORAGE_ACCOUNT="<storage-account-name>"
APP_CONFIG_RESOURCE_ID="/subscriptions/<your-subscription-id>/resourceGroups/<your-resource-group>/providers/Microsoft.AppConfiguration/configurationStores/<app-config-name>"
KEY_VAULT_RESOURCE_ID="/subscriptions/<your-subscription-id>/resourceGroups/<your-resource-group>/providers/Microsoft.KeyVault/vaults/<key-vault-name>"

# List of function app names
function_apps=(
  "function-app-name-1"
  "function-app-name-2"
  "function-app-name-3"
  # Add more function apps as needed
)

# Loop through the list of function apps and create each one
for function_app in "${function_apps[@]}"
do
  echo "Creating Function App: $function_app"

  # Create the function app
  az functionapp create \
    --resource-group $RESOURCE_GROUP \
    --consumption-plan-location $LOCATION \
    --name $function_app \
    --storage-account $STORAGE_ACCOUNT \
    --disable-app-insights true \
    --functions-version 4 \
    --runtime dotnet-isolated \
    --runtime-version 8.0
  
  echo "Assigning Managed Identity to Function App: $function_app"

  # Assign system-assigned managed identity
  az functionapp identity assign \
    --name $function_app \
    --resource-group $RESOURCE_GROUP

  # Get the principal ID (system identity) of the function app
  principalId=$(az functionapp identity show \
    --name $function_app \
    --resource-group $RESOURCE_GROUP \
    --query principalId --output tsv)

  echo "Principal ID for $function_app is $principalId"

  echo "Assigning roles for Azure App Configuration and Key Vault to Function App: $function_app"

  # Assign "App Configuration Data Reader" role to the function app's managed identity
  az role assignment create \
    --assignee $principalId \
    --role "App Configuration Data Reader" \
    --scope $APP_CONFIG_RESOURCE_ID

  # Assign "Key Vault Secrets User" role to the function app's managed identity
  az role assignment create \
    --assignee $principalId \
    --role "Key Vault Secrets User" \
    --scope $KEY_VAULT_RESOURCE_ID

  echo "Completed setup for $function_app"
done

echo "All Function Apps created and identities assigned!"
