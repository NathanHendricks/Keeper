<template>
  <div class="card p-0 my-2 selectable" @click="openModal()">
    <img :src="keep.img" class="card-img" alt="..." />
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
      <small class="ps-1">{{ keep.name }}</small>
      <div @click.stop="goToProfilePage(keep.creatorId)" class="selectable">
        <img
          :src="keep.creator.picture"
          class="img-creator rounded-circle"
          alt="..."
        />
      </div>
    </div>
  </div>

  <!-- vaultkeep modal goes here -->
  <Modal :id="'vault-keep-modal-' + keep.id">
    <template #modal-body>
      <div class="row">
        <div class="col-6">
          <img
            :src="keep.img"
            alt="keeps img"
            class="img-fluid rounded elevation-2"
          />
        </div>
        <div class="col-6">
          <div class="row mb-5">
            <p>
              <i class="mdi mdi-eye">: {{ keep.views }}</i>
              <i class="mdi mdi-alpha-k-box-outline ps-2">: {{ keep.keeps }}</i>
            </p>
          </div>
          <div class="row mb-5">
            <h5>{{ keep.name }}</h5>
            <small>{{ keep.description }}</small>
          </div>
          <div class="row align-items-end">
            <span class="d-flex justify-content-between align-items-end">
              <button
                class="btn btn-outline-info"
                v-if="account.id == keep.creatorId"
                @click="removeVaultkeep(keep.vaultKeepId)"
              >
                <small>Remove from vault</small>
              </button>
              <div @click="goToProfilePage(keep.creatorId)" class="selectable">
                <img
                  :src="keep.creator.picture"
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
import { logger } from '../utils/Logger'
import { computed } from '@vue/reactivity'
import { AppState } from '../AppState'
import { Keep } from '../Models/Keep'
import { useRoute } from 'vue-router'
import { router } from '../router'
import { Modal } from 'bootstrap'
import Pop from '../utils/Pop'
export default {
  props: {
    keep: {
      type: Keep,
      default: () => { return new Keep() }
    }
  },
  setup(props) {
    const route = useRoute()
    return {
      account: computed(() => AppState.account),
      async removeVaultkeep(id) {
        try {
          const yes = await Pop.confirm('Are you sure <b>you</b> want to remove this <em>Keep</em>?')
          if (!yes) { return }
          const modal = Modal.getOrCreateInstance(document.getElementById(`vault-keep-modal-${props.keep.id}`))
          modal.hide()
          await vaultsService.removeVaultkeep(id)
          Pop.toast('Keep has been removed', 'success')
        } catch (error) {
          Pop.toast(error.message)
          logger.log(error)
        }
      },
      goToProfilePage(id) {
        try {
          logger.log(id)
          const modal = Modal.getOrCreateInstance(document.getElementById(`vault-keep-modal-${props.keep.id}`))
          modal.hide()
          router.push({ name: 'Profile', params: { id: id } })
        } catch (error) {
          Pop.toast(error.message, 'error')
          logger.log(error)
        }
      },
      openModal() {
        const modal = Modal.getOrCreateInstance(document.getElementById(`vault-keep-modal-${props.keep.id}`))
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