import { AppState } from "../AppState"
import { Keep } from "../Models/Keep"
import { Vault } from "../Models/Vault"
import { logger } from "../utils/Logger"
import { api } from "./AxiosService"

class VaultsService{
    async getVaultsByProfileId(profileId){
        AppState.vaults = []
        const res = await api.get(`api/profiles/${profileId}/vaults`)
        logger.log('get all vaults on profile res', res.data)
        AppState.vaults = res.data.map(v => new Vault(v))
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
    }
}

export const vaultsService = new VaultsService()