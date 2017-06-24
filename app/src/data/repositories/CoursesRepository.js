import axios from 'axios'
import queryString from 'query-string'

class CoursesRepository {
    static getCourses(page, limit) {
        return axios.get('/api/courses', queryString.stringify({page, limit}))
            .then(resp => resp.data)
    }
}

export default CoursesRepository