import axios from 'axios'

class StudentsRepository {

    static getStudent(id) {
        return axios.get('/api/students/'+id)
            .then(resp => resp.data)
    }
}

export default StudentsRepository