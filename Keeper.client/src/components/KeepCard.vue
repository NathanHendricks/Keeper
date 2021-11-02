<template>
  <div class="card p-0 my-2 selectable" @click="openModal()">
    <img :src="keep.img" class="card-img" alt="..." />
    <div
      class="
        d-flex
        justify-content-between
        align-items-end
        card-img-overlay
        text-dark
        lighten-25
        shadow
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
              <i class="mdi mdi-alpha-k-box-outline ps-2">: {{ keep.keeps }}</i>
            </p>
          </div>
          <div class="row mb-5">
            <h5>{{ keep.name }}</h5>
            <small>{{ keep.description }}</small>
          </div>
          <div class="row align-items-end">
            <span class="d-flex justify-content-between align-items-end">
              <button class="btn btn-primary">
                <small>add to vault</small>
              </button>
              <i class="mdi mdi-delete text-danger selectable" />
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
import { Modal } from 'bootstrap'
import { useRoute } from 'vue-router'
import { Keep } from "../Models/Keep"
import { Profile } from '../Models/Profile'
import { router } from '../router'
import { logger } from '../utils/Logger'
import Pop from '../utils/Pop'
export default {
  props: {
    keep: {
      type: Keep,
      default: () => { return new Keep() }
    }
    // profile: {
    //     type: Profile,
    //     default: () => { return new Profile() }
    // }
  },
  setup(props) {
    const route = useRoute()
    return {
      goToProfilePage(id) {
        try {
          logger.log(id)
          const modal = Modal.getOrCreateInstance(document.getElementById(`keep-modal-${props.keep.id}`))
          modal.hide()
          router.push({ name: 'Profile', params: { id: id } })
        } catch (error) {
          Pop.toast(error.message)
          logger.log(error)
        }
      },

      openModal() {
        const modal = Modal.getOrCreateInstance(document.getElementById(`keep-modal-${props.keep.id}`))
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