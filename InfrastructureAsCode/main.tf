resource "random_string" "todoapp" {
  length  = 6
  special = false
  upper   = false
}

resource "random_string" "db_password" {
  length  = 11
  special = true
  upper   = false
}

module "resource-group" {
  source              = "./modules/resource-group"
  resource_group_name = var.resource_group_name
  location            = var.location
}

module "database" {
  source                                       = "./modules/database"
  depends_on                                   = [module.resource-group]
  resource_group_name                          = var.resource_group_name
  location                                     = var.location
  sqlserver_version                            = var.sqlserver_version
  sqlserver_administrator_login                = var.sqlserver_administrator_login
  db_password                                  = random_string.db_password.result
  azurerm_mssql_firewall_rule_start_ip_address = var.azurerm_mssql_firewall_rule_start_ip_address
  azurerm_mssql_firewall_rule_end_ip_address   = var.azurerm_mssql_firewall_rule_end_ip_address
  random                                       = random_string.todoapp.result
  sqlserver_db_name                            = var.sqlserver_db_name
  azurerm_mssql_database_collation             = var.azurerm_mssql_database_collation
  azurerm_mssql_database_sku                   = var.azurerm_mssql_database_sku
}

module "web-app" {
  source                       = "./modules/web-app"
  depends_on                   = [module.resource-group]
  resource_group_name          = var.resource_group_name
  azurerm_service_plan_os_type = var.azurerm_service_plan_os_type
  azurerm_service_plan_sku     = var.azurerm_service_plan_sku
  random                       = random_string.todoapp.result
  webapp_api_project_name      = var.webapp_api_project_name
  location                     = var.location
  dotnet_version               = var.dotnet_version
  webapp_ui_project_name       = var.webapp_ui_project_name
  node_version                 = var.node_version
}

data "azurerm_client_config" "current" {}

module "key-vaults" {
  source                                      = "./modules/key-vaults"
  depends_on                                  = [module.resource-group]
  keyvault_name                               = var.keyvault_name
  resource_group_name                         = var.resource_group_name
  location                                    = var.location
  tenant_id                                   = data.azurerm_client_config.current.tenant_id
  object_id                                   = data.azurerm_client_config.current.object_id
  azurerm_key_vault_access_policy_permissions = var.azurerm_key_vault_access_policy_permissions
  azurerm_key_vault_sku                       = var.azurerm_key_vault_sku
  random                                      = random_string.todoapp.result
  random_db_pass                              = random_string.db_password.result
}

