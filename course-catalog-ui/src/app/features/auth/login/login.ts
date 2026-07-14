import { Component, inject } from '@angular/core';
import {
  FormBuilder,
  ReactiveFormsModule,
  Validators
} from '@angular/forms';

import { Router, RouterLink } from '@angular/router';

import { MatCardModule } from '@angular/material/card';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatFormFieldModule } from '@angular/material/form-field';

import { Logo } from '../../../shared/components/logo/logo';
import { AuthService } from '../../../core/services/auth';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [
    ReactiveFormsModule,
    RouterLink,
    MatCardModule,
    MatInputModule,
    MatButtonModule,
    MatFormFieldModule,
    Logo
  ],
  templateUrl: './login.html',
  styleUrl: './login.css'
})
export class Login {

  private fb = inject(FormBuilder);

  protected authService = inject(AuthService);

  protected router = inject(Router);

  loginForm = this.fb.group({

    email: ['', [
      Validators.required,
      Validators.email
    ]],

    password: ['', Validators.required]

  });

  login(): void {
      console.log('Login button clicked');
    

  if (this.loginForm.invalid) {

    this.loginForm.markAllAsTouched();

    return;

  }

  this.authService
    .login(this.loginForm.getRawValue() as any)
    .subscribe({

      next: (response) => {

        this.authService.saveUser(response);

        switch (response.role) {

          case 'Admin':
            this.router.navigate(['/admin/dashboard']);
            break;

          case 'Instructor':
            this.router.navigate(['/instructor/dashboard']);
            break;

          case 'Student':
            this.router.navigate(['/student/dashboard']);
            break;

          default:
            this.router.navigate(['/login']);
            break;

        }

      },

      error: (error) => {

        console.error(error);

      }

    });

}

}