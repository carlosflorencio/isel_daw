import * as roles from './models/Roles'

import Teacher from './components/teachers/TeacherContainer'
import Student from './components/students/StudentContainer'
import Class from './components/classes/ClassPage'

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
  {
    path: '/classes/:id',
    component: Class,
    minRole: roles.STUDENT,
    routes: []
  }
]
