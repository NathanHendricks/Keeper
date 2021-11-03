<template>
  <div class="card p-0 my-2 selectable" @click="openModal()">
    <img :src="vaultkeep.img" class="card-img" alt="..." />
    <div
      class="
        d-flex
        justify-content-between
        align-items-end
        card-img-overlay
        text-white
        elevation-3
      "
    >
      <small class="ps-1">{{ vaultkeep.name }}</small>
      <div
        @click.stop="goToProfilePage(vaultkeep.creatorId)"
        class="selectable"
      >
        <img
          :src="vaultkeep.creator.picture"
          class="img-creator rounded-circle"
          alt="..."
        />
      </div>
    </div>
  </div>

  <!-- vaultkeep modal goes here -->
  <Modal :id="'vault-keep-modal-' + vaultkeep.id">
    <template #modal-body>
      <div class="row">
        <div class="col-6">
          <img
            :src="vaultkeep.img"
            alt="keeps img"
            class="img-fluid rounded elevation-2"
          />
        </div>
        <div class="col-6">
          <div class="row mb-5">
            <p>
              <i class="mdi mdi-eye">: {{ vaultkeep.views }}</i>
              <i class="mdi mdi-alpha-k-box-outline ps-2"
                >: {{ vaultkeep.keeps }}</i
              >
            </p>
          </div>
          <div class="row mb-5">
            <h5>{{ vaultkeep.name }}</h5>
            <small>{{ vaultkeep.description }}</small>
          </div>
          <div class="row align-items-end">
            <span class="d-flex justify-content-between align-items-end">
              <button class="btn btn-outline-info">
                <small>Remove from vault</small>
              </button>
              <i
                class="mdi mdi-delete text-danger selectable"
                v-if="account.id == vaultkeep.creatorId"
                @click="removeVaultkeep()"
                title="Remove this Vaultkeep"
              />
              <div
                @click="goToProfilePage(vaultkeep.creatorId)"
                class="selectable"
              >
                <img
                  :src="vaultkeep.creator.picture"
                  class="img-creator rounded-circle"
                  alt="..."
                />
              </div>
            </span>
          </div>
        </div>
      </div>
    </template>
  </Modal>
</template>


<script>
import { vaultsService } from '../services/VaultsService'
import { Vaultkeep } from '../Models/Vaultkeep'
import { computed } from '@vue/reactivity'
import { logger } from '../utils/Logger'
import { AppState } from '../AppState'
import { useRoute } from 'vue-router'
import { router } from '../router'
import { Modal } from 'bootstrap'
import Pop from '../utils/Pop'
export default {
  props: {
    vaultkeep: {
      type: Vaultkeep,
      default: () => { return new Vaultkeep() }
    }
  },
  setup(props) {
    const route = useRoute()
    return {
      account: computed(() => AppState.account),
      async removeVaultkeep() {
        try {
          const yes = await Pop.confirm('Are you sure <b>you</b> want to remove this <em>Keep</em>?')
          if (!yes) { return }
          const modal = Modal.getOrCreateInstance(document.getElementById(`keep-modal-${props.vaultkeep.id}`))
          modal.hide()
          await vaultsService.removeVaultkeep(props.vaultkeep.id)
          Pop.toast('Keep has been removed', 'success')
        } catch (error) {
          Pop.toast(error.message)
          logger.log(error)
        }
      },
      goToProfilePage(id) {
        try {
          logger.log(id)
          const modal = Modal.getOrCreateInstance(document.getElementById(`vault-keep-modal-${props.vaultkeep.id}`))
          modal.hide()
          router.push({ name: 'Profile', params: { id: id } })
        } catch (error) {
          Pop.toast(error.message, 'error')
          logger.log(error)
        }
      },
      openModal() {
        const modal = Modal.getOrCreateInstance(document.getElementById(`vault-keep-modal-${props.vaultkeep.id}`))
        modal.show()
      }

    }
  }
}
</script>


<style lang="scss" scoped>
.card-img {
  background-size: cover;
  background-position: center top;
}
.img-creator {
  height: 35px;
  width: 35px;
}
</style>