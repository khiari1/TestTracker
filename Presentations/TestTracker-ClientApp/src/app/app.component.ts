import { Component, OnInit } from '@angular/core';
import { PrimeNGConfig } from 'primeng/api';
import { TranslateService } from '@ngx-translate/core';
import { MsalBroadcastService, MsalService } from '@azure/msal-angular';
import { AuthenticationResult, EventMessage, EventType, InteractionStatus } from '@azure/msal-browser';
import { filter, firstValueFrom } from 'rxjs';
import { UserService } from './services/user.service';
@Component({
  selector: 'app-root',
  template: `
  <router-outlet *ngIf="!isIframe"></router-outlet>`,
})
export class AppComponent implements OnInit {
  isIframe = false;
  /**
   *
   */
  constructor(
    public translate: TranslateService,
    private authService: MsalService,
    private msalBroadcastService: MsalBroadcastService,
  ) {}


  async ngOnInit(): Promise<void> {

    this.translate.addLangs(['en', 'fr']);
    this.translate.setDefaultLang('en');
    
    
    let result = await firstValueFrom(this.msalBroadcastService.msalSubject$
      .pipe(
        filter((msg: EventMessage) => msg.eventType === EventType.LOGIN_SUCCESS)
      ));
        const payload = result.payload as AuthenticationResult;
        this.authService.instance.setActiveAccount(payload.account);
        //await this.userService.permissionFactory();
        this.isIframe = window !== window.parent && !window.opener;

    this.msalBroadcastService.inProgress$
      .pipe(
        filter((status: InteractionStatus) => status === InteractionStatus.None)
      )
      .subscribe(() => {
        this.setLoginDisplay();
      }); // Remove this line to use Angular Universal
  }

  setLoginDisplay() {
    //this.loginDisplay = this.authService.instance.getAllAccounts().length > 0;
  }
}
