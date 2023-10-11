variable "location" {
  description = "Location of the resources"
  type        = string
  sensitive   = false
  nullable    = false
}

variable "resource_group_name" {
  description = "Name of the resource group"
  type        = string
  sensitive   = false
}

variable "sqlserver_name" {
  description = "Username of the Azure SQL Server"
  type        = string
  sensitive   = true
  nullable    = false
}

variable "sqlserver_version" {
  type      = string
  sensitive = true
  nullable  = false
}

variable "azurerm_mssql_firewall_rule_start_ip_address" {
  type      = string
  sensitive = true
  nullable  = false
}

variable "azurerm_mssql_firewall_rule_end_ip_address" {
  type      = string
  sensitive = true
  nullable  = false
}

variable "azurerm_mssql_database_collation" {
  type      = string
  sensitive = true
  nullable  = false
}

variable "azurerm_mssql_database_sku" {
  type     = string
  nullable = false
}

variable "azurerm_key_vault_sku" {
  type     = string
  nullable = false
}

variable "azurerm_key_vault_access_policy_permissions" {
  type     = list(any)
  nullable = false
}

variable "azurerm_service_plan_sku" {
  type     = string
  nullable = false
}

variable "azurerm_service_plan_os_type" {
  type     = string
  nullable = false
}

variable "dotnet_version" {
  type     = string
  nullable = false
}

variable "node_version" {
  type     = string
  nullable = false
}

variable "sqlserver_db_name" {
  description = "Name of the Azure SQL Database"
  type        = string
  sensitive   = true
  nullable    = false
}

variable "sqlserver_administrator_login" {
  description = "Username of the Azure SQL Instance"
  type        = string
  sensitive   = true
  nullable    = false
}

variable "keyvault_name" {
  description = "Name of the Azure Key Vault"
  type        = string
}

variable "webapp_api_project_name" {
  description = "Name of the .NET Api Project that has to be run"
  type        = string
  nullable    = false
}

variable "webapp_ui_project_name" {
  description = "Name of the Angular Project that has to be run"
  type        = string
  nullable    = false
}

