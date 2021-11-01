<template>
    <div class="card p-0 my-2 selectable" data-bs-toggle="modal" data-bs-target="#keep-modal">
        <img :src="keep.img" class="card-img" alt="..." />
        <div class="d-flex justify-content-between align-items-end card-img-overlay text-dark lighten-25 shadow">
        <small class="ps-1">{{ keep.name }}</small>
        <img :src="keep.creator.picture" class="creator-img" alt="..." />
        </div>
    </div>

    <!-- keep modal goes here -->
    <Modal id="keep-modal">
        <template #modal-body>
            <KeepModal/>
        </template>
    </Modal>
</template>


<script>
import { Keep } from "../Models/Keep"
import { Profile } from '../Models/Profile'
export default {
    props: {
        keep: {
            type: Keep,
            default: () => { return new Keep() }
        }
        // profile: {
        //     type: Profile,
        //     default: () => { return new Profile}
        // }
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