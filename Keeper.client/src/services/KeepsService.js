import { AppState } from "../AppState"
import { Keep } from "../Models/Keep"
import { logger } from "../utils/Logger"
import { api } from "./AxiosService"

class KeepsService{
    async getAll(){
        const res = await api.get('api/keeps')
        logger.log('get all keeps res', res.data)
        AppState.keeps = res.data.map(k => new Keep(k))
        logger.log("after appstate res", AppState.keeps)
    }

    async getKeepsByProfileId(profileId){
        AppState.vaultkeeps = []
        const res = await api.get(`api/profiles/${profileId}/keeps`)
        logger.log('get all keeps on profile res', res.data)
        AppState.vaultkeeps = res.data.map(k => new Keep(k))
    }

    createKeep(newkeep){
        const res = await api.Post('api/keep', newkeep)
        logger.log('created keep', res.data)
        AppState.keeps.unshift(new Keep(res.data))
    }
}
export const keepsService = new KeepsService()