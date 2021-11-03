<template>
  <div class="col-md-12 d-flex align-items-center flex-row vault-header">
    <h3 class="ps-2">{{ vault.name }}</h3>
    <em class="ps-2"> Keeps: {{ vaultkeeps.length }} </em>
  </div>
</template>

<script>
import { computed } from '@vue/reactivity'
import { AppState } from '../AppState'
import { onMounted } from '@vue/runtime-core'
import Pop from '../utils/Pop'
import { logger } from '../utils/Logger'
import { vaultsService } from '../services/VaultsService'
import { useRoute } from 'vue-router'
export default {
  setup() {
    const route = useRoute()
    onMounted(async () => {
      try {
        await vaultsService.getkeepByVaultId(route.params.id)
      } catch (error) {
        Pop.toast(error.message, 'error')
        logger.log(error)
      }
    })
    return {
      vault: computed(() => AppState.vault),
      vaultkeep: computed(() => AppState.vaultkeeps)
    }
  }
}
</script>
