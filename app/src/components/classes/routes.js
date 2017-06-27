import * as roles from '../../models/Roles'

import ClassGroups from '../groups/ClassGroupsContainer'
import ClassStudents from '../students/ClassStudentsContainer'
import ClassTeachers from '../teachers/ClassTeachers'
import Group from '../groups/Group'


export default [
    {
        path: '/classes/:id/groups',
        exact: true,
        component: ClassGroups,
        minRole: roles.STUDENT,
        routes: []
    },
    {
        path: '/classes/:id/teachers',
        exact: true,
        component: ClassTeachers,
        minRole: roles.STUDENT,
        routes: []
    },
    {
        path: '/classes/:id/groups/:groupId',
        exact: true,
        component: Group,
        minRole: roles.STUDENT,
        routes: []
    },
    {
        path: '/classes/:id/students',
        exact: true,
        component: ClassStudents,
        minRole: roles.STUDENT,
        routes: []
    }
]