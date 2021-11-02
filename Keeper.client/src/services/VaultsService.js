import { AppState } from "../AppState"
import { Vault } from "../Models/Vault"

class VaultsService{
    getVaultsByProfileId(profileId){
        AppState.vaults = []
        const res = await api.get(`api/profiles/${profileId}/vaults`)
        logger.log('get all vaults on profile res', res.data)
        AppState.vaults = res.data.map (v => new Vault(v))
    }

    createVault(newVault){
        const res = await api.Post('api/vaults', newVault)
        logger.log('created vault', res.data)
        AppState.vaults.unshift(new Vault(res.data))
    }
}

export const vaultsService = new VaultsService()