# Configure the Azure provider
terraform {
  required_providers {
    azurerm = {
      source = "hashicorp/azurerm"
      version = ">= 2.26"
    }
  }
}

provider "azurerm" {
  features {}
}

resource "azurerm_resource_group" "macrocalchttp" {
  name     = "macrocalchttp"
  location = "westus2"
}

resource "azurerm_storage_account" "macrocalchttp" {
  name                     = "macrocalchttp"
  resource_group_name      = azurerm_resource_group.macrocalchttp.name
  location                 = azurerm_resource_group.macrocalchttp.location
  account_tier             = "Standard"
  account_replication_type = "LRS"
}

resource "azurerm_app_service_plan" "macrocalchttp" {
  name                = "macrocalchttp"
  location            = azurerm_resource_group.macrocalchttp.location
  resource_group_name = azurerm_resource_group.macrocalchttp.name
  kind                = "FunctionApp"

  sku {
    tier = "Dynamic"
    size = "Y1"
  }
}

resource "azurerm_function_app" "macrocalchttp" {
  name                       = "macrocalchttp"
  location                   = azurerm_resource_group.macrocalchttp.location
  resource_group_name        = azurerm_resource_group.macrocalchttp.name
  app_service_plan_id        = azurerm_app_service_plan.macrocalchttp.id
  storage_account_name       = azurerm_storage_account.macrocalchttp.name
  storage_account_access_key = azurerm_storage_account.macrocalchttp.primary_access_key
}

resource "azurerm_application_insights" "macrocalchttp" {
  name                = "macrocalchttp"
  location            = azurerm_resource_group.macrocalchttp.location
  resource_group_name = azurerm_resource_group.macrocalchttp.name
  application_type    = "web"
}

output "instrumentation_key" {
  value = azurerm_application_insights.macrocalchttp.instrumentation_key
}

output "app_id" {
  value = azurerm_application_insights.macrocalchttp.app_id
}