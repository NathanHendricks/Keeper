<template>
  <div class="container-fluid px-2">
    <div class="row mb-5">
      <div
        class="col-md-12 d-flex align-items-center flex-row profile-header"
        v-if="profile"
      >
        <img
          :src="profile.picture"
          alt="profile img"
          class="rounded size ms-2 mt-3"
        />
        <div class="col-md-12">
          <h3 class="ps-2">{{ profile.name }}</h3>
          <em class="ps-2"> Vaults: {{ vaults.length }} <br /> </em>
          <em class="ps-2"> Keeps: {{ keeps.length }} </em>
        </div>
      </div>
      <div v-else>
        <div class="spinner-border text-secondary mt-5 ms-5" role="status">
          <span class="visually-hidden">Loading...</span>
          <p>loading....</p>
        </div>
      </div>
    </div>
    <div v-if="vaults.length > 0">
      <div class="mb-5">
        <!-- vaults go here in line -->
        <h4 class="ms-3">Vaults</h4>
        <button
          class="btn btn-primary ms-3"
          data-bs-toggle="modal"
          data-bs-target="#new-vault-modal"
          title="Add a Vault"
        >
          <i mdi mdi-plus f-20 />
        </button>
        <!-- component goes here -->
        <div class="row d-flex flex-row ms-1 ps-1">
          <VaultCard v-for="v in vaults" :key="v.id" :vault="v" />
        </div>
      </div>
    </div>
    <div v-else>
      <div class="ms-2 mb-5">
        <h5>No Vaults....</h5>
        <button
          class="btn btn-primary"
          data-bs-toggle="modal"
          data-bs-target="#new-vault-modal"
          title="Add a Vault"
        >
          <i mdi mdi-plus f-20 />
        </button>
      </div>
    </div>
    <div v-if="keeps.length > 0" class="con">
      <div class="mb-5">
        <!-- keeps go here with masonary -->
        <h4>Keeps</h4>
        <button
          class="btn btn-primary"
          data-bs-toggle="modal"
          data-bs-target="#new-keep-modal"
          title="Add a Keep"
        >
          <i mdi mdi-plus f-20 />
        </button>
        <!-- component goes here -->
        <KeepCard v-for="k in keeps" :key="k.id" :keep="k" />
      </div>
    </div>
    <div v-else>
      <div class="mb-5">
        <h5>No Keeps....</h5>
        <button
          class="btn btn-primary"
          data-bs-toggle="modal"
          data-bs-target="#new-keep-modal"
          title="Add a Keep"
        >
          <i mdi mdi-plus f-20 />
        </button>
      </div>
    </div>
  </div>
  <!-- Vault Modal goes here-->
  <Modal id="new-vault-modal">
    <template #modal-title>
      <h3>New Vault</h3>
    </template>
    <template #modal-body>
      <VaultForm />
    </template>
  </Modal>
  <!-- Keep Modal goes here -->
  <Modal id="new-keep-modal">
    <template #modal-title>
      <h3>New Keep</h3>
    </template>
    <template #modal-body>
      <KeepForm />
    </template>
  </Modal>
</template>


<script>
import { computed, onMounted, watchEffect } from '@vue/runtime-core'
import { profilesService } from '../services/ProfilesService'
import { vaultsService } from '../services/VaultsService'
import { keepsService } from '../services/KeepsService'
import { logger } from '../utils/Logger'
import { AppState } from '../AppState'
import { useRoute } from 'vue-router'
import Pop from '../utils/Pop'
export default {
  setup() {
    const route = useRoute()
    onMounted(async () => {
      try {
        await profilesService.getProfileById(route.params.id)
        await keepsService.getKeepsByProfileId(route.params.id)
        await vaultsService.getVaultsByProfileId(route.params.id)
      } catch (error) {
        Pop.toast(error.message, 'error')
      }
    })
    watchEffect(async () => {
      if (route.params.id) {
        try {
          await profilesService.getProfileById(route.params.id)
          await keepsService.getKeepsByProfileId(route.params.id)
          await vaultsService.getVaultsByProfileId(route.params.id)
        } catch (error) {
          Pop.toast(error.message)
          logger.log(error)
        }
      }
    })
    return {
      account: computed(() => AppState.account),
      profile: computed(() => AppState.profile),
      vaults: computed(() => AppState.vaults),
      keeps: computed(() => AppState.keeps)
    }
  }
}
</script>


<style lang="scss" scoped>
.size {
  height: 10vh;
  width: 10vw;
}
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