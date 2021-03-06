export class Keep{
    constructor(keepData){
        this.id = keepData.id
        this.vaultKeepId = keepData.vaultKeepId || null
        this.creatorId = keepData.creatorId
        this.name = keepData.name
        this.description = keepData.description
        this.img = keepData.img
        this.isDeleted = keepData.isDeleted
        this.views = keepData.views
        this.shares = keepData.shares
        this.keeps = keepData.keeps
        this.creator = keepData.creator || {}
        this.createdAt = keepData.createdAt
        this.updatedAt = keepData.updatedAt
    }
}