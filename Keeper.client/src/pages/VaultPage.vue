<template>
  <div class="container-fluid">
    <div class="row mt-5" v-if="vault">
      <div class="col-10 d-flex align-items-start flex-column">
        <h3 class="ps-2">{{ vault.name }}</h3>
        <em class="ps-2"> Keeps: {{ vaultkeeps.length }} </em>
      </div>
      <div class="col-2 d-flex justify-content-end align-items-start">
        <button class="btn btn-outline-info" title="Delete this Vault">
          <small>Delete Vault</small>
        </button>
      </div>
    </div>

    <div class="row con">
      <VautlKeepCard v-for="vk in vaultkeeps" :key="vk.id" :vaultkeep="vk" />
    </div>
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
        await vaultsService.getVaultById(route.params.id)
        await vaultsService.getKeepByVaultId(route.params.id)
      } catch (error) {
        Pop.toast(error.message, 'error')
        logger.log(error)
      }
    })
    return {
      vault: computed(() => AppState.vault),
      vaultkeeps: computed(() => AppState.vaultkeeps)
    }
  }
}
</script>

<style lang="scss" scoped>
img {
  width: 100%;
  margin-bottom: 1em;
}
.con {
  padding: 1em;
  column-count: 4;
}
@media (max-width: 600px) {
  .container {
    column-count: 2;
  }
}
</style>