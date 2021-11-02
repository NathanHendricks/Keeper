export class Profile {
    constructor(proData) {
        this.id = proData.id
        this.name = proData.name
        this.picture = proData.picture
        this.createdAt = proData.createdAt
        this.updatedAt = proData.updatedAt
    }
}