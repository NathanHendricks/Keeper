<template>
    <div class="container">
        <div class="row">
            <!-- keeps go here  -->
            <Keep v-for="k in keeps" :key="k.id" :keep="k" class="col-md-4 shadow rounded p-0 m-2"/>
        </div>
    </div>
</template>


<script>
import { computed, watchEffect } from '@vue/runtime-core'
import Pop from '../utils/Pop'
import { logger } from '../utils/Logger'
import { AppState } from '../AppState'
import { keepsService } from '../services/KeepsService'
export default {
    setup(){
        watchEffect(() => {
            try {
                keepsService.getAll()
            } catch (error) {
                Pop.toast(error.message, 'error')
                logger.log(error)
            }
        })
        return {
            keeps: computed(() => AppState.keeps)
        }
    }
}
</script>


<style lang="scss" scoped>
</style>