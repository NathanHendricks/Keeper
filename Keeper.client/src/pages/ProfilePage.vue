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
        <div class="d-flex">
          <h4 class="ms-3">Vaults:</h4>
          <button
            class="btn btn-outline-primary ms-3 p-1 rounded"
            data-bs-toggle="modal"
            data-bs-target="#new-vault-modal"
            v-if="account.id == profile.id"
            title="Add a Vault"
          >
            <i class="mdi mdi-plus f-10" />
          </button>
        </div>
        <!-- component goes here -->
        <div class="row d-flex flex-row ms-1 px-1">
          <VaultCard v-for="v in vaults" :key="v.id" :vault="v" />
        </div>
      </div>
    </div>
    <div v-else>
      <div class="ms-2 mb-5">
        <h5>No Vaults....</h5>
        <button
          class="btn btn-outline-primary ms-1 p-1 rounded"
          data-bs-toggle="modal"
          data-bs-target="#new-vault-modal"
          v-if="account.id == profile.id"
          title="Add a Vault"
        >
          <i class="mdi mdi-plus f-10" />
        </button>
      </div>
    </div>
    <div v-if="keeps.length > 0">
      <div class="mb-5 ms-3">
        <!-- keeps go here with masonary -->
        <div class="d-flex">
          <h4>Keeps:</h4>
          <button
            class="btn btn-outline-primary ms-2 p-1 rounded"
            data-bs-toggle="modal"
            data-bs-target="#new-keep-modal"
            v-if="account.id == profile.id"
            title="Add a Keep"
          >
            <i class="mdi mdi-plus f-10" />
          </button>
        </div>
        <!-- component goes here -->
        <div class="con px-1">
          <KeepCard v-for="k in keeps" :key="k.id" :keep="k" />
        </div>
      </div>
    </div>
    <div v-else>
      <div class="mb-5">
        <h5>No Keeps....</h5>
        <button
          class="btn btn-outline-primary ms-2 p-1 rounded"
          data-bs-toggle="modal"
          data-bs-target="#new-keep-modal"
          v-if="account.id == profile.id"
          title="Add a Keep"
        >
          <i class="mdi mdi-plus f-10" />
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
        await vaultsService.getVaultsByAccountId(route.params.id)
      } catch (error) {
        Pop.toast(error.message, 'error')
        logger.log(error)
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
.con {
  columns: 4 200px;
  column-gap: 1rem;
  div {
    width: 150px;
    background: #ec985a;
    color: white;
    margin: 0 1rem 1rem 0;
    display: inline-block;
    width: 100%;
  }
}
@media (max-width: 600px) {
  .container {
    column-count: 2;
  }
}
</style>