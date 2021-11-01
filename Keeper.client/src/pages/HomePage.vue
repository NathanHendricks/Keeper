<template>
    <div class="container">
            <!-- keeps go here  -->
            <KeepCard v-for="k in keeps" :key="k.id" :keep="k"/>
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
img {
    width: 100%;
    margin-bottom: 1em;
}
.container{
    padding: 1em;
    column-count: 4;
}

@media(max-width: 800px){
    .container{
        column-count: 3;
    }
}
@media(max-width: 600px){
    .container{
        column-count: 2;
    }
}
@media(max-width: 400px){
    .container{
        column-count: 1;
    }
}
</style>