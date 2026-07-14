import { Component, computed, inject } from '@angular/core';
import { Router, RouterLink, RouterLinkActive } from '@angular/router';

import { MatIconModule } from '@angular/material/icon';

import { AuthService } from '../../../core/services/auth';

interface MenuItem {
  label: string;
  icon: string;
  route: string;
}

@Component({
  selector: 'app-sidebar',
  standalone: true,
  imports: [
    RouterLink,
    RouterLinkActive,
    MatIconModule
  ],
  templateUrl: './sidebar.html',
  styleUrl: './sidebar.css'
})
export class Sidebar {

  private router = inject(Router);

  protected authService = inject(AuthService);

  protected menuItems = computed<MenuItem[]>(() => {

    if (this.authService.isAdmin()) {

      // return [
      //   {
      //     label: 'Dashboard',
      //     icon: 'dashboard',
      //     route: '/admin/dashboard'
      //   },
      //   {
      //     label: 'Users',
      //     icon: 'group',
      //     route: '/admin/users'
      //   },
      //   {
      //     label: 'Courses',
      //     icon: 'school',
      //     route: '/admin/courses'
      //   }
      // ];

      return [

  {
    label: 'Dashboard',
    icon: 'dashboard',
    route: '/admin/dashboard'
  },

  {
    label: 'Users',
    icon: 'group',
    route: '/admin/users'
  },

  {
    label: 'Create Instructor',
    icon: 'person_add',
    route: '/admin/create-instructor'
  }

];

    }

    if (this.authService.isInstructor()) {

      return [
        {
          label: 'Dashboard',
          icon: 'dashboard',
          route: '/instructor/dashboard'
        },
        {
          label: 'My Courses',
          icon: 'menu_book',
          route: '/instructor/my-courses'
        },
        {
          label: 'Students',
          icon: 'groups',
          route: '/instructor/students'
        }
      ];

    }

    return [
      {
        label: 'Dashboard',
        icon: 'dashboard',
        route: '/student/dashboard'
      },
      {
        label: 'Browse Courses',
        icon: 'school',
        route: '/student/courses'
      },
      {
        label: 'My Enrollments',
        icon: 'assignment',
        route: '/student/enrollments'
      }
    ];

  });

  logout(): void {

    this.authService.logout();

    this.router.navigate(['/login']);

  }

}