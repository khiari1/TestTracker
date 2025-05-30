import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { MonitoringWidgetComponent } from './monitoring-widget/monitoring-widget.component';
import { MonitoringComponent } from './monitoring/monitoring.component';
import { HangfireComponent } from './hangFire/hangfire.component';
import { PermissionGuard } from '../guard/auth-guard.service';
import { ConfirmationService, MessageService } from 'primeng/api';
import { UnauthorizedComponent } from './unauthorized/unauthorized.component';
import { UserComponent } from './settings/user/user.component';
import { SettingsV2Component } from './settings/settings-v2.component';
import { JobComponent } from './job/job.component';

const routes: Routes = [
  { path: '', redirectTo: 'widget', pathMatch: 'full' },
  {
    path: 'unauthorized',
    component: UnauthorizedComponent,
    title: 'Unauthorized',
  },
  {
    path: 'monitoring',
    component: MonitoringComponent,
    title: 'Monitoring details',
    loadChildren: () =>
      import('./monitoring/monitoring-routing.module').then(
        (module) => module.MonitoringRoutingModule
      ),
    canActivateChild: [PermissionGuard],
    data: {
      requiredPermission: 'Monitoring_Read',
    },
  },
  {
    path: 'job',
    component: JobComponent,
    title: 'Job',
    loadChildren: () =>
      import('./job/job-routing.module').then(
        (module) => module.jobRoutingModule
      ),
    canActivateChild: [PermissionGuard],
    data: {
      requiredPermission: 'Monitoring_Read',
    },
  },
  {
    path: 'widget',
    component: MonitoringWidgetComponent,
    title: 'Widget',
    canActivate: [PermissionGuard],
    data: {
      requiredPermission: 'Monitoring_Read',
    },
  },
  {
    path: 'hangFire',
    component: HangfireComponent,
    title: 'HangFire',
    canActivate: [PermissionGuard],
    data: {
      requiredPermission: 'Hangfire_Read',
    },
  },
  {
    path: 'users',
    component: UserComponent,
    title: 'Users',
    canActivateChild: [PermissionGuard],
    data: {
      requiredPermission: 'User_Read',
    },
  },
  {
    path: 'settings',
    component: SettingsV2Component,
    title: 'Settings',
    loadChildren: () =>
      import('./settings/settings-routing.module').then(
        (module) => module.SettingsRoutingModule
      ),
  },
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
   providers:[ConfirmationService,
    MessageService,
  ],
})
export class DashboardRoutingModule {}
