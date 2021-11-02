<template>
    <div class="card p-0 my-2 selectable" data-bs-toggle="modal" data-bs-target="#keep-modal">
        <img :src="keep.img" class="card-img" alt="..." />
        <div class="d-flex justify-content-between align-items-end card-img-overlay text-dark lighten-25 shadow">
            <small class="ps-1">{{ keep.name }}</small>
            <router-link :to="{name: Profile, params: {id: keep.CreatorId} }" class="selectable">
                <img :src="keep.creator.picture" class="creator-img" alt="..." />
            </router-link>
        </div>
    </div>

    <!-- keep modal goes here -->
    <Modal :id="'keep-modal-' + keep.id">
        <template #modal-body>
            <div class="row">
                <div class="col-6">
                    {{ keep.img }}
                </div>
                <div class="col-6">
                    <div class="row">
                    <i mdi mdi-eye>{{ keep.views }}</i> 
                    <i mdi mdi-alpha-k-box-outline>{{ keep.keeps }}</i>
                    </div>
                    <div class="row">
                        <h5>{{keep.name}}</h5>
                        <small>{{keep.description}}</small>
                    </div>
                    <div class="row">
                        <button>
                            <span>ADD TO VAULT</span>
                        </button>
                        <i mdi mdi-delete/>
                        <div>
                            <img :src="keep.creator.picture"
                            alt="user photo"
                            height="40"
                            class="rounded" />
                            <span class="mx-3 text-primary">{{ user.name }}</span>
                        </div>
                    </div>
                </div>
            </div>
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