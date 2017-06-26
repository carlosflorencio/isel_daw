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

}

export default SirenHelpers