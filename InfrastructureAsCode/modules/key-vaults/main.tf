resource "azurerm_key_vault" "todoapp" {
  name                            = var.keyvault_name
  location                        = var.location
  resource_group_name             = var.resource_group_name
  tenant_id                       = var.tenant_id
  sku_name                        = var.azurerm_key_vault_sku
  enabled_for_disk_encryption     = true
  enabled_for_deployment          = true
  enabled_for_template_deployment = true
}

resource "azurerm_key_vault_access_policy" "todoapp" {
  key_vault_id          = azurerm_key_vault.todoapp.id
  tenant_id             = var.tenant_id
  object_id             = var.object_id
  secret_permissions    = var.azurerm_key_vault_access_policy_permissions
}

resource "azurerm_key_vault_secret" "todoapp" {
  name         = "DbPassword${var.random}"
  value        = var.random_db_pass
  key_vault_id = azurerm_key_vault.todoapp.id
}