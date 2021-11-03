<template>
  <div class="card ps-2-0 m-2 selectable" @click="goToVaultPage(vault.id)">
    <small class="ps-1">{{ vault.name }}</small>
  </div>
</template>


<script>
import { Vault } from '../Models/Vault'
import { router } from '../router'
import { logger } from '../utils/Logger'
export default {
  props: {
    vault: {
      type: Vault,
      default: () => { return new Vault() }
    }
  },
  setup() {
    return {
      async goToVaultPage(id) {
        try {
          router.push({ name: 'Vault', params: { id: id } })
        } catch (error) {
          // Pop.toast(error.message, 'error')
          logger.log(error)
        }
      }
    }
  }
}
</script>


<style lang="scss" scoped>
.card {
  max-width: 100px;
}
</style>