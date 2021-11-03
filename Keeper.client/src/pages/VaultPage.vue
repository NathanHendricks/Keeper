<template>
  <div class="container-fluid">
    <div class="row mt-5" v-if="vault">
      <div class="col-10 d-flex align-items-start flex-column">
        <h3 class="ps-2">{{ vault.name }}</h3>
        <em class="ps-2"> Keeps: {{ keeps.length }} </em>
      </div>
      <div class="col-2 d-flex justify-content-end align-items-start">
        <button
          class="btn btn-outline-info"
          title="Delete this Vault"
          v-if="account.id == vault.creatorId"
          @click="deleteVault(vault.id)"
        >
          <small>Delete Vault</small>
        </button>
      </div>
    </div>

    <div class="row con">
      <VautlKeepCard v-for="k in keeps" :key="k.id" :keep="k" />
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
      account: computed(() => AppState.account),
      vault: computed(() => AppState.vault),
      keeps: computed(() => AppState.keeps),

      async deleteVault(vaultId) {
        try {
          const yes = await Pop.confirm('Are you sure <b>you</b> want to remove this <em>Keep</em>?')
          if (!yes) { return }
          await vaultsService.removeVault(vaultId)
          Pop.toast('Vault has been removed', 'success')
        } catch (error) {
          Pop.toast(error.message)
          logger.log(error)
        }
      }
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