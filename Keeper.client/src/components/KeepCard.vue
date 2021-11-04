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

    <!-- keep modal goes here -->
    <Modal :id="'keep-modal-' + keep.id">
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
                <i class="mdi mdi-alpha-k-box-outline ps-2"
                  >: {{ keep.keeps }}</i
                >
              </p>
            </div>
            <div class="row mb-5">
              <h5>{{ keep.name }}</h5>
              <small>{{ keep.description }}</small>
            </div>
            <div class="row align-items-end">
              <span class="d-flex justify-content-between align-items-end">
                <div class="dropdown">
                  <button
                    class="btn btn-outline-primary dropdown-toggle"
                    type="button"
                    id="dropdownmenu"
                    data-bs-toggle="dropdown"
                    aria-expanded="false"
                  >
                    <small>add to vault</small>
                  </button>
                  <ul class="dropdown-menu" aria-labelledby="dropdownmenu">
                    <li v-for="u in uservaults" :key="u" uservault="u">
                      {{ uservaults.name }}
                    </li>
                  </ul>
                </div>
                <i
                  class="mdi mdi-delete text-danger selectable"
                  v-if="account.id == keep.creatorId"
                  @click="removeKeep()"
                  title="Remove this Keep"
                />
                <div
                  @click="goToProfilePage(keep.creatorId)"
                  class="selectable"
                >
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
  </div>
</template>


<script>
import { Modal } from 'bootstrap'
import { useRoute } from 'vue-router'
import { Keep } from "../Models/Keep"
import { router } from '../router'
import { logger } from '../utils/Logger'
import Pop from '../utils/Pop'
import { computed } from '@vue/reactivity'
import { AppState } from '../AppState'
import { keepsService } from '../services/KeepsService'
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
      uservaults: computed(() => AppState.uservaults),
      async removeKeep() {
        try {
          const yes = await Pop.confirm('Are you sure <b>you</b> want to remove this <em>Keep</em>?')
          if (!yes) { return }
          const modal = Modal.getOrCreateInstance(document.getElementById(`keep-modal-${props.keep.id}`))
          modal.hide()
          await keepsService.removeKeep(props.keep.id)
          Pop.toast('Keep has been removed', 'success')
        } catch (error) {
          Pop.toast(error.message)
          logger.log(error)
        }
      },
      goToProfilePage(id) {
        try {
          logger.log(id)
          const modal = Modal.getOrCreateInstance(document.getElementById(`keep-modal-${props.keep.id}`))
          modal.hide()
          router.push({ name: 'Profile', params: { id: id } })
        } catch (error) {
          Pop.toast(error.message, 'error')
          logger.log(error)
        }
      },
      async openModal() {
        const modal = Modal.getOrCreateInstance(document.getElementById(`keep-modal-${props.keep.id}`))
        modal.show()
        try {
          await keepsService.getById(props.keep.id)
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
.card-img {
  background-size: cover;
  background-position: center top;
}
.img-creator {
  height: 35px;
  width: 35px;
}
</style>