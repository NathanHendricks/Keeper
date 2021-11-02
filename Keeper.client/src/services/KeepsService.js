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
        AppState.keeps = []
        const res = await api.get(`api/profiles/${profileId}/keeps`)
        logger.log('get all keeps on profile res', res.data)
        AppState.keeps = res.data.map(k => new Keep(k))
    }

    async createKeep(newkeep){
        const res = await api.post('api/keeps', newkeep)
        logger.log('created keep', res.data)
        AppState.keeps.unshift(new Keep(res.data))
    }
}
export const keepsService = new KeepsService()