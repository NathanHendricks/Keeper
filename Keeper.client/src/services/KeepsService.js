import { AppState } from "../AppState"
import { Keep } from "../Models/Keep"
import { logger } from "../utils/Logger"
import { api } from "./AxiosService"

class KeepsService{
    async getAll(){
        const res = await api.get('api/keeps')
        logger.log('get all keeps res', res)
        AppState.keeps = res.data.map(k => new Keep(k))
    }
}
export const keepsService = new KeepsService()