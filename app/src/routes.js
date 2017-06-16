import * as roles from './models/Roles'

import Teacher from './components/teachers/Teacher'
import Student from './components/students/Student'

export default [
    {
        path: '/teachers/:id',
        exact: true,
        component: Teacher,
        minRole: roles.TEACHER,
        routes: []
    },
    {
        path: '/students/:id',
        exact: true,
        component: Student,
        minRole: roles.STUDENT,
        routes: []
    },
]