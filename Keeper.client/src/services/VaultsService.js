import { AppState } from "../AppState"
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

    async getVaultById(vaultId){
        const res = await api.get(`api/vaults/${vaultId}`)
        logger.log('get the vault by its id', res.data)
        AppState.vault = new Vault(res.data)
    }
    async createVault(newVault){
        const res = await api.post('api/vaults', newVault)
        logger.log('created vault', res.data)
        AppState.vaults.unshift(new Vault(res.data))
    }

    async getKeepByVaultId(vaultId){
        AppState.vaultkeeps = []
        const res = await api.get(`api/vaults/${vaultId}/keeps`)
        logger.log('get the vaultkeeps', res.data)
        AppState.vaultkeeps = res.data.map(vk => new Vaultkeep(vk))
    }
}

export const vaultsService = new VaultsService()