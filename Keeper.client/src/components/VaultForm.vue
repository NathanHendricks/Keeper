<template>
    <form @submit.prevent="handelSubmit()">
        <div class="form-group">
            <label for="name">Title</label>
            <input type="text"
                class="form-control bg-secondary"
                name="name"
                placeholder="Title..."
                v-model="editable.name"
                required>
            <label for="description">Image Url</label>
            <input type="url"
                class="form-control bg-secondary"
                name="description"
                placeholder="URL..."
                v-model="editable.value">
            <label for="isPrivate"> Private? </label>
            <input type="checkbox"
                class="ms-3 mt-3"
                name="isPrivate"
                v-model="editable.isPrivate"
            >
            <small>Private Vaults can only be seen by you</small>
        </div>
        <div class="button-group d-flex justify-content-end align-items-end">
            <button type="submit" class="btn btn-primary selectable">
                <p>create</p>
            </button>
        </div>
    </form>
</template>


<script>
import { vaultsService } from '../services/VaultsService'
import { logger } from '../utils/Logger'
import Pop from '../utils/Pop'
export default {
    setup(){
        const editable = ref({})
        return {
            editable,
            async handelSubmit(){
                try {
                    await vaultsService.createVault(editable.value)
                    Pop.toast('Vault Created', 'success')
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

</style>