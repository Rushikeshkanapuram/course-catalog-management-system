import { Routes } from '@angular/router';

import { authGuard } from './core/guards/auth-guard';
import { roleGuard } from './core/guards/role-guard';

export const routes: Routes = [

  {
    path: '',
    redirectTo: 'login',
    pathMatch: 'full'
  },

  {
    path: 'login',
    loadComponent: () =>
      import('./features/auth/login/login')
        .then(m => m.Login)
  },

  {
    path: 'register',
    loadComponent: () =>
      import('./features/auth/register/register')
        .then(m => m.Register)
  },

  // ---------------- ADMIN ----------------

  {
    path: 'admin',

    loadComponent: () =>
      import('./shared/layouts/dashboard-layout/dashboard-layout')
        .then(m => m.DashboardLayout),

    canActivate: [authGuard, roleGuard],

    data: {
      roles: ['Admin']
    },

    children: [

  {
    path: 'dashboard',
    loadComponent: () =>
      import('./features/admin/dashboard/dashboard')
        .then(m => m.Dashboard)
  },

  {
    path: 'users',
    loadComponent: () =>
      import('./features/admin/users/users')
        .then(m => m.Users)
  },

  {
    path: 'create-instructor',
    loadComponent: () =>
      import('./features/admin/create-instructor/create-instructor')
        .then(m => m.CreateInstructor)
  }

]

  },

  // ---------------- INSTRUCTOR ----------------

  {
    path: 'instructor',

    loadComponent: () =>
      import('./shared/layouts/dashboard-layout/dashboard-layout')
        .then(m => m.DashboardLayout),

    canActivate: [authGuard, roleGuard],

    data: {
      roles: ['Instructor']
    },

    children: [

  {
    path: 'dashboard',
    loadComponent: () =>
      import('./features/instructor/dashboard/dashboard')
        .then(m => m.Dashboard)
  },

  {
    path: 'my-courses',
    loadComponent: () =>
      import('./features/instructor/my-courses/my-courses')
        .then(m => m.MyCourses)
  },

  {
    path: 'create-course',
    loadComponent: () =>
      import('./features/instructor/create-course/create-course')
        .then(m => m.CreateCourse)
  },

  {
    path: 'edit-course/:id',
    loadComponent: () =>
      import('./features/instructor/edit-course/edit-course')
        .then(m => m.EditCourse)
  },
 {
  path: 'students',
  loadComponent: () =>
    import('./features/instructor/students/students')
      .then(m => m.Students)
},

{
  path: 'students/:courseId',
  loadComponent: () =>
    import('./features/instructor/students/students')
      .then(m => m.Students)
},
]
  },
  

  // ---------------- STUDENT ----------------

  {
    path: 'student',

    loadComponent: () =>
      import('./shared/layouts/dashboard-layout/dashboard-layout')
        .then(m => m.DashboardLayout),

    canActivate: [authGuard, roleGuard],

    data: {
      roles: ['Student']
    },

    children: [

  {
    path: 'dashboard',
    loadComponent: () =>
      import('./features/student/dashboard/dashboard')
        .then(m => m.Dashboard)
  },

  {
    path: 'courses',
    loadComponent: () =>
      import('./features/student/browse-courses/browse-courses')
        .then(m => m.BrowseCourses)
  },

  {
    path: 'enrollments',
    loadComponent: () =>
      import('./features/student/my-enrollments/my-enrollments')
        .then(m => m.MyEnrollments)
  }

]

  },

  {
    path: '**',
    redirectTo: 'login'
  }

];