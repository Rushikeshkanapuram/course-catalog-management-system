import { Component, inject, signal } from '@angular/core';

import { AdminService } from '../services/admin';
import { AdminDashboard } from '../models/admin-dashboard.model';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [],
  templateUrl: './dashboard.html',
  styleUrl: './dashboard.css'
})
export class Dashboard {

  private adminService = inject(AdminService);

  protected dashboard = signal<AdminDashboard | null>(null);

  protected loading = signal(true);

  constructor() {

    this.loadDashboard();

  }

  private loadDashboard(): void {

    this.adminService
      .getDashboard()
      .subscribe({

        next: response => {

          this.dashboard.set(response);

          this.loading.set(false);

        },

        error: error => {

          console.error(error);

          this.loading.set(false);

        }

      });

  }

}