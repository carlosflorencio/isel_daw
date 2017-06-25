class SirenHelpers {
    static getLink(sirenView, rel){
        let links = sirenView.links
        return links.filter(link => link.rel.includes(rel))
            .map(link => link.href)[0]
    }

    static getAction(sirenView, name){
        let actions = sirenView.actions
        return actions.filter(action => action.name.indexOf(name) >= 0)
            .map(link => link.href)[0]
    }

}

export default SirenHelpers