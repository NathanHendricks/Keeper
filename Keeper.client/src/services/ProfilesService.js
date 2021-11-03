import { AppState } from "../AppState"
import { Profile } from "../Models/Profile"
import { logger } from "../utils/Logger"
import { api } from "./AxiosService"

class ProfilesService{
    async getProfileById(id){
        AppState.profile = null
        const res = await api.get(`api/profiles/${id}`)
        // logger.log('ProfileId', res.data)
        AppState.profile = res.data
    }
}

export const profilesService = new ProfilesService()