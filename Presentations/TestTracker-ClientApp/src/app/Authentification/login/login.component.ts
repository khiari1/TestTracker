import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { MsalService } from '@azure/msal-angular';
import { AuthenticationResult } from '@azure/msal-browser';

@Component({
  selector: 'app-login',
  template: `
    <div class="flex align-items-center justify-content-center min-h-screen bg-blue-50">
      <div class="surface-card p-4 md:p-6 border-round-3xl shadow-2 w-full max-w-20rem flex flex-column align-items-center">
        <div class="text-primary font-bold text-2xl mb-4">Test Tracker</div>
        <h1 class="text-xl font-semibold mb-2">Bienvenue</h1>
        <p class="text-color-secondary mb-4">Connectez-vous pour accéder au tableau de bord</p>

        <button type="button"
                class="w-full p-2 border-none border-round-lg bg-blue-100 text-primary font-semibold flex align-items-center justify-content-center gap-2 cursor-pointer
                       hover:bg-blue-200 transition-colors"
                (click)="onLogin()"
                pButton>
          <svg class="mr-2" width="23" height="23" xmlns="http://www.w3.org/2000/svg" viewBox="0 0 23 23">
            <rect width="10" height="10" x="1" y="1" fill="#f25022" />
            <rect width="10" height="10" x="12" y="1" fill="#7fba00" />
            <rect width="10" height="10" x="1" y="12" fill="#00a4ef" />
            <rect width="10" height="10" x="12" y="12" fill="#ffb900" />
          </svg>
          Se connecter avec Microsoft
        </button>
      </div>
    </div>
  `
})
export class LoginComponent {
  constructor(private msalService: MsalService, private router: Router) {
    const accounts = this.msalService.instance.getAllAccounts();
    if (accounts.length > 0) {
      this.router.navigate(['/dashboard']);
    }
  }

  onLogin() {
    this.msalService.loginPopup({
      scopes: ['user.read']
    }).subscribe({
      next: (response: AuthenticationResult) => {
        this.msalService.instance.setActiveAccount(response.account);
        this.router.navigate(['/dashboard']);
      }
    });
  }
}
