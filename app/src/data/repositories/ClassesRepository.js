import axios from 'axios'

class ClassesRepository {
    static getCourseClasses(courseId, page, limit) {
        return axios.get('/api/courses/' + courseId + '/classes',
            { params: { page, limit } })
            .then(resp => resp.data)
    }

    static getClass(id) {
        return axios.get('/api/classes/' + id)
            .then(resp => resp.data)
    }
}

export default ClassesRepository