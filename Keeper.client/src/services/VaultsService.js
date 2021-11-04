import { AppState } from "../AppState"
import { Keep } from "../Models/Keep"
import { Vault } from "../Models/Vault"
import { Vaultkeep } from "../Models/Vaultkeep"
import { logger } from "../utils/Logger"
import { api } from "./AxiosService"

class VaultsService{
    async getVaultsByProfileId(profileId){
        AppState.vaults = []
        const res = await api.get(`api/profiles/${profileId}/vaults`)
        logger.log('get all vaults on profile res', res.data)
        AppState.vaults = res.data.map(v => new Vault(v))
    }
    async getVaultsByAccountId(profileId){
        const res = await api.get(`api/profiles/${profileId}/vaults`)
        // logger.log('get all the users vault res', res.data)
        AppState.uservaults = res.data.map(u => new Vault(u))
        logger.log("after mapping uservaults", AppState.uservaults)
    }

    async getVaultById(vaultId){
        AppState.vault = null
        const res = await api.get(`api/vaults/${vaultId}`)
        logger.log('get the vault by its id', res.data)
        AppState.vault = new Vault(res.data)
    }
    async createVault(newVault){
        const res = await api.post('api/vaults', newVault)
        logger.log('created vault', res.data)
        AppState.vaults = [new Vault(res.data), ...AppState.vaults]
    }

    async createVaultkeep(newvaultkeep){
        const res = await api.post('api/vaultkeeps', newvaultkeep)
        logger.log('created vaultkeep', res.data)
        AppState.vaultkeeps =[new Vaultkeep(res.data), ...AppState.vaultkeeps]
    }

    async getKeepByVaultId(vaultId){
        AppState.keeps = []
        const res = await api.get(`api/vaults/${vaultId}/keeps`)
        logger.log('get the vaultkeeps', res.data)
        AppState.keeps = res.data.map(vk => new Keep(vk))
    }

    async removeVault(vaultId){
        const res = await api.delete(`api/vaults/${vaultId}`)
        logger.log('delete vault res', res)
        AppState.vaults = AppState.vaults.filter(v => v.id != vaultId)
        AppState.vaultkeeps = AppState.vaultkeeps.filter((vk => vk.id != vaultId))
    }

    async removeVaultkeep(vaultkeepId){
        const res = await api.delete(`api/vaultkeeps/${vaultkeepId}`)
        logger.log('delete vaultkeep res', res)
        AppState.vaultkeeps = AppState.vaultkeeps.filter(v => v.id != vaultkeepId)
    }
}

export const vaultsService = new VaultsService()