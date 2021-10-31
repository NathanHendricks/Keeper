<template>
    <div class="card my-2" style="width: 16rem">
        <img :src="keep.img" class="card-img" alt="..." />
        <small>{{ keep.name }}</small>
        <img :src="keep.creator.picture" class="creator-img" alt="..." />
    </div>
</template>


<script>
import { Keep } from "../Models/Keep"
export default {
    props: {
        keep: {
            type: Keep,
            default: () => { return new Keep() }
        }
    },
    setup(){
        return {}
    }
}
</script>


<style lang="scss" scoped>

</style>