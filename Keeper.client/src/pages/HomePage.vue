<template>
  <div class="con ms-2">
    <!-- keeps go here  -->
    <KeepCard v-for="k in keeps" :key="k.id" :keep="k" />
  </div>
</template>


<script>
import { computed, watchEffect } from '@vue/runtime-core'
import Pop from '../utils/Pop'
import { logger } from '../utils/Logger'
import { AppState } from '../AppState'
import { keepsService } from '../services/KeepsService'
export default {
  setup() {
    watchEffect(() => {
      try {
        keepsService.getAll()
      } catch (error) {
        Pop.toast(error.message, 'error')
        logger.log(error)
      }
    })
    return {
      keeps: computed(() => AppState.keeps),
      uservaults: computed(() => AppState.uservaults)
    }
  }
}
</script>


<style lang="scss" scoped>
.con {
  columns: 6 200px;
  column-gap: 1rem;
  div {
    width: 150px;
    background: #ec985a;
    color: white;
    margin: 0 1rem 1rem 0;
    display: inline-block;
    width: 100%;
  }
}
@media (max-width: 600px) {
  .container {
    column-count: 2;
  }
}
</style>