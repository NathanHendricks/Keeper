<template>
    <div class="container-fluid px-2">
        <div class="row mb-5">
            <div class="col-md-12 d-flex align-items-center felx-row profile-header" v-if="profile">
                <img :src="profile.picture" alt="profile img" class="rounded size">
                <div class="col-md-12">
                <h3 class="ps-2">{{profile.name}}</h3>
                <em class="ps-2"> Vaults: {{vaults.length}} <br/> </em>
                <em class="ps-2"> Keeps: {{keeps.length}}  </em>
                </div>
            </div>
            <div v-else>
                <div class="spinner-border text-secondary" role="status">
                    <span class="visually-hidden">Loading...</span>
                    <p>loading....</p>
                </div>
            </div>
        </div>
        <div v-if="vaults > 0" >
            <div class="mb-5">
                <!-- vaults go here in line -->
                <h4>Vaults</h4> 
                <button class="btn btn-primary" data-bs-toggle="modal" data-bs-target="#new-vault-modal" title="Add a Vault">
                    <i mdi mdi-plus f-20/>
                </button>
                <!-- component goes here -->
            </div>
        </div>
        <div v-else>
            <div class="mb-5">
                <h5>No Vaults....</h5>
                <button class="btn btn-primary" data-bs-toggle="modal" data-bs-target="#new-vault-modal" title="Add a Vault">
                    <i mdi mdi-plus f-20/>
                </button>
            </div>
        </div>    
        <div v-if="keeps > 0" class="container">
            <div class="mb-5">
            <!-- keeps go here with masonary -->
            <h4>Keeps</h4>
            <button class="btn btn-primary" data-bs-toggle="modal" data-bs-target="#new-keep-modal" title="Add a Keep">
                <i mdi mdi-plus f-20/>
            </button>
            <!-- component goes here -->
            <KeepCard v-for="k in keeps" :key="k.id" :keep="k"/>
            </div>
        </div>
        <div v-else> 
            <div class="mb-5">
            <h5>No Keeps....</h5>
            <button class="btn btn-primary" data-bs-toggle="modal" data-bs-target="#new-keep-modal" title="Add a Keep">
                <i mdi mdi-plus f-20/>
            </button>
            </div>
        </div>
    </div>
    <!-- Vault Modal goes here-->
    <Modal id="new-vault-modal">
        <template #modal-title>
            <h3>New Vault</h3>
        </template>
        <template #modal-body>
            <VaultForm />
        </template>
    </Modal>
    <!-- Keep Modal goes here -->
    <Modal id="new-keep-modal">
        <template #modal-title>
            <h3>New Keep</h3>
        </template>
        <template #modal-body>
            <KeepForm />
        </template>
    </Modal>
</template>


<script>
import { computed, onMounted, watchEffect } from '@vue/runtime-core'
import { useRoute } from 'vue-router'
import { keepsService } from '../services/KeepsService'
import { vaultsService } from '../services/VaultsService'
import { profilesService } from '../services/ProfilesService'
import Pop from '../utils/Pop'
import { AppState } from '../AppState'
export default {
    setup(){
        onMounted(async() => {
            try {
                await profilesService.getProfileById(route.params.id)
                await keepsService.getKeepsByProfileId()
                await vaultsService.getVaultsByProfileId()
            } catch (error) {
                Pop.toast(error.message, 'error')
            }
        })
        const route = useRoute()
        async function getKeeps(){
            try {
                await keepsService.getKeepsByProfileId({ creatorId: route.params.id })
                await vaultsService.getVaultsByProfileId({ creatorId: route.params.id})
            } catch (error) {
                Pop.toast(error.message, 'error')
            }
        }
        watchEffect(async() => {
            if (route.params.id) {
                await profilesService.getProfileById(route.params.id)
                getKeeps()
            }
        })
        return {
            account: computed(() => AppState.account),
            profile: computed(() => AppState.profile),
            vaults: computed(() => AppState.vaults),
            keeps: computed(() => AppState.keeps)
        }
    }
}
</script>


<style lang="scss" scoped>
.size{
    height: 10vh;
    width: 10vw;
}
img {
    width: 100%;
    margin-bottom: 1em;
}
.container{
    padding: 1em;
    column-count: 4;
}
@media(max-width: 600px){
    .container{
        column-count: 2;
    }
}
</style>