<template>
    <form @submit.prevent="handelSubmit()">
        <div class="form-group">
            <label for="name"> Title</label>
            <input type="text"
                class="form-control bg-secondary"
                name="name"
                placeholder="Title..."
                v-model="editable.name"
                required>
            <label for="img">Image Url</label>
            <input type="text"
                class="form-control bg-secondary"
                name="img"
                placeholder="URL..."
                v-model="editable.img">
            <label for="description">Description</label>
            <input type="textarea"
                class="form-control bg-secondary"
                name="description"
                placeholder="Keep Description..."
                v-model="editable.description">
        </div>
        <div class="button-group d-flex justify-content-end align-items-end mt-2 p-0 px-2">
            <button type="submit" class="btn btn-info selectable p-0 px-2">
                <p>Create</p>
            </button>
        </div>
    </form>
</template>


<script>
import { ref } from '@vue/reactivity'
import { keepsService } from '../services/KeepsService'
import { logger } from '../utils/Logger'
import Pop from '../utils/Pop'
export default {
    setup(){
        const editable = ref({})
        return {
            editable,
            async handelSubmit(){
                try {
                    await keepsService.createKeep(editable.value)
                    Pop.toast('Keep Created', 'success')
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