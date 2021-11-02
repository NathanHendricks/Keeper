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
            <div v-if="vaults > 0">
                <!-- vaults go here in line -->
                <h4>Vaults</h4><i mdi mdi-plus/>
                <!-- component goes here -->
            </div>
            <div v-else>
                <h5>No Vaults....</h5>
            </div>    
            <div v-if="keeps > 0">
                <!-- keeps go here with masonary -->
                <h4>Keeps</h4><i mdi mdi-plus/>
                <!-- component goes here -->
            </div>
            <div v-else> 
                <h5>No Keeps....</h5>
            </div>
    </div>
</template>


<script>
import { computed, onMounted, watchEffect } from '@vue/runtime-core'
import { useRoute } from 'vue-router'
import { keepsService } from '../services/KeepsService'
import Pop from '../utils/Pop'
import { AppState } from '../AppState'
export default {
    setup(){
        onMounted(async() => {
            try {
                await keepsService.getKeeps()
                await vaultsService.getVaults()
            } catch (error) {
                Pop.toast(error.message, 'error')
            }
        })
        const route = useRoute()
        async function getKeeps(){
            try {
                await keepsService.getKeeps({ creatorId: route.params.id })
                await vaultsService.getVaults({ creatorId: route.params.id})
            } catch (error) {
                Pop.toast(error.message, 'error')
            }
        }
        watchEffect(async() => {
            if (route.params.id) {
                await profilesService.getProfileById(route.params.id)
                getKeeps()
                getVaults()
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
</style>