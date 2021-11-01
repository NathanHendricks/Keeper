<template>
    <div class="card p-0 my-2">
        <img :src="keep.img" class="card-img" alt="..." />
        <div class="d-flex justify-content-between align-items-end card-img-overlay text-dark lighten-25 shadow">
        <small class="ps-1">{{ keep.name }}</small>
        <img :src="keep.creator.picture" class="creator-img" alt="..." />
        </div>
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
.card-img{
    background-size: cover;
    background-position: center top;
}
</style>