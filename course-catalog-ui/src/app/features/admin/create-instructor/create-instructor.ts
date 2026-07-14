import { Component, inject } from '@angular/core';

import {
  FormBuilder,
  ReactiveFormsModule,
  Validators
} from '@angular/forms';

import { Router } from '@angular/router';

import { AdminService } from '../services/admin';

@Component({
  selector: 'app-create-instructor',
  standalone: true,
  imports: [
    ReactiveFormsModule
  ],
  templateUrl: './create-instructor.html',
  styleUrl: './create-instructor.css'
})
export class CreateInstructor {

  private fb = inject(FormBuilder);

  private adminService = inject(AdminService);

  private router = inject(Router);

  instructorForm = this.fb.group({

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

  createInstructor(): void {

    if (this.instructorForm.invalid) {

      this.instructorForm.markAllAsTouched();

      return;

    }

    this.adminService
      .createInstructor(
        this.instructorForm.getRawValue() as any
      )
      .subscribe({

        next: () => {

          this.router.navigate([
            '/admin/users'
          ]);

        },

        error: error => {

          console.error(error);

        }

      });

  }

}