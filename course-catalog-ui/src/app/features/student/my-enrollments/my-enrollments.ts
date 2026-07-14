import { Component, inject, signal } from '@angular/core';
import { DatePipe } from '@angular/common';

import { StudentService } from '../services/student';
import { Enrollment } from '../models/enrollment.model';

@Component({
  selector: 'app-my-enrollments',
  standalone: true,
  imports: [
    DatePipe
  ],
  templateUrl: './my-enrollments.html',
  styleUrl: './my-enrollments.css'
})
export class MyEnrollments {

  private studentService = inject(StudentService);

  protected enrollments = signal<Enrollment[]>([]);

  protected loading = signal(true);

  constructor() {

    this.loadEnrollments();

  }

  loadEnrollments(): void {

    this.studentService
      .getMyEnrollments()
      .subscribe({

        next: response => {

          this.enrollments.set(response);

          this.loading.set(false);

        },

        error: error => {

          console.error(error);

          this.loading.set(false);

        }

      });

  }

  dropCourse(id: number): void {

    if (!confirm('Drop this course?')) {

      return;

    }

    this.studentService
      .dropCourse(id)
      .subscribe({

        next: () => {

          this.loadEnrollments();

        },

        error: error => {

          console.error(error);

        }

      });

  }

}