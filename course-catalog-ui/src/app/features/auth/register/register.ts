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
import { AuthService } from '../../../core/services/auth';
import { Logo } from '../../../shared/components/logo/logo';

@Component({
  selector: 'app-register',
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
  templateUrl: './register.html',
  styleUrl: './register.css'
})
export class Register {

  private fb = inject(FormBuilder);
  private authService = inject(AuthService);

  private router = inject(Router);
  registerForm = this.fb.group({

    firstName: ['', Validators.required],

    lastName: ['', Validators.required],

    email: ['', [
      Validators.required,
      Validators.email
    ]],

    password: ['', [
      Validators.required,
      Validators.minLength(6)
    ]]

  });

  register(): void {

  if (this.registerForm.invalid) {

    this.registerForm.markAllAsTouched();

    return;

  }

  this.authService
    .register(this.registerForm.getRawValue() as any)
    .subscribe({

      next: () => {

        alert('Registration successful. Please login.');

        this.router.navigate(['/login']);

      },

      error: (error) => {

        console.error(error);

        alert(error.error?.message ?? 'Registration failed.');

      }

    });

}

}