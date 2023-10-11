variable "resource_group_name" {
  type        = string
}

variable "location" {
  type        = string
}

variable "sqlserver_version" {
  type        = string
}

variable "sqlserver_administrator_login" {
  type        = string
}

variable "db_password" {
  type        = string
}

variable "random" {
  type        = string
}

variable "azurerm_mssql_firewall_rule_start_ip_address" {
  type        = string
}

variable "azurerm_mssql_firewall_rule_end_ip_address" {
  type        = string
}

variable "sqlserver_db_name" {
  type        = string
}

variable "azurerm_mssql_database_collation" {
  type        = string
}

variable "azurerm_mssql_database_sku" {
  type        = string
}