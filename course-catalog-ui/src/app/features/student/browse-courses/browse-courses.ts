import { Component, inject, signal } from '@angular/core';
import { DatePipe } from '@angular/common';
import { StudentService } from '../services/student';
import { Course } from '../models/course.model';

@Component({
  selector: 'app-browse-courses',
  standalone: true,
imports: [
  DatePipe
],  templateUrl: './browse-courses.html',
  styleUrl: './browse-courses.css'
})
export class BrowseCourses {

  private studentService = inject(StudentService);

  protected courses = signal<Course[]>([]);

  protected loading = signal(true);

  constructor() {

    this.loadCourses();

  }

  private loadCourses(): void {

    this.studentService
      .getCourses()
      .subscribe({

        next: response => {

          this.courses.set(response);

          this.loading.set(false);

        },

        error: error => {

          console.error(error);

          this.loading.set(false);

        }

      });

  }

  enroll(courseId: number): void {

    this.studentService
      .enroll({
        courseId
      })
      .subscribe({

        next: () => {

          alert('Successfully enrolled!');

        },

        error: error => {

          console.error(error);

          alert('Enrollment failed.');

        }

      });

  }

}