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
}
export const keepsService = new KeepsService()