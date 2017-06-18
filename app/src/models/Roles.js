/**
 * Default Roles
 */
export const GUEST = 'GUEST'
export const STUDENT = 'STUDENT'
export const TEACHER = 'TEACHER'
export const ADMIN = 'ADMIN'


export const defaultRoles = {
  [ADMIN]: [TEACHER, STUDENT, GUEST],
  [TEACHER]: [STUDENT, GUEST],
  [STUDENT]: [GUEST],
  [GUEST]: []
}
