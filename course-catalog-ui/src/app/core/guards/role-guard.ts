import { inject } from '@angular/core';
import { CanActivateFn, Router } from '@angular/router';

import { AuthService } from '../services/auth';

export const roleGuard: CanActivateFn = (route, state) => {

  const authService = inject(AuthService);
  const router = inject(Router);

  const currentUser = authService.currentUser();

  if (!currentUser) {

    router.navigate(['/login']);

    return false;

  }

  const allowedRoles = route.data['roles'] as string[];

  if (allowedRoles.includes(currentUser.role)) {

    return true;

  }

  switch (currentUser.role) {

    case 'Admin':
      router.navigate(['/admin/dashboard']);
      break;

    case 'Instructor':
      router.navigate(['/instructor/dashboard']);
      break;

    case 'Student':
      router.navigate(['/student/dashboard']);
      break;

    default:
      router.navigate(['/login']);
      break;

  }

  return false;

};