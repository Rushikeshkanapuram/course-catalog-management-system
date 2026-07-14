import { Component, inject } from '@angular/core';
import {
  FormBuilder,
  ReactiveFormsModule,
  Validators
} from '@angular/forms';

import {
  ActivatedRoute,
  Router,
  RouterLink
} from '@angular/router';

import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { MatSelectModule } from '@angular/material/select';
import { MatCardModule } from '@angular/material/card';

import { InstructorService } from '../services/instructor';

@Component({
  selector: 'app-edit-course',
  standalone: true,
  imports: [
    ReactiveFormsModule,
    RouterLink,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
    MatDatepickerModule,
    MatNativeDateModule,
    MatSelectModule,
    MatCardModule
  ],
  templateUrl: './edit-course.html',
  styleUrl: './edit-course.css'
})
export class EditCourse {

  private fb = inject(FormBuilder);

  private instructorService = inject(InstructorService);

  private router = inject(Router);

  private route = inject(ActivatedRoute);

  private courseId = Number(
    this.route.snapshot.paramMap.get('id')
  );

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

    startDate: this.fb.control<Date | null>(null, Validators.required),

endDate: this.fb.control<Date | null>(null, Validators.required),

    capacity: [30, [
      Validators.required,
      Validators.min(1)
    ]]

  });

  constructor() {

    this.loadCourse();

  }

  private loadCourse(): void {

    this.instructorService
      .getCourseById(this.courseId)
      .subscribe({

        next: (course) => {

          this.courseForm.patchValue({

            code: course.code,
            title: course.title,
            description: course.description,
            category: course.category,
            startDate: new Date(course.startDate),
            endDate: new Date(course.endDate),
            capacity: course.capacity

          });

        },

        error: (error) => {

          console.error(error);

        }

      });

  }

  updateCourse(): void {

    if (this.courseForm.invalid) {

      this.courseForm.markAllAsTouched();

      return;

    }

    const request = {

      ...this.courseForm.getRawValue(),

      instructorId: 0

    };

    this.instructorService
      .updateCourse(this.courseId, request)
      .subscribe({

        next: () => {

          this.router.navigate([
            '/instructor/my-courses'
          ]);

        },

        error: (error) => {

          console.error(error);

        }

      });

  }

}