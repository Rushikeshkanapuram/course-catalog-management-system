import { Component, inject, signal } from '@angular/core';
import { Router } from '@angular/router';

import { InstructorService } from '../services/instructor';
import { Course } from '../models/course.model';

@Component({
  selector: 'app-my-courses',
  standalone: true,
  imports: [],
  templateUrl: './my-courses.html',
  styleUrl: './my-courses.css'
})
export class MyCourses {

  private instructorService = inject(InstructorService);

  protected router = inject(Router);

  protected courses = signal<Course[]>([]);

  protected isLoading = signal(true);

  constructor() {

    this.loadCourses();

  }

  protected loadCourses(): void {

    this.instructorService
      .getMyCourses()
      .subscribe({

        next: (response) => {

          this.courses.set(response);

          this.isLoading.set(false);

        },

        error: (error) => {

          console.error(error);

          this.isLoading.set(false);

        }

      });

  }

  createCourse(): void {

    this.router.navigate(['/instructor/create-course']);

  }
  editCourse(id: number): void {

  this.router.navigate([
    '/instructor/edit-course',
    id
  ]);

}

viewStudents(courseId: number): void {

  this.router.navigate([
    '/instructor/students',
    courseId
  ]);

}

deleteCourse(courseId: number): void {

  if(!confirm('Delete this course?')){

    return;

  }

  this.instructorService
    .deleteCourse(courseId)
    .subscribe({

      next: () => {

        this.loadCourses();

      },

      error: error => {

        console.error(error);

      }

    });

}
}