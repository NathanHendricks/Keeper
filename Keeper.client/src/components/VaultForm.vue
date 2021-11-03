<template>
  <form @submit.prevent="handelSubmit()">
    <div class="form-group">
      <label for="name">Title</label>
      <input
        type="text"
        class="form-control bg-secondary"
        name="name"
        placeholder="Title..."
        v-model="editable.name"
        required
      />
      <label for="description">Description</label>
      <input
        type="textarea"
        class="form-control bg-secondary"
        name="description"
        placeholder="Vault Description..."
        v-model="editable.value"
      />
      <label for="isPrivate"> Private? </label>
      <input
        type="checkbox"
        class="ms-3 mt-3"
        name="isPrivate"
        v-model="editable.isPrivate"
      />
      <br /><small>Private Vaults can only be seen by you</small>
    </div>
    <div
      class="button-group d-flex justify-content-end align-items-end p-0 px-2"
    >
      <button type="submit" class="btn btn-primary selectable p-0 px-2">
        <p>create</p>
      </button>
    </div>
  </form>
</template>


<script>
import { ref } from '@vue/reactivity'
import { vaultsService } from '../services/VaultsService'
import { logger } from '../utils/Logger'
import Pop from '../utils/Pop'
export default {
  setup() {
    const editable = ref({})
    return {
      editable,
      async handelSubmit() {
        try {
          await vaultsService.createVault(editable.value)
          Pop.toast('Vault Created', 'success')
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
</style>