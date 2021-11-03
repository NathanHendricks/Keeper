<template>
  <div class="card p-0 my-2 selectable" @click="goToVaultPage(vault.id)">
    <small class="ps-1">{{ vault.name }}</small>
  </div>
</template>


<script>
import { Vault } from '../Models/Vault'
import { router } from '../router'
import { logger } from '../utils/Logger'
import Pop from '../utils/Pop'
export default {
  props: {
    vault: {
      type: Vault,
      default: () => { return new Vault() }
    }
  },
  setup() {
    return {
      async goToVaultsPage(id) {
        try {
          router.push({ name: 'Vault', params: { id: id } })
        } catch (error) {
          Pop.toast(error.message, 'error')
          logger.log(error)
        }
      }
    }
  }
}
</script>


<style lang="scss" scoped>
</style>