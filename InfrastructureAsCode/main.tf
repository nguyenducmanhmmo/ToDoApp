terraform {
  required_providers {
    azurerm = {
      source  = "hashicorp/azurerm"
      version = "=3.75.0"
    }
    random = {
      source  = "hashicorp/random"
      version = "2.3.1"
    }
  }
  backend "azurerm" {
    resource_group_name  = "terraform-backend-rg"
    storage_account_name = "terraformbackendstactest"
    container_name       = "terraformstate"
    key                  = "terraform.tfstate"
  }
}
provider "azurerm" {
  features {}
}

resource "random_string" "todoapp" {
  length  = 6
  special = false
  upper   = false
}

resource "azurerm_resource_group" "todoapp" {
  name     = var.resource_group_name
  location = var.location

  tags = {
    Scope = "Todoapp"
  }
}

resource "azurerm_mssql_server" "todoapp" {
  name                         = "sqlserver${random_string.todoapp.result}"
  resource_group_name          = azurerm_resource_group.todoapp.name
  location                     = azurerm_resource_group.todoapp.location
  version                      = "12.0"
  administrator_login          = var.sqlserver_administrator_login
  administrator_login_password = var.sqlserver_administrator_login_password

  tags = {
    Scope = "Todoapp"
  }
}

resource "azurerm_mssql_firewall_rule" "todoapp" {
  name             = "sqlserverfwrule${random_string.todoapp.result}"
  server_id        = azurerm_mssql_server.todoapp.id
  start_ip_address = "0.0.0.0"
  end_ip_address   = "0.0.0.0"
}

resource "azurerm_mssql_database" "todoapp" {
  name      = var.sqlserver_db_name
  server_id = azurerm_mssql_server.todoapp.id
  collation = "SQL_Latin1_General_CP1_CI_AS"
  sku_name  = "Basic"
  tags = {
    Scope = "Todoapp"
  }
}

data "azurerm_client_config" "current" {}

resource "azurerm_key_vault" "todoapp" {
  name                            = var.keyvault_name
  location                        = azurerm_resource_group.todoapp.location
  resource_group_name             = azurerm_resource_group.todoapp.name
  tenant_id                       = data.azurerm_client_config.current.tenant_id
  sku_name                        = "standard"
  enabled_for_disk_encryption     = true
  enabled_for_deployment          = true
  enabled_for_template_deployment = true
}

resource "azurerm_key_vault_access_policy" "todoapp" {
  key_vault_id = azurerm_key_vault.todoapp.id
  tenant_id    = data.azurerm_client_config.current.tenant_id
  object_id    = data.azurerm_client_config.current.object_id

  key_permissions = [
    "Get",
    "List",
    "Create",
    "Delete",
  ]
  secret_permissions = [
    "Get",
    "List",
    "Set",
    "Delete",
  ]
  certificate_permissions = [
    "Get",
    "List",
    "Create",
    "Delete",
  ]
}

resource "azurerm_service_plan" "todoapp" {
  name                = "webappserviceplan${random_string.todoapp.result}"
  location            = azurerm_resource_group.todoapp.location
  resource_group_name = azurerm_resource_group.todoapp.name
  os_type             = "Windows"
  sku_name            = "F1"

  tags = {
    Scope = "Todoapp"
  }
}

resource "azurerm_windows_web_app" "todoapp_api" {
  name                = var.webapp_api_project_name
  resource_group_name = azurerm_resource_group.todoapp.name
  location            = azurerm_service_plan.todoapp.location
  service_plan_id     = azurerm_service_plan.todoapp.id

  site_config {
    always_on         = false
    app_command_line  = "dotnet TodoApp.Api.dll"
    use_32_bit_worker = true
    application_stack {
      dotnet_version = "v7.0"
    }
  }

  app_settings = {
    "WEBSITES_PORT" = "5000"
  }

  tags = {
    Scope = "Todoapp"
  }
}

resource "azurerm_windows_web_app" "todoapp" {
  name                = var.webapp_ui_project_name
  resource_group_name = azurerm_resource_group.todoapp.name
  location            = azurerm_service_plan.todoapp.location
  service_plan_id     = azurerm_service_plan.todoapp.id

  site_config {
    always_on = false
  }

  app_settings = {
    "WEBSITES_PORT"                = "80"
    "WEBSITE_NODE_DEFAULT_VERSION" = "16.13.0"
  }

  tags = {
    Scope = "Todoapp"
  }
}

resource "azurerm_key_vault_secret" "todoapp" {
  name         = "SqlConnectionString${random_string.todoapp.result}"
  value        = "Server=tcp:${azurerm_mssql_server.todoapp.fully_qualified_domain_name},1433;Initial Catalog=${azurerm_mssql_database.todoapp.name};User Id=${azurerm_mssql_server.todoapp.administrator_login};Password=${azurerm_mssql_server.todoapp.administrator_login_password};Persist Security Info=False;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"
  key_vault_id = azurerm_key_vault.todoapp.id
}

