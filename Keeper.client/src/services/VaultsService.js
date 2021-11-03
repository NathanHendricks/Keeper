import { AppState } from "../AppState"
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

    async createVault(newVault){
        const res = await api.post('api/vaults', newVault)
        logger.log('created vault', res.data)
        AppState.vaults.unshift(new Vault(res.data))
    }

    async getKeepByVaultId(vaultId){
        
    }
}

export const vaultsService = new VaultsService()