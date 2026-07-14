import { Component, inject, signal } from '@angular/core';

import { DashboardService } from '../../../core/services/dashboard';

import { InstructorDashboard } from '../../../core/models/dashboard.models';

import { StatCard } from '../../../shared/components/stat-card/stat-card';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [
    StatCard
  ],
  templateUrl: './dashboard.html',
  styleUrl: './dashboard.css'
})
export class Dashboard {

  private dashboardService = inject(DashboardService);

  protected dashboard = signal<InstructorDashboard | null>(null);

  constructor() {

    this.loadDashboard();

  }

  private loadDashboard(): void {

    this.dashboardService
      .getInstructorDashboard()
      .subscribe({

        next: (response) => {

          this.dashboard.set(response);

        },

        error: (error) => {

          console.error(error);

        }

      });

  }

}