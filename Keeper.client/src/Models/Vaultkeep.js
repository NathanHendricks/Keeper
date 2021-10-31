export class Vaultkeep{
    constructor(vkData){
        this.id = vkData.id
        this.creatorId = vkData.creatorId
        this.vaultId = vkData.vaultId
        this.keepId = vkData.keepId
        this.creator = vkData.creator || {}
        this.keep = vkData.keep || {}
        this.vault = vkData.vault || {}
        this.createdAt = vkData.createdAt
        this.updatedAt = vkData.updatedAt
    }
}