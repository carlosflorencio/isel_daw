import axios from 'axios'

class CoursesRepository {
    static getCourses(page, limit) {
        return axios.get('/api/courses', { params: { page, limit } })
            .then(resp => resp.data)
    }

    static getCourse(id) {
        return axios.get('/api/courses/'+id)
            .then(resp => resp.data)
    }
}

export default CoursesRepository