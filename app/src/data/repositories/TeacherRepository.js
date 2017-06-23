import axios from 'axios'

class TeacherRepository {
    
    static getTeacher(id){
        return axios.get('/api/teachers/' + id)
            .then(resp => resp.data)
    }

}

export default TeacherRepository