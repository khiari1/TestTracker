import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { MonitoringDetailsComponent } from './monitoring-details.component';
import { AllMonitoringComponent } from './all-monitoring.component';
import { MonitoringFormComponent } from './monitoring-form.component';
import { PermissionGuard } from 'src/app/guard/auth-guard.service';

const routes: Routes = [
  {
    path: '',
    component: AllMonitoringComponent,
    canActivateChild: [PermissionGuard],
    data: {
      requiredPermission: 'Monitoring_Read',
    },
  },
  {
    path: 'detail',
    component: MonitoringDetailsComponent,
    canActivateChild: [PermissionGuard],
    data: {
      requiredPermission: 'Monitoring_Read',
    },
  },

  {
    path: ':id',
    component: MonitoringFormComponent,
    title: 'Edit Monitoring',
    canActivateChild: [PermissionGuard],
    data: {
      requiredPermission: 'Monitoring_ReadWrite',
    },
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class MonitoringRoutingModule {}
