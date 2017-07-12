class SirenHelpers {
    static getLink(sirenView, rel){
        let links = sirenView.links
        return links.filter(link => link.rel.includes(rel))
            .map(link => link.href)[0]
    }

    static getAction(sirenView, name){
        let actions = sirenView.actions
        var result = actions.filter(action => action.name.indexOf(name) >= 0)[0]
        return result
    }

    static getSubEntity(sirenView, rel){
        return sirenView.entities
            .find(entity => 
                entity.rel.find(item => item === rel)
            )
    }

    static createAxiosConfig(sirenView, actionName) {
        let action = SirenHelpers.getAction(sirenView, actionName)
        return {
            method: action.method,
            url: action.href
        }
    }

}

export default SirenHelpers