variable "keyvault_name" {
  type        = string
}

variable "location" {
  type        = string
}

variable "resource_group_name" {
  type        = string
}

variable "tenant_id" {
  type        = string
}

variable "azurerm_key_vault_sku" {
  type        = string
}

variable "object_id" {
  type        = string
}

variable "azurerm_key_vault_access_policy_permissions" {
  type        = list
}

variable "random" {
  type        = string
}

variable "random_db_pass" {
  type        = string
}