import { HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';

import { AuthService } from '../services/auth';

export const authInterceptor: HttpInterceptorFn = (req, next) => {

  console.log('Interceptor executed');

  const authService = inject(AuthService);

  const currentUser = authService.currentUser();

  console.log(currentUser);

  if (!currentUser) {
    return next(req);
  }

  if (
    req.url.includes('/auth/login') ||
    req.url.includes('/auth/register')
  ) {
    return next(req);
  }

  const clonedRequest = req.clone({

    setHeaders: {

      Authorization: `Bearer ${currentUser.token}`

    }

  });

  console.log(clonedRequest.headers.get('Authorization'));

  return next(clonedRequest);

};