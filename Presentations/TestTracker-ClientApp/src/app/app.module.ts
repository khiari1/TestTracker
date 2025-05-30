import { APP_INITIALIZER, NgModule, TRANSLATIONS } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { CommonModule } from '@angular/common';
import {
  HTTP_INTERCEPTORS,
  HttpClientModule,
  HttpClient,
} from '@angular/common/http';
import { PagesModule } from './dashboard/dashboard.module';
import { CheckboxModule } from 'primeng/checkbox';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { InputTextModule } from 'primeng/inputtext';
import { ButtonModule } from 'primeng/button';
import { RippleModule } from 'primeng/ripple';
import { MessagesModule } from 'primeng/messages';
import { BASEURL_CONFIG } from './config';
import { TranslateLoader, TranslateModule } from '@ngx-translate/core';
import {
  MSAL_GUARD_CONFIG,
  MSAL_INSTANCE,
  MSAL_INTERCEPTOR_CONFIG,
  MsalBroadcastService,
  MsalGuard,
  MsalInterceptor,
  MsalModule,
  MsalRedirectComponent,
} from '@azure/msal-angular';
import { NotificationService } from './services/notification.service';
import { MsalService } from '@azure/msal-angular';
import { CardModule } from 'primeng/card';
import { UserService } from './services/user.service';
import {
  MSALGuardConfigFactory,
  MSALInstanceFactory,
  MSALInterceptorConfigFactory,
} from './factories';
import { HttpLoaderFactory } from './shared/shared.module';
import { SideBarToggleService } from './tsing/services/side-bar-toggle.service';
import { environment } from 'src/environments/environment.prod';
import { LoginComponent } from './Authentification/login/login.component';

@NgModule({
  declarations: [AppComponent, LoginComponent],
  imports: [
    BrowserModule,
    CommonModule,
    AppRoutingModule,
    HttpClientModule,
    PagesModule,
    CheckboxModule,
    ReactiveFormsModule,
    FormsModule,
    InputTextModule,
    ButtonModule,
    RippleModule,
    MessagesModule,
    MsalModule,
    CardModule,

    TranslateModule.forRoot({
      loader: {
        provide: TranslateLoader,
        useFactory: HttpLoaderFactory,
        deps: [HttpClient],
      },
    }),
  ],
  providers: [
    { provide: BASEURL_CONFIG, useValue: environment.baseUrl },
    {
      provide: HTTP_INTERCEPTORS,
      useClass: MsalInterceptor,
      multi: true,
    },

    {
      provide: MSAL_INSTANCE,
      useFactory: MSALInstanceFactory,
    },
    {
      provide: MSAL_GUARD_CONFIG,
      useFactory: MSALGuardConfigFactory,
    },
    {
      provide: MSAL_INTERCEPTOR_CONFIG,
      useFactory: MSALInterceptorConfigFactory,
    },
    {
      provide: TRANSLATIONS,
      useValue: 'en',
    },

    {
      provide: APP_INITIALIZER,
      useFactory: (NotificationService: NotificationService) => () =>
        NotificationService.inisialize(),
      deps: [NotificationService],
      multi: true,
    },
    {
      provide: APP_INITIALIZER,
      useFactory: (userService: UserService) => () =>
        userService.permissionFactory(),
      deps: [UserService],
      multi: true,
    },
    NotificationService,
    MsalService,
    MsalGuard,
    MsalBroadcastService,
    SideBarToggleService,
  ],
  bootstrap: [AppComponent, MsalRedirectComponent],
})
export class AppModule {}
