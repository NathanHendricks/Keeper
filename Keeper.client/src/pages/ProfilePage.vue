<template>
    <div class="container-fluid px-2">
        <div class="profile-header" v-if="profile">
            <img :src="profile.picture" alt="profile img" class="rounded size">
            <h3>{{profile.name}}</h3>
            <em>{{vaults.lenght}}</em>
            <em>{{keeps.lenght}}</em>
        </div>
        <div v-else>
            <div class="spinner-border text-secondary" role="status">
                <span class="visually-hidden">Loading...</span>
                <p>loading....</p>
            </div>
        </div>
        <div v-if="vaults" >
            <!-- vaults go here in line -->
            <h4>Vaults</h4>
            <button class="btn text-primary selectable" data-bs-toggle="modal" data-bs-target="#new-vault-modal" title="Add a Vault">
                <i mdi mdi-plus f-20/>
            </button>
            <!-- component goes here -->
        </div>
        <div v-else>
            <h5>No Vaults....</h5>
        </div>    
        <div v-if="keeps" class="container">
            <!-- keeps go here with masonary -->
            <h4>Keeps</h4>
            <button class="btn text-primary selectable" data-bs-toggle="modal" data-bs-target="#new-keep-modal" title="Add a Keep">
                <i mdi mdi-plus f-20/>
            </button>
            <!-- component goes here -->
            <KeepCard v-for="k in keeps" :key="k.id" :keep="k"/>
        </div>
        <div v-else> 
            <h5>No Keeps....</h5>
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
import Pop from '../utils/Pop'
import { AppState } from '../AppState'
export default {
    setup(){
        onMounted(async() => {
            try {
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
    height: 64px;
    width: 64px;
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