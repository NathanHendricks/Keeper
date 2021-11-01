<template>
    <div class="container-fluid">
        <div class="row masonary">
            <!-- keeps go here  -->
            <KeepCard v-for="k in keeps" :key="k.id" :keep="k" class=" img"/>
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
.img {
    width: 25%;
    margin-bottom: 1em;
}
.masonary{
    padding: 1em;
    column-count: 4;
}
</style>