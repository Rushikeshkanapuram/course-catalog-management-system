import { Component, inject } from '@angular/core';
import {
  FormBuilder,
  ReactiveFormsModule,
  Validators
} from '@angular/forms';

import { Router } from '@angular/router';

import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { MatSelectModule } from '@angular/material/select';
import { MatCardModule } from '@angular/material/card';

import { InstructorService } from '../services/instructor';

@Component({
  selector: 'app-create-course',
  standalone: true,
  imports: [
    ReactiveFormsModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatDatepickerModule,
    MatNativeDateModule,
    MatSelectModule,
    MatCardModule
  ],
  templateUrl: './create-course.html',
  styleUrl: './create-course.css'
})
export class CreateCourse {

  private fb = inject(FormBuilder);

  private instructorService = inject(InstructorService);

  private router = inject(Router);

  categories = [
    'Programming',
    'Frontend',
    'Backend',
    'Database',
    'Cloud',
    'DevOps'
  ];

  courseForm = this.fb.group({

    code: ['', Validators.required],

    title: ['', Validators.required],

    description: ['', Validators.required],

    category: ['', Validators.required],

    startDate: ['', Validators.required],

    endDate: ['', Validators.required],

    capacity: [30, [Validators.required, Validators.min(1)]]

  });

  createCourse(): void {

    if (this.courseForm.invalid) {

      this.courseForm.markAllAsTouched();

      return;

    }

    const request = {

      ...this.courseForm.getRawValue(),

      instructorId: 0

    };

    this.instructorService
      .createCourse(request as any)
      .subscribe({

        next: () => {

          this.router.navigate(['/instructor/my-courses']);

        },

        error: (error) => {

          console.error(error);

        }

      });

  }

}