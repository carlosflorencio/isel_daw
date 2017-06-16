/**
 * Default Roles
 */
export const GUEST = 'GUEST'
export const STUDENT = 'STUDENT'
export const TEACHER = 'TEACHER'


export const defaultRoles = {
  [TEACHER]: [STUDENT, GUEST],
  [STUDENT]: [GUEST],
  [GUEST]: []
}
