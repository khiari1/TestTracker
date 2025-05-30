import {NgModule} from '@angular/core';
import {RouterModule, Routes} from '@angular/router';
import {DashboardComponent} from './dashboard/dashboard.component';
import { MsalGuard, MsalInterceptor } from '@azure/msal-angular';
import { BrowserUtils } from '@azure/msal-browser';
import { MessageService } from 'primeng/api';
import { LoginComponent } from './Authentification/login/login.component';

const routes: Routes = [
  {
    path: 'login',
    component: LoginComponent,
  },
  {
    path: '',
    redirectTo: 'dashboard',
    pathMatch: 'full'
  },
  {
    path: 'dashboard',
    component: DashboardComponent,
    loadChildren: () =>
      import('./dashboard/dashboard-routing.module').then(
        (module) => module.DashboardRoutingModule
      ),
    canActivate: [MsalGuard],
  },
];

@NgModule({
  imports: [
    RouterModule.forRoot(routes, {
      // Don't perform initial navigation in iframes or popups
      initialNavigation:'enabledNonBlocking'
        // !BrowserUtils.isInIframe() && !BrowserUtils.isInPopup()
        //   ? 'enabledNonBlocking'
        //   : 'disabled', // Set to enabledBlocking to use Angular Universal
    }),
  ],
  exports: [RouterModule],
  providers: [MsalGuard, MessageService],
})
export class AppRoutingModule {}
